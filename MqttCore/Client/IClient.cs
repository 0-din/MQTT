using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTCore.Client
{
    interface IClient
    {
        string Name { get; set; }

        string Ip { get; set; }

        int Port { get; set; }
    }
}