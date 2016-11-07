using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Constatns for the AskAndAnswer module
/// </summary>
namespace AskAndAnswer
{
    /// <summary>
    /// Ask And Answer Konstants
    /// </summary>
    public class AAAK
    {
        public static string vbCRLF = System.Environment.NewLine;
        public const string DQ = "\"";
        public const string defaultConnStr = "AskAndAddConnectionString";
        public const string DELIM = "!#!";  //Constant used to separate values when communicatig with client (at least,
        //until I get JSON working....)
        //Table & field Names
        public const string WEBDISPLAYFIELDS = "webDisplayFields";
        public const string ID = "ID";
        public const string fkAPPID = "fkAppID";
        public const string intDISPLAYORDER = "intDisplayOrder";
        public const string fkCONTROLTYPE = "fkControlType";
        public const string lblPROMPT = "lblPrompt";
        public const string lblHELPMESSAGE = "lblHelpMessage";
        public const string txtDEFAULTVALUE = "txtDefaultValue";
        public const string fkKVPGROUPIDDecoder = "fkKVPGroupIDDecoder";
        public const string blINITVISIBLE = "blInitVisible";
        public const string blINITENABLED = "blInitEnabled";
        public const string blUSERCANEDIT = "blUserCanEdit";
        public const string blSELECTIONREQUIRED = "blSelectionRequired";
        public const string intIDCODE = "intIDCode";
        public const string lblHELPLINK = "lblHelpLink";
        public const string strSTOREDPROC = "strStoredProc";
        public const string cssCLASS = "cssClass";

        public const string WEBAPPIDS = "webAppIDs";
        public const string NAME = "Name";

        public const string WEBKVP = "webKVP";
        public const string fkGROUPID = "fkGroupID";
        public const string keyACTUALVALUE = "keyActualValue";
        public const string valDISPLAYEDVALUE = "valDisplayedValue";

        public const string WEBCONTROLTYPES = "webControlTypes";
        public const string DESCRIPTION = "Description";

        public const string WEBKVPGROUPIDDECODER = "webKVPGroupIDDecoder";

        public const string kvpBU = "kvpBU";
        public const string strBUCODE = "strBUCode";
       
        // OTS DATABASE TABLES/COLUMNS
        public const string otsPARTS = "otsParts";
        public const string strPARTNUMBER = "strPartNumber";
        public const string keyREQUESTEDBY = "keyRequestedBy";
        public const string dtREQUESTED = "dtRequested";
        public const string strDESCRIPTION = "strDescription";
        public const string strDESCRIPTION2 = "strDescription2";
        public const string keyTYPE = "keyType";
        public const string keySUBTYPE = "keySubType";
        public const string keyREQUESTEDFORPRODUCT = "keyRequestedForProduct";
        public const string keyPARTSTATUS = "keyPartStatus";
        public const string strPARAMETERS = "strParameters";
        public const string decMAXHEIGHT = "decMaxHeight";
        public const string decLOWVOLCOST = "decLowVolCost";
        public const string decHIGHVOLCOST = "decHighVolCost";
        public const string decENGCOST = "decENGCOST";
        public const string intVERSION = "intVersion";
        public const string keyOTSENVIRONCODE = "keyOTSEnvironCode";

        public const string otsPARTTYPE = "otsPartType";
        public const string strTYPE = "strType";
        public const string strTYPEABBR = "strTypeAbbr";
        public const string strVISIBLEPARAMS = "strVisibleParams";
        public const string strPARAMORDER = "strParamOrder";

        public const string otsPARTSUBTYPE = "otsPartSubType";
        public const string strSUBTYPE = "strSubType";
        public const string strSTYPEABBR = "strSTypeAbbr";

        public const string otsPRODUCT = "otsProduct";
        public const string strPRODUCT = "strProduct";

        public const string otsVENDORPN = "otsVendorPN";
        public const string strVENDORPARTNUMBER = "strVendorPartNumber";
        public const string strDATASHEETURL = "strDatasheetURL";
        public const string keyVENDOR = "keyVendor";
        public const string keyOTSPART = "keyOTSPart";
        public const string keyVENDORENVIRONCODE = "keyVendorEnvironCode";
        //public const string keyPARTSTATUS = "keyPartStatus";

