using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TvMazeScraper.Data;
using TvMazeScraper.Models;

namespace TvMazeScraper.Scraper
{
    public class ScraperHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ScraperSettings _settings;
        private HttpClient _client { get; set; }
        private const int idsPerPage = 250;

        public ScraperHostedService(IServiceScopeFactory scopeFactory, IOptions<ScraperSettings> settings)
        {
            _scopeFactory = scopeFactory;
            _settings = settings.Value;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_settings.EndPoint);


        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var page = GetLastPage();
            while(!stoppingToken.IsCancellationRequested)
            {
                page = await UpdateShows(page);
            }
        }

        private int GetLastPage()
        {
            using (var context = _scopeFactory.CreateScope().ServiceProvider.GetService<DataContext>())
            {
                return (context.Set<Show>().Max(s => s.Id))/idsPerPage;
            }
        }

        private async Task<int> UpdateShows(int page)
        {

            var response = await _client.GetAsync($"shows?page={page}");
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound) //end of list
                    return 0;
                else if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    //try same page again later (5 seconds)
                    Thread.Sleep(5000);
                    return page;
                }
                else //other error (possible connection error)
                {
                    //try same page again later (15 seconds)
                    Thread.Sleep(15000);
                    return page;
                }
            }
            else
            {

                var shows = await response.Content.ReadAsAsync<IEnumerable<Show>>();
                int lastId = (page * idsPerPage) - 1;
                int maxId = lastId + idsPerPage + 1;

                foreach (var show in shows)
                {
                    Console.WriteLine($"show id: {show.Id}");
                    var cast = await GetCast(show.Id);
                    show.Cast = cast;
                    using (var context = _scopeFactory.CreateScope().ServiceProvider.GetService<DataContext>())
                    {
                        //delete old entries:
                        context.Set<Show>().RemoveRange(context.Set<Show>().Where(s => s.Id > lastId && s.Id <= show.Id));
                        context.SaveChanges();
                        context.Set<Show>().Add(show);
                        context.SaveChanges();
                        lastId = show.Id;
                    }
                    //try and keep under the rate limit:
                    Thread.Sleep(1000 / _settings.CallsPerSecond);
                }
                if (lastId < maxId)
                {
                    using (var context = _scopeFactory.CreateScope().ServiceProvider.GetService<DataContext>())
                    {
                        //delete remaining entries:
                        context.Set<Show>().RemoveRange(context.Set<Show>().Where(s => s.Id > lastId && s.Id <= maxId));
                        context.SaveChanges();

                    }
                }
                //try and keep under the rate limit:
                Thread.Sleep(1000 / _settings.CallsPerSecond);

                return page + 1;
            }
        }

        private async Task<List<Cast>> GetCast(int showId)
        {
            var response = await _client.GetAsync($"shows/{showId}/cast");
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    //try same page again later (5 seconds)
                    Thread.Sleep(5000);
                    return await GetCast(showId);
                }
                else //other error (possible connection error)
                {
                    //try same page again later (15 seconds)
                    Thread.Sleep(15000);
                    return await GetCast(showId); 
                }
            }
            else //rate limit or connection error
            {
                var cast = await response.Content.ReadAsAsync<List<Cast>>();
                return cast;
            }
        }
    }
}
