using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using Stimulsoft.Report.Dictionary;

namespace MqttCore.Core
{
    public class Tcp : IDisposable
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

        public TcpClient Client
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        public string IP
        {
            get;
            set;
        }

        public Tcp(string ip, int port)
        {
            Port = port;
            IP = ip;
            IPAddress = Dns.GetHostEntry(ip).AddressList[0];
        }

        public void BuildConnectionToRecieve()
        {
            Server = new TcpListener(IPAddress, Port);
        }

        public void BuildConnectionToSend()
        {
            Client = new TcpClient(IP, Port);
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
            
            await stream.ReadAsync(recievedBuffer, 0, recievedBuffer.Length);
            
            string msg = Encoding.ASCII.GetString(recievedBuffer);

            return msg;
        }

        public async Task SendAsync(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);

            using (NetworkStream stream = Client.GetStream())
                try
                {
                    await stream.WriteAsync(buffer, 0, buffer.Length);
                }
                catch
                {
                    stream.Close();
                    throw;
                }
        }

        public void Dispose()
        {
            Client.Close();
        }
    }
}
