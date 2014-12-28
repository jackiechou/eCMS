using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class DemandRepository
    {
        private const int FunctionID = 481;
        public EntityDataContext context { get; set; }
        public DemandRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(string DemandCode)
        {
            var code = context.REC_tblDemand.FirstOrDefault(p => p.DemandCode.Equals(DemandCode));
            return (code != null);
        }

        public bool Add(REC_tblDemand model, out string errorMessage)
        {
            try
            {
                if (CheckExist(model.DemandCode))
                {
                    errorMessage = Eagle.Resource.LanguageResource.CodeIsExists;
                    return false;
                }
                model.CreateTime = DateTime.Now;
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
               
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

        public REC_tblDemand Find(int? DemandId)
        {
            return context.REC_tblDemand.Find(DemandId);
        }

        public RecruitmentDemandViewModel FindEdit(int? DemandID,int LanguageId = 10001)
        {
            var result = from demand in context.REC_tblDemand
                         join pos in context.LS_tblPosition on demand.LSPositionID equals pos.LSPositionID into PosListtmp
                         from position in PosListtmp.DefaultIfEmpty()

                         join op in context.LS_tblOnlineProcess on FunctionID equals op.FunctionID
                         join pom in context.SYS_tblProcessOnlineMaster on op.DMQuiTrinhID equals pom.DMQuiTrinhID into tmpList1
                         from list1 in tmpList1.DefaultIfEmpty()


                         where demand.DemandID == DemandID
                         select new RecruitmentDemandViewModel()
                         {
                             Year = demand.Year,
                             DemandID = demand.DemandID,
                             DemandCode = demand.DemandCode,
                             LSCompanyID = demand.LSCompanyID,
                             LSPositionID = demand.LSPositionID,
                             LSPositionName = LanguageId == 124 ? position.Name : position.VNName,
                             EffectiveDate = demand.EffectiveDate,
                             EffectiveDateAlowNull = demand.EffectiveDate,
                             DemandDate = demand.DemandDate,
                             DemandDateAlowNull = demand.DemandDate,
                             DemandQuantity = demand.DemandQuantity,
                             DemandQuantityAlowNull = demand.DemandQuantity,
                             ActualQuantity = demand.ActualQuantity,
                             Balance = demand.Balance,
                             isAdhoc = demand.isAdhoc,
                             Comment = demand.Comment,
                             CreateTime = demand.CreateTime,
                             EmpID = demand.EmpID,
                             PlanID = demand.PlanID,
                             OtherDemand = demand.OtherDemand,
                             LevelApprove = demand.LevelApprove,
                             StatusDemand = demand.StatusDemand,
                             StatusProcess = demand.StatusProcess,
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

        public List<RecruitmentDemandViewModel> Search(int? Year, int? LSCompanyID, int? LSPositionID,int? StatusDemand, int? CurrentEmpId, int ModuleID, string UserName, int LanguageId)
        {

            #region Các nhân viên được xem
            List<int?> lstUser = context.SYS_spfrmDataPermission(UserName, ModuleID).ToList();
            #endregion
            //dùng Lineage để tìm tất cả các phòng ban thuộc LSCompanyId được chọn
            string Lineage = "";
            if (LSCompanyID != null && LSCompanyID != 0)
            {
                Lineage = context.LS_tblCompany.Find(LSCompanyID).Lineage;
            }


            var result = from demand in context.REC_tblDemand

                         join op in context.LS_tblOnlineProcess on FunctionID equals op.FunctionID
                         join pom in context.SYS_tblProcessOnlineMaster on op.DMQuiTrinhID equals pom.DMQuiTrinhID into tmpList1
                         from list1 in tmpList1.DefaultIfEmpty()

                         //join pos in context.LS_tblPosition on demand.LSPositionID equals pos.LSPositionID into posListtmp
                         //from position in posListtmp.DefaultIfEmpty()

                         //join com in context.LS_tblCompany on demand.LSCompanyID equals com.LSCompanyID into comListtmp
                         //from company in comListtmp.DefaultIfEmpty()

                         join emp in context.HR_tblEmp on demand.EmpID equals emp.EmpID into empListtmp
                         from employee in empListtmp.DefaultIfEmpty()

                         where demand.Year == Year &&
                         (StatusDemand == -1 || demand.StatusDemand == StatusDemand) &&
                         (LSCompanyID == null || demand.LS_tblCompany.Lineage.Contains(Lineage)) &&
                         (LSPositionID == null || demand.LSPositionID == LSPositionID)
                         select new RecruitmentDemandViewModel()
                         {
                             DemandID = demand.DemandID,
                             DemandCode = demand.DemandCode,
                             LSCompanyID = demand.LSCompanyID,
                             LSCompanyName = LanguageId == 124 ? demand.LS_tblCompany.Name : demand.LS_tblCompany.VNName,
                             LSPositionID = demand.LSPositionID,
                             LSPositionName = LanguageId == 124 ? demand.LS_tblPosition.Name : demand.LS_tblPosition.VNName,
                             EffectiveDate = demand.EffectiveDate,
                             EffectiveDateAlowNull = demand.EffectiveDate,
                             DemandDate = demand.DemandDate,
                             DemandDateAlowNull = demand.DemandDate,
                             DemandQuantity = demand.DemandQuantity,
                             DemandQuantityAlowNull = demand.DemandQuantity,
                             ActualQuantity = demand.ActualQuantity,
                             Balance = demand.Balance,
                             isAdhoc = demand.isAdhoc,
                             Comment = demand.Comment,
                             CreateTime = demand.CreateTime,
                             EmpID = demand.EmpID,
                             EmpCreateName = employee.LastName + " " + employee.FirstName,
                             PlanID = demand.PlanID,
                             OtherDemand = demand.OtherDemand,
                             LevelApprove = demand.LevelApprove,
                             StatusDemand = demand.StatusDemand,
                             StatusProcess = demand.StatusProcess,
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
        
        //Note phần search này dành cho chức năng Recruitment Project
        //Sẽ lọc không lấy những kết quả với 2 điều kiện sau
        //1. Có DemandID nằm trong danh sách DemandIDSelectedList
        //2. StatusProcess == 1
        //3. Chỉ lấy những kết quả đã được approve ở cấp cuối cùng (demand.StatusDemand == 2*maxLevelApprove)
        public List<RecruitmentDemandResultViewModel> Search(int? Year, int? LSCompanyID, int? LSPositionID, int? CurrentEmpId, int ModuleID, string UserName, int LanguageId, List<int> DemandIDSelectedList)
        {
            var maxLevelApprove = 3;
            try
            {
                maxLevelApprove = context.LS_tblOnlineProcess.Single(p => p.FunctionID == FunctionID).NoOfLevel;
            }
            catch 
            {
            }
            #region Các nhân viên được xem
            List<int?> lstUser = context.SYS_spfrmDataPermission(UserName, ModuleID).ToList();
            #endregion
            //dùng Lineage để tìm tất cả các phòng ban thuộc LSCompanyId được chọn
            string Lineage = "";
            if (LSCompanyID != null && LSCompanyID != 0)
            {
                Lineage = context.LS_tblCompany.Find(LSCompanyID).Lineage;
            }
            var result = from demand in context.REC_tblDemand

                         join emp in context.HR_tblEmp on demand.EmpID equals emp.EmpID into empListtmp
                         from employee in empListtmp.DefaultIfEmpty()

                         where demand.Year == Year && 
                         (LSCompanyID == null || demand.LS_tblCompany.Lineage.Contains(Lineage)) &&
                         (LSPositionID == null || demand.LSPositionID == LSPositionID) &&
                         !DemandIDSelectedList.Contains(demand.DemandID) &&//điều kiện 1
                         demand.StatusProcess == 1 &&//điều kiện 2
                         demand.StatusDemand == 2 * maxLevelApprove //điều kiện 3
                         select new RecruitmentDemandResultViewModel()
                         {
                             DemandID = demand.DemandID,
                             DemandCode = demand.DemandCode,
                             LSCompanyID = demand.LSCompanyID,
                             LSCompanyName = LanguageId == 124 ? demand.LS_tblCompany.Name : demand.LS_tblCompany.VNName,
                             LSPositionID = demand.LSPositionID,
                             LSPositionName = LanguageId == 124 ? demand.LS_tblPosition.Name : demand.LS_tblPosition.VNName,
                             //EffectiveDate = demand.EffectiveDate,
                             //EffectiveDateAlowNull = demand.EffectiveDate,
                             //DemandDate = demand.DemandDate,
                             //DemandDateAlowNull = demand.DemandDate,
                             DemandQuantity = demand.DemandQuantity,
                             //DemandQuantityAlowNull = demand.DemandQuantity,
                             //ActualQuantity = demand.ActualQuantity,
                             //Balance = demand.Balance,
                             //isAdhoc = demand.isAdhoc,
                             //Comment = demand.Comment,
                             //CreateTime = demand.CreateTime,
                             //EmpID = demand.EmpID,
                             //PlanID = demand.PlanID,
                             //OtherDemand = demand.OtherDemand,
                             //LevelApprove = demand.LevelApprove,
                             //StatusDemand = demand.StatusDemand,
                             //StatusProcess = demand.StatusProcess,
                             EmpCreateName = employee.LastName + " " + employee.FirstName

                         };
            return result.ToList();
        }
        
        public bool Delete(int Id, out string ErrorMessage)
        {
            try
            {
                var modelDelete = Find(Id);
                context.Entry(modelDelete).State = System.Data.Entity.EntityState.Deleted;
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

        public bool Update(REC_tblDemand model, out string ErrorMessage)
        {
            try
            {
                //check code exists
                if (context.REC_tblDemand.FirstOrDefault(p => p.DemandCode == model.DemandCode && p.DemandID != model.DemandID) != null)
                {
                    ErrorMessage = Eagle.Resource.LanguageResource.CodeIsExists;
                    return false;
                }

                context.Entry(model).State = System.Data.Entity.EntityState.Modified;
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



        public List<RecruitmentDemandResultViewModel> GetByListID(List<int> DemandIDListAdd, int LanguageId)
        {
            var result = from demand in context.REC_tblDemand

                         join emp in context.HR_tblEmp on demand.EmpID equals emp.EmpID into empListtmp
                         from employee in empListtmp.DefaultIfEmpty()

                         where DemandIDListAdd.Contains(demand.DemandID)

                         select new RecruitmentDemandResultViewModel()
                         {
                             DemandID = demand.DemandID,
                             DemandCode = demand.DemandCode,
                             LSCompanyID = demand.LSCompanyID,
                             LSCompanyName = LanguageId == 124 ? demand.LS_tblCompany.Name : demand.LS_tblCompany.VNName,
                             LSPositionID = demand.LSPositionID,
                             LSPositionName = LanguageId == 124 ? demand.LS_tblPosition.Name : demand.LS_tblPosition.VNName,
                             //EffectiveDate = demand.EffectiveDate,
                             //EffectiveDateAlowNull = demand.EffectiveDate,
                             //DemandDate = demand.DemandDate,
                             //DemandDateAlowNull = demand.DemandDate,
                             DemandQuantity = demand.DemandQuantity,
                             //DemandQuantityAlowNull = demand.DemandQuantity,
                             //ActualQuantity = demand.ActualQuantity,
                             //Balance = demand.Balance,
                             //isAdhoc = demand.isAdhoc,
                             //Comment = demand.Comment,
                             //CreateTime = demand.CreateTime,
                             //EmpID = demand.EmpID,
                             //PlanID = demand.PlanID,
                             //OtherDemand = demand.OtherDemand,
                             //LevelApprove = demand.LevelApprove,
                             //StatusDemand = demand.StatusDemand,
                             //StatusProcess = demand.StatusProcess,
                             EmpCreateName = employee.LastName + " " + employee.FirstName
                         };
            return result.ToList();
        }
    }
}
