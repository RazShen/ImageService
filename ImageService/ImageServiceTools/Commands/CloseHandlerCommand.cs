using ImageServiceTools.Server;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceTools.Commands
	{
	class CloseHandlerCommand : ICommand
		{
        private ImageServer imageServer;
        CloseHandlerCommand(ImageServer server)
        {
            this.imageServer = server;
        }

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
                string newHandlersS = newHandlersSB.ToString().Trim().TrimEnd(';');
                //open and change app config
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConfigurationManager.AppSettings.Remove("Handler");
                ConfigurationManager.AppSettings.Add("Handler", newHandlersS);
                // Save the configuration file.
                conf.Save(ConfigurationSaveMode.Minimal);
                // Force a reload of the changed section. This 
                // makes the new values available for reading.
                ConfigurationManager.RefreshSection("appSettings");
                //this.imageServer.
                return "";
            }
            catch (Exception e)
            {
                result = false;
                return e.ToString();
            }
        }
    }
}
