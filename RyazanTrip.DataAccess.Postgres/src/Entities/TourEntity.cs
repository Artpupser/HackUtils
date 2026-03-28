namespace RyazanTrip.DataAccess.Postgres.Entities;

public class TourEntity {
   public int TourId { get; set; }
   public string Title { get; set; } = string.Empty;  
   public string Description { get; set; } = string.Empty;
   public decimal Price { get; set; }
   public DateTime TourTime { get; set; }
   public string Coords { get; set; } = string.Empty;
   
   public int? ImageId { get; set; }
   public ImageEntity? ImageEntity { get; set; }
   public ICollection<UserTourEntity> UserTours { get; set; } = new List<UserTourEntity>();
}