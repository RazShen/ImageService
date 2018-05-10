using ImageServiceTools.Logging;
using ImageServiceTools.Modal;
using Newtonsoft.Json;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageServiceTools.ServiceCommunication
	{
	class ServerIS : IServerIS
		{
        private int port { get; set; }
        private TcpListener listener { get; set; }
        private IClientHandler clientHandler { get; set; }
        ILoggingService Logging { get; set; }
        private List<TcpClient> _currClients;
		private static Mutex _mutex;


        public ServerIS(int port, IClientHandler ch, ILoggingService loggingService)
        {
            this.port = port;
            this.clientHandler = ch;
            _currClients = new List<TcpClient>();
            this.Logging = loggingService;
			_mutex = new Mutex();
			ClientHandler.GlobMutex = _mutex;
        }

        public void NotifyClients(CommandRecievedEventArgs commandRecievedEventArgs)
        {
            try
            {
				List<TcpClient> tempClient = new List<TcpClient>(_currClients);
				foreach (TcpClient client in tempClient)
                {
                    new Task(() =>
                    {
						NetworkStream stream = client.GetStream();
						StreamReader reader = new StreamReader(stream);
						StreamWriter writer = new StreamWriter(stream); 
						string result = JsonConvert.SerializeObject(commandRecievedEventArgs);
						_mutex.WaitOne();
						new BinaryWriter(client.GetStream()).Write(result);
						_mutex.ReleaseMutex();              
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
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ConnectingData.ip), port);
                listener = new TcpListener(endPoint);

                listener.Start();
                Logging.Log("Waiting for connections...", MessageTypeEnum.INFO);

                Task task = new Task(() =>
                {
                    while (true)
                    {
                        try
                        {
                            TcpClient client = listener.AcceptTcpClient();
                            _currClients.Add(client);
                            Logging.Log("Got new connection", MessageTypeEnum.INFO);
                            clientHandler.HandleClient(client, clients);
                        }
                        catch (SocketException)
                        {
                            break;
                        }
                    }
                    Logging.Log("There was a problem running the server main thread", MessageTypeEnum.INFO);
                });
                task.Start();
            } catch (Exception e)
            {
                Logging.Log(e.ToString(), MessageTypeEnum.ERROR);
            }
        }
        public void Stop()
        {
            listener.Stop();
        }
    }


}

	
