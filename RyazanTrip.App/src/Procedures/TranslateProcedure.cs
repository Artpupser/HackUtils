using System.Text.Json;

using PupaMVCF.Framework.Core;

namespace RyazanTrip.App.Procedures;

public sealed class TranslateProcedure : ProcedureBase {
   public TranslateProcedure(Request request) : base(request) {
   }
   
   public async Task<string?> Translate(string text, string from, string to, CancellationToken cancellationToken) {
      var json = await HttpClient.GetStringAsync($"https://translate.googleapis.com/translate_a/single?client=gtx&sl={from}&tl={to}&dt=t&q={text}", cancellationToken);
      return JsonDocument.Parse(json).RootElement[0][0][0].GetString() ?? null;
   }
}