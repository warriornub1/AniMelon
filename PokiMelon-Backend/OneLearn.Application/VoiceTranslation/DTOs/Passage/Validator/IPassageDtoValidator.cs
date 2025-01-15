using FluentValidation;
using OneLearn.Application.VoiceTranslation.DTOs.Common;
using OneLearn.Application.VoiceTranslation.Interface;

namespace OneLearn.Application.VoiceTranslation.DTOs.Passage.Validator
{
    public class IPassageDtoValidator : AbstractValidator<LanguageBaseDto>
    {
        private readonly ILanguageRepository _languageRepository;
        public IPassageDtoValidator(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;

            RuleFor(p => p.language_id)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var langaugeExists = await _languageRepository.Exists(id);
                    return langaugeExists;
                })
                .WithMessage("{PropertyName} does not exist.");

            RuleFor(p => p.passage)
                .NotNull()
                .WithMessage("{PropetyName} must be present")
                .MaximumLength(30000).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters");
        }
    }
}
