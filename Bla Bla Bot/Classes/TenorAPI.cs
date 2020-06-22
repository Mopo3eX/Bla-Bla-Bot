using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bla_Bla_Bot.Classes
{
    public partial class TenorAPI
    {
        [JsonProperty("weburl")]
        public Uri Weburl { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("tags")]
        public object[] Tags { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("media")]
        public Dictionary<string, Media>[] Media { get; set; }

        [JsonProperty("created")]
        public double Created { get; set; }

        [JsonProperty("shares")]
        public long Shares { get; set; }

        [JsonProperty("itemurl")]
        public Uri Itemurl { get; set; }

        [JsonProperty("composite")]
        public object Composite { get; set; }

        [JsonProperty("hasaudio")]
        public bool Hasaudio { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class Media
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("dims")]
        public long[] Dims { get; set; }

        [JsonProperty("preview")]
        public Uri Preview { get; set; }

        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public long? Size { get; set; }

        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public double? Duration { get; set; }
    }

}
