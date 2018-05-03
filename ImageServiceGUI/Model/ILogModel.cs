using ImageServiceGUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseDLL;
namespace ImageServiceGUI.Model
{
    interface ILogModel :INotifyPropertyChanged
    {
		ObservableCollection<LogTuple> Logs { get; set; }
		}
	}
