using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedFiles;

namespace ImageServiceTools.Logging
{
	public delegate void newLogNotification(CommandRecievedEventArgs commandRecieved);
	/// <summary>
	/// LoggingService for the log of this program.
	/// </summary>
	public interface ILoggingService
    {
		event newLogNotification LogNotificator;
		event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        void Log(string message, MessageTypeEnum type);           // Logging the Message
		ObservableCollection<LogTuple> Logs { get; set; }
    }
}
