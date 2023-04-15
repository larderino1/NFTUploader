using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NFTUploaderWeb.Models;

namespace NFTUploaderWeb.Pages
{
    public class UploadModel : PageModel
    {
        [BindProperty]
        public NFTModel Nft { get; set; }

        private readonly IWebHostEnvironment _environment;

        public UploadModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var filePath = Path.Combine(_environment.WebRootPath, "uploads", Nft.Image.FileName);

            using var stream = new FileStream(filePath, FileMode.Create);

            await Nft.Image.CopyToAsync(stream);

            return RedirectToPage("/Index");
        }
    }
}
