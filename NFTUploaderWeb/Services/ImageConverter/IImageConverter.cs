namespace NFTUploaderWeb.Services.ImageConverter
{
    public interface IImageConverter
    {
        Task<byte[]> ConvertIFormFileToByteArray(IFormFile file);
    }
}