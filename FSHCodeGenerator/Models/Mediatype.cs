﻿using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class MediaType
    {
        public MediaType()
        {
            Channels = new HashSet<Channel>();
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

        public virtual ICollection<Channel> Channels { get; set; }
    }
}
