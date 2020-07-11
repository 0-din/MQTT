using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MqttCore.Report;
using Stimulsoft.Report;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MqttCore.Core;
using MqttCore.Report.Type;
using System.IO;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using Stimulsoft.Report.Dashboard;

namespace Broker
{
    public partial class BrokerWindow : Form
    {
        const string brokerName = "broker";
        MQTTCore.Broker.Broker broker;
        readonly string logPath = ConfigurationManager.AppSettings["logPath"];

        public BrokerWindow()
        {
            InitializeComponent();

            broker = new MQTTCore.Broker.Broker();
            broker.LogPath = logPath;

            List<MQTTCore.Device.Publisher> publishers = new List<MQTTCore.Device.Publisher>();
            foreach (string p in ConfigurationManager.AppSettings["Publishers"].ToString().Split(';'))
            {
                string name = p.Split(':')[0];
                string ip = p.Split(':')[1];
                int port = int.Parse(p.Split(':')[2]);
                publishers.Add(new MQTTCore.Device.Publisher(name, ip, port));
            }
            broker.CreatePublishersQueue(publishers.ToArray());


            //MQTTCore.Device.Publisher[] publishers =
            //    ConfigurationManager.AppSettings["Publishers"].ToString().Split(';').Select(p => new MQTTCore.Device.Publisher(p.Split(':')[0], p.Split(':')[1], int.Parse(p.Split(':')[2]))).ToArray();
            //MQTTCore.Client.Subscriber[] subscribers =
            //    ConfigurationManager.AppSettings["Subscribers"].ToString().Split(';')
            //    .Select(p => new MQTTCore.Client.Subscriber(p.Split(':')[0],
            //                                                p.Split(':')[1],
            //                                                int.Parse(p.Split(':')[2]),
            //                                                p.Split(':')[3])).ToArray();
            //broker.CreateSubscribersQueue(subscribers);
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            CancellationTokenSource source = new CancellationTokenSource();

            if (btnStart.Tag == null || int.Parse(btnStart.Tag.ToString()) == 0)
            {
                btnStart.Tag = 1;   
                btnStart.Text = "Recieve cData";
                txtMessage.Text = "Start recieving data..." + Environment.NewLine;
                await Run(source.Token);
            }
            else
            {
                btnStart.Tag = 0;
                btnStart.Text = "Stop recieve Data";
                txtMessage.Text = "Stop recieve Data." + Environment.NewLine;
                source.Cancel();
            }
        }

        private async Task Run(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await broker.RecieveDataAsync(cancellationToken);
                await Search(DateTime.Now.AddDays(-1), DateTime.Now);
            }
        }

        private void BrokerWindow_Load(object sender, EventArgs e)
        {
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Task t = Search(dtFrom.Value, dtTo.Value);
            t.Start();
        }

        private async Task Search(DateTime from, DateTime to)
        {
            Dictionary<string, object> businessObjects = new Dictionary<string, object>();
            List<Temprature> tmps = new List<Temprature>();

            foreach (string lg in MqttCore.Core.Log.GetLogs(from, to, logPath))
            {
                JObject obj = ParseJson(lg);
                if (obj != null)
                {
                    tmps.Add(new Temprature(obj));
                }
            }

            Task t = Task.Run(() =>
            {
                ShowOnChart<IData>(tmps);
            });
            await t;
        }

        private void ShowOnChart<T>(List<Temprature> data) where T : IData
        {
            List<ObservablePoint> values = new List<ObservablePoint>();

            foreach (var d in data)
            {
                values.Add(new ObservablePoint(double.Parse(d.Time.ToString()), double.Parse(d.Value.ToString())));
            }

            cartesianChart1.Series = new SeriesCollection()
            {
                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>(values)
                }
            };
        }

        private MemoryStream CreateChart(Dictionary<string, object> businessObjects)
        {
            using (StimulsoftGenerator report = new StimulsoftGenerator("Broker"))
            {
                report.LoadReport();
                report.Compile();
                report.AddBusinussObjects(businessObjects);
                report.Render();
                return report.ExportDocument(StiExportFormat.ImageJpeg);
            }
        }

        private JObject ParseJson(string json)
        {
            JObject jobj = null;
            try
            {
                jobj = JObject.Parse(json);
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message, logPath);
            }

            return jobj;
        }
    }
}