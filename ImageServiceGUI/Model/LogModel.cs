using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageServiceGUI.ViewModel;
using SharedFiles;

namespace ImageServiceGUI.Model
	{
	class LogModel : ILogModel
		{
		private ObservableCollection<LogTuple> _logs { get; set; }
		public ObservableCollection<LogTuple> Logs
			{
			get { return this._logs; }
			set => throw new NotImplementedException();
			}
		public event PropertyChangedEventHandler PropertyChanged;

		public LogModel()
			{
			_logs = new ObservableCollection<LogTuple>();
			_logs.Add(new LogTuple
				{
				EnumType = "INFO",
				Data = "CheckCheckChec\n\nkCheckCheckCheckCheck" +
				"CheckCheckCheckCheckCheckCheckCheckCheckChecknkCheckCheckCheckCheckCheckCheckCheckCheckCheckCheckCheckCheckCheck"
				});
			_logs.Add(new LogTuple { EnumType = "WARNING", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "WARNING", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "ERROR", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "WARNING", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "WARNING", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "ERROR", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "WARNING", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "WARNING", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "ERROR", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "WARNING", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "ERROR", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "INFO", Data = "Check" });
			_logs.Add(new LogTuple { EnumType = "ERROR", Data = "Check" });
			int x;
			Random rand = new Random();

			for (int i = 0; i < 1; i++)
				{
				x = rand.Next(1, 4);
				if (x==1)
					{
					_logs.Add(new LogTuple { EnumType = "ERROR", Data = "Check " + i });
	

					} else if (x==2)
					{
					_logs.Add(new LogTuple { EnumType = "INFO", Data = "Check" + i });

						}
				else
					{
					_logs.Add(new LogTuple { EnumType = "WARNING", Data = "Check" + i });

					}
				}
			}
		}
	}
