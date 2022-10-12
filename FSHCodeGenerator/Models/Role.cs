using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class Role
    {
        public Role()
        {
            Roleclaims = new HashSet<Roleclaim>();
            Userroles = new HashSet<Userrole>();
        }

        public string Id { get; set; } = null!;
        public string? Description { get; set; }
        public string TenantId { get; set; } = null!;
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }

        public virtual ICollection<Roleclaim> Roleclaims { get; set; }
        public virtual ICollection<Userrole> Userroles { get; set; }
    }
}
