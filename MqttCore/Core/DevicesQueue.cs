using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using MqttCore;

namespace MqttCore.Core
{
    public class DevicesQueue
    {
        private static Queue<MQTTCore.Device.Publisher> _devices;

        public int Count
        {
            get 
            {
                return _devices.Count;
            }
        }

        public DevicesQueue()
        {
            _devices = new Queue<MQTTCore.Device.Publisher>();
        }

        public void Enqueue(MQTTCore.Device.Publisher publisher)
        {
            _devices.Enqueue(publisher);
        }

        public MQTTCore.Device.Publisher Dequeue()
        {
            return _devices.Dequeue();
        }

    }
}
