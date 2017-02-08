using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
namespace AskAndAnswer.ClassCode
{

    
    public class SSERelay
    {

        HttpResponse m_r = null;
        StringBuilder m_SB = new StringBuilder();

        public string GetMessageBlock()
        {
            return m_SB.ToString();
        }

        public SSERelay(HttpResponse r)
        {
            {
                m_r = r;
            }
        }

        /// <summary>
        /// Puts the message into this object's stringbuilder.
        /// </summary>
        /// <param name="msg">The message to log</param>
        /// <param name="includeDate">et TRUE to put date string before payload</param>
        /// <param name="encloseInP"></param>
        /// <param name="endWithLinBreak"></param>
        public void log(string msg, Boolean includeDate = true, 
            Boolean encloseInP = true, Boolean endWithLinBreak = false)
        {
            try
            {
                if (includeDate)
                {
                    msg = DateTime.UtcNow.ToString() + ": " + msg;
                }
                if (endWithLinBreak)
                {
                    msg = msg + DynControls.html_linebreak_string();
                }
                if (encloseInP)
                {
                    msg = "<p>" + msg + "</p>";
                }
                m_SB.Insert(0, msg);
            } catch (Exception ex)
            {
                m_SB.Append(ex.Message + DynControls.html_linebreak_string() +
                    ex.StackTrace.Replace(AAAK.vbCRLF, DynControls.html_linebreak_string()));
            }
        } 
        /// <summary>
        /// Sends an event string for server-side events
        /// </summary>
        /// <param name="msg">Payload for a data event (the message)</param>
        /// <param name="eventName">Payload for an event event (the event Name)</param>
        /// <param name="eventId">Payload for an id even (the event ID number)</param>
        /// <param name="includeDate">Set TRUE to put date string before payload</param>
        /// <param name="endData">When TRUE, data events terminate with "\n\n", which signals the event is over.</param>
        /// <param name="flush">Set TRUE to sennd the contents in the response stream immediately</param>
        public void send(string msg = "", string eventName = "", string eventId = "",
            Boolean includeDate = true, Boolean endData = true, Boolean flush = true)
        {
            //Work around until I figure out how to handle SSE correctly
            log(msg, includeDate, true, false);
            return;
            try
            {
                if (eventId != "")
                {
                    m_r.Write("id: " + eventId + "\n");
                }
                if (eventName != "")
                {
                    m_r.Write("event: " + eventName + "\n");
                }
                if (msg != "")
                {
                    if (includeDate)
                    {
                        m_r.Write("data: " + DateTime.UtcNow.ToString() + "\n");
                    }
                    string terminator = "";
                    if (endData)
                    {
                        terminator = "\n";
                    }
                    m_r.Write("data: " + msg + "\n" + terminator);
                    if (flush)
                    {
                        m_r.Flush();
                    }
                }
            } catch (Exception ex)
            {
                m_r.Write(ex.Message + DynControls.html_linebreak_string() +
                    ex.StackTrace.Replace(AAAK.vbCRLF, DynControls.html_linebreak_string()));
                m_r.Flush();
            }
        }
    }
}