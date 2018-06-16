using ImageServiceTools.Logging;
using ImageServiceTools.ServiceCommunication;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ServiceCommunication
	{
	class TransferImagesServer: IServerIS
		{
		/// <summary>
		/// this function starts the server
		/// </summary>
		/// 
		ILoggingService Logging { get; set; }
		private int Port { get; set; }
		TcpListener Listener { get; set; }
		IClientHandler Ch { get; set; }
		private List<TcpClient> clients = new List<TcpClient>();

		public TransferImagesServer(int port, ILoggingService logging, IClientHandler ch)
			{
			this.Port = port;
			this.Logging = logging;
			this.Ch = ch;
			}
		/// <summary>
		/// This function starts listening to tcp connections
		/// </summary>
		public void Start()
			{
			try
				{
				IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Port);
				Listener = new TcpListener(ep);

				Listener.Start();
				Logging.Log("Image Transfer Server Waiting for client connections...", MessageTypeEnum.INFO);
				Task task = new Task(() =>
				{
					while (true)
						{
						try
							{
							TcpClient client = Listener.AcceptTcpClient();
							Logging.Log("Got new connection", MessageTypeEnum.INFO);
							Ch.HandleClient(client, clients);
							}
						catch (Exception ex)
							{
							break;
							}
						}
					Logging.Log("Server stopped", MessageTypeEnum.INFO);
				});
				task.Start();
				}
			catch (Exception ex)
				{
				Logging.Log(ex.ToString(), MessageTypeEnum.ERROR);
				}
			}
		
		/// <summary>
		/// this function stops the server
		/// </summary>
		public void Stop()
			{

			}
		/// <summary>
		/// This function notify all cliets about an update
		/// </summary>
		/// <param name="commandRecievedEventArgs"></param>
		public void NotifyClients(CommandRecievedEventArgs commandRecievedEventArgs)
			{

			}

		}
	}
