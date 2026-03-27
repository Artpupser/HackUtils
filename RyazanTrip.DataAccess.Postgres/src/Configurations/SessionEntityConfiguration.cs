using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.DataAccess.Postgres.Configurations;

public class SessionEntityConfiguration : IEntityTypeConfiguration<SessionEntity> {
   public void Configure(EntityTypeBuilder<SessionEntity> builder) {
      builder.ToTable("sessions");

      builder.HasKey(x => x.Id);

      builder.Property(x => x.Token)
         .HasMaxLength(255);

      builder.HasOne(x => x.UserEntity)
         .WithMany(x => x.Sessions)
         .HasForeignKey(x => x.UserId)
         .OnDelete(DeleteBehavior.Cascade);
   }
}