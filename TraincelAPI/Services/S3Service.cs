using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Threading.Tasks;
using TraincelAPI.Services.Interface;
using System.IO;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

namespace TraincelAPI.Services
{
    public class S3Service: IS3Service
    {
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.EUWest2;
        private static IAmazonS3 s3Client;
        private IConfiguration _configuration;
        public S3Service(IConfiguration configuration)
        {
            _configuration = configuration;
        }
     
        public async Task<String> UploadFile(IFormFile file, string readerName, string subFolderName)
        {
            try
            {
                string fileName = file.FileName;
                string objectKey = $"{subFolderName}/{readerName}/{fileName}";
                using Stream fileToUpload = file.OpenReadStream();
                var putObjectRequest = new PutObjectRequest
                {
                    BucketName = _configuration.GetSection("S3_BUCKET")["BucketName"],
                    Key = objectKey,
                    InputStream = fileToUpload,
                    ContentType = file.ContentType
                };

                var response = await s3Client.PutObjectAsync(putObjectRequest);

                return GeneratePreSignedURL(objectKey);

            }
            catch
            {
                return null;
            }
        }

        private string GeneratePreSignedURL(string objectKey)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _configuration.GetSection("S3_BUCKET")["BucketName"],
                Key = objectKey,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddHours(24)
            };

            string url = s3Client.GetPreSignedURL(request);
            return url;
        }
    }
}
