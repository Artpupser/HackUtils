using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.DataAccess.Postgres.Configurations;

public class UserTourEntityConfiguration : IEntityTypeConfiguration<UserTourEntity> {
   public void Configure(EntityTypeBuilder<UserTourEntity> builder) {
      builder.ToTable("user_tours");

      builder.HasKey(x => x.UserTourId);

      builder.Property(x => x.GuideOption)
         .HasMaxLength(16);

      builder.Property(x => x.Status)
         .HasMaxLength(16);

      builder.HasOne(x => x.UserEntity)
         .WithMany(x => x.UserTours)
         .HasForeignKey(x => x.UserId)
         .OnDelete(DeleteBehavior.Cascade);

      builder.HasOne(x => x.TourEntity)
         .WithMany(x => x.UserTours);
   }
}