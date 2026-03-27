namespace RyazanTrip.DataAccess.Postgres.Entities;

public class TourEntity {
   public int TourId { get; set; }
   public string? Title { get; set; }
   public string? Description { get; set; }
   public decimal? Price { get; set; }

   public int? ImageId { get; set; }
   public ImageEntity? ImageEntity { get; set; }

   public ICollection<UserTourEntity> UserTours { get; set; } = new List<UserTourEntity>();
}