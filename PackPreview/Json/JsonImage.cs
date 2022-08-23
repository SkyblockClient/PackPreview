using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackPreview.Json
{
    public class JsonImage
    {

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("width")]
        public int VanillaWidth { get; set; }

        [JsonProperty("texture")]
        public string Texture { get; set; }
    }
}
