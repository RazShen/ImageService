using ImageServiceTools.Controller;
using ImageServiceTools.Modal;
using Newtonsoft.Json;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
/// /
/// </summary>
namespace ImageServiceTools.ServiceCommunication
	{
	/// <summary>
	/// Client handler handles all the client in the client list
	/// </summary>
    class ClientHandler : IClientHandler
    {
		private bool isStopped;
		IImageController controller;
		public static Mutex GlobMutex { get; set; }

		/// <summary>
		/// Constructr for the client handler, from the controller so it can invoke its commands
		/// </summary>
		/// <param name="imageController"></param>
		public ClientHandler(IImageController imageController)
        {
            this.controller = imageController;
            isStopped = false;
        }

		/// <summary>
		/// listen to client constantly, once receiving input from clients, invoke the input as a command.
		/// 
		/// </summary>
		/// <param name="client"></param>
		/// <param name="clients"></param>
        public void HandleClient(TcpClient client, List<TcpClient> clients)
        {
            new Task(() =>
            {
				try
					{
					while (!isStopped)
						{
						NetworkStream stream = client.GetStream();
						BinaryReader reader = new BinaryReader(stream);
						BinaryWriter writer = new BinaryWriter(stream);
						string desrializedCommands = reader.ReadString();
						CommandRecievedEventArgs commandRecievedEventArgs = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(desrializedCommands);
						if (commandRecievedEventArgs.CommandID == (int)CommandEnum.CloseClient)
						// if the command is remove client command
							{
							clients.Remove(client);
							client.Close();
							break;
							}
						bool resultCommand;
						// if the command isn't remove client
						string result = this.controller.ExecuteCommand((int)commandRecievedEventArgs.CommandID,
							commandRecievedEventArgs.Args, out resultCommand);
						GlobMutex.WaitOne();
						// write result to the client who sent the message
						writer.Write(result);
						GlobMutex.ReleaseMutex();
						}
					} catch (Exception ex)
					{
					//if an error accurs
						clients.Remove(client);
						client.Close();
					}
			}).Start();
        }
    }
}
