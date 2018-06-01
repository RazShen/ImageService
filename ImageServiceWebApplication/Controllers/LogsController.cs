using ImageServiceWebApplication.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApplication.Controllers
{
    public class LogsController : Controller
    {
		public LogModel logModel; 
		
		// GET: First/Create
		public ActionResult Logs()
			{
			logModel = LogModel.Instance;
			List<LogModel.Log> listOflogs = logModel.GetLogs();
			return View(logModel.logs);
			}

		[HttpPost]
		public ActionResult Logs(FormCollection form)
			{
			logModel = LogModel.Instance;

			ViewBag.SearchKey = form["typeToFind"];		
			if (ViewBag.SearchKey == "")
				{
				return View(logModel.logs);
				} else {
				List<LogModel.Log> logsByType = new List<LogModel.Log>();
				foreach (LogModel.Log log in this.logModel.logs)
					{
					if (log.Type.Equals(ViewBag.SearchKey, StringComparison.InvariantCultureIgnoreCase))
						{
						logsByType.Add(log);
						}
					}
				logsByType.Reverse<LogModel.Log>();

				return View(logsByType);
				}
			}
		//// POST: First/Create
		//[HttpPost]
		//public ActionResult Logs(LogModel.Log log)
		//	{
		//	try
		//		{
		//		logModel.logs.Add(log);

		//		return RedirectToAction("Logs");
		//		}
		//	catch
		//		{
		//		return View();
		//		}
		//	}
		}
	}