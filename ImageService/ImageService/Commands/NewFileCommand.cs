using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageService.Commands
{
	/// <summary>
	/// New file command.
	/// </summary>
    public class NewFileCommand : ICommand
    {

        private IImageServiceModal modal;

		/// <summary>
		/// NewFileCommand constructor using a modal.
		/// </summary>
		/// <param name="newModal"> image service modal - that actually does the file transfer </param>
        public NewFileCommand(IImageServiceModal newModal)
        {
            modal = newModal;            
        }

		/// <summary>
		/// Execute this command by arguments and result to update.
		/// </summary>
		/// <param name="args"> arguments for command </param>
		/// <param name="result"> result of the run </param>
		/// <returns> updated result string </returns>
		public string Execute(string[] args, out bool result)
        {
            return modal.AddFile(args, out result);
        }
    }
}
