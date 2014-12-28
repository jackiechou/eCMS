using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository.Host;
using Eagle.Repository.HR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using Eagle.Model.SYS;

namespace Eagle.Repository.SYS.FileManager
{
    public class FileRepository
    {
        public EntityDataContext context { get; set; }
        public FileRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public bool CheckImageTypeValid(HttpPostedFileBase FileUpload, out string errorMessage)
        {
            if (FileUpload != null)
            {
                string[] validImageTypes = new string[]
                {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png",
                    "image/bmp"
                };
                if (!validImageTypes.Contains(FileUpload.ContentType))
                {
                    errorMessage = Eagle.Resource.LanguageResource.FileUploadInvalid;
                    return false;
                }
            }

            errorMessage = "";
            return true;
        }
        public string GenerateDownloadLink(int? FileId)
        {
            string DownloadFileLink = "";
            if (FileId != null && FileId > 0)
            {
                FileViewModel entity = GetDetails(FileId);
                if (entity != null)
                {
                    string PhysicalFolderPath = HttpContext.Current.Server.MapPath("~" + entity.FolderPath);
                    string FileTitle = entity.FileTitle;
                    string FileName = entity.FileName;
                    string FileUrl = entity.FileUrl;
                    Uri RequestUri = HttpContext.Current.Request.Url;
                    string BaseUrl = RequestUri.Scheme + Uri.SchemeDelimiter + RequestUri.Host + (RequestUri.IsDefaultPort ? "" : ":" + RequestUri.Port);

                    DirectoryInfo dirInfo = new DirectoryInfo(PhysicalFolderPath);
                    if (!dirInfo.Exists)
                        dirInfo.Create();

                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(FileUrl));
                    if (fileInfo.Exists)
                    {
                        string DowloadText = Eagle.Resource.LanguageResource.DownloadFile;
                        string extension = fileInfo.Extension;
                        string[] ValidImageTypes = HostSettings.ExensionImage.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        //string[] ValidImageTypes = { ".bmp", ".gif", ".png", ".jpg", ".jpeg", ".tiff", ".swf" };
                        bool IsValidImageFile = false;
                        for (int i = 0; i < ValidImageTypes.Length; i++)
                        {
                            if (extension == ValidImageTypes[i])
                            {
                                IsValidImageFile = true;
                                break;
                            }
                        }
                        if (!IsValidImageFile)
                        {
                            DownloadFileLink = "<span><a data-id='"+FileId+"' href=\"" + BaseUrl + "/Handlers/DownloadFile.ashx?file=" + FileUrl + "\">" + FileTitle + "</a></span>";
                        }
                    }
                    else
                    {
                        //DownloadFileLink = "<span>File Url doesn't exist</span>";
                        fileInfo.Create();
                    }
                }
            }
            //else
            //{
            //    DownloadFileLink = "<span>No File exists</span>";
            //}
            return DownloadFileLink;
        }

