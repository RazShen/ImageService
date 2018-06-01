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
			logs.Insert(0,(new Log { FirstName = "WARNING", LastName = "Aron" }));

			logs.Insert(0, (new Log  { FirstName = "WARNING", LastName = "Aron" }));
			logs.Insert(0,( new Log { FirstName = "ERROR", LastName = "Sinai" }));
			logs.Insert(0, (new Log { FirstName = "INFO", LastName = "Nisim" }));

			}


		public class Log
			{

			public Log()
				{

				}


			[Required]
			[DataType(DataType.Text)]
			[Display(Name = "Type")]
			public string FirstName { get; set; }

			[Required]
			[DataType(DataType.Text)]
			[Display(Name = "Message")]
			public string LastName { get; set; }

			}


		}
	}
	
