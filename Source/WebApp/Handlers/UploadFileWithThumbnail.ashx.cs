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
    /// Summary description for UploadFileWithThumbnail
    /// </summary>
    public class UploadFileWithThumbnail : IHttpHandler, IRequiresSessionState 
    {

        string fileKey = string.Empty, folderKey = string.Empty, fileTitle = string.Empty, fileName = string.Empty,
            fileNameWithoutExtension = string.Empty, fileDescription = string.Empty, fileExt = string.Empty,
             virtual_dir_path = string.Empty, physicalUploadPath = string.Empty, fileType = string.Empty, result =string.Empty;

        int? fileId = null, height = null, width = null, oMainFileId = null, oThumbFileId = null;
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
          

            List<int?> lstResults = new List<int?>();

            //Upload one file -----------------------------------------------------------------------------------------------
            HttpPostedFile file = null;
            if (!string.IsNullOrEmpty(fileKey))
                file = context.Request.Files[fileKey];
            else
                file = context.Request.Files[0];
            if (file != null)
            {
                bool IsFolderCreatedByDate = false;
                fileType = file.ContentType;
                fileExt = System.IO.Path.GetExtension(file.FileName).ToLower().Trim();
                if (string.IsNullOrEmpty(fileTitle))
                    fileTitle = System.IO.Path.GetFileNameWithoutExtension(file.FileName);           
                string main_image_name = HttpContext.Current.Server.HtmlEncode(string.Format("{0}{1}", fileTitle, fileExt));
                string front_image_name = HttpContext.Current.Server.HtmlEncode(string.Format("{0}_thumb{1}", fileTitle, fileExt));
                string[] FileImg = new string[2];

                if (folderKey != string.Empty)
                {
                    virtual_dir_path = "~" + FolderRepository.GetFolderPathByFolderKey(folderKey);
                    string physicalUploadPath = HttpContext.Current.Server.MapPath(virtual_dir_path);
                    if (!Directory.Exists(physicalUploadPath))
                        Directory.CreateDirectory(physicalUploadPath);

                    if (IsFolderCreatedByDate)
                    {
                        string date = DateTime.Today.Day.ToString();
                        string month = DateTime.Today.Month.ToString();
                        string year = DateTime.Today.Year.ToString();

                        if (!System.IO.Directory.Exists(physicalUploadPath))
                            System.IO.Directory.CreateDirectory(physicalUploadPath);
                        if (!System.IO.Directory.Exists(physicalUploadPath + "\\" + year))
                            System.IO.Directory.CreateDirectory(physicalUploadPath + "\\" + year);
                        if (!System.IO.Directory.Exists(physicalUploadPath + "\\" + year + "\\" + month))
                            System.IO.Directory.CreateDirectory(physicalUploadPath + "\\" + year + "\\" + month);
                        if (!System.IO.Directory.Exists(physicalUploadPath + "\\" + year + "\\" + month + "\\" + date))
                            System.IO.Directory.CreateDirectory(physicalUploadPath + "\\" + year + "\\" + month + "\\" + date);

                        physicalUploadPath = physicalUploadPath + "\\" + year + "\\" + month + "\\" + date;

                        FileImg[0] = (year + "\\" + month + "\\" + date + "\\" + main_image_name).Replace("\\", "/");
                        FileImg[1] = (year + "\\" + month + "\\" + date + "\\" + front_image_name).Replace("\\", "/");
                    }
                    else
                    {
                        FileImg[0] = main_image_name;
                        FileImg[1] = front_image_name;
                    }

                    string main_image_path = Path.Combine(physicalUploadPath, main_image_name);
                    string front_image_path = Path.Combine(physicalUploadPath, front_image_name);

                    // Save main image================================================================================
                    System.Drawing.Image image = System.Drawing.Image.FromStream(file.InputStream);
                    image.Save(main_image_path);
                    Byte[] mainFileContent = FileUtils.ConvertImageToByteArray(image);

                    System.Drawing.Image thumb = image.GetThumbnailImage((int)width, (int)height, null, IntPtr.Zero);
                    thumb.Save(front_image_path);
                    Byte[] thumbFileContent = FileUtils.ConvertImageToByteArray(image);

                    image.Dispose();
                    thumb.Dispose();


                    int creater = Convert.ToInt32(HttpContext.Current.Session[SettingKeys.UserId].ToString());

                    if (fileId == null || fileId == 0)
                    {
                        flag = FileRepository.InsertData(folderKey, fileTitle, FileImg[0], fileDescription, mainFileContent, width, height, creater, out oMainFileId);
                        flag = FileRepository.InsertData(folderKey, fileTitle, FileImg[1], fileDescription, thumbFileContent, width, height, creater, out oThumbFileId);
                    }
                    else
                    {
                        flag = FileRepository.UpdateData((int)fileId, folderKey, fileTitle, FileImg[0], fileDescription, mainFileContent, width, height, creater, out oMainFileId);
                        flag = FileRepository.UpdateData((int)fileId, folderKey, fileTitle, FileImg[1], fileDescription, thumbFileContent, width, height, creater, out oThumbFileId);
                    }

                    lstResults.Add(oMainFileId);
                    lstResults.Add(oThumbFileId);
                }
                else
                {
                    result = Eagle.Resource.LanguageResource.FolderKeyNotFound;
                }
                result = String.Join(",", lstResults);
            }
            else
            {
                result = Eagle.Resource.LanguageResource.HttpPostedFileNotRecognized;
            }
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