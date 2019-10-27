using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lelantus.Services
{
    public class CosmosDbOptions
    {
        public string ConnectionString { get; set; }
    }
}
