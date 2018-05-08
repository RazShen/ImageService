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
	class ClientGUI : IClientGUI
		{
		private bool _running;
		private TcpClient _client;
		private static ClientGUI _instance;
		public delegate void Updator(CommandRecievedEventArgs responseObj);
		public event ImageServiceGUI.Client.Updator UpdateEvent;
		private static Mutex _mutex = new Mutex();

		private ClientGUI()
			{
			bool running = this.Start();
			this._running = running;
			}

		public bool Running()
			{
			return this._running;
			}


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

		public void Close()
			{
			_client.Close();
			this._running = false;
			}

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

		public void UpdateConstantly()
			{
			new Task(() =>
			{
				while (this._running)
					{
					NetworkStream stream = _client.GetStream();
					BinaryReader reader = new BinaryReader(stream);
					string serializedResponse = reader.ReadString();
					CommandRecievedEventArgs deserializedResponse = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(serializedResponse);
					this.UpdateEvent?.Invoke(deserializedResponse);
					}
			}).Start();
			}

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
						Console.WriteLine(e.ToString());
					}
			}).Start();
			}
		}
	}
