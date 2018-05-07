using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SharedFiles;
using System.Diagnostics;

namespace ImageServiceTools.Logging
{
	/// <summary>
	/// LoggingService for the log of this program.
	/// </summary>
	public class LoggingService : ILoggingService
    {
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
		private ObservableCollection<LogTuple> _logs { get; set; }
		public LoggingService(EventLog baseEventLog)
			{
			this._logs = new ObservableCollection<LogTuple>();
			this.GetPreviousLogs(baseEventLog);
			}
		/// <summary>
		/// Invoke the eventHandler with all it's delegates (in this case- mostly write into logger)
		/// </summary>
		/// <param name="message"> specific message for the logger </param>
		/// <param name="type"> type of the message </param>
		/// 

		public void Log(string message, MessageTypeEnum type)
			{
            MessageRecieved?.Invoke(this, new MessageRecievedEventArgs(type, message));
			_logs.Insert(0, (new LogTuple { EnumType = Enum.GetName(typeof(MessageTypeEnum), type), Data = message }));
			}

		public ObservableCollection<LogTuple> Logs
			{
			get { return this._logs; }
			set => throw new NotImplementedException();
			}

		private void GetPreviousLogs(EventLog baseEventLog)
			{
			List<EventLogEntry> tempLogs = new List<EventLogEntry>();
			int numOfLogs = baseEventLog.Entries.Count;
			int j;
			for (j = numOfLogs - 1; j >= 0; j--)
				{
					if (baseEventLog.Entries[j].Message.Contains("OnStart"))
					{
					break;
					}
				}
			for (int i = j; i <= numOfLogs - 1; i++)
				{
				
				this.Log(baseEventLog.Entries[i].Message, GetTypeFromLogEntry(baseEventLog.Entries[i].EntryType));

				}
			}

		public static MessageTypeEnum GetTypeFromLogEntry(EventLogEntryType eventLogEntryType)
			{
			switch (eventLogEntryType)
				{
				case EventLogEntryType.Information:
					return MessageTypeEnum.INFO;
				case EventLogEntryType.Warning:
					return MessageTypeEnum.WARNING;
				case EventLogEntryType.Error:
					return MessageTypeEnum.ERROR;
				default:
					return MessageTypeEnum.INFO;
				}
			}
	}
}
