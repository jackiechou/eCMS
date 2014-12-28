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
    public class MTLChildRepository
    {
         public EntityDataContext context { get; set; }
         public MTLChildRepository(EntityDataContext context)
         {
            this.context = context;
         }
         public bool Add(List<Timesheet_tblMTLChild> List, out string errorMessage)
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
         public Timesheet_tblMTLChild Find(int id)
         {
             return context.Timesheet_tblMTLChild.Find(id);
         }
         public bool Update(List<Timesheet_tblMTLChild> List, out string errorMessage)
         {
             try
             {
                 var first = List.First();
                 if (first != null)
                 {
                     foreach (var updateitem in List)
                     {
                         Timesheet_tblMTLChild modelUpdate = Find(updateitem.MLChildID);
                         modelUpdate.FromDate = updateitem.FromDate;
                         modelUpdate.ToDate = updateitem.ToDate;
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
         public bool Delete(int id)
         {
             try
             {
                 Timesheet_tblMTLChild modelUpdate = Find(id);
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
         public IEnumerable<MTLChildListViewModel> ListIn(string strEmpCode, string strEmpName, int? LSCompanyID, bool isAdmin, string userGroupID, int moduleID)
        {

            try
            {
                // phan quyen du lieu
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();

                // tim kiem theo cay thu muc cong ty, phong ban, to nhom
                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();

                return from e in context.HR_tblEmp
                       join s in context.Timesheet_tblMTLChild on e.EmpID equals s.EmpID
                       join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID into JoinPosition from p in JoinPosition.DefaultIfEmpty()
                       where (LSCompanyID == 0 || LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                       && (strEmpCode == null || e.EmpCode.Contains(strEmpCode))
                       && (strEmpName == null || (e.LastName + " " + e.FirstName).Contains(strEmpName))
                       && jointable.Contains(e.EmpID) // join voi bang phan quyen du lieu
                       select new MTLChildListViewModel()
                       {
                           MLChildID = s.MLChildID,
                           EmpID = e.EmpID,
                           EmpCode = e.EmpCode,
                           FullName = e.LastName + " " + e.FirstName,
                           FromDate = s.FromDate,
                           ToDate = s.ToDate,
                           dFromDate = s.FromDate,
                           dToDate = s.ToDate,
                           strPositionName = p.Name

                       };
            }
            catch
            {
                return new List<MTLChildListViewModel>();
            }
        }
        public IEnumerable<MTLChildListViewModel> ListNotIn(string strEmpCode, string strEmpName, int? LSCompanyID, bool isAdmin, string userGroupID, int moduleID)
        {

            try
            {
                // phan quyen du lieu
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
                // tim kiem theo cay thu muc cong ty
                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();


                return from e in context.HR_tblEmp
                       join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID into JoinPosition from p in JoinPosition.DefaultIfEmpty()
                       where !(from u in context.Timesheet_tblMTLChild
                               select u.EmpID).Contains(e.EmpID)
                       && (LSCompanyID == 0 || LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                       && (strEmpCode == null || e.EmpCode.Contains(strEmpCode))
                       && (strEmpName == null || (e.LastName + " " + e.FirstName).Contains(strEmpName))
                       && jointable.Contains(e.EmpID) // join voi bang phan quyen du lieu
                       select new MTLChildListViewModel()
                       {
                           MLChildID = 0,
                           EmpID = e.EmpID,
                           EmpCode = e.EmpCode,
                           FullName = e.LastName + " " + e.FirstName,
                           strPositionName = p.Name
                       };
            }
            catch
            {
                return new List<MTLChildListViewModel>();
            }
        }
    }
}
