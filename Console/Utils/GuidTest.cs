using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.Utils
{
    public class GuidTest
    {
        public static void Test()
        {
            System.Console.WriteLine(Guid.NewGuid());
            System.Console.WriteLine(Guid.NewGuid().ToString("N"));// only alphenumeric
            System.Console.WriteLine(Guid.NewGuid().ToString("D"));
            System.Console.WriteLine(Guid.NewGuid().ToString("B"));
            System.Console.WriteLine(Guid.NewGuid().ToString("P"));
        }
    }
}
