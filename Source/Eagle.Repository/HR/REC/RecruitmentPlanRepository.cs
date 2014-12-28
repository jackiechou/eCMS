using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Eagle.Model;

namespace Eagle.Repository
{
    public class RecruitmentPlanRepository
    {
        public EntityDataContext context { get; set; }
        public RecruitmentPlanRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public bool Add(REC_tblPlan model, out string ErrorMessage)
        {
            try
            {
                if (!CheckPlanQuantityValid(model, out ErrorMessage))
                {
                    return false;
                }

                model.RecruitedQuantity = 0;
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public bool Update(REC_tblPlan modelEdit, out string ErrorMessage)
        {
            try
            {
                if (!CheckPlanQuantityValid(modelEdit, out ErrorMessage))
                {
                    return false;
                }
                context.Entry(modelEdit).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        private bool CheckPlanQuantityValid(REC_tblPlan model, out string ErrorMessage)
        {
            // kiểm tra lớn hơn 0
            if (model.PlanQuantity <= 0)
            {
                ErrorMessage = string.Format(Eagle.Resource.LanguageResource.ErrorInputLessThan, Eagle.Resource.LanguageResource.PlanQuantity, "0");
                return false;
            }
            var Quota = context.REC_tblQuota.Where(p =>
                p.Year == model.Year &&
                p.LSCompanyID == model.LSCompanyID &&
                p.LSPositionID == model.LSPositionID
                ).FirstOrDefault();
            if (Quota == null)
	        {
                //chưa có "REC_tblQuota" nào 
                ErrorMessage = "Chưa có định mức \"chức danh\" nào được tạo cho thuộc \"phòng ban\" này trong năm \"" + model.Year.ToString()+"\"";
                return false;
            }
            else if (Quota.QuotaQuantity < model.PlanQuantity)
            {
                // kiểm tra vượt hạn mức cho phép trong "REC_tblQuota"
                ErrorMessage = string.Format(Eagle.Resource.LanguageResource.ErrorInputGreaterThan, Eagle.Resource.LanguageResource.PlanQuantity, Quota.QuotaQuantity);
                return false;
            }
            else
            {
                //Kiểm tra vượt hạn mức cho phép (đã trừ đi các plan được tạo trước đây)

                int TotalPlan = 0;
                List<int> StatusUnapprove = new List<int>() { 3, 5, 7, 9, 11 };
                // lấy tổng các PlanQuantity với điều kiện
                //không phải là plan hiện tại (dành cho chức năng Cập nhật)
                //cùng năm,Cty,phòng ban, tổ nhóm, chức vụ và không ở trạng thái unapprove
                var TotalPlantmp = context.REC_tblPlan
                                     .Where(p => p.Year == model.Year &&
                                         p.LSCompanyID == model.LSCompanyID &&
                                         p.LSPositionID == model.LSPositionID &&
                                         p.PlanID != model.PlanID &&
                                         !StatusUnapprove.Contains(p.StatusPlan))
                                     .Select(p => p.PlanQuantity)
                                     .ToList();
                if (TotalPlantmp != null && TotalPlantmp.Count > 0)
                {
                    TotalPlan = TotalPlantmp.Sum();
                }
                int MaxPlan = Quota.QuotaQuantity - TotalPlan;

                if (model.PlanQuantity > MaxPlan)
                {
                    ErrorMessage = string.Format(Eagle.Resource.LanguageResource.ErrorInputOver,
                        Eagle.Resource.LanguageResource.PlanQuantity,
                        MaxPlan, TotalPlan, Quota.QuotaQuantity);
                    return false;
                }
            }
            ErrorMessage = "";
            return true;
        }

       
        public REC_tblPlan Find(int? PlanId)
        {
            return context.REC_tblPlan.Find(PlanId);
        }

        public List<RecruitmentPlanViewModel> Search(int Year, int? LSCompanyID, int? LSPositionID, int? StatusPlan, int? CurrentEmpId, int? ModuleID, string UserName, int LanguageId = 10001)
        {
            int FunctionID = 456;
            #region Các nhân viên được xem
            List<int?> lstUser = context.SYS_spfrmDataPermission(UserName, ModuleID).ToList();
            #endregion

            //dùng Lineage để tìm tất cả các phòng ban thuộc LSCompanyId được chọn
            string Lineage = "";
            if (LSCompanyID != null && LSCompanyID != 0)
            {
                Lineage = context.LS_tblCompany.Find(LSCompanyID).Lineage;
            }

         
                var result = from p in context.REC_tblPlan
                             join op in context.LS_tblOnlineProcess on FunctionID equals op.FunctionID 
                             join pom in context.SYS_tblProcessOnlineMaster on op.DMQuiTrinhID equals pom.DMQuiTrinhID into tmpList1
                             from list1 in tmpList1.DefaultIfEmpty()
                       where
                        //Theo điều kiện lọc   
                        p.Year == Year &&
                        (StatusPlan == -1 || p.StatusPlan == StatusPlan) &&
                       (LSCompanyID == null || p.LS_tblCompany.Lineage.Contains(Lineage)) &&
                       (LSPositionID == null || p.LSPositionID == LSPositionID) &&
                       //Theo điều kiên khác
                       (
                            //Là người tạo
                            //CurrentEmpId == p.EmpIDPlan  ||
                            lstUser.Contains(p.EmpIDPlan) &&
                            (p.EmpIDPlan == CurrentEmpId || (p.EmpIDPlan != CurrentEmpId && p.StatusPlan != 0))
                            //hoặc người có quyền approve
                            //EmployeeHasPermission.Contains(p.EmpIDPlan)
                            //SectionPermisstion.Contains(p.HR_tblEmp.LSLevel2ID.Value)
                       )
                       
                       select new RecruitmentPlanViewModel()
                       {
                           PlanID = p.PlanID,
                           LSCompanyName = LanguageId == 124 ? p.LS_tblCompany.Name : p.LS_tblCompany.VNName,
                           LSPositionName = LanguageId == 124 ? p.LS_tblPosition.Name : p.LS_tblPosition.VNName,
                           PlanQuantity = p.PlanQuantity,
                           StatusProcess = p.StatusProcess, // chỉ cho xem
                           StatusPlan = p.StatusPlan, // dựa vào đây để phân quyền
                           LevelApprove = p.LevelApprove,
                           CreateDate = p.CreateDate,
                           EmpIDPlan = p.EmpIDPlan,
                           EmpIDPlanName = p.HR_tblEmp.AccountName,
                           s2 = list1.StatusLevel1A,
                           s3 = list1.StatusLevel1U,
                           s4 = list1.StatusLevel2A,
                           s5 = list1.StatusLevel2U,
                           s6 = list1.StatusLevel3A,
                           s7 = list1.StatusLevel3U,
                           s8 = list1.StatusLevel4A,
                           s9 = list1.StatusLevel4U,
                           s10 = list1.StatusLevel5A,
                           s11 = list1.StatusLevel5U

                       };
                return result.ToList();
        }

        public RecruitmentPlanViewModel FindEdit(int? PlanId)
        {
            var FunctionID = 456;

            var result = from p in context.REC_tblPlan
                         join user in context.SYS_tblUserAccount on p.EmpIDPlan equals user.EmpID into tmpList1
                         from lst1 in tmpList1.DefaultIfEmpty()

                         join emp in context.HR_tblEmp on p.EmpIDPlan equals emp.EmpID into empListtmp
                         from empList in empListtmp.DefaultIfEmpty()

                         join quota in context.REC_tblQuota on new { p.Year, p.LSCompanyID, p.LSPositionID } equals new { quota.Year, quota.LSCompanyID, quota.LSPositionID } into tmpList2
                         from lst2 in tmpList2.DefaultIfEmpty()

                         join op in context.LS_tblOnlineProcess on FunctionID equals op.FunctionID
                         join pom in context.SYS_tblProcessOnlineMaster on op.DMQuiTrinhID equals pom.DMQuiTrinhID into tmpListzz1
                         from list1 in tmpListzz1.DefaultIfEmpty()

                         where p.PlanID == PlanId
                         select new RecruitmentPlanViewModel()
                         {
                             PlanID = p.PlanID,
                             Year = p.Year,
                             LSCompanyID = p.LSCompanyID,
                             LSCompanyName = p.LS_tblCompany.Name,
                             LSPositionID = p.LSPositionID,
                             LSPositionName = p.LS_tblPosition.Name,
                             PlanQuantity = p.PlanQuantity,
                             RecruitedQuantity = p.RecruitedQuantity,
                             EmpIDPlan = p.EmpIDPlan,
                             EmpIDPlanName = lst1.UserName,
                             EmpIDFullName = empList.LastName + " " + empList.FirstName,
                             
                             CreateDate = p.CreateDate,
                             Comment = p.Comment,
                             StatusPlan = p.StatusPlan,
                             StatusProcess = p.StatusProcess,
                             LevelApprove = p.LevelApprove,
                             DateLevel1 = p.DateLevel1,
                             EmpIDLevel1 = p.EmpIDLevel1,
                             ReasonLevel1 = p.ReasonLevel1,

                             DateLevel2 = p.DateLevel2,
                             EmpIDLevel2 = p.EmpIDLevel2,
                             ReasonLevel2 = p.ReasonLevel2,

                             DateLevel3 = p.DateLevel3,
                             EmpIDLevel3 = p.EmpIDLevel3,
                             ReasonLevel3 = p.ReasonLevel3,

                             DateLevel4 = p.DateLevel4,
                             EmpIDLevel4 = p.EmpIDLevel4,
                             ReasonLevel4 = p.ReasonLevel4,
                             DateLevel5 = p.DateLevel5,
                             EmpIDLevel5 = p.EmpIDLevel5,
                             ReasonLevel5 = p.ReasonLevel5,
                             QuotaRecruitment = lst2.QuotaQuantity,
                             s2 = list1.StatusLevel1A,
                             s3 = list1.StatusLevel1U,
                             s4 = list1.StatusLevel2A,
                             s5 = list1.StatusLevel2U,
                             s6 = list1.StatusLevel3A,
                             s7 = list1.StatusLevel3U,
                             s8 = list1.StatusLevel4A,
                             s9 = list1.StatusLevel4U,
                             s10 = list1.StatusLevel5A,
                             s11 = list1.StatusLevel5U
                         };
            return result.FirstOrDefault();
        }



        public bool Delete(int Id, out string ErrorMessage)
        {
            try
            {
                var model = Find(Id);
                if (model != null)
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
                ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool ClosingPlan(int Id, out string ErrorMessage)
        {
            try
            {
                var model = Find(Id);
                if (model != null)
                {
                    model.StatusProcess = 2;
                    context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
    }
}
