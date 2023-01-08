using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class HelpAttribute : Attribute
    {
        public HelpAttribute(string s)
        {
            this.Company = s;
        }
        public string Company { get; set; }
    }

    public class CheckAttribute : Attribute
    {
        private bool Val = false;
        public CheckAttribute(bool val)
        {
            Val = val;
        }
        public bool status
        {
            get { return Val; }
        }
    }

    [Check(false)]
    [Help("HCL Technology from class")]
    public class HelpClass
    {
        public HelpClass(string name, string country)
        {
            this.EmpName = name;
            this.Country = country;
        }

        public string Details()
        {
            //string str = EmpName + "-" + Country;
            //return str;
            string str = null;
            Type type = this.GetType();
            CheckAttribute[] attrib = (CheckAttribute[])type.GetCustomAttributes(typeof(CheckAttribute), false);
            if (attrib[0].status == true)
            {
                str = EmpName + "-" + Country;
            }
            else
            {
                str = "Hi " + EmpName;
            }
            return str;
        }
        [Help("HCL Technology from property")]
        public string EmpName { get; set; }
        private string Country;
    }
}
