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
		public event newLogNotification LogNotificator;
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
			String[] args = new string[2];
			args[0] = message;
			args[1] = type.ToString();
            MessageRecieved?.Invoke(this, new MessageRecievedEventArgs(type, message));
			LogNotificator?.Invoke(new CommandRecievedEventArgs((int)CommandEnum.NewLogMessage, args, ""));
			_logs.Insert(0, (new LogTuple { EnumType = Enum.GetName(typeof(MessageTypeEnum), type), Data = message }));
			}

		public ObservableCollection<LogTuple> Logs
			{
			get { return this._logs; }
			set => throw new NotImplementedException();
			}
        /// <summary>
        /// this function gets the previous logs by given event log
        /// </summary>
        /// <param name="baseEventLog">  given event log </param>
		private void GetPreviousLogs(EventLog baseEventLog)
			{
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
				//this.Log(baseEventLog.Entries[i].Message, GetTypeFromLogEntry(baseEventLog.Entries[i].EntryType));
				_logs.Insert(0, (new LogTuple { EnumType = GetTypeFromLogEntry(baseEventLog.Entries[i].EntryType).ToString(),
					Data = baseEventLog.Entries[i].Message}));
				}
			}
        /// <summary>
        /// gets a type from long entry type 
        /// </summary>
        /// <param name="eventLogEntryType">given event log entry type</param>
        /// <returns></returns>
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
