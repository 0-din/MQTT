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
        public Queue<Publisher> Publishers
        {
            get;
            set;
        }

        public Queue<Subscriber> Subscriber
        {
            get;
            set;
        }

        public Broker()
        {
        }

        public void Start()
        {
        }

        public async Task SendDataAsync(CancellationToken cancellationToken)
        {
        }

        private async Task<string> Recieve(Publisher publisher, CancellationToken cancellationToken)
        {
            return "";
        }

        private void CreatePublishersQueue(params Publisher[] publishers)
        {
            foreach (Publisher pub in publishers)
                Publishers.Enqueue(pub);
        }

        private void CreateSubscribersQueue(params Subscriber[] subscribers)
        {
            foreach (Subscriber sub in subscribers)
                Subscriber.Enqueue(sub);
        }

    }
}