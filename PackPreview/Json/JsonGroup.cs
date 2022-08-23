using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PackPreview.Json
{
    public class JsonGroup
    {
        [JsonProperty("alias")]
        public string Alias { get; set; }


        [JsonProperty("output")]
        public string Output { get; set; }
        
        [JsonIgnore]
        public bool HasOutput => Output != null;


        [JsonProperty("width")]
        public int Width { get; set; }


        [JsonProperty("height")]
        public int Height { get; set; }


        [JsonProperty("texture")]
        public string Texture { get; set; }
        [JsonIgnore]
        public bool HasTexture => Texture != null;


        [JsonProperty("cropX")]
        public int CropX { get; set; }

        [JsonProperty("cropY")]
        public int CropY { get; set; }


        [JsonProperty("offsetX")]
        public int OffsetX { get; set; }

        [JsonProperty("offsetY")]
        public int OffsetY { get; set; }


        [JsonProperty("children")]
        public JsonGroup[] Children { get; set; } = new JsonGroup[0];


    }
}
