using FluentValidation;
using OneLearn.Application.VoiceTranslation.DTOs.Passage.Request;
using OneLearn.Application.VoiceTranslation.Interface;

namespace OneLearn.Application.VoiceTranslation.DTOs.Passage.Validator
{
    public class UpdatePassageRequestValidator : AbstractValidator<UpdatePassageRequest>
    {
        private readonly IPassageRepository _passageRepository;
        private readonly ILanguageRepository _languageRepository;
        public UpdatePassageRequestValidator(IPassageRepository passageRepository, ILanguageRepository languageRepository)
        {
            _passageRepository = passageRepository;
            _languageRepository = languageRepository;

            Include(new IPassageDtoValidator(_languageRepository));

            RuleFor(p => p.id)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var passageExists = await _passageRepository.Exists(id);
                    return passageExists;
                })
                .WithMessage("{PropertyName} does not exist.");

        }
    }
}
