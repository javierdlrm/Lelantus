using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace Lelantus.Services
{
    public interface ICosmosDbService<T> where T : class, ITableEntity
    {
        Task DeleteAsync(T deleteEntity);
        Task<T> InsertOrMergeAsync(T entity);
        Task<T> GetAsync(string partitionKey, string rowKey);
        Task<IEnumerable<T>> GetAllAsync();
    }
}