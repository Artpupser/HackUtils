namespace RyazanTrip.DataAccess.Postgres.Entities;

public class SessionEntity{
    public int Id { get; set; }
    public int? UserId { get; set; }

    public string? Token { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }

    public UserEntity? UserEntity { get; set; }
}