using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    /// <summary>
    /// settings model interface
    /// </summary>
    interface IsettingsModel : INotifyPropertyChanged
    {
        string OutputDirectory { get; set; }
        string SourceName { get; set; }
        string LogName { get; set; }
        string TumbnailSize { get; set; }
        ObservableCollection<string> Handlers { get; set; }
        /// <summary>
        /// remove handler fumctopm
        /// </summary>
        /// <param name="selectedHandler"> a given path of handler</param>  
        void RemoveHandler(string selectedHandler);
    }
	}
