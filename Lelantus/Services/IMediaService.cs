using Lelantus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lelantus.Services
{
    public interface IMediaService : ICosmosDbService<Media>
    {
        Task<IEnumerable<Media>> GetAllByDeviceAsync(string deviceId);
    }
}
