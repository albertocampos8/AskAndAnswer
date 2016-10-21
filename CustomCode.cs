using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using AskAndAnswer.ClassCode;
using DB;
using System.Text;

namespace AskAndAnswer
{
    /// <summary>
    /// Maps a control type to a number, as defined in webControlTypes
    /// </summary>
    public class CntlDecoder
    {
        public const int TEXTBOX = 1;
        public const int YESNO_DROPDOWN = 2;
        public const int MULT_DROPDOWN = 3;
    }

    /// <summary>
    /// Maps constants to the name of database tables/fieldname in your organization.
    /// If you use the SQL server scripts that came with this project, change nothing in this class.
    /// Otherwise, make changes below
    /// </summary>
    public class DBK
    {
        //Table & field Names
        public static string WEBDISPLAYFIELDS = AAAK.WEBDISPLAYFIELDS;
        public static string ID = AAAK.ID;
        public static string fkAPPID = AAAK.fkAPPID;
        public static string intDISPLAYORDER = AAAK.intDISPLAYORDER;
        public static string fkCONTROLTYPE = AAAK.fkCONTROLTYPE;
        public static string lblPROMPT = AAAK.lblPROMPT;
        public static string lblHELPMESSAGE = AAAK.lblHELPMESSAGE;
        public static string txtDEFAULTVALUE = AAAK.txtDEFAULTVALUE;
        public static string fkKVPGROUPIDDecoder = AAAK.fkKVPGROUPIDDecoder;
        public static string blINITVISIBLE = AAAK.blINITVISIBLE;
        public static string blINITENABLED = AAAK.blINITENABLED;
        public static string blSELECTIONREQUIRED = AAAK.blSELECTIONREQUIRED;
        public static string intIDCODE = AAAK.intIDCODE;

        public static string WEBAPPIDS = AAAK.WEBAPPIDS;
        public static string NAME = AAAK.NAME;

        public static string WEBKVP = AAAK.WEBKVP;
        public static string fkGROUPID = AAAK.fkGROUPID;
        public static string keyACTUALVALUE = AAAK.keyACTUALVALUE;
        public static string valDISPLAYEDVALUE = AAAK.valDisplayedValue;

        public static string WEBCONTROLTYPES = AAAK.WEBCONTROLTYPES;
        public static string DESCRIPTION = AAAK.DESCRIPTION;

        public static string WEBKVPGROUPIDDECODER = AAAK.WEBKVPGROUPIDDECODER;
        //Stored Procdedures
        //The stored procedure that retrieves the key value pair for drop down boxes from the database
        public static string spGET_WEB_KEYVALUEPAIR_INFO = AAAK.spGET_WEB_KEYVALUEPAIR_INFO;
        //The stored procedure that retrieves the information needed to generate the input fields from the database
        public static string spGET_WEB_DISPLAYFIELD_INFO = AAAK.spGET_WEB_DISPLAYFIELD_INFO;
    }
    //Database constants - if you are not using the default scripts provided with this databse,
    //change the values on the right



