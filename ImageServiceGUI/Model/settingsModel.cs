using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    class SettingsModel : IsettingsModel
    {
        //  public string OutputDirectory { get; set; }
        //  public string SourceName { get; set; }
        //  public string LogName { get; set; }
        //  public string TumbnailSize { get; set; }
        //   public ObservableCollection<string> Handlers { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public SettingsModel()
        {
            this.Initialize();
        }

        private string m_outputDirectory;
        public string OutputDirectory
        {
            get { return m_outputDirectory; }
            set
            {
                m_outputDirectory = value;
                NotifyPropertyChanged("OutputDirectory");
            }
        }
        private string m_sourceName;
        public string SourceName
        {
            get { return m_sourceName; }
            set
            {
                m_sourceName = value;
                NotifyPropertyChanged("SourceName");
            }
        }
        private string m_logName;
        public string LogName
        {
            get { return m_logName; }
            set
            {
                m_logName = value;
                NotifyPropertyChanged("LogName");
            }
        }
        private string m_tumbnailSize;
        public string TumbnailSize
        {
            get { return m_tumbnailSize; }
            set
            {
                m_tumbnailSize = value;
                NotifyPropertyChanged("TumbnailSize");
            }
        }
        /// <summary>
        /// /get and set
        /// </summary>
        public ObservableCollection<string> Handlers { get; set; }

        private void Initialize()
        {
            this.OutputDirectory = "OutputDir";
            this.SourceName = "SourceName";
            this.LogName = "log";
            this.TumbnailSize = "sss";
            Handlers = new ObservableCollection<string>();
            //string[] handlers = string.Split(';');
            string[] handlers = { "a", "b", "c"};
            foreach (string handler in handlers)
            {
                this.Handlers.Add(handler);
            }
        }



    }


}
