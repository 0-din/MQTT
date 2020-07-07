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
                return $"{Publisher.Name}_{DateTime.Now.ToString().Replace("/", "").Replace(":", "")}.txt";
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
            ValidatePath();
        }

        public void ValidatePath()
        {
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
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

        public static FileInfo[] GetLogs(DateTime from, DateTime to, string path)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (string file in Directory.GetFiles(path))
            {
                if (File.Exists(file))
                {
                    using (StreamReader reader = new StreamReader(file))
                        try
                        {
                            string content = reader.ReadToEnd();
                            result.Add("", content);
                        }
                        catch (Exception ex)
                        {
                            reader.Dispose();
                        }
                }
            }

            return null;
        }

    }
}