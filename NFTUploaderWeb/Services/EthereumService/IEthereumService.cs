using NFTUploaderWeb.Models;

namespace NFTUploaderWeb.Services.EthereumService
{
    public interface IEthereumService
    {
        Task UploadSingleNFTAsync(NFTModelForSingle nft, byte[] file);
    }
}
