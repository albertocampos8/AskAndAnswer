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
        public const int SEARCHTEXTBOX = 5;
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

        public const string asyBOM = AAAK.asyBOM;
        public const string keyTOPLEVELNAME = AAAK.keyTOPLEVELNAME;
        public const string keyASSYPN = AAAK.keyASSYPN;
        public const string keyASSYREV = AAAK.keyASSYREV;
        public const string keyUPLOADEDBY = AAAK.keyUPLOADEDBY;
        public const string keyASSYSTATUS = AAAK.keyASSYSTATUS;
        public const string keyASSYBU = AAAK.keyASSYBU;
        public const string intBOMREV = AAAK.intBOMREV;
        public const string dtUPLOADED = AAAK.dtUPLOADED;
        public const string keyREASONFORREV = AAAK.keyREASONFORREV;

        public const string asyPNS = AAAK.asyPNS;
        public const string strASSYPARTNUMBER = AAAK.strASSYPARTNUMBER;

        public const string asyREVs = AAAK.asyREVs;
        public const string strREVISION = AAAK.strREVISION;
        public const string intMAJOR = AAAK.intMAJOR;
        public const string intMINOR = AAAK.intMINOR;

        public const string asySTATUS = AAAK.asySTATUS;
        public const string strASSYSTATUS = AAAK.strASSYSTATUS;

        public const string asyCHANGEREASONS = AAAK.asyCHANGEREASONS;
        public const string strREASON = AAAK.strREASON;

        public const string asyBOMPARTS = AAAK.asyBOMPARTS;
        public const string keyASSY = AAAK.keyASSY;
        public const string keyPN = AAAK.keyPN;
        public const string strREFDES = AAAK.strREFDES;
        public const string strBOMNOTES = AAAK.strBOMNOTES;
        public const string intQTY = AAAK.intQTY;

        //END OTS DATABASE ENTRIES

        //LOCATION DATABASE TABLES/COLUMNS
        public const string locCOUNTRY = AAAK.locCOUNTRY;
        public const string strCOUNTRY = AAAK.strCOUNTRY;

        public const string locPOSTALCODE = AAAK.locPOSTALCODE;
        public const string strPOSTALCODE = AAAK.strPOSTALCODE;

        public const string locSTATEPROVINCE = AAAK.locSTATEPROVINCE;
        public const string strSTATEPROVINCE = AAAK.strSTATEPROVINCE;

        public const string locCITY = AAAK.locCITY;
        public const string strCITY = AAAK.strCITY;

        public const string locADDRESS = AAAK.locADDRESS;
        public const string strADDRESS = AAAK.strADDRESS;

        public const string locDETAIL = AAAK.locDETAIL;
        public const string strDETAIL = AAAK.strDETAIL;

        public const string locLOCATION = AAAK.locLOCATION;
        public const string strFLOOR = AAAK.strFLOOR;
        public const string dtDEFINED = AAAK.dtDEFINED;
        public const string keyCOUNTRY = AAAK.keyCOUNTRY;
        public const string keySTATEPROVINCE = AAAK.keySTATEPROVINCE;
        public const string keyCITY = AAAK.keyCITY;
        public const string keyADDRESS = AAAK.keyADDRESS;
        public const string keyDETAIL = AAAK.keyDETAIL;
        public const string keyDEFINEDBY = AAAK.keyDEFINEDBY;
        public const string keyPOSTALCODE = AAAK.keyPOSTALCODE;

        //END LOCATION DATABASE ENTRIES

        //Inventory DATABASE ENTRIES
        public const string invBULK = AAAK.invBULK;
        public const string keyLOCATIONBULK = AAAK.keyLOCATIONBULK;
        public const string keyBULKITEM = AAAK.keyBULKITEM;
        public const string keyOWNER = AAAK.keyOWNER;

        public const string invHISTORY = AAAK.invHISTORY;
        public const string keyCHANGEDBY = AAAK.keyCHANGEDBY;
        public const string dtTRANSACTION = AAAK.dtTRANSACTION;
        public const string intDELTA = AAAK.intDELTA;
        public const string strCOMMENT = AAAK.strCOMMENT;
        public const string keyTRANSACTIONTYPE = AAAK.keyTRANSACTIONTYPE;

        //End Inventory DATABASE ENTRIES

        //Stored Procedures
        public partial class SP {
            public const string spOTSGETBASEPARTNUMBER = AAAK.spOTSGETBASEPARTNUMBER;
            public const string spGETKVPBUINFO = AAAK.spGETKVPBUINFO;
            public const string spOTSINSERTNEWPARTNUMBER = AAAK.spOTSINSERTNEWPARTNUMBER;
            public const string spOTSINSERTNEWPARTNUMBER2 = AAAK.spOTSINSERTNEWPARTNUMBER2;
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
            public const string spGETKVPFULLADDRESS = AAAK.spGETKVPFULLADDRESS;
            public const string spGETKVPUSERINFO = AAAK.spGETKVPUSERINFO;
            public const string spOTSFINDBYBU = AAAK.spOTSFINDBYBU;
            public const string spOTSFINDBYDATE = AAAK.spOTSFINDBYDATE;
            public const string spOTSFINDBYDESCRIPTION = AAAK.spOTSFINDBYDESCRIPTION;
            public const string spOTSFINDBYPARTNUMBER = AAAK.spOTSFINDBYPARTNUMBER;
            public const string spOTSFINDBYPRODUCT = AAAK.spOTSFINDBYPRODUCT;
            public const string spOTSFINDBYREQUESTOR = AAAK.spOTSFINDBYREQUESTOR;
            public const string spOTSFINDBYVENDOR = AAAK.spOTSFINDBYVENDOR;
            public const string spOTSFINDBYVENDORPARTNUMBER = AAAK.spOTSFINDBYVENDORPARTNUMBER;

            public const string spOTSFINDBYBUWITHINV = AAAK.spOTSFINDBYBUWITHINV;
            public const string spOTSFINDBYDATEWITHINV = AAAK.spOTSFINDBYDATEWITHINV;
            public const string spOTSFINDBYDESCRIPTIONWITHINV = AAAK.spOTSFINDBYDESCRIPTIONWITHINV;
            public const string spOTSFINDBYPARTNUMBERWITHINV = AAAK.spOTSFINDBYPARTNUMBERWITHINV;
            public const string spOTSFINDBYPRODUCTWITHINV = AAAK.spOTSFINDBYPRODUCTWITHINV;
            public const string spOTSFINDBYREQUESTORWITHINV = AAAK.spOTSFINDBYREQUESTORWITHINV;
            public const string spOTSFINDBYVENDORWITHINV = AAAK.spOTSFINDBYVENDORWITHINV;
            public const string spOTSFINDBYVENDORPARTNUMBERWITHINV = AAAK.spOTSFINDBYVENDORPARTNUMBERWITHINV;

            public const string spOTSGETPNINFO = AAAK.spOTSGETPNINFO;
            public const string spOTSGETPNINVENTORYINFO = AAAK.spOTSGETPNINVENTORYINFO;
            public const string spOTSUPDATEPARTSTABLE = AAAK.spOTSUPDATEPARTSTABLE;
            public const string spOTSRECONCILEPARTSTABLE = AAAK.spOTSRECONCILEPARTSTABLE;
            public const string spOTSUPDATE_ADDVENDORPNTOOTSPARTNUMBER = AAAK.spOTSUPDATE_ADDVENDORPNTOOTSPARTNUMBER;
            public const string spOTSUPDATEVENDORPNTABLE = AAAK.spOTSUPDATEVENDORPNTABLE;
            public const string spOTSRECONCILEVENDORPNTABLE = AAAK.spOTSRECONCILEVENDORPNTABLE;
            public const string spOTSWHEREUSEDFORVENDORPARTNUMBER = AAAK.spOTSWHEREUSEDFORVENDORPARTNUMBER;
            public const string spOTSWHEREUSEDFORVENDORPARTNUMBERSTRING = AAAK.spOTSWHEREUSEDFORVENDORPARTNUMBERSTRING;
            public const string spOTSWHEREUSEDFORVENDORPARTNUMBERSTRINGANDVENDOR = AAAK.spOTSWHEREUSEDFORVENDORPARTNUMBERSTRINGANDVENDOR;
            public const string spOTSFINDUNDEFINEDPARTTYPES = AAAK.spOTSFINDUNDEFINEDPARTTYPES;
            public const string spOTSUPDATEPARTSBASEDINAVL = AAAK.spOTSUPDATEPARTSBASEDINAVL;
            public const string spOTSGETPNIDS = AAAK.spOTSGETPNIDS;
            public const string spUPSERTASSYBOMENTRY = AAAK.spUPSERTASSYBOMENTRY;
            public const string spDELETEASSYBOMPARTS = AAAK.spDELETEASSYBOMPARTS;
            public const string spGETASSYSTATUS = AAAK.spGETASSYSTATUS;
            public const string spOTSWHEREUSED = AAAK.spOTSWHEREUSED;
            public const string spAC_ASYNAMES = AAAK.spAC_ASYNAMES;
            public const string spAC_ASYREVSFORGIVENASYNAME = AAAK.spAC_ASYREVSFORGIVENASYNAME;
            public const string spAC_ASYBOMREVSFORGIVENASYNAMEANDREV = AAAK.spAC_ASYBOMREVSFORGIVENASYNAMEANDREV;
            public const string spAC_OTSVENDORPN = AAAK.spAC_OTSVENDORPN;
            public const string spAC_OTSVENDOR = AAAK.spAC_OTSVENDOR;
            public const string spAC_OTSPRODUCT = AAAK.spAC_OTSPRODUCT;
            public const string spAC_OTSPARTS = AAAK.spAC_OTSPARTS;
            public const string spDOWNLOADBOM = AAAK.spDOWNLOADBOM;
            public const string spRELEASEBOM = AAAK.spRELEASEBOM;
            public const string spDELETEASYBOMENTRY = AAAK.spDELETEASYBOMENTRY;
            public const string spGETASSYHISTORY = AAAK.spGETASSYHISTORY;
            public const string spEDITRELEASENOTE = AAAK.spEDITRELEASENOTE;
            public const string spEDITRELEASENOTE_RESET = AAAK.spEDITRELEASENOTE_RESET;
            public const string spLOCADDLOCATION = AAAK.spLOCADDLOCATION;
            public const string spLOCLOOKUPADDRESSBYSTREET = AAAK.spLOCLOOKUPADDRESSBYSTREET;
            public const string spAC_LOCADDRESS = AAAK.spAC_LOCADDRESS;
            public const string spAC_LOCCITY = AAAK.spAC_LOCCITY;
            public const string spAC_LOCSTATEPROVINCE = AAAK.spAC_LOCSTATEPROVINCE;
            public const string spAC_LOCPOSTALCODE = AAAK.spAC_LOCPOSTALCODE;
            public const string spAC_LOCCOUNTRY = AAAK.spAC_LOCCOUNTRY;

            public const string spINVGETINFOFORINVBULKID = AAAK.spINVGETINFOFORINVBULKID;
            public const string spINVREMOVEBULKINVENTRY = AAAK.spINVREMOVEBULKINVENTRY;
            public const string spINVUPSERTINVBULKENTRY = AAAK.spINVUPSERTINVBULKENTRY;
            public const string spINVGETPARTHISTORY = AAAK.spINVGETPARTHISTORY;
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
            public const string PN = AAAK.SP_COLALIAS.PN;
            public const string USERNAME = AAAK.SP_COLALIAS.USERNAME;
            public const string ONHAND = AAAK.SP_COLALIAS.ONHAND;
            public const string INVBULKID = AAAK.SP_COLALIAS.INVBULKID;
            public const string CHANGEDBY = AAAK.SP_COLALIAS.CHANGEDBY;
            public const string FULLADDRESS = AAAK.SP_COLALIAS.FULLADDRESS;
            public const string CONTACT = AAAK.SP_COLALIAS.CONTACT;
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
            public const int BOM_VIEW_UPLOAD = 10004;
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
        string[] m_dlim = { AAAK.DELIM };
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
        /// <param name="blRunAtServer">Set TRUE to include runat="server" as a property of the control</param>
        public void ConstructInputControls(SqlDataReader dR, System.Web.UI.Control cntlContainer,
            Dictionary<string, string> dctDefaultOverride, string uid, int displayType,
            Boolean blElementsInLine = false, Boolean blRunAtServer = false)
        {
            /* Each time we go through a dR read loop, we will add the html control strings it spawns to a string.
             * That string will be enclosed in a div element; this allows us to toggle visibility easily later.
             * THAT string will be added to the string builder.
             * When done, the stringbuilder contents will be used to generate a Literal Control, which will be added to cntlContainer.
            */
            try
            {
                clsUtil helper = new clsUtil();
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
                                                                            elDtype,
                                                                            blRunAtServer)
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
                                                                                       rdonly, 
                                                                                       blRunAtServer)
                                                              );
                                    break;
                                case CntlDecoder.SEARCHTEXTBOX:
                                    dRControlSet.Append(DynControls.html_searchbox_string("txt_" + index + uid,
                                                                                       txtCSSClass,
                                                                                        valToDisplay,
                                                                                       true,
                                                                                       elDtype,
                                                                                       (string)dR[DBK.lblHELPMESSAGE],
                                                                                       rdonly,
                                                                                       blRunAtServer)
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
                                                                                                rdonly,
                                                                                                blRunAtServer)
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

                                    lstKVPs = helper.PutKVPInDictionary(appKey);

                                    dRControlSet.Append(DynControls.html_combobox_string("cbo_" + index + uid,
                                                                                         lstKVPs,
                                                                                         cboCSSClass,
                                                                                         dR[DBK.blSELECTIONREQUIRED].Equals(true),
                                                                                         valToDisplay,
                                                                                         true,
                                                                                         elDtype,
                                                                                         (string)dR[DBK.lblHELPMESSAGE],
                                                                                         rdonly,
                                                                                         blRunAtServer)
                                                              );
                                    break;
                                case CntlDecoder.TXT_W_BROWSE_ATTACHMENTS:
                                    //The input element that will hold the file
                                    dRControlSet.Append(DynControls.html_input_string("input_" + index + uid, "file", "fileinput", AAAK.DISPLAYTYPES.INLINE, (string)dR[DBK.lblHELPMESSAGE],
                                        (Boolean)dR[DBK.blINITENABLED], blRunAtServer));
                                    dRControlSet.Append(DynControls.html_linebreak_string());
                                    //And the text box
                                    dRControlSet.Append(DynControls.html_txtbox_string("txt_" + index + uid,
                                                                                       txtCSSClass,
                                                                                        valToDisplay,
                                                                                       (Boolean)dR[DBK.blINITVISIBLE],
                                                                                       AAAK.DISPLAYTYPES.INLINE,
                                                                                       (string)dR[DBK.lblHELPMESSAGE],
                                                                                       rdonly,
                                                                                       blRunAtServer));

                                    //The browse button...
                                    // dRControlSet.Append(DynControls.html_button_string("btn_" + index, "BROWSE",
                                    //   "", true, AAAK.DISPLAYTYPES.INLINE, "Click to Browse for file..."));
                                    //Since the last element was inline, add a break
                                    dRControlSet.Append(DynControls.html_linebreak_string());
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
                    string qClass = DynControls.encodeProperty("class", "fromdb");
                    if (!(Boolean)dR[DBK.blINITVISIBLE])
                    {
                        qVisible = DynControls.encodeProperty("style", "display:none");
                    }
                    masterControlSet.Append("<div " +
                        qID +
                        qVisible +
                        qClass + 
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
        /// Returns the minor portion of the revision,
        /// which should be the last two characters, for demo purposes.
        /// Returns 0 if fails.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int getMinorRev(string r)
        {
            try
            {
                return int.Parse(r.Substring(r.Length - 2, 2));
            } catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns the major portion of the revision,
        /// which should be the third from last character, for demo purposes.
        /// Returns 0 if fails.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int getMajorRev(string r)
        {
            try
            {
                return int.Parse(r.Substring(r.Length - 3, 1));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Code specific to application
        /// </summary>
        /// <returns></returns>
        public Int64 getUserDBID()
        {
            return 1;
        }

        /// <summary>
        /// Code specific to application
        /// </summary>
        /// <returns></returns>
        public int getUserdBUID()
        {
            return 1;
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
                string[] kvp = input.Split(m_dlim, StringSplitOptions.None);
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
                string[] kvp = input.Split(m_dlim, StringSplitOptions.None);
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
            string[] kvp = input.Split(m_dlim, StringSplitOptions.None);
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
                    sp = DBK.SP.spOTSFINDBYPRODUCTWITHINV;
                    ps.Add(new SqlParameter(DBK.SPVar.searchString, "%" + searchText + "%"));
                    fldName = "Requested for Product";
                    break;
                case OTSINDK.SEARCHMETHOD_BYBU:
                    sp = DBK.SP.spOTSFINDBYBUWITHINV;
                    ps.Add(new SqlParameter(DBK.SPVar.searchID, Convert.ToByte(searchText)));
                    fldName = "BU";
                    break;
                case OTSINDK.SEARCHMETHOD_BYREQUESTOR:
                    sp = DBK.SP.spOTSFINDBYREQUESTORWITHINV;
                    ps.Add(new SqlParameter(DBK.SPVar.searchID, Convert.ToByte(searchText)));
                    //TEST ONLY
                    HttpContext.Current.Session[SESSIONKEYS.UID] = Convert.ToInt64(searchText);
                    fldName = "Requestor";
                    break;
                case OTSINDK.SEARCHMETHOD_BYVENDOR:
                    sp = DBK.SP.spOTSFINDBYVENDORWITHINV;
                    ps.Add(new SqlParameter(DBK.SPVar.searchString, "%" + searchText + "%"));
                    fldName = "Vendor";
                    break;
                case OTSINDK.SEARCHMETHOD_BYVENDORPN:
                    sp = DBK.SP.spOTSFINDBYVENDORPARTNUMBERWITHINV;
                    ps.Add(new SqlParameter(DBK.SPVar.searchString, "%" + searchText + "%"));
                    fldName = "Vendor Part Number";
                    break;
                case OTSINDK.SEARCHMETHOD_BYPN:
                    sp = DBK.SP.spOTSFINDBYPARTNUMBERWITHINV;
                    ps.Add(new SqlParameter(DBK.SPVar.searchString, "%" + searchText + "%"));
                    fldName = "Part Number";
                    break;
                case OTSINDK.SEARCHMETHOD_BYDATE:
                    sp = DBK.SP.spOTSFINDBYDATEWITHINV;
                    ps.Add(new SqlParameter(DBK.SPVar.searchDate, DateTime.Parse(searchText)));
                    fldName = "Date Requested";
                    break;
                case OTSINDK.SEARCHMETHOD_BYDESCRIPTION:
                    sp = DBK.SP.spOTSFINDBYDESCRIPTIONWITHINV;
                    ps.Add(new SqlParameter(DBK.SPVar.searchString, "%" + searchText + "%"));
                    fldName = "Description";
                    break;
                default:
                    return "<p>Did not know how to perform search for Method with ID " + Convert.ToString(searchMethod + ".</p>") +
                        m_dlim[0] + "<p>NO DATA</p>";
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
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","INV",1,true),
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
                                    tblRows.Add(new HTMLStrings.TableRow(
                                                "otsFindResultsHeader",
                                                "clsHeaderRow",
                                                new HTMLStrings.TableCell[] {
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","STATUS",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","PART NUMBER",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","DESCRIPTION",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","INV",1,true),
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
                            string onhand = "";
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
                                onhand = Convert.ToString(dR[DBK.SP_COLALIAS.ONHAND]);
                                //Button to expand the row...
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
                                        htmlSearchText = bu;
                                        break;
                                    case OTSINDK.SEARCHMETHOD_BYREQUESTOR:
                                        reqby = DynControls.EmphasizeText(reqby, reqby, "red", true);
                                        htmlSearchText = reqby;
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
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell",onhand,1,false),
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
                                                                    new HTMLStrings.TableCell("","otsFindResultsCell onhand " + pnID,onhand,1,false),
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
                                m_dlim[0] + "<p>NO DATA</p>";
                        }
                    }
                    else
                    {
                        //null data reader
                        return "<p>Method FindMatchingPN returned a null dataset.  This is a coding error.</p>" +
                            m_dlim[0] + "<p>NO DATA</p>";
                    }
                }
            }

            string strPlural = "";
            if (nResults > 1)
            {
                strPlural = "s";
            }
                
            string resultMessage = "<p>Your search for '" + htmlSearchText + "' in the '" + fldName + "' field returned " +
                DynControls.EmphasizeText(Convert.ToString(nResults), Convert.ToString(nResults), "red", true) + " record" +
                strPlural + ".";
            HTMLStrings.Table resultTable = new HTMLStrings.Table("otsSearchResults", "otsSearchResults", tblRows);
            return resultMessage + m_dlim[0] + resultTable.ToHTML();
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
            string htmlForInvTab = "";
            string htmlForTransactionTab = "";
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

            //Obtaining 'where used' info for the id is handled by a different function, which we'll call now
            htmlForWhereUsedTab = WhereUsedForPN(pnID.ToString());
            //Obtaining inventory info for the id is also handled by a different fucntion, which we'll also call now
            htmlForInvTab = InvForPN(pnID.ToString());
            htmlForTransactionTab = MakePartNumberInventoryHistoryTable(pnID);

            //Title of this section
            string bookmarkurl = "Bookmark this URL to get back to this page: " + DynControls.html_hyperlink_string("", 
                HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0] + ".aspx?ID=" + pnID,
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
            divPNVendorInfo.Controls.Add(new LiteralControl(htmlForVendorPNTab));
            //divPNVendorInfo.Style.Add(HtmlTextWriterStyle.Display, "table");
            divPNVendorInfo.Style.Add(HtmlTextWriterStyle.OverflowX, "auto");

            Panel divPNWhereUsedInfo = new Panel();
            divPNWhereUsedInfo.ID = "divPNWhereUsedInfo_" + pnID;
            divPNWhereUsedInfo.Controls.Add(new LiteralControl(htmlForWhereUsedTab));
            divPNWhereUsedInfo.Style.Add(HtmlTextWriterStyle.OverflowX, "auto");

            Panel divPNInvInfo = new Panel();
            divPNInvInfo.ID = "divPNInvInfo_" + pnID;
            divPNInvInfo.Controls.Add(new LiteralControl(htmlForInvTab));
            divPNInvInfo.Style.Add(HtmlTextWriterStyle.OverflowX, "auto");

            Panel divPNTransactions = new Panel();
            divPNTransactions.ID = "divPNTransactions_" + pnID;
            divPNTransactions.Controls.Add(new LiteralControl("<p>Please wait... querying database...</p>"));
            //divPNTransactions.Controls.Add(new LiteralControl(htmlForTransactionTab));
            divPNTransactions.Style.Add(HtmlTextWriterStyle.OverflowX, "auto");

            //Set up the unordered list for the tabs.
            StringBuilder sB = new StringBuilder();
            sB.Append("<ul>");
            sB.Append("<li>" + DynControls.html_hyperlink_string("Part Properties", "#" + divPNInfo.ID));
            sB.Append("<li>" + DynControls.html_hyperlink_string("AVL (Approved Vendor List)", "#" + divPNVendorInfo.ID));
            sB.Append("<li>" + DynControls.html_hyperlink_string("Where Used", "#" + divPNWhereUsedInfo.ID));
            sB.Append("<li>" + DynControls.html_hyperlink_string("Inventory", "#" + divPNInvInfo.ID));
            sB.Append("<li>" + DynControls.html_hyperlink_string("Transaction History", "#" + divPNTransactions.ID));
            sB.Append("</ul>");
            return sectionTitle +
                    "<div " + DynControls.encodeProperty("id", "pnsummary_" + pnID) +
                    DynControls.encodeProperty("class", "pnsummary") +
                    //DynControls.encodeProperty("style","overflow-x:auto") + ">" +
                    ">" +
                    sB.ToString() +
                    DynControls.GetHTMLText(divPNInfo) +
                    DynControls.GetHTMLText(divPNVendorInfo) +
                    DynControls.GetHTMLText(divPNWhereUsedInfo) +
                    DynControls.GetHTMLText(divPNInvInfo) +
                    DynControls.GetHTMLText(divPNTransactions) +
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
                clsUtil helper = new clsUtil();
                List<HTMLStrings.TableRow> tblRows = new List<HTMLStrings.TableRow>();
                List<string> lstVPNStatus = helper.PutKVPInDictionary("kvpl_" + DBK.SP.spGETKVPPARTSTATUS);
                List<string> lstVStatus = helper.PutKVPInDictionary("kvpl_" + DBK.SP.spGETKVPVENDORSTATUS);
                List<string> lstECode = helper.PutKVPInDictionary("kvpl_" + DBK.SP.spGETKVPENVIRONCODE);
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
                string[] kvp = input.Split(m_dlim, StringSplitOptions.None);
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
                string[] kvp = input.Split(m_dlim, StringSplitOptions.None);

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

        /// <summary>
        /// Checks the database if the given part number ID is used in any Assy BOMs.
        /// If yes, returns a table showing the where used information.
        /// If no, returns an html string informing the user the part is not used in any BOMs uploaded to the database
        /// </summary>
        /// <param name="input">Expected format:
        /// PNID</param>
        /// <returns></returns>
        public string WhereUsedForPN(string input)
        {
            try
            {
                Int64 pnID = Int64.Parse(input);
                clsDB myDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@" + DBK.keyPN, pnID));

                string sp = DBK.SP.spOTSWHEREUSED;
                
                using (myDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(sp, ps, clsDB.SPExMode.READER, ref cmd))
                    {
                        if (dR != null)
                        {
                            if (dR.HasRows)
                            {
                                return MakePartNumberWhereUsedTable(dR, pnID, myDB);
                            }
                            else
                            {
                                return "<p>Error: Unable to find Part Number with database ID " + pnID.ToString() + ".</p>";
                            }
                        }
                        else
                        {
                            return "<p>Error in WhereUsedForPN: NULL dataset.  " +
                                "This indicates a problem occurred when executing stored procedure " +
                                DBK.SP.spOTSWHEREUSED + ".</p>";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return "<p>Error in WhereUsedForPN: " + ex.Message + "</p>";
            }
        }

        /// <summary>
        /// Checks the database if the given vendor part number (and, optionally, the vendor) is used for any OTS part numbers.
        /// If yes, returns a table showing the where used information.
        /// If no, returns an empty string
        /// </summary>
        /// <param name="input">Expected format:
        /// blGiveWarning[DELIM]Vendor Part Number[DELIM]Vendor(Optional)</param>
        /// <returns></returns>
        public string WhereUsedForVPN(string input)
        {
            try
            {
                List<string> lst = input.Split(m_dlim, StringSplitOptions.None).ToList();
                Boolean includeWarning = (lst[0]=="1");
                string vpn = lst[1].ToUpper();
                string v = "";
                if (lst.Count == 3)
                {
                    v = lst[2].ToUpper();
                }

                clsDB myDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@" + DBK.strVENDORPARTNUMBER, vpn));

                string sp = DBK.SP.spOTSWHEREUSEDFORVENDORPARTNUMBERSTRING;
                if (v!="")
                {
                    sp = DBK.SP.spOTSWHEREUSEDFORVENDORPARTNUMBERSTRINGANDVENDOR;
                    ps.Add(new SqlParameter("@" + DBK.strVENDOR, v));
                }
                string htmlTable = "";
                Boolean PartIsUsed = false;
                using (myDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(sp,ps,clsDB.SPExMode.READER,ref cmd))
                    {
                        if (dR != null)
                        {
                            if (dR.HasRows) {
                                PartIsUsed = true;
                                htmlTable = MakeVendorPartNumberWhereUsedTable(dR, vpn, myDB);
                            } else
                            {
                                return "";
                            }
                        } else
                        {
                            return "<p>Error in WhereUsedForVPN: NULL dataset.  " + 
                                "This indicates a problem occurred when executing stored procedure " + 
                                DBK.SP.spOTSWHEREUSEDFORVENDORPARTNUMBERSTRING + ".</p>";
                        }
                    } 
                }
                if (PartIsUsed) {
                    if (includeWarning)
                    {
                        return GenerateVPNWhereUsedWarning(vpn, v) + htmlTable;
                    } else
                    {
                        return htmlTable;
                    }
                } else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                return "<p>Error in WhereUsedForVPN: " + ex.Message + "</p>";

            }
        }

        private string GenerateVPNWhereUsedWarning(string vendorPartNumber, string vendor)
        {
            string vendorInfo = "";
            if (vendor != "")
            {
                vendorInfo = " by <b>" + vendor + "</b>";
            }
            return "<p " + DynControls.encodeProperty("style", "font-size:60px;color:red") +
                DynControls.encodeProperty("text-align", "center") +
                ">!!! WARNING !!!</p>" +
                "<p>Vendor Part Number <b>" + vendorPartNumber + "</b>" + vendorInfo + 
                " is already used on other OTS Part Numbers.  See the table below for details.</p>";
        }

        /// <summary>
        /// Returns and html table summarizing the contents of the data reader
        /// </summary>
        /// <param name="dR">The data reader</param>
        /// <param name="vpn">A marker to make the table unique; use either the Vendor Part Number or the VPNID</param>
        public string MakeVendorPartNumberWhereUsedTable(SqlDataReader dR, string vpnMkr, clsDB myDB)
        {
            try
            {
                vpnMkr = vpnMkr.Replace(" ", "_");
                List<HTMLStrings.TableRow> lstTblRows = new List<HTMLStrings.TableRow>();
                lstTblRows.Add(new HTMLStrings.TableRow(
                    "row_h_" + vpnMkr,
                    "clsHeaderRow",
                    new HTMLStrings.TableCell[] {
                        new HTMLStrings.TableCell("","clsHeaderRow","OTS Part Number",1,true),
                        new HTMLStrings.TableCell("","clsHeaderRow","Requested By",1,true),
                        new HTMLStrings.TableCell("","clsHeaderRow","Requested For",1,true),
                        new HTMLStrings.TableCell("","clsHeaderRow","BU",1,true),
                        new HTMLStrings.TableCell("","clsHeaderRow","Date Requested",1,true)
                                                }
                                                     )
                             );
                int rowIndex = 0;
                while (dR.Read())
                {
                    lstTblRows.Add(new HTMLStrings.TableRow(
                        "row_" + vpnMkr + "_" + rowIndex.ToString(),
                        "tblRowWhereUsed",
                    new HTMLStrings.TableCell[] {
                        new HTMLStrings.TableCell("","whereUsedCell",myDB.Fld2Str(dR[DBK.strPARTNUMBER]),1,true),
                        new HTMLStrings.TableCell("","whereUsedCell",myDB.Fld2Str(dR[DBK.strNAME]),1,true),
                        new HTMLStrings.TableCell("","whereUsedCell",myDB.Fld2Str(dR[DBK.strPRODUCT]),1,true),
                        new HTMLStrings.TableCell("","whereUsedCell",myDB.Fld2Str(dR[DBK.strBUCODE]),1,true),
                        new HTMLStrings.TableCell("","whereUsedCell",myDB.Fld2Str(dR[DBK.dtREQUESTED]),1,true)
                                                }
                                                     )
                             );                        
                 }

                return new HTMLStrings.Table("tbl_" + vpnMkr, "tblWhereUsed", lstTblRows).ToHTML();
            }
            catch (Exception ex)
            {
                return "<p>Error in MakeVendorPartNumberWhereUsedTable: " + ex.Message + "</p>";

            }
        }

        /// <summary>
        /// Returns an html table summarizing the contents of the data reader
        /// NOTE: The IDs are similar to the format used for the Vendor Part Number, except instead of
        /// the ID ending in _[id], it ends in _pn[id].
        /// This helps distinguish Vendor Part Number objects and OTS Part Number objects that have the same ID
        /// (remember, each class of parts exist in separate tables)
        /// </summary>
        /// <param name="pnID">Database ID of the part number</param>
        /// <param name="dR">The data reader</param>
        public string MakePartNumberWhereUsedTable(SqlDataReader dR, Int64 pnID, clsDB myDB)
        {
            try
            {
                //First result is the part number
                dR.Read();
                string pn = myDB.Fld2Str(dR[DBK.strPARTNUMBER]);
                string id = pnID.ToString();
                //Advance to the next result
                dR.NextResult();
                if (dR.HasRows)
                {
                    List<HTMLStrings.TableRow> lstTblRows = new List<HTMLStrings.TableRow>();
                    lstTblRows.Add(new HTMLStrings.TableRow(
                        "row_h_pn" + id,
                        "clsHeaderRow",
                        new HTMLStrings.TableCell[] {
                        new HTMLStrings.TableCell("","clsHeaderRow","Assembly Name",1,true),
                        new HTMLStrings.TableCell("","clsHeaderRow","Assembly Part Number",1,true),
                        new HTMLStrings.TableCell("","clsHeaderRow","Assembly Revision",1,true),
                        new HTMLStrings.TableCell("","clsHeaderRow","BOM Revision",1,true),
                        new HTMLStrings.TableCell("","clsHeaderRow","Status",1,true),
                        new HTMLStrings.TableCell("","clsHeaderRow","Designer",1,true),
                        new HTMLStrings.TableCell("","clsHeaderRow","BU",1,true)
                                                    }
                                                         )
                                 );
                    int rowIndex = 0;
                    while (dR.Read())
                    {
                        string p = myDB.Fld2Str(dR[DBK.strNAME]);
                        string pR = myDB.Fld2Str(dR[DBK.strREVISION]);
                        string bR = myDB.Fld2Str(dR[DBK.intBOMREV]);
                        string prodLink = DynControls.html_hyperlink_string(p, HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                            "/BOMViewUpload.aspx?p=" + p + "&pR=" + pR + "&bR=" + bR + "&wu=" + pn, "", "PNWhereUsedURL", "_blank");
                        lstTblRows.Add(new HTMLStrings.TableRow(
                            "row_pn" + id + "_" + rowIndex.ToString(),
                            "tblRowWhereUsed",
                        new HTMLStrings.TableCell[] {
                        new HTMLStrings.TableCell("","whereUsedCell",prodLink,1,true,toolTip:"Click to view BOM in a new Browser Window"),
                        new HTMLStrings.TableCell("","whereUsedCell",myDB.Fld2Str(dR[DBK.strASSYPARTNUMBER]),1,true),
                        new HTMLStrings.TableCell("","whereUsedCell",pR,1,true),
                        new HTMLStrings.TableCell("","whereUsedCell",bR.PadLeft(2,'0'),1,true),
                        new HTMLStrings.TableCell("","whereUsedCell",myDB.Fld2Str(dR[DBK.strASSYSTATUS]),1,true),
                        new HTMLStrings.TableCell("","whereUsedCell",myDB.Fld2Str(dR[DBK.SP_COLALIAS.USERNAME]),1,true),
                        new HTMLStrings.TableCell("","whereUsedCell",myDB.Fld2Str(dR[DBK.valDISPLAYEDVALUE]),1,true)
                                                    }
                                                         )
                                 );
                    }

                    return new HTMLStrings.Table("tbl_pn" + id, "", lstTblRows).ToHTML();
                } else
                {
                    return "<p>This part is not used on any BOMs that have been uploaded to the database.</p>";
                }

            }
            catch (Exception ex)
            {
                return "<p>Error in MakePartNumberWhereUsedTable: " + ex.Message + "</p>";

            }
        }

        public string WhereUsedForVPNID(string input)
        {
            try
            {
                clsDB myDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@" + DBK.keyVENDORPN, input));
                string htmlResult = "";
                using (myDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(
                            DBK.SP.spOTSWHEREUSEDFORVENDORPARTNUMBER,ps,clsDB.SPExMode.READER,ref cmd))
                    {
                        if (dR != null)
                        {
                            if (dR.HasRows)
                            {
                                htmlResult = MakeVendorPartNumberWhereUsedTable(dR, input, myDB);
                            }
                            else
                            {
                                htmlResult = "<p>This Vendor Part Number resulted in the retrieval of no records from the database.</p>";
                            }
                        } else
                        {
                            htmlResult = "<p>No records retrieved; this may indicate a problem with execution of the Storeed Procedure.</p>";
                        }
                    }
                }
                return htmlResult;
            } catch (Exception ex)
            {
                return "<p>Error in WhereUsedForVPNID: " + ex.Message + "</p>";

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
        /// <param name="p">The product name</param>
        /// <param name="pRev">The product rev</param>
        /// <param name="bRev">The bom rev</param>
        /// <returns></returns>
        public string GetProductStatus(string p, string pRev, string bRev)
        {
            try
            {
                int x = -1;
                if (!int.TryParse(bRev, out x))
                {
                    x = -1;
                }
                clsDB xDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@" + DBK.strNAME, p));
                ps.Add(new SqlParameter("@" + DBK.strREVISION, pRev));
                ps.Add(new SqlParameter("@" + DBK.intBOMREV, x));
                using (xDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)xDB.ExecuteSP(DBK.SP.spGETASSYSTATUS,ps,clsDB.SPExMode.READER, ref cmd))
                    {
                        if (dR != null && dR.HasRows)
                        {
                            dR.Read();
                            return xDB.Fld2Str(dR[DBK.strASSYSTATUS]);
                        } else
                        {
                            return "NOT YET UPLOADED";
                        }
                    }
                }
            } catch (Exception ex)
            {
                return "<p>Error in CustomCode.GetProductStatus:" + AAAK.vbCRLF + 
                    ex.Message + AAAK.vbCRLF + ex.StackTrace + "</p>";
            }

        }


        /// <summary>
        /// Generates HTML for a Design Name's History
        /// </summary>
        /// <param name="desNm"></param>
        /// <returns></returns>
        public string DownloadHTMLforBOMHistory(string desNm)
        {
            StringBuilder sB = new StringBuilder();
            string rev = "";
            string bomRev = "";
            string status = "";
            string uploadDate = "";
            string uploader = "";
            string assyNumber = "";
            string relNotes = "";
            string relNoteID = "";
            string buttonViewBOM = DynControls.html_button_string("btnViewBOM_",
                "Open", "tableCellButton viewBOM",
                true, AAAK.DISPLAYTYPES.BLOCK, "Open this BOM in a new tab/window.");
            string buttonViewRelNotes = DynControls.html_button_string("btnViewRelNotes_",
                "Show", "tableCellButton btnExpand btnToggleRelNotes",
                true, AAAK.DISPLAYTYPES.BLOCK, "Show Release Notes.");
            string buttonEditReleaseNotes = DynControls.html_button_string("btnEditRelNotes_",
                "Edit", "relNoteButton relEdit",
                true, AAAK.DISPLAYTYPES.INLINE, "Put Release Notes in Edit Mode to make changes.");
            string buttonSaveReleaseNotes = DynControls.html_button_string("btnSaveRelNotes_",
                "Save", "relNoteButton relSave",
                true, AAAK.DISPLAYTYPES.INLINE, "Save your changes.",enabled:false);
            string buttonCancelReleaseNotes = DynControls.html_button_string("btnCancelRelNotes_",
                "Cancel", "relNoteButton relCancel",
                true, AAAK.DISPLAYTYPES.INLINE, "Cancel changes made to the Release Notes", enabled: false);

            try
            {
                List<HTMLStrings.TableRow> tblRows = new List<HTMLStrings.TableRow>();
                clsDB xDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@" + DBK.strNAME, desNm));
                using (xDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)xDB.ExecuteSP(DBK.SP.spGETASSYHISTORY, ps, clsDB.SPExMode.READER, ref cmd))
                    {
                        if (dR != null && dR.HasRows)
                        {
                            sB.Append(DynControls.html_header_string(desNm.ToUpper() + " History", 1));
                            tblRows.Add(new HTMLStrings.TableRow(
                                "row_h_pn",
                                "clsHeaderRow",
                                new HTMLStrings.TableCell[] {
                                new HTMLStrings.TableCell("","clsHeaderRow","Product</br>Rev",1,true),
                                new HTMLStrings.TableCell("","clsHeaderRow","BOM</br>Rev",1,true),
                                new HTMLStrings.TableCell("","clsHeaderRow","Status",1,true),
                                new HTMLStrings.TableCell("","clsHeaderRow","View BOM",1,true),
                                new HTMLStrings.TableCell("","clsHeaderRow","Release</br>Notes",1,true),
                                new HTMLStrings.TableCell("","clsHeaderRow","Date</br>Uploaded",1,true),
                                new HTMLStrings.TableCell("","clsHeaderRow","Uploaded</br>By",1,true),
                                new HTMLStrings.TableCell("","clsHeaderRow","Assembly Number",1,true)
                                                                    }
                                                                         )
                                                 );
                            int rowIndex = 0;
                            while (dR.Read())
                            {
                                //Format the text in the data reader
                                rev = xDB.Fld2Str(dR[DBK.strREVISION]);
                                bomRev = xDB.Fld2Str(dR[DBK.intBOMREV]).PadLeft(2,'0');
                                status = xDB.Fld2Str(dR[DBK.strASSYSTATUS]);
                                uploadDate = xDB.Fld2Str(dR[DBK.dtUPLOADED]);
                                uploader = xDB.Fld2Str(dR[DBK.strNAME]);
                                assyNumber = xDB.Fld2Str(dR[DBK.strASSYPARTNUMBER]);
                                relNotes = xDB.Fld2Str(dR[DBK.strREASON]);
                                relNoteID = xDB.Fld2Str(dR[DBK.ID]);

                                tblRows.Add(new HTMLStrings.TableRow(
                                            "otsFindResultsRow_" + rowIndex,
                                            "otsFindResultsRow",
                                            new HTMLStrings.TableCell[] {
                                                 new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell",rev,1,false),
                                                 new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell",bomRev,1,false),
                                                 new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell",status,1,false),
                                                 new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell withExpandButton",
                                                    buttonViewBOM.Replace("btnViewBOM_", "btnViewBOM_" + desNm + "_" + rev + "_" + bomRev),1,false,true),
                                                 new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell withExpandButton",
                                                    buttonViewRelNotes.Replace("btnViewRelNotes_", "btnViewRelNotes_" + rowIndex + "_" + relNoteID),1,false,true),
                                                 new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell",uploadDate,1,false),
                                                 new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell",uploader,1,false),
                                                 new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell",assyNumber,1,false)
                                                                        }
                                                                     )
                                            );


                                //We need to add one hidden row that will expand.
                                //Contents of the row:
                                //Text Area + buttons
                                string rC = DynControls.html_textarea_string("txtRelNote_" + rowIndex + "_" + relNoteID,
                                    "txtRelNote", relNotes, AAAK.DISPLAYTYPES.BLOCK, "", true) +
                                    buttonEditReleaseNotes.Replace("btnEditRelNotes_", "btnEditRelNotes_" + rowIndex + "_" + relNoteID) +
                                    buttonSaveReleaseNotes.Replace("btnSaveRelNotes_", "btnSaveRelNotes_" + rowIndex + "_" + relNoteID) +
                                    buttonCancelReleaseNotes.Replace("btnCancelRelNotes_", "btnCancelRelNotes_" + rowIndex + "_" + relNoteID);
                                HTMLStrings.TableRow expandRow = new HTMLStrings.TableRow(
                                            "otsExpandResultsRow_" + rowIndex,
                                            "otsExpandResultsRow",
                                            new HTMLStrings.TableCell[] {
                                                                new HTMLStrings.TableCell("otsDisplayAreaFor_" + rowIndex + "_" + relNoteID,
                                                                                            "otsFindResultsExpansionCell",
                                                                                            rC, 8, false)
                                                                        }

                                                                        );
                                expandRow.DisplayStyle = AAAK.DISPLAYTYPES.NONE;
                                tblRows.Add(expandRow);
                                rowIndex++;
                            }

                            //Finished reading dR; make table
                            HTMLStrings.Table t = new HTMLStrings.Table("otsSearchResults", "otsTable", tblRows);
                            sB.Append(t.ToHTML());
                        }
                        else
                        {
                            sB.Append("<p>Located no BOMs for <b><span style='color:red'>" +
                                                desNm +
                                                "</span></b>.  Please enter another Product Name.");
                        }
                        return sB.ToString();
                    }
                }

                    
            } catch (Exception ex)
            {
                return DynControls.renderLiteralControlErrorString(ex, "");
            }
        }

        /// <summary>
        /// Generates HTML for a formatted BOM
        /// </summary>
        /// <param name="p">Product Name</param>
        /// <param name="pRev">Product Revision</param>
        /// <param name="bRev">BOM Revision</param>
        /// <returns></returns>
        public string DownloadHTMLforBOM(string p, string pRev, string bRev)
        {
            string htmlForBOM = "";
            string htmlForBOMHeader = "";
            try
            {
                p = p.ToUpper();
                pRev = pRev.ToUpper();
                bRev = bRev.ToUpper();
                int x = -1;
                if (!int.TryParse(bRev, out x))
                {
                    x = -1;
                }
                clsDB xDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@" + DBK.strNAME, p));
                ps.Add(new SqlParameter("@" + DBK.strREVISION, pRev));
                ps.Add(new SqlParameter("@" + DBK.intBOMREV, x));
                using (xDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)xDB.ExecuteSP(DBK.SP.spDOWNLOADBOM, ps, clsDB.SPExMode.READER, ref cmd))
                    {
                        int resultSetIndex = 0;
                        do
                        {
                            if (dR != null)
                            {
                                if (resultSetIndex == 0)
                                {
                                    if (dR.HasRows)
                                    {
                                        htmlForBOMHeader = createHTMLStringForBOMHeader(dR, xDB);
                                    } else
                                    {
                                        htmlForBOMHeader = "<p>Found no Record of <b><span style='color:red'>" +
                                                p +
                                                "</span></b> Revision <b><span style='color:red'>" +
                                                pRev +
                                                "</span></b> BOM Revision <b><span style='color:red'>" +
                                                bRev +
                                                "</span></b>) </p><p>Validation checks should have prevented you " +
                                                "from seeing this message.  Please send this link to the Administrator.</p>";
                                    }
                                    
                                }
                                else
                                {
                                    if (dR.HasRows)
                                    {
                                        htmlForBOM = createHTMLStringForBOM(dR, xDB);
                                    }
                                    else
                                    {
                                        htmlForBOM = "<p>No BOM has been uploaded for <b><span style='color:red'>" +
                                                p +
                                                "</span></b> Revision <b><span style='color:red'>" +
                                                pRev +
                                                "</span></b> BOM Revision <b><span style='color:red'>" +
                                                bRev.PadLeft(2,'0') +
                                                "</span></b>.</p>";
                                    }
                                    
                                }

                            }
                            else
                            {
                            }
                            resultSetIndex = resultSetIndex + 1;
                        } while (dR.NextResult());
                    }
                }

                return htmlForBOMHeader + htmlForBOM;
            } catch (Exception ex)
            {
                return DynControls.renderLiteralControlErrorString(ex, "");
            }
        }

        private string createHTMLStringForBOMHeader(SqlDataReader dR, clsDB myDB)
        {
            try
            {
                StringBuilder sB = new StringBuilder();
                dR.Read();
                sB.Append(DynControls.html_header_string(myDB.Fld2Str(dR[DBK.strNAME]).ToUpper() +
                    " Rev " +
                    myDB.Fld2Str(dR[DBK.strREVISION]).ToUpper(), 1));

                sB.Append(DynControls.html_header_string("Assy Part Number: <u>" +
                    myDB.Fld2Str(dR[DBK.strASSYPARTNUMBER]).ToUpper() +
                    "</u> BOM Revision: <u>" +
                    myDB.Fld2Str(dR[DBK.intBOMREV]).ToUpper().PadLeft(2,'0') + "</u>", 3));
                sB.Append("<p>Last uploaded by <b>" + dR["USERNAME"] + "</b> at <b>" + ((DateTime)dR[DBK.dtUPLOADED]).ToString("g") + "</b>." );
                
                string strLbl = DynControls.html_label_string("lblReleaseNote", "Release Notes:",
                        assocCtl: "txtReasonForChange",
                        dType: AAAK.DISPLAYTYPES.BLOCK);
                if (int.Parse(myDB.Fld2Str(dR[DBK.keyASSYSTATUS]))==2)
                {
                    //This BOM is released; Create a disabled text area that is resizeable, with contents equal to the
                    //release reason
                    sB.Append(strLbl);
                    sB.Append(DynControls.html_textarea_string("txtReasonForChange", "txtinput",
                        myDB.Fld2Str(dR[DBK.strREASON]), AAAK.DISPLAYTYPES.BLOCK, "", true));
                    //Also add the ability to change the release reason
                    sB.Append(DynControls.html_button_string("btnEditRelNote", "EDIT", "relEdit",
                        dType: AAAK.DISPLAYTYPES.INLINEBLOCK,
                        toolTip: "Put the Release Note in Edit Mode so you can make and save changes."));
                    sB.Append(DynControls.html_button_string("btnSaveRelNote", "SAVE", "relSave",
                        dType: AAAK.DISPLAYTYPES.INLINEBLOCK,
                        enabled:false,
                        toolTip: "Save Changes to the Release Note"));
                    sB.Append(DynControls.html_button_string("btnCancelRelNote", "CANCEL", "relCancel",
                        dType: AAAK.DISPLAYTYPES.INLINEBLOCK,
                        enabled:false,
                        toolTip: "Cancel Changes to the Release Note"));
                } else
                {
                    //This BOM is not released. Add a Release button and a blank enabled text area

                    sB.Append(strLbl);
                    string releaseReason = "";
                    if (int.Parse(myDB.Fld2Str(dR[DBK.intBOMREV]))==1)
                    {
                        releaseReason = myDB.Fld2Str(dR[DBK.strREASON]);
                    }
                    sB.Append(DynControls.html_textarea_string("txtReasonForChange", "txtinput",
                         releaseReason, AAAK.DISPLAYTYPES.BLOCK,"Enter the reason you are changing the BOM Revision for this " + 
                         "Product. If this is the first time you are releasing this BOM, you can leave the text as 'Initial Release'",
                         false));

                    sB.Append(DynControls.html_button_string("btnDelete", "DELETE", "",
                        dType: AAAK.DISPLAYTYPES.INLINEBLOCK,
                        toolTip: "WARNING!  Once you press this button, this BOM will be PERMANENTLY deleted."));
                    sB.Append(DynControls.html_button_string("btnRelease", "RELEASE", "",
                        dType: AAAK.DISPLAYTYPES.INLINEBLOCK,
                        toolTip: "WARNING!  Once you press this button and release this BOM, the BOM will be LOCKED.  " + 
                        "You will be unable to make changes to this BOM Revision."));

                }
                //Provide a gap between this and the next block
                sB.Append(DynControls.html_linebreak_string());
                return sB.ToString();
            } catch (Exception ex)
            {
                return DynControls.renderLiteralControlErrorString(ex, "");
            }

        }

        private string createHTMLStringForBOM(SqlDataReader dR, clsDB myDB)
        {
            try
            {
                List<HTMLStrings.TableRow> tblRows = new List<HTMLStrings.TableRow>();
                int expandSpan = 0;
                //Add the Header Row
                tblRows.Add(new HTMLStrings.TableRow(
                            "bomHeader",
                            "clsHeaderRow",
                            new HTMLStrings.TableCell[] {
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","PART NUMBER",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","STATUS",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","ENV CODE",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","QTY",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","DESCRIPTION",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","REFERENCE DESIGNATORS",1,true),
                                                                    new HTMLStrings.TableCell("","clsHeaderRow","BOM NOTES",1,true)
                                                        }
                                                      )
                                     );


                expandSpan = tblRows[0].Cells.Count();
                string pn = "";
                string qty = "";
                string desc = "";
                string refdes = "";
                string bomnotes = "";
                string pnID = "";
                string status = "";
                string ecode = "";

                while (dR.Read())
                {
                    //Format the text in the data reader
                    pn = myDB.Fld2Str(dR[DBK.strPARTNUMBER]);
                    status = myDB.Fld2Str(dR[DBK.strSTATUS]);
                    ecode = myDB.Fld2Str(dR[DBK.strECODENAME]);
                    pnID = myDB.Fld2Str(dR[DBK.keyPN]);
                    qty = myDB.Fld2Str(dR[DBK.intQTY]);
                    desc = myDB.Fld2Str(dR[DBK.strDESCRIPTION]);
                    refdes = myDB.Fld2Str(dR[DBK.strREFDES]);
                    bomnotes = myDB.Fld2Str(dR[DBK.strBOMNOTES]);
                    string buttonhtml = DynControls.html_button_string("otsbtnFoundExpand_" + pnID, "Expand", "otsbtnFoundExpand btnExpand tableCellButton",
                        true, AAAK.DISPLAYTYPES.BLOCK, "Expand to get more information about the Part Number");
         
                    tblRows.Add(new HTMLStrings.TableRow(
                                "otsFindResultsRow_" + pnID,
                                "otsFindResultsRow",
                                new HTMLStrings.TableCell[] {
                                                        new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell withExpandButton",buttonhtml.Replace("btnOTSFOUNDID_", "btnOTSFOUNDID_" + pnID),1,false,true),
                                                        new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell",pn,1,false),
                                                        new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell",status,1,false),
                                                        new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell",ecode,1,false),
                                                        new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell",qty,1,false),
                                                        new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell",desc,1,false),
                                                        new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell",refdes,1,false),
                                                        new HTMLStrings.TableCell("","otsTableCell otsFindResultsCell",bomnotes,1,false)
                                                            }
                                                         )
                                );
                     

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
                } //end single pass over datareader

                HTMLStrings.Table resultTable = new HTMLStrings.Table("otsSearchResults", "otsTable", tblRows);
                return  resultTable.ToHTML();
            }
            catch (Exception ex)
            {
                return DynControls.renderLiteralControlErrorString(ex, "");
            }

        }

        /// <summary>
        /// Changes the status of an assy BOM in asyBOM.
        /// Returns the number of rows affected; you should get 1.
        /// If you get more than 1, something is wrong (bom/rev/bomrev should be unique)
        /// If you get something less than 1, somethig is also wrong (nothing changed)
        /// </summary>
        /// <param name="p">Product Name</param>
        /// <param name="pRev">Product Rev</param>
        /// <param name="bRev">BOM Rev</param>
        /// <returns></returns>
        public string releaseBOM(string input)
        {
            try
            {
                string[] arr = input.Split(m_dlim, StringSplitOptions.None);
                string p = arr[0].ToUpper();
                string pRev = arr[1].ToUpper();
                string bRev = arr[2].ToUpper();
                string relNote = arr[3].Trim();
                string result = "";
                int x = -1;
                if (!int.TryParse(bRev, out x))
                {
                    x = -1;
                }
                clsDB xDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@" + DBK.strNAME, p));
                ps.Add(new SqlParameter("@" + DBK.strREVISION, pRev));
                ps.Add(new SqlParameter("@" + DBK.intBOMREV, x));
                using (xDB.OpenConnection())
                {
                    int nAffectedRows = -1;
                    result = xDB.Fld2Str(xDB.ExecuteSP(DBK.SP.spRELEASEBOM, ps, clsDB.SPExMode.NONQUERY, ref cmd)); 
                    if (int.TryParse(result,out nAffectedRows) && 
                        nAffectedRows==1 && 
                        relNote.ToUpper() != "INITIAL RELEASE")
                    {
                        cmd.Parameters.Clear();
                        ps.Add(new SqlParameter("@" + DBK.strREASON, relNote));
                        xDB.ExecuteSP(DBK.SP.spEDITRELEASENOTE, ps, clsDB.SPExMode.NONQUERY, ref cmd);
                    }              
                }
                return result;
                

            }
            catch (Exception ex)
            {
                return DynControls.renderLiteralControlErrorString(ex, "");
            }
        }

        /// <summary>
        /// Deletes one row from asyBOM.
        /// Returns the number of rows affected; you should get 1.
        /// If you get more than 1, something is wrong (bom/rev/bomrev should be unique)
        /// If you get something less than 1, somethig is also wrong (nothing changed)
        /// </summary>
        /// <param name="p">Product Name</param>
        /// <param name="pRev">Product Rev</param>
        /// <param name="bRev">BOM Rev</param>
        /// <returns></returns>
        public string deleteBOM(string input)
        {
            try
            {
                string[] arr = input.Split(m_dlim, StringSplitOptions.None);
                string p = arr[0].ToUpper();
                string pRev = arr[1].ToUpper();
                string bRev = arr[2].ToUpper();
                int x = -1;
                if (!int.TryParse(bRev, out x))
                {
                    x = -1;
                }
                clsDB xDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@" + DBK.strNAME, p));
                ps.Add(new SqlParameter("@" + DBK.strREVISION, pRev));
                ps.Add(new SqlParameter("@" + DBK.intBOMREV, x));
                using (xDB.OpenConnection())
                {
                    string y =  xDB.Fld2Str(xDB.ExecuteSP(DBK.SP.spDELETEASYBOMENTRY, ps, clsDB.SPExMode.NONQUERY, ref cmd));
                    return y;
                }

            }
            catch (Exception ex)
            {
                return DynControls.renderLiteralControlErrorString(ex, "");
            }
        }

        /// <summary>
        /// Changes the release note for a given product/rev/bom rev
        /// </summary>
        /// <param name="input">product[DELIM]product rev[DELIM]bom rev[DELIM]release note</param>
        /// <param name="reset">if this true, we call a special stored proc to reset the release note to 'Initial Release'</param>
        /// <returns></returns>
        public string EditReleaseNote(string input, Boolean reset)
        {
            try
            {
                string[] arr = input.Split(m_dlim, StringSplitOptions.None);
                string p = arr[0].ToUpper();
                string pRev = arr[1].ToUpper();
                string bRev = arr[2].ToUpper();
                string relNote = arr[3].Trim();
                int x = -1;
                if (!int.TryParse(bRev, out x))
                {
                    x = -1;
                }
                clsDB xDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@" + DBK.strNAME, p));
                ps.Add(new SqlParameter("@" + DBK.strREVISION, pRev));
                ps.Add(new SqlParameter("@" + DBK.intBOMREV, x));
                
                using (xDB.OpenConnection())
                {
                    if (reset)
                    {
                        return (xDB.ExecuteSP(DBK.SP.spEDITRELEASENOTE_RESET, ps, clsDB.SPExMode.NONQUERY, ref cmd)).ToString();
                    } else
                    {
                        //Since we're aren't doing a reset, we must include the new release note as a parameter
                        ps.Add(new SqlParameter("@" + DBK.strREASON, relNote));
                        return (xDB.ExecuteSP(DBK.SP.spEDITRELEASENOTE, ps, clsDB.SPExMode.NONQUERY, ref cmd)).ToString();
                    }
                    
                }

            }
            catch (Exception ex)
            {
                return DynControls.renderLiteralControlErrorString(ex, "");
            }
        }

        /// <summary>
        /// Takes the contents of input and puts in invLocationBulk
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string registerLocation(string input)
        {
            try
            {
                string[] arr = input.Split(m_dlim, StringSplitOptions.None);
                string addr = arr[0].Trim().ToUpper().
                    Replace(" STREET"," ST").
                    Replace(" AVENUE"," AVE").
                    Replace(" BOULEVARD"," BLVD").
                    Replace(" SUITE"," STE").
                    Replace(" COURT"," CT").
                    Replace(" ST.", " ST").
                    Replace(" AVE.", " AVE").
                    Replace(" BLVD.", " BLVD").
                    Replace(" STE.", " STE").
                    Replace(" CT.", " CT");

                string city = arr[1].Trim().ToUpper();
                string state = arr[2].Trim().ToUpper().
                    Replace("ALABAMA", "AL").
                    Replace("ALASKA", "AK").
                    Replace("ARIZONA", "AZ").
                    Replace("ARKANSAS", "AR").
                    Replace("CALIFORNIA", "CA").
                    Replace("COLORADO", "CO").
                    Replace("CONNECTICUT", "CT").
                    Replace("DELAWARE", "DE").
                    Replace("FLORIDA", "FL").
                    Replace("GEORGIA", "GA").
                    Replace("HAWAII", "HI").
                    Replace("IDAHO", "ID").
                    Replace("ILLINOIS", "IL").
                    Replace("INDIANA", "IN").
                    Replace("IOWA", "IA").
                    Replace("KANSAS", "KS").
                    Replace("KENTUCKY", "KY").
                    Replace("LOUISIANA", "LA").
                    Replace("MAINE", "ME").
                    Replace("MARYLAND", "MD").
                    Replace("MASSACHUSETTS", "MA").
                    Replace("MICHIGAN", "MI").
                    Replace("MINNESOTA", "MN").
                    Replace("MISSISSIPPI", "MI").
                    Replace("MISSOURI", "MO").
                    Replace("MONTANA", "MT").
                    Replace("NEBRASKA", "NE").
                    Replace("NEVADA", "NV").
                    Replace("NEW HAMPSHIRE", "NH").
                    Replace("NEW JERSEY", "NJ").
                    Replace("NEW MEXICO","NM").
                    Replace("NEW YORK", "NY").
                    Replace("NORTH CAROLINA", "NC").
                    Replace("NORTH DAKOTA", "ND").
                    Replace("OHIO", "OH").
                    Replace("OKLAHOMA", "OK").
                    Replace("OREGON", "OR").
                    Replace("PENNSYLVANIA", "PA").
                    Replace("RHODE ISLAND", "RI").
                    Replace("SOUTH CAROLINA", "SC").
                    Replace("SOUTH DAKOTA", "SD").
                    Replace("TENNESSEE", "TN").
                    Replace("TEXAS", "TX").
                    Replace("UTAH", "UT").
                    Replace("VERMONT", "VT").
                    Replace("VIRGINIA", "VA").
                    Replace("WASHINGTON", "WA").
                    Replace("WEST VIRGINIA", "WV").
                    Replace("WISCONSIN", "WI").
                    Replace("WYOMING", "WY").
                    Replace("PUERTO RICO", "PR");

                string postalCode = arr[3].Trim().ToUpper();
                string country = arr[4].Trim().ToUpper();
                string floor = arr[5].Trim();
                string detail = arr[6].Trim();

                Int64 uid = 1; //REPLACE WITH ACTUAL USER ID IN REAL APPLICATION!!!
                CustomCode u = new CustomCode();
                clsDB xDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@" + DBK.strCOUNTRY, country));
                ps.Add(new SqlParameter("@" + DBK.strPOSTALCODE, postalCode));
                ps.Add(new SqlParameter("@" + DBK.strSTATEPROVINCE, state));
                ps.Add(new SqlParameter("@" + DBK.strCITY, city));
                ps.Add(new SqlParameter("@" + DBK.strADDRESS, addr));
                ps.Add(new SqlParameter("@" + DBK.strDETAIL, detail));
                ps.Add(new SqlParameter("@" + DBK.strFLOOR, floor));
                ps.Add(new SqlParameter("@" + DBK.keyDEFINEDBY, uid));
                ps.Add(new SqlParameter("@" + DBK.ID, -1));
                ps[ps.Count - 1].Direction = System.Data.ParameterDirection.Output;
                using (xDB.OpenConnection())
                {
                    xDB.ExecuteSP(DBK.SP.spLOCADDLOCATION, ps, clsDB.SPExMode.NONQUERY, ref cmd);
                    if (int.Parse(cmd.Parameters["@" + DBK.ID].Value.ToString())>0)
                    {
                        return "<p>The following location is in the database:</p>" +
                            "<p>" + addr + "</p>" +
                            "<p>" + city + ", " + state + " " + postalCode + "</p>" +
                            "<p>" + country + "</p>" +
                            "<p>Floor: " + floor + "</p>" +
                            "<p>Details: " + detail + "</p>" +
                            "<p>The Confirmation ID is " + int.Parse(cmd.Parameters["@" + DBK.ID].Value.ToString());
                    } else
                    {
                        return "<p>Unable to add address:</p>" +
                            "<p>" + xDB.ErrMsg.Replace(AAAK.vbCRLF, DynControls.html_linebreak_string()) + "</p>";
                    }

                }

            } catch (Exception ex)
            {
                return (ex.Message + AAAK.vbCRLF + ex.StackTrace).Replace(AAAK.vbCRLF, DynControls.html_linebreak_string());
            }

        }

        public string autoCompleteAddress(string streetAddress)
        {
            try
            {
                clsDB xDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@" + DBK.strADDRESS, streetAddress));
                using (xDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)xDB.ExecuteSP(DBK.SP.spLOCLOOKUPADDRESSBYSTREET, ps, clsDB.SPExMode.READER, ref cmd))
                    {
                        if (dR != null && dR.HasRows)
                        {
                            dR.Read();
                            return dR[DBK.strADDRESS] +  AAAK.DELIM +
                                dR[DBK.strCITY] + AAAK.DELIM +
                                dR[DBK.strSTATEPROVINCE] + AAAK.DELIM +
                                dR[DBK.strPOSTALCODE] + AAAK.DELIM +
                                dR[DBK.strCOUNTRY] + AAAK.DELIM +
                                dR[DBK.strFLOOR] + AAAK.DELIM +
                                dR[DBK.strDETAIL] + AAAK.DELIM;

                        }
                    }
                }
                return "";
            } catch (Exception ex)
            {
                return "";
            }
           
        }

        /// <summary>
        /// Returns html that is displayed in the inventory tab of a part number
        /// </summary>
        /// <param name="input">otsParts.ID for the part number</param>
        /// <returns></returns>
        public string InvForPN(string input)
        {
            try
            {
                StringBuilder sB = new StringBuilder();
                int qty = 0;
                Int64 pnID = Int64.Parse(input);
                clsDB myDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@" + "pnID", pnID));

                string sp = DBK.SP.spOTSGETPNINVENTORYINFO;

                using (myDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(sp, ps, clsDB.SPExMode.READER, ref cmd))
                    {
                        int resultSetIndex = 0;
                        do
                        {
                            if (dR != null )
                            {
                                if (resultSetIndex == 0)
                                {
                                    if (dR.HasRows)
                                    {
                                        resultSetIndex++;
                                        dR.Read();
                                        qty = (int)dR[DBK.SP_COLALIAS.ONHAND];
                                        sB.Append(DynControls.html_header_string("TOTAL ON-HAND INVENTORY: " + qty, 2,"invhdr_" + pnID) + "<hr>");
                                        //Disable the following until I understand how to transfer formatting of a TAB to formatting a
                                        //PAGE
                                        //sB.Append( "<p>URL: " + DynControls.html_hyperlink_string("",
                                        //    HttpContext.Current.Request.Url.AbsoluteUri + ".aspx?ID=" + pnID + "&INV=Y",
                                        //    "bkmkinv_" + pnID, "bkmkID", "_blank") + "</p>");
                                    } else
                                    {
                                        sB.Append("<p>Error: Unable to find Total Inventory of Part with database ID " +
                                            pnID.ToString() + ".</p>");
                                    }
                                }
                                else
                                {
                                    sB.Append(MakePartNumberInventoryTable(dR, pnID, myDB));
                                    //Add divInvCmt_ID to allow the user to comment
                                    sB.Append("<div id='divInvCmt_" + pnID + "'>");
                                    sB.Append(DynControls.html_label_string("lblInvCmt_" + pnID, "Reason for Update (required):", dType: AAAK.DISPLAYTYPES.INLINEBLOCK));
                                    sB.Append(DynControls.html_txtbox_string("txtInvCmt_" + pnID, "inv txtinput txtInvChangeCmt " + pnID));
                                    sB.Append("</div>");
                                }
                            }
                            else
                            {
                                sB.Append("<p>Error in InvForPN, ResultSet " + (resultSetIndex + 1) + ": NULL dataset.  " +
                                    "This indicates a problem occurred when executing stored procedure " +
                                    DBK.SP.spOTSGETPNINVENTORYINFO + ".</p>");
                            }
        
                        } while (dR.NextResult());

                    }
                }
                return sB.ToString();
            }
            catch (Exception ex)
            {
                return "<p>Error in InvForPN: " + ex.Message + "</p>";
            }
        }

        /// <summary>
        /// Returns the html for a table has all the inventory info for a given part number with part number
        /// pnID
        /// </summary>
        /// <param name="dR">The data reader containing the data</param>
        /// <param name="myDB">The clsDB object</param>
        /// <param name="pnID">ID in table otsParts.ID for the Part Number in question. This is used in the uid field
        /// of the input cells.</param>
        /// <returns>
        /// NOTE:
        /// The table is given ID = tblInvInfo_[pnID]</returns>
        /// Each row is given ID = invrow_[VPNID]_[pnID], where [VPNID] is the vendor part number ID taken from the database.
        /// Each CONTROL in the cell in the row is given ID = [baseName]_[colInddex]_[VPNID]_[pnID]
        /// where
        /// basename = 
        /// txtinput => Textbox
        /// cboinput => Dropdown box
        /// btninput => Button
        /// Note the cells themselves do not get an ID
        private string MakePartNumberInventoryTable(SqlDataReader dR, Int64 pnID, clsDB myDB)
        {
            try
            {
                //Column widths:
                string qtyColWidth = "width:10px";
                clsUtil helper = new clsUtil();
                List<HTMLStrings.TableRow> tblRows = new List<HTMLStrings.TableRow>();
                List<string> lstLoc = helper.PutKVPInDictionary("kvpl_" + DBK.SP.spGETKVPFULLADDRESS);
                List<string> lstUsers = helper.PutKVPInDictionary("kvpl_" + DBK.SP.spGETKVPUSERINFO);
                List<string> lstSign = new List<string>();
                lstSign.Add("0");
                lstSign.Add("");
                lstSign.Add("1");
                lstSign.Add("REMOVE");
                lstSign.Add("2");
                lstSign.Add("ADD");
                tblRows.Add(new HTMLStrings.TableRow(
                        "row_h",
                        "clsHeaderRow",
                        new HTMLStrings.TableCell[] {
                                        new HTMLStrings.TableCell("","clsHeaderRow","Vendor",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Vendor<br>Part<br>Number",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Current Qty",1,true,
                                        displayStyle:qtyColWidth),
                                        new HTMLStrings.TableCell("","clsHeaderRow toggleCol " + pnID,"Action",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow toggleCol " + pnID,"Delta",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Location",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Contact",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Add Location",1,true)
                                                    }
                                                         )
                                 );
                while (dR.Read())
                {
                    string vpnID = myDB.Fld2Str(dR[DBK.SP_COLALIAS.VENDORPNID]);
                    string vendor = myDB.Fld2Str(dR[DBK.strVENDOR]);
                    string vPN = myDB.Fld2Str(dR[DBK.strVENDORPARTNUMBER]);
                    string qty = myDB.Fld2Str(dR[DBK.SP_COLALIAS.ONHAND]);
                    string locID = myDB.Fld2Str(dR[DBK.keyLOCATIONBULK]);
                    string contactID = myDB.Fld2Str(dR[DBK.keyOWNER]);
                    string invBulkID = myDB.Fld2Str(dR[DBK.SP_COLALIAS.INVBULKID]);
                    //Add an html table row to the list
                    //No need to track the row ID in the unique ID, UID.  This is because
                    //we expect a unique Vendor Part Number ID (vpnID) for each VPN.
                    string uid = "_" + vpnID + "_" + pnID + "_" + invBulkID;

                    string buttonAddLochtml = DynControls.html_button_string("btnAddLoc" + uid, "Add Location", "btnAddLoc " + pnID,
                        true, AAAK.DISPLAYTYPES.BLOCK, "Add a new Location and/or SubInventory for this Vendor Part Number.",
                        enabled:false);
                    tblRows.Add(new HTMLStrings.TableRow(
                    "invrow" + uid,
                    "clsInvRow",
                    new HTMLStrings.TableCell[] {
                                                    new HTMLStrings.TableCell("","clsInvCell_" + pnID,vendor,1,false, true,
                                                        "invVendor" + uid, null, "", "clsInvCellInfo " + pnID, false),
                                                    new HTMLStrings.TableCell("","clsInvCell_" + pnID,vPN,1,false, true,
                                                        "invVendorPN" + uid, null, "","clsInvCellInfo " + pnID, false),
                                                    new HTMLStrings.TableCell("","clsInvCell_" + pnID,qty,1,false,true,
                                                        "invQty" + uid, null, "",qtyColWidth, false),
                                                    new HTMLStrings.TableCell("","toggleCol clsInvCell_" + pnID + " " + pnID,"",1,false,true,
                                                        "invSign" + uid, lstSign, "", "inv cboinput toggle " + pnID, false,
                                                        "Specify whether you are ADDING TO or REMOVING FROM inventory"),
                                                    new HTMLStrings.TableCell("","toggleCol clsInvCell_" + pnID + " " + pnID,"0",1,false,true,
                                                        "invDelta" + uid, null, "", "inv txtinput toggle " + pnID, false,
                                                        "This is always a POSITIVE number!  Set 'Action' to ADD or REMOVE to add to or subtract from inventory."),
                                                    new HTMLStrings.TableCell("","clsInvCell_" + pnID,locID,1,false,true,
                                                        "invLocCode" + uid, lstLoc, "", "inv cboinput toggle " + pnID, false,"",true),
                                                    new HTMLStrings.TableCell("","clsInvCell_" + pnID,contactID,1,false,true,
                                                        "invContactCode" + uid, lstUsers, "", "inv cboinput toggle " + pnID, false,"",true),
                                                    new HTMLStrings.TableCell("","clsInvCell_" + pnID,buttonAddLochtml,1,false,true,
                                                        "",null,"Add Location","btnAddLocInv")
                                                }
                                                     )
                             );
                }
                //End processing the data row

                //Make buttons
                //Text for tool tips.
                string editTT = "Update the Qty, Location, and/or Contact Person for the given Vendor PN.";
                string saveTT = "Save your changes; to remove a location, leave the Qty and Location blank.";
                string cancelTT = "Cancel your changes; no data will change in the database.";
                //Make Add, Edit, Save, and Cancel buttons
                string btnEdit = DynControls.html_button_string("editinv_" + pnID, "Edit", "editinv " + pnID,
                    true, AAAK.DISPLAYTYPES.INLINE, editTT, "", true);
                string btnSave = DynControls.html_button_string("saveinv_" + pnID, "Save", "saveinv " + pnID,
                    true, AAAK.DISPLAYTYPES.INLINE, saveTT, "", false);
                string btnCancel = DynControls.html_button_string("cancelinv_" + pnID, "Cancel", "cancelinv " + pnID,
                    true, AAAK.DISPLAYTYPES.INLINE, cancelTT, "", false);

                HTMLStrings.Table tbl = new HTMLStrings.Table("tblInvInfo_" + pnID, "tblInvInfo", tblRows);
                return btnEdit +  btnSave + btnCancel +
                    "<p>Tip: If you don't see the scroll bars at the bottom of the window, use the left and " + 
                    "right arrow keys to scroll through the full table width.</p><hr>" + 
                    "<div id='divInvMsg_" + pnID + "' style='color:red;font-style:italic'></div>" + tbl.ToHTML();
            }
            catch (Exception ex)
            {
                return "<p>Method createHTMLStringForVendorPNInfo error: " + ex.Message + "</p>";
            }
        }

        /// <summary>
        /// UPdates invBulk with user information in input
        /// </summary>
        /// <param name="input">FORMAT:
        /// [comment]DELIM[invBulk.ID]DELIM[QTY]DELIM[DELTA]DELIM[LocationID]DELIM[OwnerID]DELIM[VPNID]...</param>
        /// <returns></returns>
        public string  UpdatePartInventory(string input)
        {
            StringBuilder sB = new StringBuilder();
            try
            {
                string[] arr = input.Split(m_dlim, StringSplitOptions.None);
                string cmt = arr[0].ToUpper();
                for (int i =1;i<arr.Length;i=i+6)
                {
                    clsDB myDB = new clsDB();
                    SqlCommand cmd = new SqlCommand();
                    List<SqlParameter> ps = new List<SqlParameter>();
                    string spName = "";
                    string ID = arr[i];
                    string Qty = arr[i + 1];
                    string Delta = arr[i + 2];
                    string Loc = arr[i + 3];
                    string Owner = arr[i + 4];
                    string VPNID = arr[i + 5];

                    int oldQty = 0;
                    int oldLoc = -1;
                    Int64 oldOwner = -1;
                    //Get the current location, qty, and owner for the given ID
                    ps.Add(new SqlParameter("@" + DBK.ID, Int64.Parse(ID)));
                    using (myDB.OpenConnection())
                    {
                        using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(DBK.SP.spINVGETINFOFORINVBULKID, ps, clsDB.SPExMode.READER, ref cmd))
                        {
                            if (dR !=null && dR.HasRows)
                            {
                                dR.Read();
                                oldQty = (int)dR[DBK.intQTY];
                                oldLoc = (int)dR[DBK.keyLOCATIONBULK];
                                oldOwner = (Int64)dR[DBK.keyOWNER];
                            }
                        }
                    }

                    //Reset for next usage
                    ps.Clear();

                    if (Qty=="-1" && Int64.Parse(ID) > -1)
                    {
                        //User wants to remove this entry from invBulk
                        ps.Add(new SqlParameter("@" + DBK.keyBULKITEM, VPNID));
                        ps.Add(new SqlParameter("@" + DBK.keyCHANGEDBY, 1));
                        ps.Add(new SqlParameter("@" + DBK.intDELTA, -oldQty));
                        ps.Add(new SqlParameter("@" + DBK.strCOMMENT, cmt));
                        ps.Add(new SqlParameter("@" + DBK.keyTRANSACTIONTYPE, 1));
                        ps.Add(new SqlParameter("@" + DBK.keyLOCATIONBULK, oldLoc));
                        ps.Add(new SqlParameter("@" + DBK.keyOWNER, oldOwner));
                        ps.Add(new SqlParameter("@" + DBK.ID, ID));
                        spName = DBK.SP.spINVREMOVEBULKINVENTRY;
                    }
                    else if (!clsUtil.IsInteger(Qty))
                    {
                        //Ignore this value, but alert user via email
                    }
                    else if (Qty != "" && Int64.Parse(Loc) > -1 && Int64.Parse(Owner)   > -1) {
                        //Update Qty based on Delta; note the client has already determined if there is a minus or not in front of delta, so
                        int newQty = int.Parse(Qty) + int.Parse(Delta);

                        //Only continue if there is a change in the data
                        if (oldLoc != int.Parse(Loc) || oldOwner != Int64.Parse(Owner) || oldQty != newQty)
                        {
                            //User wants to make a new/update an existing entry
                            ps.Add(new SqlParameter("@" + DBK.keyBULKITEM, VPNID));
                            ps.Add(new SqlParameter("@" + DBK.keyCHANGEDBY, 1));
                            ps.Add(new SqlParameter("@" + DBK.intDELTA, Delta));
                            ps.Add(new SqlParameter("@" + DBK.strCOMMENT, cmt));
                            ps.Add(new SqlParameter("@" + DBK.keyTRANSACTIONTYPE, 1));
                            ps.Add(new SqlParameter("@" + DBK.keyLOCATIONBULK, Loc));
                            ps.Add(new SqlParameter("@" + DBK.keyOWNER, Owner));
                            ps.Add(new SqlParameter("@" + DBK.intQTY, newQty));
                            ps.Add(new SqlParameter("@" + DBK.ID, ID));
                            spName = DBK.SP.spINVUPSERTINVBULKENTRY;
                        }

                    }
                    if (spName != "")
                    {
                        using (myDB.OpenConnection())
                        {
                            myDB.ExecuteSP(spName, ps, clsDB.SPExMode.NONQUERY, ref cmd);
                            {
                                if (myDB.ErrMsg != "")
                                {
                                    sB.Append(myDB.ErrMsg);
                                }
                            }
                        }
                    }
                }

                return sB.ToString().Replace(AAAK.vbCRLF, DynControls.html_linebreak_string());
            } catch (Exception ex)
            {
                return (ex.Message + AAAK.vbCRLF + ex.StackTrace).Replace(AAAK.vbCRLF, DynControls.html_linebreak_string());
            }
        }

        /// <summary>
        /// Returns the HTML for a table showing a Part's Inventory History
        /// </summary>
        /// <param name="id">Value of otsParts.ID for which we want the history</param>
        /// <returns></returns>
        public string MakePartNumberInventoryHistoryTable(Int64 id)
        {
            try
            {

                List<HTMLStrings.TableRow> tblRows = new List<HTMLStrings.TableRow>();
                tblRows.Add(new HTMLStrings.TableRow(
                        "row_h",
                        "clsHeaderRow",
                        new HTMLStrings.TableCell[] {
                                        new HTMLStrings.TableCell("","clsHeaderRow","Transaction<br />Date",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Vendor PN",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Transaction<br />Qty",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Comment",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Changed By",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","New Location",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","New Contact<br />Person",1,true),
                                        new HTMLStrings.TableCell("","clsHeaderRow","Update<br />Method",1,true),
                                                    }
                                                         )
                                 );

                clsDB myDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@" + DBK.ID, id));
                using (myDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(DBK.SP.spINVGETPARTHISTORY,ps,clsDB.SPExMode.READER,ref cmd))
                    {
                        if (dR != null && dR.HasRows)
                        {
                            while (dR.Read())
                            {
                                tblRows.Add(new HTMLStrings.TableRow(
                                        "row_" + id,
                                        "rowInvHistory",
                                        new HTMLStrings.TableCell[] {
                                        new HTMLStrings.TableCell("","clsInvHistCell",myDB.Fld2Str(dR[DBK.dtTRANSACTION])),
                                        new HTMLStrings.TableCell("","clsInvHistCell",myDB.Fld2Str(dR[DBK.strVENDORPARTNUMBER])),
                                        new HTMLStrings.TableCell("","clsInvHistCell",myDB.Fld2Str(dR[DBK.intDELTA])),
                                        new HTMLStrings.TableCell("","clsInvHistCell",myDB.Fld2Str(dR[DBK.strCOMMENT])),
                                        new HTMLStrings.TableCell("","clsInvHistCell",myDB.Fld2Str(dR[DBK.SP_COLALIAS.CHANGEDBY])),
                                        new HTMLStrings.TableCell("","clsInvHistCell",myDB.Fld2Str(dR[DBK.SP_COLALIAS.FULLADDRESS])),
                                        new HTMLStrings.TableCell("","clsInvHistCell",myDB.Fld2Str(dR[DBK.SP_COLALIAS.CONTACT])),
                                        new HTMLStrings.TableCell("","clsInvHistCell",myDB.Fld2Str(dR[DBK.strTYPE])),
                                                                    }
                                                                         )
                                                 );
                            }
                        }

                    }
                }

                HTMLStrings.Table tbl = new HTMLStrings.Table("tblInvHistory_" + id.ToString(), "tblInvHistory", tblRows);
                //Disable the following until I understand how to transfer formatting of a TAB to formatting a
                //PAGE
                //return "<p>URL: " + DynControls.html_hyperlink_string("",
                //                            HttpContext.Current.Request.Url.AbsoluteUri + ".aspx?ID=" + id + "&INVH=Y",
                //                            "bkmkinvh_" + id, "bkmkID", "_blank")  + "</p>" +
                //    tbl.ToHTML();
                return tbl.ToHTML();

            }
            catch (Exception ex)
            {
                return (ex.Message + AAAK.vbCRLF + ex.StackTrace).Replace(AAAK.vbCRLF, DynControls.html_linebreak_string());
            }
        }

    }


}