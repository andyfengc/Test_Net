using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.Log
{
    public class Logger
    {
        private static Logger logger;
        private static string filepath;
        private Logger()
        {

        }
        public static Logger GetInstance(string path)
        {
            if (path == null)
            {
                throw new Exception("Missing logger path");
            }
            if (logger == null)
            {
                logger = new Logger();
                filepath = path + "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            }
            return logger;
        }
        public void Log(string message)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filepath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filepath));
            }
            using (var fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                using (var writer = new StreamWriter(fs))
                {
                    writer.BaseStream.Seek(0, SeekOrigin.End);
                    writer.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")} - {message}");
                }
            }
        }
    }
}
