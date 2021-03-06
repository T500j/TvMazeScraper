﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TvMazeScraper.Data;
using TvMazeScraper.Scraper;

namespace TvMazeScraper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).
                AddJsonOptions(options => 
                {
                    options.SerializerSettings.DateFormatString ="yyyy-MM-dd";
                }
                );

            services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DataContext"))
            );

            services.Configure<ScraperSettings>(options => Configuration.GetSection("ScraperSettings").Bind(options));


            services.AddHostedService<ScraperHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            

            app.UseHttpsRedirection();
            app.UseMvc();

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<DataContext>();

                if (env.IsDevelopment())
                {
                    //dbContext.Database.EnsureDeleted();
                    dbContext.Database.EnsureCreated();
                }
                else
                {
                    //TODO add migration to the project  check for pending migrations and execute if needed etc etc
                    throw new NotSupportedException("Only development is supported at the moment!");
                }
            }
        }
    }
}
