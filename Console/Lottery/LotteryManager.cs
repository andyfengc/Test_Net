using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.Models;
using OfficeOpenXml;
using Console = System.Console;

namespace Console.Lottery
{
    public class LotteryManager
    {
        public void L649ExcelToConsole(string filepath)
        {
            IEnumerable<L649> records = ExcelToEntities(filepath);
            foreach (var record in records)
            {
                System.Console.WriteLine(record.DrawDate + " " + record.No1 + " " + record.No2);
            }
        }

        public void L649ExcelToDb(string filepath)
        {
            //var lotteryDb = new LotteryContext();//sqlserver
            var lotteryDb = new MysqlTestContext();
            var l649s = lotteryDb.L649s.ToList();
            IEnumerable<L649> records = ExcelToEntities(filepath);
            foreach (var record in records)
            {
                lotteryDb.L649s.Add(record);
                lotteryDb.SaveChanges();
            }
            System.Console.WriteLine("saved successfully");
        }

        private IEnumerable<L649> ExcelToEntities(string filepath)
        {
            var records = new List<L649>();
            using (var package = new ExcelPackage())
            {
                package.Load(new FileStream(filepath, FileMode.Open));
                var worksheet = package.Workbook.Worksheets.First();
                for (var row = 1; row <= worksheet.Dimension.Rows; row++)
                {
                    var record = new L649();
                    // get date
                    var drawDateStr = worksheet.Cells[row, 1].Text;
                    DateTime d;
                    if (DateTime.TryParseExact(drawDateStr, "d\"st\" MMMM yyyy", null, DateTimeStyles.None, out d))
                        record.DrawDate = d;
                    if (DateTime.TryParseExact(drawDateStr, "d\"nd\" MMMM yyyy", null, DateTimeStyles.None, out d))
                        record.DrawDate = d;
                    if (DateTime.TryParseExact(drawDateStr, "d\"rd\" MMMM yyyy", null, DateTimeStyles.None, out d))
                        record.DrawDate = d;
                    if (DateTime.TryParseExact(drawDateStr, "d\"th\" MMMM yyyy", null, DateTimeStyles.None, out d))
                        record.DrawDate = d;
                    if (record.DrawDate == null)
                    {
                        throw new FormatException("failed to parse date: " + drawDateStr);
                    }
                    // get numbers
                    record.No1 = int.Parse(worksheet.Cells[row, 2].Text);
                    record.No2 = int.Parse(worksheet.Cells[row, 3].Text);
                    record.No3 = int.Parse(worksheet.Cells[row, 4].Text);
                    record.No4 = int.Parse(worksheet.Cells[row, 5].Text);
                    record.No5 = int.Parse(worksheet.Cells[row, 6].Text);
                    record.No6 = int.Parse(worksheet.Cells[row, 7].Text);
                    record.bonus = int.Parse(worksheet.Cells[row, 8].Text);
                    //System.Console.WriteLine(string.Format("draw_date: {0}, no_1: {1}"
                    //    , worksheet.Cells[row, 1].Text
                    //    , worksheet.Cells[row, 2].Text));
                    records.Add(record);
                }
            }
            return records;
        }
    }
}
