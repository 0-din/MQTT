using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MQTTCore.Client
{
    public class Subscriber
    {
        private TcpClient _client;

        public string Name
        {
            get;
            set;
        }

        public string Category
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

        public Subscriber(string name, string ip, int port, string category)
        {
            Name = name;
            IP = ip;
            Port = port;
            Category = category;
        }

        public async Task Connect(CancellationToken cancellationToken)
        {
            await _client.ConnectAsync(IP, Port);
        }

        public async Task SendAsync(string message, CancellationToken cancellationToken)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            NetworkStream stream = _client.GetStream();
            await stream.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
        }

        public static bool operator ==(Subscriber subscriber1, Subscriber subscriber2)
        {
            return (subscriber1.IP == subscriber2.IP);
        }

        public static bool operator !=(Subscriber subscriber1, Subscriber subscriber2)
        {
            return !(subscriber1.IP == subscriber2.IP);
        }
    }
}