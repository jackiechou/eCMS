using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Eagle.Model.SYS;
using Eagle.Repository.SYS.FileManager;

namespace Eagle.WebApp.Handlers
{
    /// <summary>
    /// Summary description for ViewFileContent
    /// </summary>
    public class ViewFileContent : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //try
            //{
            string contentType = string.Empty, folderPath = string.Empty, physicalFolderPath = string.Empty, physicalFilePath = string.Empty, filePath = string.Empty, fileNameWithoutExtension = string.Empty;
            byte[] fileContent = null;
            int? FileId = Convert.ToInt32(context.Request.QueryString["FileId"]);
            if (FileId != null && FileId > 0)
            {
                FileViewModel file_entity = FileRepository.GetMainDetails(FileId);
                if (file_entity != null)
                {
                    filePath = "~" + file_entity.FileUrl;
                    fileContent = file_entity.FileContent;
                    physicalFilePath = HttpContext.Current.Server.MapPath(filePath);
                    folderPath = "~" + file_entity.FolderPath;
                    physicalFolderPath = HttpContext.Current.Server.MapPath(folderPath);
                    contentType = file_entity.ContentType;
                    context.Response.ContentType = contentType;
                    context.Response.BinaryWrite(fileContent);
                }
                else
                    context.Response.WriteFile("~/Files/images/no_image.jpg");
            }
            else
                context.Response.WriteFile("~/Files/images/no_image.jpg");
            //}
            //catch (Exception ex)
            //{
            //    context.Response.ContentType = "text/plain";
            //    context.Response.Write(ex.Message);
            //}
            //finally { context.Response.End(); }
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