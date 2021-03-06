﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB;
using System.Data.SqlClient;
namespace AskAndAnswer.ClassCode
{
    public class clsUtil
    {

        private object LCK = new object();

        /// <summary>
        /// Returns true if x is numeric (convert to int or float), false otherwise
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Boolean IsNumeric(string x)
        {
            int y;
            try
            {
                int a = 0;
                if (int.TryParse(x, out a))
                {
                    return true;
                }

                float b = 0;
                if (float.TryParse(x, out b))
                {
                    return true;
                }
                return false;
            } catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if x is an integer (only consists of numbers 0 through 9)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Boolean IsInteger(string x)
        {
            try
            {
                if (x.All(Char.IsDigit))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Reads a list of keyvalue pairs from the Application object, based on appKey.
        /// If there is no List defined in the app object for appKey, then executes a stored proc to get the keyvalue pairs from the database
        /// store it as a list, and put that list in the App Object, so that we don't have to call the SP next time we want
        /// to use the list.
        /// 
        /// To preserve avoid collisions with the objects in the AppObject, this method uses a lock object.
        /// </summary>
        /// <param name="appKey">The App Key used to retrieve the list from the Application Object. There are two formats,
        /// depending on how the list was generated:
        /// 1) kvpl_[ID], where ID is the value of fkGroupID in table webKVP.  This allows the method to execute stored
        /// procedure spGetWebKeyValuePairInfo.  You will use this if your key-value pairs are stored in the table.
        /// 2) kvpl_[STORED PROCEDURE NAME].  Use this if a stored procedure exists in the database to retrieve the data.</param>
        /// <returns></returns>
        public  List<string> PutKVPInDictionary(string appKey)
        {
            try
            {
                //See if the kvp is already stored in the App Object
                List<string> lstKVPs = new List<string>();
                lock(LCK)
                {
                    lstKVPs = (List<string>)HttpContext.Current.Application[appKey];
                    if (lstKVPs == null)
                    {
                        clsDB xDB = new clsDB();
                        string appKeySuffix = appKey.Split('_')[1];
                        if (clsUtil.IsNumeric(appKeySuffix))
                        {
                            xDB.PutKVPInDictionary(Convert.ToInt64(appKeySuffix));
                        }
                        else
                        {
                            xDB.PutKVPInDictionary(appKeySuffix);
                        }
                        return (List<string>)HttpContext.Current.Application[appKey];
                    }
                    else
                    {
                        return lstKVPs;
                    }
                }

            }
            catch (Exception ex)
            {
                //just return an empty list.
                return new List<string>();
            }

        }

        /// <summary>
        /// Returns a JSON string based on the contents of the database and user input
        /// </summary>
        /// <param name="sProcName">the name of the stored proc to invoke.</param>
        /// <param name="filterValue">The value to feed to the stored proc</param>
        /// <param name="fieldNameForDisplayedValue">The name of the field to read from the datareader; 
        /// This value is what will be displayed in the auto-complete box.  This is the column whose matches
        /// you are expecting from the datatable.
        /// If you only provide this value, you get:
        /// ["value_0",...,"value_N-1"]</param>
        /// <param name="fieldNameForInsertedValue">Optional.  The name of the field to read from the datareader;
        /// This value is what will be inserted into the auto-complete box when you select fieldNameForDisplayedValue.
        /// When your provide this value, you get:
        /// [
        /// {label:"dR[fieldNameForDisplayedValue]_0", value:"dR[fieldNameForInsertedValue]_0"},
        /// {label:"dR[fieldNameForDisplayedValue]_N-1", value:"dR[fieldNameForInsertedValue]_N-1"},
        /// ]</param>
        /// <param name="ps">List of sql parameters required by the stored procedure.
        /// If you use this parameter, you are responsible for providing all parameters required by the stored procedure,
        /// including @filter.  In this scenario, parameters filterValue and pName are ignored.</param>
        /// <param name="pName">Parameter name; default is @filter</param>
        /// <returns></returns>
        public string[] GetJSONFromDB(string sProcName, string filterValue,
            string fieldNameForDisplayedValue, 
            string fieldNameForInsertedValue = "",
            string pName="@filter",
            List<SqlParameter> ps = null)
        {
            try
            {
                clsDB xdB = new clsDB();
                SqlCommand cmd = new SqlCommand();
                if (ps == null)
                    {
                        ps = new List<SqlParameter>();
                        ps.Add(new SqlParameter("@filter", filterValue));
                    }
                
                List <string> lstResult = new List<string>();
                using (xdB.OpenConnection())
                {
                    using (SqlDataReader dR = (SqlDataReader)xdB.ExecuteSP(sProcName,ps,clsDB.SPExMode.READER,ref cmd))
                    {
                        if (dR != null && dR.HasRows)
                        {
                            while (dR.Read())
                            {
                                if (fieldNameForInsertedValue=="")
                                {
                                    lstResult.Add(xdB.Fld2Str(dR[fieldNameForDisplayedValue]));
                                } else
                                {
                                    lstResult.Add("{label:" + 
                                        xdB.Fld2Str(dR[fieldNameForDisplayedValue]) +
                                        ", value:" +
                                        xdB.Fld2Str(dR[fieldNameForInsertedValue]) +
                                        "}");
                                }
                                
                            }
                        }
                    }
                }

                if (lstResult.Count>0)
                {
                    return lstResult.ToArray();
                } else
                {
                    return new string[0];
                }

            } catch (Exception ex)
            {
                string strErr = ex.Message + ex.StackTrace;
                return new string[0];
            }
        }
        
        /// <summary>
        /// Looks for searchVal in d; if not found, looks in database.  If found, records value in d; if not, returns default value
        /// </summary>
        /// <param name="d">Dictionary that we search for first.  </param>
        /// <param name="searchVal">The value (key) in the dictionary we are searching for; if found, we get d(key)</param>
        /// <param name="spName">The stored proc to use if searchVal is not in the dictionary</param>
        /// <param name="fldName_key">The database field name to use as the dictionary key</param>
        /// <param name="fldName_value">The database field name to use as the dictionary value</param>
        /// <param name="defaultValue">The default value to return if not searchVal not found in either the dictioanry 
        /// or the database</param>
        /// <param name="spParamName">Name of the parameter in the stored procedure.  Leave this blank, and the function will use
        /// @[fldName_key]</param>
        /// <param name="putDefaultInDictionary">If not found in the dictionary or in the database, then if this set TRUE, searchVal
        /// is put in the database with the default value, so that you don't query the database for the same searchVal next time this
        /// function is called</param>
        /// <returns></returns>
        public string GetDBValueFromDictionary(ref Dictionary<string, string> d,
                                        string searchVal,
                                        string spName,
                                        string fldName_key,
                                        string fldName_value,
                                        string defaultValue,
                                        string spParamName = "",
                                        Boolean putDefaultInDictionary = true)
        {
            try
            {
                if (d.ContainsKey(searchVal))
                {
                    return d[searchVal];
                } else
                {
                    clsDB xDB = new clsDB();
                    List<SqlParameter> ps = new List<SqlParameter>();
                    SqlCommand cmd = new SqlCommand();
                    if (spParamName == "")
                    {
                        spParamName = "@" + fldName_key;
                    }
                    ps.Add(new SqlParameter(spParamName, searchVal));
                    using (xDB.OpenConnection())
                    {
                        using (SqlDataReader dR = (SqlDataReader)xDB.ExecuteSP(spName,ps,clsDB.SPExMode.READER, ref cmd))
                        {
                            if (dR!=null && dR.HasRows)
                            {
                                dR.Read();
                                string foundVal = xDB.Fld2Str(dR[fldName_value]);
                                d.Add(searchVal, foundVal);
                                return foundVal;
                            }
                        }
                    }
                }
                //if we made it this far, we should return the default value. BUT:
                if (putDefaultInDictionary)
                {
                    d.Add(searchVal, defaultValue);
                }
                return defaultValue;

            } catch (Exception ex)
            {
                return defaultValue;
            }
        }

    }
}