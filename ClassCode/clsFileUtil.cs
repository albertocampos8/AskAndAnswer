using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace AskAndAnswer.ClassCode
{
    public class clsFileUtil
    {
        private string m_errMsg = "";
           public String ErrorMsg
        {
            get
            {
                return m_errMsg;
            }
        }
        string m_filePath = "";
        public clsFileUtil(string FilePath)
        {
            m_filePath = FilePath;
        }
        public StreamReader OpenAndRead()
        {
            return new StreamReader(m_filePath);
        }
        public Boolean Delete()
        {
            try
            {
                File.Delete(m_filePath);
                return true;
            } catch (Exception ex)
            {
                m_errMsg = ex.Message + AAAK.vbCRLF + ex.StackTrace;
                return false;
            }
        }
    }
}