using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackPreview.ImageSavers
{
    internal class DirectoryTextureSaver : ITextureSaver
    {
        public string DirectoryName;

        public DirectoryTextureSaver(string dirname)
        {
            DirectoryName = dirname;
        }

        public void SaveTexture(Image<Rgba32> image, string name)
        {
            image.SaveAsPng(Path.Combine(DirectoryName, name));
        }
    }
}
