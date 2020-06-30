using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttCore.Core
{
    public class LookUpQueue<T>
    {
        private Lookup<string, Queue<T>> PublishersLookUp
        {
            get;
            set;
        }

        public LookUpQueue()
        {

        }

        public void AddToQueue()
        {

        }

    }
}