using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using AskAndAnswer.ClassCode;
using DB;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AskAndAnswer
{

    public class ParametricSearchResponse
    {
        private string m_htmlMessage = "";
        /// <summary>
        /// The message that goes in the Message Div of the web form
        /// </summary>
        public string HTMLMessage
        {
            get
            {
                return m_htmlMessage;
            }
            set
            {
                m_htmlMessage = value;
            }
        }

        private string m_htmlResultTable = "";
        /// <summary>
        /// The message that goes in the Message Div of the web form
        /// </summary>
        public string HTMLResultTable
        {
            get
            {
                return m_htmlResultTable;
            }
            set
            {
                m_htmlResultTable = value;
            }
        }
    }
    /// <summary>
    /// Maps a control type to a number, as defined in webControlTypes
    /// </summary>
    public class CntlDecoder
    {
        public const int TEXTBOX = 1;
        public const int YESNO_DROPDOWN = 2;
        public const int MULT_DROPDOWN = 3;
        public const int TXT_W_BROWSE_ATTACHMENTS = 4;
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
        public static string blUSERCANEDIT= AAAK.blUSERCANEDIT;
        public static string blINITENABLED = AAAK.blINITENABLED;
        public static string blSELECTIONREQUIRED = AAAK.blSELECTIONREQUIRED;
        public static string intIDCODE = AAAK.intIDCODE;
        public static string lblHELPLINK = AAAK.lblHELPLINK;
        public static string strSTOREDPROC = AAAK.strSTOREDPROC;
        public static string cssCLASS = AAAK.cssCLASS;

        public static string WEBAPPIDS = AAAK.WEBAPPIDS;
        public static string NAME = AAAK.NAME;

        public static string WEBKVP = AAAK.WEBKVP;
        public static string fkGROUPID = AAAK.fkGROUPID;
        public static string keyACTUALVALUE = AAAK.keyACTUALVALUE;
        public static string valDISPLAYEDVALUE = AAAK.valDISPLAYEDVALUE;

        public static string WEBCONTROLTYPES = AAAK.WEBCONTROLTYPES;
        public static string DESCRIPTION = AAAK.DESCRIPTION;

        public static string WEBKVPGROUPIDDECODER = AAAK.WEBKVPGROUPIDDECODER;

        public static string strTYPE = AAAK.strTYPE;
        public static string strBUCODE = AAAK.strBUCODE;

        // OTS DATABASE TABLES/COLUMNS
        public const string otsPARTS = AAAK.otsPARTS;
        public const string strPARTNUMBER = AAAK.strPARTNUMBER;
        public const string keyREQUESTEDBY = AAAK.keyREQUESTEDBY;
        public const string dtREQUESTED = AAAK.dtREQUESTED;
        public const string strDESCRIPTION = AAAK.strDESCRIPTION;
        public const string strDESCRIPTION2 = AAAK.strDESCRIPTION2;
        public const string keyTYPE = AAAK.keyTYPE;
        public const string keySUBTYPE = AAAK.keySUBTYPE;
        public const string keyREQUESTEDFORPRODUCT = AAAK.keyREQUESTEDFORPRODUCT;
        public const string keyPARTSTATUS = AAAK.keyPARTSTATUS;
        public const string strPARAMETERS = AAAK.strPARAMETERS;
        public const string decMAXHEIGHT = AAAK.decMAXHEIGHT;
        public const string decLOWVOLCOST = AAAK.decLOWVOLCOST;
        public const string decHIGHVOLCOST = AAAK.decHIGHVOLCOST;
        public const string decENGCOST = AAAK.decENGCOST;
        public const string intVERSION = AAAK.intVERSION;
        public const string keyOTSENVIRONCODE = AAAK.keyOTSENVIRONCODE;

        public const string otsPARTTYPE = AAAK.otsPARTTYPE;
        //public const string strTYPE = "strType";
        public const string strVISIBLEPARAMS = AAAK.strVISIBLEPARAMS;
        public const string strPARAMORDER = AAAK.strPARAMORDER;
        public const string strTYPEABBR = AAAK.strTYPEABBR;


        public const string otsPARTSUBTYPE = AAAK.otsPARTSUBTYPE;
        public const string strSUBTYPE = AAAK.strSUBTYPE;
        public const string strSTYPEABBR = AAAK.strSTYPEABBR;

        public const string otsPRODUCT = AAAK.otsPRODUCT;
        public const string strPRODUCT = AAAK.strPRODUCT;

        public const string otsVENDORPN = AAAK.otsVENDORPN;
        public const string strVENDORPARTNUMBER = AAAK.strVENDORPARTNUMBER;
        public const string strDATASHEETURL = AAAK.strDATASHEETURL;
        public const string keyVENDOR = AAAK.keyVENDOR;
        public const string keyOTSPART = AAAK.keyOTSPART;
        public const string keyVENDORENVIRONCODE = AAAK.keyVENDORENVIRONCODE;
        //public const string keyPARTSTATUS = "keyPartStatus";

        public const string otsPARTSTATUS = AAAK.otsPARTSTATUS;
        public const string strSTATUS = AAAK.strSTATUS;

        public const string otsVENDOR = AAAK.otsVENDOR;
        public const string strVENDOR = AAAK.strVENDOR;
        public const string keyVENDORSTATUS = AAAK.keyVENDORSTATUS;
        public const string keyVENDORPARTSTATUS = AAAK.keyVENDORPARTSTATUS;

        public const string otsENVIRONCODE = AAAK.otsENVIRONCODE;
        public const string intECODE = AAAK.intECODE;
        public const string strECODENAME = AAAK.strECODENAME;

        public const string otsVENDORSTATUS = AAAK.otsVENDORSTATUS;
        public const string strVENDORSTATUS = AAAK.strVENDORSTATUS;

        public const string otsUSERS = AAAK.otsUSERS;
        public const string keyBU = AAAK.keyBU;
        public const string strNAME = AAAK.strNAME;

        public const string otsPNHISTORY = AAAK.otsPNHISTORY;
        public const string keyOTSPNIND = AAAK.keyOTSPNIND;
        public const string keyUPDATEDBY = AAAK.keyUPDATEDBY;
        public const string DATE_UPDATED = AAAK.DATE_UPDATED;
        public const string DESCRIPTION_CHANGE = AAAK.DESCRIPTION_CHANGE;
        public const string DESCRIPTION2_CHANGE = AAAK.DESCRIPTION2_CHANGE;
        public const string PART_TYPE_CHANGE = AAAK.PART_TYPE_CHANGE;
        public const string PART_SUBTYPE_CHANGE = AAAK.PART_SUBTYPE_CHANGE;
        public const string PARAMETER_CHANGE = AAAK.PARAMETER_CHANGE;
        public const string MAX_HEIGHT_CHANGE = AAAK.MAX_HEIGHT_CHANGE;
        public const string LOW_VOL_COST_CHANGE = AAAK.LOW_VOL_COST_CHANGE;
        public const string HIGH_VOL_COST_CHANGE = AAAK.HIGH_VOL_COST_CHANGE;
        public const string ENG_COST_CHANGE = AAAK.ENG_COST_CHANGE;
        public const string ENVIRONMENTAL_CODE_CHANGE = AAAK.ENVIRONMENTAL_CODE_CHANGE;
        public const string VENDOR_PART_NUMBER_CHANGE = AAAK.VENDOR_PART_NUMBER_CHANGE;
        public const string VENDOR_CHANGE = AAAK.VENDOR_CHANGE;

        public const string otsRELATION = AAAK.otsRELATION;
        public const string keyVENDORPN = AAAK.keyVENDORPN;
        //END OTS DATABASE ENTRIES

        //Stored Procedures
        public partial class SP {
            public const string spOTSGETBASEPARTNUMBER = AAAK.spOTSGETBASEPARTNUMBER;
            public const string spGETKVPBUINFO = AAAK.spGETKVPBUINFO;
            public const string spOTSINSERTNEWPARTNUMBER = AAAK.spOTSINSERTNEWPARTNUMBER;
            //The stored procedure that retrieves the key value pair for drop down boxes from the database
            public const string spGETWEBKEYVALUEPAIRINFO = AAAK.spGETWEBKEYVALUEPAIRINFO;
            public const string spOTSGETBASEPARTNMBER = AAAK.spOTSGETBASEPARTNUMBER;

            //The stored procedure that retrieves the information needed to generate the input fields from the database
            public const string spGETWEBDISPLAYFIELDINFO = AAAK.spGETWEBDISPLAYFIELDINFO;
            public const string spGETKVPPARTTYPES = AAAK.spGETKVPPARTTYPES;
            public const string spGETKVPPARTSUBTYPES = AAAK.spGETKVPPARTSUBTYPES;
            public const string spGETKVPENVIRONCODE = AAAK.spGETKVPENVIRONCODE;
            public const string spGETKVPVENDORSTATUS = AAAK.spGETKVPVENDORSTATUS;
            public const string spGETKVPPARTSTATUS = AAAK.spGETKVPPARTSTATUS;
            public const string spOTSFINDBYBU = AAAK.spOTSFINDBYBU;
            public const string spOTSFINDBYDATE = AAAK.spOTSFINDBYDATE;
            public const string spOTSFINDBYDESCRIPTION = AAAK.spOTSFINDBYDESCRIPTION;
            public const string spOTSFINDBYPARTNUMBER = AAAK.spOTSFINDBYPARTNUMBER;
            public const string spOTSFINDBYPRODUCT = AAAK.spOTSFINDBYPRODUCT;
            public const string spOTSFINDBYREQUESTOR = AAAK.spOTSFINDBYREQUESTOR;
            public const string spOTSFINDBYVENDOR = AAAK.spOTSFINDBYVENDOR;
            public const string spOTSFINDBYVENDORPARTNUMBER = AAAK.spOTSFINDBYVENDORPARTNUMBER;
            public const string spOTSGETPNINFO = AAAK.spOTSGETPNINFO;
            public const string spOTSUPDATEPARTSTABLE = AAAK.spOTSUPDATEPARTSTABLE;
            public const string spOTSUPDATE_ADDVENDORPNTOOTSPARTNUMBER = AAAK.spOTSUPDATE_ADDVENDORPNTOOTSPARTNUMBER;
            public const string spOTSUPDATEVENDORPNTABLE = AAAK.spOTSUPDATEVENDORPNTABLE;
            public const string spOTSWHEREUSEDFORVENDORPARTNUMBER = AAAK.spOTSWHEREUSEDFORVENDORPARTNUMBER;
            public const string spOTSUPDATEPARTSBASEDINAVL = AAAK.spOTSUPDATEPARTSBASEDINAVL;
        }

        public partial class SPVar
        {
            public const string basePartNumber = "@basePartNumber";
            public const string searchString = "@searchString";
            public const string searchID = "@searchID";
            public const string searchDate = "@searchDate";
            public const string intReturnCode = "@intReturnCode";
        }

        public partial class SP_COLALIAS
        {
            public const string PTYPEID = AAAK.SP_COLALIAS.PTYPEID;
            public const string PSUBTYPEID = AAAK.SP_COLALIAS.PSUBTYPEID;
            public const string BU = AAAK.SP_COLALIAS.BU;
            public const string VENDORPNID = AAAK.SP_COLALIAS.VENDORPNID;
        }

        /// <summary>
        /// App Key constants, from webAppID; these signify which group of controls an ID represents
        /// </summary>
        public class AppKeys
        {
            public const int CUSTOM_PN = 1;
            public const int GET_NEWOTSPN = 2;
            public const int SEARCH_OTS = 3;
            public const int VIEW_OTS_BASIC_INFO = 4;
            public const int VIEW_OTS_COST = 5;
            public const int VIEW_OTS_PARAMS = 6;
            public const int VIEW_OTS_HISTORY = 7;
        }

    }
    //Database constants - if you are not using the default scripts provided with this databse,
    //change the values on the right

        /// <summary>
        /// Index constants for divOTSNewIn controls; leave alone if your database matches sample
        /// </summary>
    public class OTSINDK
    {
        //Generic constants
        public const int ID_INDEX = AAAK.OTSINDK.ID_INDEX;

        public const int TYPE = AAAK.OTSINDK.TYPE;
        public const int SUBTYPE = AAAK.OTSINDK.SUBTYPE;
        public const int PKG = AAAK.OTSINDK.PKG;
        public const int VALUE = AAAK.OTSINDK.VALUE;
        public const int TOL = AAAK.OTSINDK.TOL;
        public const int SIZE = AAAK.OTSINDK.SIZE;
        public const int MPN = AAAK.OTSINDK.MPN;
        public const int VENDOR = AAAK.OTSINDK.VENDOR;
        public const int ENG = AAAK.OTSINDK.ENG;
        public const int BU = AAAK.OTSINDK.BU;
        public const int PRODUCT = AAAK.OTSINDK.PRODUCT;

        //Constants representing the control ID of the parameters the user wants to search for
        public const int FINDBY_OPTIONS = AAAK.OTSINDK.FINDBY_OPTIONS;
        public const int FIND_TEXT = AAAK.OTSINDK.FIND_TEXT;
        public const int FINDBYBU = AAAK.OTSINDK.FINDBYBU;
        public const int FINDBYREQUESTOR = AAAK.OTSINDK.FINDBYREQUESTOR;
        public const int FINDBYDATE = AAAK.OTSINDK.FINDBYDATE;

        //Constants representing the method the user has requested to search; this is taken from webKVP, 
        //and is sent to us through control ID FINDBY_OPTIONS
        public const int SEARCHMETHOD_BYPN = AAAK.OTSINDK.SEARCHMETHOD_BYPN;
        public const int SEARCHMETHOD_BYBU = AAAK.OTSINDK.SEARCHMETHOD_BYBU;
        public const int SEARCHMETHOD_BYREQUESTOR = AAAK.OTSINDK.SEARCHMETHOD_BYREQUESTOR;
        public const int SEARCHMETHOD_BYVENDOR = AAAK.OTSINDK.SEARCHMETHOD_BYVENDOR;
        public const int SEARCHMETHOD_BYVENDORPN = AAAK.OTSINDK.SEARCHMETHOD_BYVENDORPN;
        public const int SEARCHMETHOD_BYDATE = AAAK.OTSINDK.SEARCHMETHOD_BYDATE;
        public const int SEARCHMETHOD_BYPRODUCT = AAAK.OTSINDK.SEARCHMETHOD_BYPRODUCT;
        public const int SEARCHMETHOD_BYDESCRIPTION = AAAK.OTSINDK.SEARCHMETHOD_BYDESCRIPTION;

        //Constants representing the Control ID of Part Number characteristics the user wants to edit
        public const int CHANGE_DESC = AAAK.OTSINDK.CHANGE_DESC;
        public const int CHANGE_DESC2 = AAAK.OTSINDK.CHANGE_DESC2;
        public const int CHANGE_PARTTYPE = AAAK.OTSINDK.CHANGE_PARTTYPE;
        public const int CHANGE_PARTSUBTYPE = AAAK.OTSINDK.CHANGE_PARTSUBTYPE;

        //Constants representing the Control Id of Vendor Part Number characteristics the user wants to edit
        public const int CHANGE_VENDOR = AAAK.OTSINDK.CHANGE_VENDOR;
        public const int CHANGE_VENDORSTATUS = AAAK.OTSINDK.CHANGE_VENDORSTATUS;
        public const int CHANGE_VENDORPN = AAAK.OTSINDK.CHANGE_VENDORPN;
        public const int CHANGE_VENDORPNSTATUS = AAAK.OTSINDK.CHANGE_VENDORPNSTATUS;
        public const int CHANGE_VENDORPN_DATASHEET = AAAK.OTSINDK.CHANGE_VENDORPN_DATASHEET;
        public const int CHANGE_VENDORPN_COST_LOW = AAAK.OTSINDK.CHANGE_VENDORPN_COST_LOW;
        public const int CHANGE_VENDORPN_COST_HIGH = AAAK.OTSINDK.CHANGE_VENDORPN_COST_HIGH;
        public const int CHANGE_VENDORPN_COST_ENG = AAAK.OTSINDK.CHANGE_VENDORPN_COST_ENG;
        public const int CHANGE_VENDORPN_HEIGHT = AAAK.OTSINDK.CHANGE_VENDORPN_HEIGHT;
        public const int CHANGE_VENDORPN_ECODE = AAAK.OTSINDK.CHANGE_VENDORPN_ECODE;
        public const int CHANGE_VENDORPN_WHEREUSED = AAAK.OTSINDK.CHANGE_VENDORPN_WHEREUSED;
    }


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
        /// <param name="dctDefaultOverride">A dictionary that maps the default value supplied in the database
        /// to the value that overrides the default</param>
        /// <param name="uid">Unique identifier to append to the ID of each control generated in this method.
        /// One possible value is the Database ID of the record these fields are displaying.
        /// This is needed if you are calling this method several times on page for the same type of output, for
        /// example, if you are doing an accordion or tab page</param>
        /// <param name="displayType">Enter the option that will determine how this code will arrange the controls.
        /// The default value is 1, resulting in each field sequentially listed after another.  See Documentation for how this looks.
        /// To customize, define a new code, and call this procedure using that code number</param>
        /// <param name="blElementsInLine">If true, then all elements are displayed in-line; no line-breaks are put in the output</param>
        public void ConstructInputControls(SqlDataReader dR, System.Web.UI.Control cntlContainer,
            Dictionary<string, string> dctDefaultOverride, string uid, int displayType,
            Boolean blElementsInLine = false)
        {
            /* Each time we go through a dR read loop, we will add the html control strings it spawns to a string.
             * That string will be enclosed in a div element; this allows us to toggle visibility easily later.
             * THAT string will be added to the string builder.
             * When done, the stringbuilder contents will be used to generate a Literal Control, which will be added to cntlContainer.
            */
            try
            {
                //String constants for the css class of the input fields
                StringBuilder masterControlSet = new StringBuilder();

                AAAK.DISPLAYTYPES elDtype = AAAK.DISPLAYTYPES.BLOCK;
                if (blElementsInLine)
                {
                    elDtype = AAAK.DISPLAYTYPES.INLINE;
                }
                clsDB x = new clsDB();
                while (dR.Read())
                {
                    //The base class
                    string txtCSSClass = "txtinput";
                    string cboCSSClass = "cboinput";
                    string valToDisplay = x.Fld2Str(dR[DBK.txtDEFAULTVALUE]);
                    if (dctDefaultOverride.ContainsKey(valToDisplay))
                    {
                        valToDisplay = dctDefaultOverride[valToDisplay];
                    }

                    string classSuffix = x.Fld2Str(dR[DBK.cssCLASS]);
                    /*if (!classSuffix.StartsWith("_") && classSuffix != "")
                    {
                        classSuffix = "_" + classSuffix;
                    }*/
                    Boolean rdonly = false;
                    if (!(Boolean)dR[DBK.blINITENABLED])
                    {
                        rdonly = true;
                    }
                    else
                    {
                        rdonly = false;
                    }

                    //If the field is not initially enabled, but the user can edit the field, then
                    //append 'toggle' to the end of the class name
                    if (!(Boolean)dR[DBK.blINITENABLED] && (Boolean)dR[DBK.blUSERCANEDIT])
                    {
                        //Here we assign two classes-- txt/cboCSSClass is used to recognize this as an input
                        //from which to extract data, while the _toggle class indicates this field should be toggled
                        //on/off when the user presses the appropriate button 
                        txtCSSClass = txtCSSClass + " " + classSuffix + " " + "toggle";
                        cboCSSClass = cboCSSClass + " " + classSuffix + " " + "toggle";
                    }
                    else
                    {
                        txtCSSClass = txtCSSClass + " " + classSuffix;
                        cboCSSClass = cboCSSClass + " " + classSuffix;
                    }
                    //Initialize the control set string
                    StringBuilder dRControlSet = new StringBuilder();
                    System.Int16 index = (System.Int16)dR[DBK.intIDCODE];
                    //Determine the name of the main control
                    string mainCtlName = "";
                    switch ((System.Byte)dR[AAAK.fkCONTROLTYPE])
                    {
                        case CntlDecoder.TEXTBOX:
                        case CntlDecoder.TXT_W_BROWSE_ATTACHMENTS:
                            mainCtlName = "txt_";
                            break;
                        case CntlDecoder.YESNO_DROPDOWN:
                            mainCtlName = "cbo_";
                            break;
                        case CntlDecoder.MULT_DROPDOWN:
                            mainCtlName = "cbo_";
                            break;
                    }
                    mainCtlName = mainCtlName + index + uid;

                    switch (displayType)
                    {
                        case -1:
                            //Start with label describing entry requirement
                            dRControlSet.Append(DynControls.html_label_string("lbl_" + index + uid,
                                                                            (string)dR[DBK.lblPROMPT],
                                                                            "fldlabel" + classSuffix,
                                                                            mainCtlName,
                                                                            true,
                                                                            elDtype)
                                                      );
                            //Add linebreak
                            //if ((Boolean)dR[DBK.blINITVISIBLE])
                            //{
                            //    dRControlSet.Append(DynControls.html_linebreak_string());
                            //}

                            //Next, the actual input element
                            switch ((System.Byte)dR[AAAK.fkCONTROLTYPE])
                            {
                                case CntlDecoder.TEXTBOX:
                                    dRControlSet.Append(DynControls.html_txtbox_string("txt_" + index + uid,
                                                                                       txtCSSClass,
                                                                                        valToDisplay,
                                                                                       true,
                                                                                       elDtype,
                                                                                       (string)dR[DBK.lblHELPMESSAGE],
                                                                                       rdonly)
                                                              );
                                    break;
                                case CntlDecoder.YESNO_DROPDOWN:
                                    dRControlSet.Append(DynControls.html_combobox_YESNO_string("cbo_" + index + uid,
                                                                                                cboCSSClass,
                                                                                                dR[DBK.blSELECTIONREQUIRED].Equals(true),
                                                                                                dR[DBK.txtDEFAULTVALUE].Equals("0"),
                                                                                                true,
                                                                                                elDtype,
                                                                                                (string)dR[DBK.lblHELPMESSAGE],
                                                                                                rdonly)
                                                              );
                                    break;
                                case CntlDecoder.MULT_DROPDOWN:

                                    SqlCommand cmd = new SqlCommand();
                                    clsDB myDB = new clsDB();
                                    List<string> lstKVPs = new List<string>();
                                    List<SqlParameter> ps = new List<SqlParameter>();

                                    //We need to get the key-value pairs from the database.  There are two ways this can happen.
                                    //1) We can use the default procedure:
                                    string sp = DBK.SP.spGETWEBKEYVALUEPAIRINFO;
                                    string appKey = "kvpl_" + Convert.ToString(dR[DBK.fkKVPGROUPIDDecoder]);
                                    //2)...but that can get overridden by a special sp stored in the database.
                                    if (!((string)dR[DBK.strSTOREDPROC] == ""))
                                    {
                                        sp = (string)dR[DBK.strSTOREDPROC];
                                        appKey = "kvpl_" + sp;
                                    }

                                    //See if the kvp is already stored in the App Object

                                    lstKVPs = clsUtil.PutKVPInDictionary(appKey);

                                    dRControlSet.Append(DynControls.html_combobox_string("cbo_" + index + uid,
                                                                                         lstKVPs,
                                                                                         cboCSSClass,
                                                                                         dR[DBK.blSELECTIONREQUIRED].Equals(true),
                                                                                         valToDisplay,
                                                                                         true,
                                                                                         elDtype,
                                                                                         (string)dR[DBK.lblHELPMESSAGE],
                                                                                         rdonly)
                                                              );
                                    break;
                                case CntlDecoder.TXT_W_BROWSE_ATTACHMENTS:
                                    //The input element that will hold the file
                                    dRControlSet.Append(DynControls.html_input_string("input_" + index + uid, "file", "fileinput",
                                        AAAK.DISPLAYTYPES.NONE, "Browse for file to send to Component Engineer"));
                                    //And the text box
                                    //dRControlSet.Append(DynControls.html_txtbox_string("txt_" + index, "", "", true,
                                    //   AAAK.DISPLAYTYPES.INLINE, (string)dR[DBK.lblHELPMESSAGE]));

                                    //The browse button...
                                    // dRControlSet.Append(DynControls.html_button_string("btn_" + index, "BROWSE",
                                    //   "", true, AAAK.DISPLAYTYPES.INLINE, "Click to Browse for file..."));
                                    //Since the last element was inline, and a break
                                    //dRControlSet.Append(DynControls.html_linebreak_string());
                                    break;
                                default:
                                    break;
                            }
                            //Add linebreak
                            //dRControlSet.Append(DynControls.html_linebreak_string());

                            //Control for error label
                            dRControlSet.Append(DynControls.html_label_string("lblError_" + index + uid,
                                                                            "",
                                                                            "errorlabel" + classSuffix,
                                                                            "",
                                                                            false,
                                                                            elDtype)
                                                        );
                            //DONT'T Add linebreak-- error label is not visible, so display:none, and it takes no space
                            //containingDiv.Controls.Add(DynControls.html_linebreak());

                            //Control for help label
                            dRControlSet.Append(DynControls.html_label_string("lblHelp_" + index + uid,
                                                                            (string)dR[DBK.lblHELPLINK],
                                                                            "helplabel" + classSuffix,
                                                                            "",
                                                                            !dR[DBK.lblHELPMESSAGE].Equals(""),
                                                                            elDtype)
                                                       );
                            //Add linebreak after the label, if the help message is <> ""
                            //if (!dR[DBK.lblHELPMESSAGE].Equals(""))
                            //{
                            //   dRControlSet.Append(DynControls.html_linebreak_string());
                            //}

                            if (!blElementsInLine)
                            {
                                //Always terminate with a separating linebreak
                                dRControlSet.Append(DynControls.html_linebreak_string());
                            }

                            break;
                        default:

                            break;

                    }// end display switch statement
                     //Here is where we determine whether the div is visible.
                    string qVisible = "";
                    string qID = DynControls.encodeProperty("id", "visdiv_" + index + uid);
                    if (!(Boolean)dR[DBK.blINITVISIBLE])
                    {
                        qVisible = DynControls.encodeProperty("style", "display:none");
                    }
                    masterControlSet.Append("<div " +
                        qID +
                        qVisible +
                        ">" +
                        dRControlSet.ToString() +
                        "</div>");

                } //end reading dR record

                cntlContainer.Controls.Add(new LiteralControl(masterControlSet.ToString()));
            }
            catch (Exception EX)
            {
                string x = EX.Message;
            }
        }


        /// <summary>
        /// Returns a string response to the input obtained when the user pressed Submit
        /// </summary>
        /// <param name="input">String encoded response from user; expected format is
        /// ID_0!#!VALUE_0!#!ID_1!#!VALUE_1...ID_N-1!#!_VALUE_N-1</param>
        /// <returns></returns>
        public string respondToSubmitButton(string input)
        {
            try
            {
                string[] dlim = { AAAK.DELIM };
                string[] kvp = input.Split(dlim, StringSplitOptions.None);
                List<HTMLStrings.TableRow> tblRows = new List<HTMLStrings.TableRow>();
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

                for (int i = 0; i < kvp.Length; i = i + 2)
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

        /// <summary>
        /// Returns a response when the user presse btnOTSNewIn to get a new part number for an OTS part.
        /// We do this by calling stored proc spOTSInsertNewPN, which initializes the various tables in the database
        /// based on the user input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string getNewOTSPN(string input)
        {
            try
            {
                string[] dlim = { AAAK.DELIM };
                string[] kvp = input.Split(dlim, StringSplitOptions.None);
                Int32 partTypeID = 0;
                string partSubType = "";
                string pkg = "";
                string val = "";
                string tol = "";
                string size = "";
                string mpn = "";
                string vendor = "";
                string bucode = "";
                Int64 engID = -1;
                string prod = "";
                string otsPN = "";
                for (int i = 0; i < kvp.Length; i = i + 2)
                {
                    switch (Convert.ToInt16(kvp[i]))
                    {
                        case OTSINDK.TYPE:
                            partTypeID = Convert.ToInt32(kvp[i + 1]);
                            break;
                        case OTSINDK.SUBTYPE:
                            partSubType = extractNewOTSPartValue(kvp[i + 1], "SUBTYPE").ToUpper();
                            break;
                        case OTSINDK.PKG:
                            pkg = extractNewOTSPartValue(kvp[i + 1], "PKG").ToUpper();
                            break;
                        case OTSINDK.VALUE:
                            val = extractNewOTSPartValue(kvp[i + 1], "VALUE").ToUpper();
                            break;
                        case OTSINDK.TOL:
                            tol = extractNewOTSPartValue(kvp[i + 1], "TOL").ToUpper();
                            break;
                        case OTSINDK.SIZE:
                            size = extractNewOTSPartValue(kvp[i + 1], "SIZE").ToUpper();
                            break;
                        case OTSINDK.MPN:
                            mpn = kvp[i + 1].ToUpper();
                            break;
                        case OTSINDK.VENDOR:
                            vendor = kvp[i + 1].ToUpper();
                            break;
                        case OTSINDK.ENG:
                            engID = Convert.ToInt64(kvp[i + 1]);
                            break;
                        case OTSINDK.BU:
                            bucode = kvp[i + 1].ToUpper();
                            break;
                        case OTSINDK.PRODUCT:
                            prod = kvp[i + 1].ToUpper();
                            break;
                        default:
                            break;
                    }
                } //end for loop
                //Construct the description:
                Dictionary<string, string> dct = (Dictionary<string, string>)HttpContext.Current.Application["kvpd_" + DBK.SP.spGETKVPPARTTYPES];
                string desc = dct[(Convert.ToString(partTypeID))] + "," +
                    partSubType + "," +
                    pkg + ",";
                //Did user put tolerance or size?
                if (size.StartsWith("<") && tol.StartsWith("<"))
                {
                    //tolerance takes precedence
                    desc = desc + tol;
                }
                else if (!size.StartsWith("<") && !tol.StartsWith("<"))
                {
                    //tolerance takes precedence
                    desc = desc + tol;
                }
                else if (size.StartsWith("<"))
                {
                    desc = desc + tol;
                }
                else if (tol.StartsWith("<"))
                {
                    desc = desc + size;
                }
                //Now, get the next available PN
                clsSynch x = new clsSynch();
                int BasePN = x.GetOTSBasePartNumber();
                if (BasePN > 1e6)
                {
                    return "<p>Part number is " + Convert.ToString(BasePN) + ", which is more than 6 digits.</p>" +
                        "<p>The developer needs to check the seed value; you may need to start a new numbering system.</p>";
                }
                else if (BasePN < 0)
                {
                    return "<p>Part number is " + Convert.ToString(BasePN) + ", which obviously is invalid.</p>" +
                        "<p>This indicates an error occurred.  Please contact the developer.</p>";
                }
                else
                {
                    Dictionary<string, string> d = (Dictionary<string, string>)HttpContext.Current.Application[AppCode.K.BUIDToBUPNCode];
                    otsPN = Convert.ToString(BasePN).PadLeft(5, '0') + "-000-" + d[bucode];
                }
                //Now, call the sp that takes the values and initializes an ots part in the database.
                clsDB myDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter(myDB.makeParameterName(DBK.strPARTNUMBER), otsPN));
                ps.Add(new SqlParameter(myDB.makeParameterName(DBK.keyREQUESTEDBY), engID));
                ps.Add(new SqlParameter(myDB.makeParameterName(DBK.strVENDOR), vendor));
                ps.Add(new SqlParameter(myDB.makeParameterName(DBK.strVENDORPARTNUMBER), mpn));
                ps.Add(new SqlParameter(myDB.makeParameterName(DBK.strDESCRIPTION), desc));
                ps.Add(new SqlParameter(myDB.makeParameterName(DBK.keyTYPE), partTypeID));
                ps.Add(new SqlParameter(myDB.makeParameterName(DBK.strPRODUCT), prod));
                ps.Add(myDB.makeReturnParameter(System.Data.SqlDbType.Int));
                //Since we are using a return parameter, the cmd object's command type must be stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                int insertResult = -1;
                using (myDB.OpenConnection())
                {
                    myDB.ExecuteSP(DBK.SP.spOTSINSERTNEWPARTNUMBER, ps, clsDB.SPExMode.NONQUERY, ref cmd);
                    insertResult = (int)cmd.Parameters["@returnVal"].Value;
                }
                if (insertResult == 0)
                {
                    return "<p>Part number</p><br >" + otsPN +
                        "<br /><p>...has been associated with</p><br />" + mpn + "<br /><p>" +
                        "Before Nov 28, a feature will be added that will email you and the Component Engineer " +
                        "of this new Part Number (you can search for this part number in the Search Feature)." + "</p><p>" +
                        "The email will contain the file you uploaded as an attachment, so that it can be put into box.  Longer term, " +
                        "Algie can create a Box folder and upload your attachments there for you.</p>";
                }
                else
                {
                    return "<p>The database Insert Command returned Code " + insertResult +
                        ".  This indicates the part number was not created successfully.  Eventually, an email will " +
                        "be sent to the developers to notify them of the problem.  For now, please contact Alberto Campos.</p>";
                }


            }
            catch (Exception ex)
            {
                return "<p>Method getNewOTSPN: " + ex.Message + "<p>";

            }
        }

        /// <summary>
        /// Returns a string based on the input value;
        /// this string eventually becomes part of the description for a new OTS part
        /// </summary>
        /// <param name="inputV"></param>
        /// <param name="meaning"></param>
        /// <returns></returns>
        private string extractNewOTSPartValue(string inputV, string meaning)
        {
            if (inputV == "")
            {
                return "[" + meaning + " UNKNOWN]";
            }
            else
            {
                return inputV;
            }
        }

        //Returns an object that tells the client:
        //1) The result of the search
        //2) A table of all elements that match the search conditions
        public string findMatchingPN(string input)
        {
            ParametricSearchResponse retObj = new ParametricSearchResponse();
            string[] dlim = { AAAK.DELIM };
            string[] kvp = input.Split(dlim, StringSplitOptions.None);
            int searchMethod = -1;
            string searchText = "";
            for (int i = 0; i < kvp.Length; i = i + 2)
            {
                switch (Convert.ToInt16(kvp[i]))
                {
                    case OTSINDK.FINDBY_OPTIONS:
                        searchMethod = Convert.ToInt32(kvp[i + 1]);
                        break;
                    default:
                        searchText = kvp[i + 1].ToUpper();
                        break;
                }
            } //end for loop

            //Do the search
            string sp = "";
            clsDB myDB = new clsDB();
            SqlCommand cmd = new SqlCommand();
            List<SqlParameter> ps = new List<SqlParameter>();
            string fldName = "";
            string htmlSearchText = "<span " + DynControls.encodeProperty("style", "color:red") + "><b>" +
                    searchText + "</b></span>";
            switch (searchMethod)
            {
                case OTSINDK.SEARCHMETHOD_BYPRODUCT:
                    sp = DBK.SP.spOTSFINDBYPRODUCT;
                    ps.Add(new SqlParameter(DBK.SPVar.searchString, "%" + searchText + "%"));
                    fldName = "Requested for Product";
                    break;
                case OTSINDK.SEARCHMETHOD_BYBU:
                    sp = DBK.SP.spOTSFINDBYBU;
                    ps.Add(new SqlParameter(DBK.SPVar.searchID, Convert.ToByte(searchText)));
                    fldName = "BU";
                    break;
                case OTSINDK.SEARCHMETHOD_BYREQUESTOR:
                    sp = DBK.SP.spOTSFINDBYREQUESTOR;
                    ps.Add(new SqlParameter(DBK.SPVar.searchID, Convert.ToByte(searchText)));
                    //TEST ONLY
                    HttpContext.Current.Session[SESSIONKEYS.UID] = Convert.ToInt64(searchText);
                    fldName = "Requestor";
                    break;
                case OTSINDK.SEARCHMETHOD_BYVENDOR:
                    sp = DBK.SP.spOTSFINDBYVENDOR;
                    ps.Add(new SqlParameter(DBK.SPVar.searchString, "%" + searchText + "%"));
                    fldName = "Vendor";
                    break;
                case OTSINDK.SEARCHMETHOD_BYVENDORPN:
                    sp = DBK.SP.spOTSFINDBYVENDORPARTNUMBER;
                    ps.Add(new SqlParameter(DBK.SPVar.searchString, "%" + searchText + "%"));
                    fldName = "Vendor Part Number";
                    break;
                case OTSINDK.SEARCHMETHOD_BYPN:
                    sp = DBK.SP.spOTSFINDBYPARTNUMBER;
                    ps.Add(new SqlParameter(DBK.SPVar.searchString, "%" + searchText + "%"));
                    fldName = "Part Number";
                    break;
                case OTSINDK.SEARCHMETHOD_BYDATE:
                    sp = DBK.SP.spOTSFINDBYDATE;
                    ps.Add(new SqlParameter(DBK.SPVar.searchDate, DateTime.Parse(searchText)));
                    fldName = "Date Requested";
                    break;
                case OTSINDK.SEARCHMETHOD_BYDESCRIPTION:
                    sp = DBK.SP.spOTSFINDBYDESCRIPTION;
                    ps.Add(new SqlParameter(DBK.SPVar.searchString, "%" + searchText + "%"));
                    fldName = "Description";
                    break;
                default:
                    return "<p>Did not know how to perform search for Method with ID " + Convert.ToString(searchMethod + ".</p>") +
                        dlim[0] + "<p>NO DATA</p>";
            }

            int nResults = 0; //The number of records returned by the search
            int expandSpan = 0; //The span of the hidden row that will be used to display more information about a PN.
            List<HTMLStrings.TableRow> tblRows = new List<HTMLStrings.TableRow>();

            using (myDB.OpenConnection())
            {
                using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(sp, ps, clsDB.SPExMode.READER, ref cmd))
                {
                    if (dR != null)
                    {
                        if (dR.HasRows)
                        {
                            //Add the Header Row
                            switch (searchMethod)
                            {
                                case OTSINDK.SEARCHMETHOD_BYVENDOR:
                                case OTSINDK.SEARCHMETHOD_BYVENDORPN:
                                    //This includes a Column for Vendor and Vendor PN
                                    tblRows.Add(new HTMLStrings.TableRow(
                                                "otsFindResultsHeader",
                                                "clsHeaderRow",
                                                new HTMLStrings.TableCell[] {
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","STATUS",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","PART NUMBER",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","DESCRIPTION",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","VENDOR",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","VENDOR PART NUMBER",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","REQUESTED BY",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","BU",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","REQUESTED FOR",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","DATE REQUESTED",1,true),
                                                                            }
                                                                          )
                                                         );
                                    break;
                                default:
                                    //This includes a Column for Vendor and Vendor PN
                                    tblRows.Add(new HTMLStrings.TableRow(
                                                "otsFindResultsHeader",
                                                "clsHeaderRow",
                                                new HTMLStrings.TableCell[] {
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","STATUS",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","PART NUMBER",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","DESCRIPTION",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","REQUESTED BY",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","BU",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","REQUESTED FOR",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","DATE REQUESTED",1,true),
                                                                            }
                                                                          )
                                                         );
                                    break;
                            }
                            expandSpan = tblRows[0].Cells.Count();
                            string status = "";
                            string pn = "";
                            string desc = "";
                            string reqby = "";
                            string bu = "";
                            string reqfor = "";
                            string dtreq = "";
                            string vendor = "";
                            string vendorpn = "";
                            string pnID = "";
                            while (dR.Read())
                            {
                                //Format the text in the data reader
                                pnID = Convert.ToString(dR[DBK.ID]);
                                status = Convert.ToString(dR[DBK.strSTATUS]);
                                pn = Convert.ToString(dR[DBK.strPARTNUMBER]);
                                desc = Convert.ToString(dR[DBK.strDESCRIPTION]);
                                reqby = Convert.ToString(dR[DBK.strNAME]);
                                bu = Convert.ToString(dR[DBK.valDISPLAYEDVALUE]);
                                reqfor = Convert.ToString(dR[DBK.strPRODUCT]);
                                dtreq = Convert.ToString(dR[DBK.dtREQUESTED]);
                                string buttonhtml = DynControls.html_button_string("otsbtnFoundExpand_" + pnID, "Expand", "otsbtnFoundExpand",
                                    true, AAAK.DISPLAYTYPES.BLOCK, "Expand to get more information about the Part Number");
                                //Handle the fact that certain search methods show special fields
                                switch (searchMethod)
                                {
                                    case OTSINDK.SEARCHMETHOD_BYVENDOR:
                                    case OTSINDK.SEARCHMETHOD_BYVENDORPN:
                                        vendor = Convert.ToString(dR[DBK.strVENDOR]);
                                        vendorpn = Convert.ToString(dR[DBK.strVENDORPARTNUMBER]);
                                        break;
                                    default:
                                        vendor = "";
                                        vendorpn = "";
                                        break;
                                }
                                switch (searchMethod)
                                {
                                    case OTSINDK.SEARCHMETHOD_BYPRODUCT:
                                        reqfor = DynControls.EmphasizeText(reqfor, searchText, "red", true);
                                        break;
                                    case OTSINDK.SEARCHMETHOD_BYBU:
                                        bu = DynControls.EmphasizeText(bu, bu, "red", true);
                                        break;
                                    case OTSINDK.SEARCHMETHOD_BYREQUESTOR:
                                        reqby = DynControls.EmphasizeText(reqby, reqby, "red", true);
                                        break;
                                    case OTSINDK.SEARCHMETHOD_BYVENDOR:
                                        vendor = DynControls.EmphasizeText(vendor, searchText, "red", true);
                                        break;
                                    case OTSINDK.SEARCHMETHOD_BYVENDORPN:
                                        vendorpn = DynControls.EmphasizeText(vendorpn, searchText, "red", true);
                                        break;
                                    case OTSINDK.SEARCHMETHOD_BYPN:
                                        pn = DynControls.EmphasizeText(pn, searchText, "red", true);
                                        break;
                                    case OTSINDK.SEARCHMETHOD_BYDATE:
                                        dtreq = DynControls.EmphasizeText(dtreq, dtreq, "red", true);
                                        break;
                                    case OTSINDK.SEARCHMETHOD_BYDESCRIPTION:
                                        desc = DynControls.EmphasizeText(desc, searchText, "red", true);
                                        break;
                                    default:
                                        break;
                                }

                                //Consistent with the header row, the style of row we add for depends on what the user searched for
                                switch (searchMethod)
                                {
                                    case OTSINDK.SEARCHMETHOD_BYVENDOR:
                                    case OTSINDK.SEARCHMETHOD_BYVENDORPN:
                                        //This includes a Column for Vendor and Vendor PN
                                        tblRows.Add(new HTMLStrings.TableRow(
                                                    "otsFindResultsRow_" + pnID,
                                                    "otsFindResultsRow",
                                                    new HTMLStrings.TableCell[] {
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell withExpandButton",buttonhtml.Replace("btnOTSFOUNDID_", "btnOTSFOUNDID_" + pnID),1,false,true),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",status,1,false),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",pn,1,false),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",desc,1,false),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",vendor,1,false),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",vendorpn,1,false),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",reqby,1,false),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",bu,1,false),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",reqfor,1,false),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",dtreq,1,false),
                                                                                }
                                                                              )
                                                             );
                                        break;
                                    default:
                                        tblRows.Add(new HTMLStrings.TableRow(
                                                    "otsFindResultsRow_" + pnID,
                                                    "otsFindResultsRow",
                                                    new HTMLStrings.TableCell[] {
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",buttonhtml.Replace("btnOTSFOUNDID_", "btnOTSFOUNDID_" + pnID),1,false,true),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",status,1,false),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",pn,1,false),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",desc,1,false),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",reqby,1,false),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",bu,1,false),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",reqfor,1,false),
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",dtreq,1,false),
                                                                                }
                                                                              )
                                                             );
                                        break;
                                } //end switch
                                  //We need to add one hidden row that will expand.
                                HTMLStrings.TableRow expandRow = new HTMLStrings.TableRow(
                                            "otsExpandResultsRow_" + pnID,
                                            "otsExpandResultsRow",
                                            new HTMLStrings.TableCell[] {
                                                                    new HTMLStrings.TableCell("otsDisplayAreaFor_" + pnID,
                                                                                              "otsFindResultsExpansionCell",
                                                                                              "",
                                                                                              expandSpan,false),
                                                                        }
                                                                      );
                                expandRow.DisplayStyle = AAAK.DISPLAYTYPES.NONE;
                                tblRows.Add(expandRow);
                                nResults = nResults + 1;
                            } //end single pass over datareader
                        }
                        else
                        {
                            return "<p>Your search for '" + htmlSearchText + "' in the  '" + fldName + "' field returned 0 results.</p>" +
                                dlim[0] + "<p>NO DATA</p>";
                        }
                    }
                    else
                    {
                        //null data reader
                        return "<p>Method FindMatchingPN returned a null dataset.  This is a coding error.</p>" +
                            dlim[0] + "<p>NO DATA</p>";
                    }
                }
            }

            string resultMessage = "<p>Your search for '" + htmlSearchText + "' in the '" + fldName + "' field returned " +
                DynControls.EmphasizeText(Convert.ToString(nResults), Convert.ToString(nResults), "red", true) + " records.";
            HTMLStrings.Table resultTable = new HTMLStrings.Table("otsSearchResults", "otsSearchResults", tblRows);
            return resultMessage + dlim[0] + resultTable.ToHTML();
        }

        /// <summary>
        /// Returns html created for the given Part Number ID by querying the database
        /// Format of input: just the Database ID of the Part Number we want to retrieve (otsParts.ID)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string getHTMLForPartNumberID(string input)
        {
            //Open a data reader for the id to the database
            Int64 pnID = Convert.ToInt64(input);
            clsDB myDB = new clsDB();
            SqlCommand cmd = new SqlCommand();
            List<SqlParameter> ps = new List<SqlParameter>();
            ps.Add(new SqlParameter("@pnID", pnID));
            string htmlForDataTab = "";
            string htmlForVendorPNTab = "";
            string htmlForWhereUsedTab = "";
            string partNumberForTitle = "";
            using (myDB.OpenConnection())
            {
                using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(DBK.SP.spOTSGETPNINFO, ps, clsDB.SPExMode.READER, ref cmd))
                {
                    //This stored proc returns two result sets.
                    //The first set contains information about the part.
                    //The second set contains information about all Vendor PNs that report to the part.
                    int resultSetIndex = 0;
                    do
                    {
                        if (dR != null && dR.HasRows)
                        {
                            if (resultSetIndex == 0)
                            {
                                htmlForDataTab = createHTMLStringForPNInfo(dR, myDB, ref partNumberForTitle);
                            }
                            else
                            {
                                htmlForVendorPNTab = createHTMLStringForVendorPNInfo(dR, myDB, Convert.ToString(pnID));
                            }

                        }
                        else
                        {

                        }
                        resultSetIndex = resultSetIndex + 1;
                    } while (dR.NextResult());
                }
            }
            //Title of this section
            string bookmarkurl = "Bookmark this URL to get back to this page: " + DynControls.html_hyperlink_string("", HttpContext.Current.Request.Url.AbsoluteUri + ".aspx?ID=" + pnID,
                    "bkmk_" + pnID, "bkmkID", "_blank");

            string sectionTitle = HTMLStrings.HTMLHeading(partNumberForTitle, "h1", HTMLStrings.ALIGN.LEFT, "otsviewheader") +
                "<p>" + bookmarkurl + "</p>";

            //Do the tabs
            Panel divPNInfo = new Panel();
            divPNInfo.ID = "divPNInfo_" + pnID;
            divPNInfo.Controls.Add(new LiteralControl(htmlForDataTab));
            divPNInfo.Style.Add(HtmlTextWriterStyle.OverflowX, "auto");

            Panel divPNVendorInfo = new Panel();
            divPNVendorInfo.ID = "divPNVendorInfo_" + pnID;
            //divPNVendorInfo.Width = new Unit("99%");
            divPNVendorInfo.Controls.Add(new LiteralControl(htmlForVendorPNTab));
            //divPNVendorInfo.Style.Add(HtmlTextWriterStyle.Display, "table");
            divPNVendorInfo.Style.Add(HtmlTextWriterStyle.OverflowX, "auto");

            Panel divPNWhereUsedInfo = new Panel();
            divPNWhereUsedInfo.ID = "divPNWhereUsedInfo_" + pnID;
            divPNWhereUsedInfo.Controls.Add(new LiteralControl("<p>Please wait... searching database...</p>"));
            divPNWhereUsedInfo.Style.Add(HtmlTextWriterStyle.OverflowX, "auto");
            //Set up the unordered list for the tabs.
            StringBuilder sB = new StringBuilder();
            sB.Append("<ul>");
            sB.Append("<li>" + DynControls.html_hyperlink_string("Part Properties", "#" + divPNInfo.ID));
            sB.Append("<li>" + DynControls.html_hyperlink_string("AVL (Approved Vendor List)", "#" + divPNVendorInfo.ID));
            sB.Append("<li>" + DynControls.html_hyperlink_string("Where Used", "#" + divPNWhereUsedInfo.ID));
            sB.Append("</ul>");
            return sectionTitle +
                    "<div " + DynControls.encodeProperty("id", "pnsummary_" + pnID) +
                    //DynControls.encodeProperty("style","overflow-x:auto") + ">" +
                    ">" +
                    sB.ToString() +
                    DynControls.GetHTMLText(divPNInfo) +
                    DynControls.GetHTMLText(divPNVendorInfo) +
                    DynControls.GetHTMLText(divPNWhereUsedInfo) +
                    "</div>";
        }

        /// <summary>
        /// Takes all the data in the data reader and returns the string for an html table that displays the data
        /// </summary>
        /// <param name="dR"></param>
        private string createHTMLStringForPNInfo(SqlDataReader dR, clsDB myDB, ref string FullPartNumber)
        {
            try
            {
                Panel divPN_BasicInfo = new Panel();
                Panel divPN_Cost = new Panel();
                Panel divPN_Params = new Panel();
                Panel divPN_History = new Panel();
                string pn = "";
                string pnID = "";
                while (dR.Read())
                {
                    Dictionary<string, string> dct = new Dictionary<string, string>();

                    pnID = myDB.Fld2Str(dR[DBK.ID]);
                    pn = myDB.Fld2Str(dR[DBK.strPARTNUMBER]);


                    //We need to put the following values in the dictionary for when we get values for the 
                    //web display fields from the database; note that KEY value is the Default Value from table wdcDisplayFields.
                    dct.Add("[DESC]", myDB.Fld2Str(dR[DBK.strDESCRIPTION]));
                    dct.Add("[DESC2]", myDB.Fld2Str(dR[DBK.strDESCRIPTION2]));
                    dct.Add("[PARTSTATUS]", myDB.Fld2Str(dR[DBK.strSTATUS]));
                    dct.Add("[ESTATUS]", myDB.Fld2Str(dR[DBK.strECODENAME]));

                    dct.Add("[MINCOST]", myDB.Fld2Str(dR[DBK.decLOWVOLCOST]));
                    dct.Add("[MAXCOST]", myDB.Fld2Str(dR[DBK.decHIGHVOLCOST]));
                    dct.Add("[AVGCOST]", myDB.Fld2Str(dR[DBK.decENGCOST]));

                    dct.Add("[PARTTYPE]", myDB.Fld2Str(dR[DBK.SP_COLALIAS.PTYPEID]));
                    dct.Add("[PARTSUBTYPE]", myDB.Fld2Str(dR[DBK.SP_COLALIAS.PSUBTYPEID]));
                    dct.Add("[MAXHEIGHT]", myDB.Fld2Str(dR[DBK.decMAXHEIGHT]));
                    string parameters = myDB.Fld2Str(dR[DBK.strPARAMETERS]);


                    dct.Add("[REQBY]", myDB.Fld2Str(dR[DBK.strNAME]));
                    dct.Add("[BU]", myDB.Fld2Str(dR[DBK.SP_COLALIAS.BU]));
                    dct.Add("[REQFOR]", myDB.Fld2Str(dR[DBK.strPRODUCT]));
                    dct.Add("[DTREQ]", myDB.Fld2Str(dR[DBK.dtREQUESTED]));
                    dct.Add("[VERSION]", myDB.Fld2Str(dR[DBK.intVERSION]));


                    divPN_BasicInfo.ID = "divPN_BasicInfo";
                    DynControls.GenerateControlsFromDatabase(DBK.AppKeys.VIEW_OTS_BASIC_INFO, divPN_BasicInfo, dct, "_" + pnID);

                    divPN_Cost.ID = "divPN_Cost";
                    DynControls.GenerateControlsFromDatabase(DBK.AppKeys.VIEW_OTS_COST, divPN_Cost, dct, "_" + pnID);

                    divPN_Params.ID = "divPN_Params";
                    DynControls.GenerateControlsFromDatabase(DBK.AppKeys.VIEW_OTS_PARAMS, divPN_Params, dct, "_" + pnID);

                    divPN_History.ID = "divPN_History";
                    DynControls.GenerateControlsFromDatabase(DBK.AppKeys.VIEW_OTS_HISTORY, divPN_History, dct, "_" + pnID);
                }

                FullPartNumber = pn;
                //Text for tool tips.
                string editTT = "Press to edit information [NOTE: If you cannot edit a field when after pressing 'Edit', " +
                    "you need to make your change by editing information for the Vendor Part Number on the 'AVL' tab].";
                string saveTT = "Save your changes; a record of changes made to RELEASED part numbers is stored in the database.";
                string cancelTT = "Cancel your changes; no data will change in the database.";
                string revhistoryTT = "Note: Change history is only recorded for parts whose Status has advanced beyond SUBMITTED and whose Revision is greater than 1.";
                //Make Edit, Save, and Cancel buttons
                string btnEditTop = DynControls.html_button_string("editotsinfo_top_" + pnID, "Edit", "editotsinfo_" + pnID,
                    true, AAAK.DISPLAYTYPES.INLINE, editTT, "", true);
                string btnSaveTop = DynControls.html_button_string("saveotsinfo_top_" + pnID, "Save", "saveotsinfo_" + pnID,
                    true, AAAK.DISPLAYTYPES.INLINE, saveTT, "", false);
                string btnCancelTop = DynControls.html_button_string("cancelotsinfo_top_" + pnID, "Cancel", "cancelotsinfo_" + pnID,
                    true, AAAK.DISPLAYTYPES.INLINE, cancelTT, "", false);
                string btnEditBot = DynControls.html_button_string("editotsinfo_bot_" + pnID, "Edit", "editotsinfo_" + pnID,
                    true, AAAK.DISPLAYTYPES.INLINE, editTT, "", true);
                string btnSaveBot = DynControls.html_button_string("saveotsinfo_bot_" + pnID, "Save", "saveotsinfo_" + pnID,
                    true, AAAK.DISPLAYTYPES.INLINE, saveTT, "", false);
                string btnCancelBot = DynControls.html_button_string("cancelotsinfo_bot_" + pnID, "Cancel", "cancelotsinfo_" + pnID,
                    true, AAAK.DISPLAYTYPES.INLINE, cancelTT, "", false);

                string btnViewPNHistoryTop = DynControls.html_button_string("viewpnhistory_top" + pnID, "View Revision History", "viewpnhistory_" + pnID,
                    true, AAAK.DISPLAYTYPES.INLINE, revhistoryTT, "");
                string btnViewPNHistoryBot = DynControls.html_button_string("viewpnhistory_bot" + pnID, "View Revision History", "viewpnhistory_" + pnID,
                    true, AAAK.DISPLAYTYPES.INLINE, revhistoryTT, "");

                return 
                    btnEditTop + btnSaveTop + btnCancelTop + btnViewPNHistoryTop + "<hr>" +
                    HTMLStrings.HTMLHeadingStyle1("Basic Information", "h2", "otsviewheader") +
                    DynControls.GetHTMLText(divPN_BasicInfo) +
                    HTMLStrings.HTMLHeadingStyle1("Cost", "h2", "otsviewheader") +
                    DynControls.GetHTMLText(divPN_Cost) +
                    HTMLStrings.HTMLHeadingStyle1("Parameters", "h2", "otsviewheader") +
                    DynControls.GetHTMLText(divPN_Params) +
                    HTMLStrings.HTMLHeadingStyle1("History", "h2", "otsviewheader", "hViewHistory") +
                    DynControls.GetHTMLText(divPN_History) + "<hr>" +
                    btnEditBot + btnSaveBot + btnCancelBot + btnViewPNHistoryBot + "<hr>";
                
            } catch (Exception ex)
            {
                return "<p>Method createHTMLStringForPNInfo error: " + ex.Message + "</p>";
            }

        }

        /// <summary>
        /// Returns the html for a table has all the vendor part number info for a given part number with part number
        /// pnID
        /// </summary>
        /// <param name="dR">The data reader containing the data</param>
        /// <param name="myDB">The clsDB object</param>
        /// <param name="pnID">ID in table otsParts.ID for the Part Number in question. This is used in the uid field
        /// of the input cells.</param>
        /// <returns>
        /// NOTE:
        /// The table is given ID = tblVPNInfo_[pnID]</returns>
        /// Each row is given ID = row_[VPNID]_[pnID], where [VPNID] is the vendor part number ID taken from the database.
        /// Each CONTROL in the cell in the row is given ID = [baseName]_[colInddex]_[VPNID]_[pnID]
        /// where
        /// basename = 
        /// txtinput => Textbox
        /// cboinput => Dropdown box
        /// btninput => Button
        /// Note the cells themselves do not get an ID
        private string createHTMLStringForVendorPNInfo(SqlDataReader dR, clsDB myDB, string pnID)
        {
            try
            {
                List<HTMLStrings.TableRow> tblRows = new List<HTMLStrings.TableRow>();
                List<string> lstVPNStatus = clsUtil.PutKVPInDictionary("kvpl_" + DBK.SP.spGETKVPPARTSTATUS);
                List<string> lstVStatus = clsUtil.PutKVPInDictionary("kvpl_" + DBK.SP.spGETKVPVENDORSTATUS);
                List<string> lstECode = clsUtil.PutKVPInDictionary("kvpl_" + DBK.SP.spGETKVPENVIRONCODE);
                tblRows.Add(new HTMLStrings.TableRow(
                        "row_h",
                        "clsHeaderRow",
                        new HTMLStrings.TableCell[] {
                                        new HTMLStrings.TableCell("","clsHeaderRow","Vendor",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Vendor<br>Status",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Vendor<br>Part<br>Number",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Part<br>Number<br>Status",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Datasheet",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Low<br>Vol<br>Cost",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","High<br>Vol<br>Cost",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Engineering<br>Cost",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Max<br>Height<br>(mm)",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Environmental<br>Code",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Where<br>Used",1,true)
                                                    }
                                                         )
                                 );
                while (dR.Read())
                {
                    string vpnID = myDB.Fld2Str(dR[DBK.SP_COLALIAS.VENDORPNID]);
                    string vendor = myDB.Fld2Str(dR[DBK.strVENDOR]);
                    string vStatus = myDB.Fld2Str(dR[DBK.strVENDORSTATUS]);
                    string vPN = myDB.Fld2Str(dR[DBK.strVENDORPARTNUMBER]);
                    string vPNStatus = myDB.Fld2Str(dR[DBK.keyVENDORPARTSTATUS]);
                    string vDshtURL = myDB.Fld2Str(dR[DBK.strDATASHEETURL]);
                    string lowcost = myDB.Fld2Str(dR[DBK.decLOWVOLCOST]);
                    string highcost = myDB.Fld2Str(dR[DBK.decHIGHVOLCOST]);
                    string engcost = myDB.Fld2Str(dR[DBK.decENGCOST]);
                    string height = myDB.Fld2Str(dR[DBK.decMAXHEIGHT]);
                    string ecode = myDB.Fld2Str(dR[DBK.keyVENDORENVIRONCODE]);
                    //Add an html table row to the list
                    //No need to track the row ID in the unique ID, UID.  This is because
                    //we expect a unique Vendor Part Number ID (vpnID) for each VPN.
                    string uid = "_" + vpnID + "_" + pnID;
                        tblRows.Add(new HTMLStrings.TableRow(
                        "row" + uid,
                        "clsVPNRow_" + pnID,
                        new HTMLStrings.TableCell[] {
                                                    new HTMLStrings.TableCell("","clsVPNCell_" + pnID,vendor,1,false, true,
                                                        "txtinput_" + OTSINDK.CHANGE_VENDOR + uid, null, "", "", false),
                                                    new HTMLStrings.TableCell("","clsVPNCell_" + pnID,vStatus,1,false, true,
                                                        "txtinput_" + OTSINDK.CHANGE_VENDORSTATUS + uid, null, "", "", false),
                                                    new HTMLStrings.TableCell("","clsVPNCell_" + pnID,vPN,1,false,true,
                                                        "txtinput_" + OTSINDK.CHANGE_VENDORPN + uid, null, "", "txtinput toggle " + pnID, false),
                                                    new HTMLStrings.TableCell("","clsVPNCell_" + pnID,vPNStatus,1,false,true,
                                                        "cboinput_" + OTSINDK.CHANGE_VENDORPNSTATUS + uid, lstVPNStatus, "", "cboinput toggle " + pnID, false),
                                                    new HTMLStrings.TableCell("","clsVPNCell_" + pnID,vDshtURL,1,false,true,
                                                        "txtinput_" + OTSINDK.CHANGE_VENDORPN_DATASHEET + uid, null, "", "txtinput toggle " + pnID, false),
                                                    new HTMLStrings.TableCell("","clsVPNCell_" + pnID,lowcost,1,false,true,
                                                        "txtinput_" + OTSINDK.CHANGE_VENDORPN_COST_LOW + uid, null, "", "txtinput toggle " + pnID, false),
                                                    new HTMLStrings.TableCell("","clsVPNCell_" + pnID,highcost,1,false,true,
                                                        "txtinput_" + OTSINDK.CHANGE_VENDORPN_COST_HIGH + uid, null, "", "txtinput toggle " + pnID, false),
                                                    new HTMLStrings.TableCell("","clsVPNCell_" + pnID,engcost,1,false,true,
                                                        "txtinput_" + OTSINDK.CHANGE_VENDORPN_COST_ENG + uid, null, "", "txtinput toggle " + pnID, false),
                                                    new HTMLStrings.TableCell("","clsVPNCell_" + pnID,height,1,false,true,
                                                        "txtinput_" + OTSINDK.CHANGE_VENDORPN_HEIGHT + uid, null, "", "txtinput toggle " + pnID, false),
                                                    new HTMLStrings.TableCell("","clsVPNCell_" + pnID,ecode,1,false,true,
                                                        "cboinput_" + OTSINDK.CHANGE_VENDORPN_ECODE + uid, lstECode, "", "cboinput toggle " + pnID, false),
                                                    new HTMLStrings.TableCell("","clsVPNCell_" + pnID,"VIEW",1,false,true,
                                                        "btninput_" + OTSINDK.CHANGE_VENDORPN_WHEREUSED + uid, null, "VIEW","btnViewVendorWhereUsed", true)
                                                    }
                                                         )
                                 );
                }
                //End processing the data row

                //Make buttons
                //Text for tool tips.
                string addTT = "Add information for a new Vendor Part Number.";
                string editTT = "Press to edit information [NOTE: If you cannot edit a field when after pressing 'Edit', " +
                    "you need to make your change by editing information for the Vendor Part Number on the 'AVL' tab.";
                string saveTT = "Save your changes; if either the Vendor or Vendor Part Number field is empty, that row will be ignored.";
                string cancelTT = "Cancel your changes; no data will change in the database.";
                //Make Add, Edit, Save, and Cancel buttons
                string btnEdit = DynControls.html_button_string("editvpn_" + pnID, "Edit", "editvpn_" + pnID,
                    true, AAAK.DISPLAYTYPES.INLINE, editTT, "", true);
                string btnAdd = DynControls.html_button_string("addvpn_" + pnID, "Add Vendor", "addvpn_" + pnID,
                    true, AAAK.DISPLAYTYPES.INLINE, addTT, "", false);
                string btnSave = DynControls.html_button_string("savevpn_" + pnID, "Save", "savevpn_" + pnID,
                    true, AAAK.DISPLAYTYPES.INLINE, saveTT, "", false);
                string btnCancel = DynControls.html_button_string("cancelvpn_" + pnID, "Cancel", "cancelvpn_" + pnID,
                    true, AAAK.DISPLAYTYPES.INLINE, cancelTT, "", false);

                HTMLStrings.Table tbl = new HTMLStrings.Table("tblVPNInfo_" + pnID, "tblVPNInfo", tblRows);
                return btnEdit + btnAdd + btnSave + btnCancel + 
                    "<p>Tip: If you don't see the scroll bars at the bottom of the window, use the left and right arrow keys to scroll through the full table width.</p><hr>" + tbl.ToHTML();
            } catch (Exception ex)
            {
                return "<p>Method createHTMLStringForVendorPNInfo error: " + ex.Message + "</p>";
            }
        }

        
        public string UpdatePNData(string input)
        {
            try
            {
                string msg = "";
                string html = "";
                string targetID = "";
                string[] dlim = { AAAK.DELIM };
                string[] kvp = input.Split(dlim, StringSplitOptions.None);
                clsDB myDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                for (int i = 0; i < kvp.Length; i = i + 2)
                {
                    switch (Convert.ToInt16(kvp[i]))
                    {
                        case OTSINDK.CHANGE_DESC:
                            ps.Add(new SqlParameter("@" + DBK.strDESCRIPTION, kvp[i + 1]));
                            break;
                        case OTSINDK.CHANGE_DESC2:
                            ps.Add(new SqlParameter("@" + DBK.strDESCRIPTION2, kvp[i + 1]));
                            break;
                        case OTSINDK.CHANGE_PARTSUBTYPE:
                            ps.Add(new SqlParameter("@" + DBK.keySUBTYPE, Convert.ToInt16(kvp[i + 1])));
                            break;
                        case OTSINDK.CHANGE_PARTTYPE:
                            ps.Add(new SqlParameter("@" + DBK.keyTYPE, Convert.ToInt16(kvp[i + 1])));
                            break;
                        case OTSINDK.ID_INDEX:
                            ps.Add(new SqlParameter("@" + DBK.ID, Convert.ToInt64(kvp[i + 1])));
                            targetID = kvp[i + 1];
                            break;
                        default:
                            break;
                    }
                } //end for loop

                //Add a UserID, required by stored procedure, but can't implement now because log-in is not yet ready.
                clsLogin u = new clsLogin();
                ps.Add(new SqlParameter("@" + DBK.keyUPDATEDBY, u.GetUserID()));
                ps.Add(myDB.makeOutputParameter("@changed", System.Data.SqlDbType.Bit));
                using (myDB.OpenConnection())
                {
                   object res = myDB.ExecuteSP(DBK.SP.spOTSUPDATEPARTSTABLE, ps, clsDB.SPExMode.NONQUERY, ref cmd);
                    if (res.ToString().Contains(" "))
                    {
                        msg = "<p>UpdatePNData: Unable to execute stored procedure: " + res.ToString() + "</p>";
                    } else 
                    {                   
                        if (Convert.ToString(cmd.Parameters["@changed"].Value) == "0")
                        {
                            //This indicates no change.
                            msg = "<p>The data you provided resulted in no changes to store in the database.</p>";
                        } else
                        {
                            //A change occurred; return the updated results.
                            msg = "<p>Success!</p><p>Your changes were saved in the database.</p>";
                        }
                    }
                }
                html = getHTMLForPartNumberID(targetID);
                return msg + AAAK.DELIM + html;
            } catch (Exception ex)
            {
                return "<p>Method UpdatePNData error: " + ex.Message + "</p>";
            }
        }

        /// <summary>
        /// Take the data received from the client and upates tables related to the affected Vendor Part Numbers.
        /// </summary>
        /// <param name="input">
        /// Expected format is as follows (* == whatever delimiter is used by the client; if you have not changed this,
        /// it is !#!):
        /// OTS_ID*START*VENDOR_ID*COL_IND*COL_VAL* ... *START*VENDOR_ID*COL_IND*COL_VAL*END
        /// </param>
        /// <returns>
        /// 0 if no changes occured, 
        /// otherwise the number of vendor part numbers that were affected.
        /// If the return value is negative, an error occurred.
        /// 
        /// NOTE: FOR COST/HEIGHT, a value that is zero or non-numeric is converted to NULL.
        /// THIS MEANS SPECIFYING A ZERO HEIGHT OR COST IS NOT ALLOWED!
        /// </returns>
        public string UpdateVPNData(string input)
        {
            Int64 pnID = -1;
            try

            {
                List<string> htmlResults = new List<string>(); //We will have one list entry per row
                List<VendorPartNumber> lstV = new List<VendorPartNumber>();
                Decimal tmpX;   //Used when tryparsing a decimal
                string[] dlim = { AAAK.DELIM };
                string[] kvp = input.Split(dlim, StringSplitOptions.None);

                //The first element in the arry is always the ots ID, 
                pnID = Convert.ToInt64(kvp[0]);
                //Now, start from index 1
                int i = 1;
                VendorPartNumber currVPN = null;
                while (kvp[i] != "END")
                {
                    if (kvp[i] == "START")
                    {
                        if (!clsUtil.IsNumeric(kvp[i + 1]))
                        {
                            //ignore it-- we are expecting a vendor part number id, but we will see something like
                            //'undefined' if the client tried to encode a row that does not meet the expected standard
                            //(such as a header row)
                        }
                        else
                        {
                            //the value at kvp[i+1] is the id of a NEW vendor part number.
                            //if currVPN is not null, then we need to process the results we've obtained so far.
                            if (currVPN != null)
                            {
                                lstV.Add(currVPN);
                            }
                            //Now, make a new VendorPartNumber object
                            currVPN = new VendorPartNumber(Convert.ToInt64(kvp[i + 1]));

                        }
                        //Advance the index
                        i = i + 2;
                    }
                    else
                    {
                        while (kvp[i] != "START" && kvp[i] != "END")
                        {
                            //We expect kvp[i] to be numeric.  Use Switch statement to decode the values
                            switch (Convert.ToInt16(kvp[i]))
                            {
                                case OTSINDK.CHANGE_VENDOR:
                                    currVPN.Vendor = kvp[i + 1];
                                    break;
                                case OTSINDK.CHANGE_VENDORPN:
                                    currVPN.VendorPN = kvp[i + 1];
                                    break;
                                case OTSINDK.CHANGE_VENDORPNSTATUS:
                                    currVPN.VendorPN_StatusCode = Convert.ToByte(kvp[i + 1]);
                                    break;
                                case OTSINDK.CHANGE_VENDORPN_DATASHEET:
                                    currVPN.DatasheetURL = kvp[i + 1];
                                    break;
                                case OTSINDK.CHANGE_VENDORPN_COST_LOW:
                                    Decimal.TryParse(kvp[i + 1], out tmpX);
                                    currVPN.Cost_Low = tmpX;
                                    break;
                                case OTSINDK.CHANGE_VENDORPN_COST_HIGH:
                                    Decimal.TryParse(kvp[i + 1], out tmpX);
                                    currVPN.Cost_High = tmpX;
                                    break;
                                case OTSINDK.CHANGE_VENDORPN_COST_ENG:
                                    Decimal.TryParse(kvp[i + 1], out tmpX);
                                    currVPN.Cost_Eng = tmpX;
                                    break;
                                case OTSINDK.CHANGE_VENDORPN_HEIGHT:
                                    Decimal.TryParse(kvp[i + 1], out tmpX);
                                    currVPN.MaxHeight = tmpX;
                                    break;
                                case OTSINDK.CHANGE_VENDORPN_ECODE:
                                    currVPN.EnvironCode = Convert.ToByte(kvp[i + 1]);
                                    break;
                                default:
                                    break;
                            }
                            i = i + 2;
                        } //end looping through array

                    } //terminate if statement to handle begin


                } //end while loop (reached "END" in array)
                //Add the final currVPN object.
                lstV.Add(currVPN);

                //Make a new Part Number Change object
                PartNumberChanges PNC = new PartNumberChanges(pnID);
                //We should now have a list of Vendor Part Number objects (stored in lstV) whose properties 
                //we can put in the database.


                for (int j = 0; j < lstV.Count; j++)
                {
                    clsDB myDB = new clsDB();
                    SqlCommand cmd = new SqlCommand();
                    List<SqlParameter> ps = new List<SqlParameter>();
                    //The following parameter is common to both add/edit operations
                    ps.Add(myDB.makeOutputParameter(DBK.SPVar.intReturnCode, System.Data.SqlDbType.Int));

                    if (lstV[j].VPNID > 0)
                    { //A positive VPNID indicates an entry already exists for this vendor part number; update it.
                        ps.Add(new SqlParameter("@" + DBK.ID, lstV[j].VPNID));
                        ps.Add(new SqlParameter("@" + DBK.strVENDORPARTNUMBER, lstV[j].VendorPN));
                        //For decimal values (cost, height), zero is meaningless; treat it as null.

                        if (lstV[j].Cost_Low == Convert.ToDecimal("0"))
                        {
                            ps.Add(new SqlParameter("@" + DBK.decLOWVOLCOST, DBNull.Value));
                        } else
                        {
                            SqlParameter lvc = new SqlParameter("@" + DBK.decLOWVOLCOST, Convert.ToDecimal(lstV[j].Cost_Low));
                            lvc.Precision = 18;
                            lvc.Scale = 6;
                            ps.Add(lvc);
                        }

                        if (lstV[j].Cost_High == Convert.ToDecimal("0"))
                        {
                            ps.Add(new SqlParameter("@" + DBK.decHIGHVOLCOST, DBNull.Value));
                        }
                        else
                        {
                            ps.Add(new SqlParameter("@" + DBK.decHIGHVOLCOST, lstV[j].Cost_High));
                        }

                        if (lstV[j].Cost_Eng == Convert.ToDecimal("0"))
                        {
                            ps.Add(new SqlParameter("@" + DBK.decENGCOST, DBNull.Value));
                        }
                        else
                        {
                            ps.Add(new SqlParameter("@" + DBK.decENGCOST, lstV[j].Cost_Eng));
                        }
                        ps.Add(new SqlParameter("@" + DBK.strDATASHEETURL, lstV[j].DatasheetURL));
                        ps.Add(new SqlParameter("@" + DBK.keyVENDORENVIRONCODE, lstV[j].EnvironCode));
                        if (lstV[j].MaxHeight == Convert.ToDecimal("0"))
                        {
                            ps.Add(new SqlParameter("@" + DBK.decMAXHEIGHT, DBNull.Value));
                        }
                        else
                        {
                            ps.Add(new SqlParameter("@" + DBK.decMAXHEIGHT, lstV[j].MaxHeight));
                        }
                        ps.Add(myDB.makeOutputParameter("@" + DBK.keyVENDOR, System.Data.SqlDbType.Int));
                        ps.Add(new SqlParameter("@" + DBK.keyVENDORPARTSTATUS, lstV[j].VendorPN_StatusCode));
                        clsLogin u = new clsLogin();
                        ps.Add(new SqlParameter("@" + DBK.keyUPDATEDBY,u.GetUserID()));
                        using (myDB.OpenConnection())
                        {
                            
                            object result = myDB.ExecuteSP(DBK.SP.spOTSUPDATEVENDORPNTABLE, ps, clsDB.SPExMode.NONQUERY, ref cmd);
                            //We know that if the output parameter is null, an error occurred.
                            //if (Convert.IsDBNull(cmd.Parameters[DBK.SPVar.intReturnCode].Value))
                            if (result.ToString().Contains(" "))
                            {
                                htmlResults.Add("<p>Unable to execute stored procedure to update values for Vendor Part Number " +
                                    Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) + "</p>" +
                                        "<p>Error Value: " + result.ToString() + "</p><br>");
                            } else
                            {
                                switch (Convert.ToInt32(cmd.Parameters[DBK.SPVar.intReturnCode].Value))
                                {
                                    case -1:
                                        //Function did not execute successfully.
                                        htmlResults.Add("<p>Unable to change Vendor Part Number " +
                                                Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) +
                                                " for this OTS Part Number.</p><br>");

                                        break;
                                    case 2:
                                        //No changes detected for this part number.
                                        htmlResults.Add("<p>No changes were detected in the information you submitted for Vendor Part Number " +
                                                Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) + "</p>" +
                                                "<p>Contact Alberto Campos if this is incorrect</p><br>");

                                        break;
                                    case 0:
                                    case 1:
                                        if (Convert.ToInt32(cmd.Parameters[DBK.SPVar.intReturnCode].Value) == 1)
                                        {
                                            htmlResults.Add("<p>Vendor Part Number " +
                                                    Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) +
                                                    " has been updated in the database, but a record of this change was not recorded in the database.</p>" +
                                                    "<p>An email was sent to all Users affected by this change.</p>" +
                                                    "<p>An email was sent to the programming team to investigate why the change was not logged.</p><br>");
                                        }
                                        else
                                        {
                                            htmlResults.Add("<p>Vendor Part Number " +
                                                    Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) +
                                                    " has been updated in the database.  An email was sent to all Users affected by this change.</p><br>");
                                        }


                                        PNC.ListOfChangedVPNIDs.Add(lstV[j].VPNID);
                                        break;
                                    default:
                                        htmlResults.Add("<p>Unrecognized code returned from database: " + Convert.ToString(cmd.Parameters[DBK.SPVar.intReturnCode].Value) + "</p>");
                                        break;
                                }
                            }

                        }

                    }
                    else
                    { //A negative VPNID indicates an entry does not exist for this vendor part number; make a new entry.
                        ps.Add(new SqlParameter("@" + DBK.ID, pnID));
                        ps.Add(new SqlParameter("@" + DBK.strVENDORPARTNUMBER, lstV[j].VendorPN));
                        ps.Add(new SqlParameter("@" + DBK.strVENDOR, lstV[j].Vendor));

                        //Add the part
                        using (myDB.OpenConnection())
                        {
                            myDB.ExecuteSP(DBK.SP.spOTSUPDATE_ADDVENDORPNTOOTSPARTNUMBER, ps, clsDB.SPExMode.NONQUERY, ref cmd);
                            switch (Convert.ToInt32(cmd.Parameters[DBK.SPVar.intReturnCode].Value))
                            {
                                case -1:
                                    //Function did not execute successfully.
                                    htmlResults.Add("<p>Unable to add Vendor Part Number " +
                                            Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) +
                                            " to this OTS Part Number.</p><br>");

                                    break;
                                case 1:
                                    //Part number alreay associated with this OTS Part Number.
                                    htmlResults.Add("<p>Vendor Part Number " +
                                            Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) + " is already associated with this OTS Part Number.</p>" +
                                            "<p>Contact Alberto Campos if this is incorrect</p><br>");
                                    break;
                                case 0:
                                    htmlResults.Add("<p>Vendor Part Number " +
                                            Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) +
                                            " was added to this OTS Part Number.</p>" +
                                            "<p>An email was sent to all Users affected by this change.</p>" +
                                            "<p>Perform another EditOperation if you want to change any of this Vendor Part Number's properties.</p><br>");

                                    PNC.NewPNInfo.Add("PN: " +
                                                      Convert.ToString(cmd.Parameters["@" + DBK.strVENDORPARTNUMBER].Value) +
                                                      "Vendor: " +
                                                      Convert.ToString(cmd.Parameters["@" + DBK.strVENDOR].Value));
                                    break;
                                default:
                                    htmlResults.Add("<p>Unrecognized code returned from database: " + Convert.ToString(cmd.Parameters[DBK.SPVar.intReturnCode].Value) + "</p>");
                                    break;
                            } //end switch statement
                        } //DB connection closed
                    } //end if statment: add vs edit
                    //Avoid any "this parameter belongs somewhere else" errors
                    ps.Clear();
                } //finished processing all table rows.

                //Final step: Update the characteristics of the OTS PN (such as cost, height, etc). based on
                //the new information for the Vendor Part Numbers in the database
                string updateOTSTableResult = "";
                if (PNC.UpdateOTSTable(ref updateOTSTableResult))
                {
                    htmlResults.Insert(0, updateOTSTableResult);
                } else
                {
                    htmlResults.Insert(0, updateOTSTableResult);
                }
                return getHTMLForPartNumberID(Convert.ToString(pnID)) + AAAK.DELIM + 
                    String.Join("<br>", htmlResults.ToArray());
            }
            catch (Exception ex)
            {
                return getHTMLForPartNumberID(Convert.ToString(pnID)) + AAAK.DELIM + 
                    "<p>Method UpdatePNData error: " + ex.Message + "</p>";
            }
        }

        /// <summary>
        /// This class is used in Method UpdateVPNData to help with transferring data to the database.
        /// </summary>
        private class VendorPartNumber
        {
            public VendorPartNumber(Int64 vpnID)
            {
                m_vpnID = vpnID;
            }

            private Int64 m_vpnID;
            public Int64 VPNID
            {
                get
                {
                    return m_vpnID;
                }
                set
                {
                    m_vpnID = value;
                }

            }
            private string m_vendor;
            public string Vendor
            {
                get
                {
                    return m_vendor;
                }
                set
                {
                    m_vendor = value;
                }

            }
            private string m_vendorpn;
            public string VendorPN
            {
                get
                {
                    return m_vendorpn;
                }
                set
                {
                    m_vendorpn = value;
                }

            }
            private Byte m_vendorpnstatus;
            public Byte VendorPN_StatusCode
            {
                get
                {
                    return m_vendorpnstatus;
                }
                set
                {
                    m_vendorpnstatus = value;
                }

            }
            private string m_datasheeturl;
            public string DatasheetURL
            {
                get
                {
                    return m_datasheeturl;
                }
                set
                {
                    m_datasheeturl = value;
                }

            }
            private decimal m_lowcost;
            public decimal Cost_Low
            {
                get
                {
                    return m_lowcost;
                }
                set
                {
                    m_lowcost = value;
                }

            }
            private decimal m_highcost;
            public decimal Cost_High
            {
                get
                {
                    return m_highcost;
                }
                set
                {
                    m_highcost = value;
                }

            }
            private decimal m_engcost;
            public decimal Cost_Eng
            {
                get
                {
                    return m_engcost;
                }
                set
                {
                    m_engcost = value;
                }

            }
            private decimal m_maxheight;
            public decimal MaxHeight
            {
                get
                {
                    return m_maxheight;
                }
                set
                {
                    m_maxheight = value;
                }

            }
            private Byte m_ecode;
            public Byte EnvironCode
            {
                get
                {
                    return m_ecode;
                }
                set
                {
                    m_ecode = value;
                }

            }
        }

        /// <summary>
        /// This class holds a history of all changes that occurred to the underlying VENDOR Part Numbers for a given
        /// OTS Part Number.
        /// </summary>
        public class PartNumberChanges
        {
            public PartNumberChanges(Int64 otsID)
            {
                m_otsID = otsID;
            }
            private Int64 m_otsID;
            /// <summary>
            /// The original OTS ID for which the changes occurred.
            /// </summary>
            public Int64 OTSID
            {
                get
                {
                    return m_otsID;
                }
                set
                {
                    m_otsID = value;
                }
            }

            private List<string> m_NewPNInfo = new List<string>();
            /// <summary>
            /// A list of all new Part Number entries made for this OTSPN;
            /// Format of entry should be:
            /// Vendor: Vendor PN
            /// </summary>
            public List<string> NewPNInfo 
            {
                get
                {
                    return m_NewPNInfo;
                }
                set
                {
                    m_NewPNInfo = value;
                }
            }

            /// <summary>
            /// A list of Venor PN IDs (from table otsVendorPN) that have changed.
            /// We will do a 'Where Used' against these IDs to determine if changes to the underlying Vendor PN
            /// have affected the ots Part Number.
            /// </summary>
            private List<Int64> m_lstVPNIDs = new List<Int64>();
            public List<Int64>ListOfChangedVPNIDs
            {
                get
                {
                    return m_lstVPNIDs;
                }
                set
                {
                    m_lstVPNIDs = value;
                }
            }

            /// <summary>
            /// Processes the values in contained in m_NewPNInfo and m_lstVPNIDs to
            /// make the appropriate changes in both otsParts and otsPNHistory
            /// by calling the appropriate stored procedures.
            /// </summary>
            /// <param name="result">Holds an html string indicating what happened in the function.</param>
            /// <returns></returns>
            public Boolean UpdateOTSTable(ref string result)
            {
                /*General approach:
                 * 1) We will cycle through each vendor part number ID stored in m_lstVPNIDs.
                 * 2) For each VPNID, we will do a where used to get a list of all otsPN (which are Submitted or Active--
                 * this is controlled at the Stored Proc level) that have this VendorPN on its AVL
                 * 3) We cycle through each OTS PN and call procedure UpdateParameters, which updates things like
                 * Cost, Height, Environmental Code, etc. based on the properties of the Vendor Part Numbers on its AVL.
                 * 3a) If the OTS Part Number we are processing matches m_otsID, then we also pass the contents of 
                 * m_newPNInfo to the stored proc, so it can record the fact that a new part number has been assocaited with
                 * the OTS Part in the History table.
                 * 4) After finishing processing the OTS Part Number, that OTS Part Number is added to list processedOTSPN.
                 * This guards against us wasting time by calling the stored procedure in a) for cases where the same
                 * OTSPN has multiple Vendor Part Numbers, and multiple changes were made to those Vendor Part Numbers.
                 * 5) Once all Vendor Part Numbers--> OTS Part Numbers have been processed, we do a where used
                 * to get all Users of all the Products that use all the Changed OTS Part Numbers; an email is sent to
                 * those user notifying them of the change.
                 * We return true if the function completes successfully.
                 * We return false if any error occurs. 
                 */
                List<Int64> lstProcessedOTSPN = new List<Int64> ();
                List<Int64> lstChangedOTSIDs = new List<Int64>();
                try
                {
                    //Step 1)
                    for (int i = 0;i<m_lstVPNIDs.Count;i++)
                    {
                        clsDB myDB = new clsDB();
                        SqlCommand cmd = new SqlCommand();
                        List<Int64> lstWhereUsedOTSIDs = new List<Int64>();
                        List<SqlParameter> ps = new List<SqlParameter>();
                        ps.Add(new SqlParameter("@" + DBK.keyVENDORPN, m_lstVPNIDs[i]));
                        //Step 2-- do a where used for this vendor part number id
                        using (myDB.OpenConnection())
                        {
                            using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(DBK.SP.spOTSWHEREUSEDFORVENDORPARTNUMBER,
                                                                                    ps,
                                                                                    clsDB.SPExMode.READER,
                                                                                    ref cmd)
                                   )
                            {
                                if (dR != null && dR.HasRows)
                                {
                                    while (dR.Read())
                                    {
                                        lstWhereUsedOTSIDs.Add(Convert.ToInt64(dR[DBK.ID]));
                                    }
                                }
                            }
                            //DON'T FORGET TO CLEAR PARAMETERS!
                            cmd.Parameters.Clear();
                            ps.Clear();
                        }
                        
                        //Step 3-- call UpdateOTSPart for each ID, if needed.
                        for (i = 0;i<lstWhereUsedOTSIDs.Count;i++)
                        {
                            if (!lstProcessedOTSPN.Contains(lstWhereUsedOTSIDs[i]))
                            {
                                clsLogin u = new clsLogin();
                                ps.Add(new SqlParameter("@" + DBK.ID, lstWhereUsedOTSIDs[i]));
                                ps.Add(new SqlParameter("@" + DBK.keyUPDATEDBY, u.GetUserID()));
                                ps.Add(myDB.makeOutputParameter("@changed", System.Data.SqlDbType.Bit));
                                if (lstWhereUsedOTSIDs[i] == m_otsID)
                                {
                                    //This is step 3a)
                                    ps.Add(new SqlParameter("@newVendorPNs", String.Join(AAAK.vbCRLF, m_NewPNInfo.ToArray())));
                                } else
                                {
                                    ps.Add(new SqlParameter("@newVendorPNs", DBNull.Value));
                                }

                                using (myDB.OpenConnection())
                                {
                                    object r = myDB.ExecuteSP(DBK.SP.spOTSUPDATEPARTSBASEDINAVL, ps, clsDB.SPExMode.NONQUERY, ref cmd);
                                    if (r.ToString().Contains(" ")) {
                                        result = "<p>Error when executing stored procedure: " + r.ToString() + "</p>";
                                    } else if (Convert.ToByte(cmd.Parameters["@changed"].Value)==1)
                                    {
                                        lstChangedOTSIDs.Add(lstWhereUsedOTSIDs[i]);
                                    }
                                    cmd.Parameters.Clear();
                                }
                                //step 4)
                                lstProcessedOTSPN.Add(lstWhereUsedOTSIDs[i]);

                            } //end processing this ots id
                        } //end processing the ots ids associated with this vendor part number
                    } //end processing all vendor part number

                    //Step 5
                    //For each BRCMPN in lstChangedOTSIDs, do a where used assembly to get the email address of all users
                    //affected by the change; also send them details of what changed.
                    result = "<p>SUCCESS!!</p>" + 
                             "<p>Add code to email users affected by this change.</p>" +
                             "<p>Add code to backup changes in spreadsheet?</p>";
                    return true;
                } catch (Exception ex)
                {
                    result = "<p>Error in UpdateOTSTable: " + ex.Message + "</p>";
                    return false;
                }
            }





        }
    }
}