using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTCore.Client
{
    public class Subscriber : IClient, ISubscriber
    {
        public string Name
        {
            get;
            set;
        }

        public string Ip
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        public Subscriber(string name, string ip, int port)
        {
            Name = name;
            Ip = ip;
            Port = port;
        }

        public async Task SubscribeAsync(string category, string brokerName, string brokerIp)
        {
            
        }
    }
}
