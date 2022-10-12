using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class Usertoken
    {
        public string UserId { get; set; } = null!;
        public string LoginProvider { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Value { get; set; }
        public string TenantId { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
