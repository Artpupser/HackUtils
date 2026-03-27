using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.DataAccess.Postgres.Configurations;

public class LevelEntityEntityConfiguration : IEntityTypeConfiguration<LevelEntity>
{
    public void Configure(EntityTypeBuilder<LevelEntity> builder)
    {
        builder.ToTable("levels");

        builder.HasKey(x => x.LevelId);

        builder.Property(x => x.Name)
            .HasMaxLength(64);
    }
}