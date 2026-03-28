using Grpc.Core;

namespace RyazanTrip.App.Services;

public interface IAnyMicroService {
   public Task Connect(CancellationToken cancellationToken);
}