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
    class ClientHandler : IClientHandler
    {
		private bool isStopped;
		IImageController controller;
		public static Mutex GlobMutex { get; set; }

		public ClientHandler(IImageController imageController)
        {
            this.controller = imageController;
            isStopped = false;
        }
        public void HandleClient(TcpClient client, List<TcpClient> clients)
        {
            new Task(() =>
            {
				while (!isStopped)
					{
					NetworkStream stream = client.GetStream();
					BinaryReader reader = new BinaryReader(stream);
					BinaryWriter writer = new BinaryWriter(stream);
					string desrializedCommands = reader.ReadString();
					CommandRecievedEventArgs commandRecievedEventArgs = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(desrializedCommands);
                    if (commandRecievedEventArgs.CommandID == (int)CommandEnum.CloseClient)
                    {
                        clients.Remove(client);
                        client.Close();
                        break;

                    }
                    bool resultCommand;
					string result = this.controller.ExecuteCommand((int)commandRecievedEventArgs.CommandID,
						commandRecievedEventArgs.Args, out resultCommand);
					GlobMutex.WaitOne();
					writer.Write(result);
					GlobMutex.ReleaseMutex();
					}
			}).Start();
        }
    }
}
