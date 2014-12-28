using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Model.HR;

namespace Eagle.Repository.HR
{
    public class WorkingRecordRespository
    {
        public EntityDataContext context { get; set; }

        public WorkingRecordRespository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<WorkingRecordViewModel> GetBriefList(int? EmpID, int? ModuleId, string UserName, bool isAdmin, int? LanguageId)
        {
            List<WorkingRecordViewModel> lst = new List<WorkingRecordViewModel>();          
            if (EmpID != null && EmpID != 0)
            {
                var jointable = context.SYS_spfrmDataPermission(UserName, ModuleId).ToList();
                lst = (from w in context.HR_tblWorking
                         join e in context.HR_tblEmp on w.EmpID equals e.EmpID into emplist
                         from emp in emplist.DefaultIfEmpty()
                         join c in context.LS_tblCompany on w.LSCompanyID equals c.LSCompanyID
                       where w.EmpID == EmpID && (isAdmin == true || jointable.Contains(w.EmpID)) 
                         select new WorkingRecordViewModel()
                         {
                             WorkingID = w.WorkingID,
                             EmpID = w.EmpID,
                             EmpName = emp.LastName + " - " + emp.FirstName,
                             EffectiveDate = w.EffectiveDate,

                             LSChangeStatusID = w.LSChangeStatusID,

                             LineManagerID = w.LineManagerID,

                             LSCompanyID = w.LSCompanyID,
                             LSCompanyName = (LanguageId==10001)? c.Name:c.VNName,

                             LSLocationID = w.LSLocationID,

                             LSPositionID = w.LSPositionID,

                             LSGradeID = w.LSGradeID,

                             Decision = w.Decision,
                             DecisionNo = w.DecisionNo,

                             SignDate = w.SignDate,
                             SignerEmpID = w.SignerEmpID,
                             Note = w.Note,
                             Creater = w.Creater,
                             CreateDate = w.CreateDate
                         }).OrderByDescending(p => p.WorkingID).ToList();
            }
            return lst;
        }

        public List<WorkingRecordViewModel> GetList(int? EmpID, int? ModuleId, string UserName, bool isAdmin, int? LanguageId)
        {
            var jointable = context.SYS_spfrmDataPermission(UserName, ModuleId).ToList();
            var lst = (from w in context.HR_tblWorking
                            join e in context.HR_tblEmp on w.EmpID equals e.EmpID 
                            join c in context.LS_tblCompany on w.LSCompanyID equals c.LSCompanyID

                            join lo in context.LS_tblLocation on w.LSLocationID equals lo.LSLocationID into list3
                            from location in list3.DefaultIfEmpty()

                            join lp in context.LS_tblPosition on w.LSPositionID equals lp.LSPositionID into list4
                            from position in list4.DefaultIfEmpty()

                            join g in context.LS_tblGrade on w.LSGradeID equals g.LSGradeID into list5
                            from grade in list5.DefaultIfEmpty()

                            join m in context.HR_tblEmp on w.LineManagerID equals m.EmpID into list6
                            from manager in list6.DefaultIfEmpty()

                            join s in context.LS_tblChangeStatus on w.LSChangeStatusID equals s.LSChangeStatusID into list7
                            from status in list7.DefaultIfEmpty()

                            join si in context.HR_tblEmp on w.SignerEmpID equals si.EmpID into list8
                            from singer in list8.DefaultIfEmpty()

                            join u in context.SYS_tblUserAccount on w.Creater equals u.UserID into list9
                            from user in list9.DefaultIfEmpty()
                           where w.EmpID == EmpID && (isAdmin == true || jointable.Contains(e.EmpID))    
                       select new WorkingRecordViewModel()
            {
                WorkingID = w.WorkingID,
                EmpID = w.EmpID,
                EmpName = e.FirstName + " " + e.LastName,
                EffectiveDate = w.EffectiveDate,

                LSChangeStatusID = w.LSChangeStatusID,
                LSChangeStatusName = (LanguageId==10001)? status.Name: status.VNName,                 

                LineManagerID = w.LineManagerID,
                LineManagerName = manager.FirstName + " - " + manager.LastName,

                LSCompanyID = w.LSCompanyID,
                LSCompanyName = (LanguageId == 124)? c.Name : c.VNName,

                LSLocationID = w.LSLocationID,
                LSLocationName = (LanguageId == 124) ? location.Name : location.VNName,

                LSPositionID = w.LSPositionID,
                LSPositionName = (LanguageId == 124) ? position.Name : position.VNName,

                LSGradeID = w.LSGradeID,
                LSGradeName = (LanguageId == 124) ? grade.Name : grade.VNName,

                Decision = w.Decision,
                DecisionNo = w.DecisionNo,

                SignDate = w.SignDate,
                SignerEmpID = w.SignerEmpID,

                FileId = w.FileId,
                Note = w.Note,

                Creater = w.Creater,
                CreaterName = user.UserName,

                CreateDate = w.CreateDate
            }).OrderByDescending(p => p.WorkingID).ToList();
            return lst;
        }

        public bool IsDataExisted(int EmpID, int LSCompanyID, int LSChangeStatusID, DateTime EffectiveDate)
        {
            var query = context.HR_tblWorking.FirstOrDefault(c => c.EmpID == EmpID && c.LSCompanyID == LSCompanyID
                && c.LSChangeStatusID == LSChangeStatusID && c.EffectiveDate == EffectiveDate);
            return (query != null);
        }

        public bool IsIDExisted(int ID)
        {
            var query = context.HR_tblWorking.FirstOrDefault(p => p.WorkingID.Equals(ID));
            return (query != null);
        }


