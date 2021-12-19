using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Entities
{
    public class NhatNgheDbContext : DbContext
    {
        public NhatNgheDbContext(DbContextOptions<NhatNgheDbContext> options): base(options)
        {

        }

        #region DbSet
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        #endregion

        #region Config FluentAPI
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity => {
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

            modelBuilder.Entity<Product>(entity => {
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
        }
        #endregion
    }
}
