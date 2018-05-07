﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedFiles;

namespace ImageServiceTools.Logging
{
	/// <summary>
	/// LoggingService for the log of this program.
	/// </summary>
	public interface ILoggingService
    {
        event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        void Log(string message, MessageTypeEnum type);           // Logging the Message
		ObservableCollection<LogTuple> Logs { get; set; }
    }
}
