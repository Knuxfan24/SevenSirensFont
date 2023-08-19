namespace SevenSirensFont
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // If there isn't two arguments specified, then show the usage instructions.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage:\n" +
                                  "SevenSirensFont.exe {path to data folder (fig 1)} {path to .ttf/.otf file (fig 2)}\n" +
                                  "fig 1: \"D:\\Steam\\steamapps\\common\\Shantae and the Seven Sirens\\data\"\n" +
                                  "fig 2: \"C:\\Users\\Knuxf\\Downloads\\cczoinks-medium.ttf\"\n\n" +
                                  "Example:\n" +
                                  "SevenSirensFont.exe \"D:\\Steam\\steamapps\\common\\Shantae and the Seven Sirens\\data\" \"C:\\Users\\Knuxf\\Downloads\\cczoinks-medium.ttf\"\n\n" +
                                  "Press any key to exit.");
                Console.ReadKey();
                return;
            }

            // TrueType Fonts.
            if (Path.GetExtension(args[1]) == ".ttf")
            {
                // Back up the archives if backups don't exist.
                if (!File.Exists($@"{args[0]}\font.orig"))
                    File.Copy($@"{args[0]}\font.pak", $@"{args[0]}\font.orig", true);

                if (!File.Exists($@"{args[0]}\font_ar.orig"))
                    File.Copy($@"{args[0]}\font_ar.pak", $@"{args[0]}\font_ar.orig", true);

                if (!File.Exists($@"{args[0]}\font_cs.orig"))
                    File.Copy($@"{args[0]}\font_cs.pak", $@"{args[0]}\font_cs.orig", true);

                if (!File.Exists($@"{args[0]}\font_jp.orig"))
                    File.Copy($@"{args[0]}\font_jp.pak", $@"{args[0]}\font_jp.orig", true);

                // Replace the fonts.
                ReplaceFonts($@"{args[0]}\font.pak", args[1], ".ttf");
                ReplaceFonts($@"{args[0]}\font_ar.pak", args[1], ".ttf");
                ReplaceFonts($@"{args[0]}\font_cs.pak", args[1], ".ttf");
                ReplaceFonts($@"{args[0]}\font_jp.pak", args[1], ".ttf");
            }

            // OpenType Fonts.
            if (Path.GetExtension(args[1]) == ".otf")
            {
                // Back up the archives if backups don't exist.
                if (!File.Exists($@"{args[0]}\autoload.orig"))
                    File.Copy($@"{args[0]}\autoload.pak", $@"{args[0]}\autoload.orig", true);

                if (!File.Exists($@"{args[0]}\font.orig"))
                    File.Copy($@"{args[0]}\font.pak", $@"{args[0]}\font.orig", true);

                if (!File.Exists($@"{args[0]}\font_ct.orig"))
                    File.Copy($@"{args[0]}\font_ct.pak", $@"{args[0]}\font_ct.orig", true);

                if (!File.Exists($@"{args[0]}\font_jp.orig"))
                    File.Copy($@"{args[0]}\font_jp.pak", $@"{args[0]}\font_jp.orig", true);

                if (!File.Exists($@"{args[0]}\font_kr.orig"))
                    File.Copy($@"{args[0]}\font_kr.pak", $@"{args[0]}\font_kr.orig", true);

                // Replace the fonts.
                ReplaceFonts($@"{args[0]}\autoload.pak", args[1], ".otf");
                ReplaceFonts($@"{args[0]}\font.pak", args[1], ".otf");
                ReplaceFonts($@"{args[0]}\font_ct.pak", args[1], ".otf");
                ReplaceFonts($@"{args[0]}\font_jp.pak", args[1], ".otf");
                ReplaceFonts($@"{args[0]}\font_kr.pak", args[1], ".otf");
            }

            // Tell the user that we're done and to hit a key to close the program.
            Console.WriteLine("Done!\nPress any key to close.");
            Console.ReadKey();
        }

        /// <summary>
        /// Function that does the file replacement.
        /// </summary>
        /// <param name="packageFile">The package file to process.</param>
        /// <param name="fontFile">The path to the font to swap in.</param>
        /// <param name="extension">The extension to search for.</param>
        private static void ReplaceFonts(string packageFile, string fontFile, string extension)
        {
            // Tell the user that the package file is being opened.
            Console.WriteLine($"Opening {packageFile}.");

            // Open the package file.
            KnuxLib.Engines.Wayforward.Package package = new(packageFile);

            // Get the binary data of the replacement font.
            byte[] replacementFont = File.ReadAllBytes(fontFile);

            // Search through all the files for fonts.
            foreach (KnuxLib.FileNode file in package.Data)
            {
                // If this file matches our extension, then replace it.
                if (Path.GetExtension(file.Name) == extension)
                {
                    // Tell the user that a file is being replaced.
                    Console.WriteLine($"Replacing {file.Name}.");

                    // Replace the file's data with our font's.
                    file.Data = replacementFont;
                }
            }

            // Tell the user we're saving the package.
            Console.WriteLine($"Saving updated {packageFile}.\n");
            
            // Save the package.
            package.Save(packageFile);
        }
    }
}