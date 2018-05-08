using ImageServiceTools.Controller;
using ImageServiceTools.Controller.Handlers;
using ImageServiceTools.Logging;
using ImageServiceTools.Modal;
using System;
using SharedFiles;
using System.Collections;
using System.Collections.Generic;

namespace ImageServiceTools.Server
{
	/// <summary>
	/// image server class
	/// </summary>
    public class ImageServer
    {

        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;
        private IDirectoryHandler[] directoryHandlers;
        public Dictionary<string, IDirectoryHandler> Handlers { get; set; }

        #endregion

        /// <summary>
        /// image server constractor.
        /// </summary>
        /// <param name="controller"> given controller</param>
        /// <param name="logger"> given logger</param>
        /// <param name="paths">given paths</param>
        /// <param name="numOfPaths">number of path for the server</param>
        public ImageServer(IImageController controller, ILoggingService logger, string[] paths, int numOfPaths)
        {
            this.directoryHandlers = new DirectoryHandler[numOfPaths];
            this.m_controller = controller;
            this.m_logging = logger;
            this.Handlers = new Dictionary<string, IDirectoryHandler>();
            for (int i = 0; i < numOfPaths; i++)
            {
                // create newHandler for each path
         //       directoryHandlers[i] = new DirectoryHandler(m_controller, m_logging, paths[i]);
           //     directoryHandlers[i].StartHandleDirectory(paths[i]);
           //     this.CommandRecieved += directoryHandlers[i].OnCommandRecieved;
            //    directoryHandlers[i].DirectoryClose += this.RemoveDirectoryHandler;

                IDirectoryHandler newHandler = new DirectoryHandler(m_controller, m_logging, paths[i]);
                newHandler.StartHandleDirectory(paths[i]);
                this.CommandRecieved += newHandler.OnCommandRecieved;
                newHandler.DirectoryClose += this.RemoveDirectoryHandler;
                Handlers[paths[i]] = newHandler;
            }
        }
		
		/// <summary>
		/// this method closes the server
		/// </summary>
        public void CloseServer()
        {
            this.m_logging.Log("Begin closing server", MessageTypeEnum.INFO);
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseHandlerCommand, null, "");
            this.CommandRecieved?.Invoke(this, commandRecievedEventArgs);
		}

        
        /// <summary>
		/// this method removes directory newHandler from the server
		/// </summary>
		/// <param name="sender">the given sender </param>
		/// <param name="messageArgs">given arguments for the massage</param>
        private void RemoveDirectoryHandler(object sender, DirectoryCloseEventArgs messageArgs)
        {
            IDirectoryHandler sendingDirectoryHandler = sender as IDirectoryHandler;
            if (sendingDirectoryHandler == null)
            {
				//an object that isn't supposed to activate the event did it
				m_logging.Log("Unfamiliar source tried to abort handling foler: " + messageArgs.DirectoryPath, MessageTypeEnum.WARNING);
                return;
            }
            this.m_logging.Log("Directory Handler of Directory in: " + messageArgs.DirectoryPath + @" with message: " + messageArgs.Message, MessageTypeEnum.INFO);
            //unsubscribing of the IDirectoryHandler from the server message feed
            this.CommandRecieved -= sendingDirectoryHandler.OnCommandRecieved;
            if (this.CommandRecieved == null) {
                //if all the Directory Handlers closed succefully the server itself can finally close
                 this.m_logging.Log("After this message, the server is closed", MessageTypeEnum.INFO);
            }
        }

        public void CloseDirectoryHandler(String path)
        {
			if (Handlers.ContainsKey(path))
				{

				Handlers[path].OnCloseHandler(this, new DirectoryCloseEventArgs("", null));
				if (this.CommandRecieved == null)
					{
					//if all the Directory Handlers closed succefully the server itself can finally close
					this.m_logging.Log("There are no Folders being watched now.", MessageTypeEnum.INFO);
					}

				}
        }
    }
}
