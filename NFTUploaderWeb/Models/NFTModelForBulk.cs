using System.ComponentModel.DataAnnotations;

namespace NFTUploaderWeb.Models
{
    public class NFTModelForBulk
    {
        public string CreatorAddress { get; set; }

        [Required]
        public string TokenSymbol { get; set; }
        [Required]
        public string TokenName { get; set; }

        [Required]
        public IEnumerable<TokenData> Tokens { get; set; }
    }
}
