using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageService.Infrastructure.Enums
{
	/// <summary>
	/// enum represents the type of the command
	/// </summary>
    public enum CommandEnum : int
    {
        NewFileCommand,
        CloseCommand
    }
}
