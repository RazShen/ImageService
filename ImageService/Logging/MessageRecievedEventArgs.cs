using System;


/// <summary>
/// 
/// </summary>
namespace ImageService.Logging.Modal
{
	/// <summary>
	/// 
	/// </summary>
    public class MessageRecievedEventArgs : EventArgs
    {
        public MessageTypeEnum status { get; set; }
        public string message { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="typeEnum"></param>
		/// <param name="message"></param>
        public MessageRecievedEventArgs(MessageTypeEnum typeEnum, string message)
        {
            this.status = typeEnum;
            this.message = message;
        }
    }
}
