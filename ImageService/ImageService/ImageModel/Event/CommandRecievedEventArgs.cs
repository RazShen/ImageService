using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageService.Modal
{
	/// <summary>
	/// arguments for a command object
	/// </summary>
    public class CommandRecievedEventArgs : EventArgs
    {
        public int CommandID { get; set; }      // The Command ID
        public string[] Args { get; set; }
        public string RequestDirPath { get; set; }  // The Request Directory

        /// <summary>
        /// CommandRecievedEventArgs constractor
        /// </summary>
        /// <param name="id"> command ID</param>
        /// <param name="args">arguments for the command</param>
        /// <param name="path">path of the file the relevent to the command</param>
        public CommandRecievedEventArgs(int id, string[] args, string path)
        {
            CommandID = id;
            Args = args;
            RequestDirPath = path;
        }
    }
}
