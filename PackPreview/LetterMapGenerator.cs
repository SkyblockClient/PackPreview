using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PackPreview
{
    public record LetterPointer(Point location, int width);

    public class LetterMapGenerator
    {
        public static int GetCharacterSize(Image<Rgba32> ascii, Point unscaledLocation)
        {
            var fontScale = ascii.Width / 128;

            var letterSize = new Size(8 * fontScale, 8 * fontScale);

            using var letter = ImageUtils.CropImage(ascii, unscaledLocation.X * fontScale, unscaledLocation.Y * fontScale, letterSize.Width, letterSize.Height, 1);

            var sizeList = new int[letterSize.Height];


            for (int y = 0; y < letter.Height; y++)
            {
                int lastColored = 0;
                var row = letter.GetPixelRowSpan(y);
                for (int x = 0; x < row.Length; x++)
                {
                    var pixel = row[x];

                    if (!(pixel.A < byte.MaxValue))
                    {
                        lastColored = x + 1;
                    }
                }

                sizeList[y] = lastColored;
            }

            var max = sizeList.Max();

            return max == 0 ? 2 * fontScale : max;
        }

        public static Dictionary<char, LetterPointer> GenerateLetterMapFromAscii(Image<Rgba32> ascii)
        {
            var lettermap = GenerateLetterMap();

            foreach (var letter in lettermap.Keys)
            {
                var pointer = lettermap[letter];
                var letterSize = GetCharacterSize(ascii, pointer.location);
                lettermap[letter] = new LetterPointer(pointer.location, letterSize);

                Console.WriteLine(letter + " " + letterSize);
            }

            return lettermap;
        }

        public static Dictionary<char, LetterPointer> GenerateLetterMap()
        {
            var r16 = 0;
            var r24 = 0;
            var r32 = 0;
            var r40 = 0;
            var r48 = 0;
            var r56 = 0;

            var lm = new Dictionary<char, LetterPointer>()
            {
                { ' ', new LetterPointer(new Point(r16, 16), 2) },
                { '!', new LetterPointer(new Point(r16 += 8, 16), 1) },
                { '"', new LetterPointer(new Point(r16 += 8, 16), 4) },
                { '#', new LetterPointer(new Point(r16 += 8, 16), 5) },
                { '$', new LetterPointer(new Point(r16 += 8, 16), 5) },
                { '%', new LetterPointer(new Point(r16 += 8, 16), 5) },
                { '&', new LetterPointer(new Point(r16 += 8, 16), 5) },
                { '\'',new LetterPointer(new Point(r16 += 8, 16), 2) },
                { '(', new LetterPointer(new Point(r16 += 8, 16), 4) },
                { ')', new LetterPointer(new Point(r16 += 8, 16), 4) },
                { '*', new LetterPointer(new Point(r16 += 8, 16), 4) },
                { '+', new LetterPointer(new Point(r16 += 8, 16), 5) },
                { ',', new LetterPointer(new Point(r16 += 8, 16), 1) },
                { '-', new LetterPointer(new Point(r16 += 8, 16), 5) },
                { '.', new LetterPointer(new Point(r16 += 8, 16), 1) },
                { '/', new LetterPointer(new Point(r16 += 8, 16), 5) },

                { '0', new LetterPointer(new Point(r24, 24), 5) },
                { '1', new LetterPointer(new Point(r24 += 8, 24), 5) },
                { '2', new LetterPointer(new Point(r24 += 8, 24), 5) },
                { '3', new LetterPointer(new Point(r24 += 8, 24), 5) },
                { '4', new LetterPointer(new Point(r24 += 8, 24), 5) },
                { '5', new LetterPointer(new Point(r24 += 8, 24), 5) },
                { '6', new LetterPointer(new Point(r24 += 8, 24), 5) },
                { '7', new LetterPointer(new Point(r24 += 8, 24), 5) },
                { '8', new LetterPointer(new Point(r24 += 8, 24), 5) },
                { '9', new LetterPointer(new Point(r24 += 8, 24), 5) },
                { ':', new LetterPointer(new Point(r24 += 8, 24), 1) },
                { ';', new LetterPointer(new Point(r24 += 8, 24), 1) },
                { '<', new LetterPointer(new Point(r24 += 8, 24), 4) },
                { '=', new LetterPointer(new Point(r24 += 8, 24), 5) },
                { '>', new LetterPointer(new Point(r24 += 8, 24), 4) },
                { '?', new LetterPointer(new Point(r24 += 8, 24), 5) },

                { '@', new LetterPointer(new Point(r32, 32), 5) },
                { 'A', new LetterPointer(new Point(r32 += 8, 32), 5) },
                { 'B', new LetterPointer(new Point(r32 += 8, 32), 5) },
                { 'C', new LetterPointer(new Point(r32 += 8, 32), 5) },
                { 'D', new LetterPointer(new Point(r32 += 8, 32), 5) },
                { 'E', new LetterPointer(new Point(r32 += 8, 32), 5) },
                { 'F', new LetterPointer(new Point(r32 += 8, 32), 5) },
                { 'G', new LetterPointer(new Point(r32 += 8, 32), 5) },
                { 'H', new LetterPointer(new Point(r32 += 8, 32), 5) },
                { 'I', new LetterPointer(new Point(r32 += 8, 32), 3) },
                { 'J', new LetterPointer(new Point(r32 += 8, 32), 5) },
                { 'K', new LetterPointer(new Point(r32 += 8, 32), 5) },
                { 'L', new LetterPointer(new Point(r32 += 8, 32), 5) },
                { 'M', new LetterPointer(new Point(r32 += 8, 32), 5) },
                { 'N', new LetterPointer(new Point(r32 += 8, 32), 5) },
                { 'O', new LetterPointer(new Point(r32 += 8, 32), 5) },

                { 'P', new LetterPointer(new Point(r40, 40), 5) },
                { 'Q', new LetterPointer(new Point(r40 += 8, 40), 5) },
                { 'R', new LetterPointer(new Point(r40 += 8, 40), 5) },
                { 'S', new LetterPointer(new Point(r40 += 8, 40), 5) },
                { 'T', new LetterPointer(new Point(r40 += 8, 40), 5) },
                { 'U', new LetterPointer(new Point(r40 += 8, 40), 5) },
                { 'V', new LetterPointer(new Point(r40 += 8, 40), 5) },
                { 'W', new LetterPointer(new Point(r40 += 8, 40), 5) },
                { 'X', new LetterPointer(new Point(r40 += 8, 40), 5) },
                { 'Y', new LetterPointer(new Point(r40 += 8, 40), 3) },
                { 'Z', new LetterPointer(new Point(r40 += 8, 40), 5) },
                { '[', new LetterPointer(new Point(r40 += 8, 40), 3) },
                { '\\',new LetterPointer(new Point(r40 += 8, 40), 5) },
                { ']', new LetterPointer(new Point(r40 += 8, 40), 3) },
                { '^', new LetterPointer(new Point(r40 += 8, 40), 5) },
                { '_', new LetterPointer(new Point(r40 += 8, 40), 5) },

                { '`', new LetterPointer(new Point(r48, 48), 2) },
                { 'a', new LetterPointer(new Point(r48 += 8, 48), 5) },
                { 'b', new LetterPointer(new Point(r48 += 8, 48), 5) },
                { 'c', new LetterPointer(new Point(r48 += 8, 48), 5) },
                { 'd', new LetterPointer(new Point(r48 += 8, 48), 5) },
                { 'e', new LetterPointer(new Point(r48 += 8, 48), 5) },
                { 'f', new LetterPointer(new Point(r48 += 8, 48), 4) },
                { 'g', new LetterPointer(new Point(r48 += 8, 48), 5) },
                { 'h', new LetterPointer(new Point(r48 += 8, 48), 5) },
                { 'i', new LetterPointer(new Point(r48 += 8, 48), 1) },
                { 'j', new LetterPointer(new Point(r48 += 8, 48), 5) },
                { 'k', new LetterPointer(new Point(r48 += 8, 48), 4) },
                { 'l', new LetterPointer(new Point(r48 += 8, 48), 2) },
                { 'm', new LetterPointer(new Point(r48 += 8, 48), 5) },
                { 'n', new LetterPointer(new Point(r48 += 8, 48), 5) },
                { 'o', new LetterPointer(new Point(r48 += 8, 48), 5) },

                { 'p', new LetterPointer(new Point(r56, 56), 5) },
                { 'q', new LetterPointer(new Point(r56 += 8, 56), 5) },
                { 'r', new LetterPointer(new Point(r56 += 8, 56), 5) },
                { 's', new LetterPointer(new Point(r56 += 8, 56), 5) },
                { 't', new LetterPointer(new Point(r56 += 8, 56), 3) },
                { 'u', new LetterPointer(new Point(r56 += 8, 56), 5) },
                { 'v', new LetterPointer(new Point(r56 += 8, 56), 5) },
                { 'w', new LetterPointer(new Point(r56 += 8, 56), 5) },
                { 'x', new LetterPointer(new Point(r56 += 8, 56), 5) },
                { 'y', new LetterPointer(new Point(r56 += 8, 56), 5) },
                { 'z', new LetterPointer(new Point(r56 += 8, 56), 5) },
                { '{', new LetterPointer(new Point(r56 += 8, 56), 4) },
                { '|', new LetterPointer(new Point(r56 += 8, 56), 1) },
                { '}', new LetterPointer(new Point(r56 += 8, 56), 4) },
                { '~', new LetterPointer(new Point(r56 += 8, 56), 6) },
            };

            return lm;
        }
    }
}
