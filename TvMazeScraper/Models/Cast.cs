using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TvMazeScraper.Models
{
    public class Cast
    {
        [JsonProperty("person")]
        public Person Person { get; set; }

        [JsonProperty("character")]
        public Character Character { get; set; }

        //[JsonProperty("self")]
        //public bool Self { get; set; }

        //[JsonProperty("voice")]
        //public bool Voice { get; set; }
    }

    public class Character
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("image")]
        //public Image Image { get; set; }

        //[JsonProperty("_links")]
        //public Links Links { get; set; }
    }

    //public  class Image
    //{
    //    [JsonProperty("medium")]
    //    public string Medium { get; set; }

    //    [JsonProperty("original")]
    //    public string Original { get; set; }
    //}

    //public  class Links
    //{
    //    [JsonProperty("self")]
    //    public Self Self { get; set; }
    //}

    //public  class Self
    //{
    //    [JsonProperty("href")]
    //    public string Href { get; set; }
    //}

    public  class Person
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("birthday")]
        public DateTime? Birthday { get; set; }

        //[JsonProperty("deathday")]
        //public DateTime? Deathday { get; set; }

        //[JsonProperty("gender")]
        //public string Gender { get; set; }

        //[JsonProperty("image")]
        //public Image Image { get; set; }

        //[JsonProperty("_links")]
        //public Links Links { get; set; }
    }

    public class Country
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

}
