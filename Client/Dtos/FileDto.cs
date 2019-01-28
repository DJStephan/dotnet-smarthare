using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Text;

namespace Client.Dtos
{
    [Serializable()]
    public class FileDto
    {
        public string Password { get; set; }
        public byte[] Data { get; set; }
        public DateTime Expiration { get; set; }
        public int MaxDownloads { get; set; }
        public string Filename { get; set; }

        public FileDto() { }

        public FileDto(string path, string password)
        {
            this.Password = password;
            string[] strings = path.Split('/');
            this.Filename = strings[strings.Length - 1];
        }

        public FileDto(string path, string password, double expiration, int maxDownloads)
        {
            this.Password = password;
            this.Data = File.ReadAllBytes(path);
            this.Expiration = DateTime.Now.AddMinutes(expiration);
            this.MaxDownloads = maxDownloads;
            string[] strings = path.Split("\\");
            this.Filename = strings[strings.Length - 1];
        }

        public bool HasData()
        {
            if (this.Data != null)
            {
                return true;
            }

            return false;
        }
        
    }
}
