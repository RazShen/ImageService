﻿using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseDLL;

namespace ImageService.Logging
{
	/// <summary>
	/// LoggingService for the log of this program.
	/// </summary>
	public interface ILoggingService
    {
        event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        void Log(string message, MessageTypeEnum type);           // Logging the Message
    }
}
