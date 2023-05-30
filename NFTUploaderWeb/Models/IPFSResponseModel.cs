namespace NFTUploaderWeb.Models
{
    public class IPFSResponseModel
    {
        public string Name { get; set; }
        public string Hash { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string IpfsUri { get; set; }
        public string IpnsUri { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
