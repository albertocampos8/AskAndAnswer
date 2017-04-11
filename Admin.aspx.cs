using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AskAndAnswer.ClassCode;
using DB;
using System.Text;
using System.Data.SqlClient;
namespace AskAndAnswer
{
    public partial class Admin : System.Web.UI.Page
    {
        protected string cssAdminStyles = DynControls.EncodeScript("/stylesheets/AdminStyles.css");
        protected string jsBasicCode = DynControls.EncodeScript("/js/BasicCode.js");
        protected string jsDocReady_CommonBindings = DynControls.EncodeScript("/js/DocReady_CommonBindings.js");
        protected string jsDocReady_Admin = DynControls.EncodeScript("/js/DocReady_Admin.js");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string registerLocation(string input)
        {
            try
            {
                CustomCode x = new CustomCode();
                return x.registerLocation(input);
            }
            catch (Exception ex)
            {
                return  ex.Message + ex.StackTrace;

            }
        }

        [System.Web.Services.WebMethod]
        public static string autoCompleteAddress(string input)
        {
            try
            {
                CustomCode x = new CustomCode();
                return x.autoCompleteAddress(input);
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return "";
            }
        }

        [System.Web.Services.WebMethod]
        public static string[] GetStreetAddress(string term)
        {
            try
            {
                clsUtil u = new clsUtil();
                List<SqlParameter> ps = new List<SqlParameter>();

                string[] x = u.GetJSONFromDB(DBK.SP.spAC_LOCADDRESS, "%" + term + "%", DBK.strADDRESS);
                return x;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return new string[0];
            }
        }

        [System.Web.Services.WebMethod]
        public static string[] GetCity(string term)
        {
            try
            {
                clsUtil u = new clsUtil();
                List<SqlParameter> ps = new List<SqlParameter>();

                string[] x = u.GetJSONFromDB(DBK.SP.spAC_LOCCITY, "%" + term + "%", DBK.strCITY);
                return x;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return new string[0];
            }
        }

        [System.Web.Services.WebMethod]
        public static string[] GetStateProvince(string term)
        {
            try
            {
                clsUtil u = new clsUtil();
                List<SqlParameter> ps = new List<SqlParameter>();

                string[] x = u.GetJSONFromDB(DBK.SP.spAC_LOCSTATEPROVINCE, "%" + term + "%", DBK.strSTATEPROVINCE);
                return x;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return new string[0];
            }
        }

        [System.Web.Services.WebMethod]
        public static string[] GetPostalCode(string term)
        {
            try
            {
                clsUtil u = new clsUtil();
                List<SqlParameter> ps = new List<SqlParameter>();

                string[] x = u.GetJSONFromDB(DBK.SP.spAC_LOCPOSTALCODE, "%" + term + "%", DBK.strPOSTALCODE);
                return x;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return new string[0];
            }
        }

        [System.Web.Services.WebMethod]
        public static string[] GetCountry(string term)
        {
            try
            {
                clsUtil u = new clsUtil();
                List<SqlParameter> ps = new List<SqlParameter>();

                string[] x = u.GetJSONFromDB(DBK.SP.spAC_LOCCOUNTRY, "%" + term + "%", DBK.strCOUNTRY);
                return x;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return new string[0];
            }
        }

        [System.Web.Services.WebMethod]
        ///Returns list of all addresses in the database
        public static string getAddressList(string input)
        {
            try
            {
                clsDB myDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                StringBuilder sB = new StringBuilder();
                using (myDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(DBK.SP.spGETKVPFULLADDRESS, new List<SqlParameter>(), clsDB.SPExMode.READER, ref cmd))
                    {
                        if (dR != null && dR.HasRows) {
                            while (dR.Read())
                            {
                                sB.Append("<p>" + myDB.Fld2Str(dR[DBK.valDISPLAYEDVALUE]) + "</p>");
                            }
                        }
                        else
                        {
                            sB.Append("<p>No addresses found when executing stored procedure " + DBK.SP.spGETKVPFULLADDRESS + " in WebMethod getAddressList.</p>");
                        }
                    }
                }
                return sB.ToString();

            } catch (Exception ex)
            {
                return (ex.Message + ex.StackTrace).Replace(AAAK.vbCRLF,DynControls.html_linebreak_string());
            }
        }

    }
}