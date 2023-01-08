using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnidecodeSharpFork;

namespace Console.Convert
{
    public class Utf8Test
    {
        public static void CharaterToUtf8(string input)
        {
            byte[] toBytes = Encoding.UTF8.GetBytes(input);
            string output = Encoding.ASCII.GetString(toBytes, 0, toBytes.Length);
            var a = input.Unidecode();
        System.Console.WriteLine(a);

        }
}
}
