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
using MQTTCore.Broker;

namespace Broker
{
    public partial class BrokerWindow : Form
    {
        const string brokerName = "broker";
        MQTTCore.Broker.Broker broker;
        CancellationToken cancellationToken = new CancellationToken();

        public BrokerWindow()
        {
            InitializeComponent();

            broker = new MQTTCore.Broker.Broker();

            broker.LogPath = ConfigurationManager.AppSettings["logPath"];

            MQTTCore.Device.Publisher[] publishers = 
                ConfigurationManager.AppSettings["Publishers"].ToString().Split(';').Select(p => new MQTTCore.Device.Publisher(p.Split(':')[0], p.Split(':')[1], int.Parse(p.Split(':')[2]))).ToArray();

            //MQTTCore.Client.Subscriber[] subscribers =
            //    ConfigurationManager.AppSettings["Subscribers"].ToString().Split(';')
            //    .Select(p => new MQTTCore.Client.Subscriber(p.Split(':')[0],
            //                                                p.Split(':')[1],
            //                                                int.Parse(p.Split(':')[2]),
            //                                                p.Split(':')[3])).ToArray();

            broker.CreatePublishersQueue(publishers);
            //broker.CreateSubscribersQueue(subscribers);
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            while (true)
            {
                await broker.RecieveDataAsync(cancellationToken);
            }
        }

        private void BrokerWindow_Load(object sender, EventArgs e)
        {
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string lgPath = ConfigurationManager.AppSettings["logPath"];

            if (!string.IsNullOrEmpty(lgPath))
            {
                MqttCore.Core.Log.GetLogs(dtFrom.Value, dtTo.Value, lgPath);

            }
        }
    }
}