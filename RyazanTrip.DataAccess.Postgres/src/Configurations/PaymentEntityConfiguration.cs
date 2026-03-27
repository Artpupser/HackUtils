using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.DataAccess.Postgres.Configurations;

public class PaymentEntityConfiguration : IEntityTypeConfiguration<PaymentEntity> {
   public void Configure(EntityTypeBuilder<PaymentEntity> builder) {
      builder.ToTable("payments");
      builder.HasKey(x => x.PaymentId);
      builder.Property(x => x.Amount)
         .HasPrecision(10, 2);

      builder.Property(x => x.Status)
         .HasMaxLength(16);
      builder.Property(x => x.YukassaPaymentId)
         .HasMaxLength(150);
      builder.HasOne(x => x.UserTour)
         .WithMany(x => x.Payments)
         .HasForeignKey(x => x.UserTourId)
         .OnDelete(DeleteBehavior.Cascade);
   }
}