using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class AuditTrail
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Type { get; set; }
        public string? TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? AffectedColumns { get; set; }
        public string? PrimaryKey { get; set; }
        public string TenantId { get; set; } = null!;
    }
}
