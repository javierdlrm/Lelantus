using Lelantus.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lelantus.Services
{
    public class DeviceService : CosmosDbService<Device>, IDeviceService
    {
        public DeviceService(IOptions<CosmosDbOptions> options) : base(options)
        {
        }
    }
}
