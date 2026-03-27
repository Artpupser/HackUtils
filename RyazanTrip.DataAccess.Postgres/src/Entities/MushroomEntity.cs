namespace RyazanTrip.DataAccess.Postgres.Entities;

public class MushroomEntity
{
    public int MushroomId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }

    public int? ImageId { get; set; }
    public ImageEntity? Image { get; set; }

    public ICollection<MushroomSubmissionEntity> Submissions { get; set; } = new List<MushroomSubmissionEntity>();
}
