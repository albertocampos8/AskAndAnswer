using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using DB;

namespace AskAndAnswer
{
    //Code for maintaining the application object
    public class AppCode
    {
        /// <summary>
        /// Calls all the methods in this class to resynch with whatever has been defined(or updated) in the database
        /// </summary>
        public static void UpdateAppObject()
        {
            try
            {
                UpdateBUIDInfo();
            }
            catch (Exception ex)
            {
                string e = ex.Message;
            }

        }


        /// <summary>
        /// Maps: BUID to the Code used in that BU's part numbers, as well as to that BU's Name
        /// </summary>
        public static void UpdateBUIDInfo()
        {
            Dictionary<string, string> BUIDToBUPNCode = new Dictionary<string, string>();
            Dictionary<string, string> BUIDToBUName = new Dictionary<string, string>();
            //Initialize some global values to store in the Application Object
            //ID to Part Type gets stored in dctIDToPARTTYPE
            clsDB myDB = new clsDB();
            SqlCommand sqlcmd = new SqlCommand();
            List<string> lstBU = new List<string>();
            using (myDB.OpenConnection())
            {
                using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(DBK.SP.spGETKVPBUINFO, null, clsDB.SPExMode.READER, ref sqlcmd))
                {
                    if (dR != null && dR.HasRows)
                    {
                        while (dR.Read())
                        {
                            BUIDToBUPNCode.Add((string)dR[DBK.keyACTUALVALUE], (string)dR[DBK.strBUCODE]);
                            BUIDToBUName.Add((string)dR[DBK.keyACTUALVALUE], (string)dR[DBK.valDISPLAYEDVALUE]);
                            lstBU.Add((Convert.ToString(dR[DBK.keyACTUALVALUE])));
                            lstBU.Add((Convert.ToString(dR[DBK.valDISPLAYEDVALUE])));
                        }

                    }
                }
            }
            HttpContext.Current.Application[K.BUIDToBUPNCode] = BUIDToBUPNCode;
            HttpContext.Current.Application[K.BUIDToBUName] = BUIDToBUName;
            HttpContext.Current.Application["kvpl_" + DBK.SP.spGETKVPBUINFO] = lstBU;
            HttpContext.Current.Application["kvpd_" + DBK.SP.spGETKVPBUINFO] = BUIDToBUName;
        }


        /// <summary>
        /// Defines the constants used as the index for the application object
        /// </summary>
        public class K
        {
            public static string BUIDToBUPNCode = "BUIDToBUPNCode";
            public static string BUIDToBUName = "BUIDToBUName";
            public static string BULIST = "BUList"; //Use this when you need a list of BUs to feed to DynControls.html_combobox
            
        }
    }

}