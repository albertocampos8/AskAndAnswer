using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DB;

namespace InvTracker
{
    public partial class TestDBConnection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTestDB_Click(object sender, EventArgs e)
        {
            clsDB db = new clsDB("");
            if (db.TestConnection())
            {
                lblTestResult.Text = "Test OK";
            }
            else
            {
                lblTestResult.Text = "Test Failed.";
            }
        }
    }
}