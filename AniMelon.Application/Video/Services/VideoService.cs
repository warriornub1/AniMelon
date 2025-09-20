using System.Diagnostics;
using System.Globalization;
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
            var ffprobe = Path.Combine(_env.ContentRootPath, "FFmpeg", "ffprobe.exe");
            var file = upload.File;

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
                var filePath = Path.Combine(uploadsPath, $"temp.mkv");
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
                    subtitles = subtitleChoices,
                    duration = GetVideoDuration(ffprobe, filePath)
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> ExtractSubtitleToSrtAsync(SubtitleChoiceExtract subtitle)
        {
            var ffmpegPath = Path.Combine(_env.ContentRootPath, "FFmpeg", "ffmpeg.exe");

            var subtitleFileName = $"subtitle_{subtitle.Index}.srt";

            var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
            var subtitlePath = Path.Combine(_env.WebRootPath, "subtitles");
            var videoPath = Path.Combine(_env.WebRootPath, "uploads");
            var filePath = Path.Combine(uploadsPath, $"temp.mkv");

            if (Directory.Exists(subtitlePath))
                Directory.Delete(subtitlePath, true);


            if (!Directory.Exists(subtitlePath))
            {
                Directory.CreateDirectory(subtitlePath);
            }

            var outputFilePath = Path.Combine(subtitlePath, subtitleFileName);


            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = $"-i \"{filePath}\" -map 0:s:{subtitle.Index} -c:s srt \"{outputFilePath}\" -y",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            var errorBuilder = new StringBuilder();

            process.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                    errorBuilder.AppendLine(e.Data);
            };

            try
            {
                process.Start();
                process.BeginErrorReadLine();

                bool exited = await Task.Run(() => process.WaitForExit(10000)); // 10 seconds
                if (!exited)
                {
                    process.Kill();
                    throw new Exception("FFmpeg subtitle extraction timed out.");
                }

                if (process.ExitCode != 0)
                {
                    throw new Exception($"FFmpeg failed: {errorBuilder.ToString()}");
                }

                return "/subtitles/" + subtitleFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting subtitles: {ex.Message}");
                return null;
            }
            finally
            {
                process.Dispose();
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

        private async Task<List<SubtitleChoice>> GetSubtitleChoices(string ffmpegPath, string filePath)
        {
            var ffprobePath = Path.Combine(Path.GetDirectoryName(ffmpegPath), "ffprobe.exe");

            if (!System.IO.File.Exists(ffprobePath))
            {
                Console.WriteLine($"ffprobe not found at: {ffprobePath}");
                return new List<SubtitleChoice>();
            }

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffprobePath,
                    Arguments = $"-i \"{filePath}\" -show_streams -select_streams s -print_format json",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            try
            {
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

                bool exited = await Task.Run(() => process.WaitForExit(10000)); // 10 seconds
                if (!exited)
                {
                    process.Kill();
                    Console.WriteLine("ffprobe timed out while fetching subtitles.");
                    return new List<SubtitleChoice>();
                }

                if (process.ExitCode != 0)
                {
                    Console.WriteLine($"ffprobe error: {errorBuilder.ToString()}");
                    return new List<SubtitleChoice>();
                }

                var output = outputBuilder.ToString();
                if (string.IsNullOrEmpty(output))
                {
                    Console.WriteLine("ffprobe returned no subtitle output.");
                    return new List<SubtitleChoice>();
                }

                var result = new List<SubtitleChoice>();
                var json = JsonDocument.Parse(output);
                var streams = json.RootElement.GetProperty("streams").EnumerateArray();

                int subtitleIndex = 0;
                foreach (var stream in streams)
                {
                    var choice = new SubtitleChoice
                    {
                        Index = subtitleIndex,
                        Language = "unknown",
                        Title = ""
                    };

                    if (stream.TryGetProperty("tags", out var tags))
                    {
                        if (tags.TryGetProperty("language", out var lang))
                        {
                            choice.Language = lang.GetString();
                        }

                        if (tags.TryGetProperty("title", out var title))
                        {
                            choice.Title = title.GetString();
                        }
                    }

                    result.Add(choice);
                    subtitleIndex++;
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing subtitle info: {ex.Message}");
                return new List<SubtitleChoice>();
            }
            finally
            {
                process.Dispose();
            }
        }

        public TimeSpan? GetVideoDuration(string ffprobePath, string videoPath)
        {
            if (!File.Exists(videoPath))
                throw new FileNotFoundException("Video file not found.", videoPath);

            var processInfo = new ProcessStartInfo
            {
                FileName = ffprobePath, // path to ffprobe.exe
                Arguments = $"-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 \"{videoPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = new Process { StartInfo = processInfo })
            {
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (double.TryParse(output.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double seconds))
                {
                    return TimeSpan.FromSeconds(seconds);
                }

                return null;
            }
        }

    }
}
