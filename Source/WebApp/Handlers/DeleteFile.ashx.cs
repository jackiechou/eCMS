using Eagle.Repository;
using Eagle.Repository.SYS.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Eagle.WebApp.Handlers
{
    /// <summary>
    /// Summary description for DeleteFile
    /// </summary>
    public class DeleteFile :IHttpHandler, IRequiresSessionState
    { 
        int? fileId = 0;
        bool result = false;    
                 
        public void ProcessRequest(HttpContext context)
        {
            string message = string.Empty;
            if (context.Request.Params["fileId"] != null && context.Request.QueryString["fileId"] != "")
                fileId = Convert.ToInt32(context.Request.Params["fileId"]);

            if (fileId != null && fileId> 0)
                result = FileRepository.Delete(fileId, out message);

            context.Response.ContentType = "text/plain";
            context.Response.Write(result);
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