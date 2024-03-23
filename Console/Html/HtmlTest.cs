using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreMailer.Net;
namespace Console.Html
{
    internal class HtmlTest
    {
        public static string ToCss(string htmlSource)
        {
            var result = PreMailer.Net.PreMailer.MoveCssInline(htmlSource);
            return result.ToString() ;
        }
    }
}
