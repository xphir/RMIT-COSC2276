using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AngularTest.Models
{
    public partial class s3530160Context : DbContext
    {
        public s3530160Context()
        {
        }

        public s3530160Context(DbContextOptions<s3530160Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Slot> Slot { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=wdt2019.australiasoutheast.cloudapp.azure.com;uid=s3530160;database=s3530160;pwd=abc123;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.HasIndex(e => e.StaffId);

                entity.HasIndex(e => e.StudentId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.StaffId);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.StudentId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.RoomID)
                    .HasColumnName("RoomID")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.HasKey(e => new { e.RoomID, e.StartTime });

                entity.HasIndex(e => e.StaffID);

                entity.HasIndex(e => e.StudentID);

                entity.Property(e => e.RoomID).HasColumnName("RoomID");

                entity.Property(e => e.StaffID)
                    .IsRequired()
                    .HasColumnName("StaffID");

                entity.Property(e => e.StudentID).HasColumnName("StudentID");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Slot)
                    .HasForeignKey(d => d.RoomID);

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Slot)
                    .HasForeignKey(d => d.StaffID);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Slot)
                    .HasForeignKey(d => d.StudentID);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.StaffID)
                    .HasColumnName("StaffID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentID)
                    .HasColumnName("StudentID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
