using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ScpiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            PingTest();
        }

        private static void PingTest()
        {
            var hostname = ConfigurationManager.AppSettings["hostname"];
            var port = int.Parse(ConfigurationManager.AppSettings["port"]);

            Console.WriteLine($"Connecting to {hostname}:{port}");

            using (var client = new TcpClient(hostname, port))
            using (var scpiClient = new ScpiClient(client))
            {
                var response = scpiClient.Ping();

                Console.WriteLine($"Response: {response}");

                var voltage = scpiClient.GetVoltage();

                Console.WriteLine($"Voltage: {voltage}");
            }

        }
    }
}
