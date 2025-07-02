using System.Diagnostics;
using System.Text;
using System.Text.Json;
using AniMelon.Application.Video.DTOs.Request;
using AniMelon.Application.Video.DTOs.Response;

namespace AniMelon.Application.Video.Services
{
    public class VideoService : IVideoService
    {
        private readonly IWebHostEnvironment _env;

        public VideoService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<VideoConvertResponse> ConvertVideo(FileUploadDto upload)
        {
            var ffmpegPath = Path.Combine(_env.ContentRootPath, "FFmpeg", "ffmpeg.exe");
            var file = upload.File;

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (!file.FileName.EndsWith(".mkv", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Only MKV files are supported.");
            }

            try
            {
                // Create directories for uploads and HLS output
                var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
                var hlsPath = Path.Combine(_env.WebRootPath, "hls");

                if (Directory.Exists(uploadsPath))
                    Directory.Delete(uploadsPath, true);

                if (Directory.Exists(hlsPath))
                    Directory.Delete(hlsPath, true);


                Directory.CreateDirectory(uploadsPath);
                Directory.CreateDirectory(hlsPath);

                // Save the uploaded file
                var fileId = Guid.NewGuid().ToString();
                var filePath = Path.Combine(uploadsPath, $"{fileId}.mkv");
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var subtitleChoices = await GetSubtitleChoices(ffmpegPath, filePath);
                //var subtitleMap = subtitleChoices.Any() ? "-map s" : ""; // Include all subtitle tracks if available

                // Get Japanese audio track index
                var japaneseAudioIndex = await GetJapaneseAudioTrackIndex(ffmpegPath, filePath);
                var audioMap = japaneseAudioIndex.HasValue ? $"-map a:{japaneseAudioIndex.Value}" : "-map a:0"; // Fallback to first audio track

                // Start FFmpeg conversion to HLS with selected audio track
                var hlsOutputPath = hlsPath;
                Directory.CreateDirectory(hlsOutputPath);
                var m3u8Path = Path.Combine(hlsOutputPath, "playlist.m3u8");

                // Log FFmpeg output for debugging
                var logPath = Path.Combine(hlsOutputPath, "ffmpeg.log");
                var ffmpegProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = ffmpegPath,
                        Arguments = $"-i \"{filePath}\" -map v:0 {audioMap} -c:v libx264 -preset ultrafast -c:a aac -f hls -hls_time 2 -hls_list_size 0 -hls_flags delete_segments+append_list -hls_segment_filename \"{hlsOutputPath}/segment%d.ts\" \"{m3u8Path}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                // Capture FFmpeg output
                var errorLog = new StringBuilder();
                ffmpegProcess.ErrorDataReceived += (sender, e) =>
                {
                    if (e.Data != null) errorLog.AppendLine(e.Data);
                };

                ffmpegProcess.Start();
                ffmpegProcess.BeginErrorReadLine();

                // Write FFmpeg logs asynchronously
                _ = Task.Run(async () =>
                {
                    await ffmpegProcess.WaitForExitAsync();
                    if (ffmpegProcess.ExitCode != 0)
                    {
                        await System.IO.File.WriteAllTextAsync(logPath, errorLog.ToString());
                    }
                    // Clean up the original file after FFmpeg completes
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch { /* Ignore cleanup errors */ }
                });

                // Return the HLS playlist URL immediately
                var hlsUrl = $"/hls/playlist.m3u8";

                return new VideoConvertResponse
                {
                    hlsUrl = hlsUrl,
                    subtitles = subtitleChoices
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // Helper method to find the Japanese audio track index
        private async Task<int?> GetJapaneseAudioTrackIndex(string ffmpegPath, string filePath)
        {
            var ffprobePath = Path.Combine(Path.GetDirectoryName(ffmpegPath), "ffprobe.exe");

            // Validate ffprobe executable exists
            if (!System.IO.File.Exists(ffprobePath))
            {
                Console.WriteLine($"ffprobe not found at: {ffprobePath}");
                return null;
            }

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffprobePath,
                    Arguments = $"-i \"{filePath}\" -show_streams -select_streams a -print_format json",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            try
            {
                // Capture output and error streams concurrently
                var outputBuilder = new StringBuilder();
                var errorBuilder = new StringBuilder();

                process.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data != null) outputBuilder.AppendLine(e.Data);
                };
                process.ErrorDataReceived += (sender, e) =>
                {
                    if (e.Data != null) errorBuilder.AppendLine(e.Data);
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // Wait for the process to exit with a timeout (e.g., 10 seconds)
                bool exited = await Task.Run(() => process.WaitForExit(10000)); // 10-second timeout
                if (!exited)
                {
                    process.Kill();
                    Console.WriteLine("ffprobe timed out after 10 seconds.");
                    return null;
                }

                if (process.ExitCode != 0)
                {
                    Console.WriteLine($"ffprobe failed with exit code {process.ExitCode}: {errorBuilder.ToString()}");
                    return null;
                }

                var output = outputBuilder.ToString();
                if (string.IsNullOrEmpty(output))
                {
                    Console.WriteLine("ffprobe produced no output.");
                    return null;
                }

                // Parse JSON output
                var json = JsonDocument.Parse(output);
                var streams = json.RootElement.GetProperty("streams").EnumerateArray();
                int index = 0;
                foreach (var stream in streams)
                {
                    if (stream.TryGetProperty("tags", out var tags) &&
                        tags.TryGetProperty("language", out var lang) &&
                        (lang.GetString() == "jpn" || lang.GetString() == "ja"))
                    {
                        return index;
                    }
                    index++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ffprobe process: {ex.Message}");
                return null;
            }
            finally
            {
                process.Dispose();
            }

            return null; // No Japanese track found
        }

        // Helper method to extract subtitle choices
        private async Task<List<SubtitleChoice>> GetSubtitleChoices(string ffmpegPath, string filePath)
    }
}
