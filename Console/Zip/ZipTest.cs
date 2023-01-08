using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using java.io;
using Console = System.Console;

namespace Console.Zip
{
    public class ZipTest
    {
        public static void ZipFiles(string outfile, params string[] infiles)
        {
            using (var zip = ZipFile.Open(outfile, ZipArchiveMode.Create))
            {
                foreach (var infile in infiles)
                {
                    zip.CreateEntryFromFile(infile, Path.GetFileName(infile), CompressionLevel.Optimal);
                }
            }
        }

        public static void DisplayZip(string zipfile)
        {
            var zip = ZipFile.OpenRead(zipfile);
            foreach (var entry in zip.Entries)
            {
                System.Console.WriteLine(entry.FullName);
            }
        }

        public static void ExtractZip(string zipfile, string outpath)
        {
            ZipFile.ExtractToDirectory(zipfile, outpath);
            System.Console.WriteLine("Extracted successfully");
        }

        // buggy, gzipstream doesn't support zip multiple files
        public static byte[] ZipStreams(params byte[][] inBytes)
        {
            // zip
            using (var outStream = new MemoryStream())
            {
                using (var zipStream = new GZipStream(outStream, CompressionMode.Compress))
                {
                    foreach (var inByte in inBytes)
                    {
                        using (var memoryStream = new MemoryStream(inByte))
                        {
                            memoryStream.CopyTo(zipStream);
                            //zipStream.Write(inByte, 0, inByte.Length);
                        }
                    }
                }
                var bytes = outStream.ToArray();
                return bytes;
            }
            

            //var bytes = File.ReadAllBytes("c:\\Delete\\rma\\rma_label.pdf");
            //using (FileStream fs = new FileStream("c:/delete/1/1.zip", FileMode.CreateNew))
            //using (GZipStream zipStream = new GZipStream(fs, CompressionMode.Compress, false))
            //{
            //    zipStream.Write(bytes, 0, bytes.Length);
            //}
            //return null;;
        }
        // buggy, gzipstream doesn't support zip multiple files
        public static IEnumerable<byte[]> UnzipStreams(byte[] inBytes)
        {
            var outByteList = new List<byte[]>();
            // unzip
            using (var inStream = new MemoryStream(inBytes))
            {
                using (var zipStream = new GZipStream(inStream, CompressionMode.Decompress))
                {
                    using (var outStream = new MemoryStream())
                    {
                        zipStream.CopyTo(outStream);
                        outByteList.Add(outStream.ToArray());
                    }
                }
            }
            return outByteList;
        }

    }
}
