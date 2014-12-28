using Eagle.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Eagle.WebApp.Handlers
{
    /// <summary>
    /// Summary description for DownloadFile
    /// </summary>
    public class DownloadFile : IHttpHandler,IRequiresSessionState 
    {

        public void ProcessRequest(HttpContext context)
        {
            string downloadFileURL = Convert.ToString(context.Request.QueryString["file"]);
            try
            {           
                if (!string.IsNullOrEmpty(downloadFileURL))
                {
                    string physicalFilePath = context.Server.MapPath(downloadFileURL);
                    string fileName = System.IO.Path.GetFileName(physicalFilePath);

                    // Check to see if file exist
                    FileInfo fileInfo = new FileInfo(physicalFilePath);
                    if (!fileInfo.Exists)
                    {
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("<span class=\"unlinked\">" + downloadFileURL + " link doesn't exist!</script>");                        
                    }
                    else
                    {
                        HttpContext.Current.Response.ClearHeaders();
                        HttpContext.Current.Response.ClearContent();
                        HttpContext.Current.Response.AppendHeader("Content-Length", fileInfo.Length.ToString());
                        HttpContext.Current.Response.ContentType = "application/octet-stream";
                        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                        // HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(fileInfo.FullName));
                        //context.Response.TransmitFile(fileInfo.FullName);
                        context.Response.WriteFile(fileInfo.FullName);
                        //HttpContext.Current.Response.End();
                        context.Response.Flush();
                    }
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("<span class=\"unlinked\">File cannot be found!</span>");
                }
            } catch (Exception ex) { 
                context.Response.ContentType = "text/plain"; 
                context.Response.Write(ex.Message); 
            } 
            finally { context.Response.End(); }
        }

        private byte[] ReadByteArryFromFile(string destPath)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(destPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(destPath).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}