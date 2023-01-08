using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace Console.Zip
{
    public class DotNetZipTest
    {
        public static void ZipFiles(string outfile, params string[] infiles)
        {
            using (var zip = new ZipFile())
            {
                foreach (var infile in infiles)
                {
                    zip.AddFile(infile);
                }
                zip.Save(outfile);
            }
            System.Console.WriteLine("Zipped successfully");
        }

        public static void ExtractZip(string zipfile, string outpath)
        {
            using (var zip = ZipFile.Read(zipfile))
            {
                foreach (var entry in zip)
                {
                    entry.Extract(outpath, ExtractExistingFileAction.OverwriteSilently);
                }
            }
            System.Console.WriteLine("Extracted successfully");
        }
        public static byte[] ZipStreams(params byte[][] inBytes)
        {
            var count = 0;
            // way1
            //using (var ms = new MemoryStream())
            //{
            //    using (var s = new ZipOutputStream(ms))
            //    {
            //        foreach (var inByte in inBytes)
            //        {
            //            s.PutNextEntry(String.Format("entry{0}.pdf", count++));
            //            s.Write(inByte, 0, inByte.Length);
            //        }
            //    }

            //    var result = ms.ToArray();
            //    return result;
            //}

            // way2
            using (var ms = new MemoryStream())
            {
                using (ZipFile zip = new ZipFile())
                {
                    foreach (var inByte in inBytes)
                    {
                        zip.AddEntry("pdf-" + count++ + ".pdf", inByte);
                    }
                    zip.Save(ms);
                    var bytes = ms.ToArray();
                    return bytes;
                }
            }
        }

        public static IEnumerable<byte[]> UnZipStreams(byte[] inBytes)
        {
            var outByteList = new List<byte[]>();
            int count = 0;
            // unzip
            using (var inStream = new MemoryStream(inBytes))
            {
                using (var zip = ZipFile.Read(inStream))
                {
                    foreach (var entry in zip)
                    {
                        //entry.Extract("c:/delete/dep", ExtractExistingFileAction.OverwriteSilently);
                        using (var stream = entry.OpenReader())
                        {
                            using (var memorySteam = new MemoryStream())
                            {
                                stream.CopyTo(memorySteam);
                                var bytes = memorySteam.ToArray();
                                File.WriteAllBytes("c:/delete/dep" + count++ + ".pdf", bytes);
                            }
                        }
                    }
                }
            }
            return outByteList;
        }
    }
}