        public string CreateDownloadLink(int? FileId)
        {
            string DownloadFileLink = "";
            if (FileId != null)
            {
                FileViewModel entity = GetDetails(FileId);
                if (entity != null)
                {
                    string PhysicalFolderPath = HttpContext.Current.Server.MapPath("~" + entity.FolderPath);
                    string FileName = entity.FileName;
                    string FileUrl = entity.FileUrl;
                    Uri RequestUri = HttpContext.Current.Request.Url;
                    string BaseUrl = RequestUri.Scheme + Uri.SchemeDelimiter + RequestUri.Host + (RequestUri.IsDefaultPort ? "" : ":" + RequestUri.Port);

                    DirectoryInfo dirInfo = new DirectoryInfo(PhysicalFolderPath);
                    if (!dirInfo.Exists)
                        dirInfo.Create();
                    else
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(FileUrl));
                        if (fileInfo.Exists)
                        {
                            string DowloadText = Eagle.Resource.LanguageResource.DownloadFile;
                            string extension = fileInfo.Extension;
                            string[] ValidImageTypes = HostSettings.ExensionImage.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            //string[] ValidImageTypes = { ".bmp", ".gif", ".png", ".jpg", ".jpeg", ".tiff", ".swf" };
                            bool IsValidImageFile = false;
                            for (int i = 0; i < ValidImageTypes.Length; i++)
                            {
                                if (extension == ValidImageTypes[i])
                                {
                                    IsValidImageFile = true;
                                    break;
                                }
                            }
                            if (!IsValidImageFile)
                            {
                                DownloadFileLink = "<span><a data-id='" + FileId + "' href=\"" + BaseUrl + "/Handlers/DownloadFile.ashx?file=" + FileUrl + "\">" + DowloadText + "</a></span>";
                            }
                        }
                        else
                        {
                            //DownloadFileLink = "<span>File Url doesn't exist</span>";
                            fileInfo.Create();
                        }
                    }
                }
            }
            else
            {
                DownloadFileLink = "<span>No File exists</span>";
            }
            return DownloadFileLink;
        }

        public static string GetList(string FileIds)
        {
            string json = string.Empty;
            if (!string.IsNullOrEmpty(FileIds))
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    List<FileViewModel> lst = new List<FileViewModel>();
                    List<string> idList = FileIds.Split(',').Reverse().ToList();
                    if (idList.Count > 0)
                    {
                        foreach (var item in idList)
                        {
                            int? FileId = Convert.ToInt32(item);
                            var entity = (from file in context.ApplicationFiles
                                          join folder in context.ApplicationFolders on file.FolderId equals folder.FolderId
                                          where file.FileId == FileId
                                          select new FileViewModel()
                                          {
                                              FileId = file.FileId,
                                              FileTitle = file.FileTitle,
                                              FileName = file.FileName,
                                              FileDescription = file.FileDescription,
                                              Extension = file.Extension,
                                              Size = file.Size,
                                              FileUrl = folder.FolderPath + "/" + file.FileName,
                                              FolderId = file.FolderId,
                                              FolderKey = folder.FolderKey,
                                              FolderPath = folder.FolderPath,
                                              FileContent = file.FileContent,
                                              ContentType = file.ContentType,
                                              Width = file.Width,
                                              Height = file.Height
                                          }).FirstOrDefault();
                            lst.Add(entity);
                        }
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        json = serializer.Serialize(lst);
                    }
                }
            }
            return json;
        }

        public static List<FileUploadModel> GetDownloadFileList(string FileIds)
        {
            List<FileUploadModel> lst = new List<FileUploadModel>();
            if (!string.IsNullOrEmpty(FileIds))
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                   
                    List<string> idList = FileIds.Split(',').Reverse().ToList();
                    if (idList.Count > 0)
                    {
                        foreach (var item in idList)
                        {
                            int? FileId = Convert.ToInt32(item);
                            var entity = (from file in context.ApplicationFiles
                                          join folder in context.ApplicationFolders on file.FolderId equals folder.FolderId
                                          where file.FileId == FileId
                                          select new FileUploadModel()
                                          {
                                              FileId = file.FileId,
                                              FileTitle = file.FileTitle,
                                              FileName = file.FileName,
                                              FileDescription = file.FileDescription,
                                              Extension = file.Extension,
                                              Size = file.Size,
                                              FileUrl = folder.FolderPath + "/" + file.FileName,
                                              FolderId = file.FolderId,
                                              FolderKey = folder.FolderKey,
                                              FolderPath = folder.FolderPath,
                                              FileContent = file.FileContent,
                                              ContentType = file.ContentType,
                                              Width = file.Width,
                                              Height = file.Height
                                          }).FirstOrDefault();
                            lst.Add(entity);
                        }                       
                    }
                }
            }
            return lst;
        }

        public static List<FileUploadModel> GetDownloadFileList(int ItemId, string ItemTag)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<FileUploadModel> lst = new List<FileUploadModel>();
                List<int> FileIds = new List<int>();
                switch (ItemTag)
                {
                    case "Qualification":
                        FileIds = QualificationRespository.GetFileIds(ItemId);
                        break;
                    case "Contract":
                        FileIds = ContractRespository.GetFileIds(ItemId);
                        break;
                }

                if (FileIds != null && FileIds.Count > 0)
                {
                    string strFileIds = string.Join(", ", FileIds);
                    foreach (int id in FileIds)
                    {                       
                        var entity = (from file in context.ApplicationFiles
                                        join folder in context.ApplicationFolders on file.FolderId equals folder.FolderId
                                        where file.FileId == id
                                        select new FileUploadModel()
                                        {
                                            FileId = file.FileId,
                                            FileTitle = file.FileTitle,
                                            FileName = file.FileName,
                                            FileDescription = file.FileDescription,
                                            Extension = file.Extension,
                                            Size = file.Size,
                                            FileUrl = folder.FolderPath + "/" + file.FileName,
                                            FolderId = file.FolderId,
                                            FolderKey = folder.FolderKey,
                                            FolderPath = folder.FolderPath,
                                            FileContent = file.FileContent,
                                            ContentType = file.ContentType,
                                            Width = file.Width,
                                            Height = file.Height,
                                            FileIds = strFileIds,
                                            ItemId = ItemId,
                                            ItemTag = ItemTag
                                        }).FirstOrDefault();
                        if(entity!=null)
                            lst.Add(entity);
                    }                              
                }
                return lst;
            }  
        }

        public static List<FileUploadModel> GetDownloadFileList(int ItemId, string ItemTag, string FileIds)
        {            
            using (EntityDataContext context = new EntityDataContext())
            {
                List<FileUploadModel> lst = new List<FileUploadModel>();
                if (!string.IsNullOrEmpty(FileIds))
                {
                    List<string> idList = FileIds.Split(',').Reverse().ToList();
                    if (idList.Count > 0)
                    {
                        foreach (var item in idList)
                        {
                            int? FileId = Convert.ToInt32(item);
                            var entity = (from file in context.ApplicationFiles
                                            join folder in context.ApplicationFolders on file.FolderId equals folder.FolderId
                                            where file.FileId == FileId
                                            select new FileUploadModel()
                                            {
                                                FileId = file.FileId,
                                                FileTitle = file.FileTitle,
                                                FileName = file.FileName,
                                                FileDescription = file.FileDescription,
                                                Extension = file.Extension,
                                                Size = file.Size,
                                                FileUrl = folder.FolderPath + "/" + file.FileName,
                                                FolderId = file.FolderId,
                                                FolderKey = folder.FolderKey,
                                                FolderPath = folder.FolderPath,
                                                FileContent = file.FileContent,
                                                ContentType = file.ContentType,
                                                Width = file.Width,
                                                Height = file.Height,
                                                ItemId = ItemId,
                                                ItemTag = ItemTag
                                            }).FirstOrDefault();
                            lst.Add(entity);
                        }
                    }
                }
                return lst;
            }            
        }

        public static Eagle.Core.ApplicationFile Find(int? FileId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                // entity = (from file in context.ApplicationFiles where file.FileId == FileId select file).FirstOrDefault();
                return context.ApplicationFiles.Find(FileId);
            }
        }

        public static string GetFileNameByFileId(int? FileId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                string FileName = (from file in context.ApplicationFiles
                                   where file.FileId == FileId
                                   select file.FileName).FirstOrDefault();
                return FileName;
            }
        }

        public static byte[] GetFileContentByFileId(int? FileId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                byte[] FileContent = (from f in context.ApplicationFiles where f.FileId == FileId select f.FileContent).FirstOrDefault();
                return FileContent;
            }
        }

        public static string GetFileUrlByFileId(int? FileId)
        {
            string result = string.Empty;
            using (EntityDataContext context = new EntityDataContext())
            {                
                if (FileId != null && FileId > 0)
                {
                    var query = (from file in context.ApplicationFiles
                                 join folder in context.ApplicationFolders on file.FolderId equals folder.FolderId
                                 where file.FileId == FileId
                                 select new { FileUrl = folder.FolderPath + "/" + file.FileName }).FirstOrDefault();
                    result = query.FileUrl;
                }                
            }
            return result;
        }
        public List<FileViewModel> GetList()
        {
            List<FileViewModel> lst = new List<FileViewModel>();
            try
            {
                lst = (from file in context.ApplicationFiles
                       join folder in context.ApplicationFolders on file.FolderId equals folder.FolderId
                       select new FileViewModel()
                       {
                           FileId = file.FileId,
                           FileTitle = file.FileTitle,
                           FileName = file.FileName,
                           FileDescription = file.FileDescription,
                           Extension = file.Extension,
                           ContentType = file.ContentType,
                           FolderId = file.FolderId,
                           Size = file.Size,
                           Width = file.Width,
                           Height = file.Height,
                           CreatedByUserId = file.CreatedByUserId,
                           CreatedOnDate = file.CreatedOnDate,
                           LastModifiedByUserId = file.LastModifiedByUserId,
                           LastModifiedOnDate = file.LastModifiedOnDate,
                           FolderKey = folder.FolderKey,
                           FolderPath = folder.FolderPath
                       }).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lst;
        }

        public static FileViewModel GetMainDetails(int? FileId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                FileViewModel entity = new FileViewModel();
                try
                {
                    entity = (from file in context.ApplicationFiles
                              join folder in context.ApplicationFolders on file.FolderId equals folder.FolderId
                              where file.FileId == FileId
                              select new FileViewModel()
                              {
                                  FileId = file.FileId,
                                  FileTitle = file.FileTitle,
                                  FileName = file.FileName,
                                  FileDescription = file.FileDescription,
                                  FileContent = file.FileContent,
                                  Extension = file.Extension,
                                  ContentType = file.ContentType,
                                  FolderId = file.FolderId,
                                  FileUrl = folder.FolderPath + "/" + file.FileName,
                                  Size = file.Size,
                                  Width = file.Width,
                                  Height = file.Height,
                                  FolderKey = folder.FolderKey,
                                  FolderPath = folder.FolderPath
                              }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                return entity;
            }
        }
        
        public static FileUploadModel GetDetailByFileId(int FileId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                FileUploadModel entity = new FileUploadModel();
                try
                {
                    entity = (from file in context.ApplicationFiles
                              join folder in context.ApplicationFolders on file.FolderId equals folder.FolderId
                              where file.FileId == FileId
                              select new FileUploadModel()
                              {
                                  FileId = file.FileId,
                                  FileTitle = file.FileTitle,
                                  FileName = file.FileName,
                                  FileDescription = file.FileDescription,
                                  FileContent = file.FileContent,
                                  Extension = file.Extension,
                                  ContentType = file.ContentType,
                                  FolderId = file.FolderId,
                                  FileUrl = folder.FolderPath + "/" + file.FileName,
                                  Size = file.Size,
                                  Width = file.Width,
                                  Height = file.Height,
                                  FolderKey = folder.FolderKey,
                                  FolderPath = folder.FolderPath
                              }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                return entity;
            }
        }

        public static FileUploadModel GetDetailByFileId(int FileId, int ItemId, string ItemTag)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                FileUploadModel entity = new FileUploadModel();
                try
                {
                    entity = (from file in context.ApplicationFiles
                              join folder in context.ApplicationFolders on file.FolderId equals folder.FolderId
                              where file.FileId == FileId
                              select new FileUploadModel()
                              {
                                  FileId = file.FileId,
                                  FileTitle = file.FileTitle,
                                  FileName = file.FileName,
                                  FileDescription = file.FileDescription,
                                  FileContent = file.FileContent,
                                  Extension = file.Extension,
                                  ContentType = file.ContentType,
                                  FolderId = file.FolderId,
                                  FileUrl = folder.FolderPath + "/" + file.FileName,
                                  Size = file.Size,
                                  Width = file.Width,
                                  Height = file.Height,                                 
                                  FolderKey = folder.FolderKey,
                                  FolderPath = folder.FolderPath,
                                  ItemId = ItemId,
                                  ItemTag = ItemTag
                              }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                return entity;
            }
        }

        public static FileUploadModel GetDetailByFileId(int FileId, string FolderKey, int ItemId, string ItemTag)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                FileUploadModel entity = new FileUploadModel();
                try
                {
                    entity = (from file in context.ApplicationFiles
                              join folder in context.ApplicationFolders on file.FolderId equals folder.FolderId
                              where file.FileId == FileId
                              select new FileUploadModel()
                              {
                                  FileId = file.FileId,
                                  FileTitle = file.FileTitle,
                                  FileName = file.FileName,
                                  FileDescription = file.FileDescription,
                                  FileContent = file.FileContent,
                                  Extension = file.Extension,
                                  ContentType = file.ContentType,
                                  FolderId = file.FolderId,
                                  FileUrl = folder.FolderPath + "/" + file.FileName,
                                  Size = file.Size,
                                  Width = file.Width,
                                  Height = file.Height,
                                  FolderKey = folder.FolderKey,
                                  FolderPath = folder.FolderPath,
                                  ItemId = ItemId,
                                  ItemTag = ItemTag
                              }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                return entity;
            }
        }

        public static FileViewModel GetDetails(int? FileId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                FileViewModel entity = new FileViewModel();
                try
                {
                    entity = (from file in context.ApplicationFiles
                              join folder in context.ApplicationFolders on file.FolderId equals folder.FolderId
                              where file.FileId == FileId
                              select new FileViewModel()
                              {
                                  FileId = file.FileId,
                                  FileTitle = file.FileTitle,
                                  FileName = file.FileName,
                                  FileDescription = file.FileDescription,
                                  FileContent = file.FileContent,
                                  Extension = file.Extension,
                                  ContentType = file.ContentType,
                                  FolderId = file.FolderId,
                                  FileUrl = folder.FolderPath + "/" + file.FileName,
                                  Size = file.Size,
                                  Width = file.Width,
                                  Height = file.Height,
                                  CreatedByUserId = file.CreatedByUserId,
                                  CreatedOnDate = file.CreatedOnDate,
                                  LastModifiedByUserId = file.LastModifiedByUserId,
                                  LastModifiedOnDate = file.LastModifiedOnDate,
                                  FolderKey = folder.FolderKey,
                                  FolderPath = folder.FolderPath
                              }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                return entity;
            }
        }

        public static bool IsFileIdExisted(int FileId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.ApplicationFiles.FirstOrDefault(p => p.FileId.Equals(FileId));
                return (query != null);
            }
        }

        public static int Add(FileViewModel add_model, out string outMessage)
        {           
            int FileId = 0;
            outMessage = string.Empty;
            try
            {
                bool isDuplicate = IsFileIdExisted(add_model.FileId);
                if (isDuplicate == false)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        Eagle.Core.ApplicationFile model = new Eagle.Core.ApplicationFile();

                        model.FileName = add_model.FileName;
                        model.FileTitle = add_model.FileTitle;
                        model.FileDescription = add_model.FileDescription;
                        model.Extension = add_model.Extension;
                        model.ContentType = add_model.ContentType;
                        model.FolderId = add_model.FolderId;
                        model.Size = add_model.Size;
                        model.Width = add_model.Width;
                        model.Height = add_model.Height;
                        model.CreatedByUserId = add_model.CreatedByUserId;
                        model.CreatedOnDate = DateTime.Now;

                        int affectedRow = 0;
                        context.Entry(model).State = System.Data.Entity.EntityState.Added;
                        affectedRow = context.SaveChanges();
                        if (affectedRow > 0)
                        {
                            FileId = model.FileId;
                            outMessage = Eagle.Resource.LanguageResource.CreateSuccess;
                        }
                        else
                        {
                            outMessage = Eagle.Resource.LanguageResource.CreateFailure;
                        }
                    }
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return FileId;
           
        }

        public static int Edit(FileViewModel edit_model, out string outMessage)
        {
            outMessage = string.Empty;
            int affectedRow = 0;
            try
            {
                Eagle.Core.ApplicationFile entity = Find(edit_model.FileId);    
                if(entity != null)
                {
                    entity.FileName = edit_model.FileName;
                    entity.FileTitle = edit_model.FileTitle;
                    entity.FileDescription = edit_model.FileDescription;
                    entity.Extension = edit_model.Extension;
                    entity.ContentType = edit_model.ContentType;
                    entity.FolderId = edit_model.FolderId;
                    entity.Size = edit_model.Size;
                    entity.Width = edit_model.Width;
                    entity.Height = edit_model.Height;
                    entity.LastModifiedByUserId = edit_model.LastModifiedByUserId;
                    entity.LastModifiedOnDate = DateTime.Now;

                    using (EntityDataContext context = new EntityDataContext())
                    {
                        context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        affectedRow = context.SaveChanges();
                        if (affectedRow > 0)
                            outMessage = Eagle.Resource.LanguageResource.CreateSuccess;
                        else
                        {
                            affectedRow = -1;
                            outMessage = Eagle.Resource.LanguageResource.CreateFailure;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                outMessage = ex.Message;
                affectedRow = -2;
            }
            return affectedRow;
        }

        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        public static int Insert(HttpPostedFileBase FileUpload, string FolderKey, int? Width, int? Height, int? CreatedByUserId)
        {
            int FileId = 0;
            try
            {
                string ContentType = string.Empty, FileExtension = string.Empty, FileName = string.Empty, FileTitle = string.Empty, FileDescription = string.Empty, PhysicalFilePath = string.Empty;
                int? FileSize = null;
 
                if (FileUpload != null && FileUpload.ContentLength > 0)
                {
                    Eagle.Core.ApplicationFolder folder_entity = FolderRepository.GetFolderInfoByFolderKey(FolderKey);
                    string FolderPath = "~" + folder_entity.FolderPath;
                    int FolderId = folder_entity.FolderId;
                    string physical_dir_path = HttpContext.Current.Server.MapPath(FolderPath);
                    if (!Directory.Exists(physical_dir_path))
                        Directory.CreateDirectory(physical_dir_path);
                    
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        string[] FileNames = FileUtils.UploadFile(FileUpload, FolderPath);
                        FileTitle = FileNames[0];
                        FileName = FileNames[1];
                        PhysicalFilePath = Path.Combine(physical_dir_path, FileName);

                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(PhysicalFilePath);
                        if (fileInfo.Exists)
                        {
                            FileExtension = fileInfo.Extension;
                            FileSize = Convert.ToInt32(fileInfo.Length);
                            ContentType = GetMimeType(FileName);

                            if ((Width == null || Width == 0) && (Height == null || Height == 0))
                                FileUtils.IsImage(FileUpload, out Width, out Height);
                        }


                        Eagle.Core.ApplicationFile model = new Eagle.Core.ApplicationFile();
                        model.FileName = FileName;
                        model.FileTitle = FileTitle;
                        model.FileDescription = FileTitle;
                        model.Extension = FileExtension;
                        model.ContentType = ContentType;
                        model.FolderId = FolderId;
                        model.Size = FileSize;
                        model.Width = Width;
                        model.Height = Height;
                        model.CreatedByUserId = CreatedByUserId;
                        model.CreatedOnDate = DateTime.Now;

                        int affectedRow = 0;
                        context.Entry(model).State = System.Data.Entity.EntityState.Added;
                        affectedRow = context.SaveChanges();
                        if (affectedRow > 0)
                            FileId = model.FileId;
                    }
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return FileId;
        }

        public static bool Update(HttpPostedFileBase FileUpload, string FolderKey, int? Width, int? Height, int? FileId, int? LastModifiedByUserId, out int? oFileId)
        {
            string ContentType = string.Empty, FileExtension = string.Empty, FileName = string.Empty, FileTitle = string.Empty, FileDescription = string.Empty, PhysicalFilePath = string.Empty;
            int? FileSize = null;
            oFileId = null;
            bool result = false;
            try
            {
                if (FileUpload != null && FileUpload.ContentLength > 0)
                {
                    Eagle.Core.ApplicationFolder folder_entity = FolderRepository.GetFolderInfoByFolderKey(FolderKey);
                    int FolderId = folder_entity.FolderId;
                    string FolderPath = "~" + folder_entity.FolderPath;
                    string physical_dir_path = HttpContext.Current.Server.MapPath(FolderPath);
                    if (!Directory.Exists(physical_dir_path))
                        Directory.CreateDirectory(physical_dir_path);

                    using (EntityDataContext context = new EntityDataContext())
                    {
                        string OriginalFileName = GetFileNameByFileId(FileId);
                        string Original_File_Path = FolderPath + "/" + OriginalFileName;
                        string[] FileNames = Eagle.Common.Utilities.FileUtils.UploadFileAndRemoveOldFile(FileUpload, Original_File_Path, FolderPath);
                        FileTitle = FileNames[0];
                        FileName = FileNames[1];
                        string PhysicalNewFilePath = Path.Combine(physical_dir_path, FileName);
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(PhysicalNewFilePath);
                        if (fileInfo.Exists)
                        {
                            FileExtension = fileInfo.Extension;
                            FileSize = Convert.ToInt32(fileInfo.Length);
                            ContentType = GetMimeType(FileName);
                        }


                        Eagle.Core.ApplicationFile entity = Find(FileId);
                        if (entity != null)
                        {
                            entity.FileName = FileName;
                            entity.FileTitle = FileTitle;
                            entity.FileDescription = FileName;
                            entity.Extension = FileExtension;
                            entity.ContentType = ContentType;
                            entity.FolderId = FolderId;
                            entity.Size = FileSize;
                            entity.Width = Width;
                            entity.Height = Height;
                            entity.LastModifiedByUserId = LastModifiedByUserId;
                            entity.LastModifiedOnDate = DateTime.Now;

                            context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                            int affectedRow = context.SaveChanges();
                            if (affectedRow > 0)
                                result = true;
                        }
                        else
                        {
                            oFileId = Insert(FileUpload, FolderKey, Width, Height, LastModifiedByUserId);
                        }                        
                    }
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return result;
        }

        public static bool InsertData(string FolderKey, string FileTitle, string FileName, string FileDescription, byte[] FileContent, int? Width, int? Height, int? CreatedByUserId, out int? oFileId)
        {
            oFileId = null;
            bool flag = false;
            try
            {
                Eagle.Core.ApplicationFolder folder_entity = FolderRepository.GetFolderInfoByFolderKey(FolderKey);
                string FolderPath = "~" + folder_entity.FolderPath;
                int FolderId = folder_entity.FolderId;
                string physical_dir_path = HttpContext.Current.Server.MapPath(FolderPath);
                if (!Directory.Exists(physical_dir_path))
                    Directory.CreateDirectory(physical_dir_path);
                string FilePath = Path.Combine(physical_dir_path, FileName);

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(FilePath);
                string fileExtension = fileInfo.Extension;
                int fileSize = Convert.ToInt32(fileInfo.Length);

                using (EntityDataContext context = new EntityDataContext())
                {
                    Eagle.Core.ApplicationFile model = new Eagle.Core.ApplicationFile();
                    model.FileName = FileName;
                    model.FileTitle = FileTitle;
                    model.FileDescription = FileDescription;
                    model.FileContent = FileContent;
                    model.Extension = fileExtension;
                    model.ContentType = GetMimeType(FileName);
                    model.FolderId = FolderId;
                    model.Size = fileSize;
                    model.Width = Width;
                    model.Height = Height;
                    model.CreatedByUserId = CreatedByUserId;
                    model.CreatedOnDate = DateTime.Now;

                    int affectedRow = 0;
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    affectedRow = context.SaveChanges();
                    if (affectedRow > 0)
                    {
                        oFileId = model.FileId;
                        flag = true;
                    }
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return flag;
        }

        public static bool UpdateData(int FileId, string FolderKey, string FileTitle, string FileName, string FileDescription, byte[] FileContent, int? Width, int? Height, int? LastModifiedByUserId, out int? oFileId)
        {
            oFileId = 0;
            bool flag = false;
            
            try
            {                
                FolderViewModel folder_entity = FolderRepository.GeDetailsByFolderKey(FolderKey);
                int SelectedFolderId = folder_entity.FolderId;
                string SelectedFolderPath = folder_entity.FolderPath;
                string NewFilePath = "~" + SelectedFolderPath + "/" + FileName;
                string PhysicalDirPath = HttpContext.Current.Server.MapPath(SelectedFolderPath);
                if (!Directory.Exists(PhysicalDirPath))
                    Directory.CreateDirectory(PhysicalDirPath);
                
                string PhysicalNewFilePath = HttpContext.Current.Server.MapPath(NewFilePath);

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(PhysicalNewFilePath);
                string fileExtension = fileInfo.Extension;
                int fileSize = Convert.ToInt32(fileInfo.Length);

                Eagle.Core.ApplicationFile entity = Find(FileId);
                if (entity != null)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {

                        if (FileContent != null && FileContent.Length > 0)
                        {
                            string OriginalFileName = entity.FileName;
                            int OriginalFolderId = entity.FolderId;
                            string OriginalFolderPath = "~" + FolderRepository.GetFolderPathByFolderId(OriginalFolderId);
                            string PhysicalOriginalFolderPath = HttpContext.Current.Server.MapPath(OriginalFolderPath);
                            if (!Directory.Exists(PhysicalOriginalFolderPath))
                                Directory.CreateDirectory(PhysicalOriginalFolderPath);
                            string PhysicalOldFilePath = System.IO.Path.Combine(PhysicalOriginalFolderPath, OriginalFileName);
                            if (PhysicalOldFilePath != string.Empty && System.IO.File.Exists(PhysicalOldFilePath))
                                System.IO.File.Delete(PhysicalOldFilePath);

                            entity.FileContent = FileContent;
                        }
                        entity.FileName = FileName;
                        entity.FileTitle = FileTitle;
                        entity.FileDescription = FileDescription;
                        entity.Extension = fileExtension;
                        entity.ContentType = GetMimeType(FileName);
                        entity.FolderId = SelectedFolderId;
                        entity.Size = fileSize;
                        entity.Width = Width;
                        entity.Height = Height;
                        entity.LastModifiedByUserId = LastModifiedByUserId;
                        entity.LastModifiedOnDate = DateTime.Now;

                        context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        int affectedRow = context.SaveChanges();
                        if (affectedRow > 0)
                        {
                            oFileId = entity.FileId;
                            flag = true;
                        }
                    }
                }
                else
                {
                    flag = InsertData(FolderKey, FileTitle, FileName, FileDescription, FileContent, Width, Height, LastModifiedByUserId, out oFileId);
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return flag;
        }
        
        public static bool UpdateFileList(int ItemId, string ItemTag, string Added_FileIds, out string Message)
        {
            bool result = false;
            Message = string.Empty;            
            try
            {
                switch (ItemTag)
                {
                    case "Qualification":
                        QualificationRespository.UpdateFileIds(ItemId, Added_FileIds, out Message);
                        break;
                    case "Contract":
                        ContractRespository.UpdateFileIds(ItemId, Added_FileIds, out Message);
                        break;
                }              
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
            }
            return result;
        }
        
        public static bool UpdateFileId(int ItemId, string ItemTag, int FileId)
        {
            bool result = false;
            try
            {
                switch (ItemTag)
                {
                    case "Employee":
                        EmployeeRepository.UpdateFileId(ItemId, FileId);
                        break;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
            }
            return result;
        }
        
        public static bool Delete(int? id, out string message)
        {
            bool result = false;
            message = string.Empty;
            if (id != null && id > 0)
            {
                try
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        Eagle.Core.ApplicationFile entity = new Eagle.Core.ApplicationFile();
                        entity = Find(id);
                        if (entity != null)
                        {
                            context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                            int affectedRow = context.SaveChanges();
                            if (affectedRow > 0)
                            {
                                string FolderPath = FolderRepository.GetFolderPathByFolderId(entity.FolderId);
                                string physicalFilePath = HttpContext.Current.Server.MapPath("~/" + FolderPath + "/" + entity.FileName);
                                System.IO.File.Delete(physicalFilePath);
                                result = true;
                                message = Eagle.Resource.LanguageResource.DeleteSuccess;
                            }
                        }
                        else
                        {
                            message = Eagle.Resource.LanguageResource.IDNoExistsErrorMessage;
                        }
                    }
                }
                catch (Exception ex){ message = ex.Message; }
            }
            return result;
        }

        public static bool DeleteFileInFileList(int ItemId, string ItemTag, int FileId, out string FileIds, out string Message)
        {
            bool result = false;
            Message = string.Empty;
            FileIds = string.Empty;
            try
            {
                result = Delete(FileId, out Message);
                switch (ItemTag)
                {
                    case "Qualification":
                        result = QualificationRespository.UpdateFileListAfterDeletingFile(ItemId, FileId, out FileIds);
                        break;
                    case "Contract":
                        result = ContractRespository.UpdateFileListAfterDeletingFile(ItemId, FileId, out FileIds);
                        break;
                }
                
            }
            catch (Exception ex) { ex.ToString();}
            return result;
        }
    }
}
