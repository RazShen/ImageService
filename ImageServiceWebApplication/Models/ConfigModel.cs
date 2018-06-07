using ImageServiceWebApplication.Client;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWebApplication.Models
	{
	public class ConfigModel
		{

		//members
		private Client.Configurations configurations;
		private IClient ImageWebModelClient { get; set; }


		public ConfigModel()
			{
			configurations = Client.Configurations.Instance;
			SourceName = configurations.SourceName;
			LogName = configurations.LogName;
		    OutputDirectory = configurations.OutputDirectory;
			Handlers = configurations.Handlers;
			ThumbnailSize = configurations.TumbnailSize;
			this.ImageWebModelClient = Client.Client.Instance;
			this.ImageWebModelClient.UpdateEvent += ConstUpdate;
			//SourceName = "ss";
			//LogName = "ee";
			//OutputDirectory = "we";
			//ThumbnailSize = "size";

			//List<string> Handlers2 = new List<string>
			//{
			//	"11", "22"
			//};
			//Handlers = Handlers2;
			}

		public bool DeleteHandler(string HandlerToDelete)
			{
			try
				{
				string[] arr = { HandlerToDelete };
				if (!ImageWebModelClient.Running()) { return false; }
				CommandRecievedEventArgs commandReq = new CommandRecievedEventArgs((int)CommandEnum.CloseHandlerCommand, arr, "");
				this.ImageWebModelClient.WriteCommandToServer(commandReq);
				}
			catch (Exception e)
				{
				Console.WriteLine(e.ToString());
				return false;
				}
			return true;
			}

		private void CloseHandler(CommandRecievedEventArgs responseObj)
			{
			if (Handlers.Contains(responseObj.Args[0]) && responseObj.Args != null
				&& responseObj != null && Handlers != null)
				{
				this.Handlers.Remove(responseObj.Args[0]);
				}
			}
		private void ConstUpdate(CommandRecievedEventArgs args)
			{
			if (args != null)
				{
				switch (args.CommandID)
					{
					case (int)CommandEnum.CloseHandlerCommand:
						CloseHandler(args);
						break;
					}
				}
			}
		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Output Directory:  ")]
		public string OutputDirectory { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Source Name:   ")]
		public string SourceName { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Log Name:  ")]
		public string LogName { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Thumbnail Size:    ")]
		public string ThumbnailSize { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Handlers: ")]
		public ObservableCollection<string> Handlers { get; set; }
		}
	}