using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.DataAccess.Postgres.Configurations;

public class UserBonusEntityConfiguration : IEntityTypeConfiguration<UserBonusEntity> {
   public void Configure(EntityTypeBuilder<UserBonusEntity> builder) {
      builder.ToTable("user_bonuses");

      builder.HasKey(x => x.UserBonusId);

      builder.Property(x => x.Status)
         .HasMaxLength(16);

      builder.HasOne(x => x.User)
         .WithMany(x => x.UserBonuses)
         .HasForeignKey(x => x.UserId)
         .OnDelete(DeleteBehavior.Cascade);

      builder.HasOne(x => x.Bonus)
         .WithMany(x => x.UserBonuses)
         .HasForeignKey(x => x.BonusId)
         .OnDelete(DeleteBehavior.Cascade);
   }
}