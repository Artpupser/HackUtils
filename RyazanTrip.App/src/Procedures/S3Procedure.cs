using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

using PupaMVCF.Framework.Core;

using RyazanTrip.App.Models;

namespace RyazanTrip.App.Procedures;

public record S3UploadResult
{
   internal bool Success { get; set; }
   internal string Url { get; set; } = string.Empty;  
   
   public S3UploadResult(bool success, string url) {
      Success = success;
      Url = url;
   }
}

public sealed class S3Procedure {
   private readonly IAmazonS3 _s3Client;
   private readonly TransferUtility _transferUtility;
   public string BucketName { get; set; } = string.Empty;
   public S3Procedure(IConfiguration configuration) {
      var s3Configuration = configuration.GetSection("Selectel");
      BucketName = s3Configuration["BucketName"] ?? throw new Exception("BucketName is not configured");
      var config = new AmazonS3Config
      {
         ServiceURL = s3Configuration["EndPoint"] ?? throw new Exception("EndPoint is not configured"),         
         ForcePathStyle = true,          
         RegionEndpoint = Amazon.RegionEndpoint.USEast1 // фиктивный, т.к. используем ServiceURL
      };
      _s3Client = new AmazonS3Client( s3Configuration["AccessKey"] ?? throw new Exception("AccessKey is not configured"), s3Configuration["SecretKey"] ?? throw new Exception("SecretKey is not configured"), config);
      _transferUtility = new TransferUtility(_s3Client);
   }
   public async Task<S3UploadResult> UploadImageBytesAsync(byte[] imageBytes, string objectKey, CancellationToken cancellationToken) {
      var result = new S3UploadResult(false, GetPublicUrl(objectKey));
      try
      {
         using var stream = new MemoryStream(imageBytes);
         var request = new PutObjectRequest {
            BucketName = BucketName,
            Key = objectKey,
            InputStream = stream,
            ContentType = "image/webp",
            CannedACL = S3CannedACL.PublicRead
         };
         await _s3Client.PutObjectAsync(request, cancellationToken);
         result.Success = true;
      }
      catch (Exception e) {
         WebApp.SecureContextInstance.Logger.LogInformation("Ошибка при загрузке изображения в S3: {Message}", e);
         result.Success = false;
      }

      return result;
   }
   
   public async Task<bool> ImageExistsAsync(string objectKey, CancellationToken cancellationToken = default)
   {
      try 
      {
         var headBucket =  await _s3Client.GetObjectAsync(new GetObjectRequest() 
         { 
            BucketName = BucketName,
            Key = objectKey,
         }, cancellationToken);
         return headBucket == null;
      }
      catch 
      {
         return false; // 404 или любая ошибка = файл не существует
      }
   }
   
   private string GetPublicUrl(string fileName) => $"https://afc5e8a7-ce80-4666-abe8-61a18f87a6b2.selstorage.ru/{fileName}";
}