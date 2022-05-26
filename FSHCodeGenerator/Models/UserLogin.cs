using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class UserLogin
    {
        public string Id { get; set; } = null!;
        public string LoginProvider { get; set; } = null!;
        public string ProviderKey { get; set; } = null!;
        public string? ProviderDisplayName { get; set; }
        public string UserId { get; set; } = null!;
        public string TenantId { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
