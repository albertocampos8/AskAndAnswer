using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AskAndAnswer.ClassCode;
using DB;
using System.Data.SqlClient;

namespace AskAndAnswer
{
    public partial class UploadOTS : System.Web.UI.Page
    {
        protected string cssUploadOTS = DynControls.EncodeScript("/stylesheets/OTSUploadStyles.css");
        protected string jsBasicCode = DynControls.EncodeScript("/js/BasicCode.js");
        protected string jsDocReady_CommonBindings = DynControls.EncodeScript("/js/DocReady_CommonBindings.js");
        protected string jsDocReady_UploadOTS = DynControls.EncodeScript("/js/DocReady_UploadOTS.js");



        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.ContentType = "text/event-stream";
            //Response.Expires = -1;
            //Response.ContentType = "text/event-stream";
            //Response.Write("THIS IS A TEST XXXXX");
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //HttpResponse resp = HttpContext.Current.Response;
            //Response.AddHeader("Content-Type", "text/event-stream");
            //Response.AddHeader("Cache-Control", "no-cache");
            //Response.AddHeader("Access-Control-Allow-Origin", "*");
            //Response.AddHeader("Connection", "keep-alive");
                    
            
            clsFileUtil f = new clsFileUtil("");
            string fullServerFileName = "";
            SSERelay resp = new SSERelay(Response);
            List<string> lstFileNames = new List<string>();

            try
            {
                string fn = System.IO.Path.GetFileName(filFileUpload.PostedFile.FileName);
                string fd = Server.MapPath(AAAK.TEMPDATA) + "\\";
                fullServerFileName = fd + "\\" + fn;
                if (filFileUpload.PostedFile != null && filFileUpload.PostedFile.ContentLength > 0)
                {
                    
                    f.MakeDirectory(fd + "\\");
                    //Save the uploaded file
                    filFileUpload.PostedFile.SaveAs(fullServerFileName);

                    
                    //****If zip file, unzip and start loop here****//
                    if (fn.EndsWith(".zip"))
                    {

                    } else
                    {
                        lstFileNames.Add(fn);
                    }

                    clsOTSUploader cOU = new clsOTSUploader();
                    for (int i= 0;i< lstFileNames.Count();i++)
                    {
                        string processResult = cOU.ProcessFile(resp, fd, fn);
                    }
                    
                }
                else
                {
                    resp.send(fn + " is not a legal File.  Please make sure you have selected a valid file.");
                    resp.send("File not processed.");
                }
                
                //Put the stringbuilder in divMsg
                divResult.InnerHtml = resp.GetMessageBlock();
            }

            catch (Exception ex)
            {
                string errMsg = ex.GetType().ToString() + ex.Message + ex.StackTrace;
                resp.send("data: " + errMsg.Replace(AAAK.vbCRLF, DynControls.html_linebreak_string()));
                //divMsg.Controls.Add(new LiteralControl("<p>" + errMsg.Replace(AAAK.vbCRLF, DynControls.html_linebreak_string()) + "</p>"));
            } finally
            {

                try
                {
                    System.IO.FileInfo fl = new System.IO.FileInfo(fullServerFileName);
                    fl.Delete();
                }
                catch (Exception eM)
                {
                    string errM = eM.Message + eM.StackTrace;
                }

            }
        }
    }
}