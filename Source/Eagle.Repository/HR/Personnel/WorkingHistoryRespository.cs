using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model.HR;

namespace Eagle.Repository.HR
{
    public class WorkingHistoryRespository
    {
        public EntityDataContext context { get; set; }

        public WorkingHistoryRespository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<WorkingHistoryViewModel> GetList(int? EmpID, int? ModuleId, string UserName, bool isAdmin, int? LanguageId)
        {
            List<WorkingHistoryViewModel> lst = new List<WorkingHistoryViewModel>();
            if (EmpID != null && EmpID != 0)
            {
                var jointable = context.SYS_spfrmDataPermission(UserName, ModuleId).ToList();
                lst = (from w in context.HR_tblWorkingHistory
                       join e in context.HR_tblEmp on w.EmpID equals e.EmpID into employee
                       from e in employee.DefaultIfEmpty()
                       where w.EmpID == EmpID && (isAdmin == true || jointable.Contains(w.EmpID))    
                       select new WorkingHistoryViewModel()
                       {
                           WorkingHistoryID = w.WorkingHistoryID,
                           EmpID = w.EmpID,
                           EmpName = e.FirstName + " " + e.LastName,
                           FromDate = w.FromDate,
                           ToDate = w.ToDate,
                           WorkFor = w.WorkFor,
                           Position = w.Position,
                           Duty = w.Duty,
                           LeaveReason = w.LeaveReason,
                           FormerCompanyAddress = w.FormerCompanyAddress,
                           ContactPersonName = w.ContactPersonName,
                           ContactPersonPosition = w.ContactPersonPosition,
                           ContactPersonPhone = w.ContactPersonPhone,
                           ContactPersonEmail = w.ContactPersonEmail,
                           Note = w.Note
                       }).OrderByDescending(w => w.WorkingHistoryID).ToList();
            }
            return lst;
        }

        public WorkingHistoryViewModel Details(int id)
        {
            try
            {
                var query = (from w in context.HR_tblWorkingHistory
                             join e in context.HR_tblEmp on w.EmpID equals e.EmpID into employee
                             from e in employee.DefaultIfEmpty()
                             where w.WorkingHistoryID == id
                             select new WorkingHistoryViewModel()
                             {
                                 WorkingHistoryID = w.WorkingHistoryID,
                                 EmpID = w.EmpID,
                                 EmpName = e.FirstName + " " + e.LastName,
                                 FromDate = w.FromDate,
                                 ToDate = w.ToDate,
                                 WorkFor = w.WorkFor,
                                 Position = w.Position,
                                 Duty = w.Duty,
                                 LeaveReason = w.LeaveReason,
                                 FormerCompanyAddress = w.FormerCompanyAddress,
                                 ContactPersonName = w.ContactPersonName,
                                 ContactPersonPosition = w.ContactPersonPosition,
                                 ContactPersonPhone = w.ContactPersonPhone,
                                 ContactPersonEmail = w.ContactPersonEmail,
                                 Note = w.Note
                             }).FirstOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return new WorkingHistoryViewModel();
            }
        }

        public bool IsDataExisted(int EmpID, string WorkFor)
        {
            var query = context.HR_tblWorkingHistory.FirstOrDefault(c => c.EmpID == EmpID && c.WorkFor.ToUpper().Equals(WorkFor));
            return (query != null);
        }

        public bool IsIDExisted(int ID)
        {
            var query = context.HR_tblWorkingHistory.FirstOrDefault(c => c.WorkingHistoryID.Equals(ID));
            return (query != null);
        }

        public bool IsEmpIDExisted(int EmpID)
        {
            var query = context.HR_tblWorkingHistory.FirstOrDefault(c => c.EmpID.Equals(EmpID));
            return (query != null);
        }

        public HR_tblWorkingHistory Find(int? ID)
        {
            return context.HR_tblWorkingHistory.Find(ID);
        }
        public bool Add(WorkingHistoryViewModel add_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool isDuplicate = IsDataExisted(add_model.EmpID, add_model.WorkFor);
                if (isDuplicate == false)
                {
                    HR_tblWorkingHistory model = new HR_tblWorkingHistory();
                    model.EmpID = add_model.EmpID;
                    model.FromDate = add_model.FromDate;
                    model.ToDate = add_model.ToDate;
                    model.WorkFor = add_model.WorkFor;
                    model.Position = add_model.Position;
                    model.Duty = add_model.Duty;
                    model.LeaveReason = add_model.LeaveReason;
                    model.FormerCompanyAddress = add_model.FormerCompanyAddress;
                    model.ContactPersonName = add_model.ContactPersonName;
                    model.ContactPersonPosition = add_model.ContactPersonPosition;
                    model.ContactPersonPhone = add_model.ContactPersonPhone;
                    model.ContactPersonEmail = add_model.ContactPersonEmail;
                    model.Note = add_model.Note;
                   
                    int affectedRow = 0;
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    affectedRow = context.SaveChanges();
                    if (affectedRow == 1)
                    {
                        add_model.WorkingHistoryID = model.WorkingHistoryID;
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
        
        public bool Update(WorkingHistoryViewModel edit_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {

                bool EmpIDExist = IsEmpIDExisted(edit_model.EmpID);
                if(EmpIDExist == true)
                {
                    HR_tblWorkingHistory model = Find(edit_model.WorkingHistoryID);
                    model.EmpID = edit_model.EmpID;
                    model.FromDate = edit_model.FromDate;
                    model.ToDate = edit_model.ToDate;
                    model.WorkFor = edit_model.WorkFor;
                    model.Position = edit_model.Position;
                    model.Duty = edit_model.Duty;
                    model.LeaveReason = edit_model.LeaveReason;
                    model.FormerCompanyAddress = edit_model.FormerCompanyAddress;
                    model.ContactPersonName = edit_model.ContactPersonName;
                    model.ContactPersonPosition = edit_model.ContactPersonPosition;
                    model.ContactPersonPhone = edit_model.ContactPersonPhone;
                    model.ContactPersonEmail = edit_model.ContactPersonEmail;
                    model.Note = edit_model.Note;

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

        public bool Delete(int? id, out string Message)
        {
            bool result = false;
            Message = string.Empty;
            try
            {
                HR_tblWorkingHistory model = Find(id);
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
    }
}
