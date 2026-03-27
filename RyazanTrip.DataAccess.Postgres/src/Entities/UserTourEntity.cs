namespace RyazanTrip.DataAccess.Postgres.Entities;

public class UserTourEntity {
   public int UserTourId { get; set; }
   public int? UserId { get; set; }
   public int? TourId { get; set; }

   public string? GuideOption { get; set; }
   public string? Status { get; set; }

   public UserEntity? UserEntity { get; set; }
   public TourEntity? TourEntity { get; set; }

   public ICollection<PaymentEntity> Payments { get; set; } = new List<PaymentEntity>();
}