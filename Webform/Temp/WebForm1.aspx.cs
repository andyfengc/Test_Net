using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Webform.Temp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbl1.Text = "!!";
            }

        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            lbl1.Text = "changed";
        }

        protected void btnHtmlChange_Click(object sender, EventArgs e)
        {
            lbl1.Text = "html changed";
        }

        protected void txt1_TextChanged(object sender, EventArgs e)
        {
            lbl1.Text = txt1.Text;
        }
    }
}