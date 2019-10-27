using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Lelantus_af
{
    public class MediaEntity : TableEntity
    {
        public MediaEntity()
        {
        }

        public MediaEntity(string id, string deviceId, string name, string format, string url, bool visible=false)
        {
            PartitionKey = DeviceId = deviceId;
            RowKey = MediaId = id;
            Name = name;
            Visible = visible;
            Format = format;
            Url = url;
            Type = GetTypeFromFormat(format).ToString();
            Date = DateTime.UtcNow;
        }

        public string MediaId { get; set; }
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }

        public bool Visible { get; set; }

        private MediaType GetTypeFromFormat(string format)
        {
            switch (format)
            {
                case "mp3":
                case "wav":
                    return MediaType.Audio;
                case "mp4":
                case "avi":
                    return MediaType.Video;
                case "png":
                case "jpg":
                case "jpeg":
                    return MediaType.Image;
                default:
                    throw new Exception("Unknown format");
            }
        }
    }

    public enum MediaType
    {
        Video,
        Image,
        Audio
    }
}
