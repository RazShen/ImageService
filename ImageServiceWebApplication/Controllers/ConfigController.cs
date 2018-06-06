
using ImageServiceWebApplication.Models;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApplication.Controllers
	{
	public class ConfigController : Controller
		{
		public static ConfigModel cm = new ConfigModel();
		private static string handlerToDelete;

		// GET: Config
		public ActionResult Config()
			{

			return View(cm);
			}

		public ActionResult DeleteHandler(string handler)
			{
			handlerToDelete = handler;
			return View("Confirmation");

			}

		public ActionResult DeleteOK()
			{
			cm.DeleteHandler(handlerToDelete);
			System.Threading.Thread.Sleep(1000);
			return RedirectToAction("Config");
			}

		public ActionResult DeleteCancel()
			{
			return RedirectToAction("Config");
			}
		}
	}