using Microsoft.EntityFrameworkCore;
using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.DataAccess.Postgres;

public class RyazanTripDbContext(DbContextOptions<RyazanTripDbContext> options) : DbContext(options)
{
    public DbSet<RoleEntity> RolesSet { get; set; }
    public DbSet<LevelEntity> LevelsSet { get; set; }
    public DbSet<UserEntity> UsersSet { get; set; }
    public DbSet<SessionEntity> SessionsSet { get; set; }
    public DbSet<ImageEntity> ImagesSet { get; set; }
    public DbSet<TourEntity> ToursSet { get; set; }
    public DbSet<UserTourEntity> UserToursSet { get; set; }
    public DbSet<PaymentEntity> PaymentsSet { get; set; }
    public DbSet<MushroomEntity> MushroomsSet { get; set; }
    public DbSet<MushroomSubmissionEntity> MushroomSubmissionsSet { get; set; }
    public DbSet<BonusEntity> BonusesSet { get; set; }
    public DbSet<UserBonusEntity> UserBonusesSet { get; set; }
    public DbSet<QrCodeEntity> QrCodesSet { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RyazanTripDbContext).Assembly);
    }
}
