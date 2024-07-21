using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class AwsController : Controller
    {
        public IActionResult AwsS3()
        {
            return View();
        }

        public async Task<string> GetStringFromAwsS3()
        {
            string accessKey = "";
            string secretKey = "";
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            RegionEndpoint defaultEndpoint = RegionEndpoint.USEast1;
            var request = new GetObjectRequest()
            {
                BucketName = "",
                Key = ""
            };

            string responseBody = null;

            using (var client = new AmazonS3Client(credentials, defaultEndpoint))
            using (GetObjectResponse response = await client.GetObjectAsync(request))
            using (Stream responseStream = response.ResponseStream)
            using (var reader = new StreamReader(responseStream))
            {
                responseBody = await reader.ReadToEndAsync();
            }

            return responseBody;
        }

        public async Task<List<string>> GetListStringFromAwsS3()
        {
            string accessKey = "";
            string secretKey = "";
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            RegionEndpoint defaultEndpoint = RegionEndpoint.USEast1;

            var request = new ListObjectsV2Request()
            {
                BucketName = "",
                Prefix = ""
            };

            var keys = new List<string>();

            using (var client = new AmazonS3Client(credentials, defaultEndpoint))
            {
                var response = await client.ListObjectsV2Async(request);

                keys = response.S3Objects.Select(x => x.Key).ToList();
            }

            return keys;
        }

        public async Task<dynamic> GetObjFromAwsS3()
        {
            string accessKey = "";
            string secretKey = "";
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            RegionEndpoint defaultEndpoint = RegionEndpoint.USEast1;

            var request = new GetObjectRequest()
            {
                BucketName = "",
                Key = ""
            };

            var obj = default(dynamic);

            string responseBody = null;

            using (var client = new AmazonS3Client(credentials, defaultEndpoint))
            using (GetObjectResponse response = await client.GetObjectAsync(request))
            using (Stream responseStream = response.ResponseStream)
            using (var reader = new StreamReader(responseStream))
            {
                responseBody = await reader.ReadToEndAsync();

                obj = JsonConvert.DeserializeObject<dynamic>(responseBody);
            }

            return obj;
        }

        public async Task<List<S3Object>> GetListObjFromAwsS3()
        {
            string accessKey = "";
            string secretKey = "";
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            RegionEndpoint defaultEndpoint = RegionEndpoint.USEast1;

            List<S3Object> objects = null;
            var request = new ListObjectsV2Request()
            {
                BucketName = "",
                Prefix = ""
            };

            var keys = new List<string>();

            using (var client = new AmazonS3Client(credentials, defaultEndpoint))
            {
                var response = await client.ListObjectsV2Async(request);

                objects = response.S3Objects;
            }

            return objects;
        }

        public async Task<bool> AddObjToAwsS3()
        {
            string accessKey = "";
            string secretKey = "";
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            RegionEndpoint defaultEndpoint = RegionEndpoint.USEast1;

            bool succeeded = false;

            var request = new PutObjectRequest()
            {
                BucketName = "",
                Key = ""
            };
            request.CannedACL = S3CannedACL.PublicRead;

            byte[] data = default;

            using (var client = new AmazonS3Client(credentials, defaultEndpoint))
            using (request.InputStream = new MemoryStream(data))
            {
                PutObjectResponse response = await client.PutObjectAsync(request);
                succeeded = response.HttpStatusCode == HttpStatusCode.OK;
            }

            return succeeded;
        }
    }
}
