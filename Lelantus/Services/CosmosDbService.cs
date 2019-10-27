using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lelantus.Services
{
    public class CosmosDbService<T> : ICosmosDbService<T> where T : class, ITableEntity, new()
    {
        private readonly CloudStorageAccount _account;
        public readonly CloudTable Table;
        
        public CosmosDbService(IOptions<CosmosDbOptions> options)
        {
            _account = CreateCloudStorageAccount(options.Value.ConnectionString);
            Table = GetTableClient(typeof(T).Name);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            TableQuery<T> query = new TableQuery<T>().Take(100);
            var result = await Table.ExecuteQuerySegmentedAsync<T>(query, null);
            return result;
        }

        public async Task<T> GetAsync(string partitionKey, string rowKey)
        {
            try
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
                TableResult result = await Table.ExecuteAsync(retrieveOperation);
                T entity = result.Result as T;
                if (entity != null)
                {
                    Console.WriteLine("\t{0}\t{1}\t{2}\t{3}", entity.PartitionKey, entity.RowKey);
                }

                if (result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of Retrieve Operation: " + result.RequestCharge);
                }

                return entity;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        public async Task<T> InsertOrMergeAsync(T entity)
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
                TableResult result = await Table.ExecuteAsync(insertOrMergeOperation);
                T insertedCustomer = result.Result as T;

                if (result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
                }

                return insertedCustomer;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        public async Task DeleteAsync(T deleteEntity)
        {
            try
            {
                if (deleteEntity == null)
                {
                    throw new ArgumentNullException("deleteEntity");
                }

                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                TableResult result = await Table.ExecuteAsync(deleteOperation);

                if (result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of Delete Operation: " + result.RequestCharge);
                }

            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        private CloudStorageAccount CreateCloudStorageAccount(string connectionString)
        {
            return CloudStorageAccount.Parse(connectionString);
        }

        private CloudTable GetTableClient(string tableName)
        {
            var tableClient = _account.CreateCloudTableClient();

            var table = tableClient.GetTableReference(tableName);

            // Create the cloud table client to interacting with the table service   
            if (table.CreateIfNotExists())
            {
                Console.WriteLine("Created Table named: {0}", tableName);
            }
            else
            {
                Console.WriteLine("Table {0} already exists", tableName);
            }
            return table;
        }
    }
}
