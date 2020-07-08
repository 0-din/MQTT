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
            get => $"{Publisher.Name}_{DateTime.Now.ToString().Replace("/","-").Replace(" ", "_")}.txt";
        }

        private DateTime LogTime
        {
            get => DateTime.Now;
        }

        public Log(string path, Publisher publisher, string message)
        {
            Publisher = publisher;
            Message = message;
            this.Path = System.IO.Path.Combine(path, FileName);
            ValidatePath();
        }

        public Log()
        {
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

        public static IEnumerable<string> GetLogs(DateTime from, DateTime to, string path)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            string[] files = Directory.GetFiles(path)
                                        .Where(x => File.Exists(x))
                                        .Where(x => File.GetCreationTime(x) >= from && File.GetCreationTime(x) <= to).ToArray();

            foreach (string file in files)
                using (StreamReader reader = new StreamReader(file))
                    yield return reader.ReadToEnd();
        }

        public static async void LogError(string message, string path)
        {
            Log lg = new Log();
            lg.Path = System.IO.Path.Combine(path, $"Error_{DateTime.Now.ToString().Replace("/", "-").Replace(" ", "_")}.txt");
            lg.Message = message;
            await lg.SaveAsync();
        }
    }
}