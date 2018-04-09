using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Modal;

/// <summary>
/// 
/// </summary>
namespace ImageService.Logging
{
	/// <summary>
	/// 
	/// </summary>
    public class LoggingService : ILoggingService
    {
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="type"></param>
        public void Log(string message, MessageTypeEnum type)
        {
            MessageRecieved?.Invoke(this, new MessageRecievedEventArgs(type, message));
        }
    }
}
