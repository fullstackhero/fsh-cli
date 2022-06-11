using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class Tenant
    {
        public string Id { get; set; } = null!;
        public string Identifier { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
        public string AdminEmail { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime ValidUpto { get; set; }
        public string? Issuer { get; set; }
    }
}
