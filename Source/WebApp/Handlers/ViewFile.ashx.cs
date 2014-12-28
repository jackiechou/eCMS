using Eagle.Common.Utilities;
using Microsoft.Win32;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Eagle.Model.SYS;
using Eagle.Repository.SYS.FileManager;

namespace Eagle.WebApp.Handlers
{
    /// <summary>
    /// Summary description for ViewFile
    /// </summary>
    public class ViewFile : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
             //try
             //{
                 string contentType = string.Empty, folderPath = string.Empty, physicalFolderPath = string.Empty, physicalFilePath = string.Empty, filePath = string.Empty, fileNameWithoutExtension = string.Empty;
                 byte[] fileContent = null;
                 string sFileId = context.Request.QueryString["FileId"];
                 if (!string.IsNullOrEmpty(sFileId) && sFileId !="0")
                {
                    int? FileId = Convert.ToInt32(sFileId);
                    FileViewModel file_entity = FileRepository.GetMainDetails(FileId);
                    if (file_entity != null)
                    {
                        filePath = "~" + file_entity.FileUrl;
                        fileContent = file_entity.FileContent;
                        physicalFilePath = HttpContext.Current.Server.MapPath( filePath);
                        folderPath = "~" + file_entity.FolderPath;
                        physicalFolderPath = HttpContext.Current.Server.MapPath(folderPath);
                        contentType = file_entity.ContentType;
                        context.Response.ContentType = contentType;

                        if (!string.IsNullOrEmpty(file_entity.FolderPath))
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(physicalFolderPath);
                            if (!dirInfo.Exists)
                                dirInfo.Create();
                            
                            FileInfo fileInfo = new FileInfo(physicalFilePath);
                            if (!fileInfo.Exists)
                                context.Response.BinaryWrite(fileContent);
                            else
                                context.Response.WriteFile(filePath);                          
                        }
                        else
                            context.Response.WriteFile("~/Files/images/no_image.jpg");
                    }else
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