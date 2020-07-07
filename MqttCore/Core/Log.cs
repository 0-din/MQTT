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
    public class Log
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

        private string FileName
        {
            get
            {
                //c:\\brokerlog\\p1_7 / 7 / 2020 6:15:47 AM.txt
                return $"{Publisher.Name}_{DateTime.Now.ToString().Replace("/","").Replace(":", "")}.txt";
            }
        }

        private DateTime LogTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        public Log(string path, Publisher publisher, string message)
        {
            Publisher = publisher;
            Message = message;
            this.Path = System.IO.Path.Combine(path, FileName);
        }

        public async Task SaveAsync()
        {
            using (StreamWriter sw = new StreamWriter(Path))
                try
                {
                    await sw.WriteAsync(Message);
                }
                catch (Exception ex)
                {
                    throw;
                }
        }   
    }
}