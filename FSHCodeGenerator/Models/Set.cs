using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class Set
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public float Score { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}
