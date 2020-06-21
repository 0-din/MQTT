using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MQTTCore.Device
{
    public class Publisher
    {
        public string Name
        {
            get;
            set;
        }

        public string IP
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        public Publisher(string name, string ip, int port)
        {
            Name = name;
            IP = ip;
            Port = port;
        }

        public async Task PublishDataAsync(string message)
        {
            using (TcpClient client = new TcpClient(IP, Port))
                try
                {
                    byte[] sendBuffer = Encoding.ASCII.GetBytes(message);
                    int bytecount = sendBuffer.Length;

                    using (NetworkStream stream = client.GetStream())
                        try
                        {
                            await stream.WriteAsync(sendBuffer, 0, sendBuffer.Length);
                        }
                        catch (Exception ex)
                        {
                            stream.Dispose();
                        }
                }
                catch (Exception ex)
                {
                    client.Dispose();
                }
        }
    }
}
