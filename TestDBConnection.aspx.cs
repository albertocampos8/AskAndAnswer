using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DB;
using AskAndAnswer.ClassCode;

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

        protected void btnEmail_Click(object sender, EventArgs e)
        {
            try
            {
                clsEMail m = new clsEMail(txtServer.Text);
                if (!m.Send("alberto.campos@broadcom.com","alberto.campos@broacom.com","TEST SENDER","Does this work?",
                    "Sent from " + m.SMTPServerName +"."))
                {
                    Response.Write(m.ErrorMessage);
                }
            } catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}