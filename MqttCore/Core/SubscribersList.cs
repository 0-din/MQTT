using MQTTCore.Client;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttCore.Core
{
    public class SubscribersList
    {
        private static Dictionary<string, List<Subscriber>> _queue;

        public Dictionary<string, List<Subscriber>> List
        {
            get
            {
                if (_queue == null)
                    _queue = new Dictionary<string, List<Subscriber>>();
                return _queue;
            }
            private set
            {
                _queue = value;
            }
        }

        public List<Subscriber> this[string category]
        {
            get
            {
                return this.List[category];
            }
            private set
            {
                this.List[category] = value;
            }
        }

        public SubscribersList()
        {
        }

        public void AddSubscriber(Subscriber subscriber)
        {
            if (!this.List.ContainsKey(subscriber.Category) || this[subscriber.Category] == null)
                this[subscriber.Category] = new List<Subscriber>();
            this[subscriber.Category].Add(subscriber);
        }

        public Subscriber GetSubscriber(Subscriber subscriber)
        {
            if (!this.List.ContainsKey(subscriber.Category) || this[subscriber.Category] == null)
                throw new Exception("Unkown list.");

            return this.List.Where(x => x.Key == subscriber.Category)
                            .FirstOrDefault().Value
                            .Where(y => y.Name == subscriber.Name)
                            .FirstOrDefault();
        }
    }
}