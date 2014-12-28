using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Eagle.Model;
using Eagle.Model.HR;
using Eagle.Core;
using System.Transactions;
using System.Data.Entity.Validation;
using AutoMapper;

namespace Eagle.Repository
{
    public class TimekeepingRepository
    {
        public EntityDataContext context { get; set; }
        public TimekeepingRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public Timesheet_tblTimeKeeping_YYYYMM Find(int id)
        {
            var model = context.vTimeKeepings.Find(id);
            Timesheet_tblTimeKeeping_YYYYMM ret = new Timesheet_tblTimeKeeping_YYYYMM();
            if (model != null)
            {
                Mapper.CreateMap<vTimeKeeping, Timesheet_tblTimeKeeping_YYYYMM>();
                Mapper.Map(model, ret);
                return ret;
            }
            else
            {
                return null;
            }
        }
        public string Update(int TimekeepingID, decimal workDays, string MMYYYY)
        {

            try
            {
                Timesheet_tblTimeKeeping_YYYYMM modelUpdate = Find(TimekeepingID);
                modelUpdate.WorkDays = workDays;
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return strErr;
            }
        }
        public IEnumerable<TimekeepingListViewModel> _Search(string MMYYYY, DateTime FromDate, DateTime ToDate, string strEmpCode, string strEmpName, int? LSCompanyID, bool isAdmin, string userGroupID, int moduleID,int type)
        {

            try
            {

                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();

                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();

                return (from s in context.vTimeKeepings
                        join e in context.HR_tblEmp on s.EmpID equals e.EmpID
                        join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID into JoinPosition
                        from p in JoinPosition.DefaultIfEmpty()
                        join ld in context.Timesheet_tbLeaveDayType on s.LSLeaveDayTypeID equals ld.LSLeaveDayTypeID into JoinLeaveDayType
                        from ld in JoinLeaveDayType.DefaultIfEmpty()
                        join sh in context.Timesheet_tblShift on s.LSShiftID equals sh.LSShiftID into JoinShift
                        from sh in JoinShift.DefaultIfEmpty()
                        where (LSCompanyID == 0 || LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                        && (strEmpCode == null || e.EmpCode.Contains(strEmpCode))
                        && (strEmpName == null || (e.LastName + " " + e.FirstName).Contains(strEmpName))
                        && jointable.Contains(e.EmpID) // join voi bang phan quyen du lieu
                        //&& s.Type == type  // 1 cham cong tay, 2 cham cong qua may
                        && s.MMYYYY == MMYYYY && (s.DateID >= FromDate && s.DateID <= ToDate)
                        select new TimekeepingListViewModel()
                        {
                            EmpID = e.EmpID,
                            EmpCode = e.EmpCode,
                            EmpName = e.FirstName,
                            FullName = e.LastName + " " + e.FirstName,
                            PositonName = p.Name,
                            MMYYYY = MMYYYY,
                            WorkDays = s.WorkDays,
                        } into x
                        group x by new
                        {
                            x.EmpID,x.EmpCode,x.EmpName,x.FullName,x.PositonName, x.MMYYYY
                        } into g
                        orderby g.Key.EmpName ascending
                        select new TimekeepingListViewModel()
                        {
                            EmpID = g.Key.EmpID,
                            EmpCode = g.Key.EmpCode,
                            EmpName = g.Key.EmpName,
                            FullName = g.Key.FullName,
                            PositonName = g.Key.PositonName,
                            MMYYYY = g.Key.MMYYYY,
                            WorkDays = g.Sum(p => p.WorkDays)
                        }).ToList();
            }
            catch
            {
                return new List<TimekeepingListViewModel>();
            }
        }
        public List<TimekeepingListViewModel> _ListPopup(string MMYYYY, int EmpID)
        {

            try
            {
                return (from s in context.vTimeKeepings
                        join e in context.HR_tblEmp on s.EmpID equals e.EmpID
                        join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID into JoinPosition from p in JoinPosition.DefaultIfEmpty()
                        join ld in context.Timesheet_tbLeaveDayType on s.LSLeaveDayTypeID equals ld.LSLeaveDayTypeID into JoinLeaveDayType from ld in JoinLeaveDayType.DefaultIfEmpty() 
                        join sh in context.Timesheet_tblShift on s.LSShiftID equals sh.LSShiftID into JoinShift from sh in JoinShift.DefaultIfEmpty()
                        where s.MMYYYY == MMYYYY && s.EmpID == EmpID //&& s.Type == 1   
                        orderby s.DateID ascending
                        select new TimekeepingListViewModel()
                        {
                            EmpID = e.EmpID,
                            EmpCode = e.EmpCode,
                            EmpName = e.FirstName,
                            FullName = e.LastName + " " + e.FirstName,
                            PositonName = p.Name,
                            DateID = s.DateID,
                            LeaveType = ld.Name,
                            LeaveDays = s.LeaveDays,
                            WorkDays = s.WorkDays,
                            TimekeepingID = s.TimekeepingID
                        }).ToList();
            }
            catch
            {
                return new List<TimekeepingListViewModel>();
            }
        }
    }
}
