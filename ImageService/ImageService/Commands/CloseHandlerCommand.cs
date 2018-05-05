using ImageService.Server;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
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
                result = true;
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

                }


            }
            catch (Exception e)
            {
                
            }
            throw new NotImplementedException();
        }
    }
	}
