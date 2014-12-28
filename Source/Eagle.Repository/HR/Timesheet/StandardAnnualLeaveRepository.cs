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
namespace Eagle.Repository
{
    public class StandardAnnualLeaveRepository
    {
        public EntityDataContext context { get; set; }
        public StandardAnnualLeaveRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public string AddAdjust(Timesheet_tblAnnualLeaveAdjust model)
        {
            try
            {
                context.Entry(model).State = System.Data.EntityState.Added;
                context.SaveChanges();
                return "success";
            }
            catch 
            {
                return "error";
            }
        }
        public string UpdateAdjust(StandardAnnualLeaveAdjustViewModel model)
        {
            try
            {
                Timesheet_tblAnnualLeaveAdjust modelUpdate = FindAdjust(model.ALAdjustID);
                modelUpdate.Days = model.Days;
                modelUpdate.Year = model.Year;
                modelUpdate.Note = model.Note;
                context.Entry(modelUpdate).State = System.Data.EntityState.Modified;
                context.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return strErr;
            }
        }
        public bool Add(List<Timesheet_tblStandAnualLeaveDays> List, out string errorMessage)
        {
            try
            {
                var first = List.First();
                if (first != null)
                {
                    foreach (var additem in List)
                    {
                        context.Entry(additem).State = System.Data.EntityState.Added;
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
        public Timesheet_tblStandAnualLeaveDays Find(int id)
        {
            return context.Timesheet_tblStandAnualLeaveDays.Find(id);
        }
        public Timesheet_tblAnnualLeaveAdjust FindAdjust(int id)
        {
            return context.Timesheet_tblAnnualLeaveAdjust.Find(id);
        }
        public StandardAnnualLeaveAdjustViewModel FindEdit(int id)
        {
            try
            {
                StandardAnnualLeaveAdjustViewModel ret = context.Timesheet_tblAnnualLeaveAdjust
                                        .Where(p => p.ALAdjustID == id)
                                        .Select(p => new StandardAnnualLeaveAdjustViewModel()
                                        {
                                            ALAdjustID = p.ALAdjustID,
                                            Year = p.Year,
                                            Days =p.Days,
                                            EmpID = p.EmpID,
                                            Note = p.Note
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new StandardAnnualLeaveAdjustViewModel();
            }

        }
        public bool DeleteAdjust(int id)
        {
            try
            {
                Timesheet_tblAnnualLeaveAdjust modelUpdate = FindAdjust(id);
                context.Entry(modelUpdate).State = System.Data.EntityState.Deleted;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return false;
            }

        }
        public bool Update(List<Timesheet_tblStandAnualLeaveDays> List, out string errorMessage)
        {
            try
            {
                var first = List.First();
                if (first != null)
                {
                    foreach (var updateitem in List)
                    {
                        Timesheet_tblStandAnualLeaveDays modelUpdate = Find(updateitem.StandardALID);
                        modelUpdate.StandardALDays = updateitem.StandardALDays;
                        context.Entry(modelUpdate).State = System.Data.EntityState.Modified;
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
        public IEnumerable<StandardAnnualLeaveCreateViewModel> List(bool isAdmin, string userGroupID, int moduleID)
        {
            try
            {
                var StandardAL = context.SYS_tblParameter.Select(p => p.StandardAnnualLeave).FirstOrDefault();

                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
                return from e in context.HR_tblEmp
                       join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID
                           // danh cho left join
                        into JoinPosition
                       from p in JoinPosition.DefaultIfEmpty()
                       where !(from u in context.Timesheet_tblStandAnualLeaveDays
                               select u.EmpID).Contains(e.EmpID)
                       && (isAdmin == true || jointable.Contains(e.EmpID)) // join voi bang phan quyen du lieu
                       select new StandardAnnualLeaveCreateViewModel()
                       {
                           StandardALID = 0,
                           EmpID = e.EmpID,
                           EmpCode = e.EmpCode,
                           FullName = e.LastName + " " + e.FirstName,
                           StartDate = e.StartDate,
                           StandardALDays = StandardAL,
                           strPositonName = p.Name

                       };
            }
            catch
            {
                return new List<StandardAnnualLeaveCreateViewModel>();
            }
        }
        public IEnumerable<StandardAnnualLeaveAdjustViewModel> ListAdjust(int? EmpID)
        {
            try
            {
                
                return from e in context.Timesheet_tblAnnualLeaveAdjust                                              
                       where e.EmpID == EmpID.Value
                       select new StandardAnnualLeaveAdjustViewModel()
                       {
                           ALAdjustID = e.ALAdjustID,
                           Year = e.Year,
                           EmpID = e.EmpID,
                           Days = e.Days,
                           AdjustDate = e.AdjustDate,                           
                           Note = e.Note
                       };
            }
            catch
            {
                return new List<StandardAnnualLeaveAdjustViewModel>();
            }
        }

        public IEnumerable<StandardAnnualLeaveCreateViewModel> ListIn(string strEmpCode, string strEmpName, int? LSCompanyID, bool isAdmin, string userGroupID, int moduleID)
        {

            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
               
                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();

                return from e in context.HR_tblEmp
                       join s in context.Timesheet_tblStandAnualLeaveDays on e.EmpID equals s.EmpID
                       join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID into JoinPosition from p in JoinPosition.DefaultIfEmpty()
                       where (LSCompanyID == 0 || LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                       && (strEmpCode == null || e.EmpCode.Contains(strEmpCode))
                       && (strEmpName == null || (e.LastName + " " + e.FirstName).Contains(strEmpName))
                       && (isAdmin == true || jointable.Contains(e.EmpID)) // join voi bang phan quyen du lieu
                       select new StandardAnnualLeaveCreateViewModel()
                       {
                           StandardALID = s.StandardALID,
                           EmpID = e.EmpID,
                           EmpCode = e.EmpCode,
                           FullName = e.LastName + " " + e.FirstName,
                           StartDate = e.StartDate,
                           StandardALDays = s.StandardALDays,
                           strPositonName = p.Name

                       };
            }
            catch
            {
                return new List<StandardAnnualLeaveCreateViewModel>();
            }
        }
        public IEnumerable<StandardAnnualLeaveCreateViewModel> ListNotIn(string strEmpCode, string strEmpName, int? LSCompanyID, bool isAdmin, string userGroupID, int moduleID)
        {

            try
            {
                var StandardAL = context.SYS_tblParameter.Select(p => p.StandardAnnualLeave).FirstOrDefault();
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();
                return from e in context.HR_tblEmp
                       join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID into JoinPosition
                       from p in JoinPosition.DefaultIfEmpty()
                       where !(from u in context.Timesheet_tblStandAnualLeaveDays
                               select u.EmpID).Contains(e.EmpID)
                       && (LSCompanyID == 0 || LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                       && (strEmpCode == null || e.EmpCode.Contains(strEmpCode))
                       && (strEmpName == null || (e.LastName + " " + e.FirstName).Contains(strEmpName))
                       && (isAdmin == true || jointable.Contains(e.EmpID)) // join voi bang phan quyen du lieu
                       select new StandardAnnualLeaveCreateViewModel()
                       {
                           StandardALID = 0,
                           EmpID = e.EmpID,
                           EmpCode = e.EmpCode,
                           FullName = e.LastName + " " + e.FirstName,
                           StandardALDays = StandardAL,
                           StartDate = e.StartDate,
                           strPositonName = p.Name

                       };
            }
            catch
            {
                return new List<StandardAnnualLeaveCreateViewModel>();
            }
        }
    }
}
