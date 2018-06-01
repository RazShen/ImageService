using ImageServiceWebApplication.Models;
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
			logModel = new LogModel();
			return View(logModel.logs);
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