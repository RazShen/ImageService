using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.ViewModel
	{
	class LogViewModel: INotifyPropertyChanged
		{
		public event PropertyChangedEventHandler PropertyChanged;
		private ObservableCollection<LogTuple> _logMessages;
		
		public LogViewModel()
			{
			_logMessages = new ObservableCollection<LogTuple>();
			_logMessages.Add(new LogTuple { EnumType = "INFO", Data =  "Check"});
			_logMessages.Add(new LogTuple { EnumType = "WARNING", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "WARNING", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "ERROR", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "WARNING", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "WARNING", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "ERROR", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "WARNING", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "WARNING", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "ERROR", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "WARNING", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "ERROR", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logMessages.Add(new LogTuple { EnumType = "ERROR", Data = "Check" });

			}
		public ObservableCollection<LogTuple> LogMessages
			{
			get { return this._logMessages; }
			set => throw new NotImplementedException();
			}
		}
	}
