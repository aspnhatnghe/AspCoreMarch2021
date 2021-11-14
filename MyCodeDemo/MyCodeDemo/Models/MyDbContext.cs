using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.Models
{
    public class MyDbContext : DbContext
    {
        #region DbSet <---> Table
        public DbSet<Loai> Loais { get; set; }
        public DbSet<HangHoa> HangHoas { get; set; }
        #endregion


        public MyDbContext(DbContextOptions options): base(options)
        {

        }

        //Sử dụng Fluent API để định nghĩa
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loai>(entity => {
                entity.HasIndex(e => e.TenLoai).IsUnique();
                entity.ToTable("Loai");
            });
            modelBuilder.Entity<HangHoa>(entity => {
                entity.ToTable("HangHoa");
                entity.HasKey(e => new { e.MaHH });
                entity.Property(e => e.TenHH).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SoLuong).HasDefaultValue(0);

                entity.HasOne(e => e.Loai)
                    .WithMany(lo => lo.HangHoas)
                    .HasForeignKey("MaLoai")
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_HangHoa_Loai");
            });
        }
    }
}
