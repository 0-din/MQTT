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
            PublishersQ = new DevicesQueue();
            SubscribersQ= new SubscribersQueue();
        }

        public async void Start(CancellationToken cancellationToken)
        {
        }

        public async Task RecieveData(CancellationToken cancellationToken)
        {
            for (int i = 0; i < PublishersQ.Count; i++)
            {
                Publisher publisher = PublishersQ.Dequeue();

                string message = await publisher.RecieveAsync(cancellationToken);


                PublishersQ.Enqueue(publisher);
            }
        }

        public void CreatePublishersQueue(params Publisher[] publishers)
        {
            for (int i = 0; i < publishers.Length; i++)
                PublishersQ.Enqueue(publishers[i]);
        }

        public void CreateSubscribersQueue(params Subscriber[] subscribers)
        {
            for (int i = 0; i < subscribers.Length; i++)
                SubscribersQ.Enqueue(subscribers[i]);
        }
    }
}