using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm.Plugin
{
    public partial class MsgDialog : Form, IMsgPlug
    {
        public MsgDialog()
        {
            InitializeComponent();
        }

        public void OnShowDlg()
        {
            this.Text = "插件子窗体";
            this.ShowDialog();//调用Form的ShowDialog,显示窗体
        }

        public string OnShowInfo()
        {
            return "MyDlg";
        }
    }
}
