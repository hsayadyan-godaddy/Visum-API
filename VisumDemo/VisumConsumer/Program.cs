using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Newtonsoft.Json;
using VisumData;

namespace VisumConsumer
{
    class Program
    {
        public static async Task Main(string[] args)
        {

            try
            {
                Console.WriteLine("Start Listening...");
                await StartAsync(CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static readonly string topic = "simpletalk_topic";
        public static async Task StartAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "st_consumer_group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using (var builder = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                builder.Subscribe(topic);
                var cancelToken = new CancellationTokenSource();
                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var consumer = builder.Consume(cancelToken.Token);
                        var message = consumer.Message.Value;
                        // send to API and save into the DB Collection
                        Console.WriteLine($"Message: {message} received from {consumer.TopicPartitionOffset}");
                        //await SendToPort(message);
                        await SendToDB(message);
                    }
                }
                catch (Exception ex)
                {
                    builder.Close();
                }
            }
            await Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }


        public static async Task SendToPort(string json)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:5500");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                //string json = "{\"user\":\"test\"," +
                //              "\"password\":\"bla\"}";

                streamWriter.Write(json);
            }
        }

        public static async Task SendToDB(string jsonString)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:5000/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.PostAsJsonAsync("api/data", JsonConvert.DeserializeObject<WellData>(jsonString));
                if (response.IsSuccessStatusCode)
                {
                    //TODO
                }
                else
                {
                    //TODO
                }
            }
        }

    }
}
