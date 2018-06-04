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

		public ActionResult PhotoView(string photoView)
			{

			this.photosModel = PhotosModel.Instance;
			foreach (PhotosModel.Photo photo in this.photosModel.photosList)
				{
				if (photo.RealRelPath == photoView)
					{
					return View(photo);
					}
				}
			return View();
			}

		public ActionResult ReturnFromDelete(string photoView)
			{

			// delete the photo
			this.photosModel = PhotosModel.Instance;
			foreach (PhotosModel.Photo photo in this.photosModel.photosList)
				{
				if (photo.RealRelPath == photoView)
					{
					try
						{
						string thumbnailToDelete = photo.ThumbnailPath;
						string pathToDelete = photo.Path;
						if (System.IO.File.Exists(pathToDelete)) { int x = 5; }
						System.IO.File.Delete(thumbnailToDelete);
						System.IO.File.Delete(pathToDelete);
						this.photosModel.photosList.Remove(photo);
						}
					catch (Exception e) { }
					return RedirectToAction("Photos");
					}
				}
			return RedirectToAction("Photos");
			}		

		public ActionResult DeleteImage(string photoView)
			{
			this.photosModel = PhotosModel.Instance;
			foreach (PhotosModel.Photo photo in this.photosModel.photosList)
				{
				if (photo.RealRelPath == photoView)
					{
					return View(photo);
					}
				}
			return View();
			}
		
		}
}