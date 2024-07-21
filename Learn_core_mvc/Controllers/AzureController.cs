using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class AzureController : Controller
    {
        public IActionResult AzureBlob()
        {
            return View();
        }

        public string GetFileURL()
        {
            string containerName = "";
            string blobName = "";
            string connectionString = "";
            // Create a CloudStorageAccount object
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create a CloudBlobClient object
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Reference the container and blob
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);
            // Set the expiration time for the SAS token (e.g., 1 hour from now)
            DateTime expirationTime = DateTime.UtcNow.AddHours(1);

            // Create a SAS token with read permission and the specified expiration time
            string sasToken = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = expirationTime
            });

            Uri sasUri = new Uri(blob.Uri + sasToken);

            return sasUri.ToString();
        }

        public async Task<bool> UploadFileToBlob()
        {
            string connectionString = "";
            string filePath = "";
            string containerName = "";
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);


            var blobName = System.IO.Path.GetFileName(filePath);
            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(filePath))
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }

            return true;
        }

        public async Task<bool> GetAllBlobs()
        {
            string containerName = "";
            string connectionString = "";

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            BlobContinuationToken continuationToken = null;
            do
            {
                BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(continuationToken);
                continuationToken = resultSegment.ContinuationToken;

                foreach (IListBlobItem blobItem in resultSegment.Results)
                {
                    if (blobItem is CloudBlockBlob blockBlob)
                    {
                        Console.WriteLine($"Block Blob Name: {blockBlob.Name}");
                        // Add additional processing logic as needed
                    }
                    else if (blobItem is CloudPageBlob pageBlob)
                    {
                        Console.WriteLine($"Page Blob Name: {pageBlob.Name}");
                        // Add additional processing logic as needed
                    }
                    // Add more conditions if your container supports other types of blobs
                }

            } while (continuationToken != null);

            return true;
        }


        public async Task<bool> DeleteFile()
        {
            string containerName = "";
            string blobName = "";
            string connectionString = "";
            // Create a CloudStorageAccount object
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create a CloudBlobClient object
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Reference the container and blob
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);

            // Delete the blob
            await blob.DeleteIfExistsAsync();

            return true;

        }
    }
}
