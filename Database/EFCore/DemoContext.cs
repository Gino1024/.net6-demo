using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Database.Models.SQLModels;

namespace Database.EFCore
{
    /* dotnet ef migrations add init -s "..\DotNet6.WebAPI.Demo" */
    /* dotnet ef database update -s ..\DotNet6.WebAPI.Demo */
    public partial class DemoContext : DbContext
    {
        public DemoContext()
        {
        }

        public DemoContext(DbContextOptions<DemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TUser> TUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("T_User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Account).HasMaxLength(50);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
