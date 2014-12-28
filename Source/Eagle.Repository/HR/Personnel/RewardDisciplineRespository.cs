using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model.HR;

namespace Eagle.Repository.HR
{
    public class RewardDisciplineRespository
    {
        public EntityDataContext context { get; set; }

        public RewardDisciplineRespository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<RewardDisciplineViewModel> GetList(int? EmpID, int? ModuleId, string UserName, bool isAdmin, int? LanguageId)
        {
             List<RewardDisciplineViewModel> lst = new List<RewardDisciplineViewModel>();
             if (EmpID != null && EmpID != 0)
             {
                 var jointable = context.SYS_spfrmDataPermission(UserName, ModuleId).ToList();
                 lst = (from r in context.HR_tblReward
                        join e in context.HR_tblEmp on r.EmpID equals e.EmpID into employee
                        from e in employee.DefaultIfEmpty()
                        join lr in context.LS_tblReward on r.LSRewardID equals lr.LSRewardID into reward
                        from lr in reward.DefaultIfEmpty()
                        join rl in context.LS_tblRewardLevel on r.LSRewardLevelID equals rl.LSRewardLevelID into rewardlevel
                        from rl in rewardlevel.DefaultIfEmpty()
                        where r.EmpID == EmpID && (isAdmin == true || jointable.Contains(r.EmpID))    
                        select new RewardDisciplineViewModel()
                        {
                            EmpID = r.EmpID,
                            EmpName = e.FirstName + " " + e.LastName,
                            RewardID = r.RewardID,
                            Type = r.Type,
                            LSRewardID = r.LSRewardID,
                            LSRewardName = (LanguageId == 124)? lr.Name: lr.VNName,
                            LSRewardLevelID = r.LSRewardLevelID,
                            LSRewardLevelName = (LanguageId == 124) ? rl.Name : rl.VNName,
                            EffectiveDate = r.EffectiveDate,
                            Reason = r.Reason,
                            Decision = r.Decision,
                            DecisionNo = r.DecisionNo,
                            SignedDate = r.SignedDate,
                            SignerEmpID = r.SignerEmpID,
                            FileId = r.FileId,
                            Note = r.Note
                        }).OrderByDescending(c => c.RewardID).ToList();
             }
            return lst;
        }

        public RewardDisciplineViewModel Details(int id, int? LanguageId)
        {
            try
            {
                var query = (from r in context.HR_tblReward
                             join e in context.HR_tblEmp on r.EmpID equals e.EmpID into employee
                             from e in employee.DefaultIfEmpty()
                             join lr in context.LS_tblReward on r.LSRewardID equals lr.LSRewardID into reward
                             from lr in reward.DefaultIfEmpty()
                             join rl in context.LS_tblRewardLevel on r.LSRewardLevelID equals rl.LSRewardLevelID into rewardlevel
                             from rl in rewardlevel.DefaultIfEmpty()
                             where r.RewardID == id
                             select new RewardDisciplineViewModel()
                             {
                                 EmpID = r.EmpID,
                                 EmpName = e.FirstName + " " + e.LastName,
                                 RewardID = r.RewardID,
                                 Type = r.Type,
                                 LSRewardID = r.LSRewardID,
                                 LSRewardName = (LanguageId == 124) ? lr.Name : lr.VNName,
                                 LSRewardLevelID = r.LSRewardLevelID,
                                 LSRewardLevelName = (LanguageId == 124) ? rl.Name : rl.VNName,
                                 EffectiveDate = r.EffectiveDate,
                                 Reason = r.Reason,
                                 Decision = r.Decision,
                                 DecisionNo = r.DecisionNo,
                                 SignedDate = r.SignedDate,
                                 SignerEmpID = r.SignerEmpID,
                                 FileId = r.FileId,
                                 Note = r.Note
                             }).FirstOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return new RewardDisciplineViewModel();
            }
        }

        public bool IsDataExisted(int Type, int LSRewardID, int LSRewardLevelID, int EmpID)
        {
            var query = context.HR_tblReward.FirstOrDefault(c=>c.Type == Type && c.LSRewardID == LSRewardID && c.LSRewardLevelID == LSRewardLevelID && c.EmpID == EmpID);
            return (query != null);
        }

        public bool IsIDExisted(int ID)
        {
            var query = context.HR_tblReward.FirstOrDefault(c => c.RewardID.Equals(ID));
            return (query != null);
        }

        public bool IsEmpIDExisted(int EmpID)
        {
            var query = context.HR_tblReward.FirstOrDefault(c => c.EmpID.Equals(EmpID));
            return (query != null);
        }

        public HR_tblReward Find(int? ID)
        {
            return context.HR_tblReward.Find(ID);
        }

        public bool Add(RewardDisciplineViewModel add_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool isDuplicate = IsDataExisted(add_model.Type, add_model.LSRewardID, add_model.LSRewardLevelID, add_model.EmpID);
                if (isDuplicate == false)
                {
                    HR_tblReward model = new HR_tblReward();
                    model.EmpID = add_model.EmpID;
                    model.Type = add_model.Type;
                    model.LSRewardID = add_model.LSRewardID;
                    model.LSRewardLevelID = add_model.LSRewardLevelID;
                    model.EffectiveDate = add_model.EffectiveDate;
                    model.Reason = add_model.Reason;
                    model.Decision = add_model.Decision;
                    model.DecisionNo = add_model.DecisionNo;
                    model.SignedDate = add_model.SignedDate;
                    model.SignerEmpID = add_model.EmpID;
                    model.FileId = add_model.FileId;
                    model.Note = add_model.Note;

                    int affectedRow = 0;
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    affectedRow = context.SaveChanges();
                    if (affectedRow == 1)
                    {
                        add_model.RewardID = model.RewardID;
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

        public bool Update(RewardDisciplineViewModel edit_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {

                bool EmpIDExist = IsEmpIDExisted(edit_model.EmpID);
                if (EmpIDExist == true)
                {
                    HR_tblReward model = Find(edit_model.RewardID);
                    model.EmpID = edit_model.EmpID;
                    model.Type = edit_model.Type;
                    model.LSRewardID = edit_model.LSRewardID;
                    model.LSRewardLevelID = edit_model.LSRewardLevelID;
                    model.EffectiveDate = edit_model.EffectiveDate;
                    model.Reason = edit_model.Reason;
                    model.Decision = edit_model.Decision;
                    model.DecisionNo = edit_model.DecisionNo;
                    model.SignedDate = edit_model.SignedDate;
                    model.SignerEmpID = edit_model.EmpID;
                    model.FileId = edit_model.FileId;
                    model.Note = edit_model.Note;

                    context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    int affectedRows = context.SaveChanges();
                    if (affectedRows == 1)
                    {
                        Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                        result = true;
                    }
                }
                else
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
                HR_tblReward model = Find(id);
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
