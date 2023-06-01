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
        public IEnumerable<IFormFile> Images { get; set; }

        [Required]
        public IFormFile ExcelSheet { get; set; }

        public IEnumerable<TokenData> Tokens { get; set; } = new List<TokenData>();
    }
}
