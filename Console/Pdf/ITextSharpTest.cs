using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using org.apache.pdfbox;
using Console = System.Console;

namespace Console.Pdf
{
    class ITextSharpTest
    {
        public static string ExtractTextFromPdf(string path)
        {
            path = Directory.GetCurrentDirectory() + "\\1.pdf";
            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                return text.ToString();
            }
        }

        public static void CreatePdf(string text, string outputPath)
        {
            //// to file stream
            //var doc = new Document();
            //PdfWriter pdfWriter = PdfWriter.GetInstance(doc, new FileStream(outputPath + "/doc1.pdf", FileMode.Create));
            //doc.Open();
            //// add text
            //doc.Add(new Paragraph(text));
            //doc.Close();

            // to memory stream
            var memoryStream = new MemoryStream();
            var doc = new Document();
            PdfWriter pdfWriter = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();
            // add text
            doc.Add(new Paragraph(text));
            doc.Close();
            memoryStream.Close();
            File.WriteAllBytes(outputPath + "/doc1.pdf", memoryStream.ToArray());

            System.Console.WriteLine("write successfully");
        }
        
        // add image and rotate
        public static void CreatePdfFromImage(string imageFilename, string outputname)
        {
            // way 1
            //// create a temp pdf for rotate image
            //var imageStream = new MemoryStream();
            //var imageDocument = new Document();
            //var pdfWriter = PdfWriter.GetInstance(imageDocument, imageStream);
            //imageDocument.Open();
            //// add image
            //Image image = Image.GetInstance(imageFilename); // 1400x800 image size = 19.44 inch x 11.11 inch
            ////image.ScalePercent(36);
            ////image.SetAbsolutePosition(0, 0);
            //imageDocument.Add(image);
            //imageDocument.Close();
            //imageStream.Close();
            //var imagePdfBytes = imageStream.ToArray();
            //File.WriteAllBytes("c:/delete/doc1.pdf", imagePdfBytes);

            //// rotate image pdf and add to new pdf
            //var printableMemory = new MemoryStream();
            //Rectangle paperSize = new Rectangle((float) 4 * 72, (float) (8.5 - 1) * 72);
            //var printableDocument = new Document(paperSize);
            //PdfWriter printablePdfWriter = PdfWriter.GetInstance(printableDocument, printableMemory);
            //printableDocument.Open();
            //// scale and rotate image pdf
            //var imageLabelPdfReader= new PdfReader(imagePdfBytes);
            //PdfImportedPage imageLabelPage = printablePdfWriter.GetImportedPage(imageLabelPdfReader, 1);
            //printablePdfWriter.DirectContent.AddTemplate(imageLabelPage, 0, -1f, 1f, 0, 0, imageLabelPdfReader.GetPageSizeWithRotation(1).Width * 0.36);
            //printableDocument.Close();
            //printableMemory.Close();

            //File.WriteAllBytes(outputname, printableMemory.ToArray());

            // way2
            var imageStream = new MemoryStream();
            Rectangle paperSize = new Rectangle((float)4 * 72, (float)(8.5 - 1) * 72);
            var imageDocument = new Document(paperSize);
            var pdfWriter = PdfWriter.GetInstance(imageDocument, imageStream);
            imageDocument.Open();
            // add image
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageFilename); // 1400x800 image size = 19.44 inch x 11.11 inch
            image.ScalePercent(36);
            image.RotationDegrees = 270f;
            image.SetAbsolutePosition(0, 0);
            imageDocument.Add(image);
            imageDocument.Close();
            imageStream.Close();
            var imagePdfBytes = imageStream.ToArray();
            File.WriteAllBytes("c:/delete/doc1.pdf", imagePdfBytes);
            System.Console.WriteLine("write image to pdf successfully");
        }


        public static void CreateMergedPdfInSeparatePages(string text, string outputPath)
        {
            var url = "https://ct.soa-gw.canadapost.ca/rs/artifact/ce8323bcbbdbd32b/10000219541/0";
            // Create REST Request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            // Set Basic Authentication Header using username and password variables
            string auth = "Basic " + System.Convert.ToBase64String(Encoding.Default.GetBytes("ce8323bcbbdbd32b" + ":" + "6fa051027eecdcf9db47a0"));
            request.Headers = new WebHeaderCollection();
            request.Headers.Add("Authorization", auth);
            request.Headers.Add("Accept-Language", "en-CA");
            request.Accept = "application/pdf,application/zpl";

            // Execute REST Request
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var mediaStr = response.ContentType;

            // Write Artifact to file
            var doc = new Document();
            var copyStream = new MemoryStream();
            PdfCopyFields copy = new PdfCopyFields(copyStream);
            // get and merge label pdf
            using (var responseStream = response.GetResponseStream())
            {
                using (var stream = new MemoryStream())
                {
                    responseStream.CopyTo(stream);
                    var bytes1 = stream.ToArray();
                    //merge
                    PdfReader reader1 = new PdfReader(bytes1);
                    copy.AddDocument(reader1);
                }
            }
            // generate and merge another pdf
            using (var doc2 = new Document())
            {
                using (var stream2 = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc2, stream2);
                    doc2.Open();
                    // add text
                    doc2.Add(new Paragraph(text));
                    //
                    doc2.Close();
                    var bytes2 = stream2.ToArray();
                    // merge
                    PdfReader reader2 = new PdfReader(bytes2);
                    copy.AddDocument(reader2);
                }
            }
            // output
            copy.Close();
            doc.Close();
            File.WriteAllBytes(outputPath, copyStream.ToArray());
            System.Console.WriteLine("write successfully");
        }

        public static void Merge2PdfIn1Page(string outputFilename)
        {
            var url = "https://ct.soa-gw.canadapost.ca/rs/artifact/ce8323bcbbdbd32b/10000234993/0";
            // get pdf1
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            string auth = "Basic " + System.Convert.ToBase64String(Encoding.Default.GetBytes("ce8323bcbbdbd32b" + ":" + "6fa051027eecdcf9db47a0"));
            request.Headers = new WebHeaderCollection();
            request.Headers.Add("Authorization", auth);
            request.Headers.Add("Accept-Language", "en-CA");
            request.Accept = "application/pdf,application/zpl";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var mediaStr = response.ContentType;

            //// create (barcode + canada post label) pdf
            var printableLabelStream = new MemoryStream();
            Rectangle halfLetterPageSize = new Rectangle((float)4 * 72, (float)(8.5 - 1) * 72);
            var printableLabelDocument = new Document(halfLetterPageSize);
            PdfWriter printableLabelWriter = PdfWriter.GetInstance(printableLabelDocument, printableLabelStream);
            //PdfWriter writer1 = PdfWriter.GetInstance(doc1, new FileStream("c:/delete/rotate22.pdf", FileMode.Create));
            printableLabelDocument.Open();

            // add barcode
            printableLabelDocument.Add(Chunk.NEWLINE);
            printableLabelDocument.Add(Chunk.NEWLINE);
            Barcode128 code128 = new Barcode128();
            code128.CodeType = Barcode.CODE128;
            code128.ChecksumText = true;
            code128.GenerateChecksum = true;
            code128.Code = "RMA-KeBe";
            PdfContentByte pcb = printableLabelWriter.DirectContent;
            var barcode = code128.CreateImageWithBarcode(pcb, null, null);
            barcode.Alignment = 1;
            printableLabelDocument.Add(barcode);
            printableLabelDocument.Add(Chunk.NEWLINE);

            // add canada post label

            // load pdf to memory
            //var pureLabelReader = new PdfReader(response.GetResponseStream());
            //MemoryStream pureLabelStream = new MemoryStream();
            //PdfStamper stamper = new PdfStamper(pureLabelReader, pureLabelStream);
            //stamper.Close();
            //pureLabelStream.Close();
            //var pureLabelBytes = pureLabelStream.ToArray();
            //// read the pdf
            //var pureLabelReader2 = new PdfReader(pureLabelBytes);

            var pureLabelReader = new PdfReader(response.GetResponseStream());
            PdfImportedPage page1 = printableLabelWriter.GetImportedPage(pureLabelReader, 1);
            // rotate 90 and scale
            //printableLabelWriter.DirectContent.AddTemplate(page1, 1f, 0, 0, 1f, 0, 0);
            printableLabelWriter.DirectContent.AddTemplate(page1, 0, 0);
            printableLabelDocument.Close();
            printableLabelStream.Close();
            var printableLabelPdfBytes = printableLabelStream.ToArray();
            //File.WriteAllBytes("c:/delete/modifed_acth_label.pdf", labelPdfBytes);

            //// create instruction + (barcode+label) pdf
            Document completeLabelDocument = new Document(PageSize.LETTER);
            PdfWriter completeLabelWriter = PdfWriter.GetInstance(completeLabelDocument, new FileStream(outputFilename, FileMode.Create));
            completeLabelDocument.Open();

            // add header 
            var header = new Paragraph("Customer Care Service");
            header.Alignment = 1;
            completeLabelDocument.Add(header);
            // add deadline
            completeLabelDocument.Add(Chunk.NEWLINE);
            var deadline1 = new Phrase("All the items must be returned by ");
            var deadline2 = new Phrase("December 12, 2016", FontFactory.GetFont("Arial", 12, Font.BOLD));
            var deadline = new Paragraph();
            deadline.Add(deadline1);
            deadline.Add(deadline2);
            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100f;
            PdfPCell cell = new PdfPCell();
            cell.PaddingTop = 8;
            cell.PaddingBottom = 8;
            cell.PaddingLeft = 20;
            cell.Phrase = deadline;
            table.AddCell(cell);
            completeLabelDocument.Add(table);

            // add instructions:
            completeLabelDocument.Add(Chunk.NEWLINE);
            completeLabelDocument.Add(new Paragraph("Additional Instructions for mailing your package",
                FontFactory.GetFont("Arial", 12, Font.BOLD)));
            List list = new List(List.UNORDERED);
            list.Add("Print the label.");
            list.Add("Cut out the Merchandise Return Label.");
            list.Add("Securely pack the items to be returned in a box and include the separate barcode in the package.");
            list.Add("Affix the label squarely onto the address side of the parcel, covering up any previous delivery address and barcode without overlapping any adjacent side.");
            list.Add("Take the package to your nearest Canada Post office for delivery. No postage is necessary if the package is mailed from within Canada.");
            completeLabelDocument.Add(list);

            //// add a border way1
            //var content = writerx.DirectContent;
            //var pageBorderRect = new Rectangle(docx.PageSize);
            //pageBorderRect.Left += docx.LeftMargin;
            //pageBorderRect.Right -= docx.RightMargin;
            //pageBorderRect.Top -= docx.TopMargin;
            //pageBorderRect.Bottom += docx.BottomMargin;
            //content.SetColorStroke(BaseColor.RED);
            //content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
            //content.Stroke();

            //// add a border way2
            //PdfContentByte content = writerx.DirectContent;
            //Rectangle pageRect = docx.PageSize;
            //content.SetColorStroke(BaseColor.GRAY);

            ////content.Rectangle(pageRect.Left + 10, pageRect.Bottom + 10, pageRect.Width - 20, pageRect.Height - 20);
            //Rectangle bordeRectangle = new Rectangle(pageRect.Left + 10, pageRect.Bottom + 10, pageRect.Width - 20, pageRect.Height - 20);
            //bordeRectangle.BorderWidth = 2;
            //bordeRectangle.Border = Rectangle.BOX;
            //content.Rectangle(bordeRectangle);

            //content.Stroke();

            // add label instruction
            completeLabelDocument.Add(Chunk.NEWLINE);
            completeLabelDocument.Add(new Paragraph("Return Mailing Label", FontFactory.GetFont("Arial", 12, Font.BOLD)));
            completeLabelDocument.Add(new Paragraph("Cut this label and affix to the outside of the return package"));
            var separateLine = new Paragraph("------------------------------------------------------------------------------------------------------------------------");
            completeLabelDocument.Add(separateLine);
            completeLabelDocument.Add(Chunk.NEWLINE);

            // add and rotate label pdf
            //var printableLabelPdfReader = new PdfReader(response.GetResponseStream());
            var printableLabelPdfReader = new PdfReader(printableLabelPdfBytes);
            PdfImportedPage printableLabelPage = completeLabelWriter.GetImportedPage(printableLabelPdfReader, 1);
            // rotate 90 and scale
            //writerx.DirectContent.AddTemplate(pagex, 0, -1f * (8.0 / 6), 1f * (8.0 / 6), 0, 0, pdfreaderx.GetPageSizeWithRotation(1).Height);
            completeLabelWriter.DirectContent.AddTemplate(printableLabelPage, 0, -1f, 1f, 0, 0, printableLabelPdfReader.GetPageSizeWithRotation(1).Height * 0.8);
            completeLabelDocument.Close();

            //memoryStream.Close();
            //pdf1Bytes = memoryStream.ToArray();

            // rotate 90 and save to file
            //using (var fs = new FileStream("c:/delete/rotate1.pdf", FileMode.Create))
            //{
            //    PdfReader pdf1Reader = new PdfReader(response.GetResponseStream());
            //    PdfDictionary page = pdf1Reader.GetPageN(1);
            //    PdfNumber rotation = page.GetAsNumber(PdfName.ROTATE);
            //    //int rotateDegree = rotation == null ? 90 : (rotation.IntValue + 90) % 360;
            //    page.Put(PdfName.ROTATE, new PdfNumber(90));
            //    // persist changes
            //    PdfStamper stamper = new PdfStamper(pdf1Reader, fs);
            //    stamper.Close();
            //}

            // get pdf2
            //var document = new Document();
            //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("c:/delete/merge1.pdf", FileMode.Create));
            //document.Open();

            //PdfContentByte cb = writer.DirectContent;
            //PdfImportedPage page1;
            //PdfImportedPage page2;

            //PdfReader reader1 = new PdfReader(pdf1Bytes);
            //PdfReader reader2 = new PdfReader("c:/delete/page2.pdf");
            //page1 = writer.GetImportedPage(reader1, 1);
            //page2 = writer.GetImportedPage(reader2, 1);
            //cb.AddTemplate(page1, 0, 0);
            //cb.AddTemplate(page2, 0, 0);
            //document.Close();


            //System.Console.WriteLine("write successfully");

        }

        public static void GenerateBarCode(string outputFilename)
        {
            Barcode128 code128 = new Barcode128();
            code128.CodeType = Barcode.CODE128;
            code128.ChecksumText = true;
            code128.GenerateChecksum = true;
            code128.Code = "Hell, Andy Feng!";
            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White));
            bm.Save(outputFilename, System.Drawing.Imaging.ImageFormat.Gif);
        }

        public static void GenerateRmaLabel(string outputFilename)
        {
            // to byte array
            var document = new Document(PageSize.LETTER, 50, 50, 50, 50);
            using (var memoryStream = new MemoryStream())
            {
                var writer = PdfWriter.GetInstance(document, memoryStream);
                // add header
                writer.PageEvent = new PdfPageEvents()
                {
                    Header = "Customer Care Service"
                };
                document.Open();
                //// add header
                //var header = new Paragraph("Customer Care Service");
                //header.Alignment = 1;
                //doc2.Add(header);
                // add title
                document.Add(Chunk.NEWLINE);
                Font arial = FontFactory.GetFont("Arial", 14, Font.BOLD);
                var title = new Paragraph("Return Authorization Label", arial);
                title.Alignment = 0;
                document.Add(title);
                // add instruction
                var instruction = new Paragraph("Cut this and place inside the return package");
                document.Add(instruction);
                // add horizontal line
                document.Add(Chunk.NEWLINE);
                Paragraph horizontalLine = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                document.Add(horizontalLine);

                // add barcode
                document.Add(Chunk.NEWLINE);
                document.Add(Chunk.NEWLINE);
                var barcode = new Barcode128();
                barcode.CodeType = Barcode.CODE128;
                barcode.ChecksumText = true;
                barcode.GenerateChecksum = true;
                barcode.Code = "RMA-KeBe";
                PdfContentByte pcb = writer.DirectContent;
                var barcodeImage = barcode.CreateImageWithBarcode(pcb, null, null);
                barcodeImage.Alignment = 1; // center
                // scale the barcode
                barcodeImage.ScalePercent(150.0f);
                document.Add(barcodeImage);
                document.Add(Chunk.NEWLINE);

                // add table
                document.Add(Chunk.NEWLINE);
                PdfPTable table = new PdfPTable(3);
                // add row 1
                PdfPCell cell1 = new PdfPCell();
                //cell1.MinimumHeight = 30.0f;
                cell1.PaddingTop = 8;
                cell1.PaddingBottom = 8;
                cell1.Phrase = new Phrase("Sku", FontFactory.GetFont("Arial", 12, Font.BOLD));
                table.AddCell(cell1);

                table.AddCell(new PdfPCell()
                {
                    PaddingTop = 8,
                    PaddingBottom = 8,
                    Phrase = new Paragraph("Item description", FontFactory.GetFont("Arial", 12, Font.BOLD))
                });
                table.AddCell(new PdfPCell()
                {
                    PaddingTop = 8,
                    PaddingBottom = 8,
                    Phrase = new Paragraph("Quantity", FontFactory.GetFont("Arial", 12, Font.BOLD))
                });
                //table.AddCell("Sku");
                //table.AddCell("Item description");
                //table.AddCell("Quantity");

                // add row 2
                table.AddCell("N403-KS-BK-2");
                table.AddCell("Arc 7 Black");
                table.AddCell("1");
                table.AddCell("N403-ATT-BK-2");
                table.AddCell("Arc 7 Sleepcover");
                table.AddCell("1");
                document.Add(table);
                document.Close();
                var bytes = memoryStream.ToArray();
                var fs2 = new FileStream(outputFilename, FileMode.Create);
                fs2.Write(bytes, 0, bytes.Length);
                fs2.Close();
            }
            System.Console.WriteLine("write rma successfully");

        }

        public static void CreatePdfWithFont()
        {
            Document doc = new Document(PageSize.LETTER, 10, 10, 20, 10);
            doc.Open();
            int totalfonts = FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");

            StringBuilder sb = new StringBuilder();

            foreach (string fontname in FontFactory.RegisteredFonts)
            {
                sb.Append(fontname + "\n");
            }

            doc.Add(new Paragraph("All Fonts:\n" + sb.ToString()));
            Font arial = FontFactory.GetFont("Arial", 12, Font.BOLD);
            Font verdana = FontFactory.GetFont("Verdana", 20, Font.BOLD | Font.UNDERLINE, new BaseColor(255, 0, 0));

            //rdana.SetStyle(4);
            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(verdana);
            using (FileStream msReport = new FileStream("c:/delete/" + DateTime.Now.Ticks + ".pdf", FileMode.Create))
            {
                doc.Open();

                PdfWriter pdfWriter = PdfWriter.GetInstance(doc, msReport);
                PdfPCell cell;
                PdfPTable table = new PdfPTable(1);
                //
                cell = new PdfPCell(new Phrase("User I", new Font(verdana)));
                cell.Colspan = 4;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                cell.VerticalAlignment = 1;
                cell.Border = 0;
                table.AddCell(cell);
                //
                PdfPCell cell2 = new PdfPCell(new Phrase("User I", new Font(arial)));
                table.AddCell(cell2);
                doc.Open();
                doc.Add(table);
                doc.Close();
            }
        }

        public static void GenerateCompleteLabelFromImage(string imageFilename, string outputFilename)
        {
            var imageBytes = File.ReadAllBytes(imageFilename);

            //// create (barcode + carrier label) pdf
            var printableLabelStream = new MemoryStream();
            Rectangle halfLetterPageSize = new Rectangle((float)4 * 72, (float)(8.5 - 1) * 72);
            var printableLabelDocument = new Document(halfLetterPageSize); // image + barcode document
            PdfWriter printableLabelWriter = PdfWriter.GetInstance(printableLabelDocument, printableLabelStream);
            //PdfWriter writer1 = PdfWriter.GetInstance(doc1, new FileStream("c:/delete/rotate22.pdf", FileMode.Create));
            printableLabelDocument.Open();

            // add barcode
            printableLabelDocument.Add(Chunk.NEWLINE);
            printableLabelDocument.Add(Chunk.NEWLINE);
            Barcode128 code128 = new Barcode128();
            code128.CodeType = Barcode.CODE128;
            code128.ChecksumText = true;
            code128.GenerateChecksum = true;
            code128.Code = "RMA-KeBe";
            PdfContentByte pcb = printableLabelWriter.DirectContent;
            var barcode = code128.CreateImageWithBarcode(pcb, null, null);
            barcode.Alignment = 1;
            printableLabelDocument.Add(barcode);
            printableLabelDocument.Add(Chunk.NEWLINE);

            // add pure carrier label image
            var imageStream = new MemoryStream();
            Rectangle paperSize = new Rectangle((float)4 * 72, (float)(8.5 - 1) * 72);
            var imageDocument = new Document(paperSize);
            var pdfWriter = PdfWriter.GetInstance(imageDocument, imageStream);
            imageDocument.Open();
            // add image
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageFilename); // 1400x800 image size = 19.44 inch x 11.11 inch
            image.ScalePercent(36);
            image.RotationDegrees = 270f;
            image.SetAbsolutePosition(0, 0);
            imageDocument.Add(image);
            imageDocument.Close();
            imageStream.Close();
            var imagePdfBytes = imageStream.ToArray();

            var imageLabelPdfReader = new PdfReader(imagePdfBytes);
            PdfImportedPage page1 = printableLabelWriter.GetImportedPage(imageLabelPdfReader, 1);
            printableLabelWriter.DirectContent.AddTemplate(page1, 0, -50);

            printableLabelDocument.Close();
            printableLabelStream.Close();
            var printableLabelPdfBytes = printableLabelStream.ToArray();
            //File.WriteAllBytes("c:/delete/temp.pdf", printableLabelPdfBytes);

            //// create instruction + (barcode+label) pdf
            Document scmReturnDocument = new Document(PageSize.LETTER);
            PdfWriter scmReturnWriter = PdfWriter.GetInstance(scmReturnDocument, new FileStream(outputFilename, FileMode.Create));
            scmReturnDocument.Open();

            // add header 
            var header = new Paragraph("Customer Care Service");
            header.Alignment = 1;
            scmReturnDocument.Add(header);
            // add deadline
            scmReturnDocument.Add(Chunk.NEWLINE);
            var deadline1 = new Phrase("All the items must be returned by ");
            var deadline2 = new Phrase("December 12, 2016", FontFactory.GetFont("Arial", 12, Font.BOLD));
            var deadline = new Paragraph();
            deadline.Add(deadline1);
            deadline.Add(deadline2);
            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100f;
            PdfPCell cell = new PdfPCell();
            cell.PaddingTop = 8;
            cell.PaddingBottom = 8;
            cell.PaddingLeft = 20;
            cell.Phrase = deadline;
            table.AddCell(cell);
            scmReturnDocument.Add(table);

            // add instructions:
            scmReturnDocument.Add(Chunk.NEWLINE);
            scmReturnDocument.Add(new Paragraph("Additional Instructions for mailing your package",
                FontFactory.GetFont("Arial", 12, Font.BOLD)));
            List list = new List(List.UNORDERED);
            list.Add("Print the label.");
            list.Add("Cut out the Merchandise Return Label.");
            list.Add("Securely pack the items to be returned in a box and include the separate barcode in the package.");
            list.Add("Affix the label squarely onto the address side of the parcel, covering up any previous delivery address and barcode without overlapping any adjacent side.");
            list.Add("Take the package to your nearest Canada Post office for delivery. No postage is necessary if the package is mailed from within Canada.");
            scmReturnDocument.Add(list);

            // add label instruction
            scmReturnDocument.Add(Chunk.NEWLINE);
            scmReturnDocument.Add(new Paragraph("Return Mailing Label", FontFactory.GetFont("Arial", 12, Font.BOLD)));
            scmReturnDocument.Add(new Paragraph("Cut this label and affix to the outside of the return package"));
            var separateLine = new Paragraph("------------------------------------------------------------------------------------------------------------------------");
            scmReturnDocument.Add(separateLine);
            scmReturnDocument.Add(Chunk.NEWLINE);

            // add and rotate label pdf
            //var printableLabelPdfReader = new PdfReader(response.GetResponseStream());
            var printableLabelPdfReader = new PdfReader(printableLabelPdfBytes);
            PdfImportedPage printableLabelPage = scmReturnWriter.GetImportedPage(printableLabelPdfReader, 1);
            // rotate 90 and scale
            //writerx.DirectContent.AddTemplate(pagex, 0, -1f * (8.0 / 6), 1f * (8.0 / 6), 0, 0, pdfreaderx.GetPageSizeWithRotation(1).Height);
            scmReturnWriter.DirectContent.AddTemplate(printableLabelPage, 0, -1f, 1f, 0, 0, printableLabelPdfReader.GetPageSizeWithRotation(1).Height * 0.8);
            scmReturnDocument.Close();
        }
    }
}
