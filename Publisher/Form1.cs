using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MQTTCore.Device;

namespace Publisher
{
    public partial class Form1 : Form
    {
        const string publisherName = "publisher1";
        MQTTCore.Device.Publisher publisher;

        public Form1()
        {
            InitializeComponent();

            publisher = new MQTTCore.Device.Publisher(
                                                publisherName,
                                                ConfigurationManager.AppSettings["ip"],
                                                int.Parse(ConfigurationManager.AppSettings["port"]));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
        }
    }
}