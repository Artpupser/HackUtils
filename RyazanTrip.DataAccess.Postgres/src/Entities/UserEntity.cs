namespace RyazanTrip.DataAccess.Postgres.Entities;

public class UserEntity {
   public int Id { get; set; }
   public int RoleId { get; set; }
   public int LevelId { get; set; }

   public string Email { get; set; } = string.Empty;
   public string PasswordHash { get; set; } = string.Empty;
   public string Username { get; set; } = string.Empty;
   public string Town { get; set; } = string.Empty;
   public int? Experience { get; set; }
   public RoleEntity? RoleEntity { get; set; }
   public LevelEntity? LevelEntity { get; set; }
   public ICollection<SessionEntity> Sessions { get; set; } = new List<SessionEntity>();
   public ICollection<UserTourEntity> UserTours { get; set; } = new List<UserTourEntity>();
   public ICollection<MushroomSubmissionEntity> MushroomSubmissions { get; set; } =
      new List<MushroomSubmissionEntity>();
   public ICollection<UserBonusEntity> UserBonuses { get; set; } = new List<UserBonusEntity>();
}