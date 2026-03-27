using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.DataAccess.Postgres.Configurations;

public class TourEntityConfiguration : IEntityTypeConfiguration<TourEntity>
{
    public void Configure(EntityTypeBuilder<TourEntity> builder)
    {
        builder.ToTable("tours");

        builder.HasKey(x => x.TourId);

        builder.Property(x => x.Title)
            .HasMaxLength(150);

        builder.Property(x => x.Price)
            .HasPrecision(10, 2);

        builder.HasOne(x => x.ImageEntity)
            .WithMany(x => x.Tours)
            .HasForeignKey(x => x.ImageId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}