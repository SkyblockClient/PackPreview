using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace PackPreview.ImageLoaders
{
    public interface ITextureLoader
    {
        public Image<Rgba32> LoadTexture(params string[] locations);
    }
}