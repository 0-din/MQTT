using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MqttCore.Core
{
    public class DevicesQueue
    {
        private static Queue<Publisher> _devices;

        public DevicesQueue()
        {
            _devices = new Queue<Publisher>();
        }

        public void Enqueue(Publisher publisher)
        {
            _devices.Enqueue(publisher);
        }

        public Publisher Dequeue()
        {
            return _devices.Dequeue();
        }

    }
}
