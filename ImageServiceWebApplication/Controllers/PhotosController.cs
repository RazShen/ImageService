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

		/// <summary>
		/// Returns all the photos in the thumbnail view.
		/// </summary>
		/// <returns></returns>
		public ActionResult Photos()
			{
			this.photosModel = PhotosModel.Instance;
			this.photosModel.Initialize();
			return View(this.photosModel.photosList);
			}
		
		/// <summary>
		/// Returns a view of spcific photo
		/// </summary>
		/// <param name="photoView"></param>
		/// <returns> view of specific photo</returns>
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

		/// <summary>
		/// Returns the new photos view
		/// </summary>
		/// <param name="photoView"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Delete a specific image (open new view to make sure the user wants to delete)
		/// </summary>
		/// <param name="photoView"></param>
		/// <returns></returns>
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