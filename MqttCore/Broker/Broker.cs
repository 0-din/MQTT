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

        public Queue<Subscriber> Subscribers
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

        public async Task SendAsync(CancellationToken cancellationToken)
        {
            Subscriber sub = Subscribers.Dequeue();
            await sub.Send("", cancellationToken);
        }

        public async Task<string> RecieveAsync(Publisher publisher, CancellationToken cancellationToken)
        {
            Publisher pub = Publishers.Dequeue();
            return await pub.ListenAsync(cancellationToken);
        }

        public void CreatePublishersQueue(params Publisher[] publishers)
        {
            foreach (Publisher pub in publishers)
                Publishers.Enqueue(pub);
        }

        public void CreateSubscribersQueue(params Subscriber[] subscribers)
        {
            foreach (Subscriber sub in subscribers)
                Subscribers.Enqueue(sub);
        }

        private void AddSubscriberToQueue(Subscriber subscriber)
        {

        }
    }
}