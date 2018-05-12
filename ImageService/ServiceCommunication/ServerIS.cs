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
    /// <summary>
    /// server for image service class
    /// </summary>
	class ServerIS : IServerIS
		{
        private int port { get; set; }
        private TcpListener listener { get; set; }
        private IClientHandler clientHandler { get; set; }
        ILoggingService Logging { get; set; }
        private List<TcpClient> _currClients;
		private static Mutex _mutex;

        /// <summary>
        /// Constractor for Server for Image service
        /// </summary>
        /// <param name="port"></param>
        /// <param name="ch"></param>
        /// <param name="loggingService"></param>
        public ServerIS(int port, IClientHandler ch, ILoggingService loggingService)
        {
            this.port = port;
            this.clientHandler = ch;
            _currClients = new List<TcpClient>();
            this.Logging = loggingService;
			_mutex = new Mutex();
			ClientHandler.GlobMutex = _mutex;
        }
        /// <summary>
        /// this fucntion notify all clients about an update 
        /// </summary>
        /// <param name="commandRecievedEventArgs"></param>
        public void NotifyClients(CommandRecievedEventArgs commandRecievedEventArgs)
        {
            try
            {
				List<TcpClient> tempClient = new List<TcpClient>(_currClients);
				foreach (TcpClient client in tempClient)
                {
                    new Task(() =>
                    {
						try
							{
							NetworkStream stream = client.GetStream();
							string jsonCommand = JsonConvert.SerializeObject(commandRecievedEventArgs);
							_mutex.WaitOne();
							new BinaryWriter(stream).Write(jsonCommand);
							_mutex.ReleaseMutex();
							}
						catch (Exception e) { _currClients.Remove(client); };
                    }).Start();
                }
            } catch (Exception e) {
                Logging.Log(e.ToString(), SharedFiles.MessageTypeEnum.ERROR);
            }
        }
        /// <summary>
        /// this function starts the server
        /// </summary>
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
                            clientHandler.HandleClient(client, _currClients);
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
        /// <summary>
        /// This function stops the server
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }
    }
}

	
