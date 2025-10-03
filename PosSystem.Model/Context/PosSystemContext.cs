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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>(static entity =>
            {
                entity.HasKey(r => r.IdRole);

                entity.Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(r => r.Status)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false);

                entity.Property(r => r.CreationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(r => r.Description).IsUnique();
            });
        }
    }
}
