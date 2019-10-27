using Microsoft.Azure.Devices.Client;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IoTSimulatedDevice
{
    class Program
    {
        private const string DeviceConnectionString = "HostName=lelantus-ioth.azure-devices.net;DeviceId=4918ae98-2c60-4536-aae4-a471b0bfc962;SharedAccessKey=dCa4KdCglbooOl+PMpfr+oCxYe8kZjP9B9pM//YrQNg=";
        private const string DevicePrimaryKey = "dCa4KdCglbooOl+PMpfr+oCxYe8kZjP9B9pM//YrQNg=";
        private static DeviceClient _deviceClient;
        private const string FolderPath = @"C:\Users\javie\Workspace\Temporary\IoTSimulatedDevice\Media";

        static void Main(string[] args)
        {
            _deviceClient ??= DeviceClient.CreateFromConnectionString(DeviceConnectionString, TransportType.Http1);
            UploadFilesInFolder(FolderPath).Wait();
        }

        private static async Task UploadFilesInFolder(string path)
        {
            foreach (var file in Directory.GetFiles(path))
            {
                await UploadFile(file);
            }
        }

        private static async Task UploadFile(string path)
        {
            var fileStreamSource = new FileStream(path, FileMode.Open);
            var fileName = Path.GetFileName(fileStreamSource.Name);
            Console.WriteLine("Uploading File: {0}", fileName);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            await _deviceClient.UploadToBlobAsync(fileName, fileStreamSource);
            watch.Stop();

            Console.WriteLine("Time to upload file: {0}ms\n", watch.ElapsedMilliseconds);
        }
    }
}
