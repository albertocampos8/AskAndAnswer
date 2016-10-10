using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AskAndAnswer;
using System.Data.SqlClient;
using System.Text;
using DB;
/// <summary>
/// Code that returns HTML code for standard html/asp controls
/// 
/// </summary>
namespace AskAndAnswer.ClassCode
{
 
    public static class DynControls
    {

        /// <summary>
        /// Returns an ASP label control
        /// </summary>
        /// <param name="cntlID">ID of the label</param>
        /// <param name="lblText">Text displayed in the label</param>
        /// <param name="cssclass">CSS Class of the label</param>
        /// <returns></returns>
        public static Label asp_label(string cntlID, string lblText, string cssclass = "", Boolean canSee = true)
        {
            try
            {
                return new Label { ID = cntlID, Text = lblText, CssClass = cssclass, Visible = canSee };
            }
            catch (Exception e)
            {
                return new Label { ID = cntlID, Text = e.Message, CssClass = cssclass, Visible = true };
            }
        }

        /// <summary>
        /// Returns an HTML label control
        /// </summary>
        /// <param name="cntlID">ID of the label</param>
        /// <param name="lblText">Text displayed in the label</param>
        /// <param name="cssclass">CSS Class of the label</param>
        /// <param name="assocCtl">The control on the page that this label is for</param>
        /// <param name="canSee">Set true if the control should be visible</param>
        /// <returns></returns>
        public static LiteralControl html_label(string cntlID, string lblText,  string cssclass = "", string assocCtl = "", Boolean canSee = true)
        {
            //Enclose values in double (escaped) quotes
            string qID = encodeProperty("id", cntlID);
            string qCssClass = encodeProperty("class", cssclass);
            string qFor = "";
            if (!assocCtl.Equals(""))
            {
                qFor = encodeProperty("for", assocCtl);
            }
            string qVisible = "";
            if (!canSee)
            {
                qVisible = encodeProperty("style", "display:none");
            }
            string controlText = "<label " + qFor + 
                    qID +
                    qCssClass +
                    qVisible +
                    ">" + lblText + "</label>";
            try
            {
                return new LiteralControl(controlText);
            }
            catch (Exception e)
            {
                return renderLiteralControlError(e, controlText);
            }

        }


        /// <summary>
        /// Returns an html text box
        /// </summary>
        /// <param name="cntlID">ID of the text box</param>
        /// <param name="cssclass">CSS class of the text box</param>
        /// <param name="defaultValue">default value text box should contain when displayed; leave blank and box will be empty</param>
        /// <returns></returns>
        public static LiteralControl html_txtbox(string cntlID, string cssclass = "", string defaultValue = "", Boolean canSee = true)
        {
            //Enclose values in double (escaped) quotes
            string qID = encodeProperty("id", cntlID);
            string qCssClass = encodeProperty("class", cssclass);
            string qDefaultValue = encodeProperty("value", defaultValue);
            string qVisible = "";
            if (!canSee)
            {
                qVisible = encodeProperty("style", "display:none");
            }
            string controlText = "<input " + qID +
                    encodeProperty("type", "text") +
                    qCssClass +
                    qDefaultValue +
                    qVisible +
                    " />";
            try
            {
                return new LiteralControl(controlText);
            }
            catch (Exception e)
            {
                return renderLiteralControlError(e, controlText);
            }
        }
        
        /// <summary>
        /// Returns an html combo box that contains either YES (value = 1) or NO (value = 0)
        /// </summary>
        /// <param name="cntlID">ID of the control</param>
        /// <param name="cssclass">css class of the control</param>
        /// <param name="selectionRequired">Set TRUE if the user must select something; in this case, 
        /// --SELECTION REQUIRED-- will be shown in the combo box.</param>
        /// <param name="defaultValueIsNo">Specify which value (yes or no) is the default selected value;
        /// This has no effect if selectionRequired = true</param>
        /// <returns></returns>
        public static LiteralControl html_combobox_YESNO(string cntlID, string cssclass = "", 
            Boolean selectionRequired = false, Boolean defaultValueIsNo = true, Boolean canSee = true)
        {
            string selReqOpt = "";
            if (selectionRequired)
            {
                selReqOpt = "<option disabled selected value >--SELECTION REQUIRED--</option>";
            }
            string noOption = "<option " + encodeProperty("value", "0") + ">No</option>";
            string yesOption = "<option " + encodeProperty("value", "1") + ">Yes</option>";
            string Options = selReqOpt + noOption + yesOption;
            if (!defaultValueIsNo)
            {
                Options = selReqOpt + yesOption + noOption;
            }

            //Enclose values in double (escaped) quotes
            string qID = encodeProperty("id", cntlID);
            string qCssClass = encodeProperty("class", cssclass);
            string qVisible = "";
            if (!canSee)
            {
                qVisible = encodeProperty("style", "display:none");
            }
            string controlText = "<select " + qID +
                    qCssClass + 
                    qVisible + ">" +
                    Options +
                    "</select>";     
            try
            {
                return new LiteralControl(controlText);
            } catch (Exception e)
            {
                return renderLiteralControlError(e, controlText);
            }
        }

