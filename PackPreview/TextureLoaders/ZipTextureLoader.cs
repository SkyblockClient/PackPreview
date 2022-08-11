using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackPreview.ImageLoaders
{
    internal class ZipTextureLoader : ITextureLoader
    {
        public string PackName {
            get => _packName; 
            set => _packName = value.EndsWith(".zip") ? value : value + ".zip";
        } string _packName;

        ZipArchive Texturepack;
        ZipArchive BaseTexturepack;

        public ZipTextureLoader(string packname)
        {
            this.PackName = packname;

            this.Texturepack = ZipFile.OpenRead(PackName);
            this.BaseTexturepack = ZipFile.OpenRead("base.zip");
        }

        public Image<Rgba32> LoadTexture(params string[] locations)
        {
            foreach (var location in locations)
            {
                var entry = Texturepack.GetEntry(location);
                if (entry is not null)
                {
                    return Image.Load<Rgba32>(entry.Open());
                }
            }

            foreach (var location in locations)
            {
                var entry = BaseTexturepack.GetEntry(location);
                if (entry is not null)
                {
                    return Image.Load<Rgba32>(entry.Open());
                }
            }

            return Image.Load<Rgba32>(Path.Combine("base_pack", locations.First()));
        }
    }
}
