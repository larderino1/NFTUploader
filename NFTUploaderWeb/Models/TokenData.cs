using System.ComponentModel.DataAnnotations;

namespace NFTUploaderWeb.Models
{
    public class TokenData
    {
        [Required]
        public string ImageName { get; set; }
        [Required]
        public string TokenTitle { get; set; }
        [Required]
        public string TokenDescription { get; set; }
        [Required]
        public int TokenPrice { get; set; }
    }
}
