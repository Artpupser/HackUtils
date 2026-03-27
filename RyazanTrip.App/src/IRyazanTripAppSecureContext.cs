using RyazanTrip.DataAccess.Postgres;

namespace RyazanTrip.App;

public interface IRyazanTripAppSecureContext {
   public RyazanTripDbContext Context { get; }
}