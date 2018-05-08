using ImageServiceTools.Modal;
using System;
using System.IO;
using ImageServiceTools.Logging;
using SharedFiles;
using ImageServiceTools.Server;

namespace ImageServiceTools.Controller.Handlers
{
	/// <summary>
	/// DirectoryHandler for handling a single directory.
	/// </summary>
	public class DirectoryHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher[] m_dirWatchers;             // The Watchers of the Dir
        private string m_path;                              // The Path of directory
		// The Event That Notifies that the Directory is being closed
		public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;
		#endregion

		/// <summary>
		/// DirectoryHandler constructor.
		/// </summary>
		/// <param name="controller"> transfer the new file command </param>
		/// <param name="logger"> writing to the log </param>
		/// <param name="inputPath"> of the folder to handle </param>
		public DirectoryHandler(IImageController controller, ILoggingService logger, String inputPath)
        {
            this.m_controller = controller;
            this.m_logging = logger;
            this.m_path = inputPath;
        }


		/// <summary>
		/// StartHandleDirectory method that constructs the filewatchers.
		/// </summary>
		/// <param name="dirPath"> of the path to handle </param>
		public void StartHandleDirectory(string dirPath)
        {
            this.m_logging.Log("starting to handling the directory in path: " + dirPath, MessageTypeEnum.INFO);
            this.m_path = dirPath;

            string[] extension = {"*.jpg", "*.png", "*.gif", "*.bmp"};
            this.m_dirWatchers = new FileSystemWatcher[extension.Length];
            for (int i = 0; i < extension.Length ; i ++) {
                this.m_dirWatchers[i] = new FileSystemWatcher(this.m_path, extension[i])
                {
                    IncludeSubdirectories = true,
                    NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite 
                };
                this.m_dirWatchers[i].EnableRaisingEvents = true;
                this.m_dirWatchers[i].Created += new FileSystemEventHandler(delegate (object sender, FileSystemEventArgs e)
                {
                    string[] args = new string[4];
                    args[0] = this.m_path;
                    args[1] = e.Name;
                    DateTime date = GetExplorerFileDate(e.FullPath);
                    this.m_logging.Log("GetDateTakenFromImage: " + args[0], MessageTypeEnum.INFO);
                    args[2] = date.Year.ToString();
                    args[3] = date.Month.ToString();
                    bool result;
                    this.m_logging.Log("new file in directory of path: " + this.m_path, MessageTypeEnum.INFO);
                    string s = this.m_controller.ExecuteCommand((int) CommandEnum.NewFileCommand, args, out result); 
                    this.m_logging.Log(s, MessageTypeEnum.INFO);
                });
            }    
        }

		/// <summary>
		/// OnCommandRecieved of this handler actually tells the server that this handler is being closed
		/// and when the last handler is closed the server can finally stop.
		/// </summary>
		/// <param name="sender"> sender </param>
		/// <param name="e"> CommandRecievedEventArgs </param>
		public void OnCommandRecieved(object sender, CommandRecievedEventArgs e) {
            switch (e.CommandID)
            {                
                case (int) CommandEnum.CloseHandlerCommand:
                    this.m_logging.Log("closing Directory Handler of directory in path: " + this.m_path, MessageTypeEnum.INFO);
                    DirectoryClose?.Invoke(this, new DirectoryCloseEventArgs(this.m_path, "closing"));
                    for (int i = 0; i< 4;i ++) {
                        this.m_dirWatchers[i].Dispose();
                    }
                    break;
                default:
                    throw new ArgumentException();
            }
        }

		/// <summary>
		/// Get time when the picture was taken.
		/// </summary>
		/// <param name="filename"> path to the picture </param>
		/// <returns> full DateTime </returns>
        private static DateTime GetExplorerFileDate(string filename)
        {
            DateTime now = DateTime.Now;
            TimeSpan localOffset = now - now.ToUniversalTime();
            return File.GetLastWriteTimeUtc(filename) + localOffset;
        }

        /// <summary>
        /// close the handler
        /// </summary>
        /// <param name="sender"> sender </param>
        /// <param name="e"> CommandRecievedEventArgs </param>
        public void OnCloseHandler(object sender, DirectoryCloseEventArgs e)
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    this.m_dirWatchers[i].EnableRaisingEvents = false;
                }
                 ((ImageServer)sender).CommandRecieved -= this.OnCommandRecieved;
                this.m_logging.Log("closed handler of path " + this.m_path, MessageTypeEnum.INFO);
            } catch (Exception ex)
            {
                this.m_logging.Log("Eroor when closing handler of path " + this.m_path, MessageTypeEnum.INFO);
            }
        }
        
        }
    }
