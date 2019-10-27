using Lelantus.Models;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lelantus.Services
{
    public class MediaService : CosmosDbService<Media>, IMediaService
    {
        public MediaService(IOptions<CosmosDbOptions> options) : base(options)
        {
        }

        public async Task<IEnumerable<Media>> GetAllByDeviceAsync(string deviceId)
        {
            TableQuery<Media> query = new TableQuery<Media>().Take(100).Where(TableQuery.GenerateFilterCondition("DeviceId", QueryComparisons.Equal, deviceId));
            var result = await Table.ExecuteQuerySegmentedAsync(query, null);
            return result;
        }
    }
}
