using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Console.Misc
{
    public class InitOrder
    {
        public InitOrder()
        {
            MyClass3 my3 = new MyClass3();
            System.Console.WriteLine();
            MyClass3 my = new MyClass3(3, 4);
            System.Console.ReadKey();
        }
    }

    public class MyClass
    {
        private OtherClass otherClass = new OtherClass();
        private OtherClass2 otherClass2;

        static MyClass()
        {
            System.Console.WriteLine("MyClass static constructor");
        }

        public MyClass()
        {
            otherClass2 = new OtherClass2();
            System.Console.WriteLine("MyClass default constructor");
        }
        public MyClass(int a, int b)
        {
            System.Console.WriteLine("MyClass parameterized constructor:a={0}, b={1}.", a, b);
        }
    }

    public class MyClass2 : MyClass
    {
        public MyClass2()
        {
            System.Console.WriteLine("MyClass2 default constructor");
        }
        public MyClass2(int a, int b)
        {
            System.Console.WriteLine("MyClass2 parameterized constructor:a={0}, b={1}", a, b);
        }
    }

    public class MyClass3 : MyClass2
    {

        public MyClass3()
        {
            System.Console.WriteLine("MyClass3 default constructor");
        }
        public MyClass3(int a, int b)
        {
            System.Console.WriteLine("MyClass3 parameterized constructor:a={0}, b={1}.", a, b);
        }
    }

    public class OtherClass
    {
        static OtherClass()
        {
            System.Console.WriteLine("    OtherClass static constructor");
        }
        public OtherClass()
        {
            System.Console.WriteLine("    OtherClass default constructor");
        }
    }

    public class OtherClass2
    {
        static OtherClass2()
        {
            System.Console.WriteLine("    OtherClass2 static constructor");
        }
        public OtherClass2()
        {
            System.Console.WriteLine("    OtherClass2 default constructor");
        }
    }
}
