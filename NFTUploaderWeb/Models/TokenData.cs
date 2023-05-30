using System.ComponentModel.DataAnnotations;

namespace NFTUploaderWeb.Models
{
    public class TokenData
    {
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string TokenTitle { get; set; }
        [Required]
        public string TokenDescription { get; set; }
    }
}
