using FluentValidation;
using OneLearn.Application.VoiceTranslation.DTOs.Common;
using OneLearn.Application.VoiceTranslation.Interface;

namespace OneLearn.Application.VoiceTranslation.DTOs.Passage.Request
{
    public class UpdatePassageRequest : LanguageBaseDto
    {
        public int id { get; set; }
    }


}
