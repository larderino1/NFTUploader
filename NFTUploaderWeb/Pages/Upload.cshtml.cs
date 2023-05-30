using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NFTUploaderWeb.Models;
using NFTUploaderWeb.Services.EthereumService;
using NFTUploaderWeb.Services.ImageConverter;

namespace NFTUploaderWeb.Pages
{
    public class UploadModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public NFTModelForSingle Nft { get; set; }

        private readonly IEthereumService _ethereumService;

        private readonly IImageConverter _imageConverter;

        public UploadModel(
            IEthereumService ethereumService,
            IImageConverter imageConverter)
        {
            _ethereumService = ethereumService;

            _imageConverter = imageConverter;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Nft = new NFTModelForSingle();
                return Page();
            }

            var fileBytes = await _imageConverter.ConvertIFormFileToByteArray(Nft.Token.Image);

            await _ethereumService.UploadSingleNFTAsync(Nft, fileBytes);

            return RedirectToPage("/Index");
        }
    }
}
