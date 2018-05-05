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

namespace ImageServiceGUI.Client
	{
	class ClientGUI: IClientGUI
		{
		private bool _running;
		private TcpClient _client;



		public bool Running()
			{
			return this._running;
			}

		public void Close()
			{
			_client.Close();
			this._running = false;
			}

		public bool Start()
			{
			try { 
				bool result = true;
				IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ConnectingData.ip), ConnectingData.port);
				_client = new TcpClient();
				_client.Connect(endPoint);
				// connected to server now

				_running = true;
				return result;
				}
			catch (Exception ex)
				{
				Console.WriteLine(ex.ToString());
				return false;

				}
			}

		public void UpdateConstantly()
			{
			new Task(() =>
			{
				while (this._running)
					{
					using (NetworkStream stream = _client.GetStream())
					using (BinaryReader reader = new BinaryReader(stream))
					using (BinaryWriter writer = new BinaryWriter(stream))
						{
						string response = reader.ReadString();
						Console.WriteLine($"Recieve {response} from Server");
						CommandRecievedEventArgs responseObj = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(response);
						// Deal with the update
						// need sleep?
						}
					}
			}).Start();
			}

		public CommandRecievedEventArgs WriteCommandToServer(CommandRecievedEventArgs argsForCommand)
			{
			string convertedJSON = JsonConvert.SerializeObject(argsForCommand);
			using (NetworkStream stream = _client.GetStream())
			using (BinaryReader reader = new BinaryReader(stream))
			using (BinaryWriter writer = new BinaryWriter(stream))
				{
				// Write JSON to server
				writer.Write(convertedJSON);
				// Read JSON from server (response)
				string result = reader.ReadString();
				// Return the Deserialized respose.
				return JsonConvert.DeserializeObject<CommandRecievedEventArgs>(result);
				}
			}
		}
	}
