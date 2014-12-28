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
    public class OTLimitByStaffRepository
    {
         public EntityDataContext context { get; set; }
         public OTLimitByStaffRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool Add(List<Timesheet_tblOTLimitByStaff> List, out string errorMessage)
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
        public bool Delete(int id)
        {
            try
            {
                Timesheet_tblOTLimitByStaff modelUpdate = Find(id);
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
        public Timesheet_tblOTLimitByStaff Find(int id)
        {
            return context.Timesheet_tblOTLimitByStaff.Find(id);
        }

        public bool Update(List<Timesheet_tblOTLimitByStaff> List, out string errorMessage)
        {
            try
            {
                var first = List.First();
                if (first != null)
                {
                    foreach (var updateitem in List)
                    {
                        Timesheet_tblOTLimitByStaff modelUpdate = Find(updateitem.OTLimitByStaffID);
                        modelUpdate.Hours = updateitem.Hours;
                        context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
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
        public IEnumerable<OTLimitByStaffListViewModel> List(bool isAdmin, string userGroupID, int moduleID)
        {
            try
            {
                var StandardHours = context.SYS_tblParameter.Select(p => p.OTLimit).FirstOrDefault();

                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
                return from e in context.HR_tblEmp
                       join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID
                           // danh cho left join
                        into JoinPosition
                       from p in JoinPosition.DefaultIfEmpty()
                       where !(from u in context.Timesheet_tblOTLimitByStaff
                               select u.EmpID).Contains(e.EmpID)
                       && (isAdmin == true || jointable.Contains(e.EmpID)) // join voi bang phan quyen du lieu
                       select new OTLimitByStaffListViewModel()
                       {
                           OTLimitByStaffID = 0,
                           EmpID = e.EmpID,
                           EmpCode = e.EmpCode,
                           FullName = e.LastName + " " + e.FirstName,
                           Hours = StandardHours,
                           strPositonName = p.Name

                       };
            }
            catch
            {
                return new List<OTLimitByStaffListViewModel>();
            }
        }
        public IEnumerable<OTLimitByStaffListViewModel> ListIn(string strEmpCode, string strEmpName, int? LSCompanyID, bool isAdmin, string userGroupID, int moduleID)
        {

            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
               
                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();

                return from e in context.HR_tblEmp
                       join s in context.Timesheet_tblOTLimitByStaff on e.EmpID equals s.EmpID
                       join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID into JoinPosition from p in JoinPosition.DefaultIfEmpty()
                       where (LSCompanyID == 0 || LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                       && (strEmpCode == null || e.EmpCode.Contains(strEmpCode))
                       && (strEmpName == null || (e.LastName + " " + e.FirstName).Contains(strEmpName))
                       && (isAdmin == true || jointable.Contains(e.EmpID)) // join voi bang phan quyen du lieu
                       select new OTLimitByStaffListViewModel()
                       {
                           OTLimitByStaffID = s.OTLimitByStaffID,
                           EmpID = e.EmpID,
                           EmpCode = e.EmpCode,
                           FullName = e.LastName + " " + e.FirstName,
                           Hours = s.Hours,
                           strPositonName = p.Name

                       };
            }
            catch
            {
                return new List<OTLimitByStaffListViewModel>();
            }
        }
        public IEnumerable<OTLimitByStaffListViewModel> ListNotIn(string strEmpCode, string strEmpName, int? LSCompanyID, bool isAdmin, string userGroupID, int moduleID)
        {

            try
            {
                var StandardOTLimit = context.SYS_tblParameter.Select(p => p.OTLimit).FirstOrDefault();
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();
                return from e in context.HR_tblEmp
                       join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID into JoinPosition
                       from p in JoinPosition.DefaultIfEmpty()
                       where !(from u in context.Timesheet_tblOTLimitByStaff
                               select u.EmpID).Contains(e.EmpID)
                       && (LSCompanyID == 0 || LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                       && (strEmpCode == null || e.EmpCode.Contains(strEmpCode))
                       && (strEmpName == null || (e.LastName + " " + e.FirstName).Contains(strEmpName))
                       && (isAdmin == true || jointable.Contains(e.EmpID)) // join voi bang phan quyen du lieu
                       select new OTLimitByStaffListViewModel()
                       {
                           OTLimitByStaffID = 0,
                           EmpID = e.EmpID,
                           EmpCode = e.EmpCode,
                           FullName = e.LastName + " " + e.FirstName,
                           Hours = StandardOTLimit,
                           strPositonName = p.Name

                       };
            }
            catch
            {
                return new List<OTLimitByStaffListViewModel>();
            }
        }
    }
}
