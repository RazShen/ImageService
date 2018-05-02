using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.ViewModel
	{
	class SettingsViewModel
		{
        public string VM_OutputDirectory { get; }
        public string VM_SourceName { get; }
        string VM_LogName { get; }
        public string VM_TumbnailSize { get; }
        ObservableCollection<string> VM_Handlers { get; }
    }
	}
