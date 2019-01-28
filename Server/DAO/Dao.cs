using System;
using System.Collections.Generic;
using System.Text;
using Client.Dtos;
using CsharpAssessmentSmartShare;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Server.Models;
using NpgsqlTypes;

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

        public ReturnDto Download()
        {
            ReturnDto returnDto;
            var query = from f in Db.Files
                        where f.Filename == this.File.Filename
                        select f;
            var dbFile = query.SingleOrDefault();
            DateTime now = DateTime.Now;
   
            if(dbFile != null)
            {
                 if(dbFile.Password.Equals(File.Password))
                {
                    if(now.CompareTo(dbFile.Expiration) > 0)
                    {
                        Db.Files.Remove(dbFile);
                        Db.SaveChanges();
                    }
                    else
                    {
                        returnDto = new ReturnDto(true, "File downloaded successfully", dbFile.Filename, dbFile.Data);
                        dbFile.Downloads += 1;
                        if(dbFile.Downloads == dbFile.MaxDownloads)
                        {
                            Db.Files.Remove(dbFile);
                        }

                        Db.SaveChanges();
                        return returnDto;
                    }
                    
                }
            }

            return returnDto = new ReturnDto(false, "File could not be downloaded.");
        }

        public ReturnDto View()
        {
            ReturnDto returnDto;
            var query = from f in Db.Files
                        where f.Filename == this.File.Filename
                        select f;
            var dbFile = query.SingleOrDefault();
            Console.WriteLine(dbFile);
            Console.WriteLine(dbFile.Password.Equals(File.Password));

            if(dbFile != null && dbFile.Password.Equals(File.Password))
            {
                DateTime now = DateTime.Now;
                var timeLeft = dbFile.Expiration.Subtract(now).TotalMinutes;
                if(timeLeft > 0)
                {
                    var downloadsLeft = dbFile.MaxDownloads == -1 ? "unlimited" : (dbFile.MaxDownloads - dbFile.Downloads).ToString();
                    return returnDto = new ReturnDto(true, $"{File.Filename} can be downloaded {downloadsLeft} times in the next {timeLeft} minutes.");
                }
                else
                {
                    Db.Files.Remove(dbFile);
                    Db.SaveChanges();
                }
            }

            return returnDto = new ReturnDto(false, "File could not be viewed");
        }
    }
}
