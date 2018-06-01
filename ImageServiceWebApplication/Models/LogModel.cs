using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWebApplication.Models
	{
	public class LogModel
		{
		public List<Log> logs;
		public LogModel()
			{
			logs = new List<Log>();
			logs.Insert(0,(new Log { Type = "WARNING", Message = "AronOld" }));
			logs.Insert(0, (new Log  { Type = "WARNING", Message = "AronNew" }));
			logs.Insert(0,( new Log { Type = "ERROR", Message = "Sinai" }));
			logs.Insert(0, (new Log { Type = "INFO", Message = "Nisim" }));
			}


		public class Log
			{
			[Required]
			[DataType(DataType.Text)]
			[Display(Name = "Type")]
			public string Type { get; set; }

			[Required]
			[DataType(DataType.Text)]
			[Display(Name = "Message")]
			public string Message { get; set; }
			}

	
		}
	}
	
