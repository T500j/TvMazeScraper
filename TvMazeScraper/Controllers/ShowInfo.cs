using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMazeScraper.Models;

namespace TvMazeScraper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowInfo : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Show>> Get(int id, int page)
        {
            throw new NotImplementedException();
        }
    }
}
