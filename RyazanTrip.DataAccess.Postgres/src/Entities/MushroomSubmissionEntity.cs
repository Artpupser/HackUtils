namespace RyazanTrip.DataAccess.Postgres.Entities;

public class MushroomSubmissionEntity
{
    public int SubmissionId { get; set; }
    public int? UserId { get; set; }
    public int? MushroomId { get; set; }
    public int? ImageId { get; set; }

    public string? Status { get; set; }

    public UserEntity? User { get; set; }
    public MushroomEntity? Mushroom { get; set; }
    public ImageEntity? Image { get; set; }
}