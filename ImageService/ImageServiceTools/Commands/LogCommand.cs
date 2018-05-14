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
	/// <summary>
	/// New file command
	/// </summary>
	class LogCommand : ICommand
		{

		private ILoggingService _loggingService;
		/// <summary>
		/// Constructor for command from logging service.
		/// </summary>
		/// <param name="loggingService"></param>
		public LogCommand(ILoggingService loggingService)
			{
			this._loggingService = loggingService;
			}

		/// <summary>
		/// Generate new LogCommand using the logging service
		/// </summary>
		/// <param name="args"> input args</param>
		/// <param name="result"> out result </param>
		/// <returns></returns>
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
