using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
namespace ImageService.Controller
{
	/// <summary>
	/// 
	/// </summary>
    public interface IImageController
    {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandID"></param>
		/// <param name="args"></param>
		/// <param name="result"></param>
		/// <returns></returns>
        string ExecuteCommand(int commandID, string[] args, out bool result);          // Executing the Command Requet
    }
}
