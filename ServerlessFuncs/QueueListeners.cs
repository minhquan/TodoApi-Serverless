using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ServerlessFuncs
{
    public static class QueueListeners
    {
        [FunctionName("QueueListeners")]
        public static async Task Run([QueueTrigger("todos", Connection = "AzureWebJobsStorage")]Todo todo, 
            [Blob("todos", Connection ="AzureWebJobsStorage")] CloudBlobContainer container,
            ILogger log)
        {
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference($"{todo.Id}.txt");
            await blob.UploadTextAsync($"Create a new task: {todo.TaskDescription}");
            log.LogInformation($"C# Queue trigger function processed: {todo.TaskDescription}");
        }
    }
}
