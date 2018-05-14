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
        public delegate void handlerCloseDelegate(CommandRecievedEventArgs commandRecieved);
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;
        public event handlerCloseDelegate CloseHandlerAlertAll;

		//private IDirectoryHandler[] directoryHandlers;
		public Dictionary<string, IDirectoryHandler> Handlers { get { return _handlers; } set { throw new NotImplementedException(); } }
        private Dictionary<string, IDirectoryHandler> _handlers;
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
            //this._handler = new DirectoryHandler[numOfPaths];
            this.m_controller = controller;
            this.m_logging = logger;
            this._handlers = new Dictionary<string, IDirectoryHandler>();
            for (int i = 0; i < numOfPaths; i++)
            {
                // create newHandler for each path
				 //  directoryHandlers[i] = new DirectoryHandler(m_controller, m_logging, paths[i]);
           // directoryHandlers[i].StartHandleDirectory(paths[i]);
           //this.CommandRecieved += directoryHandlers[i].OnCommandRecieved;
            //  directoryHandlers[i].DirectoryClose += this.RemoveDirectoryHandler;

                IDirectoryHandler newHandler = new DirectoryHandler(m_controller, m_logging, paths[i]);
                newHandler.StartHandleDirectory(paths[i]);
                this.CommandRecieved += newHandler.OnCommandRecieved;
                newHandler.DirectoryClose += this.RemoveDirectoryHandler;
                _handlers[paths[i]] = newHandler;
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

		/// <summary>
		/// Close directory handler (invoked from closehandler command)
		/// </summary>
		/// <param name="path"> handler path</param>
		/// <returns> succeed/didn't succeed </returns>
        public bool CloseDirectoryHandler(String path)
        {
			if (Handlers.ContainsKey(path))
				{
                bool result = _handlers[path].OnCloseHandler(this, new DirectoryCloseEventArgs("", null));
                if (result)
                {
                    String[] args = { path };
                    CommandRecievedEventArgs commandArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseHandlerCommand, args, "");
                    this.CloseHandlerAlertAll?.Invoke(commandArgs);
                    _handlers.Remove(path);
                }
				if (this.CommandRecieved == null)
					{
					//if all the Directory Handlers closed succefully the server itself can finally close
					this.m_logging.Log("There are no Folders being watched now.", MessageTypeEnum.WARNING);
					}

                return result;
				}
            return false;
        }
    }
}
