using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class UserRole
    {
        public string UserId { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public string TenantId { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
