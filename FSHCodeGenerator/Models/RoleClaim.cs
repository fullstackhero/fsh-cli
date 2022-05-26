using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class RoleClaim
    {
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string TenantId { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }

        public virtual Role Role { get; set; } = null!;
    }
}
