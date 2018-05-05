using ImageService.Logging;
using ImageService.Modal;
using Newtonsoft.Json;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ServiceCommunication
	{
	class ServerIS : IServerIS
		{
        private int port { get; set; }
        private TcpListener listener { get; set; }
        private IClientHandler ch { get; set; }
        ILoggingService Logging { get; set; }
        private List<TcpClient> clients;



        public ServerIS(int port, IClientHandler ch, ILoggingService loggingService)
        {
            this.port = port;
            this.ch = ch;
            clients = new List<TcpClient>();
            this.Logging = loggingService;
        }

        public void NotifyClients(CommandRecievedEventArgs commandRecievedEventArgs)
        {
            try
            {
                foreach (TcpClient client in clients)
                {
                    new Task(() =>
                    {
                        using (NetworkStream stream = client.GetStream())
                        using (StreamReader reader = new StreamReader(stream))
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            string result = JsonConvert.SerializeObject(commandRecievedEventArgs);
                            writer.Write(result);
                        }
                        client.Close();
                    }).Start();
                }
            } catch (Exception e) {
                Logging.Log(e.ToString(), SharedFiles.MessageTypeEnum.ERROR);
            }
        }

        public void Start()
        {
            try
            {
                IPEndPoint ep = new
                IPEndPoint(IPAddress.Parse(ConnectingData.ip), port);
                listener = new TcpListener(ep);

                listener.Start();
                Logging.Log("Waiting for connections...", SharedFiles.MessageTypeEnum.INFO);

                Task task = new Task(() =>
                {
                    while (true)
                    {
                        try
                        {
                            TcpClient client = listener.AcceptTcpClient();
                            clients.Add(client);
                            Logging.Log("Got new connection", SharedFiles.MessageTypeEnum.INFO);
                            ch.HandleClient(client);
                        }
                        catch (SocketException)
                        {
                            break;
                        }
                    }
                    Logging.Log("Server stopped", SharedFiles.MessageTypeEnum.INFO);

                });
                task.Start();
            } catch (Exception e)
            {
                Logging.Log(e.ToString(), SharedFiles.MessageTypeEnum.ERROR);
            }
        }
        public void Stop()
        {
            listener.Stop();
        }
    }


}

	
