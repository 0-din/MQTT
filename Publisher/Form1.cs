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
        }
            
        private void btnSend_Click(object sender, EventArgs e)
        {
            Connect("localhost", "this is me.");
        }

        static void Connect(String server, String message)
        {
            try
            {
                Int32 port = 7777;
                TcpClient client = new TcpClient(server, port);

                if (client.Connected)
                {
                }

                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);

                //Console.WriteLine("Sent: {0}", message);

                //data = new Byte[256];

                //String responseData = String.Empty;

                //Int32 bytes = stream.Read(data, 0, data.Length);
                //responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //Console.WriteLine("Received: {0}", responseData);

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