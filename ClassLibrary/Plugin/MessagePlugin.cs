using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForm.Plugin;

namespace ClassLibrary.Plugin
{
    public class MessagePlugin : IMsgPlug
    {
        public void OnShowDlg()
        {
            Console.WriteLine("控制台调用插件的OnShowDlg方法");
        }

        public string OnShowInfo()
        {
            return "myConsole";
        }

    }
}
