using Microsoft.AspNetCore.Mvc;
using OneLearn.Application.VoiceTranslation.Services;

namespace OneLearn.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        public LanguageController(ILanguageService languageService)
        {
           _languageService = languageService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllLanguages()
        {
            var language = await _languageService.GetAllLanguagesAsync();
            return Ok(language);
        }
    }
}
