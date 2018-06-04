using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageServiceWebApplication.Models;

namespace ImageServiceWebApplication.Controllers
	{
    public class MainController : Controller
    {
		/// <summary>
		/// Returns the view of the image web
		/// </summary>
		/// <returns> view of the image web</returns>
        // GET: First
        public ActionResult ImageWeb()
        {
			List<ImageWebModel.Student> studentsList = ImageWebModel.LoadText();
			ImageWebModel iWM = ImageWebModel.Instance;
			ViewBag.status = iWM.GetServiceStatus();

			int numOfPics = iWM.GetNumOfPics();
			if (numOfPics == -1)
				{
				ViewBag.numOfPhotos = "Error occurred, Couldn't get the output directory from the service" ;
				} else
				{
				ViewBag.numOfPhotos = iWM.GetNumOfPics();
				}
			return View(studentsList);
        }
    }
}
