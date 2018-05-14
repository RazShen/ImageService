using ImageServiceGUI.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
	{
    /// <summary>
    /// main window model class
    /// </summary>
	class MainWindowModel : INotifyPropertyChanged
		{
		public IClientGUI _logClient { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;
        private String isConnected;
        /// <summary>
        /// constractor for main window model
        /// </summary>
        public MainWindowModel()
			{
			this._logClient = ClientGUI.Instance;
            HasConnection = _logClient.RunningToString();
        }
		/// <summary>
		/// there's a binding between the view and this field
		/// </summary>
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
        /// <summary>
        /// on property change, used to notify the main view that the client is not running.
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
