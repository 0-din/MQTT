using MqttCore.Core;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MQTTCore.Device
{
    public class Publisher
    {
        private MqttCore.Core.Tcp _tcp;

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

        public Publisher(string name, string ip, int port)
        {
            Name = name;
            IP = ip;
            Port = port;

            _tcp = new MqttCore.Core.Tcp(IP, Port);
        }

        public void Start()
        {
            _tcp.StartListening();
        }

        public async Task<string> ListenAsync(CancellationToken cancellationToken)
        {
            return await _tcp.RecieveAsync(cancellationToken);
        }
    }
}
