using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Verbs
{
    [Verb("view", HelpText = "Displays remaining downloads and time left to download file")]
    public class ViewOptions
    {
        [Value(0, MetaName = "filename", HelpText = "The file to be viewed", Required = true)]
        public string FileName { get; set; }

        [Value(1, MetaName = "password", HelpText = "Password for the file", Required = true)]
        public string Password { get; set; } 

        public static int ExecuteViewAndReturnExitCode(ViewOptions options)
        {
            Console.WriteLine(options.FileName);
            string success = Api.Api.View(options.FileName, options.Password);
            Console.WriteLine(success);
            return 0;
        }
    }
}
