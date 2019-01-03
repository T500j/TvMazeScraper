using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TvMazeScraper.Models
{
    public abstract class DbEntity
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
