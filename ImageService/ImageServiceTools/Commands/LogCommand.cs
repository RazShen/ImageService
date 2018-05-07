using ImageServiceTools.Logging;
using Newtonsoft.Json;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceTools.Commands
	{
	class LogCommand : ICommand
		{

		private ILoggingService _loggingService;
		public LogCommand(ILoggingService loggingService)
			{
			this._loggingService = loggingService;
			}

		public string Execute(string[] args, out bool result)
			{
			try
				{
				ObservableCollection<LogTuple> logs = this._loggingService.Logs;
				string decriptedLogs = JsonConvert.SerializeObject(logs);
				string[] arr = new string[1];
				arr[0] = decriptedLogs;
				CommandRecievedEventArgs commandSendArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, arr, "");
				result = true;
				return JsonConvert.SerializeObject(commandSendArgs);
				}
			catch (Exception e)
				{
				result = false;
				return "Error in get previous logs command";
				}
			}

		}
	}
