namespace NFTUploaderWeb.Services.ImageConverter
{
    public class ImageConverter : IImageConverter
    {
        public ImageConverter() { }

        public async Task<byte[]> ConvertIFormFileToByteArray(IFormFile file)
        {
            var memoryStream = new MemoryStream();

            memoryStream.Seek(0, SeekOrigin.Begin);

            await file.CopyToAsync(memoryStream);

            var fileBytes = memoryStream.ToArray();

            return fileBytes;
        } 
    }
}
