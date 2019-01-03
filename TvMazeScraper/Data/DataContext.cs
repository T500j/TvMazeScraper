using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TvMazeScraper.Models;

namespace TvMazeScraper.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
               
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Show>().Property(s => s._jsonCast).HasColumnName("JsonCast");
            modelBuilder.Entity<Show>().Property(s => s.Id).ValueGeneratedNever();
        }
    }
}
