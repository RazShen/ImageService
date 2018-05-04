using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Modal;
using System.Collections.ObjectModel;
using SharedFiles;


namespace ImageService.Logging
{
	/// <summary>
	/// LoggingService for the log of this program.
	/// </summary>
	public class LoggingService : ILoggingService
    {
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
		public ObservableCollection<LogTuple> Logs { get; set; }

		/// <summary>
		/// Invoke the eventHandler with all it's delegates (in this case- mostly write into logger)
		/// </summary>
		/// <param name="message"> specific message for the logger </param>
		/// <param name="type"> type of the message </param>
		public void Log(string message, MessageTypeEnum type)
        {
            MessageRecieved?.Invoke(this, new MessageRecievedEventArgs(type, message));
        }
    }
}
