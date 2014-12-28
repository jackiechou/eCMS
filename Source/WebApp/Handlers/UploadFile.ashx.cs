using Eagle.Common.Settings;
using Eagle.Common.Utilities;
using Eagle.Model;
using Eagle.Repository;
using Eagle.Repository.SYS.FileManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Eagle.WebApp.Handlers
{
    /// <summary>
    /// Summary description for UploadFile
    /// </summary>
    public class UploadFile : IHttpHandler, IRequiresSessionState 
    {
        string fileKey = string.Empty, folderKey = string.Empty,
            fileTitle = string.Empty, fileName = string.Empty, fileDescription = string.Empty, fileType = string.Empty, fileExt = string.Empty,
            virtual_dir_path = string.Empty, physical_dir_path = string.Empty, filePath = string.Empty, result = string.Empty;

        int? fileId = null, height = null, width = null, oFileId = null;
        bool flag = false;

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Params["fileKey"] != null && context.Request.QueryString["fileKey"] != "")
                fileKey = Convert.ToString(context.Request.Params["fileKey"]);

            if (context.Request.Params["folderKey"] != null && context.Request.QueryString["folderKey"] != "")
                folderKey = Convert.ToString(context.Request.Params["folderKey"]);

            if (context.Request.Params["fileId"] != null && context.Request.QueryString["fileId"] != "")
                fileId = Convert.ToInt32(context.Request.Params["fileId"]);

            if (context.Request.Params["fileName"] != null && context.Request.QueryString["fileName"] != "")
                fileTitle = Convert.ToString(context.Request.Params["fileName"]);

            if (context.Request.Params["fileDescription"] != null && context.Request.QueryString["fileDescription"] != "")
                fileDescription = Convert.ToString(context.Request.Params["fileDescription"]);

            if (context.Request.Params["width"] != null && context.Request.QueryString["width"] != "")
                width = Convert.ToInt32(context.Request.Params["width"]);

            if (context.Request.Params["height"] != null && context.Request.QueryString["height"] != "")
                height = Convert.ToInt32(context.Request.Params["height"]);


            //Upload one file -----------------------------------------------------------------------------------------------
            HttpPostedFile file = null;
            if(!string.IsNullOrEmpty(fileKey))
                file = context.Request.Files[fileKey];
            else
                file = context.Request.Files[0];

            if (file.ContentLength > 0)
            {
                Stream fs = file.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] fileContent = br.ReadBytes((Int32)fs.Length);


                if (folderKey != string.Empty)
                {
                    virtual_dir_path = "~" + FolderRepository.GetFolderPathByFolderKey(folderKey);
                    physical_dir_path = HttpContext.Current.Server.MapPath(virtual_dir_path);
                    if (!Directory.Exists(physical_dir_path))
                        Directory.CreateDirectory(physical_dir_path);
                    
                    fileExt = System.IO.Path.GetExtension(file.FileName).ToLower().Trim();
                    fileType = file.ContentType;
                    fileName = FileUtils.GenerateEncodedFileNameWithDate(file.FileName);

                    if (string.IsNullOrEmpty(fileTitle))
                        fileTitle = System.IO.Path.GetFileNameWithoutExtension(fileName); 
                    filePath = System.IO.Path.Combine(context.Server.MapPath(virtual_dir_path), fileName);
                    file.SaveAs(filePath);

                    int creater = Convert.ToInt32(HttpContext.Current.Session[SettingKeys.UserId].ToString());

                    if (fileId == null || fileId == 0)
                        flag = FileRepository.InsertData(folderKey, fileTitle, fileName, fileDescription, fileContent, width, height, creater, out oFileId);
                    else
                        flag = FileRepository.UpdateData((int)fileId, folderKey, fileTitle, fileName, fileDescription, fileContent, width, height, creater, out oFileId);
                }
                else
                {
                    result = Eagle.Resource.LanguageResource.FolderKeyNotFound;
                }
            }
            else
            {
                result = Eagle.Resource.LanguageResource.HttpPostedFileNotRecognized;
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(oFileId);
            context.Response.End();
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