using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class RecruitmentProjectRepository
    {
        public EntityDataContext context { get; set; }
        public RecruitmentProjectRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public List<RecruitmentProjectViewModel> GetAll(int LanguageId)
        {

            var result = from p in context.REC_tblProject
                         join c in context.LS_tblCurrency on p.LSCurrencyID equals c.LSCurrencyID into Listtmp
                         from currency in Listtmp.DefaultIfEmpty()

                         join l in context.LS_tblLocation on p.LSLocationID equals l.LSLocationID into List2tmp
                         from location in List2tmp.DefaultIfEmpty()

                         select new RecruitmentProjectViewModel()
                        {
                            ProjectID = p.ProjectID,
                            ProjectCode = p.ProjectCode,
                            Status = p.Status,
                            PlanCost = p.PlanCost,
                            ActualCost = p.ActualCost,
                            LSCurrencyName = LanguageId == 124 ? currency.Name : currency.VNName,
                            LSLocationName = LanguageId == 124 ? location.Name : location.VNName
                        };

            return result.ToList();
        }

        public bool CheckExist(string ProjectCode)
        {
            var code = context.REC_tblProject.FirstOrDefault(p => p.ProjectCode == ProjectCode);
            return (code != null);
        }

        public REC_tblProject Find(int? ProjectId)
        {
            return context.REC_tblProject.Find(ProjectId);
        }

        public bool Add(REC_tblProject modelAdd, out string errorMessage)
        {
            try
            {
                context.Entry(modelAdd).State = System.Data.Entity.EntityState.Added;
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

        public bool Update(REC_tblProject modelUpdate, out string errorMessage)
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
        public List<RecruitmentProjectViewModel> Search(RecruitmentProjectSearchViewModel model, int LanguageId)
        {
            var result = from p in context.REC_tblProject
                        
                         join c in context.LS_tblCurrency on p.LSCurrencyID equals c.LSCurrencyID into Listtmp
                         from currency in Listtmp.DefaultIfEmpty()

                         join l in context.LS_tblLocation on p.LSLocationID equals l.LSLocationID into List2tmp
                         from location in List2tmp.DefaultIfEmpty()
                         
                         where p.Status == model.Status &&
                               (model.ProjectCode == null || p.ProjectCode.Contains(model.ProjectCode))
                         select new RecruitmentProjectViewModel()
                         {
                             ProjectID = p.ProjectID,
                             ProjectCode = p.ProjectCode,
                             Status = p.Status,
                             PlanCost = p.PlanCost,
                             ActualCost = p.ActualCost,
                             LSCurrencyName = LanguageId == 124 ? currency.Name : currency.VNName,
                             LSLocationName = LanguageId == 124 ? location.Name : location.VNName
                         };

            return result.ToList();
        }

        public bool Delete(int Id, out string errorMessage)
        {
            try
            {
                var modelDelete = Find(Id);
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

        public RecruitmentProjectViewModel FindEdit(int? ProjectID, int LanguageId)
        {
            RecruitmentProjectViewModel retModel = (from p in context.REC_tblProject

                                                    join c in context.LS_tblCurrency on p.LSCurrencyID equals c.LSCurrencyID into Listtmp
                                                    from currency in Listtmp.DefaultIfEmpty()

                                                    join l in context.LS_tblLocation on p.LSLocationID equals l.LSLocationID into List2tmp
                                                    from location in List2tmp.DefaultIfEmpty()
                                                    where p.ProjectID == ProjectID
                                                    select new RecruitmentProjectViewModel()
                                                    {
                                                        ProjectID = p.ProjectID,
                                                        ProjectCode = p.ProjectCode,
                                                        ApplyDateFrom = p.ApplyDateFrom,
                                                        ApplyDateTo = p.ApplyDateTo,
                                                        RecruitFrom = p.RecruitFrom,
                                                        RecruitTo = p.RecruitTo,


                                                        ApplyDateFromAlowNull = p.ApplyDateFrom,
                                                        ApplyDateToAlowNull = p.ApplyDateTo,
                                                        RecruitFromAlowNull = p.RecruitFrom,
                                                        RecruitToAlowNull = p.RecruitTo,

                                                        Status = p.Status,
                                                        PlanCost = p.PlanCost,
                                                        ActualCost = p.ActualCost,
                                                        LSCurrencyID = p.LSCurrencyID,
                                                        LSLocationID = p.LSLocationID,
                                                        Content = p.Content,
                                                        Description = p.Description,
                                                        LSCurrencyName = LanguageId == 124 ? currency.Name : currency.VNName,
                                                        LSLocationName = LanguageId == 124 ? location.Name : location.VNName

                                                    }).FirstOrDefault();
            return retModel;
        }


        public Dictionary<int, string> GetProjectCodeBy(int? EmpID)
        {
            // Lấy danh sách các Phương án tuyển dụng theo nhân viên được phỏng vấn
            var result = (from pr in context.REC_tblProject
                          join ptournament in context.REC_tblProjectTournament on pr.ProjectID equals ptournament.ProjectID
                          join emp in context.HR_tblEmp on ptournament.EmpID equals emp.EmpID

                          where pr.Status == 1 // Là những phương án đang được mở
                                && emp.EmpID == EmpID // là nhân viên được quyền phỏng vấn trong 1 (hoặc nhiều) trong các vòng của phương án này
                          orderby pr.RecruitFrom descending
                          select new { ProjectID = pr.ProjectID, ProjectCode = pr.ProjectCode }).Distinct().ToList();

            Dictionary<int, string> ret = new Dictionary<int, string>();
            ret.Add(0,"--" + Eagle.Resource.LanguageResource.Select + "--");
            if (result != null && result.Count > 0)
            {
                foreach (var item in result)
                {
                    ret.Add(item.ProjectID, item.ProjectCode);
                }
            }
            return ret;
        }

        public Dictionary<int, string> GetAllProjectCode()
        {
            // Lấy danh sách các Phương án tuyển dụng theo nhân viên được phỏng vấn
            var result = (from pr in context.REC_tblProject
                          join ptournament in context.REC_tblProjectTournament on pr.ProjectID equals ptournament.ProjectID
                          join emp in context.HR_tblEmp on ptournament.EmpID equals emp.EmpID

                          where pr.Status == 1 // Là những phương án đang được mở
                          select new { ProjectID = pr.ProjectID, ProjectCode = pr.ProjectCode }).Distinct().ToList();

            Dictionary<int, string> ret = new Dictionary<int, string>();
            ret.Add(0, "--" + Eagle.Resource.LanguageResource.Select + "--");
            if (result != null && result.Count > 0)
            {
                foreach (var item in result)
                {
                    ret.Add(item.ProjectID, item.ProjectCode);
                }
            }
            return ret;
        }
    }
}
