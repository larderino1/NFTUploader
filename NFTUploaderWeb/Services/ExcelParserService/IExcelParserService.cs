using NFTUploaderWeb.Models;

namespace NFTUploaderWeb.Services.ExcelParserService
{
    public interface IExcelParserService
    {
        Task<List<TokenData>> ParseExcel(IFormFile excelFile, string fullPath);
    }
}