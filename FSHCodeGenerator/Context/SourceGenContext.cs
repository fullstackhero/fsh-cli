using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FSHCodeGenerator.Models;

namespace FSHCodeGenerator
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

        public virtual DbSet<AuditTrail> AuditTrails { get; set; } = null!;
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleClaim> RoleClaims { get; set; } = null!;
        public virtual DbSet<TheChild> TheChilds { get; set; } = null!;
        public virtual DbSet<TheParent> TheParents { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserClaim> UserClaims { get; set; } = null!;
        public virtual DbSet<UserLogin> UserLogins { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<UserToken> UserTokens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=FSHDb;Integrated Security=True;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditTrail>(entity =>
            {
                entity.ToTable("AuditTrails", "Auditing");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TenantId).HasMaxLength(64);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brands", "Catalog");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.TenantId).HasMaxLength(64);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products", "Catalog");

                entity.HasIndex(e => e.BrandId, "IX_Products_BrandId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ImagePath).HasMaxLength(2048);

                entity.Property(e => e.Name).HasMaxLength(1024);

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TenantId).HasMaxLength(64);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles", "Identity");

                entity.HasIndex(e => new { e.NormalizedName, e.TenantId }, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.Property(e => e.TenantId).HasMaxLength(64);
            });

            modelBuilder.Entity<RoleClaim>(entity =>
            {
                entity.ToTable("RoleClaims", "Identity");

                entity.HasIndex(e => e.RoleId, "IX_RoleClaims_RoleId");

                entity.Property(e => e.TenantId).HasMaxLength(64);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<TheChild>(entity =>
            {
                entity.ToTable("TheChilds", "Catalog");

                entity.HasIndex(e => e.BrandId, "IX_TheChilds_BrandId");

                entity.HasIndex(e => e.TheParentId, "IX_TheChilds_TheParentId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.TheChildren)
                    .HasForeignKey(d => d.BrandId);

                entity.HasOne(d => d.TheParent)
                    .WithMany(p => p.TheChildren)
                    .HasForeignKey(d => d.TheParentId);
            });

            modelBuilder.Entity<TheParent>(entity =>
            {
                entity.ToTable("TheParents", "Catalog");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "Identity");

                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => new { e.NormalizedUserName, e.TenantId }, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.ObjectId).HasMaxLength(256);

                entity.Property(e => e.TenantId).HasMaxLength(64);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.ToTable("UserClaims", "Identity");

                entity.HasIndex(e => e.UserId, "IX_UserClaims_UserId");

                entity.Property(e => e.TenantId).HasMaxLength(64);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.ToTable("UserLogins", "Identity");

                entity.HasIndex(e => new { e.LoginProvider, e.ProviderKey, e.TenantId }, "IX_UserLogins_LoginProvider_ProviderKey_TenantId")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "IX_UserLogins_UserId");

                entity.Property(e => e.TenantId).HasMaxLength(64);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("UserRoles", "Identity");

                entity.HasIndex(e => e.RoleId, "IX_UserRoles_RoleId");

                entity.Property(e => e.TenantId).HasMaxLength(64);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.ToTable("UserTokens", "Identity");

                entity.Property(e => e.TenantId).HasMaxLength(64);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
