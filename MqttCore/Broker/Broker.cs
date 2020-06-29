using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using MQTTCore.Device;
using MQTTCore.Client;

namespace MQTTCore.Broker
{
    public class Broker
    {
        private Socket _listenSoket;
        private Socket _sendSocket;

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

        public int Devices
        {
            get;
            set;
        }

        public Broker(string name, string ip, int port)
        {
            Name = name;
            IP = ip;
            Port = port;
        }

        public async Task SendDataAsync(CancellationToken cancellationToken)
        {
            List<IClient> clients = await GetClientsAsync(cancellationToken);
            foreach (var c in clients)
            {
            }
        }

        private async Task<List<Publisher>> GetDevicesAsync(CancellationToken cancellationToken)
        {
            List<Publisher> publishers = new List<Publisher>();
            publishers.Add(new Publisher("p1", "localhost", 8000));
            return publishers;
        }

        private async Task<List<IClient>> GetClientsAsync(CancellationToken cancellationToken)
        {
            List<IClient> clients = new List<IClient>();
            clients.Add(new Subscriber("s2", "localhost", 8000));
            return clients;
        }

        public async Task<Dictionary<Publisher, string>> ListeningAsync(CancellationToken cancellationToken)
        {
            Dictionary<Publisher, string> messages = new Dictionary<Publisher, string>();
            List<Publisher> publishers = await GetDevicesAsync(cancellationToken);

            foreach (var p in publishers)
            {
                messages.Add(p, await ListenToPublisher(p, cancellationToken));
            }

            return messages;
        }

        private async Task<string> ListenToPublisher(Publisher publisher, CancellationToken cancellationToken)
        {
            MqttCore.Core.Tcp tcp = new MqttCore.Core.Tcp(publisher.IP, publisher.Port);
            tcp.StartListening();
            return await tcp.RecieveAsync(cancellationToken);
        }

        public void Dispose()
        {
        }
    }
}