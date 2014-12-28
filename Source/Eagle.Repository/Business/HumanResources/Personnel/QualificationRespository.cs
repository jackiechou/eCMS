using Eagle.Common.Data;
using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Model.HR;
using Eagle.Model.SYS;

namespace Eagle.Repository.HR
{
    public class QualificationRespository
    {
         public EntityDataContext context { get; set; }

        public QualificationRespository(EntityDataContext context)
        {
            this.context = context;
        }

        public static List<int> GetFileIds(int id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<int> FileIds = new List<int>();
                if (id > 0)
                {
                    string strFileIds = (from u in context.HR_tblQualification where u.QualificationID == id select u.FileIds).FirstOrDefault();
                    if (!string.IsNullOrEmpty(strFileIds))
                        FileIds = strFileIds.Split(',').Select(s => int.Parse(s)).ToList();

                }
                return FileIds;
            }            
        }

        public static string GenerateQualificationNo(int num)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                System.Nullable<Int32> Max_ID = (from u in context.HR_tblQualification select u.QualificationID).DefaultIfEmpty(0).Max() + 1;
                string _MaxID = Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
                return _MaxID;
            }
        }

        public static string GenerateQualificationNo(int num, string sid)
        {  
            using (EntityDataContext context = new EntityDataContext())
            {
                System.Nullable<Int32> Max_ID = (from u in context.HR_tblQualification select u.QualificationID).DefaultIfEmpty(0).Max() + 1;
                string number = Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
                return string.Format("{0}-{1}", number, sid.Substring(0, 5).ToUpper());
            }
        }

        public static int GeneratePriority(int? EmpID)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                int Max_ID = 0;
                if (EmpID != null && EmpID > 0)
                    Max_ID = (from u in context.HR_tblQualification where u.EmpID == EmpID select u.Priority).DefaultIfEmpty(0).Max() + 1;
                else
                    Max_ID = (from u in context.HR_tblQualification select u.Priority).DefaultIfEmpty(0).Max() + 1;
                return Max_ID;
            }
        }

        public List<QualificationViewModel> GetList(int? EmpID, int? ModuleId, string UserName, bool isAdmin)
        {
            List<QualificationViewModel> lst = new List<QualificationViewModel>();
            if (EmpID != null && EmpID != 0)
            {
                var jointable = context.SYS_spfrmDataPermission(UserName, ModuleId).ToList();
                lst = (from p in context.HR_tblQualification
                             join e in context.HR_tblEmp on p.EmpID equals e.EmpID into emplist
                             from emp in emplist.DefaultIfEmpty()
                            where p.EmpID == EmpID && (isAdmin == true || jointable.Contains(p.EmpID))     
                             select new QualificationViewModel()
                             {
                                 QualificationID = p.QualificationID,
                                 EmpID = p.EmpID,
                                 EmpName = emp.LastName + " - " + emp.FirstName,
                                 QualificationNo = p.QualificationNo,
                                 LSQualificationID = p.LSQualificationID,
                                 QualificationDate = p.QualificationDate,
                                 FromMonth = p.FromMonth,
                                 ToMonth = p.ToMonth,
                                 LSSchoolID = p.LSSchoolID,
                                 OtherSchool = p.OtherSchool,
                                 LSFacultyID = p.LSFacultyID,
                                 OtherFaculty = p.OtherFaculty,
                                 LSMajorID = p.LSMajorID,
                                 LSTrainingTypeID = p.LSTrainingTypeID,
                                 PayByCompany = p.PayByCompany,
                                 TrainingPlace = p.TrainingPlace,
                                 LSCountryID = p.LSFacultyID,
                                 Note = p.Note,
                                 FileIds = p.FileIds,
                                 InitialFileIds = p.FileIds,
                                 Priority = p.Priority
                             }).OrderBy(p => p.Priority).ToList();
            }          
            return lst;
        }

        public List<QualificationViewModel> GetListById(int? EmpID, int? LanguageId)
        {
            List<QualificationViewModel> lst = new List<QualificationViewModel>();
            if (EmpID != null && EmpID != 0)
            {                
                lst = (from p in context.HR_tblQualification
                       join q in context.LS_tblQualification on p.LSQualificationID equals q.LSQualificationID into qualificationlist
                       from qualification in qualificationlist.DefaultIfEmpty()
                       join e in context.HR_tblEmp on p.EmpID equals e.EmpID into emplist
                       from emp in emplist.DefaultIfEmpty()
                       where p.EmpID == EmpID
                       select new QualificationViewModel()
                       {
                           QualificationID = p.QualificationID,
                           EmpID = p.EmpID,
                           EmpName = emp.FirstName + " - " + emp.LastName,
                           QualificationNo = p.QualificationNo,
                           LSQualificationID = p.LSQualificationID,
                           LSQualificationName = (LanguageId == 124)?qualification.Name:qualification.VNName,
                           QualificationDate = p.QualificationDate,
                           FromMonth = p.FromMonth,
                           ToMonth = p.ToMonth,
                           LSSchoolID = p.LSSchoolID,
                           OtherSchool = p.OtherSchool,
                           LSFacultyID = p.LSFacultyID,
                           OtherFaculty = p.OtherFaculty,
                           LSMajorID = p.LSMajorID,
                           LSTrainingTypeID = p.LSTrainingTypeID,
                           PayByCompany = p.PayByCompany,
                           TrainingPlace = p.TrainingPlace,
                           LSCountryID = p.LSFacultyID,
                           Note = p.Note,
                           FileIds = p.FileIds,
                           InitialFileIds = p.FileIds,
                           Priority = p.Priority
                       }).OrderBy(p => p.Priority).ToList();
            }
            return lst;
        }
                
        public QualificationViewModel Details(int id, int? LanguageId)
        {
            try
            {
                var query = (from p in context.HR_tblQualification
                             join s in context.LS_tblSchool on p.LSSchoolID equals s.LSSchoolID into school
                             from s in school.DefaultIfEmpty()

                             join f in context.LS_tblFaculty on p.LSFacultyID equals f.LSFacultyID into faculty
                             from f in faculty.DefaultIfEmpty()

                             join m in context.LS_tblMajor on p.LSMajorID equals m.LSMajorID into major
                             from m in major.DefaultIfEmpty()

                             join t in context.LS_tblTrainingType on p.LSTrainingTypeID equals t.LSTrainingTypeID into training_type
                             from t in training_type.DefaultIfEmpty()

                             join c in context.LS_tblCountry on p.LSCountryID equals c.LSCountryID into country
                             from c in country.DefaultIfEmpty()                             

                             where p.QualificationID == id
                             select new QualificationViewModel()
                             {
                                 QualificationID = p.QualificationID,
                                 EmpID = p.EmpID,                               
                                 QualificationNo = p.QualificationNo,
                                 LSQualificationID = p.LSQualificationID,
                                 QualificationDate = p.QualificationDate,

                                 FromMonth = p.FromMonth,
                                 ToMonth = p.ToMonth,

                                 LSSchoolID = p.LSSchoolID,
                                 LSSchoolName = (LanguageId == 124)?s.Name:s.VNName,
                                 OtherSchool = p.OtherSchool,

                                 LSFacultyID = p.LSFacultyID,
                                 LSFacultyName = (LanguageId == 124)?f.Name:f.VNName,
                                 OtherFaculty = p.OtherFaculty,

                                 LSMajorID = p.LSMajorID,
                                 LSMajorName = (LanguageId == 124)?m.Name:m.VNName,

                                 LSTrainingTypeID = p.LSTrainingTypeID,
                                 LSTrainingTypeName = (LanguageId == 124)?t.Name:t.VNName,

                                 PayByCompany = p.PayByCompany,
                                 TrainingPlace = p.TrainingPlace,
                                 LSCountryID = p.LSCountryID,
                                 LSCountryName = (LanguageId == 124)?c.Name:c.VNName,

                                 Note = p.Note,
                                 FileIds = p.FileIds,
                                 InitialFileIds = p.FileIds,
                                 Priority = p.Priority
                             }).FirstOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return new QualificationViewModel();
            }
        }

        public QualificationEditModel GetDetails(int id, int? LanguageId)
        {          
            QualificationEditModel entity = new QualificationEditModel();
            entity = (from p in context.HR_tblQualification

                      join s in context.LS_tblSchool on p.LSSchoolID equals s.LSSchoolID into school
                      from s in school.DefaultIfEmpty()

                      join f in context.LS_tblFaculty on p.LSFacultyID equals f.LSFacultyID into faculty
                      from f in faculty.DefaultIfEmpty()

                      join m in context.LS_tblMajor on p.LSMajorID equals m.LSMajorID into major
                      from m in major.DefaultIfEmpty()

                      join t in context.LS_tblTrainingType on p.LSTrainingTypeID equals t.LSTrainingTypeID into training_type
                      from t in training_type.DefaultIfEmpty()

                      join c in context.LS_tblCountry on p.LSCountryID equals c.LSCountryID into country
                      from c in country.DefaultIfEmpty()

                      join q in context.LS_tblQualification on p.LSQualificationID equals q.LSQualificationID into qualification
                      from q in qualification.DefaultIfEmpty()

                      where p.QualificationID == id
                      select new QualificationEditModel()
                      {
                          QualificationID = p.QualificationID,
                          EmpID = p.EmpID,
                          QualificationNo = p.QualificationNo,
                          LSQualificationID = p.LSQualificationID,
                          LSQualificationName = (LanguageId == 124) ? q.Name : q.VNName,
                          QualificationDate = p.QualificationDate,

                          FromMonth = p.FromMonth,
                          ToMonth = p.ToMonth,

                          LSSchoolID = p.LSSchoolID,
                          LSSchoolName = (LanguageId == 124) ? s.Name : s.VNName,
                          OtherSchool = p.OtherSchool,

                          LSFacultyID = p.LSFacultyID,
                          LSFacultyName = (LanguageId == 124) ? f.Name : f.VNName,
                          OtherFaculty = p.OtherFaculty,

                          MajorID = p.LSMajorID,
                          LSMajorName = (LanguageId == 124) ? m.Name : m.VNName,

                          LSTrainingTypeID = p.LSTrainingTypeID,
                          LSTrainingTypeName = (LanguageId == 124) ? t.Name : t.VNName,

                          PayByCompany = p.PayByCompany,
                          TrainingPlace = p.TrainingPlace,
                          LSCountryID = p.LSCountryID,
                          LSCountryName = (LanguageId == 124) ? c.Name : c.VNName,

                          Note = p.Note,
                          FileIds = p.FileIds,
                          InitialFileIds = p.FileIds,
                          Priority = p.Priority
                      }).FirstOrDefault();

            return entity;
        }

        public bool IsPriorityExists(int EmpID, int Priority)
        {
            var query = context.HR_tblQualification.Where(x => x.Priority == Priority && x.EmpID == EmpID);
            bool result = (query.Count() > 0) ? true : false;
            return result;
        }

        public bool IsDataExisted(string QualificationNo, int LSQualificationID, int EmpID)
        {
            var query = context.HR_tblQualification.FirstOrDefault(c => c.QualificationNo.ToUpper().Equals(QualificationNo.ToUpper())
                && c.LSQualificationID == LSQualificationID && c.EmpID == EmpID
                );
            return (query != null);
        }

        public static bool IsIDExisted(int ID)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.HR_tblQualification.FirstOrDefault(p => p.QualificationID.Equals(ID));
                return (query != null);
            }
        }

        public static bool IsCodeExisted(string Code)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = from p in context.HR_tblQualification where p.QualificationNo.ToLower().Trim() == Code.ToLower().Trim() select p;
                int i = query.Count();                
                bool result = (query.Count() > 0);
                return result;
            }
        }

        public static HR_tblQualification Find(int id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                HR_tblQualification entity = new HR_tblQualification();
                if(id > 0)
                    entity = context.HR_tblQualification.Find(id);
                return entity;
            }
        }
               
        public static bool UpdateListOrder(int id, int listOrder, out string messsage)
        {
            bool result = false;
            messsage = string.Empty;
            try
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    HR_tblQualification entity = Find(id);
                    entity.Priority = listOrder;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int i = context.SaveChanges();
                    if (i == 1)
                    {
                        result = true;
                        messsage = Eagle.Resource.LanguageResource.UpdateSuccess;
                    }
                }
            }
            catch (Exception ex)
            {
                messsage = ex.Message;
                result = false;
            }
            return result;
        }
        
        public HR_tblQualification Find(int? id)
        {
            return context.HR_tblQualification.Find(id);
        }

        public bool Add(QualificationViewModel add_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool isDuplicate = IsDataExisted(add_model.QualificationNo,add_model.LSQualificationID,add_model.EmpID);
                if (isDuplicate == false)
                {
                    bool isQualificationNoExist = IsCodeExisted(add_model.QualificationNo);
                    if (isQualificationNoExist)
                        add_model.QualificationNo = GenerateQualificationNo(10);

                    HR_tblQualification model = new HR_tblQualification();
                    model.EmpID = add_model.EmpID;
                    model.QualificationNo = add_model.QualificationNo;
                    model.LSQualificationID = add_model.LSQualificationID;
                    model.LSSchoolID = add_model.LSSchoolID;
                    model.OtherSchool = add_model.OtherSchool;
                    model.LSFacultyID = add_model.LSFacultyID;
                    model.OtherFaculty = add_model.OtherFaculty;
                    model.LSMajorID = add_model.LSMajorID;
                    model.LSTrainingTypeID = add_model.LSTrainingTypeID;
                    model.PayByCompany = add_model.PayByCompany;
                    model.TrainingPlace = add_model.TrainingPlace;
                    model.LSCountryID = add_model.LSCountryID;
                    model.Note = add_model.Note;
                    model.FileIds = add_model.FileIds;
                    model.Priority = add_model.Priority;

                    if (add_model.QualificationDate != null && add_model.QualificationDate.ToString() != string.Empty)
                        model.QualificationDate = Convert.ToDateTime(add_model.QualificationDate.ToString());   

                    if (add_model.FromMonth != null && add_model.FromMonth.ToString() != string.Empty)
                        model.FromMonth = Convert.ToDateTime(add_model.FromMonth.ToString());

                    if (add_model.ToMonth != null && add_model.ToMonth.ToString() != string.Empty)
                        model.ToMonth = Convert.ToDateTime(add_model.ToMonth.ToString());
                   
                    int affectedRow = 0;
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    affectedRow = context.SaveChanges();
                    if (affectedRow == 1)
                    {
                        add_model.QualificationID = model.QualificationID;
                        Message = Eagle.Resource.LanguageResource.CreateSuccess;
                        result = true;                       
                    }
                }
                else
                {
                    result = false;
                    Message = Eagle.Resource.LanguageResource.DuplicateError;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                Message = Eagle.Resource.LanguageResource.SystemError;
            }
            return result;
        }

        public bool Edit(QualificationViewModel edit_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                HR_tblQualification model = Find(edit_model.QualificationID);
                if (model.QualificationID > 0)
                {
                    List<string> AddedFileIdList = edit_model.FileIds.Split(',').ToList();
                    List<string> InitialFileIdList = edit_model.InitialFileIds.Split(',').ToList();
                    List<string> FileIdList = InitialFileIdList.Union(AddedFileIdList).ToList();
                    string FileIds = string.Join(",", FileIdList);

                    model.EmpID = edit_model.EmpID;
                    model.QualificationNo = edit_model.QualificationNo;
                    model.LSQualificationID = edit_model.LSQualificationID;
                    model.LSSchoolID = edit_model.LSSchoolID;
                    model.OtherSchool = edit_model.OtherSchool;
                    model.LSFacultyID = edit_model.LSFacultyID;
                    model.OtherFaculty = edit_model.OtherFaculty;
                    model.LSMajorID = edit_model.LSMajorID;
                    model.LSTrainingTypeID = edit_model.LSTrainingTypeID;
                    model.PayByCompany = edit_model.PayByCompany;
                    model.TrainingPlace = edit_model.TrainingPlace;
                    model.LSCountryID = edit_model.LSCountryID;
                    model.Note = edit_model.Note;
                    model.FileIds = edit_model.FileIds;
                    model.Priority = edit_model.Priority;

                    if (edit_model.QualificationDate != null && edit_model.QualificationDate.ToString() != string.Empty)
                        model.QualificationDate = Convert.ToDateTime(edit_model.QualificationDate.ToString());

                    if (edit_model.FromMonth != null && edit_model.FromMonth.ToString() != string.Empty)
                        model.FromMonth = Convert.ToDateTime(edit_model.FromMonth.ToString());

                    if (edit_model.ToMonth != null && edit_model.ToMonth.ToString() != string.Empty)
                        model.ToMonth = Convert.ToDateTime(edit_model.ToMonth.ToString());

                    model.Note = edit_model.Note;
                    context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    int affectedRows = context.SaveChanges();
                    if (affectedRows == 1)
                    {
                        Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                Message = Eagle.Resource.LanguageResource.SystemError;
            }
            return result;
        }

        public bool Insert(QualificationCreateModel add_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool isDuplicate = IsIDExisted(add_model.QualificationID);
                if (isDuplicate == false)
                {
                    HR_tblQualification model = new HR_tblQualification();
                    model.EmpID = add_model.EmpID;
                    model.QualificationNo = add_model.QualificationNo;
                    model.LSQualificationID = add_model.LSQualificationID;
                    model.LSSchoolID = add_model.LSSchoolID;
                    model.OtherSchool = add_model.OtherSchool;
                    model.LSFacultyID = add_model.LSFacultyID;
                    model.OtherFaculty = add_model.OtherFaculty;
                    model.LSMajorID = add_model.MajorID;
                    model.LSTrainingTypeID = add_model.LSTrainingTypeID;
                    model.PayByCompany = add_model.PayByCompany;
                    model.TrainingPlace = add_model.TrainingPlace;
                    model.LSCountryID = add_model.LSCountryID;
                    model.Note = add_model.Note;
                    model.FileIds = add_model.FileIds;
                    model.Priority = add_model.Priority;

                    if (add_model.QualificationDate != null && add_model.QualificationDate.ToString() != string.Empty)
                        model.QualificationDate = Convert.ToDateTime(add_model.QualificationDate.ToString());

                    if (add_model.FromMonth != null && add_model.FromMonth.ToString() != string.Empty)
                        model.FromMonth = Convert.ToDateTime(add_model.FromMonth.ToString());

                    if (add_model.ToMonth != null && add_model.ToMonth.ToString() != string.Empty)
                        model.ToMonth = Convert.ToDateTime(add_model.ToMonth.ToString());

                    int affectedRow = 0;
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    affectedRow = context.SaveChanges();
                    if (affectedRow == 1)
                    {
                        add_model.QualificationID = model.QualificationID;
                        Message = Eagle.Resource.LanguageResource.CreateSuccess;
                        result = true;
                    }
                }
                else
                {
                    result = false;
                    Message = Eagle.Resource.LanguageResource.DuplicateError;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                Message = Eagle.Resource.LanguageResource.SystemError;
            }
            return result;
        }

        public bool Update(QualificationEditModel edit_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {

                HR_tblQualification model = Find(edit_model.QualificationID);
                if (model.QualificationID > 0)
                {
                    //List<string> AddedFileIdList = new List<string>();
                    //List<string> InitialFileIdList = new List<string>();
                    //List<string> FileIdList = new List<string>();

                    //if (!string.IsNullOrEmpty(edit_model.FileIds))
                    //    edit_model.FileIds.Split(',').ToList();
                    //if (!string.IsNullOrEmpty(model.FileIds))
                    //    InitialFileIdList = model.FileIds.Split(',').ToList();
                    //FileIdList = InitialFileIdList.Union(AddedFileIdList).ToList();
                    //string FileIds = string.Join(",", FileIdList);

                    model.EmpID = edit_model.EmpID;
                    model.QualificationNo = edit_model.QualificationNo;
                    model.LSQualificationID = edit_model.LSQualificationID;
                    model.LSSchoolID = edit_model.LSSchoolID;
                    model.OtherSchool = edit_model.OtherSchool;
                    model.LSFacultyID = edit_model.LSFacultyID;
                    model.OtherFaculty = edit_model.OtherFaculty;
                    model.LSMajorID = edit_model.MajorID;
                    model.LSTrainingTypeID = edit_model.LSTrainingTypeID;
                    model.PayByCompany = edit_model.PayByCompany;
                    model.TrainingPlace = edit_model.TrainingPlace;
                    model.LSCountryID = edit_model.LSCountryID;
                    model.Note = edit_model.Note;
                    model.FileIds = model.FileIds;
                    model.Priority = edit_model.Priority;

                    if (edit_model.QualificationDate != null && edit_model.QualificationDate.ToString() != string.Empty)
                        model.QualificationDate = Convert.ToDateTime(edit_model.QualificationDate.ToString());

                    if (edit_model.FromMonth != null && edit_model.FromMonth.ToString() != string.Empty)
                        model.FromMonth = Convert.ToDateTime(edit_model.FromMonth.ToString());

                    if (edit_model.ToMonth != null && edit_model.ToMonth.ToString() != string.Empty)
                        model.ToMonth = Convert.ToDateTime(edit_model.ToMonth.ToString());

                    model.Note = edit_model.Note;
                    context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    int affectedRows = context.SaveChanges();
                    if (affectedRows == 1)
                    {
                        Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                Message = Eagle.Resource.LanguageResource.SystemError;
            }
            return result;
        }

        public bool Delete(int? id, out string Message)
        {
            bool result = false;
            Message = string.Empty;
            try
            {
                HR_tblQualification model = Find(id);
                if (model != null)
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                    Message = Eagle.Resource.LanguageResource.DeleteSuccess;
                    result = true;
                }
                else
                {
                    Message = Eagle.Resource.LanguageResource.IDNoExistsErrorMessage;
                    result = false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                Message = Eagle.Resource.LanguageResource.DeleteFailure;
                result = false;
            }

            return result;
        }

        #region FILE MANAGER ============================================================
        public static List<int> GetFileList(int id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var listofFileIds = (from u in context.HR_tblQualification where u.QualificationID == id select u.FileIds).ToList();
                return listofFileIds.Select(s => int.Parse(s)).ToList();
            }
        }

        public static List<FileViewModel> GetDownloadedFileList(int id)
        {
            List<FileViewModel> lst = new List<FileViewModel>();
            List<int> FileIdList = GetFileList(id);
            string FileIds = string.Join(",", FileIdList);
            if (FileIdList.Count > 0)
            {
                using (EntityDataContext context = new EntityDataContext())
                {                
                    foreach (var item in FileIdList)
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
                }
            }        
            return lst;
        }

        public static List<FileViewModel> GetDownloadFileList(string FileIds)
        {
            List<FileViewModel> lst = new List<FileViewModel>();
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
                    }
                }
            }
            return lst;
        }

        public static bool UpdateFileListAfterDeletingFile(int Id, int FileId, out string FileIds)
        {       
            bool result = false;
            FileIds = string.Empty;
            try
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    HR_tblQualification entity = Find(Id);
                    List<string> FileIdList = entity.FileIds.Split(',').ToList();
                    FileIdList.Remove(FileId.ToString());
                    FileIds = string.Join(",", FileIdList);

                    entity.FileIds = FileIds;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int affectedRows = context.SaveChanges();
                    if (affectedRows == 1)
                        result = true;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }
        
        public static bool AddFileToFileList(int FileId, string OriginalFileIds, out string ModifiedFileIds)
        {
            ModifiedFileIds = string.Empty;
            bool result = false;
            try
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    HR_tblQualification entity = context.HR_tblQualification.Where(x => x.FileIds == OriginalFileIds).FirstOrDefault();
                    List<string> FileIdList = entity.FileIds.Split(',').ToList(); 
                    FileIdList.Add(FileId.ToString());

                    //Update FileIds
                    entity.FileIds = string.Join(",", FileIdList);
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int affectedRows = context.SaveChanges();
                    if (affectedRows == 1)
                    {
                        ModifiedFileIds = Eagle.Resource.LanguageResource.UpdateSuccess;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                ModifiedFileIds = Eagle.Resource.LanguageResource.SystemError;
            }
            return result;
        }

        public static bool UpdateFileIds(int Id, string Added_FileIds, out string FileIds)
        {
            FileIds = string.Empty;
            bool result = false;
            try
            {
                List<string> AddedFileIdList = new List<string>();
                List<string> InitialFileIdList = new List<string>();
                HR_tblQualification entity = Find(Id);
                if (!(string.IsNullOrEmpty(entity.FileIds)))
                    InitialFileIdList = entity.FileIds.Trim().Split(',').ToList();
                if (!(string.IsNullOrEmpty(Added_FileIds)))
                    AddedFileIdList = Added_FileIds.Trim().Split(',').ToList();
                List<string> FileIdList = InitialFileIdList.Union(AddedFileIdList).ToList();
                FileIds = string.Join(",", FileIdList);
                entity.FileIds = FileIds;

                using (EntityDataContext context = new EntityDataContext())
                {    
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int affectedRows = context.SaveChanges();
                    if (affectedRows == 1)
                        result = true;
                }
            }
            catch (Exception ex) { ex.ToString();}
            return result;
        }

        public bool DeleteFileInFileList(int id, int FileId, out string FileIds)
        {
            bool result = false;
            FileIds = string.Empty;
            try
            {
                HR_tblQualification model = Find(id);
                if (model != null)
                {
                    List<int> FileIdList = GetFileList(id);
                    FileIdList.Remove(FileId);
                    FileIds = string.Join(",", FileIdList);

                    model.FileIds = FileIds;
                    context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    int affectedRows = context.SaveChanges();
                    if (affectedRows == 1)
                        result = true;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
            }
            return result;
        }
        #endregion  ==========================================================================
    }
}
