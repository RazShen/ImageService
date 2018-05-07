using ImageServiceTools.Controller;
using ImageServiceTools.Controller.Handlers;
using ImageServiceTools.Logging;
using ImageServiceTools.Modal;
using System;
using SharedFiles;

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
        private DirectoyHandler[] directoryHandlers;
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
            this.directoryHandlers = new DirectoyHandler[numOfPaths];
            this.m_controller = controller;
            this.m_logging = logger;
            for (int i = 0; i < numOfPaths; i++)
            {
                // create handler for each path
                directoryHandlers[i] = new DirectoyHandler(m_controller, m_logging, paths[i]);
                directoryHandlers[i].StartHandleDirectory(paths[i]);
                this.CommandRecieved += directoryHandlers[i].OnCommandRecieved;
                directoryHandlers[i].DirectoryClose += this.RemoveDirectoryHandler;
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
		/// this method removes directory handler from the server
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
            //unsubscribing of the DirectoryHandler from the server message feed
            this.CommandRecieved -= sendingDirectoryHandler.OnCommandRecieved;
            if (this.CommandRecieved == null) {
                //if all the Directory Handlers closed succefully the server itself can finally close
                 this.m_logging.Log("After this message, the server is closed", MessageTypeEnum.INFO);
            }
        }

        public void CloseDirectoryHandler(String path)
        {

        }
    }
}
