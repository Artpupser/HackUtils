namespace RyazanTrip.DataAccess.Postgres.Entities;

public class QrCodeEntity {
   public int QrId { get; set; }
   public string? Type { get; set; }
   public int? ReferenceId { get; set; }
   public string? Content { get; set; }
}