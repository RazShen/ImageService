using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SharedFiles
	{
    /// <summary>
    /// Argments for the DirectoryCloseEvent 
    /// </summary>
    public class DirectoryCloseEventArgs : EventArgs
    {
        public string DirectoryPath { get; set; }
        public string Message { get; set; }             // The Message That goes to the logger

        /// <summary>
        /// DirectoryCloseEventArgs constractor.
        /// </summary>
        /// <param name="dirPath"> te path of the directory</param>
        /// <param name="message">the messege to send</param>
        public DirectoryCloseEventArgs(string dirPath, string message)
        {
            DirectoryPath = dirPath;                    // Setting the Directory Name
            Message = message;                          // Storing the String
        }
    }
}
