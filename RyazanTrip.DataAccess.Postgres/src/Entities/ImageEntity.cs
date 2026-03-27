namespace RyazanTrip.DataAccess.Postgres.Entities;

public class ImageEntity
{
    public int ImageId { get; set; }
    public string? Url { get; set; }

    public ICollection<TourEntity> Tours { get; set; } = new List<TourEntity>();
    public ICollection<MushroomEntity> Mushrooms { get; set; } = new List<MushroomEntity>();
    public ICollection<MushroomSubmissionEntity> Submissions { get; set; } = new List<MushroomSubmissionEntity>();
}