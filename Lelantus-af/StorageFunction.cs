using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Lelantus_af
{
    public static class StorageFunction
    {
        private const string _tableName = "Media";
        private static CloudStorageAccount _account;

        [FunctionName("StorageFunction")]
        public static void Run(
            [BlobTrigger("lelantus-sa-container/{name}", Connection = "StorageConnectionString")]Stream myBlob,
            ILogger logger,
            string name, ILogger log,
            ExecutionContext context)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config["TableConnectionString"];
            _account = CloudStorageAccount.Parse(connectionString);

            var media = MediaEntityUtils.GetMediaEntity(name);
            var table = GetMediaTable().Result;
            var insertedMedia = InsertOrMergeEntityAsync(table, media).Result;

            Console.WriteLine("Media inserted: " + media.Name);
        }


        private static async Task<CloudTable> GetMediaTable()
        {
            CloudTableClient tableClient = _account.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(_tableName);
            if (await table.CreateIfNotExistsAsync())
            {
                Console.WriteLine("Created Table named: {0}", _tableName);
            }
            else
            {
                Console.WriteLine("Table {0} already exists", _tableName);
            }

            return await Task.FromResult(table);
        }

        private static async Task<MediaEntity> InsertOrMergeEntityAsync(CloudTable table, MediaEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                MediaEntity insertedMedia = result.Result as MediaEntity;

                if (result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
                }

                return insertedMedia;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }
    }
}
