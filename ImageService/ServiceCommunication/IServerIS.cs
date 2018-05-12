using ImageServiceTools.Modal;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceTools.ServiceCommunication
	{
    /// <summary>
    /// server for Image Service interface
    /// </summary>
	interface IServerIS
		{
        /// <summary>
        /// this function starts the server
        /// </summary>
        void Start();
        /// <summary>
        /// this function stops the server
        /// </summary>
        void Stop();
        /// <summary>
        /// This function notify all cliets about an update
        /// </summary>
        /// <param name="commandRecievedEventArgs"></param>
        void NotifyClients(CommandRecievedEventArgs commandRecievedEventArgs);
		}
	}
