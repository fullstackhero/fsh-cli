using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class Set
    {
        public string Key { get; set; } = null!;
        public double Score { get; set; }
        public string Value { get; set; } = null!;
        public DateTime? ExpireAt { get; set; }
    }
}
