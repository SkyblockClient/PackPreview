// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using PackPreview.ImageLoaders;
using Newtonsoft.Json;
using PackPreview.Json;
using PackPreview.ImageSavers;

namespace PackPreview
{
    static class Program
    {
        private static Dictionary<string, string> Arguments;
        static void Main(string[] args)
        {
            var json = JsonConvert.DeserializeObject<JsonGroupParent>(File.ReadAllText("groups.json"));

            Directory.CreateDirectory("output compliance");
            Directory.CreateDirectory("output phqdark");
            Console.WriteLine("loading compliance");
            new PackPreviewGenerator(new ZipTextureLoader("compliance.zip"), new DirectoryTextureSaver("output compliance")).GeneratePreview(json);
            Console.WriteLine("loading phqdark");
            new PackPreviewGenerator(new ZipTextureLoader("phqdark.zip"), new DirectoryTextureSaver("output phqdark")).GeneratePreview(json);

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

