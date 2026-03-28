using Grpc.Core;
using Grpc.Net.Client;

namespace RyazanTrip.App.Services;
using Proto;

public sealed class YandexMicroService : IAnyMicroService {
   private readonly YandexAuthService.YandexAuthServiceClient _client;
   private readonly GrpcChannel _channel;
   public YandexMicroService(IConfiguration configuration) {
      var yandexConfig = configuration.GetSection("YandexServiceConnect");
      var grpcUrl = $"http://{yandexConfig["Host"]}:{yandexConfig["Port"]}";
      _channel = GrpcChannel.ForAddress(grpcUrl);
      _client = new YandexAuthService.YandexAuthServiceClient(_channel);
   }
      
   public async Task<string> GetAuthUrlAsync(CancellationToken cancellationToken)
   {
      var response = await _client.GetYandexAuthURLAsync(new Empty(), cancellationToken: cancellationToken);
      return response.Url;
   }

   public async Task<YandexUserResponse> ExchangeCodeAsync(string code, CancellationToken cancellationToken)
   {
      var response = await _client.ExchangeCodeAsync(new YandexCodeRequest
      {
         Code = code
      }, cancellationToken: cancellationToken);
      return response;
   }

   public async Task Connect(CancellationToken cancellationToken) {
      if (_channel.State != ConnectivityState.Ready) {
         await _channel.ConnectAsync(cancellationToken);
      }
   }
}