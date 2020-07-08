using Stimulsoft.Base.Json.Linq;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttCore.Report.Type
{
    public class Temprature : IData
    {
        public string Type { get; set; }

        public string Name { get; set; }

        public string IP { get; set; }

        public DateTime Time { get; set; }

        public object Value { get; set; }

        public Temprature(Newtonsoft.Json.Linq.JObject json)
        {
            this.Type = json.Children().Where(p => p.Path.Equals("type")).FirstOrDefault().First().ToString();
            this.Name = json.Children().Where(p => p.Path.Equals("name")).FirstOrDefault().First().ToString();
            this.IP = json.Children().Where(p => p.Path.Equals("ip")).FirstOrDefault().First().ToString();
            this.Time = DateTime.Parse( json.Children().Where(p => p.Path.Equals("time")).FirstOrDefault().First().ToString());
            this.Value = json.Children().Where(p => p.Path.Equals("value")).FirstOrDefault().First().ToString();
        }

        public Temprature(string name , string ip, DateTime time, string value)
        {
            Name = name;
            IP = ip;
            Time = time;
            Value = value;
        }
    }
}
