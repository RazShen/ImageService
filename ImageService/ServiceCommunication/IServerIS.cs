using ImageServiceTools.Modal;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceTools.ServiceCommunication
	{
	interface IServerIS
		{
        void Start();
        void Stop();
        void NotifyClients(CommandRecievedEventArgs commandRecievedEventArgs);
		}
	}
