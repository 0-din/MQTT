using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTCore.Client
{
    interface ISubscriber
    {
        Task SubscribeAsync(string category, string brokerName, string brokerIp);
    }
}