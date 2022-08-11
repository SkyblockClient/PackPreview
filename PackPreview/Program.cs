// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using PackPreview.ImageLoaders;
using SixLabors.ImageSharp;

namespace PackPreview
{
    static class Program
    {
        static void Main(string[] args)
        {
            Directory.CreateDirectory("output");

            var ziploader = new ZipTextureLoader("Essential_Theme.zip");
            var dirloader = new DirectoryTextureLoader("sakamata");

            var generator = new PackPreviewGenerator(ziploader);

            generator.GeneratePreview().SaveAsPng("output/preview.png");

        }
    }
}

