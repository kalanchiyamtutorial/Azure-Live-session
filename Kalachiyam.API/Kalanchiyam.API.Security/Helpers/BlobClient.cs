using System;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace Kalanchiyam.API.Security.Helpers
{
    public static class BlobClient
    {
        private static string blobConnection = "Storageconnection";
        private static string containerName = "containername";
        public static async Task<string> DownloadString(string blobUri)
        {
            string result = string.Empty;
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blobConnection);
                CloudBlobClient myBlobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = myBlobClient.GetContainerReference(containerName);
                CloudBlockBlob myBlob = container.GetBlockBlobReference(blobUri);
                result = myBlob.DownloadTextAsync().Result;
            }
            catch (Exception ex)
            {
                throw;
                ex.ToString();
            }
            return result;
        }

        public static async Task<Stream> DownloadStream(string blobUri)
        {
            Stream ms = new MemoryStream();
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blobConnection);
                CloudBlobClient myBlobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = myBlobClient.GetContainerReference(containerName);
                CloudBlockBlob myBlob = container.GetBlockBlobReference(blobUri);
                await myBlob.DownloadToStreamAsync(ms);
            }
            catch (Exception ex)
            {
                throw;
                ex.ToString();
            }
            return ms;

        }

        public static async Task<bool> UploadStream(Stream stream, string blobUri)
        {
            bool result = false;
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blobConnection);
                CloudBlobClient myBlobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = myBlobClient.GetContainerReference(containerName);
                CloudBlockBlob myBlob = container.GetBlockBlobReference(blobUri);
                await myBlob.UploadFromStreamAsync(stream);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                throw;
                ex.ToString();
            }
            return result;
        }
        public static async Task<bool> deletefromBlob( string filename)
        {
            bool result = false;
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blobConnection);
                CloudBlobClient myBlobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = myBlobClient.GetContainerReference(containerName);
                CloudBlockBlob myBlob = container.GetBlockBlobReference(filename);
                result =await myBlob.DeleteIfExistsAsync();
                
            }
            catch (Exception ex)
            {

                throw;
                ex.ToString();
            }
            return result;
        }

       
    }
}
