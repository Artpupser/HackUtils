namespace RyazanTrip.DataAccess.Postgres.Entities;

public class RoleEntity
{
    public int RoleId { get; set; }
    public string? Name { get; set; }
    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}