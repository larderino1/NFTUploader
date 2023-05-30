using Contracts.Contracts.MyNFT;
using Contracts.Contracts.MyNFT.ContractDefinition;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.Compilation;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using NFTUploaderWeb.Constants;
using NFTUploaderWeb.Models;
using NFTUploaderWeb.Services.ImageConverter;
using NFTUploaderWeb.Services.IpfsService;
using System.Numerics;
using System.Text;

namespace NFTUploaderWeb.Services.EthereumService
{
    internal class EthereumService : IEthereumService
    {
        private readonly string _infuraNodeUrl;

        private readonly string _encodedCredentials;

        private readonly string _privateKey;

        private readonly IIpfsService _ipfsService;

        private readonly IImageConverter _imageConverter;

        private readonly HttpClient _httpClient;

        public EthereumService(
            IIpfsService ipfsService,
            IImageConverter imageConverter,
            IConfiguration config,
            IHttpClientFactory httpClientFactory) 
        {

            _infuraNodeUrl = config[ConfigurationConstants.InfuraIPFSApiEndpoint];

            _privateKey = config.GetConnectionString("PrivateKey");

            _ipfsService = ipfsService;

            _imageConverter = imageConverter;

            _httpClient = httpClientFactory.CreateClient();
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

            var tokenMetadata = await _ipfsService.AddImageAndMetadataToInfuraIPFS(file, nft.Token.Image.Name, nft.Token.TokenTitle, nft.Token.TokenDescription);

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

            nftDeployment.SetGasPriceFromGwei(1.00001m);

            var contract = await MyNFTService.DeployContractAndGetServiceAsync(web3, nftDeployment);

            foreach(var token in model.Tokens)
            {
                var convertedImage = await _imageConverter.ConvertIFormFileToByteArray(token.Image);

                var tokenMetadata = await _ipfsService.AddImageAndMetadataToInfuraIPFS(convertedImage, token.Image.Name, token.TokenTitle, token.TokenDescription);

                await contract.SafeMintRequestAsync(new SafeMintFunction
                {
                    To = model.CreatorAddress,
                    Uri = string.IsNullOrEmpty(tokenMetadata) ? string.Empty : tokenMetadata,
                    TokenPrice = BigInteger.Zero
                });
            }

        }
    }
}
