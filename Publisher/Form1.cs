using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
            txtIP.Text = "localhost";
            txtPort.Text = "7777";
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Task t = Connect(txtIP.Text);
            t.Wait();
        }

        private async Task Connect(String server)
        {
            try
            {
                Int32 port = int.Parse(txtPort.Text);
                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();

                using (StreamReader sr = new StreamReader(@"json1.json"))
                {
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(sr.ReadToEnd());
                    await stream.WriteAsync(data, 0, data.Length);
                }

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}