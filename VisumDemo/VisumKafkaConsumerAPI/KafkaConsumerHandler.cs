using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using VisumData;

namespace VisumKafkaConsumerAPI
{
    public class KafkaConsumerHandler : IHostedService
    {
        private readonly string topic = "simpletalk_topic";
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "st_consumer_group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using (var builder = new ConsumerBuilder<Ignore,
                string>(conf).Build())
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


        public async Task SendToDB(string jsonString)
        {
            using (var httpClient  = new HttpClient())
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
