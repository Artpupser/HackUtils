using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.DataAccess.Postgres.Configurations;

public class MushroomEntityConfiguration : IEntityTypeConfiguration<MushroomEntity>
{
    public void Configure(EntityTypeBuilder<MushroomEntity> builder)
    {
        builder.ToTable("mushrooms");

        builder.HasKey(x => x.MushroomId);

        builder.Property(x => x.Name)
            .HasMaxLength(150);

        builder.Property(x => x.Location)
            .HasMaxLength(255);

        builder.HasOne(x => x.Image)
            .WithMany(x => x.Mushrooms)
            .HasForeignKey(x => x.ImageId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}