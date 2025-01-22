using OneLearn.Application.VoiceTranslation.DTOs.Language.Response;

namespace OneLearn.Application.VoiceTranslation.Services
{
    public interface ILanguageService
    {
        Task<IEnumerable<GetAllLanguageResponse>> GetAllLanguagesAsync();
    }
}
