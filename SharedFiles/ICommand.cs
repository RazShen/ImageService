using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SharedFiles
{
	/// <summary>
	/// ICommand interface.
	/// </summary>
	public interface ICommand
    {
		/// <summary>
		/// Execute this command by arguments and result to update.
		/// </summary>
		/// <param name="args"> arguments for command </param>
		/// <param name="result"> result of the run </param>
		/// <returns></returns>
        string Execute(string[] args, out bool result);          // The Function That will Execute The 
    }
}
