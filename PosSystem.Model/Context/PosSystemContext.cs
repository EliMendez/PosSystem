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
        public PosSystemContext(DbContextOptions<PosSystemContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<DocumentNumber> DocumentNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>(static entity =>
            {
                entity.HasKey(r => r.RoleId);

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

            modelBuilder.Entity<User>(static entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false);

                entity.Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false);

                entity.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .IsRequired()
                .HasForeignKey(u => u.RoleId);

                entity.Property(u => u.Phone)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);

                entity.Property(u => u.Status)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false);

                entity.Property(u => u.CreationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(u => u.Phone).IsUnique();
            });

            modelBuilder.Entity<Business>(static entity =>
            {
                entity.HasKey(b => b.BusinessId);

                entity.Property(b => b.Ruc)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

                entity.Property(b => b.CompanyName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(b => b.Phone)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);

                entity.Property(b => b.Address)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

                entity.Property(b => b.Owner)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(b => b.Discount)
                .IsRequired()
                .HasDefaultValue(0)
                .HasPrecision(4,2)
                .IsUnicode(false);

                entity.Property(u => u.CreationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(u => u.Phone).IsUnique();
            });

            modelBuilder.Entity<Category>(static entity =>
            {
                entity.HasKey(c => c.CategoryId);

                entity.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(c => c.Status)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false);

                entity.Property(c => c.CreationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

                //Relationship with product
                object value = entity.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(c => c.Description).IsUnique();
            });

            modelBuilder.Entity<Product>(static entity =>
            {
                entity.HasKey(p => p.ProductId);

                entity.Property(p => p.Barcode)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

                entity.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .IsRequired()
                .HasForeignKey(p => p.CategoryId);

                entity.Property(p => p.SalePrice)
                .IsRequired()
                .HasPrecision(18,2)
                .IsUnicode(false);

                entity.Property(p => p.Stock)
                .IsRequired()
                .HasDefaultValue(0)
                .IsUnicode(false);

                entity.Property(p => p.MinimumStock)
                .IsRequired()
                .HasDefaultValue(5)
                .IsUnicode(false);

                entity.Property(p => p.Status)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false);

                entity.Property(p => p.CreationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(p => p.Barcode).IsUnique();
                entity.HasIndex(p => p.Description).IsUnique();
            });

            modelBuilder.Entity<Sale>(static entity =>
            {
                entity.HasKey(s => s.SaleId);

                entity.Property(s => s.Bill)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

                entity.Property(s => s.SaleDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

                entity.Property(s => s.Dni)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

                entity.Property(s => s.Customer)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(s => s.Discount)
                .IsRequired()
                .HasDefaultValue(0)
                .HasPrecision(4,2)
                .IsUnicode(false);

                entity.Property(s => s.Total)
                .IsRequired()
                .HasDefaultValue(0)
                .HasPrecision(18, 2)
                .IsUnicode(false);

                entity.Property(s => s.Status)
                .IsRequired()
                .HasConversion<string>();

                entity.Property(s => s.AnnulledDate)
                .IsRequired();

                entity.Property(s => s.Reason)
                .IsRequired(false)
                .HasColumnType("TEXT");

                entity.Property(s => s.UserCancel)
                .IsRequired(false);

                //Relationship with sale detail
                object value = entity.HasMany(s => s.SaleDetails)
                .WithOne(sd => sd.Sale)
                .HasForeignKey(sd => sd.SaleId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(s => s.Bill).IsUnique();
            });

            modelBuilder.Entity<SaleDetail>(static entity =>
            {
                entity.HasKey(sd => sd.SaleDetailId);

                entity.HasOne(sd => sd.Sale)
                .WithMany(sd => sd.SaleDetails)
                .HasForeignKey(sd => sd.SaleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(sd => sd.Product)
                .WithMany()
                .HasForeignKey(sd => sd.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

                entity.Property(sd => sd.ProductName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(sd => sd.Price)
                .IsRequired()
                .HasPrecision(18, 2)
                .IsUnicode(false);

                entity.Property(sd => sd.Count)
                .IsRequired()
                .HasDefaultValue(1)
                .IsUnicode(false);

                entity.Property(sd => sd.Discount)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasDefaultValue(0)
                .IsUnicode(false);

                entity.Property(sd => sd.Total)
                .IsRequired()
                .HasPrecision(18, 2)
                .IsUnicode(false);
            });

            modelBuilder.Entity<DocumentNumber>(static entity =>
            {
                entity.HasKey(dn => dn.DocumentNumberId);

                entity.Property(dn => dn.Document)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

                entity.Property(dn => dn.CreationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(dn => dn.Document).IsUnique();
            });
        }
    }
}
