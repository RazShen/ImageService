using ImageServiceTools.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageServiceTools.Controller
{
	/// <summary>
	/// IImageController interface, controller that takes care of images.
	/// </summary>
	public interface IImageController
    {
		/// <summary>
		/// Execute this command by arguments and result to update.
		/// </summary>
		/// <param name="args"> arguments for command </param>
		/// <param name="result"> result of the run </param>
		/// <returns></returns>
		string ExecuteCommand(int commandID, string[] args, out bool result);          // Executing the Command Request
		ImageServer ImageServer { get; set; }
    }
}
