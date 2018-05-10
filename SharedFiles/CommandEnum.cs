using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SharedFiles
{
	/// <summary>
	/// enum represents the type of the command
	/// </summary>
    public enum CommandEnum : int
    {
        NewFileCommand,
        GetConfigCommand,
		LogCommand,
		CloseHandlerCommand,
		NewLogMessage,
        CloseClient
    }
}
