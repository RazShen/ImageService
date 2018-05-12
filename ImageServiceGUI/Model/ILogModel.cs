using ImageServiceGUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedFiles;
namespace ImageServiceGUI.Model
{
    /// <summary>
    /// ilog model interface
    /// </summary>
    interface ILogModel :INotifyPropertyChanged
    {
		ObservableCollection<LogTuple> Logs { get; set; }
		}
	}
