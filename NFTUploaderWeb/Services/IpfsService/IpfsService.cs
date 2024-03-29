﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NFTUploaderWeb.Constants;
using NFTUploaderWeb.Models;
using System.Text;

namespace NFTUploaderWeb.Services.IpfsService
{
    public class IpfsService : IIpfsService
    {
        private readonly string _ipfsGateway;

        private readonly string _encodedCredentials;

        private readonly string _ipfsServiceURL;

        private readonly HttpClient _httpClient;

        public IpfsService(
            IConfiguration config,
            IHttpClientFactory httpClientFactory)
        {
            _ipfsGateway = config[ConfigurationConstants.InfuraIPFSGatewayEndpoint];

            _ipfsServiceURL = config[ConfigurationConstants.InfuraIPFSEndpoint];

            _httpClient = httpClientFactory.CreateClient("InfuraIPFS");
        }

        public async Task<string> AddImageAndMetadataToInfuraIPFS(byte[] file,
                                                                  string imageFileName,
                                                                  string tokenName,
                                                                  string tokenDesctiption)
        {
            var imageContent = new MultipartFormDataContent
            {
                { new ByteArrayContent(file), "Image", imageFileName }
            };

            var imageAdditionResponse = await _httpClient.PostAsync($"{_ipfsServiceURL}/add", imageContent);

            if (imageAdditionResponse.IsSuccessStatusCode)
            {
                var imageAdditionResponseContent = await imageAdditionResponse.Content.ReadAsStringAsync();

                var imageHash = JsonConvert.DeserializeObject<IPFSResponseModel>(imageAdditionResponseContent).Hash;

                var ipfsImageMetadataModel = new IPFSMetadataModel()
                {
                    Name = tokenName,
                    Description = tokenDesctiption,
                    Image = $"{_ipfsGateway}{imageHash}"
                };

                var ipfsMetadataJson = JsonConvert.SerializeObject(ipfsImageMetadataModel);

                var ipfsMetadataBytes = Encoding.UTF8.GetBytes(ipfsMetadataJson);

                var ipfsMetadataContent = new MultipartFormDataContent
                {
                    { new ByteArrayContent(ipfsMetadataBytes), "Metadata", $"{imageFileName}.json" }
                };

                var metadataResponse = await _httpClient.PostAsync($"{_ipfsServiceURL}/add", ipfsMetadataContent);

                var metadataResponseContent = await metadataResponse.Content.ReadAsStringAsync();

                var metadataHash = JsonConvert.DeserializeObject<IPFSResponseModel>(metadataResponseContent).Hash;

                var metadataUri = $"{_ipfsGateway}{metadataHash}";

                return metadataUri;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
