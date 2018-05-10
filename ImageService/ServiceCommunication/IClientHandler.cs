﻿using System;
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
        void HandleClient(TcpClient client, List<TcpClient> clients);
    }
}
