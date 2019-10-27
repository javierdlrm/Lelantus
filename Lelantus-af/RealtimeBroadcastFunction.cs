using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Lelantus_af
{
    public static class RealtimeBroadcastFunction
    {
        [FunctionName("RealtimeBroadcastFunction")]
        public static async Task<IActionResult> Run(
            [BlobTrigger("lelantus-sa-container/{name}", Connection = "StorageConnectionString")]Stream myBlob,
            string name, ILogger log)
        {
            var media = MediaEntityUtils.GetMediaEntity(name);

            var httpClient = new HttpClient();
            var result = await httpClient.PostAsync(new Uri("https://https://lelantus.azurewebsites.net/api/MediaHub"), new StringContent(JsonConvert.SerializeObject(media)));
            return new OkResult();
        }

        private static async Task BroadcastMedia(IAsyncCollector<SignalRMessage> signalRMessages, MediaEntity media) {

            //var mediaObject = JsonConvert.SerializeObject(media);

            Console.WriteLine("Broadcasting");

            

            Console.WriteLine("Media broadcasted: " + media.MediaId);
        }
    }
}
