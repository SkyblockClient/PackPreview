using PackPreview.ImageLoaders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PackPreview
{
    public class PackPreviewGenerator
    {
        const int HOTBAR_BOTTOM_PIXEL = 1;

        const int SCALE = 4;
        private ITextureLoader Loader { get; }
        private Image<Rgba32> Ascii;
        private Dictionary<char, LetterPointer> letterMap;

        public PackPreviewGenerator(ITextureLoader loader)
        {
            this.Loader = loader;
            this.Ascii = Loader.LoadTexture(
                "assets/minecraft/textures/font/ascii.png", 
                "assets/minecraft/mcpatcher/font/ascii.png"
                );
            this.letterMap = LetterMapGenerator.GenerateLetterMapFromAscii(Ascii);
        }

        public Image<Rgba32> GeneratePreview()
        {
            var outputImage = new Image<Rgba32>(182 * SCALE, (59 + HOTBAR_BOTTOM_PIXEL * 2) * SCALE); // create output image of the correct dimensions
            outputImage.Mutate(o => o
                .DrawImage(ButtonGroup(), new Point(0, 0), 1f)
                .DrawImage(HotbarGroup(), new Point(0, (20 + 1) * SCALE), 1f)
            );
            return outputImage;
        }

        Image LetterGroup(string letters)
        {
            var fontScale = 1;
            var scale = SCALE;
            var letterSize = new Size(8, 8);



            fontScale = Ascii.Width / 128;

            scale = SCALE / fontScale;

            letterSize = new Size(8 * fontScale, 8 * fontScale);

            var expectedWidths = new int[letters.Length];
            var charDistance = (1 * fontScale * scale);

            for (int i = 0; i < letters.Length; i++)
            {
                var pointer = this.letterMap[letters[i]];
                expectedWidths[i] = (pointer.width * scale) + charDistance;
            }

            var textPng = new Image<Rgba32>(expectedWidths.Sum() - charDistance, letterSize.Height * scale);

            var currentWidth = 0;

            for (int i = 0; i < letters.Length; i++)
            {
                var pointer = letterMap[letters[i]];
                using var letterPng = ImageUtils.CropImage(this.Ascii, pointer.location.X * fontScale, pointer.location.Y * fontScale, letterSize.Width, letterSize.Height, scale);

                textPng.Mutate(x => x
                    .DrawImage(letterPng, new Point(currentWidth, 0), 1.0F)
                );

                currentWidth += expectedWidths[i];
            }

            textPng.SaveAsPng("output/text.png");

            return textPng;
        }
        Image ButtonGroup()
        {
            var scale = SCALE;
            var outputDimensions = new Size(182 * scale, 20 * scale);
            var buttonWidth = 90;

            var buttonGroupPng = new Image<Rgba32>(outputDimensions.Width, outputDimensions.Height);

            using var weak = ButtonWeakGroup();
            using var strong = ButtonStrongGroup();

            buttonGroupPng.Mutate(x => x
                .DrawImage(weak, new Point(0, 0), 1.0F)
                .DrawImage(strong, new Point(outputDimensions.Width - buttonWidth * scale, 0), 1.0F)
            );

            buttonGroupPng.SaveAsPng("output/buttongroup.png");

            return buttonGroupPng;
        }

        Image ButtonStrongGroup()
        {
            var scale = SCALE;
            var buttonWidth = 90;
            var totalButtonWidth = 200;
            var outputDimensions = new Size(buttonWidth * scale, 20 * scale);

            var buttonStrongPng = new Image<Rgba32>(outputDimensions.Width, outputDimensions.Height);

            using var widgetsPng = Loader.LoadTexture("assets/minecraft/textures/gui/widgets.png");
            using var leftHalf = ImageUtils.CropImage(widgetsPng, 0, 86, buttonWidth / 2, 20, scale);
            using var rightHalf = ImageUtils.CropImage(widgetsPng, totalButtonWidth - buttonWidth / 2, 86, buttonWidth / 2, 20, scale);

            buttonStrongPng.Mutate(x => x
                .DrawImage(leftHalf, new Point(0, 0), 1.0F)
                .DrawImage(rightHalf, new Point(buttonWidth / 2 * scale, 0), 1.0F));

            using var text = LetterGroup("Hover");

            var x = buttonStrongPng.Width / 2 - text.Width / 2;
            var y = buttonStrongPng.Height / 2 - text.Height / 2;

            buttonStrongPng.Mutate(o => o
                .DrawImage(text, new Point(x, y), 1.0F)
            );

            buttonStrongPng.SaveAsPng("output/buttonstrong.png");

            return buttonStrongPng;
        }
        Image ButtonWeakGroup()
        {
            var scale = SCALE;
            var buttonWidth = 90;
            var totalButtonWidth = 200;
            var outputDimensions = new Size(buttonWidth * scale, 20 * scale);

            var buttonWeakPng = new Image<Rgba32>(outputDimensions.Width, outputDimensions.Height);

            using var widgetsPng = Loader.LoadTexture("assets/minecraft/textures/gui/widgets.png");
            using var leftHalf = ImageUtils.CropImage(widgetsPng, 0, 66, buttonWidth / 2, 20, scale);
            using var rightHalf = ImageUtils.CropImage(widgetsPng, totalButtonWidth - buttonWidth / 2, 66, buttonWidth / 2, 20, scale);

            buttonWeakPng.Mutate(x => x
                .DrawImage(leftHalf, new Point(0, 0), 1.0F)
                .DrawImage(rightHalf, new Point(buttonWidth / 2 * scale, 0), 1.0F));

            using var text = LetterGroup("Button");

            var x = buttonWeakPng.Width / 2 - text.Width / 2;
            var y = buttonWeakPng.Height / 2 - text.Height / 2;

            buttonWeakPng.Mutate(o => o
                .DrawImage(text, new Point(x, y), 1.0F)
            );

            buttonWeakPng.SaveAsPng("output/buttonweak.png");

            return buttonWeakPng;
        }
        Image HotbarGroup()
        {
            var outputDimensions = new Size(182 * SCALE, (40 + HOTBAR_BOTTOM_PIXEL) * SCALE);

            var hotbargroupPng = new Image<Rgba32>(outputDimensions.Width, outputDimensions.Height);

            hotbargroupPng.Mutate(x => x
                .DrawImage(HotbarSlotsGroup(), new Point(0, (16) * SCALE), 1.0F)
                .DrawImage(XpBarGroup(), new Point(0, 10 * SCALE), 1.0F)
                .DrawImage(HeartGroup(), new Point(0, 0), 1.0F)
            );

            hotbargroupPng.SaveAsPng("output/hotbargroup.png");

            return hotbargroupPng;
        }
        Image HeartGroup()
        {
            var scale = SCALE;
            var outputDimensions = new Size((8 * 10 + 1) * scale, 9 * scale);

            var heartsPng = new Image<Rgba32>(outputDimensions.Width, outputDimensions.Height);

            using var iconsPng = Loader.LoadTexture("assets/minecraft/textures/gui/icons.png");
            using var fullHeart = ImageUtils.CropImage(iconsPng, 52, 0, 9, 9, scale);
            using var emptyHeart = ImageUtils.CropImage(iconsPng, 16, 0, 9, 9, scale);

            for (int i = 0; i < 10; i++)
            {
                heartsPng.Mutate(x => x
                .DrawImage(emptyHeart, new Point((8 * i) * scale, 0), 1.0F)
                .DrawImage(fullHeart, new Point((8 * i) * scale, 0), 1.0F));
            }

            heartsPng.SaveAsPng("output/hearts.png");

            return heartsPng;
        }
        Image XpBarGroup()
        {
            var scale = SCALE;
            var outputDimensions = new Size(182 * scale, 5 * scale);

            var xpbarPng = new Image<Rgba32>(outputDimensions.Width, outputDimensions.Height);

            using var iconsPng = Loader.LoadTexture("assets/minecraft/textures/gui/icons.png");
            using var xpbar = ImageUtils.CropImage(iconsPng, 0, 69, 182, 5, scale);

            xpbarPng.Mutate(x => x
                .DrawImage(xpbar, new Point(0, 0), 1f)
            );

            xpbarPng.SaveAsPng("output/xpbar.png");

            return xpbarPng;
        }
        Image HotbarSlotsGroup()
        {
            var outputDimensions = new Size(182 * SCALE, (23 + HOTBAR_BOTTOM_PIXEL) * SCALE); // actually 23, one pixel is always hidden

            var hotbarPng = new Image<Rgba32>(outputDimensions.Width, outputDimensions.Height);

            using var widgetsPng = Loader.LoadTexture("assets/minecraft/textures/gui/widgets.png");
            using var hotbarlist = ImageUtils.CropImage(widgetsPng, 0, 0, 182, 22, SCALE);
            using var hotbarselected = ImageUtils.CropImage(widgetsPng, 0, 22, 24, 24, SCALE);

            var padding = 1;
            hotbarPng.Mutate(x => x
                .DrawImage(hotbarlist, new Point(0, padding * SCALE), 1f)
                .DrawImage(hotbarselected, new Point(20 * SCALE, 0), 1f)
            );

            hotbarPng.SaveAsPng("output/hotbar.png");

            return hotbarPng;
        }
    }
}
