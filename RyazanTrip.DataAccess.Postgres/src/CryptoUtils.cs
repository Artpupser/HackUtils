using System.Security.Cryptography;
using System.Text;

namespace RyazanTrip.DataAccess.Postgres;

public static class CryptoUtils {
   public static string ComputeSha256Hash(string input) {
      var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
      var builder = new StringBuilder();
      foreach (var t in bytes)
         builder.Append(t.ToString("x2"));
      return builder.ToString();
   }
}