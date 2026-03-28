using RyazanTrip.App.Services;
using RyazanTrip.DataAccess.Postgres;

namespace RyazanTrip.App;

public interface IRyazanTripAppSecureContext {
   public YandexMicroService YandexMicroMicroService { get; }
   public RyazanTripDbContext Context { get; }
}