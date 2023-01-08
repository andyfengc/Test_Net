using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using NPOI.XWPF.UserModel;

namespace Console.Office
{
    public class PoiTest
    {
        public static void GenerateExcel(string filepath)
        {
            using (var fs = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                // Create a new workbook
                IWorkbook workbook = new XSSFWorkbook();
                // create a sheet
                ISheet sheet1 = workbook.CreateSheet("Sheet1");

                sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 10));
                var rowIndex = 0;
                IRow row = sheet1.CreateRow(rowIndex);
                row.Height = 30 * 80;
                row.CreateCell(0).SetCellValue("this is content");
                sheet1.AutoSizeColumn(0);
                rowIndex++;

                var sheet2 = workbook.CreateSheet("Sheet2");
                var style1 = workbook.CreateCellStyle();
                style1.FillForegroundColor = HSSFColor.Blue.Index2;
                style1.FillPattern = FillPattern.SolidForeground;

                var style2 = workbook.CreateCellStyle();
                style2.FillForegroundColor = HSSFColor.Yellow.Index2;
                style2.FillPattern = FillPattern.SolidForeground;

                var cell2 = sheet2.CreateRow(0).CreateCell(0);
                cell2.CellStyle = style1;
                cell2.SetCellValue(0);

                cell2 = sheet2.CreateRow(1).CreateCell(0);
                cell2.CellStyle = style2;
                cell2.SetCellValue(1);

                workbook.Write(fs);
            }
        }

        public static byte[] GenerateExcel2()
        {
            using (var memoryStream = new MemoryStream())
            {
                var workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("My sheet");
                IRow row = sheet.CreateRow(0);
                var cell = row.CreateCell(0);
                cell.SetCellValue("Hello");
                cell = row.CreateCell(1);
                cell.SetCellValue("World");
                for (int i = 0; i <= 20; i++) sheet.AutoSizeColumn(i);

                workbook.Write(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static void GenerateWord(string filepath)
        {
            using (var fs = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                XWPFDocument doc = new XWPFDocument();
                var p0 = doc.CreateParagraph();
                p0.Alignment = ParagraphAlignment.CENTER;
                XWPFRun r0 = p0.CreateRun();
                r0.FontFamily = "microsoft yahei";
                r0.FontSize = 18;
                r0.IsBold = true;
                r0.SetText("This is title");

                var p1 = doc.CreateParagraph();
                p1.Alignment = ParagraphAlignment.LEFT;
                p1.IndentationFirstLine = 500;
                XWPFRun r1 = p1.CreateRun();
                r1.FontFamily = "·ÂËÎ";
                r1.FontSize = 12;
                r1.IsBold = true;
                r1.SetText("This is content, content content content content content content content content content");

                doc.Write(fs);
            }
        }
    }
}
