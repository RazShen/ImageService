using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedFiles;
namespace ImageServiceGUI.ViewModel
	{
	/// <summary>
	/// View Model for the log (enables the view to ask for data/actions from the model)
	/// </summary>
	class LogViewModel: INotifyPropertyChanged
		{
		public event PropertyChangedEventHandler PropertyChanged;
		private ILogModel logModel = new LogModel();
		/// <summary>
		/// The viewed list of logs
		/// </summary>
		public ObservableCollection<LogTuple> Logs
			{
			get { return this.logModel.Logs; }
			set => throw new NotImplementedException();
			}

		}
	}
