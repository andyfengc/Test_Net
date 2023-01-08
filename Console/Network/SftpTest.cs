using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace Console.Network
{
    public class SftpTest
    {

        public static void ListDirectory()
        {
            string host = "swdrop.shop.ca";
            int port = 22;
            string username = "kebe";
            string password = "123456";
            string remoteDirectory = "/fromclient/test/";
            using (var sftp = new SftpClient(host, port, username, password))
            {
                sftp.Connect();
                var files = sftp.ListDirectory(remoteDirectory);
                foreach (var file in files)
                {
                    System.Console.WriteLine(file.FullName);
                }
            }
        }

        public static void UploadFile()
        {
            string host = "swdrop.shop.ca";
            int port = 22;
            string username = "kebe";
            string password = "123456";
            string localFile = @"d:/delete/test.txt";
            string remoteDirectory = "/fromclient/test/";

            using (var sftp = new SftpClient(host, port, username, password))
            {
                sftp.Connect();
                System.Console.WriteLine("Connected to {0}", host);

                sftp.ChangeDirectory(remoteDirectory);
                System.Console.WriteLine("Changed directory to {0}", remoteDirectory);

                var listDirectory = sftp.ListDirectory(remoteDirectory);
                System.Console.WriteLine("Listing directory:");
                foreach (var file in listDirectory)
                {
                    System.Console.WriteLine(" - " + file.Name);
                }

                //using (var file = File.OpenRead(localFile))
                //{
                //    // overrite exising file if same name
                //    sftp.UploadFile(file, Path.GetFileName(localFile));
                //}

                using (var fileStream = new FileStream(localFile, FileMode.Open))
                {
                    System.Console.WriteLine("Uploading {0} ({1:N0} bytes)",
                                        localFile, fileStream.Length);
                    sftp.BufferSize = 4 * 1024; // bypass Payload error large files
                    sftp.UploadFile(fileStream, Path.GetFileName(localFile));
                }

                sftp.Disconnect();
            }
        }

        public static void DownloadFile()
        {
            string host = "swdrop.shop.ca";
            string username = "kebe";
            string password = "123456";
            string localFile = @"d:/delete/download.txt";
            string remoteFile = "/fromclient/test/test.txt";

            using (var sftp = new SftpClient(host, username, password))
            {
                sftp.Connect();

                //using (var file = File.OpenWrite(localFile)) // not overwrite
                //{
                //    sftp.DownloadFile(remoteFile, file);
                //}
                using (var file = new FileStream(localFile, FileMode.Create, FileAccess.Write))// we specify overwrite file with same name
                {
                    sftp.DownloadFile(remoteFile, file);
                }
                sftp.Disconnect();
            }
        }

        public static void MoveFile()
        {
            string host = "swdrop.shop.ca";
            string username = "kebe";
            string password = "123456";
            string remoteFile = "/fromclient/test/test.txt";
            string remoteDestFile = "/fromclient/test/archive/test.txt";


            using (var sftp = new SftpClient(host, username, password))
            {
                sftp.Connect();

                SftpFile file = sftp.Get(remoteFile);
                file.MoveTo(remoteDestFile);

                sftp.Disconnect();
            }
        }

        public static void ReadXmlFile()
        {
            string host = "swdrop.shop.ca";
            int port = 22;
            string username = "k3b3";
            string password = "123456";
            string remoteDirectory = "/fromclient/test/";

            using (var sftp = new SftpClient(host, port, username, password))
            {
                sftp.Connect();
                var files = sftp.ListDirectory(remoteDirectory);
                // load file names
                foreach (var file in files)
                {
                    string filename = file.Name;
                    // read *.xml
                    if (filename.ToUpper().EndsWith(".XML"))
                    {
                        MemoryStream stream = new MemoryStream();
                        StreamReader reader = new StreamReader(stream);
                        sftp.DownloadFile(remoteDirectory + filename, stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        System.Console.WriteLine("File Name: " + filename);
                        System.Console.WriteLine("File Size: " + stream.Length);
                        System.Console.WriteLine("" + reader.ReadToEnd());
                        System.Console.WriteLine();
                    }
                }
                sftp.Disconnect();
            }
        }
    }
}
