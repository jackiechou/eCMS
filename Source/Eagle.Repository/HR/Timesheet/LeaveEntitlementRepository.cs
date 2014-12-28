using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Eagle.Model;
using Eagle.Core;
using System.Transactions;
using System.Data.Entity.Validation;

namespace Eagle.Repository
{
    public class LeaveEntitlementRepository
    {
        public EntityDataContext context { get; set; }
        public LeaveEntitlementRepository(EntityDataContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// the hien danh sach cac leave request màn hình  leave record de tim kiem, update chinh sua du lieu
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LeaveApplicationCreateViewModel> ListRecord(bool isAdmin, string userGroupID, int? moduleID, int? CurrentEmpID, LeaveApplicationCreateViewModel model)
        {

            try
            {
                var FunctionID = 457; // functionID trong bang SYS_tblFunctionList
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();

                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();


                var tmp = from p in context.Timesheet_tblLeaveApplicationMaster
                          join l in context.Timesheet_tbLeaveDayType on p.LSLeaveDayTypeID equals l.LSLeaveDayTypeID
                          join e in context.HR_tblEmp on p.EmpID equals e.EmpID

                          join op in context.LS_tblOnlineProcess on FunctionID equals op.FunctionID
                          join pom in context.SYS_tblProcessOnlineMaster on op.DMQuiTrinhID equals pom.DMQuiTrinhID into tmpListzz1
                          from list1 in tmpListzz1.DefaultIfEmpty()

                          where jointable.Contains(p.EmpID) // join voi bang phan quyen du lieu
                          && (model.LSCompanyID == 0 || model.LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                          && (p.EmpID == CurrentEmpID || (p.EmpID != CurrentEmpID && p.Status != 0) )
                          && (p.LSLeaveDayTypeID == model.LSLeaveDayTypeID || model.LSLeaveDayTypeID == 0 || model.LSLeaveDayTypeID == null)
                          && (p.Year == model.Year ||  model.Year == 0)
                          && (model.EmpCode == null || e.EmpCode.Contains(model.EmpCode))
                          && (model.FullName == null || (e.LastName + " " + e.FirstName).Contains(model.FullName))
                          select new LeaveApplicationCreateViewModel()
                          {
                              LeaveApplicationMasterID= p.LeaveApplicationMasterID,
                              Year = p.Year,
                              Days = p.Days,
                              FromDate = p.FromDate,
                              ToDate = p.ToDate,
                              LSLeaveDayTypeName = l.Name,
                              //Creater= p.Creater,
                              Creater = e.LastName + " " + e.FirstName,
                              CreateTime =p.CreateTime,
                              Status = p.Status,
                              //// hien thi trang thai 
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

                List<LeaveApplicationCreateViewModel> ret = new List<LeaveApplicationCreateViewModel>();
                foreach (var item in tmp)
                {
                    LeaveApplicationCreateViewModel s = new LeaveApplicationCreateViewModel()
                    {
                        LeaveApplicationMasterID = item.LeaveApplicationMasterID,
                        Year = item.Year,
                        Days = item.Days,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        LSLeaveDayTypeName = item.LSLeaveDayTypeName,
                        Creater = item.Creater,
                        CreateTime = item.CreateTime,
                        Status = item.Status,
                        
                        // hien thi trang thai 
                        s2 = item.s2,
                        s3 = item.s3,
                        s4 = item.s4,
                        s5 = item.s5,
                        s6 = item.s6,
                        s7 = item.s7,
                        s8 = item.s8,
                        s9 = item.s9,
                        s10 = item.s10,
                        s11 = item.s11
                    };
                    ret.Add(s);
                }
                return ret;
            }
            catch
            {
                return new List<LeaveApplicationCreateViewModel>();
            }
        }
        public LeaveEntitlementViewModel LeaveEntitlement(int? EmpID, int? Year)
        {

            try
            {
                LeaveEntitlementViewModel ret = new LeaveEntitlementViewModel();
                
                // Leave Entitlement cho nhan vien tu du lieu ngay phép chuan cua nhan vien
                var tmp = context.Timesheet_tblStandAnualLeaveDays.FirstOrDefault(p => p.EmpID == EmpID);
                // lay phan dieu chinh phep nam cua nhan vien 
                DateTime? startDate = context.HR_tblEmp.FirstOrDefault(p => p.EmpID == EmpID).StartDate;

                if (tmp != null && startDate.Value.Year  < Year)
                {
                    ret.LeaveEntitlement = tmp.StandardALDays; // neu nam nghi phep lon hon nam cua ngay bat dau lam viec
                }
                else if (tmp != null && startDate.Value.Year == Year) // neu moi vao cung nam xin nghi phep tinh ra so phep  duoc huong con lai cho toi cuoi nam
                {

                    decimal dMonth = DateTime.Parse("12/31/" + Year.Value.ToString()).Subtract(startDate.Value) .Days / (365 / 12);
                    ret.LeaveEntitlement = tmp.StandardALDays / 12 * dMonth;
                }
                else
                {
                    ret.LeaveEntitlement = 0;  // neu nam xin nghi phep nhỏ hơn năm của ngày bắt đầu làm việc tai cong ty
                }

                var adjustAnnualLeave = (from a in context.Timesheet_tblAnnualLeaveAdjust
                                         where a.EmpID == EmpID && a.Year == Year
                                         select a.Days
                                         ).ToList();
                if (adjustAnnualLeave != null && adjustAnnualLeave.Count > 0)
                {
                    ret.LeaveEntitlement = ret.LeaveEntitlement + adjustAnnualLeave.Sum();
                }
                // tinh tham nien
                var tmpSeniority = context.HR_tblEmp.FirstOrDefault(p => p.EmpID == EmpID);
                if (tmpSeniority != null)
                {
                    #region tinh tham nien
                    int iYear =  System.DateTime.Now.Subtract(tmpSeniority.StartDate.Value).Days/365;
                    if (iYear < 5)
                        ret.Seniority = 0;
                    else if ( iYear >= 5 && iYear < 10)
                        ret.Seniority = 1;
                    else if ( iYear >= 10 && iYear < 15)
                        ret.Seniority = 2;
                    else if (iYear >= 15 && iYear < 20)
                        ret.Seniority = 3;
                    else if (iYear >= 15 && iYear < 20)
                        ret.Seniority = 3;
                    else if (iYear >= 15 && iYear < 20)
                        ret.Seniority = 4;
                    else if (iYear >= 20 && iYear < 25)
                        ret.Seniority = 5;
                    else if (iYear >= 25 && iYear < 30)
                        ret.Seniority = 6;
                    else if (iYear >= 30 && iYear < 35)
                        ret.Seniority = 7;
                    else if (iYear >= 35 && iYear < 40)
                        ret.Seniority = 8;
                    else if (iYear >= 40 && iYear < 45)
                        ret.Seniority = 9;
                    else if (iYear >= 45 && iYear < 50)
                        ret.Seniority = 10;
                    else if (iYear >= 50 && iYear < 55)
                        ret.Seniority = 11;
                    else if (iYear >= 55 && iYear < 60)
                        ret.Seniority = 12;
                    else if (iYear >= 60 && iYear < 65)
                        ret.Seniority = 13;
                    else if (iYear >= 65 && iYear < 70)
                        ret.Seniority = 14;
                    else if (iYear >= 70 && iYear < 75)
                        ret.Seniority = 15;
                    else if (iYear >= 75 && iYear < 80)
                        ret.Seniority = 16;
                    else if (iYear >= 80 && iYear < 85)
                        ret.Seniority = 17;
                    #endregion
                }
                // tinh phep tu nam cu chuyen sang
                var tmpLeaveForward = context.Timesheet_tblReconciliation.FirstOrDefault(p => p.EmpID == EmpID && p.Year == Year);
                if (tmpLeaveForward != null)
                    ret.LeaveForward = tmpLeaveForward.LeaveForward;
                else
                    ret.LeaveForward = 0;

                // tinh tong so phep 1 nam cua nhan vien duoc huong = phep nam + tham nien + phep nam truoc mang sang
                ret.TotalLeave = (ret.LeaveEntitlement == null ? 0 : ret.LeaveEntitlement) +
                                 (ret.Seniority == 0 ? 0 : ret.Seniority) +
                                 (ret.LeaveForward == null ? 0 : ret.LeaveForward);
                                    
                // tinh so phep da nghi trong nam = so phep da nghi tu trong nam
                var tmpLeaveTaken = (from m in context.Timesheet_tblLeaveApplicationMaster
                                     join d in context.Timesheet_tblLeaveApplicationDetail on m.LeaveApplicationMasterID equals d.LeaveApplicationMasterID
                                     join l in context.Timesheet_tbLeaveDayType on m.LSLeaveDayTypeID equals  l.LSLeaveDayTypeID
                                     join s in context.LS_tblLeaveType on l.LSLeaveTypeID equals s.LSLeaveTypeID
                                     where m.EmpID == EmpID &&  s.LSLeaveTypeCode.Contains("AL") &&
                                          m.Year == Year && 
                                          (m.Status == 0 || m.Status == 1 || m.Status==2 || m.Status == 4 || m.Status== 6 || m.Status == 8)
                                     select d.Days).ToList();

                if (tmpLeaveTaken != null && tmpLeaveTaken.Count > 0)
                    ret.LeaveTaken = tmpLeaveTaken.Sum();
                else
                    ret.LeaveTaken = 0;

                ret.Balance = ret.TotalLeave - ret.LeaveTaken;

                return ret;
            }
            catch ( Exception ex)
            {
                string strError = ex.Message;
                return new LeaveEntitlementViewModel();
            }
        }
        public List<LeaveApplicationDetailListViewModel> ListLeaveTakenEdit(int LeaveApplicationMasterID)
        {
            try
            {
                List<LeaveApplicationDetailListViewModel> ret = new List<LeaveApplicationDetailListViewModel>();
                return ret = (from d in context.Timesheet_tblLeaveApplicationDetail
                              where d.LeaveApplicationMasterID == LeaveApplicationMasterID
                              select new LeaveApplicationDetailListViewModel()
                              {
                                  LeaveApplicationMasterID = d.LeaveApplicationMasterID,
                                  LeaveApplicationDetailID = d.LeaveApplicationDetailID,
                                  LeaveDate = d.LeaveDate,
                                  Days = d.Days,
                                  TypeLeave = d.TypeLeave,                
                              }).ToList();

            }
            catch {

                return new List<LeaveApplicationDetailListViewModel>();
            }

                
        }
        public List<LeaveApplicationDetailListViewModel> ListLeaveTaken(DateTime FromDate, DateTime ToDate, int? EmpID)
        {
            try
            {
                List<LeaveApplicationDetailListViewModel> ret = new List<LeaveApplicationDetailListViewModel>();
                var tmpLeaveType = context.SYS_tblParameter.FirstOrDefault();
                int iLeaveType = 1;
                if (tmpLeaveType != null)
                    iLeaveType = tmpLeaveType.LeaveType;
                if (iLeaveType == 1)
                {
                    //Working schedule số ngày làm việc từ lịch đầu tiên
                    var tmpCout = from d in context.TimeSheet_tblScheduleDetail
                                   join m in context.TimeSheet_tblSchedule on d.ScheduleID equals m.ScheduleID
                                   join e in context.TimeSheet_tblAssignEmpSchedule on d.ScheduleID equals e.ScheduleID
                                   where d.DateID >= FromDate && d.DateID <= ToDate && d.LSShiftID != null && e.EmpID == EmpID
                                   select new LeaveApplicationDetailListViewModel() { 
                                             LeaveDate = d.DateID,
                                             Days = 1
                                         };
                    // so ngay lam viec theo lich da thay doi
                    var tmpCoutChanged = from d in context.TimeSheet_tblScheduleDetail
                                         join c in context.Timesheet_tblScheduleChange on d.ScheduleID equals c.ScheduleID_To
                                         where d.DateID >= c.FromDate && d.DateID <= c.ToDate && d.DateID != null && c.EmpID == EmpID
                                         select new LeaveApplicationDetailListViewModel() { 
                                             LeaveDate = d.DateID,
                                             Days = 1
                                         };
                    var tmpResult = tmpCout.Concat(tmpCoutChanged).Distinct();

                    foreach (var item in tmpResult)
                    {
                        LeaveApplicationDetailListViewModel s = new LeaveApplicationDetailListViewModel()
                        {
                           LeaveDate = item.LeaveDate,
                           Days = item.Days
                        };
                        ret.Add(s);
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return new List<LeaveApplicationDetailListViewModel>();
            }
        }
        public string LeaveTaken(DateTime FromDate, DateTime ToDate, int? EmpID)
        {
            var tmpLeaveType = context.SYS_tblParameter.FirstOrDefault();
            int iLeaveType = 1;
            if (tmpLeaveType != null)
                iLeaveType = tmpLeaveType.LeaveType;
            string strLeaveTaken = "";
            string strLeaveTakenChanged = "";
            if (iLeaveType == 1)
            { 
                //Working schedule số ngày làm việc từ lịch đầu tiên
                var tmpCout = (from d in context.TimeSheet_tblScheduleDetail
                               join m in context.TimeSheet_tblSchedule on d.ScheduleID equals m.ScheduleID
                               join e in context.TimeSheet_tblAssignEmpSchedule on d.ScheduleID equals e.ScheduleID
                               where d.DateID >= FromDate && d.DateID <= ToDate
                               && d.LSShiftID != null 
                               && e.EmpID == EmpID
                               select d.DateID).ToList();
                strLeaveTaken=  tmpCout.Count().ToString();

                // so ngay lam viec theo lich da thay doi
                var tmpCoutChanged = (from d in context.TimeSheet_tblScheduleDetail
                                       join c in context.Timesheet_tblScheduleChange on d.ScheduleID equals c.ScheduleID_To
                                       where d.DateID >= c.FromDate && d.DateID <= c.ToDate
                                       && d.DateID != null
                                       && c.EmpID == EmpID
                                       && d.DateID >= FromDate && d.DateID <= ToDate
                                       select d.DateID).ToList();
                var tmpResult = tmpCout.Concat(tmpCoutChanged).Distinct();
                strLeaveTakenChanged = tmpResult.Count().ToString();

            }
            else
            {
                //System calendar

            }
            return strLeaveTakenChanged;
        }
        // check trung du lieu  cung 1 loai nghi trung thoi gian
        public bool CheckExist(DateTime? FromDate, DateTime? ToDate, int? EmpID, int LSLeaveDayTypeID, int LeaveApplicationMasterID)
        {
            
            if (LeaveApplicationMasterID == 0)
            {
               var code = from d in context.Timesheet_tblLeaveApplicationDetail
                           join m in context.Timesheet_tblLeaveApplicationMaster on d.LeaveApplicationMasterID equals m.LeaveApplicationMasterID
                           where m.EmpID == EmpID && m.LSLeaveDayTypeID == LSLeaveDayTypeID
                           && d.LeaveDate >= FromDate && d.LeaveDate <= ToDate
                           select m.EmpID;
               return (code != null && code.Count() > 0);
            }
            else
            {
                var code = from d in context.Timesheet_tblLeaveApplicationDetail
                       join m in context.Timesheet_tblLeaveApplicationMaster on d.LeaveApplicationMasterID equals m.LeaveApplicationMasterID
                       where m.EmpID == EmpID && m.LSLeaveDayTypeID == LSLeaveDayTypeID
                       && d.LeaveDate >= FromDate && d.LeaveDate <= ToDate
                       && d.LeaveApplicationMasterID != LeaveApplicationMasterID
                       select m.EmpID;
                return (code != null && code.Count() > 0);
            }
            
        }
        public bool Add(Timesheet_tblLeaveApplicationMaster model, decimal? BalanceCompare, out string errorMessage)
        {
            try
            {
                // thuc hien kiem tra du lieu truoc khi luu : so ngay xin phép nghỉ lon hon ngay phep năm còn lại
                var ALType = (from t in context.Timesheet_tbLeaveDayType
                             join l in context.LS_tblLeaveType on t.LSLeaveTypeID equals l.LSLeaveTypeID
                             where t.LSLeaveDayTypeID == model.LSLeaveDayTypeID
                             select l.LSLeaveTypeCode).FirstOrDefault();
                if (BalanceCompare < model.Days && ALType == "AL")
                {
                    errorMessage = Eagle.Resource.LanguageResource.ErrorAnnualLeave;
                    return false;
                }
                // kiem tra gioi han cao nhat co the nghi  trong 1 lần nghỉ phép
                // thuc hien kiem tra du lieu truoc khi luu : so ngay xin phép nghỉ lon hon ngay phep năm còn lại
                var DaysPerPeriod = (from t in context.Timesheet_tbLeaveDayType
                              where t.LSLeaveDayTypeID == model.LSLeaveDayTypeID
                              select t.DaysPerPeriod).FirstOrDefault();
                if (DaysPerPeriod < model.Days)
                {
                    errorMessage = Eagle.Resource.LanguageResource.ErrorAnnualLeavePeriod + " (" + DaysPerPeriod.ToString() + ")";
                    return false;
                }



                //var ListFromDatabase = context.Timesheet_tblLeaveApplicationMaster.Where(p => p.LeaveApplicationMasterID == model.LeaveApplicationMasterID).ToList();
                //foreach (var deleteitem in ListFromDatabase)
                //{
                //    context.Entry(deleteitem).State = System.Data.Entity.EntityState.Deleted;
                //}

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
        public bool Update(Timesheet_tblLeaveApplicationMaster model, out string errorMessage)
        {
            try
            {
           
                context.Entry(model).State = System.Data.Entity.EntityState.Modified;
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
        public bool AddDetail(List<Timesheet_tblLeaveApplicationDetail> List, out string errorMessage)
        {
            try
            {
                var first = List.First();
                if (first != null)
                {
                    foreach (var additem in List)
                    {
                        context.Entry(additem).State = System.Data.Entity.EntityState.Added;
                    }
                    context.SaveChanges();
                    errorMessage = "success";
                    return true;
                }
                errorMessage = "error";
                return false;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return false;
            }
        }
        #region DropdownList
        private List<AutoCompleteViewModel> GetDropdownList(string searchTerm)
        {
            var tmp = context.Timesheet_tbLeaveDayType
                           .Where(c => c.Name.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.LSLeaveDayTypeID,
                               name = c.Name,
                               text = ""
                           });
            List<AutoCompleteViewModel> ret = new List<AutoCompleteViewModel>();
            foreach (var item in tmp)
            {
                AutoCompleteViewModel p = new AutoCompleteViewModel()
                {
                    id = item.id.ToString(),
                    name = item.name,
                    text = item.text
                };
                ret.Add(p);
            }
            return ret;
        }
        // dùng cho bind dropdownlist
        public List<AutoCompleteViewModel> ListDropdown(string searchTerm, int pageSize, int pageNume)
        {
            return GetDropdownList(searchTerm).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
        }
        #endregion
        public LeaveApplicationCreateViewModel FindEdit(int id)
        {
            try
            {
                var FunctionID = 457; // functionID trong bang SYS_tblFunctionList
                LeaveApplicationCreateViewModel ret = (from m in context.Timesheet_tblLeaveApplicationMaster
                                                       join d in context.Timesheet_tblLeaveApplicationDetail on m.LeaveApplicationMasterID equals d.LeaveApplicationMasterID
                                                       
                                                       join op in context.LS_tblOnlineProcess on FunctionID equals op.FunctionID
                                                       join pom in context.SYS_tblProcessOnlineMaster on op.DMQuiTrinhID equals pom.DMQuiTrinhID into tmpListzz1
                                                       from list1 in tmpListzz1.DefaultIfEmpty()
                                                       
                                                       where m.LeaveApplicationMasterID == id
                                                       select new LeaveApplicationCreateViewModel()
                                                       {
                                                           LeaveApplicationMasterID = m.LeaveApplicationMasterID,
                                                           Year = m.Year,
                                                           EmpID = m.EmpID,
                                                           Status = m.Status,
                                                           LevelApprover = m.LevelApprover,
                                                           LSLeaveDayTypeID = m.LSLeaveDayTypeID,
                                                           LSLeaveDayTypeName = m.Timesheet_tbLeaveDayType.Name,
                                                           FromDate  = m.FromDate,
                                                           ToDate = m.ToDate,
                                                           Comment = m.Comment,
                                                           LeaveTaken = m.Days,
                                                           Creater = m.Creater,
                                                           isFirstComment = m.isFirstComment,
                                                           CurrentComment = m.CurrentComment,
                                                           //// hien thi trang thai 
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
                                                           
                                                       }).FirstOrDefault();                
                
                return ret;
            }
            catch
            {
                return new LeaveApplicationCreateViewModel();
            }
        }
        public Timesheet_tblLeaveApplicationMaster Find(int id)
        {
            return context.Timesheet_tblLeaveApplicationMaster.Include("Timesheet_tblLeaveApplicationDetail").Where(p => p.LeaveApplicationMasterID == id).FirstOrDefault();
        }
        public Timesheet_tblLeaveApplicationDetail FindDetail(int id)
        {
            return context.Timesheet_tblLeaveApplicationDetail.Where(p => p.LeaveApplicationMasterID == id).FirstOrDefault();
        }
        // xoa du lieu tren luoi danh sach leave record
        public bool Delete(int id)
        {
            try
            {


                var ListFromDatabase = context.Timesheet_tblLeaveApplicationDetail.Where(p => p.LeaveApplicationMasterID == id).ToList();
                foreach (var deleteitem in ListFromDatabase)
                {
                    context.Entry(deleteitem).State = System.Data.Entity.EntityState.Deleted;
                }

                Timesheet_tblLeaveApplicationMaster modelUpdate = Find(id);
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return false;
            }

        }
    }
}
