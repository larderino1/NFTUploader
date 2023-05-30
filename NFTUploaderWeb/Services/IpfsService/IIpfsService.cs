namespace NFTUploaderWeb.Services.IpfsService
{
    public interface IIpfsService
    {
        Task<string> AddImageAndMetadataToInfuraIPFS(byte[] file,
                                                     string imageFileName,
                                                     string tokenName,
                                                     string tokenDesctiption);
    }
}
