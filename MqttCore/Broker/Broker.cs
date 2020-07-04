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
        public DevicesQueue Publishers
        {
            get;
            set;
        }

        public SubscribersQueue Subscribers
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
        }

        public void CreateSubscribersQueue(params Subscriber[] subscribers)
        {
        }

        private void AddSubscriberToQueue(Subscriber subscriber)
        {

        }
    }
}