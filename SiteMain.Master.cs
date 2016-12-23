using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using AskAndAnswer.ClassCode;
namespace AskAndAnswer
{
    public partial class SiteMain : System.Web.UI.MasterPage
    {
        protected string cssJquery = DynControls.EncodeScript("/js/jquery-ui-1.12.1.uilightness/jquery-ui.css", true);
        protected string css2Col = DynControls.EncodeScript("/stylesheets/2colscreen.css", true);

        protected string jsJquery = DynControls.EncodeScript("/js/jquery-ui-1.12.1.uilightness/external/jquery/jquery.js");
        protected string jsJqueryUI = DynControls.EncodeScript("/js/jquery-ui-1.12.1.uilightness/jquery-ui.js");
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}