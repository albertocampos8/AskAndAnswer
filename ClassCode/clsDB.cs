using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
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

    }
}
