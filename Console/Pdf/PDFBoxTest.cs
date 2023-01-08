using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using java.io;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using Console = System.Console;

namespace Console.Pdf
{
    class PDFBoxTest
    {
        public static string ExtractTextFromPdf(string path)
        {
            path = Directory.GetCurrentDirectory() + "\\1.pdf";
            PDDocument doc = null;
            try
            {
                doc = PDDocument.load(path);
                if (doc.isEncrypted())
                {
                    doc.decrypt("");
                    doc.setAllSecurityToBeRemoved(true);
                }
                PDDocumentCatalog catalog = doc.getDocumentCatalog();
                var pageList = catalog.getAllPages();
                PDFTextStripper stripper = new PDFTextStripper();
                stripper.setSortByPosition(true);
                if (pageList != null)
                {
                    System.Console.WriteLine("{0} pages", pageList.size());
                    for (int page = 0; page < pageList.size(); page++)
                    {
                        System.Console.WriteLine("\t page {0}", page + 1);
                        stripper.setStartPage(page + 1);
                        stripper.setEndPage(page + 1);
                        string pageText = stripper.getText(doc);
                        var lineText = Regex.Split(pageText, "\r\n");
                        int lineIndex = 0;
                        foreach (var line in lineText)
                        {
                            //System.Console.WriteLine("\t\t{0}: {1}", ++lineIndex, line);
                            string pattern = @"7\d{15}\s";
                            Match m = Regex.Match(line, pattern);
                            if (m.Success)
                            {
                                string chargePattern = @"\d*\.\d*$";
                                Match chargeMatch = Regex.Match(line, chargePattern);
                                System.Console.WriteLine("\t\t" + m.Value + "\t" + chargeMatch.Value);

                                //System.Console.WriteLine("Value  = " + m.Value);
                                //System.Console.WriteLine("Length = " + m.Length);
                                //System.Console.WriteLine("Index  = " + m.Index);
                            }
                        }
                    }
                }
                stripper.setSortByPosition(true);
                string text = stripper.getText(doc);
                return text;
            }
            finally
            {
                if (doc != null)
                {
                    doc.close();
                }
            }
        }
    }
}
