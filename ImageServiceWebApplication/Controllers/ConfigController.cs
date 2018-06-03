
using ImageServiceWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApplication.Controllers
{
    public class ConfigController : Controller
    {
        static ConfigModel cm = new ConfigModel();
        // GET: Config
        public ActionResult Config()
        {

            return View(cm);
        }
    }
}