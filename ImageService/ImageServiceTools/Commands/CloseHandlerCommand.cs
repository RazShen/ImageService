using ImageServiceTools.Server;
using Newtonsoft.Json;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceTools.Commands
	{
    /// <summary>
    /// close handler command
    /// </summary>
	class CloseHandlerCommand : ICommand
		{
        private ImageServer imageServer;
        /// <summary>
        /// Constractor for a close handlewr command
        /// </summary>
        /// <param name="server">given image server </param>
        public CloseHandlerCommand(ImageServer server)
        {
            this.imageServer = server;
        }
        /// <summary>
        /// This function execute close handler command 
        /// </summary>
        /// <param name="args">given args array</param>
        /// <param name="result"> indicate if the execute succeed</param>
        /// <returns></returns>
        public string Execute(string[] args, out bool result)
        {
            try
            {
                result = false;
                if(args.Length < 1 || args == null)
                {
                    result = false;
                    throw new Exception("invalid argumernts for close handler command");
                }
                string handlerToDelete = args[0];
                string[] handlers = (ConfigurationManager.AppSettings.Get("Handler").Split(';'));
                StringBuilder newHandlersSB = new StringBuilder();
                for(int i = 0; i < handlers.Length; i++)
                {
                    if(handlers[i] != handlerToDelete)
                    {
                        newHandlersSB.Append(handlers[i] + ";");
                    }
                    else
                    {
                        result = true;
                    }
                }
                string finishedH = newHandlersSB.ToString().Trim().TrimEnd(';');
                //open and change app config
                Configuration configM = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				configM.AppSettings.Settings.Remove("Handler");
				configM.AppSettings.Settings.Add("Handler", finishedH);
				// Save the configuration file.
				configM.Save(ConfigurationSaveMode.Minimal);
				// Force a reload of the changed section. This 
				// makes the new values available for reading.
				ConfigurationManager.RefreshSection("appSettings");
				//  this.imageServer.CloseServer();
				String[] arr = new String[1];
				CommandRecievedEventArgs commandSendArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseHandlerCommand, arr, "");
				if (this.imageServer.CloseDirectoryHandler(handlerToDelete))
					{
					arr[0] = "True";
					}
				else
					{
					arr[0] = "False";
					}
				return JsonConvert.SerializeObject(commandSendArgs);
				}
			catch (Exception e)
            {
                result = false;
                return e.ToString();
            }
        }
    }
}
