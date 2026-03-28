using PupaMVCF.Framework.Core;

namespace RyazanTrip.App.Procedures;

public abstract class ProcedureBase : IDisposable {
   protected readonly HttpClient HttpClient;
   protected ProcedureBase(Request request) {
      HttpClient = new HttpClient();
   }
   public void Dispose() {
      HttpClient.Dispose();
   }
}