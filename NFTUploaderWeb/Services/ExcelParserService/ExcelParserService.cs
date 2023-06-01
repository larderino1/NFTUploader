using NFTUploaderWeb.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Text;

namespace NFTUploaderWeb.Services.ExcelParserService
{
    public class ExcelParserService : IExcelParserService
    {

        public async Task<List<TokenData>> ParseExcel(IFormFile excelFile, string fullPath)
        {

            if (excelFile != null && excelFile.Length > 0)
            {
                var fileExtension = Path.GetExtension(excelFile.FileName).ToLower();

                ISheet sheet;

                var excelPath = Path.Combine(fullPath, excelFile.FileName);

                using var stream = new FileStream(excelPath, FileMode.Create);

                await excelFile.CopyToAsync(stream);

                stream.Position = 0;

                if (fileExtension == ".xls")
                {
                    HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats   
                    sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                }
                else
                {
                    XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format   
                    sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook    
                }

                var headerRow = sheet.GetRow(0);

                int cellCount = headerRow.LastCellNum;

                for (int i = 0; i < cellCount; i++)
                {
                    var cell = headerRow.GetCell(i);

                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) 
                        continue;
                }

                var tokens = new List<TokenData>();

                for(int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);

                    if(row == null)
                        continue;

                    if (row.Cells.All(d => d.CellType == CellType.Blank))
                        continue;

                    var token = new TokenData
                    {
                        ImageName = row.Cells[0].ToString(),
                        TokenTitle = row.Cells[1].ToString(),
                        TokenDescription = row.Cells[2].ToString(),
                        TokenPrice = Convert.ToInt32(row.Cells[3].ToString())
                    };

                    tokens.Add(token);
                }

                if (File.Exists(excelPath))
                {
                    File.Delete(excelPath);
                }

                return tokens;
            }

            return new List<TokenData>();
        }
    }
}
