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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Sale> SaleDetails { get; set; }

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

            modelBuilder.Entity<Category>(static entity =>
            {
                entity.HasKey(c => c.idCategory);

                entity.Property(c => c.description)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(c => c.status)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false);

                entity.Property(c => c.creationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

                //Relationship with product
                object value = entity.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.idCategory)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(c => c.description).IsUnique();
            });

            modelBuilder.Entity<Product>(static entity =>
            {
                entity.HasKey(p => p.idProduct);

                entity.Property(p => p.barcode)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

                entity.Property(p => p.description)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .IsRequired()
                .HasForeignKey(p => p.idCategory);

                entity.Property(p => p.salePrice)
                .IsRequired()
                .HasPrecision(18,2)
                .IsUnicode(false);

                entity.Property(p => p.stock)
                .IsRequired()
                .HasDefaultValue(0)
                .IsUnicode(false);

                entity.Property(p => p.minimumStock)
                .IsRequired()
                .HasDefaultValue(5)
                .IsUnicode(false);

                entity.Property(p => p.status)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false);

                entity.Property(p => p.creationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(p => p.barcode).IsUnique();
                entity.HasIndex(p => p.description).IsUnique();
            });

            modelBuilder.Entity<Sale>(static entity =>
            {
                entity.HasKey(s => s.idSale);

                entity.Property(s => s.bill)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

                entity.Property(s => s.saleDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

                entity.Property(s => s.dni)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

                entity.Property(s => s.customer)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(s => s.discount)
                .IsRequired()
                .HasDefaultValue(0)
                .HasPrecision(4,2)
                .IsUnicode(false);

                entity.Property(s => s.total)
                .IsRequired()
                .HasDefaultValue(0)
                .HasPrecision(18, 2)
                .IsUnicode(false);

                entity.Property(s => s.status)
                .IsRequired()
                .HasConversion<string>();

                entity.Property(s => s.annulledDate)
                .IsRequired();

                entity.Property(s => s.reason)
                .IsRequired(false)
                .HasColumnType("TEXT");

                entity.Property(s => s.userCancel)
                .IsRequired(false);

                //Relationship with sale detail
                object value = entity.HasMany(s => s.SaleDetails)
                .WithOne(sd => sd.Sale)
                .HasForeignKey(sd => sd.idSale)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(s => s.bill).IsUnique();
            });

            modelBuilder.Entity<SaleDetail>(static entity =>
            {
                entity.HasKey(sd => sd.idSaleDetail);

                entity.HasOne(sd => sd.Sale)
                .WithMany(sd => sd.SaleDetails)
                .HasForeignKey(sd => sd.idSale)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(sd => sd.Product)
                .WithMany()
                .HasForeignKey(sd => sd.idProduct)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

                entity.Property(sd => sd.productName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(sd => sd.price)
                .IsRequired()
                .HasPrecision(18, 2)
                .IsUnicode(false);

                entity.Property(sd => sd.count)
                .IsRequired()
                .HasDefaultValue(1)
                .IsUnicode(false);

                entity.Property(sd => sd.discount)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasDefaultValue(0)
                .IsUnicode(false);

                entity.Property(sd => sd.total)
                .IsRequired()
                .HasPrecision(18, 2)
                .IsUnicode(false);
            });
        }
    }
}
