using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NFTUploaderWeb.Models;
using NFTUploaderWeb.Services.EthereumService;
using NFTUploaderWeb.Services.ExcelParserService;
using NFTUploaderWeb.Services.ImageConverter;

namespace NFTUploaderWeb.Pages
{
    public class UploadCollectionModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public NFTModelForBulk Nft { get; set; }

        private readonly IEthereumService _ethereumService;

        private readonly IExcelParserService _excelParserService;

        public UploadCollectionModel(
            IEthereumService ethereumService,
            IExcelParserService excelParserService)
        {
            _ethereumService = ethereumService;

            _excelParserService = excelParserService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Nft = new NFTModelForBulk();
                return Page();
            }

            //Nft.Tokens = await _excelParserService.ParseExcel(Nft.ExcelSheet, Path.GetTempPath());

            //await _ethereumService.UploadNftInBulkAsync(Nft);

            return RedirectToPage("/Index", "OnGet", new { Nft.CreatorAddress });
        }
    }
}
