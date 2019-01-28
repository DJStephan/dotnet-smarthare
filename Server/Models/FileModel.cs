using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Server.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public byte[] Data { get; set; }
        public DateTime Expiration { get; set; }
        public int MaxDownloads { get; set; }
        public int Downloads { get; set; } = 0;
        public string Password { get; set; }

        public FileModel() { }

        public FileModel(string filename, string password, byte[] data, DateTime expiration, int maxDownloads)
        {
            this.Filename = filename;
            this.Data = data;
            this.Expiration = expiration;
            this.MaxDownloads = maxDownloads;
            this.Password = password;
        }
    }
}
