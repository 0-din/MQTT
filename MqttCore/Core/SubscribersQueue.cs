using MQTTCore.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttCore.Core
{
    public class SubscribersQueue
    {
        private static Dictionary<string, Queue<Subscriber>> _queue;

        public Dictionary<string, Queue<Subscriber>> Queue
        {
            get
            {
                if (_queue == null)
                    _queue = new Dictionary<string, Queue<Subscriber>>();
                return _queue;
            }
            private set
            {
                _queue = value;
            }
        }

        public Queue<Subscriber> this[string category]
        {
            get
            {
                return this.Queue[category];
            }
            private set
            {
                this.Queue[category] = value;
            }
        }

        public SubscribersQueue()
        {
        }

        public void Enqueue(Subscriber subscriber)
        {
            if (!this.Queue.ContainsKey(subscriber.Category) || this[subscriber.Category] == null)
                this[subscriber.Category] = new Queue<Subscriber>();
            this[subscriber.Category].Enqueue(subscriber);
        }

        public Subscriber Dequeue(Subscriber subscriber, string category)
        {
            return this.Queue[category].Dequeue();
        }

        public Subscriber Peek(Subscriber subscriber, string category)
        {
            return this.Queue[category].Peek();
        }
    }
}