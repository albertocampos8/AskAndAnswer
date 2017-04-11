using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using DB;
using System.Text;
using AskAndAnswer.ClassCode;
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

        private object lockUpdateInvObj = new object();
        /// <summary>
        /// Updates invBulk with user information in input
        /// </summary>
        /// <param name="input">FORMAT:
        /// [comment]DELIM[invBulk.ID]DELIM[EXISTING QTY]DELIM[DELTA]DELIM[SubInv]DELIM[LocationID]DELIM[OwnerID]DELIM[VPNID]DELIM[TransactionType ID]......</param>
        /// <returns></returns>
        public string UpdatePartInventory(string input, string[] m_dlim)
        {
            StringBuilder sB = new StringBuilder();
            try
            {
                string[] arr = input.Split(m_dlim, StringSplitOptions.None);
                string cmt = arr[0].ToUpper();
                lock (lockUpdateInvObj)
                {
                    for (int i = 1; i < arr.Length; i = i + 8)
                    {
                        clsDB myDB = new clsDB();
                        SqlCommand cmd = new SqlCommand();
                        List<SqlParameter> ps = new List<SqlParameter>();
                        string spName = "";
                        string ID = arr[i];
                        string Qty = arr[i + 1];
                        string Delta = arr[i + 2];
                        string SubInv = arr[i + 3].ToUpper();
                        string Loc = arr[i + 4];
                        string Owner = arr[i + 5];
                        string VPNID = arr[i + 6];
                        string TransTypeID = arr[i + 7];

                        int oldQty = 0;
                        int oldLoc = -1;
                        Int64 oldOwner = -1;
                        string oldSubInv = "";
                        int oldKeySubInv = -1;
                        //Get the current subinv, location, qty, and owner for the given ID
                        ps.Add(new SqlParameter("@" + DBK.ID, Int64.Parse(ID)));
                        using (myDB.OpenConnection())
                        {
                            using (SqlDataReader dR = (SqlDataReader)myDB.ExecuteSP(DBK.SP.spINVGETINFOFORINVBULKID, ps, clsDB.SPExMode.READER, ref cmd))
                            {
                                if (dR != null && dR.HasRows)
                                {
                                    dR.Read();
                                    oldQty = (int)dR[DBK.intQTY];
                                    oldLoc = (int)dR[DBK.keyLOCATIONBULK];
                                    oldOwner = (Int64)dR[DBK.keyOWNER];
                                    oldKeySubInv = (int)dR[DBK.keySUBINV];
                                    oldSubInv = (string)dR[DBK.strSUBINV];
                                }
                            }
                        }

                        //Reset for next usage
                        ps.Clear();

                        if (Qty == "-1" && Int64.Parse(ID) > -1)
                        {
                            //User wants to remove this entry from invBulk
                            ps.Add(new SqlParameter("@" + DBK.keyBULKITEM, VPNID));
                            ps.Add(new SqlParameter("@" + DBK.keyCHANGEDBY, AAAK.CHANGEDBY));
                            ps.Add(new SqlParameter("@" + DBK.intDELTA, -oldQty));
                            ps.Add(new SqlParameter("@" + DBK.strCOMMENT, cmt));
                            ps.Add(new SqlParameter("@" + DBK.keyTRANSACTIONTYPE, 1));
                            ps.Add(new SqlParameter("@" + DBK.keyLOCATIONBULK, oldLoc));
                            ps.Add(new SqlParameter("@" + DBK.keyOWNER, oldOwner));
                            ps.Add(new SqlParameter("@" + DBK.keySUBINV, oldKeySubInv));
                            ps.Add(new SqlParameter("@" + DBK.ID, ID));
                            spName = DBK.SP.spINVREMOVEBULKINVENTRY;
                        }
                        else if (!clsUtil.IsInteger(Qty))
                        {
                            //Ignore this value, but alert user via email
                        }
                        else if (Qty != "" && Int64.Parse(Loc) > -1 && Int64.Parse(Owner) > -1)
                        {
                            //Update Qty based on Delta; note the client has already determined if there is a minus or not in front of delta, so
                            int newQty = int.Parse(Qty) + int.Parse(Delta);

                            //Only continue if there is a change in the data
                            if (oldLoc != int.Parse(Loc) || oldOwner != Int64.Parse(Owner) || oldQty != newQty || oldSubInv != SubInv)
                            {
                                //User wants to make a new/update an existing entry
                                ps.Add(new SqlParameter("@" + DBK.keyBULKITEM, VPNID));
                                ps.Add(new SqlParameter("@" + DBK.keyCHANGEDBY, AAAK.CHANGEDBY));
                                ps.Add(new SqlParameter("@" + DBK.intDELTA, Delta));
                                ps.Add(new SqlParameter("@" + DBK.strCOMMENT, cmt));
                                ps.Add(new SqlParameter("@" + DBK.keyTRANSACTIONTYPE, TransTypeID));
                                ps.Add(new SqlParameter("@" + DBK.keyLOCATIONBULK, Loc));
                                ps.Add(new SqlParameter("@" + DBK.keyOWNER, Owner));
                                ps.Add(new SqlParameter("@" + DBK.intQTY, newQty));
                                ps.Add(new SqlParameter("@" + DBK.ID, ID));
                                ps.Add(new SqlParameter("@" + DBK.strSUBINV, SubInv));
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
                }

                return sB.ToString().Replace(AAAK.vbCRLF, DynControls.html_linebreak_string());
            }
            catch (Exception ex)
            {
                return (ex.Message + AAAK.vbCRLF + ex.StackTrace).Replace(AAAK.vbCRLF, DynControls.html_linebreak_string());
            }
        }
    }
}