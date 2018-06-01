using SharedFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceWebApplication.Client
	{
	/// <summary>
	/// Delegate of update command (settings and log models has this delegate which is assigned to update event.
	/// </summary>
	/// <param name="args"></param>
	public delegate void Updator(CommandRecievedEventArgs args);

	interface IClient
		{
		/// <summary>
		/// if client is running
		/// </summary>
		/// <returns></returns>
		bool Running();
		/// <summary>
		/// close the tcp connection
		/// </summary>
		void Close();
		/// <summary>
		/// Write command to the server
		/// </summary>
		/// <param name="argsForCommand"> inputted args to write</param>
		void WriteCommandToServer(CommandRecievedEventArgs argsForCommand);
		/// <summary>
		/// Constantly read from the server
		/// </summary>
		void UpdateConstantly();
		/// <summary>
		/// Update event.
		/// </summary>
		event Updator UpdateEvent;
		/// <summary>
		/// Returns a string if running
		/// </summary>
		/// <returns></returns>
        String RunningToString();
		}
	}
