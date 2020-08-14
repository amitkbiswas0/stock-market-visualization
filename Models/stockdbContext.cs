using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StockMarket.Models
{
  public partial class stockdbContext : DbContext
  {
    public stockdbContext()
    {
    }

    public stockdbContext(DbContextOptions<stockdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Stocks> Stocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasPostgresExtension("uuid-ossp");

      modelBuilder.Entity<Stocks>(entity =>
      {
        entity.ToTable("stocks");

        entity.Property(e => e.Id)
                  .HasColumnName("id")
                  .HasDefaultValueSql("uuid_generate_v4()");

        entity.Property(e => e.Close)
                  .HasColumnName("close")
                  .HasMaxLength(50);

        entity.Property(e => e.High)
                  .HasColumnName("high")
                  .HasMaxLength(50);

        entity.Property(e => e.Low)
                  .HasColumnName("low")
                  .HasMaxLength(50);

        entity.Property(e => e.Open)
                  .HasColumnName("open")
                  .HasMaxLength(50);

        entity.Property(e => e.TradeCode)
                  .IsRequired()
                  .HasColumnName("trade_code")
                  .HasMaxLength(50);

        entity.Property(e => e.TradeDate)
                  .HasColumnName("trade_date")
                  .HasColumnType("date");

        entity.Property(e => e.Volume)
                  .HasColumnName("volume")
                  .HasMaxLength(50);
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
