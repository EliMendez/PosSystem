using Microsoft.EntityFrameworkCore;
using PosSystem.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Model.Context
{
    public class PosSystemContext : DbContext
    {
        public PosSystemContext(DbContextOptions<PosSystemContext> options): base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>(static entity =>
            {
                entity.HasKey(r => r.idRole);

                entity.Property(r => r.description)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(r => r.status)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false);

                entity.Property(r => r.creationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(r => r.description).IsUnique();
            });

            modelBuilder.Entity<User>(static entity =>
            {
                entity.HasKey(u => u.idUser);

                entity.Property(u => u.name)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false);

                entity.Property(u => u.surname)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false);

                entity.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .IsRequired()
                .HasForeignKey(u => u.idRole);

                entity.Property(u => u.phone)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);

                entity.Property(u => u.status)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false);

                entity.Property(u => u.creationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(u => u.phone).IsUnique();
            });
        }
    }
}
