using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;

namespace Roofstock01
{
    public partial class RoofStockDBContext : DbContext
    {
        public RoofStockDBContext()
        {
                
        }

        public RoofStockDBContext(DbContextOptions<RoofStockDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Properties> Properties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Properties>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.YearBuilt)
                  .HasColumnName("year_built")
                  .HasColumnType("int");

                entity.Property(e => e.GrossYield)
                    .HasColumnName("gross_yield")
                    .HasColumnType("numeric(6, 2)");

                entity.Property(e => e.ListPrice)
                    .HasColumnName("list_price")
                    .HasColumnType("numeric(15, 2)");

                entity.Property(e => e.MonthlyRent)
                    .HasColumnName("monthly_rent")
                    .HasColumnType("numeric(10, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
