using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AskAndAnswer.ClassCode;
using System.Data.SqlClient;

namespace AskAndAnswer
{
    /// <summary>
    /// See this link:
    /// https://support.microsoft.com/en-us/kb/323246
    /// You may need a second master page with a different form tag:...
    /// 
    ///<form id = "Form1" method="post" enctype="multipart/form-data" runat="server">
    /// </summary>
    /// 
    public partial class BOMViewUpload : System.Web.UI.Page
    {
        public string RELEASEKEY = "released";
        protected string cssOTSStyles = DynControls.EncodeScript("/stylesheets/OTSstyles.css", true);
        protected string cssBOMViewStyles = DynControls.EncodeScript("/stylesheets/BOMViewStyles.css", true);
        protected string jsBasicCode = DynControls.EncodeScript("/js/BasicCode.js");
        protected string jsBOMViewCode = DynControls.EncodeScript("/js/BOMViewCode.js");
        protected string jsOTSViewAndEditFunctions = DynControls.EncodeScript("/js/OTSViewAndEditFunctions.js");
        protected string jsBOMViewDocReady_CommonBindings = DynControls.EncodeScript("/js/DocReady_CommonBindings.js");
        protected string jsDocReady_BOMView = DynControls.EncodeScript("/js/DocReady_BOMView.js");
        protected void Page_Load(object sender, EventArgs e)
        {
            string html = "";
            try
            {
                //Check for URL Parameters-- that determines if this page is being called to view an existing bom
                string p = Request.QueryString["p"];
                string pR = Request.QueryString["pR"];
                string bR = Request.QueryString["bR"];

                if (p != null && pR != null && bR != null)
                {
                    CustomCode x = new CustomCode();
                    html = x.DownloadHTMLforBOM(p, pR, bR);
                    divResult.Controls.Add(new LiteralControl(html));
                    txtProduct.Text = p.ToUpper();
                    txtProductRev.Text = pR.ToUpper();
                    txtBOMRev.Text = bR.ToUpper();
                    txtProductStatus.Text = x.GetProductStatus(p, pR, bR).ToUpper();
                    if (txtProductStatus.Text.ToUpper() == RELEASEKEY.ToUpper())
                    {
                        btnUpload.Enabled = false;
                        btnUpload.Style.Add("display", "none");
                        divBrowse.Style.Add("display", "none");
                    }
                    
                    this.Title = txtProduct.Text + " Rev " + txtProductRev.Text + " (BOM Rev " + txtBOMRev.Text.PadLeft(2, '0') + ")";
                } else
                {
                    this.Title = "BOM View and Upload";
                }

            } catch (Exception ex) {
                divResult.Controls.Add(new LiteralControl(DynControls.renderLiteralControlErrorString(ex,html)));
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            clsAssyBOM aB = null;
            try
            {
                
                string fn = System.IO.Path.GetFileName(filFileUpload.PostedFile.FileName);
                if (filFileUpload.PostedFile != null && filFileUpload.PostedFile.ContentLength > 0)
                {
                    
                    string fullServerFileName = Server.MapPath(AAAK.TEMPDATA) + "\\" + fn;
                    filFileUpload.PostedFile.SaveAs(fullServerFileName);
                    string pName = txtProduct.Text;
                    string pRev = txtProductRev.Text;
                    string bRev = txtBOMRev.Text;

                    //create an AssyBOM object
                    try
                    {
                        aB = new clsAssyBOM(pName, "", pRev, 1, 1, int.Parse(bRev), "", fullServerFileName);

                    } catch (Exception ex2)
                    {
                        divMsg.Controls.Add(new LiteralControl("<p>Could not upload the BOM because of the following errors:</p>" +
                        aB.HTML_ErrorMsg));
                        return;
                    }

                    if (aB == null) {
                        divMsg.Controls.Add(new LiteralControl("<p>Could not upload the BOM.  Please make sure you provided a valid upload BOM.</p>" +
                            "Assembly BOM Object was null"));
                        return;
                    } else if (aB.HTML_ErrorMsg != "")
                    {
                        divMsg.Controls.Add(new LiteralControl("<p>Could not upload the BOM.  Please make sure you provided a valid upload BOM.</p>" + 
                            "The error encountered was: " + aB.HTML_ErrorMsg));
                        return;
                    }

                    //Continue with upload if no errors happened during initialization
                    if (aB.UploadToDB())
                    {
                        divMsg.Controls.Add(new LiteralControl("<p>BOM Successfully Uploaded!  Next step is redirect so user can view uploaded BOM.</p>"));
                    }
                    else
                    {
                        string errorResult = aB.HTML_ErrorMsg;

                        string partAnalysisResult = aB.HTML_BadPartMessage;
                        if (partAnalysisResult != "")
                        {
                            divMsg.Controls.Add(new LiteralControl("<p>Could not upload the BOM because of the following part number errors:</p>" +
                                partAnalysisResult));
                        }

                        if (errorResult != "")
                        {
                            divMsg.Controls.Add(new LiteralControl("<p>Could not upload the BOM because of the following errors:</p>" +
                                errorResult));
                        }

                        if (errorResult != "" || partAnalysisResult != "")
                        {
                            divResult.Controls.Clear();
                            return;
                        }
                    }

                } else
                {
                    divMsg.Controls.Add(new LiteralControl("<p>" + fn + " is not a legal Upload BOM.  Please make sure you have selected a valid file."));
                }
            }

            catch (Exception ex)
            {
                string errMsg = ex.GetType().ToString() + ex.Message + ex.StackTrace;
                divMsg.Controls.Add(new LiteralControl("<p>" + errMsg.Replace(AAAK.vbCRLF, DynControls.html_linebreak_string()) + "</p>"));
            }
        }

        [System.Web.Services.WebMethod]
        public static string[] GetAssyNames(string term)
        {
            try
            {
                clsUtil u = new clsUtil();
                List<SqlParameter> ps = new List<SqlParameter>();

                string[] x = u.GetJSONFromDB(DBK.SP.spAC_ASYNAMES, "%" + term + "%", DBK.strNAME);
                return x;
            } catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return new string[0];
            }
        }

