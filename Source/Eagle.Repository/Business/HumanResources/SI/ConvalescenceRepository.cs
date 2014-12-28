using AutoMapper;
using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class ConvalescenceRepository
    {
        public EntityDataContext context { get; set; }
        public ConvalescenceRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public List<ConvalescenceViewModel> Search(ConvalescenceSearch2ViewModel model, int LanguageId)
        {
            if (string.IsNullOrEmpty(model.EmpCode))
            {
                model.EmpCode = "";
            }
            if (string.IsNullOrEmpty(model.FullName))
            {
                model.FullName = "";
            }
            //dùng Lineage để tìm tất cả các phòng ban thuộc LSCompanyId được chọn
            string Lineage = "";
            if (model.LSCompany2ID != null && model.LSCompany2ID != 0)
            {
                Lineage = context.LS_tblCompany.Find(model.LSCompany2ID).Lineage;
            }

            var result = from p in context.SI_tblConvalescence
                         join emp in context.HR_tblEmp on p.EmpID equals emp.EmpID

                         join com in context.LS_tblCompany on emp.LSCompanyID equals com.LSCompanyID into List1
                         from com in List1.DefaultIfEmpty()

                         join lt in context.LS_tblLeaveType on p.LeaveType equals lt.LSLeaveTypeID
                         
                         where (model.EmpCode == "" || emp.EmpCode.Contains(model.EmpCode)) && //Tìm theo code
                         (model.FullName == "" || (emp.LastName + " " + emp.FirstName).Contains(model.FullName)) && //Tìm theo họ tên
                         (model.FromDate2 == null || p.ToDate >= model.FromDate2) &&//Lọc theo ngày
                         (model.ToDate2 == null || (p.LeaveDate <= model.ToDate2)) &&
                         (model.LSCompany2ID == null || com.Lineage.Contains(Lineage)) &&
                         (model.LSWorkPlace2ID == null || model.LSWorkPlace2ID == emp.LSLocationID) //Tìm kiếm theo địa điểm làm việc

                         select new ConvalescenceViewModel()
                         {
                             ID = p.ID,
                             YYYY = p.YYYY,
                             Quarter = p.Quarter,
                             EmpID = p.EmpID,
                             Stage = p.Stage,
                             Days = p.Days,
                             LeaveDate = p.LeaveDate,
                             ToDate = p.ToDate,
                             IsConcentrate = p.IsConcentrate,
                             Percent = p.Percent,
                             Amount = p.Amount,
                             Note = p.Note,
                             LeaveType = p.LeaveType,
                             LSLeaveTypeCode = lt.LSLeaveTypeCode,
                             LSLeaveDayTypeID = p.LSLeaveDayTypeID,
                             InsuranceLeaveID = p.InsuranceLeaveID,
                             MinSalary = p.MinSalary,
                             EmpCode = emp.EmpCode,
                             FullName = emp.LastName + " " + emp.FirstName,
                             LSCompanyName = com.Name
                             
                         };
            return result.ToList();
        }

        public List<ConvalescenceViewModel> GetBy(ConvalescenceSearchViewModel model,int LanguageId)
        {
            if (string.IsNullOrEmpty(model.EmpCode))
            {
                model.EmpCode = "";
            }
            if (string.IsNullOrEmpty(model.FullName))
            {
                model.FullName = "";
            }
            //dùng Lineage để tìm tất cả các phòng ban thuộc LSCompanyId được chọn
            string Lineage = "";
            if (model.LSCompanyID != null && model.LSCompanyID != 0)
            {
                Lineage = context.LS_tblCompany.Find(model.LSCompanyID).Lineage;
            }
            
            //Danh sách dùng để lấy lương tối thiểu
            List<SYS_tblPayrollParameter> SalaryList = context.SYS_tblPayrollParameter.ToList();
            //Danh sách nghỉ dướng sức
            List<int?> insuranceLeaveList = context.SI_tblConvalescence.Select(p => p.InsuranceLeaveID).ToList();

            var result = (from p in context.SI_tblInsuranceLeave
                         join emp in context.HR_tblEmp on p.EmpID equals emp.EmpID
                         join com in context.LS_tblCompany on emp.LSCompanyID equals com.LSCompanyID into List1
                         from com in List1.DefaultIfEmpty()

                         join ldt in context.Timesheet_tbLeaveDayType on p.LSLeaveDayTypeID equals ldt.LSLeaveDayTypeID
                         join lt in context.LS_tblLeaveType on ldt.LSLeaveTypeID equals lt.LSLeaveTypeID

                         where
                         (model.EmpCode == "" || emp.EmpCode.Contains(model.EmpCode)) && //Tìm theo code
                         (model.FullName == "" || (emp.LastName + " " + emp.FirstName).Contains(model.FullName)) && //Tìm theo họ tên
                         p.IsConvalescence == true && //Chỉ lấy khi checkbox nghỉ dưỡng sức được check
                         !insuranceLeaveList.Contains(p.ID) &&//Nghỉ dưỡng sức rồi thì k hiện ra
                         (model.FromDate == null || p.ToDate >= model.FromDate) &&//Lọc theo ngày
                         (model.ToDate == null || (p.FromDate <= model.ToDate)) &&
                         (model.LSCompanyID == null || com.Lineage.Contains(Lineage)) &&
                         (model.LSWorkPlaceID == null || model.LSWorkPlaceID == emp.LSLocationID) //Tìm kiếm theo địa điểm làm việc
                         select new ConvalescenceViewModel()
                         { 
                              EmpID = p.EmpID,
                              FullName = emp.LastName + " " + emp.FirstName,
                              EmpCode = emp.EmpCode,
                              tmpToDate = p.ToDate,
                              IsConcentrate = false,
                              Stage = 1,
                              LSCompanyName = (LanguageId == 124)? com.Name : com.VNName,
                              LSLeaveTypeCode = lt.LSLeaveTypeCode,
                              LeaveType = lt.LSLeaveTypeID,
                              LSLeaveDayTypeID = p.LSLeaveDayTypeID,
                              InsuranceLeaveID = p.ID,
                              Amount = 0

                         }).ToList();

            foreach (var item in result)
            {
                item.LeaveDate = item.tmpToDate.AddDays(1);
                item.YYYY = item.tmpToDate.AddDays(1).Year;
                item.Quarter = item.tmpToDate.AddDays(1).Month / 4 + 1;
                item.Percent = 25;

                #region Get Min Salary
                var payrollModel = (from p in context.SYS_tblPayrollParameter
                where p.EffectiveDate <= item.LeaveDate
                orderby p.EffectiveDate descending
                select p).FirstOrDefault();

                if (payrollModel != null)
                {
                    item.MinSalary = payrollModel.MinSalary.Value;
                }
                else
                {
                    item.MinSalary = 0;
                }
                #endregion
            }

            return result.ToList();
        }

        public bool Insert(List<ConvalescenceViewModel> list, out string errorMessage)
        {
            try
            {

                Mapper.CreateMap<ConvalescenceViewModel, SI_tblConvalescence>();
                foreach (var item in list)
                {
                    if (item.Days != null)
                    {
                        DateTime? LeaveDate = null;
                        if (Eagle.Common.Utilities.DateTimeUtils.TryConvertToDate(item.strLeaveDate, out LeaveDate))
                        {
                            item.LeaveDate = LeaveDate.Value;
                            item.ToDate = LeaveDate.Value.AddDays((double)item.Days.Value);
                            item.Amount = ((decimal)item.Days.Value * item.MinSalary.Value * item.Percent.Value) / 100;
                        }
                        SI_tblConvalescence model = new SI_tblConvalescence();
                        Mapper.Map(item, model);

                        //Kiểm tra nếu chưa nghỉ mới thêm
                        //
                        if (context.SI_tblConvalescence.Where(p => p.InsuranceLeaveID == item.InsuranceLeaveID).FirstOrDefault() == null)
                        {
                            context.Entry(model).State = System.Data.Entity.EntityState.Added;
                        }
                    }
                    else
                    {
                        errorMessage = Eagle.Resource.LanguageResource.PleaseEnterTheNumberOfDaysOff;
                        return false;
                    }
                }
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



        public bool Delete(int? id, out string errorMessage)
        {
            try
            {
                var ModelDelete = context.SI_tblConvalescence.Find(id);
                context.Entry(ModelDelete).State = System.Data.Entity.EntityState.Deleted;
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
    }
}
