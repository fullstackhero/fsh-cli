using System;
using System.Collections.Generic;
using FSHCodeGenerator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FSHCodeGenerator.Context

{
    public partial class SourceGenContext : DbContext
    {
        public SourceGenContext()
        {
        }

        public SourceGenContext(DbContextOptions<SourceGenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aggregatedcounter> Aggregatedcounters { get; set; } = null!;
        public virtual DbSet<Audittrail> Audittrails { get; set; } = null!;
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Channel> Channels { get; set; } = null!;
        public virtual DbSet<Counter> Counters { get; set; } = null!;
        public virtual DbSet<Distributedlock> Distributedlocks { get; set; } = null!;
        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; } = null!;
        public virtual DbSet<Hash> Hashes { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Jobparameter> Jobparameters { get; set; } = null!;
        public virtual DbSet<Jobqueue> Jobqueues { get; set; } = null!;
        public virtual DbSet<Jobstate> Jobstates { get; set; } = null!;
        public virtual DbSet<List> Lists { get; set; } = null!;
        public virtual DbSet<MediaType> MediaTypes { get; set; } = null!;
        public virtual DbSet<MyChannel> MyChannels { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Roleclaim> Roleclaims { get; set; } = null!;
        public virtual DbSet<Server> Servers { get; set; } = null!;
        public virtual DbSet<Set> Sets { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;
        public virtual DbSet<Tenant> Tenants { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Userclaim> Userclaims { get; set; } = null!;
        public virtual DbSet<Userlogin> Userlogins { get; set; } = null!;
        public virtual DbSet<Userrole> Userroles { get; set; } = null!;
        public virtual DbSet<Usertoken> Usertokens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{

            //    optionsBuilder.UseMySQL("server=localhost;user=root;password=MySqlHeyd6941;database=AppingWebCollection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                ;

            modelBuilder.Entity<Aggregatedcounter>(entity =>
            {
                entity.ToTable("aggregatedcounter");

               // entity.UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Key, "IX_CounterAggregated_Key")
                    .IsUnique();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key).HasMaxLength(100);
            });

            modelBuilder.Entity<Audittrail>(entity =>
            {
                entity.ToTable("audittrails");

                entity.Property(e => e.Id)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.DateTime).HasMaxLength(6);

                entity.Property(e => e.TenantId).HasMaxLength(64);

                entity.Property(e => e.UserId)
                    .UseCollation("ascii_general_ci")
                    ;
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("brands");

                entity.Property(e => e.Id)
                    .UseCollation("ascii_general_ci");

                entity.Property(e => e.CreatedBy)
                    .UseCollation("ascii_general_ci");

                entity.Property(e => e.CreatedOn).HasMaxLength(6);

                entity.Property(e => e.DeletedBy)
                    .UseCollation("ascii_general_ci");

                entity.Property(e => e.DeletedOn).HasMaxLength(6);

                entity.Property(e => e.LastModifiedBy)
                    .UseCollation("ascii_general_ci");
                    

                entity.Property(e => e.LastModifiedOn).HasMaxLength(6);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.TenantId).HasMaxLength(64);
            });

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.ToTable("channels");

                entity.HasIndex(e => e.MediaTypeId, "IX_Channels_MediaTypeId");

                entity.Property(e => e.Id)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.CreatedBy)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.CreatedOn).HasMaxLength(6);

                entity.Property(e => e.DeletedBy)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.DeletedOn).HasMaxLength(6);

                entity.Property(e => e.LastModifiedBy)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.LastModifiedOn).HasMaxLength(6);

                entity.Property(e => e.MediaTypeId)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.HasOne(d => d.MediaType)
                    .WithMany(p => p.Channels)
                    .HasForeignKey(d => d.MediaTypeId)
                    .HasConstraintName("FK_Channels_MediaTypes_MediaTypeId");
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.ToTable("counter");

              //  entity.HasCharSet("utf8")
              //      .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Key, "IX_Counter_Key");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key).HasMaxLength(100);
            });

            modelBuilder.Entity<Distributedlock>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("distributedlock");

                //entity.HasCharSet("utf8")
                //    .UseCollation("utf8_general_ci");

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.Property(e => e.Resource).HasMaxLength(100);
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.ToTable("hash");

                //entity.HasCharSet("utf8")
                //    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => new { e.Key, e.Field }, "IX_Hash_Key_Field")
                    .IsUnique();

                entity.Property(e => e.ExpireAt).HasMaxLength(6);

                entity.Property(e => e.Field).HasMaxLength(40);

                entity.Property(e => e.Key).HasMaxLength(100);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("job");

                //entity.HasCharSet("utf8")
                //    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.StateName, "IX_Job_StateName");

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.Property(e => e.ExpireAt).HasMaxLength(6);

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<Jobparameter>(entity =>
            {
                entity.ToTable("jobparameter");

                //entity.HasCharSet("utf8")
                //    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.JobId, "FK_JobParameter_Job");

                entity.HasIndex(e => new { e.JobId, e.Name }, "IX_JobParameter_JobId_Name")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Jobparameters)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_JobParameter_Job");
            });

            modelBuilder.Entity<Jobqueue>(entity =>
            {
                entity.ToTable("jobqueue");

                //entity.HasCharSet("utf8")
                //    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => new { e.Queue, e.FetchedAt }, "IX_JobQueue_QueueAndFetchedAt");

                entity.Property(e => e.FetchToken).HasMaxLength(36);

                entity.Property(e => e.FetchedAt).HasMaxLength(6);

                entity.Property(e => e.Queue).HasMaxLength(50);
            });

            modelBuilder.Entity<Jobstate>(entity =>
            {
                entity.ToTable("jobstate");

                //entity.HasCharSet("utf8")
                //    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.JobId, "FK_JobState_Job");

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Jobstates)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_JobState_Job");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.ToTable("list");

                //entity.HasCharSet("utf8")
                //    .UseCollation("utf8_general_ci");

                entity.Property(e => e.ExpireAt).HasMaxLength(6);

                entity.Property(e => e.Key).HasMaxLength(100);
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.ToTable("mediatypes");

                entity.Property(e => e.Id)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.CreatedBy)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.CreatedOn).HasMaxLength(6);

                entity.Property(e => e.DeletedBy)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.DeletedOn).HasMaxLength(6);

                entity.Property(e => e.LastModifiedBy)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.LastModifiedOn).HasMaxLength(6);
            });

            modelBuilder.Entity<MyChannel>(entity =>
            {
                entity.ToTable("mychannels");

                entity.HasIndex(e => e.ChannelId, "IX_MyChannels_ChannelId");

                entity.Property(e => e.Id)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.ChannelId)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.CreatedBy)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.CreatedOn).HasMaxLength(6);

                entity.Property(e => e.DeletedBy)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.DeletedOn).HasMaxLength(6);

                entity.Property(e => e.LastModifiedBy)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.LastModifiedOn).HasMaxLength(6);

                entity.Property(e => e.UserId)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.MyChannels)
                    .HasForeignKey(d => d.ChannelId)
                    .HasConstraintName("FK_MyChannels_Channels_ChannelId");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.BrandId, "IX_Products_BrandId");

                entity.Property(e => e.Id)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.BrandId)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.CreatedBy)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.CreatedOn).HasMaxLength(6);

                entity.Property(e => e.DeletedBy)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.DeletedOn).HasMaxLength(6);

                entity.Property(e => e.ImagePath).HasMaxLength(2048);

                entity.Property(e => e.LastModifiedBy)
                    .UseCollation("ascii_general_ci")
                   ;

                entity.Property(e => e.LastModifiedOn).HasMaxLength(6);

                entity.Property(e => e.Name).HasMaxLength(1024);

                entity.Property(e => e.TenantId).HasMaxLength(64);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Products_Brands_BrandId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.HasIndex(e => new { e.NormalizedName, e.TenantId }, "RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.Property(e => e.TenantId).HasMaxLength(64);
            });

            modelBuilder.Entity<Roleclaim>(entity =>
            {
                entity.ToTable("roleclaims");

                entity.HasIndex(e => e.RoleId, "IX_RoleClaims_RoleId");

                entity.Property(e => e.CreatedOn).HasMaxLength(6);

                entity.Property(e => e.LastModifiedOn).HasMaxLength(6);

                entity.Property(e => e.TenantId).HasMaxLength(64);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Roleclaims)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_RoleClaims_Roles_RoleId");
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("server");

                //entity.HasCharSet("utf8")
                //    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasMaxLength(100);

                entity.Property(e => e.LastHeartbeat).HasMaxLength(6);
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.ToTable("set");

                //entity.HasCharSet("utf8")
                //    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => new { e.Key, e.Value }, "IX_Set_Key_Value")
                    .IsUnique();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(256);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("state");

                //entity.HasCharSet("utf8")
                //    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.JobId, "FK_HangFire_State_Job");

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_State_Job");
            });

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.ToTable("tenants");

                entity.HasIndex(e => e.Identifier, "IX_Tenants_Identifier")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(64);

                entity.Property(e => e.ValidUpto).HasMaxLength(6);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => new { e.NormalizedUserName, e.TenantId }, "UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEnd).HasMaxLength(6);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.ObjectId).HasMaxLength(256);

                entity.Property(e => e.RefreshTokenExpiryTime).HasMaxLength(6);

                entity.Property(e => e.TenantId).HasMaxLength(64);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Userclaim>(entity =>
            {
                entity.ToTable("userclaims");

                entity.HasIndex(e => e.UserId, "IX_UserClaims_UserId");

                entity.Property(e => e.TenantId).HasMaxLength(64);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userclaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserClaims_Users_UserId");
            });

            modelBuilder.Entity<Userlogin>(entity =>
            {
                entity.ToTable("userlogins");

                entity.HasIndex(e => new { e.LoginProvider, e.ProviderKey, e.TenantId }, "IX_UserLogins_LoginProvider_ProviderKey_TenantId")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "IX_UserLogins_UserId");

                entity.Property(e => e.TenantId).HasMaxLength(64);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userlogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserLogins_Users_UserId");
            });

            modelBuilder.Entity<Userrole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("userroles");

                entity.HasIndex(e => e.RoleId, "IX_UserRoles_RoleId");

                entity.Property(e => e.TenantId).HasMaxLength(64);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Userroles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserRoles_Roles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userroles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRoles_Users_UserId");
            });

            modelBuilder.Entity<Usertoken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("usertokens");

                entity.Property(e => e.TenantId).HasMaxLength(64);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Usertokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserTokens_Users_UserId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
