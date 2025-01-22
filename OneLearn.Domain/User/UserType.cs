using OneLearn.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneLearn.Domain.User
{
    public class UserType : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
