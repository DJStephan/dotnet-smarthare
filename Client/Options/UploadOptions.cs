using System;
using System.IO;
using Client.Utils;
using CommandLine;


namespace Client.Verbs
{
    [Verb("upload", HelpText = "Uploads a file")]
    public class UploadOptions
    {
        [Value(0, MetaName = "filename", HelpText = "The file to be uploaded", Required = true)]
        public string FileName { get; set; }

        [Value(1, MetaName = "password", HelpText = "Password for the file", Required = false)]
        public string Password { get; set; } = PasswordGenerator.Generate();

        [Value(2, MetaName = "expiration", HelpText = "Number of minutes file will be available to download", Required = false)]
        public double Expiration { get; set; } = 60;

        [Value(3, MetaName = "maxDownloads", HelpText = "Maximum number of times file can be downloaded", Required = false)]
        public int MaxDownloads { get; set; } = -1;//if left @-1 there will be no cap on downloads
        
        public static int ExecuteUploadAndReturnExitCode(UploadOptions options)
        {
            
            var file = new FileInfo(options.FileName);
            if (!file.Exists)
            {
                return 10;
            }
            Console.WriteLine($"Uploading {file.FullName}");
            Console.WriteLine($"Password: {options.Password}");
            var success = Api.Api.Upload(file.FullName, options.Password, options.Expiration, options.MaxDownloads);
            Console.WriteLine(success);
            return 0;
        }
    }
}