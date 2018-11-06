using System;
using System.IO;
using ActiveDirectoryTools.Interfaces;

namespace ActiveDirectoryTools.Models
{
    public class Thumbnail : IThumbnail
    {
        public string Name { get; set; }
        public byte[] ThumbnailData { get; set; }

        public enum Format
        {
            BMP, GIF, JPG, PNG
        }

        public void ExportToDisk(Thumbnail thumbnail, Format format, string location)
        {
            // Conversion?

            File.WriteAllBytes(Path.Combine(location, thumbnail.Name + "." + format), thumbnail.ThumbnailData);
        }
    }
}
