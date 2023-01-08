using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = System.Console;

namespace Console.Db
{
    public class Asc
    {
         public static void TestNonprintableChars()
        {
            char[] chars = new char[] { '\x0', '\x1', '\x2', '\x3', '\x4', '\x5', '\x6', '\x7', '\x8', '\x9', '\xA', '\xB', '\xC', '\xD', '\xE', '\xF', '\x10', '\x11', '\x12', '\x13', '\x14', '\x15', '\x16', '\x17', '\x18', '\x19', '\x1A', '\x1B', '\x1C', '\x1D', '\x1E', '\x1F' };
            foreach (var ch in chars)
            {
                TestNonprintableChar(ch);
                SaveAsc(ch);
                System.Console.WriteLine();
            }
        }

       public static void TestNonprintableChar(char ch)
        {
            System.Console.WriteLine("ASCII is: " + (int)ch);
            System.Console.WriteLine("character is: " + ch);
            var str = "a" + ch + "b" + ch + "c" + ch + "d";
            System.Console.WriteLine("string is: " + str);
            System.Console.WriteLine("string length is: " + str.Length);
            var chArray = str.Split(ch);
            System.Console.WriteLine("char array size is: " + chArray.Length);
            foreach (var c in chArray)
            {
                //System.Console.WriteLine(c);
            }
        }


        public static void SaveAsc(char ch)
        {
            using (var db = new MssqlDbContext())
            {
                Temp t = new Temp()
                {
                    Content = "some contents..." + ch + "...",
                    DateTime = DateTime.Now
                };
                db.Temps.Add(t);
                db.SaveChanges();
                //System.Console.Write("1 record inserted");
            }
        }

        public static void ReadAsc()
        {
            using (var db = new MssqlDbContext())
            {
                var temps = db.Temps.ToList();
                foreach (var temp in temps)
                {
                   System.Console.WriteLine(temp.Content);
                }
            }
        }

        public static void WriteAsc()
        {
            string contents = "";
            using (var db = new MssqlDbContext())
            {
                var temps = db.Temps.ToList();
                foreach (var temp in temps)
                {
                    contents +=temp.Content+"\r\n";
                }
            }
             File.WriteAllText(@"d:\asc.txt", contents);
       }
    }

    public class Temp
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
    }
}
