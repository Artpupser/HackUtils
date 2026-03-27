using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.DataAccess.Postgres.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity> {
   public void Configure(EntityTypeBuilder<UserEntity> builder) {
      builder.ToTable("users");

      builder.HasKey(x => x.UserId);

      builder.Property(x => x.Email)
         .HasMaxLength(254);

      builder.Property(x => x.PasswordHash)
         .HasMaxLength(255);

      builder.Property(x => x.Username)
         .HasMaxLength(64);

      builder.HasOne(x => x.RoleEntity)
         .WithMany(x => x.Users)
         .HasForeignKey(x => x.RoleId)
         .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(x => x.LevelEntity)
         .WithMany(x => x.Users)
         .HasForeignKey(x => x.LevelId)
         .OnDelete(DeleteBehavior.Restrict);
   }
}