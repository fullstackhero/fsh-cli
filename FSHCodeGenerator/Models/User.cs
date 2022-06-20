using System;
using System.Collections.Generic;

namespace FSHCodeGenerator.Models
{
    public partial class User
    {
        public User()
        {
            Userclaims = new HashSet<Userclaim>();
            Userlogins = new HashSet<Userlogin>();
            Userroles = new HashSet<Userrole>();
            Usertokens = new HashSet<Usertoken>();
        }

        public string Id { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string? ObjectId { get; set; }
        public string TenantId { get; set; } = null!;
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual ICollection<Userclaim> Userclaims { get; set; }
        public virtual ICollection<Userlogin> Userlogins { get; set; }
        public virtual ICollection<Userrole> Userroles { get; set; }
        public virtual ICollection<Usertoken> Usertokens { get; set; }
    }
}
