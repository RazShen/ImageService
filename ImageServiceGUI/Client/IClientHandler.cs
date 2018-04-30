using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Client
	{
	public interface IClientHandler
		{
		void HandleClient(TcpClient client);
		}
	}
