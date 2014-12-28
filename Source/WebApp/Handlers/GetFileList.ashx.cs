using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eagle.Model.SYS;
using Eagle.Repository.SYS.FileManager;

namespace Eagle.WebApp.Handlers
{
    /// <summary>
    /// Summary description for GetFileList
    /// </summary>
    public class GetFileList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string FileIdList = context.Request.QueryString["FileIds"];
                string result = FileRepository.GetList(FileIdList);
                context.Response.ContentType = "text/plain";
                context.Response.Write(result);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(ex.Message);
            }
            finally { context.Response.End(); }
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