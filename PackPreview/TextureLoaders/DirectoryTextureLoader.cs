using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackPreview.ImageLoaders
{
    internal class DirectoryTextureLoader : ITextureLoader
    {
        public string PackName;

        public DirectoryTextureLoader(string packname)
        {
            PackName = packname;
        }

        public Image<Rgba32> LoadTexture(params string[] locations)
        {
            foreach (var location in locations)
            {
                var path = Path.Combine(PackName, location);
                if (File.Exists(path))
                {
                    return Image.Load<Rgba32>(path);
                }
            }

            foreach (var location in locations)
            {
                var path = Path.Combine("base_pack", location);
                if (File.Exists(path))
                {
                    return Image.Load<Rgba32>(path);
                }
            }

            return Image.Load<Rgba32>(Path.Combine("base_pack", locations.First()));
        }
    }
}
