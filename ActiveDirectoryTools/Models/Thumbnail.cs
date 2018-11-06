using System;
using System.IO;
using ActiveDirectoryTools.Interfaces;

namespace ActiveDirectoryTools.Models
{
    public class Thumbnail : IThumbnail
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public byte[] ThumbnailData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
