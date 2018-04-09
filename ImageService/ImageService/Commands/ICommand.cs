//using ImageService.Infrastructure.Enums;
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
    public interface ICommand
    {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		/// <param name="result"></param>
		/// <returns></returns>
        string Execute(string[] args, out bool result);          // The Function That will Execute The 
    }
}
