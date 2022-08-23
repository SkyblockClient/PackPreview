using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackPreview.Json
{
    public class JsonGroupParent
    {
        [JsonProperty("scale")]
        public int Scale { get; set; } = 4;

        [JsonProperty("images")]
        public JsonImage[] Images { get; set; } = new JsonImage[0];

        [JsonProperty("groups")]
        public JsonGroup[] Groups { get; set; } = new JsonGroup[0];
    }
}
