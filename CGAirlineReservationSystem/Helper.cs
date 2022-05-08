using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using CGAirlineReservationSystem.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CGAirlineReservationSystem
{
    public class Helper
    {
        public static async Task<bool> UploadBlob(IConfiguration config, Reservation reservation)
        {
            string blobConnString = config.GetConnectionString("StorAccConnString");
            BlobServiceClient client = new BlobServiceClient(blobConnString);
            string container = config.GetValue<string>("Container");
            var containerClient = client.GetBlobContainerClient(container);

            string fileName = "cg.ars.reservation." + Guid.NewGuid().ToString() + ".json";
            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            //memorystream
            using (var stream = new MemoryStream())
            {
                var serializer = JsonSerializer.Create(new JsonSerializerSettings());

                // Use the 'leave open' option to keep the memory stream open after the stream writer is disposed
                using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true))
                {
                    // Serialize the job to the StreamWriter
                    serializer.Serialize(writer, reservation);
                }

                // Rewind the stream to the beginning
                stream.Position = 0;

                // Upload the job via the stream
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            await SendMessageToServiceBusQueue(config, reservation);
            return true;
        }

        private static async Task<bool> SendMessageToServiceBusQueue(IConfiguration config, Reservation reservation)
        {
            string connStr = config.GetConnectionString("ServiceBusConnString");
            string queuename = config.GetValue<String>("Queuename");
            ServiceBusClient client = new ServiceBusClient(connStr);
            ServiceBusSender sender = client.CreateSender(queuename);

            await sender.SendMessageAsync(new ServiceBusMessage(JsonConvert.SerializeObject(reservation)));
            return true;
        }
    }
}
