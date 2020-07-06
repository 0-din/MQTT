using MQTTCore.Device;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttCore.Core
{
    public class Log : IDisposable
    {
        public string Message
        {
            get;
            set;
        }

        public Publisher Publisher
        {
            get;
            set;
        }

        private string Path
        {
            get;
            set;
        }

        private DateTime LogTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        public Log(string path)
        {
            this.Path = System.IO.Path.Combine(path);
        }

        public async Task SaveAsync()
        {
            
        }

        public void Dispose()
        {
        }
    }
}
