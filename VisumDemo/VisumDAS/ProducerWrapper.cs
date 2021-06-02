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

        public async Task<string> WriteMessage(string message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
                
            };
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var dr = await producer.ProduceAsync(_topiName, new Message<Null, string>
                    {
                        Value = message
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something went wrong: {ex}");
                    return ex.Message;
                }
                return message;
            }

        }

    }
}
