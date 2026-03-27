namespace RyazanTrip.DataAccess.Postgres.Entities;

public class LevelEntity {
   public int LevelId { get; set; }
   public string? Name { get; set; }
   public int? RequiredExp { get; set; }

   public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}