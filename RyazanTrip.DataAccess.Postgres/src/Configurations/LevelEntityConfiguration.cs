using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.DataAccess.Postgres.Configurations;

public class LevelEntityConfiguration : IEntityTypeConfiguration<LevelEntity> {
   public void Configure(EntityTypeBuilder<LevelEntity> builder) {
      builder.ToTable("levels");
      builder.HasKey(x => x.LevelId);
      builder.Property(x => x.Name)
         .HasMaxLength(64);
      builder.HasData(
         new LevelEntity { LevelId = 1, Name = "Медь", RequiredExp = 0},
         new LevelEntity { LevelId = 2, Name = "Железо", RequiredExp = 100},
         new LevelEntity { LevelId = 3, Name = "Золото", RequiredExp = 500},
         new LevelEntity { LevelId = 4, Name = "Платина", RequiredExp = 1000},
         new LevelEntity { LevelId = 5, Name = "Брилиант", RequiredExp = 5000}
      );
   }
}