using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AskAndAnswer.ClassCode;
namespace AskAndAnswer
{
    public class clsLogin
    {
        /// <summary>
        /// Returns the user id in the table by looking in the session object; if not present, gives the log in screen.
        /// </summary>
        public Int64 GetUserID()
        {
            try {
                if (clsUtil.IsNumeric(HttpContext.Current.Session[SESSIONKEYS.UID].ToString())) {
                    return Convert.ToInt64(HttpContext.Current.Session[SESSIONKEYS.UID].ToString());
                } else
                {
                    //Code to get log in will go here; for now, set arbitrary
                    return 1;
                }
            } catch (Exception ex) {
                return 1;
            }
        }
    }
}