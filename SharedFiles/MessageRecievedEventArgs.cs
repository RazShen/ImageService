using System;

namespace SharedFiles
{
	/// <summary>
	/// MessageRecievedEventArgs class.
	/// </summary>
	public class MessageRecievedEventArgs : EventArgs
    {
        public MessageTypeEnum status { get; set; }
        public string message { get; set; }

		/// <summary>
		/// Constructor for MessageRecievedEventArgs made from MessageTypeEnum and the message itself.
		/// </summary>
		/// <param name="message"> specific message for the logger </param>
		/// <param name="type"> type of the message </param>
		public MessageRecievedEventArgs(MessageTypeEnum typeEnum, string message)
        {
            this.status = typeEnum;
            this.message = message;
        }
    }
}
