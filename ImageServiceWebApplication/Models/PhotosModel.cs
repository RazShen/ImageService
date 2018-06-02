using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageServiceWebApplication.Models
	{
	public class PhotosModel
		{







		public class PhotosContainer
			{
			private string m_outputDir;
			public List<Image> PhotosList = new List<Image>();

			public PhotosContainer()
				{
				// set up Initialize from output folder

				}

			private void Initialize()
				{
				string thumbnailDir = m_outputDir + "\\Thumbnails";
				if (!Directory.Exists(thumbnailDir))
					{
					return;
					}
				DirectoryInfo di = new DirectoryInfo(thumbnailDir);
				//The only file types are relevant.
				string[] validExtensions = { ".jpg", ".png", ".gif", ".bmp" };
				foreach (DirectoryInfo yearDirInfo in di.GetDirectories())
					{

					foreach (DirectoryInfo monthDirInfo in yearDirInfo.GetDirectories())
						{

						foreach (FileInfo fileInfo in monthDirInfo.GetFiles())
							{
							if (validExtensions.Contains(fileInfo.Extension.ToLower()))
								{
								PhotosList.Add(Image.FromFile(fileInfo.FullName));
								}
							}
						}
					}
				}
			}
		}
	}