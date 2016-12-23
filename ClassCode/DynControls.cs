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
        /// Empahses a portion of a string with color, bold, italics, or underline
        /// </summary>
        /// <param name="inputStr">The entire input string</param>
        /// <param name="portionToEmphasize">The porition of the input string to emphasize; if inputStr = portionToEmphasize, 
        /// then the entire string is emphasized</param>
        /// <param name="fontColor">Font color to use in emphasis; set to "" to leave default</param>
        /// <param name="isBold">set TRUE for bold effect</param>
        /// <param name="isItalic">set TRUE for italic effect</param>
        /// <param name="isUnderline">set TRUE for underline effect</param>
        /// <returns></returns>
        public static string EmphasizeText(string inputStr, string portionToEmphasize, string fontColor = "",
            Boolean isBold = false, Boolean isItalic = false, Boolean isUnderline = false)
        {
            try
            {
                string newPortionToEmphasize = portionToEmphasize;
                if (fontColor != "")
                {
                    inputStr = inputStr.Replace(portionToEmphasize, "<span " +
                               DynControls.encodeProperty("style", "color:red") + ">" + portionToEmphasize + "</span>");
                }
                if (isBold)
                {
                    newPortionToEmphasize = "<b>" + newPortionToEmphasize + "</b>";
                }
                if (isItalic)
                {
                    portionToEmphasize = "<i>" + newPortionToEmphasize + "</i>";
                }
                if (isUnderline)
                {
                    portionToEmphasize = "<u>" + newPortionToEmphasize + "</u>";
                }
                if (portionToEmphasize != newPortionToEmphasize)
                {
                    inputStr = inputStr.Replace(portionToEmphasize, newPortionToEmphasize);
                }

                return inputStr;
            }
            catch (Exception ex)
            {
                return inputStr;
            }

        }


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
        /// Returns an HTML string for a label control
        /// </summary>
        /// <param name="cntlID">ID of the label</param>
        /// <param name="lblText">Text displayed in the label</param>
        /// <param name="cssclass">CSS Class of the label</param>
        /// <param name="assocCtl">The control on the page that this label is for</param>
        /// <param name="canSee">Set true if the control should be visible</param>
        /// <returns></returns>
        public static string html_label_string(string cntlID, string lblText, string cssclass = "", string assocCtl = "", 
            Boolean canSee = true, AAAK.DISPLAYTYPES dType = AAAK.DISPLAYTYPES.UNDEFINED)
        {
            try {
                //Enclose values in double (escaped) quotes
                string qID = encodeProperty("id", cntlID);
                string qCssClass = encodeProperty("class", cssclass);
                string qFor = "";
                if (!assocCtl.Equals(""))
                {
                    qFor = encodeProperty("for", assocCtl);
                }
                string qVisible = DecodeDisplayValue(dType);
                if (!canSee)
                {
                    qVisible = encodeProperty("style", "display:none");
                }
                return "<label " + qFor +
                        qID +
                        qCssClass +
                        qVisible +
                        ">" + lblText + "</label>";
            } catch (Exception ex) {
                return "<p>" + ex.Message + "</p>";
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
        public static LiteralControl html_label(string cntlID, string lblText,  string cssclass = "", string assocCtl = "", 
            Boolean canSee = true, AAAK.DISPLAYTYPES dType = AAAK.DISPLAYTYPES.UNDEFINED)
        {
            string controlText = html_label_string(cntlID, lblText, cssclass, assocCtl, canSee, dType);
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
        /// Returns an html string for a text box
        /// </summary>
        /// <param name="cntlID">ID of the text box</param>
        /// <param name="cssclass">CSS class of the text box</param>
        /// <param name="defaultValue">default value text box should contain when displayed; leave blank and box will be empty</param>
        /// <param name="canSee">Set FALSE to make this control invisible</param>
        /// <param name="dType">Value to use for css display value</param>
        /// <param name="toolTip">Text for tooltip that is displayed when hovering over the textbox</param>
        /// <param name="blReadonly">Set TRUE to disable the textbox</param>
        /// <returns></returns>
        public static string html_txtbox_string(string cntlID, string cssclass = "", string defaultValue = "", Boolean canSee = true,
            AAAK.DISPLAYTYPES dType = AAAK.DISPLAYTYPES.UNDEFINED, string toolTip = "", Boolean blReadonly = false)
        {
            try
            {
                //Enclose values in double (escaped) quotes
                string qID = encodeProperty("id", cntlID);
                string qCssClass = encodeProperty("class", cssclass);
                string qDefaultValue = encodeProperty("value", defaultValue);
                string qVisible = DecodeDisplayValue(dType);
                string qTitle = encodeProperty("title", toolTip);
                if (!canSee)
                {
                    qVisible = encodeProperty("style", "display:none");
                }
                string qReadonly = "";
                if (blReadonly)
                {
                    qReadonly = " readonly ";
                }
                return "<input " + qID +
                        encodeProperty("type", "text") +
                        qCssClass +
                        qDefaultValue +
                        qTitle +
                        qVisible +
                        qReadonly +
                        " />";
            }
            catch (Exception ex)
            {
                return "<p>" + ex.Message + "</p>";
            }
        }
        /// <summary>
        /// Returns an html text box
        /// </summary>
        /// <param name="cntlID">ID of the text box</param>
        /// <param name="cssclass">CSS class of the text box</param>
        /// <param name="defaultValue">default value text box should contain when displayed; leave blank and box will be empty</param>
        /// <returns></returns>
        public static LiteralControl html_txtbox(string cntlID, string cssclass = "", string defaultValue = "", 
            Boolean canSee = true, AAAK.DISPLAYTYPES dType = AAAK.DISPLAYTYPES.UNDEFINED, string toolTip = "", 
            Boolean blReadonly = false)
        {
            string controlText = html_txtbox_string(cntlID, cssclass, defaultValue, canSee, dType, toolTip, blReadonly);
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
        /// Returns the html string for an input element
        /// </summary>
        /// <param name="cntlID"></param>
        /// <param name="cssclass"></param>
        /// <param name="canSee"></param>
        /// <param name="dType"></param>
        /// <param name="toolTip"></param>
        /// <param name="blReadonly"></param>
        /// <returns></returns>
        public static string html_input_string(string cntlID, string inputType,  string cssclass = "", 
            AAAK.DISPLAYTYPES dType = AAAK.DISPLAYTYPES.NONE, string toolTip = "", Boolean blReadonly = false)
        {
            try
            {
                string qID = encodeProperty("id", cntlID);
                string qType = encodeProperty("type", inputType);
                string qCssClass = encodeProperty("class", cssclass);
                string qVisible = DecodeDisplayValue(dType);
                string qTitle = encodeProperty("title", toolTip);
                string qName = encodeProperty("title", cntlID);
                string qReadonly = "";
                if (blReadonly)
                {
                    qReadonly = " readonly ";
                }
                return "<input " +
                        qID +
                        qType +
                        qCssClass +
                        qVisible +
                        qName +
                        qTitle + 
                        qReadonly + "/>";
            }
            catch (Exception ex)
            {
                return "<p>" + ex.Message + "</p>";
            }
        }

        /// <summary>
        /// Returns an html input element
        /// </summary>
        /// <param name="cntlID"></param>
        /// <param name="cssclass"></param>
        /// <param name="canSee"></param>
        /// <param name="dType"></param>
        /// <param name="toolTip"></param>
        /// <returns></returns>
        public static LiteralControl html_input(string cntlID, string inputType, string cssclass = "",
            AAAK.DISPLAYTYPES dType = AAAK.DISPLAYTYPES.NONE, string toolTip = "", Boolean rdonly = false)
        {
            string controlText = html_input_string(cntlID, inputType, cssclass, dType, toolTip, rdonly);
            try
            {
                return new LiteralControl(controlText);
            }
            catch (Exception ex)
            {
                return renderLiteralControlError(ex, controlText);
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
        /// <param name="canSee">Set FALSE to hide control</param>
        /// <param name="dType">Value for the css display property</param>
        /// <param name="toolTip">Text to show when hovering over the textbox</param>
        /// <param name="blReadonly">Set TRUE to disable the drop-down box</param>
        /// <returns></returns>
        public static String html_combobox_YESNO_string(string cntlID, string cssclass = "",
            Boolean selectionRequired = false, Boolean defaultValueIsNo = true, Boolean canSee = true,
            AAAK.DISPLAYTYPES dType = AAAK.DISPLAYTYPES.UNDEFINED, string toolTip = "", Boolean blReadonly = false)
        {
            try
            {
                string selReqOpt = "";
                if (selectionRequired)
                {
                    selReqOpt = "<option disabled selected value >--SELECTION REQUIRED--</option>";
                }
                string noOption = "<option " + encodeProperty("value", "0")  + ">No</option>";
                string yesOption = "<option " + encodeProperty("value", "1") + ">Yes</option>";
                string Options = selReqOpt + noOption + yesOption;
                if (!defaultValueIsNo)
                {
                    Options = selReqOpt + yesOption + noOption;
                }

                //Enclose values in double (escaped) quotes
                string qID = encodeProperty("id", cntlID);
                string qCssClass = encodeProperty("class", cssclass);
                string qVisible = DecodeDisplayValue(dType);
                string qTitle = encodeProperty("title", toolTip);
                if (!canSee)
                {
                    qVisible = encodeProperty("style", "display:none");
                }
                string qReadonly = "";
                if (blReadonly)
                {
                    qReadonly = encodeProperty("disabled","disabled");
                }
                return "<select " + qID +
                        qCssClass +
                        qReadonly +
                        qTitle +
                        qVisible + ">" +
                        Options +
                        "</select>";
            }
            catch (Exception ex)
            {
                return "<p>" + ex.Message + "</p>";
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
        /// <param name="canSee">Set false to hide control</param>
        /// <param name="dType">Value for CSS 'display' property</param>
        /// <param name="toolTip">Text to display when hovering over combobox</param>
        /// <param name="blDisable">Set TRUE to disable</param>
        /// <returns></returns>
        public static LiteralControl html_combobox_YESNO(string cntlID, string cssclass = "", 
            Boolean selectionRequired = false, Boolean defaultValueIsNo = true, Boolean canSee = true,
            AAAK.DISPLAYTYPES dType = AAAK.DISPLAYTYPES.UNDEFINED, string toolTip  = "", Boolean blDisable = false)
        {
            string controlText = html_combobox_YESNO_string(cntlID, cssclass, 
                selectionRequired, defaultValueIsNo, canSee, dType, toolTip,
                blDisable);
            try
            {
                return new LiteralControl(controlText);
            } catch (Exception e)
            {
                return renderLiteralControlError(e, controlText);
            }
        }


        /// <summary>
        /// Returns a string for an html combo box that contains key-value pairs from a List
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
        /// <param name="dType">Value for CSS Display property</param>
        /// <param name="toolTip">Text to display when user hovers over control</param>
        /// <param name="blReadonly">Set TRUE to disable</param>
        /// <returns></returns>
        public static string html_combobox_string(string cntlID, List<string> lstKVP, string cssclass = "",
            Boolean selectionRequired = false, string defaultValue = "", Boolean canSee = true,
            AAAK.DISPLAYTYPES dType = AAAK.DISPLAYTYPES.UNDEFINED, string toolTip = "", 
            Boolean blReadonly = false)
        {
            try
            {
                string selReqOpt = "";
                if (selectionRequired)
                {
                    selReqOpt = "<option disabled selected value >--SELECTION REQUIRED--</option>";
                }
                string defaultOption = "";
                StringBuilder strB = new StringBuilder();
                for (int i = 0; i < lstKVP.Count - 1; i = i + 2)
                {
                    string option = "<option " + encodeProperty("value", lstKVP[i]) + ">" + lstKVP[i + 1] + "</option>";
                    if (lstKVP[i].Equals(defaultValue) && (!selectionRequired))
                    //We have found the default value, and we are not required to make a selection;
                    //save this option to place at the beginning.
                    {
                        defaultOption = option;
                    }
                    else
                    {
                        strB.Append(option);
                    }
                }
                string Options = "";
                if (selectionRequired)
                {
                    Options = selReqOpt + strB.ToString();
                }
                else
                {
                    Options = defaultOption + strB.ToString();
                }

                //Enclose values in double (escaped) quotes
                string qID = encodeProperty("id", cntlID);
                string qCssClass = encodeProperty("class", cssclass);
                string qVisible = DecodeDisplayValue(dType);
                string qTitle = encodeProperty("title", toolTip);
                
                if (!canSee)
                {
                    qVisible = encodeProperty("style", "display:none");
                }

                string qReadonly = "";
                if (blReadonly)
                {
                    qReadonly = encodeProperty("disabled","disabled");
                }
                return "<select " + qID +
                        qCssClass +
                        qVisible +
                        qReadonly +
                        qTitle + ">" +
                        Options +
                        "</select>";
            }
            catch (Exception ex)
            {
                return "<p>" + ex.Message + "</p>";
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
            Boolean selectionRequired = false, string defaultValue="", Boolean canSee = true,
            AAAK.DISPLAYTYPES dType = AAAK.DISPLAYTYPES.UNDEFINED, string toolTip = "", Boolean blReadonly = false)
        {
            string controlText = html_combobox_string(cntlID, lstKVP, cssclass, selectionRequired, defaultValue, 
                canSee, dType, toolTip, blReadonly);
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
        /// Returns an html string for a button
        /// </summary>
        /// <param name="cntlID">Button ID</param>
        /// <param name="displayText">Text to show on the button face, e.g., 'Submit'</param>
        /// <param name="cssclass">CSS Class of button</param>
        /// <param name="canSee">Leave True to make button visible.</param>
        /// <param name="dType">Display type</param>
        /// <param name="toolTip">Text to show in tool tip</param>
        /// <param name="form">Form associated with this button</param>
        /// <returns></returns>
        public static string html_button_string(string cntlID, string displayText, string cssclass = "",
            Boolean canSee = true, AAAK.DISPLAYTYPES dType = AAAK.DISPLAYTYPES.UNDEFINED,
            string toolTip = "", string form = "", Boolean enabled = true)
        {
            try
            {
                string qID = encodeProperty("id", cntlID);
                string qcssclass = encodeProperty("class", cssclass);
                string qDisplayValue = encodeProperty("value", displayText);
                string qVisible = DecodeDisplayValue(dType);
                string qTitle = encodeProperty("title", toolTip);
                string qForm = encodeProperty("form", form);
                string qDisabled = "";
                if (!enabled) {
                    qDisabled = " disabled ";
                }
                if (!canSee)
                {
                    qVisible = encodeProperty("style", "display:none");
                }
                return "<input " +
                    qID +
                    encodeProperty("type", "button") +
                    qcssclass +
                    qTitle + 
                    qForm + 
                    qVisible + 
                    qDisplayValue + 
                    qDisabled + "/>";
            } catch (Exception ex)
            {
                return "<p>" + ex.Message + "</p>";
            }                
        }

        /// <summary>
        /// Returns an html button
        /// </summary>
        /// <param name="cntlID">Button ID</param>
        /// <param name="displayText">Text to show on the button face, e.g., 'Submit'</param>
        /// <param name="cssclass">CSS Class of button</param>
        /// <param name="blEncloseInSpan">Set TRUE and button will be enclosed in span, allowing it to fill up entire space.</param>
        /// <param name="canSee">Leave True to make button visible.</param>
        /// <param name="dType">Value for CSS display property</param>
        /// <param name="toolTip">Value to display when user hovers over button</param>
        /// <param name="form">Form section associated with this button</param>
        /// <param name="disabled">Set TRUE to disable the button</param>
        /// <returns></returns>
        public static LiteralControl html_button(string cntlID, string displayText, string cssclass = "", Boolean canSee = true,
            AAAK.DISPLAYTYPES dType = AAAK.DISPLAYTYPES.UNDEFINED, string toolTip = "", string form = "", Boolean enabled = true)
        {
            string controlText = html_button_string(cntlID, displayText, cssclass, canSee, dType, toolTip, form, enabled);
            try
            {
                return new LiteralControl(controlText);
            }
            catch (Exception ex)
            {
                return renderLiteralControlError(ex, controlText);
            }

        }

        /// <summary>
        /// Generates html string for a li element
        /// </summary>
        /// <param name="cntlID"><id of the list item/param>
        /// <param name="cssclass">css class of the list item</param>
        /// <param name="text">Text of the list item</param>
        /// <param name="href">href value; needed when making tabs; 
        /// set this to the name of the div associated with this tab</param>
        /// <returns></returns>
        public static string html_li_string(string cntlID, string cssclass, string text, string href="")
        {
            string controlText = "";
            try
            {
                string qID = encodeProperty("id", cntlID);
                string qClass = encodeProperty("class", cssclass);
                controlText = "<li " + qID + qClass + ">";
                if (href != "")
                {
                    controlText = controlText + "<a " + encodeProperty("href", href) + ">";
                }
                controlText = controlText + text;
                if (href != "")
                {
                    controlText = controlText + "</a>";
                }
                controlText = controlText + "</li>";
                return controlText;
            }
            catch (Exception ex)
            {
                return "<p>" + ex.Message + "</p>";
            }
        }

        public static LiteralControl html_li(string cntlID, string cssclass, string text, string href = "")
        {
            string controlText = html_li_string(cntlID, cssclass, text, href);
            try
            {
                return new LiteralControl(controlText);
            }
            catch (Exception ex)
            {
                return renderLiteralControlError(ex, controlText);
            }
        }

        /// <summary>
        /// Returns a link
        /// </summary>
        /// <param name="linkText">text to display in the link</param>
        /// <param name="url">URL of the link</param>
        /// <param name="id">if of the "a" link element</param>
        /// <param name="cssClass">class of the "a" link element</param>
        /// <param name="target">Set to _blank to make the link open in a new tab/window</param>
        /// <returns></returns>
        public static string html_hyperlink_string(string linkText, string url, string id = "", string cssClass = "",
            string target = "")
        {
            try
            {
                if (linkText == "")
                {
                    linkText = url;
                }
                string qID = encodeProperty("id", id);
                string qcssClass = encodeProperty("class", cssClass);
                string qurl = encodeProperty("href", url);
                string qTarget = encodeProperty("target", target);
                return "<a " + qTarget + qurl + qID + qcssClass + ">" + linkText + "</a>";
            }
            catch (Exception ex)
            {
                return "<p>" + ex.Message + "</p>";
            }
        }

        public static LiteralControl html_hyperlink(string linkText, string url, string id = "", string cssClass = "",
            string target = "")
        {
            string controlText = "";
            try
            {
                controlText = html_hyperlink_string(linkText, url, id, cssClass, target);
                return new LiteralControl(controlText);
            }
            catch (Exception ex)
            {
                return renderLiteralControlError(ex, controlText);
            }
        }

        /// <summary>
        /// Creates elements based on the fields in a database.
        /// Optionally, the elements are enclosed in a from with ID 'form_[formID]'.
        /// </summary>
        /// <param name="appID">The appID on which these controls are based.</param>
        /// <param name="cntlContainer">The container that contains these controls</param>
        /// <param name="dctDefaultOverride">A dictionary that maps the default value given in the database
        /// with the value to use as an override</param>
        /// <param name="uid">A unique identified to append to the control IDs of the generated controls.
        /// You will need this if you are calling this method several times to generate similar output one page.
        /// (One recommended value for thie Unique ID is the Database ID).</param>
        /// <param name="cntlDisplayStyle"></param>
        /// <param name="blElementsInLine">Set false to allow displaying all elements in line (no line breaks)</param>
        public static void GenerateControlsFromDatabase(int appID, System.Web.UI.Control cntlContainer,
            Dictionary<string,string> dctDefaultOverride = null, string uid = "", int cntlDisplayStyle = -1,
            Boolean blElementsInLine = false)
        {
            try
            {
                if (dctDefaultOverride == null)
                {
                    dctDefaultOverride = new Dictionary<string, string>();
                }
                SqlCommand cmd = new SqlCommand();
                clsDB myDB = new clsDB();
                ControlCollection cntls = new ControlCollection(cntlContainer);
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@" + DBK.fkAPPID, appID));
                using (myDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(DBK.SP.spGETWEBDISPLAYFIELDINFO,
                                                                            ps,
                                                                            clsDB.SPExMode.READER,
                                                                            ref cmd)
                          )
                    {
                        if ((dR != null) && (dR.HasRows))
                        {
                            CustomCode x = new CustomCode();
                            x.ConstructInputControls(dR, cntlContainer, dctDefaultOverride, uid, cntlDisplayStyle,
                                blElementsInLine);
                            
                        }
                    }
                }
            } catch (Exception ex)
            {
                cntlContainer.Controls.Add(renderLiteralControlError(ex,""));
            }

        }

        public static string html_linebreak_string()
        {
            return "<br />";
        }
        /// <summary>
        /// Returns a line break element
        /// </summary>
        /// <returns></returns>
        public static LiteralControl html_linebreak()
        {
            return new LiteralControl(html_linebreak_string());
        }

        /// <summary>
        /// Puts double quotes around the value of an html property
        /// </summary>
        /// <param name="propName">Propety Name</param>
        /// <param name="propValue">Property Value</param>
        /// <returns></returns>
        public static String encodeProperty(string propName, string propValue)
        {
            if (propValue == "")
            {
                return "";
            } else
            {
                return propName + "=" + AAAK.DQ + propValue + AAAK.DQ + " ";
            }
        }

        public static string GetHTMLText(Control cntl)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                using (System.IO.StringWriter sw = new System.IO.StringWriter(sb))
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        cntl.RenderControl(hw);
                        return sb.ToString();
                    }
                }
            } catch (Exception ex)
            {
                return "<p>DynControls.GetHTMLText Error: " + ex.Message + "<p>";
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

        /// <summary>
        /// Returns a display: <VALUE> string to affect a component's display
        /// </summary>
        /// <param name="dType"></param>
        /// <returns></returns>
        public static string DecodeDisplayValue(AAAK.DISPLAYTYPES dType)
        {
            try
            {
                switch (dType) { 
                    case AAAK.DISPLAYTYPES.UNDEFINED:
                        return "";
                    case AAAK.DISPLAYTYPES.BLOCK:
                        return encodeProperty("style", "display:block");
                    case AAAK.DISPLAYTYPES.INLINE:
                        return encodeProperty("style", "display:inline");
                    case AAAK.DISPLAYTYPES.NONE:
                        return encodeProperty("style", "display:none");
                    case AAAK.DISPLAYTYPES.FILL:
                        //set the style and width to 100%; NOTE: this will cause the element to fill the entire container.
                        return encodeProperty("style", "height:100%;width:100%");
                    default:
                        return "";
            }
           } catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// Appends the sw version to the script name and encloses it in double quotes
        /// </summary>
        /// <param name="scriptName">Name of script/css style sheet</param>
        /// <param name="encloseInDblQuotes">Set true if returned string should be enclosed in double quotes
        /// (required for css style sheets)</param>
        /// <returns></returns>
        public static string EncodeScript(string scriptName, Boolean encloseInDblQuotes = false)
        {
            string s =  scriptName + AAAK.bustCache();
            if (encloseInDblQuotes)
            {
                return AAAK.DQ + s + AAAK.DQ;
            } else
            {
                return s;
            }

            
        }


}
}