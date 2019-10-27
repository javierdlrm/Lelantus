using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Lelantus_af
{
    public static class RealtimeBroadcastFunction
    {
        [FunctionName("RealtimeBroadcastFunction")]
        public static void Run(
            [BlobTrigger("lelantus-sa-container/{name}", Connection = "StorageConnectionString")]Stream myBlob,
            string name, ILogger log)
        {
            var media = MediaEntityUtils.GetMediaEntity(name);

            var httpClient = new HttpClient();
            var myContent = JsonConvert.SerializeObject(media);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = httpClient.PostAsync(new Uri("https://lelantus.azurewebsites.net/api/MediaHub"), byteContent).Result;
        }
    }
}
