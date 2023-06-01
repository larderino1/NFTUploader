using NFTUploaderWeb.Models;
using NFTUploaderWeb.Models.NFTCollections;

namespace NFTUploaderWeb.Services.EthereumService
{
    public interface IEthereumService
    {
        Task UploadSingleNFTAsync(NFTModelForSingle nft, byte[] file);

        Task UploadNftInBulkAsync(NFTModelForBulk model);

        Task<NftCollections> GetNftByAddressAsync(string address);
    }
}
