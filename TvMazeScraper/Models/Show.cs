using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TvMazeScraper.Models
{
    public class Show:DbEntity
    {
    
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        //[JsonProperty("genres")]
        //public string[] Genres { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("runtime")]
        public long Runtime { get; set; }

        [JsonProperty("premiered")]
        public DateTimeOffset Premiered { get; set; }

        [JsonProperty("officialSite")]
        public string OfficialSite { get; set; }

        //[JsonProperty("schedule")]
        //public Schedule Schedule { get; set; }

        //[JsonProperty("rating")]
        //public Rating Rating { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        //[JsonProperty("network")]
        //public Network Network { get; set; }

        //[JsonProperty("webChannel")]
        //public object WebChannel { get; set; }

        //[JsonProperty("externals")]
        //public Externals Externals { get; set; }

        //[JsonProperty("image")]
        //public Image Image { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("updated")]
        public int Updated { get; set; }

        //[JsonProperty("_links")]
        //public Links Links { get; set; }

        private string 

        [NotMapped]
        [JsonProperty("cast")]
        public virtual ICollection<Cast> Cast { get; set; }
    }
}
