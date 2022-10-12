using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class MyChannel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ChannelId { get; set; }
        public bool Favorite { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public Guid? DeletedBy { get; set; }

        public virtual Channel Channel { get; set; } = null!;
    }
}
