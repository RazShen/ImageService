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
using System.Threading.Tasks;

namespace ImageServiceTools.ServiceCommunication
	{
    class ClientHandler : IClientHandler
    {
		private bool isStopped;
		IImageController controller;
        public ClientHandler(IImageController imageController)
        {
            this.controller = imageController;
            isStopped = false;
        }
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
				while (!isStopped)
					{
					NetworkStream stream = client.GetStream();
					BinaryReader reader = new BinaryReader(stream);
					BinaryWriter writer = new BinaryWriter(stream);
					string commandLine = reader.ReadString();
					CommandRecievedEventArgs commandRecievedEventArgs = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(commandLine);

					Console.WriteLine("Got command: {0}", commandLine);
					bool r;
					string result = this.controller.ExecuteCommand((int)commandRecievedEventArgs.CommandID,
						commandRecievedEventArgs.Args, out r);
					writer.Write(result);
					//client.Close();
					}
			}).Start();
        }
    }
}
