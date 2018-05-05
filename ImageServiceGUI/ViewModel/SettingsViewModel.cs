using SharedFiles;
using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using ImageService.Modal;
using ImageService.Infrastructure.Enums;

namespace ImageServiceGUI.ViewModel
{
    class SettingsViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<string> VM_Handlers
        {
            get { return model.Handlers; }
            set => throw new NotImplementedException();
        }

        private IsettingsModel model;
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
            this.RemoveCommand =  new DelegateCommand<object>(Remove, CanRemove) as ICommand;
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
        private bool CanRemove(object obj) {
            if(this.selectedHandler !=null)
            {
                return true;
            }
            return false;
        }
        private void Remove(object obj)
        {
            string[] arrToSent = { this.selectedHandler };
            CommandRecievedEventArgs eventArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseHandlerCommand, arrToSent, "");
            //sent tcp command
            this.model.Handlers.Remove(this.selectedHandler);
        }
        private string selectedHandler;
        public string SelectedHandler
        {
            get { return this.selectedHandler; }
            set
            {
                selectedHandler = value;
                var command = this.RemoveCommand as DelegateCommand<object>;
                command.RaiseCanExecuteChanged();
            }
        }
    }
}
