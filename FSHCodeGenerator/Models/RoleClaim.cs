using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class Roleclaim
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Group { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string TenantId { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }

        public virtual Role Role { get; set; } = null!;
    }
}
