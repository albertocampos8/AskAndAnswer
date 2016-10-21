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
        public const string blSELECTIONREQUIRED = "blSelectionRequired";
        public const string intIDCODE = "intIDCode";

        public const string WEBAPPIDS = "webAppIDs";
        public const string NAME = "Name";

        public const string WEBKVP = "webKVP";
        public const string fkGROUPID = "fkGroupID";
        public const string keyACTUALVALUE = "keyActualValue";
        public const string valDisplayedValue = "valDisplayedValue";

        public const string WEBCONTROLTYPES = "webControlTypes";
        public const string DESCRIPTION = "Description";

        public const string WEBKVPGROUPIDDECODER = "webKVPGroupIDDecoder";
        //Stored Procdedures
        //The stored procedure that retrieves the key value pair for drop down boxes from the database
        public const string spGET_WEB_KEYVALUEPAIR_INFO = "spGetWebKeyValuePairInfo";
        //The stored procedure that retrieves the information needed to generate the input fields from the database
        public const string spGET_WEB_DISPLAYFIELD_INFO = "spGetWebDisplayFieldInfo";

        public enum DISPLAYTYPES { UNDEFINED, NONE, BLOCK, INLINE}
    }
}