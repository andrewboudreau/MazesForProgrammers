using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MazesForProgrammers.Render.Extensions
{
    public static class ImageExtensions
    {
        public static void SaveAndOpen(this Image image, string file)
        {
            file = file.Replace("{0}", DateTime.UtcNow.Ticks.ToString());
            if (Path.GetExtension(file) != ".png")
            {
                file += ".png";
            }

            var outputFile = file;
            image.Save(outputFile, ImageFormat.Png);

            var path = Path.GetFullPath(outputFile);
            using var proc = Process.Start(@"cmd.exe ", $@"/c {path}");
            proc.WaitForExit(1_000);
        }
    }
}
