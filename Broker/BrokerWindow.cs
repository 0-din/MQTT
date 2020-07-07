using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MqttCore.Report;
using MQTTCore.Broker;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Components.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            while (true)
            {
                CancellationToken cancellationToken = new CancellationToken();
                await broker.RecieveDataAsync(cancellationToken);
            }
        }

        private void BrokerWindow_Load(object sender, EventArgs e)
        {
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Dictionary<string, float> data = new Dictionary<string, float>();

            foreach (string lg in MqttCore.Core.Log.GetLogs(dtFrom.Value, dtTo.Value, logPath))
            {
                JObject obj = ParseJson(lg);
                if (obj != null)
                {
                    
                }
            }
        }

        private static JObject ParseJson(string json)
        {
            JObject jobj = null;
            try
            {
                jobj = JObject.Parse(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return jobj;
        }

        private void GetReport()
        {
            using (StimulsoftGenerator report = new StimulsoftGenerator("ReportAccounting"))
            {
                report.LoadReport();
                //report.AddBusinussObjects(StiOfficialInvoiceObjects());
                report.Compile();
                //report.AddVariables(AddVariables());
                report.Render();
                report.ExportDocument(StiExportFormat.Pdf);
                //PDFHelper.WritePdfOnPage(report.ExportDocument(StiExportFormat.Pdf), HttpContext.Current.Response, fileName: $"financial-{_view.InvoiceNumber}", contentDisposition: "attachment");
            }
        }
    }
}