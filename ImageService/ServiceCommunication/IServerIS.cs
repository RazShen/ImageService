using ImageService.Modal;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ServiceCommunication
	{
	interface IServerIS
		{
        void Start();
        void Stop();
        void NotifyClients(CommandRecievedEventArgs commandRecievedEventArgs);
		}
	}
