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
        public DbSet<Business> Businesses { get; set; }

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

            modelBuilder.Entity<Business>(static entity =>
            {
                entity.HasKey(b => b.idBusiness);

                entity.Property(b => b.ruc)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

                entity.Property(b => b.companyName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(b => b.email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(b => b.phone)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);

                entity.Property(b => b.address)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

                entity.Property(b => b.owner)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(b => b.discount)
                .IsRequired()
                .HasDefaultValue(0)
                .HasPrecision(4,2)
                .IsUnicode(false);

                entity.Property(u => u.creationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(u => u.phone).IsUnique();
            });
        }
    }
}
