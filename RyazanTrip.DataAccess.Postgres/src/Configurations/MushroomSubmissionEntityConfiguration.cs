using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.DataAccess.Postgres.Configurations;

public class MushroomSubmissionEntityConfiguration : IEntityTypeConfiguration<MushroomSubmissionEntity> {
   public void Configure(EntityTypeBuilder<MushroomSubmissionEntity> builder) {
      builder.ToTable("mushroom_submissions");

      builder.HasKey(x => x.SubmissionId);

      builder.Property(x => x.Status)
         .HasMaxLength(16);

      builder.HasOne(x => x.User)
         .WithMany(x => x.MushroomSubmissions)
         .HasForeignKey(x => x.UserId)
         .OnDelete(DeleteBehavior.Cascade);

      builder.HasOne(x => x.Mushroom)
         .WithMany(x => x.Submissions)
         .HasForeignKey(x => x.MushroomId)
         .OnDelete(DeleteBehavior.Cascade);

      builder.HasOne(x => x.Image)
         .WithMany(x => x.Submissions)
         .HasForeignKey(x => x.ImageId)
         .OnDelete(DeleteBehavior.SetNull);
   }
}