    /// <summary>
    /// Put all code that is specific to your organization's business logic here
    /// </summary>
    public class CustomCode
    {
        /// <summary>
        /// Add controls to cntlContainer based on the contents of dR.
        /// </summary>
        /// <param name="dR">DataReader object.  See documentation for what this should contain.  Access the field Names
        /// as AAAK.FIELDNAME</param>
        /// <param name="cntlContainer">The container that will contain the objects</param>
        /// <param name="displayType">Enter the option that will determine how this code will arrange the controls.
        /// The default value is 1, resulting in each field sequentially listed after another.  See Documentation for how this looks.
        /// To customize, define a new code, and call this procedure using that code number</param>
        public static void ConstructInputControls(SqlDataReader dR, System.Web.UI.Control cntlContainer, int displayType = -1)
        {
          
            while (dR.Read())
            {
                System.Int16 index = (System.Int16)dR[DBK.intIDCODE];
                               
                //Determine the name of the main control
                string mainCtlName = "";
                switch ((System.Byte)dR[AAAK.fkCONTROLTYPE])
                {
                    case CntlDecoder.TEXTBOX:
                        mainCtlName = "txt_";
                        break;
                    case CntlDecoder.YESNO_DROPDOWN:
                        mainCtlName = "cbo_";
                        break;
                    case CntlDecoder.MULT_DROPDOWN:
                        mainCtlName = "cbo_";
                        break;
                }
                mainCtlName = mainCtlName + index;

                switch (displayType)
                {
                    case -1:
                        //Start with label describing entry requirement
                        cntlContainer.Controls.Add(DynControls.html_label("lbl_" + index, 
                                                                        (string)dR[DBK.lblPROMPT],
                                                                        "fldlabel",
                                                                        mainCtlName, 
                                                                        (Boolean)dR[DBK.blINITVISIBLE])
                                                  );
                        //Add linebreak
                        if ((Boolean)dR[DBK.blINITVISIBLE])
                        {
                            cntlContainer.Controls.Add(DynControls.html_linebreak());
                        }
                        
                        //Next, the actual input element
                        switch ((System.Byte)dR[AAAK.fkCONTROLTYPE])
                        {
                            case CntlDecoder.TEXTBOX:
                                cntlContainer.Controls.Add(DynControls.html_txtbox("txt_" + index,
                                                                                   "txtinput", 
                                                                                    (string)dR[DBK.txtDEFAULTVALUE],
                                                                                   (Boolean)dR[DBK.blINITVISIBLE])
                                                          );
                                break;
                            case CntlDecoder.YESNO_DROPDOWN:
                                cntlContainer.Controls.Add(DynControls.html_combobox_YESNO("cbo_" + index,
                                                                                            "cboinput",
                                                                                            dR[DBK.blSELECTIONREQUIRED].Equals(true),
                                                                                            dR[DBK.txtDEFAULTVALUE].Equals("0"),
                                                                                            (Boolean)dR[DBK.blINITVISIBLE])
                                                          );
                                break;
                            case CntlDecoder.MULT_DROPDOWN:
                                //We need to get the key-value pairs from the database.
                                SqlCommand cmd = new SqlCommand();
                                clsDB myDB = new clsDB();
                                List<SqlParameter> ps = new List<SqlParameter>();

                                List<string> lstKVPs = new List<string>();
                                ps.Add(new SqlParameter("@" + DBK.fkGROUPID, dR[DBK.fkKVPGROUPIDDecoder]));
                                using (myDB.OpenConnection())
                                {
                                    using (SqlDataReader dRdr = (SqlDataReader)myDB.ExecuteSP(DBK.spGET_WEB_KEYVALUEPAIR_INFO,
                                                        ps,
                                                        clsDB.SPExMode.READER,
                                                        ref cmd)
      )
                                    {
                                        if ((dRdr != null) && (dRdr.HasRows))
                                        {
                                            while (dRdr.Read())
                                            {
                                                try
                                                {
                                                    object x = dRdr[DBK.valDISPLAYEDVALUE];
                                                } catch (Exception e3) {
                                                    string x = e3.Message;
                                                }
                                                
                                                lstKVPs.Add((string)dRdr[DBK.keyACTUALVALUE]);
                                                lstKVPs.Add((string)dRdr[DBK.valDISPLAYEDVALUE]);
                                            }
                                        }
                                    }
                                }
                                cntlContainer.Controls.Add(DynControls.html_combobox("cbo_" + index,
                                                                                     lstKVPs,
                                                                                     "cboinput",
                                                                                     dR[DBK.blSELECTIONREQUIRED].Equals(true),
                                                                                     (string)dR[DBK.txtDEFAULTVALUE],
                                                                                     (Boolean)dR[DBK.blINITVISIBLE])
                                                          );
                                break;
                            default:
                                break;
                        }
                        //Add linebreak
                        if ((Boolean)dR[DBK.blINITVISIBLE])
                        {
                            cntlContainer.Controls.Add(DynControls.html_linebreak());
                        }

                        //Control for error label
                        cntlContainer.Controls.Add(DynControls.html_label("lblError_" + index,
                                                                        "",
                                                                        "errorlabel",
                                                                        "",
                                                                        false)
                                                    );
                        //DONT'T Add linebreak-- error label is not visible, so display:none, and it takes no space
                        //cntlContainer.Controls.Add(DynControls.html_linebreak());

                        //Control for help label
                        cntlContainer.Controls.Add(DynControls.html_label("lblHelp_" + index,
                                                                        (string)dR[DBK.lblHELPMESSAGE],
                                                                        "helplabel",
                                                                        "",
                                                                        !dR[DBK.lblHELPMESSAGE].Equals("") && (Boolean)dR[DBK.blINITVISIBLE])
                                                   );
                        //Add linebreak after the label, if the control is visible AND the help message is <> ""
                        if (!dR[DBK.lblHELPMESSAGE].Equals("") && (Boolean)dR[DBK.blINITVISIBLE])
                        {
                            cntlContainer.Controls.Add(DynControls.html_linebreak());
                        }

                        //Add separating linebreak
                        if ((Boolean)dR[DBK.blINITVISIBLE])
                        {
                            cntlContainer.Controls.Add(DynControls.html_linebreak());
                        }

                        break;
                    default:

                        break;

                }
            }
        }

        /// <summary>
        /// Returns a string response to the input obtained when the user pressed Submit
        /// </summary>
        /// <param name="input">String encoded response from user; expected format is
        /// ID_0!#!VALUE_0!#!ID_1!#!VALUE_1...ID_N-1!#!_VALUE_N-1</param>
        /// <returns></returns>
        public static string respondToSubmitButton(string input)
        {
            try
            {
                string[] dlim = { "!#!" };
                string[] kvp = input.Split(dlim, StringSplitOptions.None);
                List <HTMLStrings.TableRow> tblRows = new List<HTMLStrings.TableRow>();
                //Add the header row
                tblRows.Add(new HTMLStrings.TableRow(
                    "row_h",
                    "clsHeaderRow",
                    new HTMLStrings.TableCell[] {
                        new HTMLStrings.TableCell("","clsHeaderRow","KEY",1,true),
                        new HTMLStrings.TableCell("","clsHeaderRow","VALUE",1,true),
                                                }
                                                     )
                             );

                for (int i = 0; i < kvp.Length - 1; i = i + 2)
                {
                    tblRows.Add(new HTMLStrings.TableRow(
                                "row_" + kvp[i].ToString(),
                                "clsHeaderRow",
                                new HTMLStrings.TableCell[] {
                                                    new HTMLStrings.TableCell("","outputtablecell",kvp[i],1,true),
                                                    new HTMLStrings.TableCell("","outputtablecell",kvp[i+1],1,true),
                                                            }
                                                                 )
                                         );

                }
                HTMLStrings.Table tbl = new HTMLStrings.Table("", "", tblRows);
                return tbl.ToHTML();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}