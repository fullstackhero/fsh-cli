using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class Job
    {
        public Job()
        {
            Jobparameters = new HashSet<Jobparameter>();
            Jobstates = new HashSet<Jobstate>();
            States = new HashSet<State>();
        }

        public int Id { get; set; }
        public int? StateId { get; set; }
        public string? StateName { get; set; }
        public string InvocationData { get; set; } = null!;
        public string Arguments { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpireAt { get; set; }

        public virtual ICollection<Jobparameter> Jobparameters { get; set; }
        public virtual ICollection<Jobstate> Jobstates { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}
