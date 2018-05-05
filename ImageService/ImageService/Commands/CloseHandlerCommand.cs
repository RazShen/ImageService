using ImageService.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
	{
	class CloseHandlerCommand
		{
        private ImageServer imageServer;
        CloseHandlerCommand(ImageServer server)
        {
            this.imageServer = server;
        }
		}
	}
