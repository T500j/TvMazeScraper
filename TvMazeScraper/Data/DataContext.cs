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
            base.OnModelCreating(modelBuilder);
            //add all models of Type DbEntity

            var entityMethod = typeof(ModelBuilder).GetMethod("Entity",
                new Type[] { });
            var entityTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.BaseType == typeof(DbEntity));
            foreach(var type in entityTypes)
            {
                
                entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object []{ });
            }

        }
    }
}
