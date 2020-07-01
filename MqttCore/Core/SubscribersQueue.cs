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
        Dictionary<string, Queue<Subscriber>> _queue;

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

        public void Enqueue(Subscriber subscriber, string category)
        {
            if (this[category] == null)
                this[category] = new Queue<Subscriber>();

            this[category].Enqueue(subscriber);
        }

        public Subscriber Dequeue(Subscriber Dequeue, string category)
        {
            return this.Queue[category].Dequeue();
        }

        public Subscriber Peek(Subscriber Dequeue, string category)
        {
            return this.Queue[category].Peek();
        }
    }
}