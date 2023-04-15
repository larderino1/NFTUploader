using System.ComponentModel.DataAnnotations;

namespace NFTUploaderWeb.Models
{
    public class NFTModel
    {
        public string CreatorAddress { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string TokenSymbol { get; set; }
        [Required]
        public string TokenName { get; set; }
        [Required]
        public int NumberOfTokens { get; set; }
        [Required]
        public decimal TokenPrice { get; set; }
        [Required]
        public decimal RoyaltyFee { get; set; }
    }
}
