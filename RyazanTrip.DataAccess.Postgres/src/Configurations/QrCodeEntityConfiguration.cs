using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.DataAccess.Postgres.Configurations;

public class QrCodeEntityConfiguration : IEntityTypeConfiguration<QrCodeEntity>
{
    public void Configure(EntityTypeBuilder<QrCodeEntity> builder)
    {
        builder.ToTable("qr_codes");

        builder.HasKey(x => x.QrId);

        builder.Property(x => x.Type)
            .HasMaxLength(32);
    }
}