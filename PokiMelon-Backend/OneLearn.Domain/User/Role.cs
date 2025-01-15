using OneLearn.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace OneLearn.Domain.User
{
    public class Role : BaseModel
    {
        [Required]
        [MaxLength(250)]
        public string RoleName { get; set; }
        public string? Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        //public ICollection<UserRole> UserRoles { get; set; }
        //public ICollection<RolePrivilege> RolePrivileges { get; set; }
    }
}
