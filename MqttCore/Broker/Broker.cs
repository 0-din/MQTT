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
using System.Security.Cryptography.X509Certificates;
using Stimulsoft.Report.Gauge;

namespace MQTTCore.Broker
{
    public class Broker
    {
        public DevicesList Publishers
        {
            get;
            set;
        }

        public SubscribersList Subscribers
        {
            get;
            set;
        }

        public Broker()
        {
            Publishers = new DevicesList();
            Subscribers = new SubscribersList();
        }

        public async void Start(CancellationToken cancellationToken)
        {
        }

        public async Task RecieveData(CancellationToken cancellationToken)
        {
            for (int i = 0; i < Publishers.Count; i++)
            {
                Publisher publisher = Publishers[i];
                string message = await publisher.RecieveAsync(cancellationToken);

                var subs = Subscribers[publisher.Name];
            }
        }

        public void CreatePublishersQueue(params Publisher[] publishers)
        {
            for (int i = 0; i < publishers.Length; i++)
                Publishers.AddPublisher(publishers[i]);
        }

        public void CreateSubscribersQueue(params Subscriber[] subscribers)
        {
            for (int i = 0; i < subscribers.Length; i++)
                Subscribers.AddSubscriber(subscribers[i]);
        }
    }
}