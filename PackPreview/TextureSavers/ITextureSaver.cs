using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace PackPreview.ImageSavers
{
    public interface ITextureSaver
    {
        public void SaveTexture(Image<Rgba32> image, string name);
    }
}