using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using DB;
namespace AskAndAnswer
{
    /// <summary>
    /// Special class that uses a lock to prevent contention between two users trying to access a resource at the
    /// same time
    /// </summary>
    public class clsSynch
    {
        private  object lockOTSobj = new object();
        /// <summary>
        /// Calls stored procedure spOTSGetBasePartNumber to get the next available number from table
        /// otsSeed; this number is used to form the OTS Part Number.
        /// </summary>
        /// <returns></returns>
        public int GetOTSBasePartNumber()
        {
            
            try
            {
                clsDB myDB = new clsDB();
                SqlParameter p = myDB.makeOutputParameter("@basePartNumber", System.Data.SqlDbType.Int);
                SqlCommand cmd = new SqlCommand();
                List<SqlParameter> ps = new List<SqlParameter>();
                ps.Add(p);

                using (myDB.OpenConnection())
                {
                    lock(lockOTSobj)
                    {
                        myDB.ExecuteSP(DBK.SP.spOTSGETBASEPARTNUMBER, ps, clsDB.SPExMode.NONQUERY, ref cmd);
                    }
                    return Convert.ToInt32(cmd.Parameters[DBK.SPVar.basePartNumber].Value);

                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}