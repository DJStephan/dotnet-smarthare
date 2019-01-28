using System;
using System.Collections.Generic;
using Client.Verbs;
using CommandLine;
using static Client.Verbs.DownloadOptions;
using static Client.Verbs.UploadOptions;
using static Client.Verbs.ViewOptions;

namespace Client
{
    public class Program
    {
        public static int Main(string[] args)
        {
           return Parser.Default.ParseArguments<DownloadOptions, UploadOptions, ViewOptions>(args)
                .MapResult(
                    (DownloadOptions opts) => ExecuteDownloadAndReturnExitCode(opts),
                    (UploadOptions opts) => ExecuteUploadAndReturnExitCode(opts),
                    (ViewOptions opts) => ExecuteViewAndReturnExitCode(opts),
                    errs => 1);
            //var runCount = 1;

            //// run a bunch of different commandline args through a simulated Main entry point
            //foreach (var args in new List<string[]>
            //{
            //    //experiment with different command line values here
            //    //new[] {"download", "test.txt", "password"}, 
            //    //new[] {"-h"},
            //    //new[] {"--version"},
            //    //new[] {"upload"},
            //    //new[] {"upload", "Program.cs"},
            //    new[] {"upload", "Program.cs", "password123"},
            //    //new[] {"download"},
            //    //new[] {"download", "Program.cs"},
            //    //new[] {"download", "Program.cs", "p@ssw0rd"},
            //    //new[] {"download", "Program.cs", "password123"}
            //})
            //{
            //    Console.WriteLine("*** RUN #{0}, args: {1}", runCount++, String.Join(" ", args));
            //    __YourMain(args);
            //    Console.WriteLine("");
            //}
        }

        public static void __YourMain(string[] args)
        {
            Parser.Default.ParseArguments<DownloadOptions, UploadOptions>(args)
                .MapResult(
                    (DownloadOptions opts) => ExecuteDownloadAndReturnExitCode(opts),
                    (UploadOptions opts) => ExecuteUploadAndReturnExitCode(opts),
                    errs => 1);
        }
    }
}