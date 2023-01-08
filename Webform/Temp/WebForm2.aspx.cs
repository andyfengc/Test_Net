using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Webform.Temp
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lstDepartment.Items.AddRange(new ListItem[]
                {
                    new ListItem() {Value = "1", Text = "IT"}, 
                    new ListItem() {Value = "2", Text = "Supply chain"}, 
                    new ListItem() {Value = "3", Text = "Big data"}, 
                });
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var name = txtName.Text;

            var dept = "";
            if (lstDepartment.SelectedItem != null)
            {
                dept = lstDepartment.SelectedItem.Text;
            }
            lblResult.Text = name + " " + dept;
        }
    }
}