        public const string otsPARTSTATUS = "otsPartStatus";
        public const string strSTATUS = "strStatus";

        public const string otsVENDOR = "otsVendor";
        public const string strVENDOR = "strVendor";
        public const string keyVENDORSTATUS = "keyVendorStatus";
        public const string keyVENDORPARTSTATUS = "keyVendorPartStatus";

        public const string otsENVIRONCODE = "otsEnvironCode";
        public const string intECODE = "intECode";
        public const string strECODENAME = "strECodeName";

        public const string otsVENDORSTATUS = "otsVendorStatus";
        public const string strVENDORSTATUS = "strVendorStatus";

        public const string otsUSERS = "otsUsers";
        public const string keyBU = "keyBU";
        public const string strNAME = "strName";

        public const string otsPNHISTORY = "otsPNHistory";
        public const string keyOTSPNIND = "keyOTSPNID";
        public const string keyUPDATEDBY = "keyUpdatedBy";
        public const string DATE_UPDATED = "Date_Updated";
        public const string DESCRIPTION_CHANGE = "Description_Change";
        public const string DESCRIPTION2_CHANGE = "Description2_Change";
        public const string PART_TYPE_CHANGE = "Part_Type_Change";
        public const string PART_SUBTYPE_CHANGE = "Part_SubType_Change";
        public const string PARAMETER_CHANGE = "Parameter_Change";
        public const string MAX_HEIGHT_CHANGE = "Max_Height_Change";
        public const string LOW_VOL_COST_CHANGE = "Low_Vol_Cost_Change";
        public const string HIGH_VOL_COST_CHANGE = "High_Vol_Cost_Change";
        public const string ENG_COST_CHANGE = "Eng_Cost_Change";
        public const string ENVIRONMENTAL_CODE_CHANGE = "Environmental_Code_Change";
        public const string VENDOR_PART_NUMBER_CHANGE = "Vendor_Part_Number_Change";
        public const string VENDOR_CHANGE = "Vendor_Change";

        public const string otsRELATION = "otsRelation";
        public const string keyVENDORPN = "keyVendorPN";

        //END OTS DATABASE ENTRIES


        //public const string ID = "ID";
        //Stored Procdedures
        //The stored procedure that retrieves the key value pair for drop down boxes from the database
        public const string spGETWEBKEYVALUEPAIRINFO = "spGetWebKeyValuePairInfo";
        public const string spGETKVPBUINFO = "spGetKVPBUInfo";
        //The stored procedure that retrieves the information needed to generate the input fields from the database
        public const string spGETWEBDISPLAYFIELDINFO = "spGetWebDisplayFieldInfo";
        public const string spGETKVPPARTTYPES = "spGetKVPPartTypes";
        public const string spGETKVPPARTSUBTYPES = "spGetKVPPartSubTypes";
        public const string spGETKVPENVIRONCODE = "spGetKVPEnvironCode";
        public const string spGETKVPPARTSTATUS = "spGetKVPPartStatus";
        public const string spGETKVPVENDORSTATUS = "spGetKVPVendorStatus";
        public const string spOTSGETBASEPARTNUMBER = "spOTSGetBasePartNumber";
        public const string spOTSINSERTNEWPARTNUMBER = "spOTSInsertNEwPartNumber";
        public const string spOTSFINDBYBU = "spOTSFindByBU";
        public const string spOTSFINDBYDATE = "spOTSFindByDate";
        public const string spOTSFINDBYDESCRIPTION = "spOTSFindByDescription";
        public const string spOTSFINDBYPARTNUMBER = "spOTSFindByPartNumber";
        public const string spOTSFINDBYPRODUCT = "spOTSFindByProduct";
        public const string spOTSFINDBYREQUESTOR = "spOTSFindByRequestor";
        public const string spOTSFINDBYVENDOR = "spOTSFindByVendor";
        public const string spOTSFINDBYVENDORPARTNUMBER = "spOTSFindByVendorPartNumber";
        public const string spOTSGETPNINFO = "spOTSGetPNInfo";
        public const string spOTSUPDATEPARTSTABLE = "spOTSUpdatePartsTable";
        public const string spOTSUPDATE_ADDVENDORPNTOOTSPARTNUMBER = "spOTSUpdate_AddVendorPartNumberToOTSPartNumber";
        public const string spOTSUPDATEVENDORPNTABLE = "spOTSUpdateVendorPNTable";
        public const string spOTSWHEREUSEDFORVENDORPARTNUMBER = "spOTSWhereUsedForVendorPartNumber";
        public const string spOTSUPDATEPARTSBASEDINAVL = "spOTSUpdatePartsBasedOnAVL";