        [System.Web.Services.WebMethod]
        public static string[] GetAssyRevs(string term)
        {
            try
            {
                string[] strSpl = { AAAK.DELIM };
                string[] arrTerms = term.Split(strSpl, StringSplitOptions.None);
                List<SqlParameter> sqlPs = new List<SqlParameter>();
                sqlPs.Add(new SqlParameter("@strName", arrTerms[0]));
                sqlPs.Add(new SqlParameter("@filter", "%" + arrTerms[1] + "%"));
                clsUtil u = new clsUtil();
                string[] x = u.GetJSONFromDB(DBK.SP.spAC_ASYREVSFORGIVENASYNAME, "", DBK.strREVISION, ps: sqlPs);
                return x;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return new string[0];
            }

        }

        
        [System.Web.Services.WebMethod]
        public static string[] GetAssyBOMRevs(string term)
        {
            try
            {
                string[] strSpl = { AAAK.DELIM };
                string[] arrTerms = term.Split(strSpl, StringSplitOptions.None);
                List<SqlParameter> sqlPs = new List<SqlParameter>();
                sqlPs.Add(new SqlParameter("@strName", arrTerms[0]));
                sqlPs.Add(new SqlParameter("@strRevision", arrTerms[1]));
                sqlPs.Add(new SqlParameter("@filter", "%" + arrTerms[2] + "%"));
                clsUtil u = new clsUtil();
                string[] x = u.GetJSONFromDB(DBK.SP.spAC_ASYBOMREVSFORGIVENASYNAMEANDREV, "", DBK.intBOMREV, ps: sqlPs);
                return x;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return new string[0];
            }

        }

        [System.Web.Services.WebMethod]
        public static string releaseBOM(string input)
        {
            try
            {
                CustomCode x = new CustomCode();
                return x.releaseBOM(input);
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return "[]";
            }
        }

        /// <summary>
        /// Returns an empty string if the input
        /// (Format: Product DELIM Product Rev DELEIM BOM Rev)
        /// ...is not RELEASED.
        /// 
        /// Otherwise, returns a message to the user explaining what can and can't happen with 
        /// a RELEASED Bill of Material.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string GetProductStatus(string input)
        {
            string[] dlim = { AAAK.DELIM };
            string[] arr = input.Split(dlim, StringSplitOptions.None);
            CustomCode x = new CustomCode();
            return x.GetProductStatus(arr[0],arr[1],arr[2]);
        }
    }
}