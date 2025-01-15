using FluentValidation;
using OneLearn.Application.VoiceTranslation.DTOs.Passage.Request;
using OneLearn.Application.VoiceTranslation.Interface;

namespace OneLearn.Application.VoiceTranslation.DTOs.Passage.Validator
{
    public class CreatePassageRequestValidator : AbstractValidator<CreatePassageRequest>
    {
        private readonly ILanguageRepository _languageRepository;

        public CreatePassageRequestValidator(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
            Include(new IPassageDtoValidator(_languageRepository));
        }
    }
}
