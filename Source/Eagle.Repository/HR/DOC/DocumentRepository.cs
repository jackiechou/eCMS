using AutoMapper;
using Eagle.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Eagle.Model;

namespace Eagle.Repository
{
    public class DocumentRepository
    {
        public EntityDataContext context { get; set; }
        public DocumentRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public DOC_tblDocument Find(int? DocumentId)
        {
            return context.DOC_tblDocument.Find(DocumentId);
        }
        public DocumentViewModel FindEdit(int id)
        {
            var model = (from doc in context.DOC_tblDocument
                         join empUpload in context.HR_tblEmp on doc.CreatedByUserId equals empUpload.EmpID into tmplist1
                         from empUpload in tmplist1.DefaultIfEmpty()

                         join empModified in context.HR_tblEmp on doc.LastModifiedByUserId equals empModified.EmpID into tmplist2
                         from empModified in tmplist2.DefaultIfEmpty()

                         select new DocumentViewModel()
                         {
                             DocumentId = doc.DocumentId,
                             LSReferenceId = doc.LSReferenceId,
                             FileTitle = doc.FileTitle,
                             FileNameVirtual = doc.FileNameVirtual,
                             FileNamePhysical = doc.FileNamePhysical,
                             Extension = doc.Extension,
                             ContentType = doc.ContentType,
                             //FolderId = doc.FolderId,
                             //FileContent = doc.FileContent,
                             FileDescription = doc.FileDescription,
                             Keywords = doc.Keywords,
                             Size = doc.Size,
                             CreatedByUserId = doc.CreatedByUserId,
                             CreatedOnDate = doc.CreatedOnDate,
                             LastModifiedByUserId = doc.LastModifiedByUserId,
                             LastModifiedOnDate = doc.LastModifiedOnDate,
                             UserUpload = empUpload.LastName + " " + empUpload.FirstName,
                             UserModified = empModified.LastName + " " + empModified.FirstName
                         }).FirstOrDefault();

            return model;
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

        public bool Delete(int id, out string errorMessage)
        {
            try
            {
                var modelDelete = Find(id);
                context.Entry(modelDelete).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                errorMessage = "";
                return true;

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool Update(DOC_tblDocument modelUpdate, out string errorMessage)
        {
            try
            {
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                errorMessage = "";
                return true;

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream =
                   new System.IO.FileStream(_FileName, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                // Writes a block of bytes to this stream using data from
                // a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}",
                                  _Exception.ToString());
            }

            // error occured, return false
            return false;
        }



        public bool GetFilePath(int DocumentId, out string filePathOut, out string errorMessage)
        {
            //Document Model
            var documentModel = Find(DocumentId);
            //Nếu không có file báo lỗi
            if (documentModel == null)
            {
                filePathOut = "/";
                errorMessage = Eagle.Resource.LanguageResource.FileNotFound;
                return false;
            }
            else
            {
                //Folder Model
                var folderModel = context.ApplicationFolders.Where(p => p.FolderId == documentModel.FolderId).FirstOrDefault();
                //Nếu chưa có folder Báo lỗi
                if (folderModel == null)
                {
                    filePathOut = "/";
                    errorMessage = Eagle.Resource.LanguageResource.CouldNotFindFolder;
                    return false;
                }
                else
                {
                    //folder Path
                    string folderPath = HttpContext.Current.Server.MapPath("~" + folderModel.FolderPath);

                    //Nếu chưa có folder tạo folder
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                    }

                    //file Path 
                    string filePath = Path.Combine(folderPath, documentModel.FileNamePhysical);
                    //Nếu chưa có fle tạo file
                    if (!System.IO.File.Exists(filePath))
                    {
                        System.IO.File.WriteAllBytes(filePath, documentModel.FileContent);
                    }
                    //Tạo chuỗi đường dẫn http trả về 
                    string ret = folderModel.FolderPath + "/" + documentModel.FileNamePhysical;
                    filePathOut = ret;
                    errorMessage = "";
                    return true;
                }
            }
        }



        public List<DocumentViewModel> GetDocumentsBy(int? CandidateID, int LanguageId)
        {
            var list = from candidate in context.REC_tblCandidate
                       where candidate.CandidateID == CandidateID
                       from p in candidate.DOC_tblDocument
                       join ca in context.HR_tblEmp on p.CreatedByUserId equals ca.EmpID
                       join reference in context.DOC_tblDocumentReference on p.LSReferenceId equals reference.LSReferenceId into tmpList1
                       from re in tmpList1.DefaultIfEmpty()
                       select new DocumentViewModel()
                       {
                           DocumentId = p.DocumentId,
                           LSReferenceId = p.LSReferenceId,
                           FileTitle = p.FileTitle,
                           FileNameVirtual = p.FileNameVirtual,
                           FileNamePhysical = p.FileNamePhysical,
                           //Extension = p.Extension,
                           // ContentType = p.ContentType,
                           //FolderId = p.FolderId,
                           //FileContent = p.FileContent,
                           FileDescription = p.FileDescription,
                           //Keywords = p.Keywords,
                           //Size = p.Size,
                           //CreatedByUserId = p.CreatedByUserId,
                           CreatedOnDate = p.CreatedOnDate,
                           //LastModifiedByUserId = p.LastModifiedByUserId,
                           //LastModifiedOnDate = p.LastModifiedOnDate,
                           UserUpload = ca.LastName + " " + ca.FirstName,
                           ReferenceType = (re != null) ? ((LanguageId == 124) ? re.Name : re.VNName) : Eagle.Resource.LanguageResource.NoClassificats
                       };
            return list.ToList();
        }
    }
}