        //COLUMN ALIAS NAMES Used in Stored Procs
        public class SP_COLALIAS
        {
            public const string PTYPEID = "PTYPEID";
            public const string PSUBTYPEID = "PSUBTYPEID";
            public const string BU = "BU";
            public const string VENDORPNID = "VendorPNID";
        }
        public enum DISPLAYTYPES { UNDEFINED, NONE, BLOCK, INLINE, FILL}

        /// <summary>
        /// Refer to the index of the web page controls for the divOTSNewIn with constants
        /// </summary>
        public class OTSINDK
        {
            //Generic constants
            public const int ID_INDEX = -1;
            //Constants representing the control ID of the parameters a user can supply when requesting an OTSPN
            public const int TYPE = 23;
            public const int SUBTYPE = 13;
            public const int PKG = 14;
            public const int VALUE = 15;
            public const int TOL = 16;
            public const int SIZE = 17;
            public const int MPN = 18;
            public const int VENDOR = 19;
            public const int ENG = 20;
            public const int BU = 21;
            public const int PRODUCT = 22;

            //Constants representing the control ID of the parameters the user wants to search for
            public const int FINDBY_OPTIONS = 24;
            public const int FIND_TEXT = 25;
            public const int FINDBYBU = 26;
            public const int FINDBYREQUESTOR = 27;
            public const int FINDBYDATE = 28;

            //Constants representing the method the user has requested to search; this is taken from webKVP, 
            //and is sent to us through control ID FINDBY_OPTIONS
            public const int SEARCHMETHOD_BYPN = 1;
            public const int SEARCHMETHOD_BYBU = 2;
            public const int SEARCHMETHOD_BYREQUESTOR = 3;
            public const int SEARCHMETHOD_BYVENDOR = 4;
            public const int SEARCHMETHOD_BYVENDORPN = 5;
            public const int SEARCHMETHOD_BYDATE = 6;
            public const int SEARCHMETHOD_BYPRODUCT = 7;
            public const int SEARCHMETHOD_BYDESCRIPTION = 8;

            //Constants representing the Control ID of Part Number characteristics the user wants to edit
            public const int CHANGE_DESC = 37;
            public const int CHANGE_DESC2 = 38;
            public const int CHANGE_PARTTYPE = 44;
            public const int CHANGE_PARTSUBTYPE = 45;

            //Constants representing the Control Id of Vendor Part Number characteristics the user wants to edit
            public const int CHANGE_VENDOR = 0;
            public const int CHANGE_VENDORSTATUS = 1;
            public const int CHANGE_VENDORPN = 2;
            public const int CHANGE_VENDORPNSTATUS = 3;
            public const int CHANGE_VENDORPN_DATASHEET = 4;
            public const int CHANGE_VENDORPN_COST_LOW = 5;
            public const int CHANGE_VENDORPN_COST_HIGH = 6;
            public const int CHANGE_VENDORPN_COST_ENG = 7;
            public const int CHANGE_VENDORPN_HEIGHT = 8;
            public const int CHANGE_VENDORPN_ECODE = 9;
            public const int CHANGE_VENDORPN_WHEREUSED = 10;
        }

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

    public class SESSIONKEYS
    {
        public const string UID = "uid";
    }
}