        public bool Add(WorkingRecordViewModel add_model, out string Message)
        {
            Message = string.Empty;         
            bool result = false;
            try
            {
                bool isDuplicate = IsDataExisted(add_model.EmpID, add_model.LSCompanyID, add_model.LSChangeStatusID, add_model.EffectiveDate);
                if (isDuplicate == false)
                {
                    HR_tblWorking model = new HR_tblWorking();

                    model.EmpID = add_model.EmpID;
                    model.LSChangeStatusID = add_model.LSChangeStatusID;
                    model.LineManagerID = add_model.LineManagerID;
                    model.LSCompanyID = Convert.ToInt32(add_model.LSCompanyID);
                    model.LSLocationID = add_model.LSLocationID;
                    model.LSPositionID = add_model.LSPositionID;
                    model.LSGradeID = add_model.LSGradeID;

                    if (add_model.EffectiveDate != null && add_model.EffectiveDate.ToString() != string.Empty)
                        model.EffectiveDate = Convert.ToDateTime(add_model.EffectiveDate.ToString());

                    model.Decision = add_model.Decision;
                    model.DecisionNo = add_model.DecisionNo;
                    model.FileId = add_model.FileId;

                    model.SignerEmpID = add_model.EmpID;                    
                    model.Note = add_model.Note;                   
                    model.CreateDate = DateTime.Now;
                    model.Creater = add_model.Creater;


                    int affectedRow = 0;
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    affectedRow = context.SaveChanges();
                    if(affectedRow == 1)
                    {
                        int i = UpdateEmpInfo(model.EmpID, model.LSCompanyID, model.LineManagerID, model.LSLocationID, model.LSPositionID);
                       if(i==1){
                           add_model.WorkingID = model.WorkingID;
                           Message = Eagle.Resource.LanguageResource.CreateSuccess;
                           result = true;
                       }
                    }
                }else
                {
                    result = false;
                    Message = Eagle.Resource.LanguageResource.DuplicateError;
                }
            }
            catch (Exception ex) {                 
                ex.ToString();
                result = false;
                Message = Eagle.Resource.LanguageResource.SystemError;
            }
            return result;
        }

        public int UpdateEmpInfo(int EmpID, int LSCompanyID, int? LineManagerID, int? LSLocationID, int? LSPositionID)
        {
            int result = 0;     
            HR_tblEmp modelUpdate = EmployeeRepository.Find(EmpID);
            modelUpdate.LSCompanyID = LSCompanyID;         

            modelUpdate.LineManagerID = LineManagerID;
            modelUpdate.LSLocationID = LSLocationID;
            modelUpdate.LSPositionID = LSPositionID;

            context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
            result = context.SaveChanges();             
            return result;
        }

        public HR_tblWorking Find(int? id)
        {
            return context.HR_tblWorking.Find(id);
        }

        public WorkingRecordViewModel Details(int id)
        {
            //try
            //{
                var query = (from w in context.HR_tblWorking 
                            join e in context.HR_tblEmp on w.EmpID equals e.EmpID 
                            where w.WorkingID == id
                            select new WorkingRecordViewModel()
                            {
                               WorkingID = w.WorkingID,
                               EmpID = w.EmpID,
                               EffectiveDate = w.EffectiveDate,
                               LSChangeStatusID = w.LSChangeStatusID,
                               LineManagerID = w.LineManagerID,
                               LSCompanyID = w.LSCompanyID,                              
                               LSLocationID = w.LSLocationID,
                               LSPositionID = w.LSPositionID,                               
                               LSGradeID = w.LSGradeID,                               
                               Decision = w.Decision,
                               DecisionNo = w.DecisionNo,
                               SignDate = w.SignDate,
                               SignerEmpID = w.SignerEmpID,
                               EmpName = e.FirstName + " " + e.LastName,
                               FileId = w.FileId,
                               Note = w.Note,
                               Creater = w.Creater,
                               CreateDate = w.CreateDate
                           }).FirstOrDefault();
                return query;
            //}
            //catch(Exception ex)
            //{
            //    ex.ToString();
            //    return new WorkingRecordViewModel();
            //}
        }

        public bool Update(WorkingRecordViewModel model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                HR_tblWorking modelUpdate = Find(model.WorkingID);
                if (modelUpdate.WorkingID > 0)
                {
                    modelUpdate.EmpID = model.EmpID;
                    modelUpdate.LSChangeStatusID = model.LSChangeStatusID;
                    modelUpdate.LineManagerID = model.LineManagerID;
                    modelUpdate.LSCompanyID = Convert.ToInt32(model.LSCompanyID);                   
                    modelUpdate.LSLocationID = model.LSLocationID;
                    modelUpdate.LSPositionID = model.LSPositionID;
                    modelUpdate.LSGradeID = model.LSGradeID;

                    if (model.EffectiveDate != null && model.EffectiveDate.ToString() != string.Empty)
                        modelUpdate.EffectiveDate = Convert.ToDateTime(model.EffectiveDate.ToString());

                    modelUpdate.Decision = model.Decision;
                    modelUpdate.DecisionNo = model.DecisionNo;
                    modelUpdate.FileId = model.FileId;

                    modelUpdate.SignerEmpID = model.EmpID;
                    modelUpdate.Note = model.Note;
                    modelUpdate.Creater = model.Creater;
                    modelUpdate.CreateDate = DateTime.Now;

                    context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                    int affectedRows = context.SaveChanges();
                    if (affectedRows == 1)
                    {
                        int i = UpdateEmpInfo(model.EmpID, model.LSCompanyID, model.LineManagerID, model.LSLocationID, model.LSPositionID);
                        if (i == 1)
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

        public bool Delete(int? id, out string Message)
        {
            bool result = false;
            Message = string.Empty;
            try
            {
                HR_tblWorking model = Find(id);
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
