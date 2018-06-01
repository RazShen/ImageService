using ImageServiceWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApplication.Controllers
{
    public class PhotosController : Controller
    {
		
		// GET: Photos
		//public ActionResult Index()
  //      {
  //          return View();
  //      }

		// GET: First/Details
		public ActionResult Photos()
			{
			return View();
			}

		//// GET: First/Edit/5
		//public ActionResult Edit(int id)
		//	{
		//	foreach (Employee emp in employees)
		//		{
		//		if (emp.ID.Equals(id))
		//			{
		//			return View(emp);
		//			}
		//		}
		//	return View("Error");
		//	}

		// POST: First/Edit/5
		//[HttpPost]
		//public ActionResult Edit(int id, Employee empT)
		//	{
		//	try
		//		{
		//		foreach (Employee emp in employees)
		//			{
		//			if (emp.ID.Equals(id))
		//				{
		//				emp.copy(empT);
		//				return RedirectToAction("ImageWeb");
		//				}
		//			}

		//		return RedirectToAction("ImageWeb");
		//		}
		//	catch
		//		{
		//		return RedirectToAction("Error");
		//		}
		//	}

		//// GET: First/Delete/5
		//public ActionResult Delete(int id)
		//	{
		//	int i = 0;
		//	foreach (Employee emp in employees)
		//		{
		//		if (emp.ID.Equals(id))
		//			{
		//			employees.RemoveAt(i);
		//			return RedirectToAction("Details");
		//			}
		//		i++;
		//		}
		//	return RedirectToAction("Error");
		//	}
		}
}