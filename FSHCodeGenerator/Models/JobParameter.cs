using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class Jobparameter
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Name { get; set; } = null!;
        public string? Value { get; set; }

        public virtual Job Job { get; set; } = null!;
    }
}
