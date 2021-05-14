using System;
using System.Net;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace VisumDAS
{
    public class ProducerWrapper
    {
        private readonly string _topiName;

        private static readonly Random rand = new Random();

        public ProducerWrapper(string topicName)
        {
            _topiName = topicName;
        }

        public async Task WriteMessage(string message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = Dns.GetHostName()
            };
            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {


                var dr = await producer.ProduceAsync(_topiName, new Message<string, string>
                {
                    Key = rand.Next(5).ToString(),
                    Value = message
                });
            }

        }

    }
}
