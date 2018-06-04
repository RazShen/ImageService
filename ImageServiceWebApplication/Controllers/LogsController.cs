using ImageServiceWebApplication.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApplication.Controllers
{
	/// <summary>
	/// Logs Controller.
	/// </summary>
    public class LogsController : Controller
    {
		public LogModel logModel; 
		/// <summary>
		/// Returns the view of all the logs.
		/// </summary>
		/// <returns></returns>
		// GET: First/Create
		public ActionResult Logs()
			{
			logModel = LogModel.Instance;
			List<LogModel.Log> listOflogs = logModel.GetLogs();
			return View(logModel.logs);
			}

		/// <summary>
		/// Returns view of the logs after search has accured.
		/// </summary>
		/// <param name="form"> of the search</param>
		/// <returns> view </returns>
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
		}
	}