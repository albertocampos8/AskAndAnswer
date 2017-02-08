using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
namespace AskAndAnswer.ClassCode
{
    public class clsEMail
    {
        SmtpClient m_client = null;

        string m_smtpSever = "";
        public string SMTPServerName
        {
            get { return m_smtpSever; }
        }
        string m_errorMsg = "";
        public string ErrorMessage
        {
            get { return m_errorMsg; }
        }
        public clsEMail(string smtpServer)
        {
            m_smtpSever = smtpServer;
            try
            {
                m_client = new SmtpClient(m_smtpSever);
            } catch (Exception ex)
            {
                string errMsg = ex.Message + ex.StackTrace;
            }
        }

        /// <summary>
        /// Send an email
        /// </summary>
        /// <param name="toAddress">Primary email address</param>
        /// <param name="fromAddress">Email address to which a response will be sent</param>
        /// <param name="fromName">Name displayed as the sender</param>
        /// <param name="subject">Email subject line</param>
        /// <param name="msgBody">Message Body</param>
        /// <param name="addlRecipients">Additional email addresses for the To Line; delimit with semicolon</param>
        /// <param name="ccRecipients">Email addresses for the cc Line; delimit with semicolon</param>
        /// <param name="attachmentPathsAndFiles">list of files to attach to the email; delimit with semicolon</param>
        /// <returns></returns>
        public Boolean Send(string toAddress, string fromAddress, string fromName, string subject, string msgBody,
            string addlRecipients = "", string ccRecipients = "", string attachmentPathsAndFiles = "")
        {
            try
            {
                MailMessage m = new MailMessage(new MailAddress(fromAddress, fromName), new MailAddress(toAddress));
                m.Subject = subject;
                m.Body = msgBody;
                m_client.Send(m);
                return true;
            }
            catch (Exception ex)
            {
                m_errorMsg = ex.Message + ex.StackTrace;
                return false;
            }



        }

    }
}