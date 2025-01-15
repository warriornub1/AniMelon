using OneLearn.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneLearn.Domain.VoiceTranslation
{
    public class Passage : BaseModel
    {
        public int langauge_id { get; set; }

        public string passage { get; set; }
    }
}
