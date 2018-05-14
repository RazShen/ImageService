using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageServiceTools.ServiceCommunication
	{
	interface IClientHandler
		{
		/// <summary>
		/// Handle the clients that the server accepted and tcp conneted to it.
		/// </summary>
		/// <param name="client"> tcp client</param>
		/// <param name="clients"> connected client list </param>
        void HandleClient(TcpClient client, List<TcpClient> clients);
    }
}
