using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace ImageService.Modal
{
	/// <summary>
	///  image service modal class
	/// </summary>
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
		public int x;
        public string OutputFolder { get; set; }            // The Output Folder
        public int ThumbnailSize { get; set; }
        #endregion

        /// <summary>
        ///  this function adds a file to the direcctory 
        /// </summary>
        /// <param name="args">The Path of the Image from the file</param>
        /// <param name="result"> out variable that will set to true if the file added succesfully and false otherwise</param>
        /// <returns> a string represent the result of the action</returns>
        public string AddFile(string[] args, out bool result)
        {
            string path = args[0] + "\\" + args[1];
            try
            {
                if(File.Exists(path)) {
					String year = args[2];
					String month = args[3];
                    //create output directory and make it hidden.
					DirectoryInfo outputDir = Directory.CreateDirectory(OutputFolder);
                    outputDir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
					Directory.CreateDirectory(OutputFolder + "\\" + year);
					Directory.CreateDirectory(OutputFolder + "\\" + year + "\\" + month);
					Directory.CreateDirectory(OutputFolder + "\\" + "Thumbnails");
					Directory.CreateDirectory(OutputFolder + "\\" + "Thumbnails" + "\\" + year);
					Directory.CreateDirectory(OutputFolder + "\\" + "Thumbnails" + "\\" + year + "\\" + month);
					string finalOutputPath = OutputFolder + "\\" + year + "\\" + month + "\\";
					string finalOutputTPath = OutputFolder + "\\" + "Thumbnails" + "\\" + year + "\\" + month + "\\";
					int counter = 0;
					String fileName = Path.GetFileName(path);                         
					while (File.Exists(finalOutputTPath + fileName)) {
						counter++;
						fileName = Path.GetFileNameWithoutExtension(path) + "(" + counter.ToString() + ")" + Path.GetExtension(path);
					}
					Image thumbnail;
					using (thumbnail = Image.FromFile(path))
					{
						thumbnail = (Image)(new Bitmap(thumbnail, new Size(this.ThumbnailSize, this.ThumbnailSize)));
						thumbnail.Save(finalOutputTPath + fileName);
					}
					counter = 0;
					fileName = Path.GetFileName(path); 
					while (File.Exists(finalOutputPath + fileName))
					{
						counter++;
						fileName = Path.GetFileNameWithoutExtension(path) + "(" + counter.ToString() + ")" + Path.GetExtension(path);
					}
					File.Move(path, finalOutputPath + fileName);    

					result = true;
					return "The file: " + Path.GetFileName(path) + " is now added to " + finalOutputPath +
						" and also to the thumbnails folder";
				} else {
						result = false;
						return "path is not exist";
				}
			} catch (Exception ex) {
				result = false;
				return ex.ToString() + path;
			}
        }
    }
}
