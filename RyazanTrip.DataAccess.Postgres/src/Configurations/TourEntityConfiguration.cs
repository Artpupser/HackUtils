using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.DataAccess.Postgres.Configurations;

public class TourEntityConfiguration : IEntityTypeConfiguration<TourEntity> {
   public void Configure(EntityTypeBuilder<TourEntity> builder) {
      builder.ToTable("tours");

      builder.HasKey(x => x.TourId);

      builder.Property(x => x.Title)
         .IsRequired()
         .HasMaxLength(150);

      builder.Property(x => x.Description)
         .IsRequired()
         .HasMaxLength(2000);

      builder.Property(x => x.Price)
         .HasPrecision(10, 2)
         .IsRequired();

      builder.Property(x => x.TourTime)
         .IsRequired()
         .HasColumnType("datetime2");

      builder.Property(x => x.Coords)
         .IsRequired()
         .HasMaxLength(1000);

      builder.Property(x => x.ImageId);

      builder.HasOne(x => x.ImageEntity)
         .WithMany(x => x.Tours)
         .HasForeignKey(x => x.ImageId)
         .OnDelete(DeleteBehavior.SetNull)
         .IsRequired(false);

      builder.HasMany(x => x.UserTours)
         .WithOne(ut => ut.TourEntity)
         .HasForeignKey(ut => ut.TourId)
         .OnDelete(DeleteBehavior.Cascade);

      builder.HasIndex(x => x.TourTime);
      builder.HasIndex(x => x.Price);
      builder.HasIndex(x => x.Title);

      builder.HasIndex(x => new { x.Title, x.TourTime })
         .IsUnique();
   }
}