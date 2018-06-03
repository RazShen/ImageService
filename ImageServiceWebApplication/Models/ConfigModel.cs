using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWebApplication.Models
	{
	public class ConfigModel
		{

        //members
        private Client.Configurations configurations;

        public ConfigModel()
        {
          //  configurations = Client.Configurations.Instance;
        //    SourceName = configurations.SourceName;
      //      LogName = configurations.LogName;
    //        OutputDirectory = configurations.OutputDirectory;
  //          Handlers = configurations.Handlers;
//            ThumbnailSize = configurations.TumbnailSize;

            SourceName = "ss";
            LogName = "ee";
            OutputDirectory = "we";
            ThumbnailSize = "size";

            List<string> Handlers2 = new List<string>
            {
                "11"
            };
            Handlers = Handlers2;
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
        public List<string> Handlers { get; set; }

    }
}