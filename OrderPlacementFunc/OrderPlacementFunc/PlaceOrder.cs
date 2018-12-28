using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Queue;

namespace OrderPlacementFunc
{
    public static class PlaceOrder
    {
        [FunctionName("PlaceOrder")]
        public static async void Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HTTP trigger function publishing queue.");
            string connString = "DefaultEndpointsProtocol=https;AccountName=jerry110160;AccountKey=TW8zajmxoCq0rZTjR9BWn9TMKneLviUKmgbk4Nwsp1J3iCPiOiSjClYOLY8m9leW8APWczIjDqVpDidN12THwQ==;EndpointSuffix=core.windows.net";
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connString);
                //CloudStorageAccount.Parse(connString);
                // CloudConfigurationManager.GetSetting(connString));

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            CloudQueue queue = queueClient.GetQueueReference("order-placement");

            // Create the queue if it doesn't already exist.
            //queue.CreateIfNotExists();

            // Create a message and add it to the queue.
            CloudQueueMessage message = new CloudQueueMessage("Hello, World");
            await queue.AddMessageAsync(message);

        }
    }
}
