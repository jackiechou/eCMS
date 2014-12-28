using Eagle.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Eagle.Model.SYS;

namespace Eagle.Repository.SYS.FileManager
{
    public class FolderRepository
    {
        public EntityDataContext context { get; set; }
        public FolderRepository(EntityDataContext context)
        {
            this.context = context;
        }
        
       public string GetMappedDirectory(string VirtualDirectory)
        {
            string MappedDir = string.Empty;
            try
            {
                if (String.IsNullOrEmpty(VirtualDirectory))
                    MappedDir = AddTrailingSlash(Path.GetFullPath(VirtualDirectory));
            }
            catch (Exception exc)
            {
                exc.ToString();
            }
            return MappedDir;
        }
        private static void WriteStream(HttpResponse objResponse, Stream objStream)
        {
            byte[] bytBuffer = new byte[10000];
            int intLength;
            long lngDataToRead;
            try
            {
                lngDataToRead = objStream.Length;
                while (lngDataToRead > 0)
                {
                    if (objResponse.IsClientConnected)
                    {
                        intLength = objStream.Read(bytBuffer, 0, 10000);
                        objResponse.OutputStream.Write(bytBuffer, 0, intLength);
                        objResponse.Flush();

                        lngDataToRead = lngDataToRead - intLength;
                    }
                    else
                    {
                        lngDataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.Write("Error : " + ex.Message);
            }
            finally
            {
                if (objStream != null)
                {
                    objStream.Close();
                    objStream.Dispose();
                }
            }
        }
        public static string AddTrailingSlash(string strSource)
        {
            if (!strSource.EndsWith("\\"))
                strSource = strSource + "\\";
            return strSource;
        }
        public static string RemoveTrailingSlash(string strSource)
        {
            if (String.IsNullOrEmpty(strSource))
                return "";
            if (strSource.Substring(strSource.Length - 1, 1) == "\\" || strSource.Substring(strSource.Length - 1, 1) == "/")
            {
                return strSource.Substring(0, strSource.Length - 1);
            }
            else
            {
                return strSource;
            }
        }
        public static string StripFolderPath(string strOrigPath)
        {
            if (strOrigPath.IndexOf("\\") != -1)
            {
                return Regex.Replace(strOrigPath, "^0\\\\", "");
            }
            else
            {
                return strOrigPath.StartsWith("0") ? strOrigPath.Substring(1) : strOrigPath;
            }
        }
        public static string FormatFolderPath(string strFolderPath)
        {
            if (String.IsNullOrEmpty(strFolderPath))
            {
                return "";
            }
            else
            {
                if (strFolderPath.EndsWith("/"))
                {
                    return strFolderPath;
                }
                else
                {
                    return strFolderPath + "/";
                }
            }
        }

        public ApplicationFolder Find(int id)
        {
            ApplicationFolder entity = new ApplicationFolder();
            try
            {
                entity = (from f in context.ApplicationFolders
                          where f.FolderId == id
                          select f).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return entity;
        }

        public int GetFolderIdByFolderKey(string FolderKey)
        {
            int FolderId = (from f in context.ApplicationFolders
                            where f.FolderKey == FolderKey
                            select f.FolderId).FirstOrDefault();
            return FolderId;
        }
        public static string GetFolderPathByFolderId(int FolderId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                string FolderPath = (from f in context.ApplicationFolders
                                     where f.FolderId == FolderId
                                     select f.FolderPath).FirstOrDefault();

                if (!string.IsNullOrEmpty(FolderPath))
                {
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(FolderPath)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(FolderPath));
                }
                return FolderPath;
            }
        }
        public static string GetFolderPathByFolderKey(string FolderKey)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                string FolderPath = (from f in context.ApplicationFolders
                                     where f.FolderKey == FolderKey
                                     select f.FolderPath).FirstOrDefault();
                if (!string.IsNullOrEmpty(FolderPath))
                {
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(FolderPath)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(FolderPath));
                }
                return FolderPath;
            }
        }

        public static ApplicationFolder GetFolderInfoByFolderKey(string FolderKey)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                ApplicationFolder entity = (from f in context.ApplicationFolders
                                 where f.FolderKey == FolderKey
                                 select f).FirstOrDefault();
                return entity;
            }
        }


        public List<FolderViewModel> GetList()
        {
            List<FolderViewModel> lst = new List<FolderViewModel>();
            try
            {
                lst = (from f in context.ApplicationFolders
                       select new FolderViewModel()
                       {
                           FolderId = f.FolderId,
                           FolderKey = f.FolderKey,
                           FolderPath = f.FolderPath,
                           CreatedByUserId = f.CreatedByUserId,
                           CreatedOnDate = f.CreatedOnDate,
                           LastModifiedByUserId = f.LastModifiedByUserId,
                           LastModifiedOnDate = f.LastModifiedOnDate
                       }).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lst;
        }

        public FolderViewModel GeDetails(int id)
        {
            FolderViewModel entity = new FolderViewModel();
            try
            {
                entity = (from f in context.ApplicationFolders
                          where f.FolderId == id
                       select new FolderViewModel()
                       {
                           FolderId = f.FolderId,
                           FolderKey = f.FolderKey,
                           FolderPath = f.FolderPath,
                           CreatedByUserId = f.CreatedByUserId,
                           CreatedOnDate = f.CreatedOnDate,
                           LastModifiedByUserId = f.LastModifiedByUserId,
                           LastModifiedOnDate = f.LastModifiedOnDate
                       }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return entity;
        }

        public static FolderViewModel GeDetailsByFolderKey(string FolderKey)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                FolderViewModel entity = new FolderViewModel();
                try
                {
                    entity = (from f in context.ApplicationFolders
                              where f.FolderKey == FolderKey
                              select new FolderViewModel()
                              {
                                  FolderId = f.FolderId,
                                  FolderKey = f.FolderKey,
                                  FolderPath = f.FolderPath,
                                  CreatedByUserId = f.CreatedByUserId,
                                  CreatedOnDate = f.CreatedOnDate,
                                  LastModifiedByUserId = f.LastModifiedByUserId,
                                  LastModifiedOnDate = f.LastModifiedOnDate
                              }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                return entity;
            }
        }

        public bool IsExists(int ID)
        {
            var query = context.ApplicationFolders.FirstOrDefault(p => p.FolderId.Equals(ID));
            return (query != null);
        }

        public int Add(FileViewModel add_model, int? CreatedByUserId, out string outMessage)
        {
            int FileId = 0;
            outMessage = string.Empty;
            try
            {
                bool isDuplicate = IsExists(add_model.FolderId);
                if (isDuplicate == false)
                {
                    ApplicationFolder model = new ApplicationFolder();

                    model.FolderKey = add_model.FolderKey;
                    model.FolderPath = add_model.FolderPath;
                    model.CreatedByUserId = CreatedByUserId;
                    model.CreatedOnDate = DateTime.Now;

                    int affectedRow = 0;
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    affectedRow = context.SaveChanges();
                    if (affectedRow > 0)
                    {
                        FileId = model.FolderId;
                        outMessage = Eagle.Resource.LanguageResource.CreateSuccess;
                    }
                    else
                    {
                        outMessage = Eagle.Resource.LanguageResource.CreateFailure;
                    }
                }
            }
            catch (Exception ex) { ex.ToString(); }
            return FileId;
        }

        public int Update(FileViewModel edit_model, int? LastModifiedByUserId, out string outMessage)
        {
            outMessage = string.Empty;
            int affectedRow = 0;
            try
            {
                ApplicationFolder entity = Find(edit_model.FileId);
                entity.FolderKey = edit_model.FolderKey;
                entity.FolderPath = edit_model.FolderPath;
                entity.LastModifiedByUserId = LastModifiedByUserId;
                entity.LastModifiedOnDate = DateTime.Now;

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
            catch (Exception ex)
            {
                outMessage = ex.Message;
                affectedRow = -2;
            }
            return affectedRow;
        }
        public bool Delete(int id)
        {
            try
            {
                ApplicationFolder entity = Find(id);
                context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return false;
            }

        }
    }
}
