namespace RyazanTrip.DataAccess.Postgres.Entities;

public class PaymentEntity {
   public int PaymentId { get; set; }
   public int? UserTourId { get; set; }
   public decimal? Amount { get; set; }
   public string? Status { get; set; }
   public string? YukassaPaymentId { get; set; }
   public DateTime? CreatedAt { get; set; }
   public UserTourEntity? UserTour { get; set; }
}