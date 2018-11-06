namespace ActiveDirectoryTools.Interfaces
{
    public interface IThumbnail
    {
        string Name { get; set; }
        byte[] ThumbnailData { get; set; }
    }
}
