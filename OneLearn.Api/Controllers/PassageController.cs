using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OneLearn.Application.Response;
using OneLearn.Application.VoiceTranslation.DTOs.Passage.Request;
using OneLearn.Application.VoiceTranslation.Services;

namespace OneLearn.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PassageController : ControllerBase
    {
        private readonly IPassageService _passageService;
        private APIResponse _apiResponse; 
        public PassageController(IPassageService passageService, IMemoryCache cache)
        {
            _passageService = passageService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllPassage()
        {
            var result = await _passageService.GetAllPassageAsync();
            _apiResponse.Data = result;
            _apiResponse.Status = true;
            _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_apiResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPassageById(int id)
        {
            var result = await _passageService.GetPassageByIdAsync(id);
            _apiResponse.Data = result;
            _apiResponse.Status = true;
            _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_apiResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> CreatePassage([FromBody] CreatePassageRequest request)
        {
            await _passageService.CreatePassageAsync("asd", request);
            _apiResponse.Data = request;
            _apiResponse.Status = true;
            _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> UpdatePassage([FromBody] CreatePassageRequest request)
        {
            await _passageService.CreatePassageAsync("asd", request);
            return NoContent();
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[HttpDelete]
        //public async Task<IActionResult> DeletePassage()
        //{
        //    await _passageService.CreatePassageAsync("asd");
        //    _apiResponse.Data = true;
        //    _apiResponse.Status = true;
        //    _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
        //    return Ok(_apiResponse);
        //}
    }
}
