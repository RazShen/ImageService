using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Configuration;
using ImageService.Server;
using ImageService.Modal;
using ImageService.Controller;
using ImageService.Logging;
using ImageService.Infrastructure;
using ImageService.Logging.Modal;
using BaseDLL;

namespace ImageService
{

    public enum ServiceState
    {
        SERVICE_STOPPED = 0x00000001,
        SERVICE_START_PENDING = 0x00000002,
        SERVICE_STOP_PENDING = 0x00000003,
        SERVICE_RUNNING = 0x00000004,
        SERVICE_CONTINUE_PENDING = 0x00000005,
        SERVICE_PAUSE_PENDING = 0x00000006,
        SERVICE_PAUSED = 0x00000007,
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceStatus
    {
        public int dwServiceType;
        public ServiceState dwCurrentState;
        public int dwControlsAccepted;
        public int dwWin32ExitCode;
        public int dwServiceSpecificExitCode;
        public int dwCheckPoint;
        public int dwWaitHint;
    };

	/// <summary>
	/// 
	/// 
	/// </summary>
    public partial class ImageService : ServiceBase
    {
        private System.ComponentModel.IContainer components;
        private System.Diagnostics.EventLog eventLog1;
        private int eventId = 1;
        private ImageServer imageServer;          // The Image Server
        private IImageServiceModal modal;
        private IImageController controller;
        private ILoggingService logging;

		/// <summary>
		/// constactor for Image service
		/// </summary>
		/// <param name="args">the given arguments for image service</param>
        public ImageService(string[] args)
        {
            InitializeComponent();
            string eventSourceName = ConfigurationManager.AppSettings.Get("SourceName");
            string logName = ConfigurationManager.AppSettings.Get("LogName");
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName);
            }
            eventLog1.Source = eventSourceName;
            eventLog1.Log = logName;

            //initialize the members
            this.modal = new ImageServiceModal()  {
                    OutputFolder = ConfigurationManager.AppSettings.Get("OutputDir"),
                    ThumbnailSize = Int32.Parse(ConfigurationManager.AppSettings.Get("ThumbnailSize"))
            };
            this.controller = new ImageController(this.modal);
            this.logging = new LoggingService();
            this.logging.MessageRecieved += this.WriteMessage;
            string[] directories = (ConfigurationManager.AppSettings.Get("Handler").Split(';'));
            this.imageServer = new ImageServer(this.controller, this.logging, directories, directories.Length);
        }

		/// <summary>
		/// the function starts the server
		/// </summary>
		/// <param name="args">given argumnets</param>
        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            eventLog1.WriteEntry("In OnStart");
            // Set up a timer to trigger every minute.  
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds  
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();

            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

		/// <summary>
		/// the function stops the server
		/// </summary>
        protected override void OnStop()
        {
            eventLog1.WriteEntry("In onStop.");
            eventLog1.WriteEntry("Tells server to stop.");
            this.imageServer.CloseServer();
        }

		/// <summary>
		/// the function operates every certain time and log it to the event log
		/// </summary>
		/// <param name="sender">the sender</param>
		/// <param name="args"> arguments for the timer </param>
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.  
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }

		/// <summary>
		/// the function write in on continue to the event log
		/// </summary>
        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
        }

		/// <summary>
		/// the function write to the envent log the is in on pause
		/// </summary>
        protected override void OnPause()
        {
            eventLog1.WriteEntry("In OnPause.");
        }
        
		/// <summary>
		/// the function write massage to the event log
		/// </summary>
		/// <param name="sender"> the given sendere to the event log</param>
		/// <param name="e">the given arguments of the massage (name and status)</param>
        public void WriteMessage(Object sender, MessageRecievedEventArgs e)
        {
            eventLog1.WriteEntry(e.message, GetType(e.status));
        }
        
		/// <summary>
		/// this method gets the status of the massage and returns the type of the event log massage
		/// </summary>
		/// <param name="status"> the given statud of the massage</param>
		/// <returns> the type of the event log massage</returns>
        private EventLogEntryType GetType(MessageTypeEnum status)
        {
            switch (status)
            {
                case MessageTypeEnum.ERROR:
                    return EventLogEntryType.Error;
                case MessageTypeEnum.WARNING:
                    return EventLogEntryType.Warning;
                case MessageTypeEnum.INFO:
                default:
                    return EventLogEntryType.Information;
            }
        }
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
    }
}
