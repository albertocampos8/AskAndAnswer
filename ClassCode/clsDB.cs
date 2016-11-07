using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using AskAndAnswer;

namespace DB
{
    /// <summary>
    /// Summary description for clsDB
    /// This object uses the database connection defined in ConfigurationManager.ConnectionStrings[clsK.defaultConnStr].ConnectionString;
    /// </summary>
    public class clsDB
    {
        private SqlConnection m_c = new SqlConnection();

        public enum SPExMode
        {
            READER,
            SCALAR,
            NONQUERY
        }

        //Class variables
        //The connection string used in the project's Web.config file; used to connect to the Help Database
        private string m_connStr = ConfigurationManager.ConnectionStrings[AAAK.defaultConnStr].ConnectionString;


        /// <summary>
        /// Instatiate an instance of this object.
        /// </summary>
        /// <param name="newConnStr">Override the default value of this connection string, which is defined in clsK.defaultConnStr</param>
        public clsDB(string newConnStr = "")
        {
            if (newConnStr != "")
            {
                m_connStr = newConnStr;
            }
        }

        public SqlConnection getC()
        {
            return new SqlConnection(m_connStr);
        }

        public SqlConnection OpenConnection()
        {
            m_c = new SqlConnection(m_connStr);
            m_c.Open();
            return m_c;
        }
        public void CloseConnection()
        {
            m_c.Close();
        }

        /// <summary>
        /// Tests a connection to the database that uses connection string stored in m_connStr
        /// </summary>
        /// <returns></returns>
        public bool TestConnection()
        {
            try
            {
                using (var c = getC())
                {
                    c.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Converts the database object into a string; 
        /// If object is null, return ""
        /// </summary>
        /// <param name="dbVal"></param>
        /// <param name="valIfNull">Value to return if null, e.g., N/A.  Returns "" if unspecified</param>
        /// <returns></returns>
        public string Fld2Str(object dbVal, string valIfNull = "")
        {
            try
            {
                if (dbVal != DBNull.Value)
                {
                    return Convert.ToString(dbVal);
                } else
                {
                    return valIfNull;
                }
            } catch (Exception ex)
            {
                return "Error in Method Fld2str for " + dbVal.ToString() + " " + ex.Message;
            }
        }

        /// <summary>
        /// Returns a DBNull object for an empty string.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public object EmptyStr2Null(string val)
        {
            try
            {
                if (val == "")
                {
                    return DBNull.Value;
                } else
                {
                    return val;
                }
            }
            catch (Exception ex)
            {
                return "Error in Method Fld2str for " + val + " " + ex.Message;
            }
        }
        /// <summary>
        /// Returns the result of a Stored Procedure; you must make sure the connection is open before calling this function
        /// </summary>
        /// <param name="storedProcName">Nameof stored procedure</param>
        /// <param name="ps">Collection of Parameters required by the Stored Procuedure.</param>
        /// <param name="executionMode">Execution mode of Stored Procedure.  This determines the type of object returned.</param>
        /// <returns></returns>
        public Object ExecuteSP(string storedProcName,
                                List<SqlParameter> ps,
                                SPExMode executionMode,
                                ref SqlCommand generatedSQLcmd)
        {
            try
            {
                //Create the command object
                SqlCommand cmd = new SqlCommand(storedProcName, m_c);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add parameters from user-supplied list of parameters ps to the command
                if (ps != null)
                {
                    foreach (SqlParameter p in ps)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
 
                generatedSQLcmd = cmd;
                //Open the connection

                    switch (executionMode)
                    {
                        case SPExMode.NONQUERY:
                            int y = (int)cmd.ExecuteNonQuery();
                            //cmd.ExecuteNonQuery();
                            return y;
                        case SPExMode.READER:
                            SqlDataReader dR = cmd.ExecuteReader();
                            return dR;
                        case SPExMode.SCALAR:
                            return cmd.ExecuteScalar();
                    }

                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Converts a column name into an SQL Server Parameter name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string makeParameterName(string name)
        {
            return "@" + name;
        }

        /// <summary>
        /// Return an SQL output parameter
        /// </summary>
        /// <param name="parameterName">Name of parameter; you must supply "@"</param>
        /// <param name="dType">Data Type; use System.Data.SqlDbType type</param>
        /// <param name="size">Size of parameter; default is 1000000000</param>
        /// <returns></returns>
        public SqlParameter makeOutputParameter(string parameterName, SqlDbType dType, int size = 1000000000)
        {
            try
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = parameterName;
                p.Size = size;
                p.Direction = ParameterDirection.Output;
                return p;
            } catch (Exception ex)
            {
                return new SqlParameter();
            }
        }

        public SqlParameter makeReturnParameter(SqlDbType dType, string parameterName = "@returnVal")
        {
            try
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = parameterName;
                p.Direction = ParameterDirection.ReturnValue;
                return p;
            }
            catch (Exception ex)
            {
                return new SqlParameter();
            }
        }

        /// <summary>
        /// Converts an object from a database to a string.
        /// If the object is Null, return an empty string ("")
        /// </summary>
        /// <param name="dbVal"></param>
        /// <returns></returns>

        /// <summary>
        /// Puts the key value pair with fkAppID = appID that is defined in webKVP into the application object.
        /// To access the dictionary, use key "kvpd_[APPID]"
        /// To access the list, use key "kvpl_[APPID]"
        /// </summary>
        /// <param name="appID"></param>
        public void PutKVPInDictionary(Int64 appID)
        {
            try
            {
                Dictionary<string, string> d = new Dictionary<String, string>();
                List<string> l = new List<string>();
                clsDB myDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@" + DBK.fkGROUPID, appID));
                using (myDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(DBK.SP.spGETWEBKEYVALUEPAIRINFO, ps,
                        clsDB.SPExMode.READER, ref cmd))
                    {
                        if (dR != null && dR.HasRows)
                        {
                            while (dR.Read())
                            {
                                d.Add(Convert.ToString(dR[DBK.keyACTUALVALUE]), Convert.ToString(dR[DBK.valDISPLAYEDVALUE]));
                                l.Add(Convert.ToString(dR[DBK.keyACTUALVALUE]));
                                l.Add(Convert.ToString(dR[DBK.valDISPLAYEDVALUE]));
                            }

                        }
                    }
                }


                HttpContext.Current.Application["kvpd_" + Convert.ToString(appID)] = d;
                HttpContext.Current.Application["kvpl_" + Convert.ToString(appID)] = l;
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
        }

        /// <summary>
        /// Puts the key value pair that you obtain from a storedProcedure into the application object.
        /// To access the dictionary, use key "kvpd_[spName]"
        /// To access the list, use key "kvpl_[spName]"
        /// </summary>
        /// <param name="appID"></param>
        public void PutKVPInDictionary(string spName)
        {
            try
            {
                Dictionary<string, string> d = new Dictionary<String, string>();
                List<string> l = new List<string>();
                clsDB myDB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                using (myDB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(spName, null,
                        clsDB.SPExMode.READER, ref cmd))
                    {
                        if (dR != null && dR.HasRows)
                        {
                            while (dR.Read())
                            {
                                d.Add(Convert.ToString(dR[DBK.keyACTUALVALUE]), Convert.ToString(dR[DBK.valDISPLAYEDVALUE]));
                                l.Add(Convert.ToString(dR[DBK.keyACTUALVALUE]));
                                l.Add(Convert.ToString(dR[DBK.valDISPLAYEDVALUE]));
                            }

                        }
                    }
                }


                HttpContext.Current.Application["kvpd_" + spName] = d;
                HttpContext.Current.Application["kvpl_" + spName] = l;
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
        }
    }
}
