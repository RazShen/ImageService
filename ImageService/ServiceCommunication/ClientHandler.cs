using ImageService.Controller;
using ImageService.Modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ServiceCommunication
	{
    class ClientHandler : IClientHandler
    {
        IImageController controller;
        public ClientHandler (IImageController imageController)
        {
            this.controller = imageController;
        }
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string commandLine = reader.ReadLine();
                    bool outbool;
                    CommandRecievedEventArgs commandRecievedEventArgs = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(commandLine);
                    string result = controller.ExecuteCommand(commandRecievedEventArgs.CommandID, commandRecievedEventArgs.Args, out outbool);
                    writer.Write(result);
                }
                client.Close();
            }).Start();
        }
    }
}
