using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;

namespace MqttCore.Core
{
    public class Tcp
    {
        public IPAddress IPAddress
        {
            get;
            set;
        }

        public TcpListener Server
        {
            get;
            set;
        }

        public Tcp(string ip, int port)
        {
            IPAddress = Dns.GetHostEntry(ip).AddressList[0];
            Server = new TcpListener(IPAddress, port);
        }

        public void StartListening()
        {
            try
            {
                Server.Start();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> RecieveAsync(CancellationToken cancellationToken)
        {
            TcpClient tcpc = default(TcpClient);

            tcpc = await Server.AcceptTcpClientAsync();

            byte[] recievedBuffer = new byte[1024];
            
            NetworkStream stream = tcpc.GetStream();
            stream.Read(recievedBuffer, 0, recievedBuffer.Length);
            string msg = Encoding.ASCII.GetString(recievedBuffer);

            return msg;
        }
    }
}
