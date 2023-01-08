using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm.Plugin
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// 存放插件的集合
        /// </summary>
        private ArrayList plugins = new ArrayList();
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 载入所有插件
        /// </summary>
        private void LoadAllPlugs()
        {
            //获取插件目录(plugins)下所有文件
            string[] files = Directory.GetFiles(Application.StartupPath + @"\plugins");
            foreach (string file in files)
            {
                if (file.ToUpper().EndsWith(".DLL"))
                {
                    try
                    {
                        //载入dll
                        Assembly ab = Assembly.LoadFrom(file);
                        Type[] types = ab.GetTypes();
                        foreach (Type t in types)
                        {
                            //如果某些类实现了预定义的IMsg.IMsgPlug接口，则认为该类适配与主程序(是主程序的插件)
                            if (t.GetInterface("IMsgPlug") != null)
                            {
                                plugins.Add(ab.CreateInstance(t.FullName));
                                listBox1.Items.Add(t.FullName);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }



        private void btnLoadPlug_Click(object sender, EventArgs e)
        {
            LoadAllPlugs();
        }

        //调用插件的方法
        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == -1) return;
            object selObj = this.plugins[this.listBox1.SelectedIndex];
            Type t = selObj.GetType();
            MethodInfo OnShowDlg = t.GetMethod("OnShowDlg");
            MethodInfo OnShowInfo = t.GetMethod("OnShowInfo");

            OnShowDlg.Invoke(selObj, null);
            object returnValue = OnShowInfo.Invoke(selObj, null);
            this.lblMsg.Text = returnValue.ToString();

        }

    }
}
