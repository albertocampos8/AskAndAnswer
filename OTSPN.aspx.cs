using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AskAndAnswer.ClassCode;

namespace AskAndAnswer
{
    public partial class OTSPN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check for any url encoded parameters
            string foundIDData = "";
            if (Request.QueryString.HasKeys())
            {
                try
                {
                    string targetID = Request.QueryString["ID"];
                    CustomCode x = new CustomCode();
                    foundIDData = x.getHTMLForPartNumberID(targetID);
                    otsdivs.Controls.Add(new LiteralControl(foundIDData));
                    foreach (Control c in divMenuButtons.Controls)
                    {
                        divMenuButtons.Controls.Remove(c);
                    }
                    return;
                } catch (Exception ex)
                {

                }
            } 
            //Create three panels and add them to the existing div
            Panel divOTSNew = new Panel();
            divOTSNew.ID = "divOTSNew";
            divOTSNew.Style.Add(HtmlTextWriterStyle.Display, "none");
            otsdivs.Controls.Add(divOTSNew);

            //There are two panels in divOTSNew: divOTSNewIn and divOTSNewOut

            Panel divOTSNewIn = new Panel();
            divOTSNewIn.ID = "divOTSNewIn";
            //Get the input controls from the database for divOTSNewIn...
            DynControls.GenerateControlsFromDatabase(DBK.AppKeys.GET_NEWOTSPN, divOTSNewIn);
            //Add a Submit button
            divOTSNewIn.Controls.Add(DynControls.html_button("btnOTSNewIn", "SUBMIT", "inputButton",
                true, AAAK.DISPLAYTYPES.BLOCK, "Create an OTS Part Number", "frmNewOTS"));
            //...and add a div for the ajax output
            Panel divOTSNewOut = new Panel();
            divOTSNewOut.ID = "divOTSNewOut";
            divOTSNewOut.Controls.Add(new LiteralControl("<p>Enter the information on the left, then press Submit to get your new OTS Part Number.</p>"));
            //Add these two sub panels
            divOTSNew.Controls.Add(divOTSNewIn);
            divOTSNew.Controls.Add(divOTSNewOut);
            //***** End divOTSNew

            //***** Start divOTSFind
            Panel divOTSFind = new Panel();
            divOTSFind.ID = "divOTSFind";
            divOTSFind.Style.Add(HtmlTextWriterStyle.Display, "none");
            //divOTSFind.Style.Add(HtmlTextWriterStyle.OverflowX, "auto");
            otsdivs.Controls.Add(divOTSFind);
            //Now: The html in this div is simple, but as the user make selections, the AJAX calls increase the 
            //complexity of the children's html.
            //This div has 3 divs.
            ///divOTSFind Child Div 1:
            Panel divSearch = new Panel();
            divSearch.ID = "divSearch";

            DynControls.GenerateControlsFromDatabase(DBK.AppKeys.SEARCH_OTS, divSearch,null,"",-1,true);
            //Create the search button
            string btnSearchHtmlString = DynControls.html_button_string("btnLook", "SEARCH", "searchButton",
                true, AAAK.DISPLAYTYPES.BLOCK, form: "frmSearchOTS");
            divSearch.Controls.Add(new LiteralControl(btnSearchHtmlString));

            ///divOTSFind Child Div 2:
            ///The message div
            Panel divSearchMsg = new Panel();
            divSearchMsg.Style.Add(HtmlTextWriterStyle.Display, "block");
            divSearchMsg.ID = "divMessage";
            divSearchMsg.Controls.Add(new LiteralControl("<p " + DynControls.encodeProperty("id", "searchmsg") + ">" +
                "Enter search criteria above to find Part Number Information.</p>"));
            ///divOTSFind Child Div 3:
            ///
            Panel divLook = new Panel();
            divLook.Style.Add(HtmlTextWriterStyle.Display, "block");
            divLook.Style.Add(HtmlTextWriterStyle.OverflowX, "auto");
            divLook.ID = "divLook";
            /// divLook's html will be determine when user presses SEARCH

            ///Add the three divs to divOTSFind
            divOTSFind.Controls.Add(divSearch);
            divOTSFind.Controls.Add(new LiteralControl("<div></div>"));
            divOTSFind.Controls.Add(divSearchMsg);
            divOTSFind.Controls.Add(divLook);
            
            Panel divOTSAdmin = new Panel();
            divOTSAdmin.ID = "divOTSAdmin";
            divOTSAdmin.Controls.Add(new LiteralControl("<p>Admin</p>"));
            divOTSAdmin.Style.Add(HtmlTextWriterStyle.Display, "none");
            otsdivs.Controls.Add(divOTSAdmin);
        }

        [System.Web.Services.WebMethod]
        public static string getNewOTSPN(string input)
        {
            CustomCode x = new CustomCode();
            return x.getNewOTSPN(input);
        }

        [System.Web.Services.WebMethod]
        public static string findMatchingPN(string input)
        {
            CustomCode x = new CustomCode();
            return x.findMatchingPN(input);
        }

        [System.Web.Services.WebMethod]
        public static string getHTMLForPartNumberID(string input)
        {
            CustomCode x = new CustomCode();
            return x.getHTMLForPartNumberID(input);
        }

        [System.Web.Services.WebMethod]
        public static string UpdatePNData(string input)
        {
            CustomCode x = new CustomCode();
            return x.UpdatePNData(input);
        }

        [System.Web.Services.WebMethod]
        public static string UpdateVPNData(string input)
        {
            CustomCode x = new CustomCode();
            return x.UpdateVPNData(input);
        }

    }
}