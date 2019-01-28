using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace CsharpAssessmentSmartShare
{
    public class SmartShareContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=127.0.0.1;port=5432;database=smartShare;userid=postgres;password=password");
        }
    }

}