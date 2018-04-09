using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
namespace ImageService.Commands
{
	/// <summary>
	/// 
	/// </summary>
    public class NewFileCommand : ICommand
    {

        private IImageServiceModal modal;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="newModal"></param>
        public NewFileCommand(IImageServiceModal newModal)
        {
            modal = newModal;            // Storing the Modal
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		/// <param name="result"></param>
		/// <returns></returns>
        public string Execute(string[] args, out bool result)
        {
            return modal.AddFile(args, out result);
            // The String Will Return the New Path if result = true, and will return the error message
        }
    }
}
