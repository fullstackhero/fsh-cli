using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class Distributedlock
    {
        public string Resource { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
