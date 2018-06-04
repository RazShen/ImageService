using ImageServiceWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApplication.Controllers
{
    public class PhotosController : Controller
    {
		private PhotosModel photosModel;

		public ActionResult Photos()
			{
			this.photosModel = PhotosModel.Instance;
			return View(this.photosModel.photosList);
			}
		}
}