using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Console.Text
{
    public class RegularExpressionTest
    {
        public static void TestXmlFileName()
        {
            string pattern = @"^[\w\d_]*\.xml$";
            Regex regex = new Regex(pattern);
            System.Console.WriteLine("Is xml file? " + regex.IsMatch("EXAMPLE_20150428025309_order.xml"));
        }
    }
}
