using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ScpiDemo
{
    public class ScpiClient : IDisposable
    {
        private readonly TcpClient client;
        private MemoryStream stream;

        public ScpiClient(TcpClient client)
        {
            this.client = client;
        }

        public string Ping()
        {
            var response = Send(Commands.Ping);

            return response;
        }

        public string GetVoltage()
        {
            var command = Send(Commands.GetVoltage);

            return Send(command);
        }

        private string Send(string request)
        {
            var data = Encoding.ASCII.GetBytes(request);

            using (var stream = client.GetStream())
            {
                stream.Write(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                var response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                return response;
            }
        }

        private string Send2(string request)
        {
            using (var stream = client.GetStream())
            {
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                writer.WriteLine(request);
                writer.Flush();

                var response = reader.ReadLine();

                reader.Dispose();
                writer.Dispose();


                return response;
            }
        }

        public void Dispose()
        {
            client.Close();
        }
    }
}
