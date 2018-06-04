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
		private PhotosContainer photosContainer;
		private static PhotosModel _instance;
		public List<Photo> photosList;
		public Boolean exists;
		/// <summary>
		/// Constructor of the photo model
		/// </summary>
		private PhotosModel()
			{
			this.photosContainer = new PhotosContainer();
			photosList = this.photosContainer.PhotosList;
			exists = true;
			}
		/// <summary>
		/// Get instance of the photo model (singelton)
		/// </summary>
		public static PhotosModel Instance
			{
			get
				{
				if (_instance != null)
					{
					return _instance;
					}
				_instance = new PhotosModel();
				return _instance;
				}
			}


		/// <summary>
		/// Initialize the photo model
		/// </summary>
		public void Initialize()
			{
			this.photosContainer = new PhotosContainer();
			this.photosList = this.photosContainer.PhotosList;
			}

		public class Photo
			{
			public int CreationYear { get; set; }
			public int CreationMonth { get; set; }
			public String Name { get; set; }
			public String RelPath { get; set; }
			public String RealRelPath { get; set; }
			public String Path { get; set; }
			public String ThumbnailPath { get; set; }
			}
		public class PhotosContainer
			{
			private string m_outputDir;
			public List<Photo> PhotosList = new List<Photo>();
			/// <summary>
			/// Photo container constructor.
			/// </summary>
			public PhotosContainer()
				{
				// set up Initialize from output folder
				this.m_outputDir = Client.Configurations.Instance.OutputDirectory;
				this.Initialize();
				}

			/// <summary>
			/// Initialize the photos container with information about the photos
			/// </summary>
			private void Initialize()
				{
				try
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
									try
										{
										PhotosList.Add(new Photo { Name = fileInfo.Name, CreationMonth = Int32.Parse(monthDirInfo.Name), CreationYear = Int32.Parse(yearDirInfo.Name),
											RelPath = "~/Uploads/" + yearDirInfo.Parent.Parent.Name + "/" + yearDirInfo.Parent.Name + "/" + yearDirInfo.Name + "/" + monthDirInfo.Name + "/" + fileInfo.Name,
											RealRelPath = "~/Uploads/" + yearDirInfo.Parent.Parent.Name + "/" + yearDirInfo.Name + "/" + monthDirInfo.Name + "/" + fileInfo.Name,
											Path = fileInfo.FullName.Replace(@"Thumbnails\", String.Empty),
											ThumbnailPath = fileInfo.FullName
											});
										}
									catch (Exception e)
										{ // couldn't read data for photo 

										}
									}
								}
							}
						}
					} catch (Exception e)
					{
					// something went wrong initializing the photos
					}
				}
			}
		}
	}