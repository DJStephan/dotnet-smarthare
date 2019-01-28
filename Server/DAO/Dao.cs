using System;
using System.Collections.Generic;
using System.Text;
using Client.Dtos;
using CsharpAssessmentSmartShare;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Server.Models;



namespace Server.DAO
{
    public class Dao
    {
        public SmartShareContext Db { get; set; }
        public FileDto File { get; set; }

        public Dao(SmartShareContext db, FileDto file)
        {
            this.Db = db;
            this.File = file;
        }

        public ReturnDto Upload()
        {
            var query = from f in Db.Files
                            where f.Filename == this.File.Filename
                            select f;
            var dbFile = query.SingleOrDefault();

            if(dbFile != null)
            {
                return new ReturnDto(false, $"A file with name {this.File.Filename} already exists!");
            }

            FileModel newFile = new FileModel(this.File.Filename,this.File.Password, this.File.Data, this.File.Expiration, this.File.MaxDownloads);
            Db.Add(newFile);
            try
            {
                Db.SaveChanges();
            }
            catch(Exception e)
            {
                if (e is DbUpdateException || e is DbUpdateConcurrencyException)
                {
                    
                    return new ReturnDto(false, e.Message);
                }

                throw;
            }
            return new ReturnDto(true, $"{this.File.Filename} save successfully.");
        }
    }
}
