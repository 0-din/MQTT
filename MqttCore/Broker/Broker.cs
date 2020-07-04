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
using MqttCore.Core;

namespace MQTTCore.Broker
{
    public class Broker
    {
        public DevicesQueue PublishersQ
        {
            get;
            set;
        }

        public SubscribersQueue SubscribersQ
        {
            get;
            set;
        }

        public Broker()
        {
        }

        public async Task SendAsync(CancellationToken cancellationToken)
        {
        }

        public async Task<string> RecieveAsync(Publisher publisher, CancellationToken cancellationToken)
        {
            return null;
        }

        public void CreatePublishersQueue(params Publisher[] publishers)
        {
            for (int i = 0; i < publishers.Length; i++)
                PublishersQ.Enqueue(publishers[i]);
        }

        public void CreateSubscribersQueue(params Subscriber[] subscribers)
        {
            for (int i = 0; i < subscribers.Length; i++)
                SubscribersQ.Dequeue(subscribers[i]);
        }

        private void AddSubscriberToQueue(Subscriber subscriber)
        {

        }
    }
}