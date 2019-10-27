using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lelantus.Models
{
    public class Media : TableEntity, ITableEntity
    {
        public string MediaId { get; set; }
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
        public string Url { get; set; }
        public String Type { get; set; }
        public DateTime Date { get; set; }

        public bool Visible { get; set; }
    }
}
