namespace RyazanTrip.DataAccess.Postgres.Entities;

public class BonusEntity
{
    public int BonusId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public short? DiscountPercent { get; set; }
    public string? QrCode { get; set; }

    public ICollection<UserBonusEntity> UserBonuses { get; set; } = new List<UserBonusEntity>();
}