using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NFTUploaderWeb.Models.NFTCollections;
using NFTUploaderWeb.Services.EthereumService;

namespace NFTUploaderWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty(SupportsGet = true)]
        public string CreatorAddress { get; set; }

        [BindProperty]
        public NftCollections NftCollections { get; set; }

        private readonly IEthereumService _ethereumService;

        public IndexModel(
            ILogger<IndexModel> logger,
            IEthereumService ethereumService)
        {
            _logger = logger;

            _ethereumService = ethereumService;
        }

        public async Task<IActionResult> OnGet()
        {
            if(!string.IsNullOrEmpty(CreatorAddress))
            {
                NftCollections = await _ethereumService.GetNftByAddressAsync(CreatorAddress);

                NftCollections.Collections = NftCollections.Collections
                    .Where(x => 
                        x.CollectionData != null && 
                        x.CollectionData.Assets
                        .Any(x => x.Metadata != null))
                    .ToList();
            }

            return Page();
        }
    }
}