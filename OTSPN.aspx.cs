﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AskAndAnswer.ClassCode;
using DB;
using System.Data.SqlClient;
namespace AskAndAnswer
{
    public partial class OTSPN : System.Web.UI.Page
    {
        protected string cssOTSStyles = DynControls.EncodeScript("/stylesheets/OTSstyles.css", true);
        protected string jsOTSGlobals = DynControls.EncodeScript("/js/OTSGlobals.js");
        protected string jsBasicCode = DynControls.EncodeScript("/js/BasicCode.js");
        protected string jsOTSViewAndEditFunctions = DynControls.EncodeScript("/js/OTSViewAndEditFunctions.js");
        protected string jsOTSSearchFunctions = DynControls.EncodeScript("/js/OTSSearchFunctions.js");
        protected string jsOTSNewPNCode = DynControls.EncodeScript("/js/OTSNewPNCode.js");
        protected string jsDocReady_CommonBindings = DynControls.EncodeScript("/js/DocReady_CommonBindings.js");
        protected string jsDocReady_OTSPN = DynControls.EncodeScript("/js/DocReady_OTSPN.js");
        protected void Page_Load(object sender, EventArgs e)
        {
            //check for any url encoded parameters
            string foundIDData = "";
            if (Request.QueryString.HasKeys())
            {
                try
                {
                    
                    CustomCode x = new CustomCode();
                    string targetID = Request.QueryString["ID"];
                    //The following is a bogus division that will go at the end of the displayed page so that the client can't obtain the ID
                    string htmlForID = "<div " + 
                        DynControls.encodeProperty("id", "x_" + targetID) + 
                        DynControls.encodeProperty("class","getID") + " ></div>";
                    //Get the PN associated with the targetID
                    clsDB myDB = new clsDB();
                    SqlCommand cmd = new SqlCommand();
                    List<SqlParameter> ps = new List<SqlParameter>();
                    ps.Add(new SqlParameter("@pnID", Int64.Parse(targetID)));
                    string pageHeader = "";
                    using (myDB.OpenConnection())
                    {
                        using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(DBK.SP.spOTSGETPNINFO, ps, clsDB.SPExMode.READER, ref cmd))
                        {
                            if (dR != null && dR.HasRows)
                            {
                                dR.Read();
                                pageHeader = myDB.Fld2Str(dR[DBK.strPARTNUMBER]);
                            }
                        }
                    }

                    if (Request.QueryString["INV"]!=null)
                    {
                        pageHeader = DynControls.html_header_string(pageHeader + " Inventory", 1);
                        foundIDData = pageHeader + x.InvForPN(targetID) + htmlForID;
                    } else if(Request.QueryString["INVH"] != null) {
                        pageHeader = DynControls.html_header_string(pageHeader + " Inventory History", 1);
                        foundIDData = pageHeader + x.MakePartNumberInventoryHistoryTable(Int64.Parse(targetID)) + htmlForID;
                    } else {                        
                        foundIDData = x.getHTMLForPartNumberID(targetID);
                    }
                    otsdivs.Controls.Add(new LiteralControl(foundIDData));
                    foreach (Control c in divMenuButtons.Controls)
                    {
                        divMenuButtons.Controls.Remove(c);
                    }
                    return;
                } catch (Exception ex)
                {
                    string x = ex.Message + ex.StackTrace;
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
        public static string getHTMLForPartNumberIDInventory(string input)
        {
            CustomCode x = new CustomCode();
            return x.InvForPN(input);
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


        /// <summary>
        /// Gets an html table for the WHERE USED information of a Vendor Part Number.
        /// </summary>
        /// <param name="input">Expected Format:
        /// [0 or 1][DELIM]Vendor Part Number[DELIM]Vendor(Optional)</param>
        /// <returns>
        /// If receive 0, this method just returns a table if WHERE USED has results.
        /// If receive 1, this method returns a warning html text block in front of the table.
        /// If the part is not used on any OTS PN, this function returns an empty string.</returns>
        [System.Web.Services.WebMethod]
        public static string WhereUsedForVPN(string input)
        {
            CustomCode x = new CustomCode();
            return x.WhereUsedForVPN(input);
        }

        /// <summary>
        /// Similar to WhereUsedForVPN, except this just uses the ID of the Part Number in the database.
        /// HTML Table with where used information is returned to client.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string WhereUsedForVPNID(string input)
        {
            CustomCode x = new CustomCode();
            return x.WhereUsedForVPNID(input);
        }

        /// <summary>
        /// Does a 'where used' for a given part number ID (that is, otsParts.ID)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string WhereUsedForPN(string input)
        {
            CustomCode x = new CustomCode();
            return x.WhereUsedForPN(input);
        }

        [System.Web.Services.WebMethod]
        public static string[] AC_GetPartNumber(string term)
        {
            try
            {
                clsUtil u = new clsUtil();
                string[] x = u.GetJSONFromDB(DBK.SP.spAC_OTSPARTS, "%" + term + "%", DBK.strPARTNUMBER);
                return x;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return new string[0];
            }
        }

        [System.Web.Services.WebMethod]
        public static string[] AC_GetProduct(string term)
        {
            try
            {
                clsUtil u = new clsUtil();
                string[] x = u.GetJSONFromDB(DBK.SP.spAC_OTSPRODUCT, "%" + term + "%", DBK.strPRODUCT);
                return x;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return new string[0];
            }
        }
        [System.Web.Services.WebMethod]
        public static string[] AC_GetVendor(string term)
        {
            try
            {
                clsUtil u = new clsUtil();
                string[] x = u.GetJSONFromDB(DBK.SP.spAC_OTSVENDOR, "%" + term + "%", DBK.strVENDOR);
                return x;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return new string[0];
            }
        }

        [System.Web.Services.WebMethod]
        public static string[] AC_GetVendorPN(string term)
        {
            try
            {
                clsUtil u = new clsUtil();
                string[] x = u.GetJSONFromDB(DBK.SP.spAC_OTSVENDORPN, "%" + term + "%", DBK.strVENDORPARTNUMBER);
                return x;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return new string[0];
            }
        }

        [System.Web.Services.WebMethod]
        public static string updatePartInventory(string input)
        {
            CustomCode x = new CustomCode();
            return x.UpdatePartInventory(input);
        }

        [System.Web.Services.WebMethod]
        public static string MakePartNumberInventoryHistoryTable(string input)
        {
            CustomCode x = new CustomCode();
            return x.MakePartNumberInventoryHistoryTable(Int64.Parse(input));
        }

        [System.Web.Services.WebMethod]
        public static string[] GetSubInventory(string term)
        {
            try
            {
                clsUtil u = new clsUtil();
                List<SqlParameter> ps = new List<SqlParameter>();

                string[] x = u.GetJSONFromDB(DBK.SP.spAC_INVSUBINVENTORY, "%" + term + "%", DBK.strSUBINV);
                return x;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return new string[0];
            }
        }

    }
}