using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class TheParent
    {
        public TheParent()
        {
            TheChildren = new HashSet<TheChild>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public Guid? DeletedBy { get; set; }

        public virtual ICollection<TheChild> TheChildren { get; set; }
    }
}
