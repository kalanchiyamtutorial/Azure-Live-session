using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kalanchiyam.API.Security.Helpers
{
    public static class QueueClient
    {
        private static string queueConnection = "Storageconnection";
        public static async Task<CloudQueue> CreateQueueAsync(string queueName)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(queueConnection);
            CloudQueueClient queueClient = cloudStorageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference(queueName);
            try
            {
                await queue.CreateIfNotExistsAsync();
            }
            catch
            {

                throw;
            }

            return queue;
        }

        public static async Task AddQueueMessagesAsync(string Message)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(queueConnection);
            CloudQueueClient queueClient = cloudStorageAccount.CreateCloudQueueClient();
            string queueName = System.Guid.NewGuid().ToString();
            CloudQueue queue = queueClient.GetQueueReference(queueName);
            await queue.CreateIfNotExistsAsync();
            await queue.AddMessageAsync(new CloudQueueMessage(Message));

        }
        public static async Task UpdateEnqueuedMessageAsync(CloudQueue queue, string Message)
        {

            await queue.AddMessageAsync(new CloudQueueMessage(Message));
            CloudQueueMessage message = await queue.GetMessageAsync();
            message.SetMessageContent(Message);
            await queue.UpdateMessageAsync(
                message,
                TimeSpan.Zero,  // For the purpose of the sample make the update visible immediately
                MessageUpdateFields.Content |
                MessageUpdateFields.Visibility);
        }
        public static async Task DeleteQueueAsync(string queueId)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(queueConnection);
            CloudQueueClient queueClient = cloudStorageAccount.CreateCloudQueueClient();
            CloudQueue cloudQueue = queueClient.GetQueueReference(queueId);
            await cloudQueue.DeleteIfExistsAsync();
        }
    }
}
