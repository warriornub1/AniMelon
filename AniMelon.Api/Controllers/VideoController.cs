using System.Diagnostics;
using System.Text;
using System.Text.Json;
using AniMelon.Application.Video.DTOs.Request;
using AniMelon.Application.Video.Services;
using Microsoft.AspNetCore.Mvc;

namespace AniMelon_Backend.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]

    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpPost]
        [RequestSizeLimit(30000000000)]

        public async Task<IActionResult> Upload([FromForm] FileUploadDto upload)
        {
            var response = await _videoService.ConvertVideo(upload);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> ExtractSubtitle([FromBody] SubtitleChoiceExtract subtitle)
        {
            var response = await _videoService.ExtractSubtitleToSrtAsync(subtitle);
            return Ok(response);
        }

        //[HttpPost]
        //public async Task<IActionResult> ExtractDefinition([FromBody] subtitleDefi)

    }
}
