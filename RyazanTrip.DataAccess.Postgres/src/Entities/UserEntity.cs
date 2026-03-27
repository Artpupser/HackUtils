namespace RyazanTrip.DataAccess.Postgres.Entities;

public class UserEntity
{
    public int UserId { get; set; }
    public int? RoleId { get; set; }
    public int? LevelId { get; set; }

    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? FullName { get; set; }
    public int? Experience { get; set; }

    public RoleEntity? RoleEntity { get; set; }
    public LevelEntity? LevelEntity { get; set; }

    public ICollection<SessionEntity> Sessions { get; set; } = new List<SessionEntity>();
    public ICollection<UserTourEntity> UserTours { get; set; } = new List<UserTourEntity>();
    public ICollection<MushroomSubmissionEntity> MushroomSubmissions { get; set; } = new List<MushroomSubmissionEntity>();
    public ICollection<UserBonusEntity> UserBonuses { get; set; } = new List<UserBonusEntity>();
}