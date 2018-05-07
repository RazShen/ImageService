using ImageServiceTools.Commands;
using ImageServiceTools.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedFiles;


namespace ImageServiceTools.Controller
{
	/// <summary>
	/// ImageController, controller that takes care of images.
	/// </summary>
	public class ImageController : IImageController
    {
        private IImageServiceModal imageModal;  // The Modal Object
        private Dictionary<int, ICommand> commands;

		/// <summary>
		/// ImageController constructor from IImageServiceModal (that actually handles the image).
		/// </summary>
		/// <param name="modal"> IImageServiceModal the handles pictures </param>
		public ImageController(IImageServiceModal modal)
        {
            imageModal = modal;                    // Storing the Modal Of The System
            commands = new Dictionary<int, ICommand>() {};
            commands[0] = new NewFileCommand(modal);
			commands[1] = new GetConfigCommand();
			
			}

		/// <summary>
		/// Execute this command by arguments and result to update.
		/// </summary>
		/// <param name="args"> arguments for command </param>
		/// <param name="result"> result of the run </param>
		/// <returns></returns>
		public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {
			// Create a task for the command.
            if (commands.Keys.Contains<int>(commandID)) {
				Task<Tuple<string, bool>> task = new Task<Tuple<string, bool>>(() =>
				{
					// Temporary boolean for this task
					bool resultTemp;
					string response;
					response = this.commands[commandID].Execute(args, out resultTemp);
					// Return the result as tuple.
					return Tuple.Create(response, resultTemp);
				});
				task.Start();
				task.Wait();
				Tuple<string, bool> finalResult = task.Result;
				// return the string result from this command (and also update the boolean)
				resultSuccesful = finalResult.Item2;
				return finalResult.Item1;
            } else {
                resultSuccesful = false;
                return "Command doesn't exist, failed";
            }
        }
    }
}
