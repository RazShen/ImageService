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

namespace ImageServiceGUI.Client
	{
	/// <summary>
	/// Client of the GUI class (which handlers all the gui request and receive to/from the server)
	/// </summary>
	class ClientGUI : IClientGUI
		{
		private bool _running;
		private TcpClient _client;
		private static ClientGUI _instance;
		public delegate void Updator(CommandRecievedEventArgs responseObj);
		public event ImageServiceGUI.Client.Updator UpdateEvent;
		private static Mutex _mutex = new Mutex();
		/// <summary>
		/// Client GUI (singleton)
		/// </summary>
		private ClientGUI()
			{
			bool running = this.Start();
			this._running = running;
			}

		/// <summary>
		/// Boolean indicating if the client is connected.
		/// </summary>
		/// <returns></returns>
		public bool Running()
			{
			return this._running;
			}

		/// <summary>
		/// Get an instance of the client (singelton)
		/// </summary>
		public static IClientGUI Instance
			{
			get
				{
				if (_instance != null)
					{
					return _instance;
					}
				_instance = new ClientGUI();
				return _instance;
				}
			}

		/// <summary>
		/// Close the connection with the server (sends close command)
		/// </summary>
		public void Close()
			{
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseClient, null, "");
            WriteCommandToServer(commandRecievedEventArgs);
            _client.Close();
			this._running = false;
			}

		/// <summary>
		/// Start connection with the server.
		/// </summary>
		/// <returns> connected/not connected</returns>
		public bool Start()
			{
			try
				{
				bool result = true;
				IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ConnectingData.ip), ConnectingData.port);
				_client = new TcpClient();
				_client.Connect(endPoint);
				// connected to server now
				_running = true;
				return result;
				}
			catch (Exception e)
				{
				Console.WriteLine(e.ToString());
				return false;

				}
			}

		/// <summary>
		/// This method constantly receiving data from the server, once the client is connected to the server (running)
		/// the client receives command args and invoke its event (updateEvent) where delegates from settings model
		/// and log model are signed to.
		/// </summary>
		public void UpdateConstantly()
			{
			new Task(() =>
			{
				try
					{
					while (this._running)
						{
						NetworkStream stream = _client.GetStream();
						BinaryReader reader = new BinaryReader(stream);
						string serializedResponse = reader.ReadString();
						CommandRecievedEventArgs deserializedResponse = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(serializedResponse);
						if (deserializedResponse.Args != null)
							{
							if (deserializedResponse.Args[0].Equals("True") || deserializedResponse.Args[0].Equals("False"))
								{
								continue;
								}
							}
						this.UpdateEvent?.Invoke(deserializedResponse);
						}
					} catch (Exception e)
					{
					Console.WriteLine(e.ToString());
					String[] failArgs = new string[2];
					failArgs[1] = MessageTypeEnum.ERROR.ToString();
					failArgs[0] = "Can't receive messages from server";
					CommandRecievedEventArgs failureArgs = new CommandRecievedEventArgs((int)CommandEnum.NewLogMessage, failArgs, "");
					this.UpdateEvent?.Invoke(failureArgs);
					};
			}).Start();
			}
		/// <summary>
		/// this method is used for the view model (to set background to gray)
		/// </summary>
		/// <returns></returns>
        public String RunningToString()
        {
            if (this.Running()) 
            {
                return "True";
            }
            return "False";
        }

		/// <summary>
		/// Write command to server method (writes a serialized command to the server)
		/// </summary>
		/// <param name="args"></param>
        public void WriteCommandToServer(CommandRecievedEventArgs args)
			{
			new Task(() =>
			{
				try
					{
					string jsonCommand = JsonConvert.SerializeObject(args);
					NetworkStream stream = _client.GetStream();
					BinaryWriter writer = new BinaryWriter(stream);
					_mutex.WaitOne();
					writer.Write(jsonCommand);
					_mutex.ReleaseMutex();
					}
				catch (Exception e)
					{
					// If failed writing, add a fail log to this client.
						Console.WriteLine(e.ToString());
						String[] failArgs = new string[2];
						failArgs[1] = MessageTypeEnum.ERROR.ToString();
						failArgs[0] = "Can't write message to server";
						CommandRecievedEventArgs failureArgs = new CommandRecievedEventArgs((int)CommandEnum.NewLogMessage, failArgs, "");
						this.UpdateEvent?.Invoke(failureArgs);
						}
			}).Start();
			}
		}
	}
