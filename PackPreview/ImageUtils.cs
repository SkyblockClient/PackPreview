using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace PackPreview
{
    public class ImageUtils
    {
        public static Image<Rgba32> CropImage(Image<Rgba32> img, int x, int y, int width, int height, int scale)
        {
            var cropRectangle = new Rectangle(x, y, width, height);

            var clone = img.Clone(x => x.Crop(cropRectangle));

            var hotbarOptions = BasicSampler();
            hotbarOptions.Size = new Size(clone.Width * scale, clone.Height * scale);
            clone.Mutate(x => x.Resize(hotbarOptions));

            return clone;
        }
        static ResizeOptions BasicSampler()
        {
            var options = new ResizeOptions();
            options.Sampler = KnownResamplers.NearestNeighbor;

            return options;
        }

    }
}
