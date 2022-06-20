using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class Jobstate
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; } = null!;
        public string? Reason { get; set; }
        public string? Data { get; set; }

        public virtual Job Job { get; set; } = null!;
    }
}
