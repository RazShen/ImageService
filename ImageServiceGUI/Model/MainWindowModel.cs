using ImageServiceGUI.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
	{
	class MainWindowModel : INotifyPropertyChanged
		{
		public IClientGUI _logClient { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;
        private String isConnected;

        public MainWindowModel()
			{
			this._logClient = ClientGUI.Instance;
            HasConnection = _logClient.RunningToString();
        }
		public String HasConnection
			{
			get
				{
				if (_logClient.Running())
					{
					return "True";
					}
				return "False";
				}
			set
				{
                isConnected = value;
                OnPropertyChanged("HasConnection");

            }
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
