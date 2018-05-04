using ImageService.Commands;
using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.ViewModel
	{
	class SettingsViewModel : INotifyPropertyChanged
		{

        ObservableCollection<string> VM_Handlers { get; }
        private SettingsModel model;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RemoveCommand { get; set; }


        public SettingsViewModel()
        {
            this.model = new SettingsModel();
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }


        private void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public string VM_OutputDirectory
        {
            get { return model.OutputDirectory; }
        }
        public string VM_SourceName
        {
            get { return model.SourceName; }
        }
        public string VM_LogName
        {
            get { return model.LogName; }
        }
        public string VM_TumbnailSize
        {
            get { return model.TumbnailSize; }
        }
    }
}
