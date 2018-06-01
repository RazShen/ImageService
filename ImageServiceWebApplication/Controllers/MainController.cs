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
        
        // GET: First
        public ActionResult ImageWeb()
        {
			List<ImageWebModel.Student> studentsList = ImageWebModel.LoadText();
			ImageWebModel iWM = new ImageWebModel();
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

        //[HttpGet]
        //public ActionResult AjaxView()
        //{
        //    return View();
        //}


		//[HttpGet]
  //      public JObject GetEmployee()
  //      {
  //          JObject data = new JObject();
  //          data["FirstName"] = "Kuky";
  //          data["LastName"] = "Mopy";
  //          return data;
  //      }

  //      [HttpPost]
  //      public JObject GetEmployee(string name, int salary)
  //      {
  //          foreach (var empl in employees)
  //          {
  //              if (empl.Salary > salary || name.Equals(name))
  //              {
  //                  JObject data = new JObject();
  //                  data["FirstName"] = empl.FirstName;
  //                  data["LastName"] = empl.LastName;
  //                  data["Salary"] = empl.Salary;
  //                  return data;
  //              }
  //          }
  //          return null;
  //      }



        

        
    }
}
