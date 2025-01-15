using OneLearn.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace OneLearn.Domain.VoiceTranslation
{
    public class Language : BaseModel
    {
        public string language { get; set; }
        public string language_Code { get; set; }
    }
}
