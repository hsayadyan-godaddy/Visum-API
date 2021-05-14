using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace VisumDAS
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await RunTaskInterval();

            Console.ReadLine();
        }

        private static async Task RunTaskInterval(CancellationToken cancellationToken = default)
        {
            while (true)
            {
                await CreateMessageAndSend();
                await Task.Delay(1000, cancellationToken);
                if (cancellationToken.IsCancellationRequested)
                    break;
            };
        }

        public static WellData CreateRandomPT()
        {
            Random rnd = new Random();
            double T = rnd.Next(6000000, 10000000) / 100000.00000;
            double P = rnd.Next(500000, 900000) / 100000.00000;

            Console.WriteLine($"T : { T} - P : { P}");
           
            return new WellData
            {
                Temperature = T,
                Pressure = P
            };
        }

        
        public static async Task CreateMessageAndSend()
        {
            var data = CreateRandomPT();
            var jsonData = JsonConvert.SerializeObject(data);
            var prod = new ProducerWrapper("demoTopic");
            await prod.WriteMessage(jsonData);
        }
    }
}
