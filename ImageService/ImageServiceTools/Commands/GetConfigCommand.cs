
using ImageServiceTools.Modal;
using Newtonsoft.Json;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageServiceTools.Commands
{
    /// <summary>
    /// get configuration command class
    /// </summary>
    class GetConfigCommand : ICommand
    {
        static class Constants
        {
            public const int size = 5;
        }
        /// <summary>
        /// This function executes get configuration command  
        /// </summary>
        /// <param name="args">given args array</param>
        /// <param name="result"> indicate if the execute succeed</param>
        /// <returns></returns>
        public string Execute(string[] args, out bool result)
        {
            try
            {
                result = true;
                string[] arr = new string[Constants.size];
                arr[0] = ConfigurationManager.AppSettings.Get("OutputDir");
                arr[1] = ConfigurationManager.AppSettings.Get("SourceName");
                arr[2] = ConfigurationManager.AppSettings.Get("LogName");
                arr[3] = ConfigurationManager.AppSettings.Get("ThumbnailSize");
                arr[4] = ConfigurationManager.AppSettings.Get("Handler");
                CommandRecievedEventArgs commandSendArgs = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, arr, "");
                return JsonConvert.SerializeObject(commandSendArgs);
            }
            catch (Exception ex)
            {
                result = false;
                return ex.ToString();
            }
        }
    }
}
