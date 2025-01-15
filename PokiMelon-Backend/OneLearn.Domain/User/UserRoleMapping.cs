using OneLearn.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneLearn.Domain.User
{
    public class UserRoleMapping : BaseModel
    {
        public int UserId { get; set; }
        //public User User { get; set; }
        public int RoleId { get; set; }
        //public Role Role { get; set; }
    }
}
