namespace NFTUploaderWeb.Constants
{
    public static class ConfigurationConstants
    {
        internal readonly static string QuickNodeUrl = nameof(QuickNodeUrl);

        internal readonly static string InfuraIPFSSection = "InfuraIPFS:";

        internal readonly static string InfuraIPFSEndpoint = $"{InfuraIPFSSection}Endpoint";

        internal readonly static string InfuraIPFSKey = $"{InfuraIPFSSection}Key";

        internal readonly static string InfuraIPFSSecret = $"{InfuraIPFSSection}Secret";

        internal readonly static string InfuraIPFSApiEndpoint = $"{InfuraIPFSSection}ApiEndpoint";

        internal readonly static string InfuraIPFSGatewayEndpoint = $"{InfuraIPFSSection}GatewayEndpoint";
    }
}
