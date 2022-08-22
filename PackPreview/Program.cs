// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using PackPreview.ImageLoaders;
using SixLabors.ImageSharp;

namespace PackPreview
{
    static class Program
    {
        private static Dictionary<string, string> Arguments;
        static void Main(string[] args)
        {
            Arguments = ResolveArguments(args);


            var scale = int.Parse(GetArgument("scale", "4"));
            var pack = GetArgument("pack", "base.zip");

            Directory.CreateDirectory("output");

            var ziploader = new ZipTextureLoader(pack);
            //var dirloader = new DirectoryTextureLoader("nameless");

            var generator = new PackPreviewGenerator(ziploader);

            generator.GeneratePreview(scale).SaveAsPng("output/preview.png");

        }

        private static string GetArgument(string key, string defaultvalue)
        {
            if (Arguments.ContainsKey(key))
            {
                return Arguments[key];
            }
            return defaultvalue;
        }

        private static Dictionary<string, string> ResolveArguments(string[] args)
        {
            var arguments = new Dictionary<string, string>();
            foreach (string argument in args)
            {
                int idx = argument.IndexOf('=');
                if (idx > 0)
                    arguments[argument.Substring(0, idx)] = argument.Substring(idx + 1);
            }
            return arguments;
        }
    }
}

