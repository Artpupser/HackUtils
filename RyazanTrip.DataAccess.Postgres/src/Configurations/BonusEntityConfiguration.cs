using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.DataAccess.Postgres.Configurations;

public class BonusEntityConfiguration : IEntityTypeConfiguration<BonusEntity> {
   public void Configure(EntityTypeBuilder<BonusEntity> builder) {
      builder.ToTable("bonuses");
      builder.HasKey(x => x.BonusId);
      builder.Property(x => x.Title)
         .HasMaxLength(150);
      builder.Property(x => x.DiscountPercent);
   }
}