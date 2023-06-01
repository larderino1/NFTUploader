namespace NFTUploaderWeb.Models.NFTCollections
{
    public class CollectionData
    {
        public int Total { get; set; }

        public IEnumerable<Asset> Assets { get; set; }
    }
}