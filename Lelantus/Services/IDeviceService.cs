using Lelantus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lelantus.Services
{
    public interface IDeviceService : ICosmosDbService<Device>
    {
    }
}
