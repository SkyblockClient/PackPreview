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

            Console.WriteLine("loadedImage.Width / vanillaWidth " + loadedImage.Width / vanillaWidth);
            Console.WriteLine("\tvanillaWidth " + vanillaWidth);
            Console.WriteLine("\tloadedImage.Width " + loadedImage.Width);

            RelativeScale = loadedImage.Width / vanillaWidth;
            Console.WriteLine("globalScale / RelativeScale " + globalScale / RelativeScale);
            Console.WriteLine("\tglobalScale" + globalScale);
            Console.WriteLine("\tRelativeScale" + RelativeScale);
            ResizeScale = globalScale / RelativeScale;
        }

        public Image<Rgba32> Crop(int x, int y, int width, int height)
        {
            Console.WriteLine("x * RelativeScale " + x * RelativeScale);
            Console.WriteLine("y * RelativeScale " + y * RelativeScale);
            Console.WriteLine("width * RelativeScale " + width * RelativeScale);
            Console.WriteLine("height * RelativeScale " + height * RelativeScale);
            Console.WriteLine("ResizeScale " + ResizeScale);

            return ImageUtils.CropImage(LoadedImage, x * RelativeScale, y * RelativeScale, width * RelativeScale, height * RelativeScale, ResizeScale);
        }

    }
}
