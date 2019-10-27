using System;
using System.Collections.Generic;
using System.Text;

namespace Lelantus_af
{
    public static class MediaEntityUtils
    {
        private const string ENDPOINT = "https://lelantussa.blob.core.windows.net/lelantus-sa-container";

        public static MediaEntity GetMediaEntity(string path)
        {
            var segments = path.Split('/', '.');
            string deviceId = segments[0];
            string fileName = segments[1];
            string format = segments[2];
            string url = ENDPOINT + "/" + deviceId + "/" + fileName + "." + format;
            return new MediaEntity(Guid.NewGuid().ToString(), deviceId, fileName, format, url, visible: true);
        }
    }
}
