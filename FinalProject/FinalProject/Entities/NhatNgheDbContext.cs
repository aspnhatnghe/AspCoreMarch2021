using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Entities;

namespace FinalProject.Entities
{
    public class NhatNgheDbContext : DbContext
    {
        public NhatNgheDbContext(DbContextOptions<NhatNgheDbContext> options) : base(options)
        {

        }

        #region DbSet
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        #endregion

        #region Config FluentAPI
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>(e =>
            {
                e.ToTable("UserInfo");
                e.HasKey(u => u.Id);
                e.HasIndex(u => u.Username).IsUnique();
                e.HasIndex(u => u.Email).IsUnique();
                e.HasIndex(u => u.Phone).IsUnique();
                e.Property(u => u.FullName).IsRequired().HasMaxLength(150);
                e.Property(u => u.Address).IsRequired().HasMaxLength(250);
                e.Property(u => u.Phone).IsRequired().HasMaxLength(250);
                e.Property(u => u.Email).IsRequired().HasMaxLength(250);
                e.Property(u => u.IsActive).HasDefaultValue(true);
            });

            modelBuilder.Entity<Role>(e =>
            {
                e.ToTable("Role");
                e.HasKey(u => u.Id);
                e.HasIndex(u => u.RoleName).IsUnique();
                e.Property(u => u.RoleName).IsRequired().HasMaxLength(150);
            });

            modelBuilder.Entity<UserRole>(e =>
            {
                e.ToTable("UserRole");
                e.HasKey(u => u.Id);
                e.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .HasConstraintName("FK_UserRole_Role");
                e.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .HasConstraintName("FK_UserRole_UserInfo");
                e.Property(ur => ur.Access).HasDefaultValueSql("1");
                e.Property(ur => ur.Add).HasDefaultValueSql("0");
                e.Property(ur => ur.Modify).HasDefaultValueSql("0");
                e.Property(ur => ur.Remove).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.CategoryName).IsRequired().HasMaxLength(100);
                entity.HasIndex(c => c.CategoryName).IsUnique();
                entity.Property(c => c.SeoUrl).IsRequired().HasMaxLength(100);
                entity.HasIndex(c => c.SeoUrl).IsUnique();

                entity.HasOne(p => p.ParentCategory)
                    .WithMany(p => p.ChildCategories)
                    .HasForeignKey(p => p.ParentCategoryId)
                    .HasConstraintName("FK_Category_ParentCategory");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(p => p.Id).HasName("PK_Product");
                entity.Property(p => p.ProductName).IsRequired().HasMaxLength(200);
                entity.HasIndex(p => p.ProductName).IsUnique();
                entity.Property(c => c.SeoUrl).IsRequired().HasMaxLength(200);
                entity.HasIndex(c => c.SeoUrl).IsUnique();

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .HasConstraintName("FK_Product_Category");
            });

            modelBuilder.Entity<UserAddress>(e => {
                e.ToTable("UserAddress");
                e.HasKey(e => e.Id);
            });

            modelBuilder.Entity<UserAddress>(e => {
                e.ToTable("UserAddress");
                e.HasKey(e => e.Id);
                e.Property(e => e.Address).IsRequired().HasMaxLength(150);

                e.HasOne(ua => ua.User)
                    .WithMany(u => u.UserAddresses)
                    .HasForeignKey(ua => ua.UserId)
                    .HasConstraintName("FK_UA_User");
            });

            modelBuilder.Entity<Payment>(e => {
                e.ToTable("Payment");
                e.HasKey(e => e.Id);
                e.Property(p => p.PaymentName).IsRequired().HasMaxLength(150);
            });

            modelBuilder.Entity<PaymentDetail>(e => {
                e.ToTable("PaymentDetail");
                e.HasKey(e => e.Id);

                e.HasOne(pd => pd.Payment)
                    .WithMany(p => p.PaymentDetails)
                    .HasForeignKey(ua => ua.PaymentId)
                    .HasConstraintName("FK_PD_Payment");

                e.HasOne(pd => pd.Order)
                    .WithMany(o => o.PaymentDetails)
                    .HasForeignKey(pd => pd.OrderId)
                    .HasConstraintName("FK_PD_Order");
            });

            modelBuilder.Entity<Order>(e => {
                e.ToTable("Order");
                e.HasKey(e => e.Id);

            });
            modelBuilder.Entity<OrderDetail>(e => {
                e.ToTable("OrderDetail");
                e.HasKey(e => e.Id);

                e.HasOne(od => od.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(pd => pd.OrderId)
                    .HasConstraintName("FK_OD_Order");

                e.HasOne(od => od.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(od => od.ProductId)
                    .HasConstraintName("FK_OD_Product");
            });
        }
        #endregion

        #region Config FluentAPI
        public DbSet<FinalProject.Entities.UserInfo> UserInfo { get; set; }
        #endregion
    }
}
