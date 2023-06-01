namespace NFTUploaderWeb.Models.NFTCollections
{
    public class Asset
    {
        public string TokenId { get; set; }

        public string Supply { get; set; }

        public IPFSMetadataModel Metadata { get; set; }
    }
}