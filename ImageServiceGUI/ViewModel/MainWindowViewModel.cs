using ImageServiceGUI.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageServiceGUI.ViewModel
{
	/// <summary>
	/// MainWindow view model, basically made for the main window to know if there is a tcp connection with the server.
	/// </summary>
    class MainWindowViewModel : INotifyPropertyChanged
    {
        MainWindowModel mainWindow = new MainWindowModel();
        public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// Property if the singleton client has connection.
		/// </summary>
        public String VM_HasConnection
        {
            get
            {
                return mainWindow.HasConnection;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        private void NotifyPropertyChanged(string propName)
        {
            PropertyChangedEventArgs propertyChangedEventArgs = new PropertyChangedEventArgs(propName);
            this.PropertyChanged?.Invoke(this, propertyChangedEventArgs);
        }
        public ICommand CloseCommand { get; set; }
		public String HasConnection
			{
			get
				{
				return mainWindow.HasConnection;
				}
			set
				{
				throw new NotImplementedException();
				}
			}

		/// <summary>
		/// Constructor for the main window VM (constructs a main window model)
		/// </summary>
        public MainWindowViewModel()
        {
            this.mainWindow = new MainWindowModel();
            this.mainWindow.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

            this.CloseCommand = new DelegateCommand<object>(this.OnClose, this.CanClose);
        }

        private void OnClose(object obj)
        {
            this.mainWindow._logClient.Close();
        }

        private bool CanClose(object obj) => true;
    }
}