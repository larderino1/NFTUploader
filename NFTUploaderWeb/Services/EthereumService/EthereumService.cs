using Contracts.Contracts.MyNFT;
using Contracts.Contracts.MyNFT.ContractDefinition;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.Compilation;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using NFTUploaderWeb.Constants;
using NFTUploaderWeb.Models;
using NFTUploaderWeb.Models.NFTCollections;
using NFTUploaderWeb.Services.ImageConverter;
using NFTUploaderWeb.Services.IpfsService;
using System.Numerics;
using System.Text;

namespace NFTUploaderWeb.Services.EthereumService
{
    internal class EthereumService : IEthereumService
    {
        private readonly string _infuraNodeUrl;

        private readonly string _privateKey;

        private readonly string _infuraNftApiUrl;

        private readonly IIpfsService _ipfsService;

        private readonly IImageConverter _imageConverter;

        private readonly HttpClient _httpClientForInfuraNode;

        private readonly HttpClient _httpClientForInfuraNftApi;

        public EthereumService(
            IIpfsService ipfsService,
            IImageConverter imageConverter,
            IConfiguration config,
            IHttpClientFactory httpClientFactory) 
        {

            _infuraNodeUrl = config[ConfigurationConstants.InfuraApiEndpoint];

            _infuraNftApiUrl = config[ConfigurationConstants.NftInfuraApiEndpoint];

            _privateKey = config.GetConnectionString("PrivateKey");

            _ipfsService = ipfsService;

            _imageConverter = imageConverter;

            _httpClientForInfuraNode = httpClientFactory.CreateClient("InfuraNode");

            _httpClientForInfuraNftApi = httpClientFactory.CreateClient("InfuraNFT_API");
        }

        public async Task UploadSingleNFTAsync(NFTModelForSingle nft, byte[] file)
        {
            var account = new Account(_privateKey);
            var web3 = new Web3(account, _infuraNodeUrl);

            web3.TransactionManager.UseLegacyAsDefault = true;

            var nftDeployment = new MyNFTDeployment()
            {
                TokenName = nft.TokenName,
                TokenSymbol = nft.TokenSymbol
            };

            nftDeployment.SetGasPriceFromGwei(1.00001m);

            var tokenMetadata = await _ipfsService.AddImageAndMetadataToInfuraIPFS(file, nft.Image.Name, nft.Token.TokenTitle, nft.Token.TokenDescription);

            var contract = await MyNFTService.DeployContractAndGetServiceAsync(web3, nftDeployment);

            var result = await contract.SafeMintRequestAsync(new SafeMintFunction
            {
                To = nft.CreatorAddress,
                Uri = string.IsNullOrEmpty(tokenMetadata) ? string.Empty : tokenMetadata,
                TokenPrice = BigInteger.Zero
            });
        }

        public async Task UploadNftInBulkAsync(NFTModelForBulk model)
        {
            var account = new Account(_privateKey);
            var web3 = new Web3(account, _infuraNodeUrl);

            web3.TransactionManager.UseLegacyAsDefault = true;

            var nftDeployment = new MyNFTDeployment()
            {
                TokenName = model.TokenName,
                TokenSymbol = model.TokenSymbol
            };

            nftDeployment.SetGasPriceFromGwei(5.00001m);

            var contract = await MyNFTService.DeployContractAndGetServiceAsync(web3, nftDeployment);

            foreach(var token in model.Tokens)
            {
                var image = model.Images.FirstOrDefault(x => x.FileName == token.ImageName);

                var convertedImage = await _imageConverter.ConvertIFormFileToByteArray(image);


                var tokenMetadata = await _ipfsService.AddImageAndMetadataToInfuraIPFS(convertedImage, image.FileName, token.TokenTitle, token.TokenDescription);

                var test = await contract.SafeMintRequestAsync(new SafeMintFunction
                {
                    To = model.CreatorAddress,
                    Uri = string.IsNullOrEmpty(tokenMetadata) ? string.Empty : tokenMetadata,
                    TokenPrice = BigInteger.Zero
                });
            }
        }

        public async Task<NftCollections> GetNftByAddressAsync(string address)
        {
            var nftCollections = new NftCollections();

            var collectionsResponse = await _httpClientForInfuraNftApi.GetAsync($"{_infuraNftApiUrl}/accounts/{address}/assets/collections");

            if(collectionsResponse != null && collectionsResponse.IsSuccessStatusCode)
            {
                var collectionResponseContent = await collectionsResponse.Content.ReadAsStringAsync();

                nftCollections = JsonConvert.DeserializeObject<NftCollections>(collectionResponseContent);

                foreach(var collection in nftCollections.Collections)
                {
                    var collectionDataResponse = await _httpClientForInfuraNftApi.GetAsync($"{_infuraNftApiUrl}/nfts/{collection.Contract}/tokens?resync=false");

                    if(collectionDataResponse != null && collectionDataResponse.IsSuccessStatusCode)
                    {
                        var collectionDataResponseContent = await collectionDataResponse.Content.ReadAsStringAsync();

                        var collectionData = JsonConvert.DeserializeObject<CollectionData>(collectionDataResponseContent);

                        collection.CollectionData = collectionData;
                    }
                }
            }

            return nftCollections;
        }
    }
}
