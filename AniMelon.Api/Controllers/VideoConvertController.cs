using System.Diagnostics;
using AniMelon.Application.Video.DTOs.Request;
using AniMelon.Application.Video.Services;
using Microsoft.AspNetCore.Mvc;

namespace AniMelon_Backend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VideoConvertController : ControllerBase
    {
        //private readonly IWebHostEnvironment _env;

        //private static Process? ffmpegProcess;
        //public VideoConvertController(IWebHostEnvironment env)
        //{
        //    _env = env;
        //}

        //[HttpPost]
        //[RequestSizeLimit(int.MaxValue)]
        //public async Task<IActionResult> ConvertFileIntoMp4([FromForm] FileUploadDto upload)
        //{
        //    var file = upload.File;

        //    if (file == null || file.Length == 0)
        //        return BadRequest("File is missing.");

        //    // Save uploaded file
        //    var uploadsFolder = Path.Combine(_env.ContentRootPath, "uploads");
        //    Directory.CreateDirectory(uploadsFolder);
        //    var inputFilePath = Path.Combine(uploadsFolder, file.FileName);

        //    using (var stream = new FileStream(inputFilePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    // Output path
        //    var outputFileName = Path.GetFileNameWithoutExtension(file.FileName) + ".mp4";
        //    var outputFolder = Path.Combine(_env.ContentRootPath, "converted");
        //    Directory.CreateDirectory(outputFolder);
        //    var outputFilePath = Path.Combine(outputFolder, outputFileName);

        //    var ffmpegPath = Path.Combine(_env.ContentRootPath, "FFmpeg", "ffmpeg.exe");
        //    var ffprobePath = Path.Combine(_env.ContentRootPath, "FFmpeg", "ffprobe.exe");
        //    var args = $"-y -i \"{inputFilePath}\" -c:v libx264 -c:a aac -preset ultrafast \"{outputFilePath}\"";

        //    var process = new Process
        //    {
        //        StartInfo = new ProcessStartInfo
        //        {
        //            FileName = ffmpegPath,
        //            Arguments = args,
        //            RedirectStandardOutput = true,
        //            RedirectStandardError = true,
        //            UseShellExecute = false,
        //            CreateNoWindow = true
        //        }
        //    };

        //    process.Start();
        //    string stderr = await process.StandardError.ReadToEndAsync();
        //    process.WaitForExit();

        //    if (!System.IO.File.Exists(outputFilePath))
        //    {
        //        return StatusCode(500, $"FFmpeg failed.\n\n{stderr}");
        //    }

        //    // Return the converted video
        //    var contentType = "video/mp4";
        //    var fileBytes = await System.IO.File.ReadAllBytesAsync(outputFilePath);
        //    return File(fileBytes, contentType, outputFileName);

        //}

        //[HttpPost]
        //[RequestSizeLimit(int.MaxValue)]

        //public async Task<IActionResult> TranscodeToHls([FromForm] FileUploadDto upload)
        //{
        //    if (upload.File == null || upload.File.Length == 0)
        //    {
        //        return BadRequest("No file uploaded.");
        //    }

        //    var ffmpegPath = Path.Combine(_env.ContentRootPath, "FFmpeg", "ffmpeg.exe");
        //    string uploadsDir = Path.Combine("wwwroot", "uploads");
        //    Directory.CreateDirectory(uploadsDir);

        //    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.File.FileName);
        //    string inputFile = Path.Combine(uploadsDir, fileName);

        //    // Save uploaded file
        //    using (var stream = new FileStream(inputFile, FileMode.Create))
        //    {
        //        await upload.File.CopyToAsync(stream);
        //    }

        //    string outputDir = Path.Combine("wwwroot", "streams", Guid.NewGuid().ToString());
        //    Directory.CreateDirectory(outputDir);
        //    string outputM3U8 = Path.Combine(outputDir, "index.m3u8");

        //    string segmentPattern = Path.Combine(outputDir, "segment_%03d.ts");

        //    string arguments = $"-i \"{inputFile}\" -c:v libx264 -c:a aac -preset ultrafast " +
        //                       $"-f hls -hls_time 4 -hls_list_size 0 -hls_segment_filename \"{segmentPattern}\" " +
        //                       $"\"{outputM3U8}\"";

        //    var process = new Process
        //    {
        //        StartInfo = new ProcessStartInfo
        //        {
        //            FileName = ffmpegPath,
        //            Arguments = arguments,
        //            RedirectStandardOutput = true,
        //            RedirectStandardError = true,
        //            UseShellExecute = false,
        //            CreateNoWindow = true
        //        }
        //    };

        //    process.Start();
        //    string stderr = await process.StandardError.ReadToEndAsync();
        //    await process.WaitForExitAsync();

        //    if (process.ExitCode != 0)
        //    {
        //        return BadRequest($"FFmpeg failed: {stderr}");
        //    }

        //    string publicUrl = $"/streams/{Path.GetFileName(outputDir)}/index.m3u8";
        //    return Ok(new { streamUrl = publicUrl });

        //}


        //[HttpPost]
        //[RequestSizeLimit(int.MaxValue)]

        //public async Task<IActionResult> StreamFile([FromForm] FileUploadDto upload)
        //{
        //    var ffmpegPath = Path.Combine(_env.ContentRootPath, "FFmpeg", "ffmpeg.exe");

        //    var inputFile = Path.GetTempFileName() + ".mkv";
        //    string outputDir = Path.Combine("wwwroot", "streams", Guid.NewGuid().ToString());
        //    Directory.CreateDirectory(outputDir);

        //    var playlistPath = Path.Combine(outputDir, "index.m3u8");

        //    await using (var stream = new FileStream(inputFile, FileMode.Create))
        //    {
        //        await upload.File.CopyToAsync(stream);
        //    }

        //    var ffmpeg = new Process
        //    {
        //        StartInfo = new ProcessStartInfo
        //        {
        //            FileName = ffmpegPath,
        //            Arguments = $"-i \"{inputFile}\" -preset ultrafast -c:v libx264 -f hls -hls_time 2 -hls_list_size 5 -hls_flags delete_segments \"{playlistPath}\"",
        //            RedirectStandardOutput = true,
        //            RedirectStandardError = true,
        //            UseShellExecute = false,
        //            CreateNoWindow = true
        //        }
        //    };

        //    ffmpeg.Start();

        //    // Read stderr async to avoid blocking
        //    _ = Task.Run(async () =>
        //    {
        //        using var reader = ffmpeg.StandardError;
        //        string? line;
        //        while ((line = await reader.ReadLineAsync()) != null)
        //        {
        //            Console.WriteLine(line); // Optional: log or ignore
        //        }
        //    });

        //    // Wait for the first m3u8 playlist to appear (basic readiness check)
        //    var timeout = Task.Delay(10_000); // Optional timeout
        //    while (!System.IO.File.Exists(playlistPath))
        //    {
        //        if (timeout.IsCompleted) break;
        //        await Task.Delay(500);
        //    }

        //    return Ok(new { url = $"/streams/{Path.GetFileName(outputDir)}/index.m3u8" });

        //}



        //[HttpPost]
        //[RequestSizeLimit(int.MaxValue)] // 100 MB
        //public async Task<IActionResult> Upload([FromForm] FileUploadDto upload)
        //{
        //   string hlsFolder = Path.Combine("wwwroot", "hls");

        //    var file = upload.File;
        //    if (file == null || file.Length == 0)
        //        return BadRequest("No file uploaded");

        //    // Clean previous upload
        //    if (!Directory.Exists(hlsFolder))
        //        Directory.CreateDirectory(hlsFolder);

        //    // Delete old segments & playlist
        //    foreach (var f in Directory.GetFiles(hlsFolder))
        //        System.IO.File.Delete(f);

        //    // Save uploaded file
        //    string uploadedFilePath = Path.Combine("wwwroot", Guid.NewGuid() + Path.GetExtension(file.FileName));
        //    await using (var stream = new FileStream(uploadedFilePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    // Stop any previous FFmpeg process
        //    if (ffmpegProcess != null && !ffmpegProcess.HasExited)
        //    {
        //        ffmpegProcess.Kill();
        //        ffmpegProcess.Dispose();
        //    }

        //    // Start FFmpeg live streaming
        //    var ffmpegArgs = $"-re -i \"{uploadedFilePath}\" -preset ultrafast -g 48 -sc_threshold 0 " +
        //                     $"-f hls -hls_time 4 -hls_list_size 5 -hls_flags delete_segments " +
        //                     $"-hls_segment_filename \"{Path.Combine(hlsFolder, "segment_%03d.ts")}\" " +
        //                     $"{Path.Combine(hlsFolder, "playlist.m3u8")}";

        //    var ffmpegPath = Path.Combine(_env.ContentRootPath, "FFmpeg", "ffmpeg.exe");

        //    ffmpegProcess = new Process
        //    {
        //        StartInfo = new ProcessStartInfo
        //        {
        //            FileName = ffmpegPath,
        //            Arguments = ffmpegArgs,
        //            UseShellExecute = false,
        //            RedirectStandardError = true,
        //            CreateNoWindow = true,
        //        }
        //    };

        //    ffmpegProcess.ErrorDataReceived += (sender, e) =>
        //    {
        //        if (!string.IsNullOrEmpty(e.Data))
        //            Console.WriteLine("[FFmpeg] " + e.Data);
        //    };

        //    ffmpegProcess.Start();
        //    ffmpegProcess.BeginErrorReadLine();

        //    Task.Delay(500);
        //    return Ok(new { message = "File uploaded and live streaming started" });
        //}

        //[HttpGet]
        //public async Task<IActionResult> KillProcess()
        //{
        //    ffmpegProcess.Kill();
        //    ffmpegProcess.Dispose();
        //    return Ok();
        //}
    }
}
