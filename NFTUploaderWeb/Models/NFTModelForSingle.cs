using System.ComponentModel.DataAnnotations;

namespace NFTUploaderWeb.Models
{
    public class NFTModelForSingle
    {
        public string CreatorAddress { get; set; }

        [Required]
        public string TokenSymbol { get; set; }
        [Required]
        public string TokenName { get; set; }
        [Required]
        public TokenData Token { get; set; }
    }
}