        /// <summary>
        /// Returns an html combo box that contains key-value pairs from a List
        /// </summary>
        /// <param name="cntlID">ID of the control</param>
        /// <param name="lstKVP">List of key-value pairs.
        /// Let the even numbered index be i.
        /// Then the value at index i is the key, aka 'Actual Value'.
        /// The value at index i+1 is the value, aka 'Displayed Value'.
        /// Exmaple: ind0: 0 ind1:A ind1:1 ind2:B; when user selects A, 0 is the actual value. 
        /// </param>
        /// <param name="cssclass">css class of the control</param>
        /// <param name="selectionRequired">Set TRUE if the user must select something; in this case, 
        /// --SELECTION REQUIRED-- will be shown in the combo box.</param>
        /// <param name="defaultValue">Only has effect if selectionRequired is FALSE.
        /// Specify the ACTUAL VALUE.  That value will be displayed as the default value,
        /// regardless of its actual display order.  If you leave this at "", then the first item will automatically be selected.</param>
        /// <param name="canSee">Set TRUE to make the control visible</param>
        /// <returns></returns>
        public static LiteralControl html_combobox(string cntlID, List<string> lstKVP, string cssclass = "",
            Boolean selectionRequired = false, string defaultValue="", Boolean canSee = true)
        {
            string selReqOpt = "";
            if (selectionRequired)
            {
                selReqOpt = "<option disabled selected value >--SELECTION REQUIRED--</option>";
            }
            string defaultOption = "";
            StringBuilder strB = new StringBuilder();
            for (int i = 0;i < lstKVP.Count-1;i=i+2)
            {
                string option = "<option " + encodeProperty("value", lstKVP[i]) + ">" + lstKVP[i + 1] + "</option>";
                if (lstKVP[i].Equals(defaultValue) && (!selectionRequired))
                    //We have found the default value, and we are not required to make a selection;
                    //save this option to place at the beginning.
                {
                    defaultOption = option;
                } else
                {
                    strB.Append(option);
                }
            }
            string Options = "";
            if (selectionRequired)
            {
                Options = selReqOpt + strB.ToString();
            } else
            {
                Options = defaultOption + strB.ToString();
            }

            //Enclose values in double (escaped) quotes
            string qID = encodeProperty("id", cntlID);
            string qCssClass = encodeProperty("class", cssclass);
            string qVisible = "";
            if (!canSee)
            {
                qVisible = encodeProperty("style", "display:none");
            }
            string controlText = "<select " + qID +
                    qCssClass +
                    qVisible + ">" +
                    Options +
                    "</select>";
            try
            {
                return new LiteralControl(controlText);
            }
            catch (Exception e)
            {
                return renderLiteralControlError(e, controlText);
            }
        }


        public static void GenerateControlsFromDatabase(int appID, System.Web.UI.Control cntlContainer)
        {
            SqlCommand cmd = new SqlCommand();
            clsDB myDB = new clsDB();
            ControlCollection cntls = new ControlCollection(cntlContainer);
            List<SqlParameter> ps = new List<SqlParameter>();
            ps.Add(new SqlParameter("@" + DBK.fkAPPID,appID));
            using (myDB.OpenConnection())
            {
                using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(DBK.spGET_WEB_DISPLAYFIELD_INFO,
                                                                        ps,
                                                                        clsDB.SPExMode.READER,
                                                                        ref cmd)
                      )
                {
                    if ((dR != null) && (dR.HasRows))
                    {
                        CustomCode.ConstructInputControls(dR, cntlContainer);
                    }
                }
            }
        }
        /// <summary>
        /// Returns a line break element
        /// </summary>
        /// <returns></returns>
        public static LiteralControl html_linebreak()
        {
            return new LiteralControl("<br />");
        }

        /// <summary>
        /// Puts double quotes around the value of an html property
        /// </summary>
        /// <param name="propName">Propety Name</param>
        /// <param name="propValue">Property Value</param>
        /// <returns></returns>
        private static String encodeProperty(string propName, string propValue)
        {
            if (propValue == "")
            {
                return "";
            } else
            {
                return propName + "=" + AAAK.DQ + propValue + AAAK.DQ + " ";
            }
        }

        /// <summary>
        /// Returns a literal control consisting of:
        /// Error Message
        /// HTML code that cause the error.
        /// </summary>
        /// <param name="err">The error object; we will return it's message property</param>
        /// <param name="htmlCode">The html code that caused the error</param>
        /// <returns></returns>
        private static LiteralControl renderLiteralControlError(Exception err, string htmlCode)
        {
            LiteralControl l = new LiteralControl();
            l.Text = err.Message + AAAK.vbCRLF + htmlCode;
            return l;
        }


}
}