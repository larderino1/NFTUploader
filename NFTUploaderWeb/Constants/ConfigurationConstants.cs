namespace NFTUploaderWeb.Constants
{
    public static class ConfigurationConstants
    {
        internal readonly static string QuickNodeUrl = nameof(QuickNodeUrl);

        internal readonly static string InfuraIPFSSection = "InfuraIPFS:";

        internal readonly static string InfuraIPFSEndpoint = $"{InfuraIPFSSection}Endpoint";

        internal readonly static string InfuraIPFSKey = $"{InfuraIPFSSection}Key";

        internal readonly static string InfuraIPFSSecret = $"{InfuraIPFSSection}Secret";

        internal readonly static string InfuraIPFSGatewayEndpoint = $"{InfuraIPFSSection}GatewayEndpoint";

        internal readonly static string InfuraApiSection = "InfuraAPI:";

        internal readonly static string InfuraApiEndpoint = $"{InfuraApiSection}ApiEndpoint";

        internal readonly static string InfuraApiKey = $"{InfuraApiSection}Key";

        internal readonly static string InfuraApiSecret = $"{InfuraApiSection}Secret";

        internal readonly static string NftInfuraApiEndpoint = $"{InfuraApiSection}NftApiEndpoint";
    }
}
