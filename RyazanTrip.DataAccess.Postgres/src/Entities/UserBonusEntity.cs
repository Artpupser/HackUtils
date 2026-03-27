namespace RyazanTrip.DataAccess.Postgres.Entities;

public class UserBonusEntity {
   public int UserBonusId { get; set; }
   public int? UserId { get; set; }
   public int? BonusId { get; set; }

   public string? Status { get; set; }

   public UserEntity? User { get; set; }
   public BonusEntity? Bonus { get; set; }
}