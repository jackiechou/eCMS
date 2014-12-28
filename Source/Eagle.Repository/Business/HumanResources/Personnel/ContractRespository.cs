using Eagle.Common.Data;
using Eagle.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Model.HR;
using Eagle.Model.Report.HR.Contract;

namespace Eagle.Repository.HR
{
    public class ContractRespository
    {
        public EntityDataContext context { get; set; }

        public ContractRespository(EntityDataContext context)
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
                    string strFileIds = (from u in context.HR_tblContract where u.ContractID == id select u.FileIds).FirstOrDefault();
                    if (!string.IsNullOrEmpty(strFileIds))
                        FileIds = strFileIds.Split(',').Select(s => int.Parse(s)).ToList();
                }
                return FileIds;
            }
        }

        public string GenerateContractNo(int num)
        {
            System.Nullable<Int32> Max_ID = (from u in context.HR_tblContract select u.ContractID).DefaultIfEmpty(0).Max() + 1;
            string _MaxID = Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
            return _MaxID;
        }

        public string GenerateContractNo(int num, string sid)
        {
            System.Nullable<Int32> Max_ID = (from u in context.HR_tblContract select u.ContractID).DefaultIfEmpty(0).Max() + 1;
            string _MaxID = Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
            return string.Format("{0}-{1}", num, sid.Substring(0, 5).ToUpper());
        }

        public bool CheckExistCode(string Code)
        {
            var query = context.HR_tblContract.FirstOrDefault(p => p.ContractNo.Equals(Code));
            return (query != null);
        }

        public List<ContractViewModel> GetList(int? EmpID, int? ModuleId, string UserName, bool isAdmin, int? LanguageId)
        {
            List<ContractViewModel> lst = new List<ContractViewModel>();
            if (EmpID != null && EmpID !=0)
            {
                var jointable = context.SYS_spfrmDataPermission(UserName, ModuleId).ToList();
                lst = (from c in context.HR_tblContract
                             join t in context.LS_tblContractType on c.LSContractTypeID equals t.LSContractTypeID into type
                             from t in type.DefaultIfEmpty()
                             join e in context.HR_tblEmp on c.EmpID equals e.EmpID into employee
                             from e in employee.DefaultIfEmpty()
                             where c.EmpID == EmpID && (isAdmin == true || jointable.Contains(c.EmpID))                   
                             select new ContractViewModel()
                             {
                                 ContractID = c.ContractID,
                                 ContractNo = c.ContractNo,
                                 LSContractTypeID = c.LSContractTypeID,
                                 LSContractTypeName = (LanguageId==10001)? t.Name:t.VNName,
                                 ContractStatus = c.ContractStatus,
                                 EmpID = c.EmpID,
                                 EmpName = e.FirstName + " " + e.LastName,
                                 ProbationFrom = c.ProbationFrom,
                                 ProbationTo = c.ProbationTo,
                                 SignedDate = c.SignedDate,
                                 EffectiveDate = c.EffectiveDate,
                                 ExpiredDate = c.ExpiredDate,
                                 ProbationSalary = c.ProbationSalary,
                                 InsuranceSalary = c.InsuranceSalary,
                                 LSPositionID = c.LSPositionID,
                                 LSLocationID = c.LSLocationID,
                                 MethodPIT = c.MethodPIT,
                                 PercentPIT = c.PercentPIT,
                                 FileIds = c.FileIds,
                                 Note = c.Note,
                                 Creater = c.Creater,
                                 CreatedDate = c.CreatedDate
                             }).OrderByDescending(c => c.ContractID).ToList(); 
            }
           return lst;
        }

        public List<ContractViewModel> GetListByEmpID(int? EmpID, int? LanguageId)
        {
            List<ContractViewModel> lst = new List<ContractViewModel>();
            if (EmpID != null && EmpID != 0)
            {
                lst = (from c in context.HR_tblContract
                       join t in context.LS_tblContractType on c.LSContractTypeID equals t.LSContractTypeID into type
                       from t in type.DefaultIfEmpty()
                       join e in context.HR_tblEmp on c.EmpID equals e.EmpID into employee
                       from e in employee.DefaultIfEmpty()
                       where c.EmpID == EmpID
                       select new ContractViewModel()
                       {
                           ContractID = c.ContractID,
                           ContractNo = c.ContractNo,
                           LSContractTypeID = c.LSContractTypeID,
                           LSContractTypeName = (LanguageId == 124) ? t.Name : t.VNName,
                           ContractStatus = c.ContractStatus,
                           EmpID = c.EmpID,
                           EmpName = e.FirstName + " " + e.LastName,
                           ProbationFrom = c.ProbationFrom,
                           ProbationTo = c.ProbationTo,
                           SignedDate = c.SignedDate,
                           EffectiveDate = c.EffectiveDate,
                           ExpiredDate = c.ExpiredDate,
                           ProbationSalary = c.ProbationSalary,
                           InsuranceSalary = c.InsuranceSalary,
                           LSPositionID = c.LSPositionID,
                           LSLocationID = c.LSLocationID,
                           MethodPIT = c.MethodPIT,
                           PercentPIT = c.PercentPIT,
                           FileIds = c.FileIds,
                           Note = c.Note,
                           Creater = c.Creater,
                           CreatedDate = c.CreatedDate
                       }).OrderByDescending(c => c.ContractID).ToList();
            }
            return lst;
        }

        public ContractViewModel Details(int id, int? LanguageId)
        {
            try
            {
                ContractViewModel entity = new ContractViewModel();
                entity = (from c in context.HR_tblContract
                          join t in context.LS_tblContractType on c.LSContractTypeID equals t.LSContractTypeID into typelist
                          from type in typelist.DefaultIfEmpty()

                          join e in context.HR_tblEmp on c.EmpID equals e.EmpID into employeelist
                          from employee in employeelist.DefaultIfEmpty()

                          join p in context.LS_tblPosition on c.LSPositionID equals p.LSPositionID into positionlist
                          from position in positionlist.DefaultIfEmpty()

                          join l in context.LS_tblLocation on c.LSLocationID equals l.LSLocationID into locationlist
                          from location in locationlist.DefaultIfEmpty()

                          where c.ContractID == id
                          select new ContractViewModel()
                          {
                              ContractID = c.ContractID,
                              ContractNo = c.ContractNo,
                              EmpID = c.EmpID,
                              EmpName = employee.FirstName + " - " + employee.LastName,
                              LSContractTypeID = c.LSContractTypeID,
                              LSContractTypeName = (LanguageId == 124) ? type.Name : type.VNName,
                              ContractStatus = c.ContractStatus,
                              ProbationFrom = c.ProbationFrom,
                              ProbationTo = c.ProbationTo,
                              SignedDate = c.SignedDate,
                              EffectiveDate = c.EffectiveDate,
                              ExpiredDate = c.ExpiredDate,
                              ProbationSalary = c.ProbationSalary,
                              InsuranceSalary = c.InsuranceSalary,
                              LSPositionID = c.LSPositionID,
                              LSPositionName = (LanguageId == 124) ? position.Name : position.VNName,
                              LSLocationID = c.LSLocationID,
                              LSLocationName = (LanguageId == 124) ? location.Name : location.VNName,
                              MethodPIT = c.MethodPIT,
                              PercentPIT = c.PercentPIT,
                              FileIds = c.FileIds,
                              Note = c.Note,
                              Creater = c.Creater,
                              CreatedDate = c.CreatedDate
                          }).FirstOrDefault();
                return entity;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return new ContractViewModel();
            }
        }

        public ContractEditModel GetDetails(int id, int? LanguageId)
        {
            try
            {
                ContractEditModel entity = new ContractEditModel();            
                entity = (from c in context.HR_tblContract
                            join t in context.LS_tblContractType on c.LSContractTypeID equals t.LSContractTypeID into type
                            from t in type.DefaultIfEmpty()

                            join e in context.HR_tblEmp on c.EmpID equals e.EmpID into employee
                            from e in employee.DefaultIfEmpty()

                            join p in context.LS_tblPosition on c.LSPositionID equals p.LSPositionID into position
                            from p in position.DefaultIfEmpty()

                            join l in context.LS_tblLocation on c.LSLocationID equals l.LSLocationID into location
                            from l in location.DefaultIfEmpty()

                            where c.ContractID == id
                            select new ContractEditModel()
                            {
                                ContractID = c.ContractID,
                                ContractNo = c.ContractNo,
                                EmpID = c.EmpID,
                                EmpName = e.FirstName + " " + e.LastName,
                                LSContractTypeID = c.LSContractTypeID,
                                LSContractTypeName = (LanguageId == 124)? t.Name: t.VNName,
                                ContractStatus = c.ContractStatus,
                                ProbationFrom = c.ProbationFrom,
                                ProbationTo = c.ProbationTo,
                                SignedDate = c.SignedDate,
                                EffectiveDate = c.EffectiveDate,
                                ExpiredDate = c.ExpiredDate,
                                ProbationSalary = c.ProbationSalary,
                                InsuranceSalary = c.InsuranceSalary,
                                PositionID = c.LSPositionID,
                                PositionName = (LanguageId == 124) ? p.Name : p.VNName,
                                LocationID = c.LSLocationID,
                                LocationName = (LanguageId == 124) ? l.Name : l.VNName,
                                MethodPIT = c.MethodPIT,
                                PercentPIT = c.PercentPIT,
                                FileIds = c.FileIds,
                                Note = c.Note,
                                Creater = c.Creater,
                                CreatedDate = c.CreatedDate
                            }).FirstOrDefault();
               
                return entity;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return new ContractEditModel();
            }
        }

        public bool IsDataExisted(string ContractNo, int LSContractTypeID, int EmpID)
        {
            var query = context.HR_tblContract.FirstOrDefault(c => c.ContractNo.ToUpper().Equals(ContractNo.ToUpper())
                && c.LSContractTypeID == LSContractTypeID && c.EmpID == EmpID
                );
            return (query != null);
        }

        public bool IsContractNoExisted(string ContractNo)
        {
            var query = context.HR_tblContract.FirstOrDefault(c => c.ContractNo.ToUpper().Equals(ContractNo.ToUpper())
                );
            return (query != null);
        }

        public bool IsIDExisted(int ID)
        {
            var query = context.HR_tblContract.FirstOrDefault(c => c.ContractID.Equals(ID));
            return (query != null);
        }

        public static bool IsEmpIDExisted(int EmpID)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.HR_tblContract.FirstOrDefault(c => c.EmpID.Equals(EmpID));
                return (query != null);
            }
        }

        public static HR_tblContract Find(int? ID)
        {            
            using (EntityDataContext context = new EntityDataContext())
            {
                //return (from c in context.HR_tblContract.AsNoTracking() where c.ContractID == ID select c).FirstOrDefault();
                return context.HR_tblContract.Find(ID);
            }
        }

        public bool Add(ContractViewModel add_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool isDuplicate = IsDataExisted(add_model.ContractNo, add_model.LSContractTypeID, add_model.EmpID);
                if (isDuplicate == false)
                {
                    decimal? ProbationSalary = null, InsuranceSalary = null;
                    if (!string.IsNullOrEmpty(add_model.ProbationSalaryEdit))
                    {
                        string _ProbationSalary = Eagle.Common.Utilities.StringUtils.RemoveExtraTextWithoutPointOrComma(add_model.ProbationSalaryEdit);
                        ProbationSalary = Convert.ToDecimal(_ProbationSalary);
                    }
                    if (!string.IsNullOrEmpty(add_model.ProbationSalaryEdit))
                    {
                        string _InsuranceSalary = Eagle.Common.Utilities.StringUtils.RemoveExtraTextWithoutPointOrComma(add_model.InsuranceSalaryEdit);
                        InsuranceSalary = Convert.ToDecimal(_InsuranceSalary);
                    }

                    HR_tblContract model = new HR_tblContract();
                    model.EmpID = add_model.EmpID;
                    model.SignedEmpID = add_model.EmpID;
                    model.ContractNo = add_model.ContractNo;
                    model.LSContractTypeID = add_model.LSContractTypeID;
                    model.ContractStatus = add_model.ContractStatus;
                    model.ProbationFrom = add_model.ProbationFrom;
                    model.ProbationTo = add_model.ProbationTo;
                    model.SignedDate = add_model.SignedDate;
                    model.EffectiveDate = add_model.EffectiveDate;
                    model.ExpiredDate = add_model.ExpiredDate;
                    model.ProbationSalary = ProbationSalary;
                    model.InsuranceSalary = InsuranceSalary;
                    model.LSPositionID = add_model.LSPositionID;
                    model.LSLocationID = add_model.LSLocationID;
                    model.MethodPIT = add_model.MethodPIT;
                    model.PercentPIT = add_model.PercentPIT;
                    model.FileIds = add_model.FileIds;
                    model.Note = add_model.Note;
                    model.Creater = add_model.Creater;
                    model.CreatedDate = DateTime.Now;

                    int affectedRow = 0;
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    affectedRow = context.SaveChanges();
                    if (affectedRow == 1)
                    {
                        add_model.ContractID = model.ContractID;
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

        public static bool UpdateFileIds(int Id, string Added_FileIds, out string FileIds)
        {           
            FileIds = string.Empty;
            bool result = false;
            try
            {
                List<string> AddedFileIdList = new List<string>();
                List<string> InitialFileIdList = new List<string>();
                using (EntityDataContext context = new EntityDataContext())
                {
                    HR_tblContract entity = Find(Id);   
                    if (!(string.IsNullOrEmpty(entity.FileIds)))
                        InitialFileIdList = entity.FileIds.Trim().Split(',').ToList();
                    if (!(string.IsNullOrEmpty(Added_FileIds)))
                        AddedFileIdList = Added_FileIds.Trim().Split(',').ToList();
                    List<string> FileIdList = InitialFileIdList.Union(AddedFileIdList).ToList();
                    FileIds = string.Join(",", FileIdList);


                    entity.FileIds = FileIds;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int affectedRows = context.SaveChanges();
                    if (affectedRows == 1)                   
                        result = true;
                }
            }
            catch (Exception ex) { ex.ToString();}
            return result;
        }

        public bool Edit(ContractViewModel edit_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {

                bool EmpIDExist = IsEmpIDExisted(edit_model.EmpID);
                if(EmpIDExist == true)
                {
                    decimal? ProbationSalary = null, InsuranceSalary = null;
                    if (!string.IsNullOrEmpty(edit_model.ProbationSalaryEdit))
                    {
                        string _ProbationSalary = Eagle.Common.Utilities.StringUtils.RemoveExtraTextWithoutPointOrComma(edit_model.ProbationSalaryEdit);
                        ProbationSalary = Convert.ToDecimal(_ProbationSalary);
                    }
                    if (!string.IsNullOrEmpty(edit_model.ProbationSalaryEdit))
                    {
                        string _InsuranceSalary = Eagle.Common.Utilities.StringUtils.RemoveExtraTextWithoutPointOrComma(edit_model.InsuranceSalaryEdit);
                        InsuranceSalary = Convert.ToDecimal(_InsuranceSalary);
                    }

                    HR_tblContract model = Find(edit_model.ContractID);
                    model.EmpID = edit_model.EmpID;
                    model.ContractNo = edit_model.ContractNo;
                    model.LSContractTypeID = edit_model.LSContractTypeID;
                    model.ContractStatus = edit_model.ContractStatus;
                    model.ProbationFrom = edit_model.ProbationFrom;
                    model.ProbationTo = edit_model.ProbationTo;
                    model.SignedDate = edit_model.SignedDate;
                    model.EffectiveDate = edit_model.EffectiveDate;
                    model.ExpiredDate = edit_model.ExpiredDate;
                    model.ProbationSalary = ProbationSalary;
                    model.InsuranceSalary = InsuranceSalary;
                    model.LSPositionID = edit_model.LSPositionID;
                    model.LSLocationID = edit_model.LSLocationID;
                    model.MethodPIT = edit_model.MethodPIT;
                    model.PercentPIT = edit_model.PercentPIT;
                    model.FileIds = edit_model.FileIds;
                    model.Note = edit_model.Note;
                    model.Creater = edit_model.Creater;
                    model.CreatedDate = DateTime.Now;

                    context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    int affectedRows = context.SaveChanges();
                    if (affectedRows == 1)
                    {
                        Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                        result = true;
                    }
                }else
                {
                    result = Add(edit_model, out Message);
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

        public bool Insert(ContractEditModel add_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool isDataDuplicate = IsDataExisted(add_model.ContractNo, add_model.LSContractTypeID, add_model.EmpID);
                if (isDataDuplicate == false)
                {
                    decimal? ProbationSalary = null, InsuranceSalary = null;
                    if (!string.IsNullOrEmpty(add_model.ProbationSalaryEdit))
                    {
                        string _ProbationSalary = Eagle.Common.Utilities.StringUtils.RemoveExtraTextWithoutPointOrComma(add_model.ProbationSalaryEdit);
                        ProbationSalary = Convert.ToDecimal(_ProbationSalary);
                    }
                    if (!string.IsNullOrEmpty(add_model.ProbationSalaryEdit))
                    {
                        string _InsuranceSalary = Eagle.Common.Utilities.StringUtils.RemoveExtraTextWithoutPointOrComma(add_model.InsuranceSalaryEdit);
                        InsuranceSalary = Convert.ToDecimal(_InsuranceSalary);
                    }

                    bool isContractNoExist = IsContractNoExisted(add_model.EmpCode);
                    if (isContractNoExist)
                        add_model.ContractNo = GenerateContractNo(10);

                    HR_tblContract model = new HR_tblContract();
                    model.EmpID = add_model.EmpID;
                    model.SignedEmpID = add_model.EmpID;
                    model.ContractNo = add_model.ContractNo;
                    model.LSContractTypeID = add_model.LSContractTypeID;
                    model.ContractStatus = add_model.ContractStatus;
                    model.ProbationFrom = add_model.ProbationFrom;
                    model.ProbationTo = add_model.ProbationTo;
                    model.EffectiveDate = add_model.EffectiveDate;
                    model.ExpiredDate = add_model.ExpiredDate;
                    model.ProbationSalary = ProbationSalary;
                    model.InsuranceSalary = InsuranceSalary;
                    model.LSPositionID = add_model.PositionID;
                    model.LSLocationID = add_model.LocationID;
                    model.MethodPIT = add_model.MethodPIT;
                    model.PercentPIT = add_model.PercentPIT;
                    model.FileIds = add_model.FileIds;
                    model.Note = add_model.Note;
                    model.Creater = add_model.Creater;
                    model.CreatedDate = DateTime.Now;

                    int affectedRow = 0;
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    affectedRow = context.SaveChanges();
                    if (affectedRow == 1)
                    {
                        add_model.ContractID = model.ContractID;
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
               
        public bool Update(ContractEditModel edit_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                 bool EmpIDExist = IsEmpIDExisted(edit_model.EmpID);
                 if (EmpIDExist == true)
                 {
                     decimal? ProbationSalary = null, InsuranceSalary = null;
                     if (!string.IsNullOrEmpty(edit_model.ProbationSalaryEdit))
                     {
                         string _ProbationSalary = Eagle.Common.Utilities.StringUtils.RemoveExtraTextWithoutPointOrComma(edit_model.ProbationSalaryEdit);
                         ProbationSalary = Convert.ToDecimal(_ProbationSalary);
                     }
                     if (!string.IsNullOrEmpty(edit_model.ProbationSalaryEdit))
                     {
                         string _InsuranceSalary = Eagle.Common.Utilities.StringUtils.RemoveExtraTextWithoutPointOrComma(edit_model.InsuranceSalaryEdit);
                         InsuranceSalary = Convert.ToDecimal(_InsuranceSalary);
                     }

                     HR_tblContract model = Find(edit_model.ContractID);
                     if (model != null)
                     {
                         model.EmpID = edit_model.EmpID;
                         model.ContractNo = edit_model.ContractNo;
                         model.LSContractTypeID = edit_model.LSContractTypeID;
                         model.ContractStatus = edit_model.ContractStatus;
                         model.ProbationFrom = edit_model.ProbationFrom;
                         model.ProbationTo = edit_model.ProbationTo;
                         model.EffectiveDate = edit_model.EffectiveDate;
                         model.ExpiredDate = edit_model.ExpiredDate;
                         model.ProbationSalary = ProbationSalary;
                         model.InsuranceSalary = InsuranceSalary;
                         model.LSPositionID = edit_model.PositionID;
                         model.LSLocationID = edit_model.LocationID;
                         model.MethodPIT = edit_model.MethodPIT;
                         model.PercentPIT = edit_model.PercentPIT;
                         model.FileIds = edit_model.FileIds;
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
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                Message = Eagle.Resource.LanguageResource.SystemError;
            }
            return result;
        }

        public bool Delete(int? id, out string message)
        {
            bool result = false;
            message = string.Empty;
            try
            {
                HR_tblContract model = Find(id);
                if (model != null)
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                    result = true;
                    message = Eagle.Resource.LanguageResource.DeleteSuccess;
                }
                else
                {
                    message = Eagle.Resource.LanguageResource.IDNoExistsErrorMessage;
                    result = false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                message = Eagle.Resource.LanguageResource.DeleteFailure;
            }

            return result;
        }

        #region FILE MANAGER ==========================================================
        public static bool UpdateFileListAfterDeletingFile(int Id, int FileId, out string FileIds)
        {
            bool result = false;
            FileIds = string.Empty;
            try
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    HR_tblContract entity = Find(Id);
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
        #endregion ====================================================================
    }
}
