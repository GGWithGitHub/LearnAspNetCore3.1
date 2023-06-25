using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository
{
    // I have installed following package -
    // AWSSDK.Core
    // AWSSDK.S3
    public abstract class S3Repository<T>
    {
        private string accessKey;
        private string secretKey;
        private static readonly RegionEndpoint defaultEndpoint = RegionEndpoint.USEast1;

        protected BasicAWSCredentials Credentials
        {
            get
            {
                return new BasicAWSCredentials(accessKey, secretKey);
            }
        }

        public S3Repository(string awsAccessKey, string awsSecretKey)
        {
            accessKey = awsAccessKey;
            secretKey = awsSecretKey;
        }
        public async Task<string> GetString(string bucket, string key, RegionEndpoint endpoint = null)
        {
            var request = new GetObjectRequest()
            {
                BucketName = bucket,
                Key = key
            };

            string responseBody = null;

            using (var client = new AmazonS3Client(Credentials, endpoint ?? defaultEndpoint))
            using (GetObjectResponse response = await client.GetObjectAsync(request))
            using (Stream responseStream = response.ResponseStream)
            using (var reader = new StreamReader(responseStream))
            {
                responseBody = await reader.ReadToEndAsync();
            }

            return responseBody;
        }

        public async Task<List<string>> ListObjects(string bucket, string prefix, RegionEndpoint endpoint = null)
        {
            var request = new ListObjectsV2Request()
            {
                BucketName = bucket,
                Prefix = prefix
            };

            var keys = new List<string>();

            using (var client = new AmazonS3Client(Credentials, endpoint ?? defaultEndpoint))
            {
                var response = await client.ListObjectsV2Async(request);

                keys = response.S3Objects.Select(x => x.Key).ToList();
            }

            return keys;
        }

        public async Task<List<S3Object>> ListRawObjects(string bucket, string prefix, RegionEndpoint endpoint = null)
        {
            List<S3Object> objects = null;
            var request = new ListObjectsV2Request()
            {
                BucketName = bucket,
                Prefix = prefix
            };

            var keys = new List<string>();

            using (var client = new AmazonS3Client(Credentials, endpoint ?? defaultEndpoint))
            {
                var response = await client.ListObjectsV2Async(request);

                objects = response.S3Objects;
            }

            return objects;
        }

        public async Task<T> GetObject(string bucket, string key, RegionEndpoint endpoint = null)
        {
            var request = new GetObjectRequest()
            {
                BucketName = bucket,
                Key = key
            };

            var obj = default(T);

            string responseBody = null;

            try
            {

                using (var client = new AmazonS3Client(Credentials, endpoint ?? defaultEndpoint))
                using (GetObjectResponse response = await client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                using (var reader = new StreamReader(responseStream))
                {
                    responseBody = await reader.ReadToEndAsync();

                    obj = JsonConvert.DeserializeObject<T>(responseBody);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return obj;
        }

        //gg
        public async Task<dynamic> GetXmlToJsonString(string bucket, string key, RegionEndpoint endpoint = null)
        {
            var request = new GetObjectRequest()
            {
                BucketName = bucket,
                Key = key
            };

            dynamic obj = "";

            string responseBody = null;

            try
            {

                using (var client = new AmazonS3Client(Credentials, endpoint ?? defaultEndpoint))
                using (GetObjectResponse response = await client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                using (var reader = new StreamReader(responseStream))
                {
                    responseBody = await reader.ReadToEndAsync();

                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml(responseBody);

                    string json = JsonConvert.SerializeXmlNode(doc);

                    obj = Newtonsoft.Json.Linq.JObject.Parse(json);

                    //var menu = Convert.ToString(data["response"]["menu"]);

                    //var menuData = JsonConvert.DeserializeObject<List<T>>(menu);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return obj;
        }

        public async Task<List<T>> GetObjects(string bucket, string key, RegionEndpoint endpoint = null)
        {
            var request = new GetObjectRequest()
            {
                BucketName = bucket,
                Key = key
            };

            var obj = default(List<T>);

            string responseBody = null;

            try
            {

                using (var client = new AmazonS3Client(Credentials, endpoint ?? defaultEndpoint))
                using (GetObjectResponse response = await client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                using (var reader = new StreamReader(responseStream))
                {
                    responseBody = await reader.ReadToEndAsync();

                    obj = JsonConvert.DeserializeObject<List<T>>(responseBody);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return obj;
        }

        public async Task<bool> DeleteObject(string bucket, string key, RegionEndpoint endpoint = null)
        {
            bool succeeded = false;
            var request = new DeleteObjectRequest()
            {
                BucketName = bucket,
                Key = key
            };

            using (var client = new AmazonS3Client(Credentials, endpoint ?? defaultEndpoint))
            {
                DeleteObjectResponse response = await client.DeleteObjectAsync(request);
                succeeded = response.HttpStatusCode == HttpStatusCode.NoContent;
            }

            return succeeded;
        }
        //gg

        public async Task<bool> AddObject(string bucket, string key, byte[] data, bool makePublic = true, RegionEndpoint endpoint = null)
        {
            bool succeeded = false;

            var request = new PutObjectRequest()
            {
                BucketName = bucket,
                Key = key
            };

            if (makePublic)
                request.CannedACL = S3CannedACL.PublicRead;

            using (var client = new AmazonS3Client(Credentials, endpoint ?? defaultEndpoint))
            using (request.InputStream = new MemoryStream(data))
            {
                PutObjectResponse response = await client.PutObjectAsync(request);
                succeeded = response.HttpStatusCode == HttpStatusCode.OK;
            }

            return succeeded;
        }


        public string GetExpiringUrl(string bucket, string key, DateTime expires, RegionEndpoint endpoint = null)
        {
            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest()
            {
                BucketName = bucket,
                Expires = expires,
                Key = key
            };

            using var client = new AmazonS3Client(Credentials, endpoint ?? defaultEndpoint);

            return client.GetPreSignedURL(request);
        }
    }
}
