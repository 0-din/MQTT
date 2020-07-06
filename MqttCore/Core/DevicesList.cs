using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using MqttCore;

namespace MqttCore.Core
{
    public class DevicesList
    {
        private List<MQTTCore.Device.Publisher> _devices;

        public MQTTCore.Device.Publisher this[int i]
        {
            get
            {
                return _devices[i];
            }
            private set
            {
                _devices.Add(value);
            }
        }

        public int Count
        {
            get
            {
                return _devices.Count;
            }
        }

        public DevicesList()
        {
            _devices = new List<MQTTCore.Device.Publisher>();
        }

        public void AddPublisher(MQTTCore.Device.Publisher publisher)
        {
            _devices.Add(publisher);
        }

        public MQTTCore.Device.Publisher GetPublisher(MQTTCore.Device.Publisher publisher)
        {
            return _devices.Where(x => x == publisher).FirstOrDefault();
        }
    }
}