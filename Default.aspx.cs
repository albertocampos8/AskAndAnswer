using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AskAndAnswer.ClassCode;

namespace AskAndAnswer
{
    public partial class Default : System.Web.UI.Page
    {
        private Boolean m_useDB = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Add controls to pnlInput; attributes for these controls are taken from the database, so that this can
            //be a truly generic solution
            if (m_useDB) {
                DynControls.GenerateControlsFromDatabase(1,pnlInput);
                pnlInput.Controls.Add(DynControls.html_button("btnSubmit", "SUBMIT", "inputButton", true));
            } else {
                pnlInput.Controls.Add(DynControls.html_label("lbl_1", "Enter Board Name:", "fldlabel","txt_1"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_txtbox("txt_1", "txtinput"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblError_1", "Error Message goes here; so on for other elements.", "errorlabel", "",true));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblHelp_1", "Help link will go here.", "helplabel"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_linebreak());

                pnlInput.Controls.Add(DynControls.html_label("lbl_2", "Enter Family Number:", "fldlabel", "txt_2"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_txtbox("txt_2", "txtinput"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblError_2", "Error Message goes here; so on for other elements.", "errorlabel", "", false));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblHelp_2", "Help link will go here.", "helplabel"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_linebreak());

                pnlInput.Controls.Add(DynControls.html_label("lbl_3", "Enter Release Revision:", "fldlabel", "txt_3"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_txtbox("txt_3", "txtinput"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblError_3", "Error Message goes here; so on for other elements.", "errorlabel","",  false));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblHelp_3", "Help link will go here.", "helplabel"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_linebreak());

                pnlInput.Controls.Add(DynControls.html_label("lbl_4", "Do you need a PCB Number?", "fldlabel", "cbo_4"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_combobox_YESNO("cbo_4", "cboInput"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblError_4", "Error Message goes here; so on for other elements.", "errorlabel", "", false));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblHelp_4", "Help link will go here.", "helplabel"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_linebreak());

                pnlInput.Controls.Add(DynControls.html_label("lbl_5", "Enter Number of SHIELD Part Numbers", "fldlabel", "txt_5"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_txtbox("txt_5", "txtinput", "0"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblError_5", "Error Message goes here; so on for other elements.", "errorlabel", "", false));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblHelp_5", "Help link will go here.", "helplabel"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_linebreak());

                pnlInput.Controls.Add(DynControls.html_label("lbl_6", "Enter Number of SROM/NVRAM Part Numbers", "fldlabel", "txt_6"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_txtbox("txt_6", "txtinput", "0"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblError_6", "Error Message goes here; so on for other elements.", "errorlabel", "", false));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblHelp_6", "Help link will go here.", "helplabel"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_linebreak());

                pnlInput.Controls.Add(DynControls.html_label("lbl_7", "Enter Number of BT FW Part Numbers", "fldlabel", "txt_7"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_txtbox("txt_7", "txtinput", "0"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblError_7", "Error Message goes here; so on for other elements.", "errorlabel", "", false));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblHelp_7", "Help link will go here.", "helplabel"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_linebreak());

                pnlInput.Controls.Add(DynControls.html_label("lbl_8", "Enter Number of WORK INSTRUCTION Part Numbers", "fldlabel", "txt_8"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_txtbox("txt_8", "txtinput", "0"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblError_8", "Error Message goes here; so on for other elements.", "errorlabel", "", false));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblHelp_8", "Help link will go here.", "helplabel"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_linebreak());

                pnlInput.Controls.Add(DynControls.html_label("lbl_9", "Is this a Bring-Up (BU) Board?", "fldlabel", "cbo_9"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_combobox_YESNO("cbo_9", "cboInput", true));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblError_9", "Error Message goes here; so on for other elements.", "errorlabel","",  false));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblHelp_9", "Help link will go here.", "helplabel"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_linebreak());

                pnlInput.Controls.Add(DynControls.html_label("lbl_10", "Do you need a Part Number for a SOCKET (SKT) BU Board?", "fldlabel", "cbo_10"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_combobox_YESNO("cbo_10", "cboInput", true));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblError_10", "Error Message goes here; so on for other elements.", "errorlabel", "", false));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblHelp_10", "Help link will go here.", "helplabel"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_linebreak());

                pnlInput.Controls.Add(DynControls.html_label("lbl_11", "Do you need a Part Number for a SOLDER (SOL) BU Board?", "fldlabel", "cbo_11"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_combobox_YESNO("cbo_11", "cboInput", true));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblError_11", "Error Message goes here; so on for other elements.", "errorlabel", "", false));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_label("lblHelp_11", "Help link will go here.", "helplabel"));
                pnlInput.Controls.Add(DynControls.html_linebreak());
                pnlInput.Controls.Add(DynControls.html_linebreak());
            }

        }

        [System.Web.Services.WebMethod]
        public static string respond(string input)
        {
            return CustomCode.respondToSubmitButton(input);
        }
    }

}