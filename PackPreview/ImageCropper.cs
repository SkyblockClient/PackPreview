using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackPreview
{
    public class ImageCropper
    {
        public Image<Rgba32> LoadedImage;
        public int GlobalScale;
        public int VanillaWidth;

        public int RelativeScale;
        public int ResizeScale;
        public ImageCropper(Image<Rgba32> loadedImage, int vanillaWidth, int globalScale)
        {
            LoadedImage = loadedImage;
            VanillaWidth = vanillaWidth;
            GlobalScale = globalScale;

            RelativeScale = loadedImage.Width / vanillaWidth;
            ResizeScale = globalScale / RelativeScale;
        }

        public Image<Rgba32> Crop(int x, int y, int width, int height)
        {
            return ImageUtils.CropImage(LoadedImage, x * RelativeScale, y * RelativeScale, width * RelativeScale, height * RelativeScale, ResizeScale);
        }

    }
}
