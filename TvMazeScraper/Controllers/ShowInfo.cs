using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMazeScraper.Data;
using TvMazeScraper.Models;

namespace TvMazeScraper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowInfo : ControllerBase
    {
        private readonly DataContext _context;
        private const int ItemsPerPage = 50;

        public ShowInfo(DataContext context)
        {
            _context = context;
        }
        [HttpGet()]
        public ActionResult<IEnumerable<Show>> Get(int page=0)
        {

            var showInfo = _context.Set<Show>().OrderBy(s => s.Id).Skip(ItemsPerPage * page).Take(ItemsPerPage).
                Select(s=> 
                new
                {
                    id = s.Id,
                    name = s.Name,
                    cast = s.Cast.OrderByDescending(c => c.Person.Birthday).Select(c => new
                    {
                        id = c.Person.Id,
                        name = c.Person.Name,
                        birthday = c.Person.Birthday
                    }
                    )
                }
                );

            if (showInfo.Count() == 0)
                return NotFound(showInfo);
            else
                return Ok(showInfo);
        }
    }
}
