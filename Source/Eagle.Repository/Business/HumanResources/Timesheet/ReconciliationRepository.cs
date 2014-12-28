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
    public class ReconciliationRepository
    {
        public EntityDataContext context { get; set; }
        public ReconciliationRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public List<ReconciliationViewModel> ListNotIn(string strEmpCode, string strEmpName, bool isAdmin, string userGroupID, int moduleID, int Year, int LSCompanyID)
        {

            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
                // dung tim kiem theo cay thuc muc LSCompany
                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();

                var lstReconciliation = context.SYS_spfrmReconciliation(Year, strEmpCode, strEmpName)
                                        .Select(p => new ReconciliationViewModel()
                                        {
                                           ReconciliationID  = 0,
                                           EmpID = p.EmpID,
                                           EmpCode = p.EmpCode,
                                           FullName = p.FullName,
                                           ALPrevious = p.TotalLeave,
                                           ALTakenPrevious = p.LeaveTaken,
                                           ALBalancePrevious = p.Remain,
                                           LeaveForward = p.Remain,
                                           Year = Year
                                        }).ToList();

                return (from a in lstReconciliation
                        join b in jointable on a.EmpID equals b.Value                        
                        join e in context.HR_tblEmp on a.EmpID equals e.EmpID
                        join c in context.LS_tblCompany on e.LSCompanyID equals c.LSCompanyID into lj from  lj1 in lj.DefaultIfEmpty()
                        where (isAdmin == true || jointable.Contains(a.EmpID))
                        && (LSCompanyID == 0 || allChildenCompany.Contains(e.LSCompanyID))
                        select new ReconciliationViewModel()
                        {
                            ReconciliationID = a.ReconciliationID,
                            EmpID = a.EmpID,
                            EmpCode = a.EmpCode,
                            FullName = a.FullName,
                            ALPrevious = a.ALPrevious,
                            ALTakenPrevious = a.ALTakenPrevious,
                            ALBalancePrevious = a.ALBalancePrevious,
                            LeaveForward = a.LeaveForward,
                            Year = Year,
                            CompanyName = lj1.Name,
                            CompanyNameVN = lj1.VNName
                        }).ToList();
                        
            }
            catch
            {
                return new List<ReconciliationViewModel>();
            }
        }
        public bool Add(List<Timesheet_tblReconciliation> List, out string errorMessage)
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

        public IEnumerable<ReconciliationViewModel> ListIn(string strEmpCode, string strEmpName, bool isAdmin, string userGroupID, int moduleID, int Year, int LSCompanyID)
        {
            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();

                // dung tim kiem theo cay thuc muc LSCompany
                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();

                return from r in context.Timesheet_tblReconciliation
                       join e in context.HR_tblEmp on r.EmpID equals e.EmpID
                       join c in context.LS_tblCompany on e.LSCompanyID equals c.LSCompanyID
                       where  (strEmpCode == null || e.EmpCode.Contains(strEmpCode))
                       && (strEmpName == null || (e.LastName + " " + e.FirstName).Contains(strEmpName))
                       && (r.Year == Year)
                       && (LSCompanyID == 0 || LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                       && (isAdmin == true || jointable.Contains(e.EmpID)) // join voi bang phan quyen du lieu

                       select new ReconciliationViewModel()
                       {
                           ReconciliationID = r.ReconciliationID,
                           EmpID = r.EmpID,
                           EmpCode = e.EmpCode,
                           FullName = e.LastName +  " " + e.FirstName,
                           ALPrevious = r.ALPrevious,
                           ALTakenPrevious = r.ALTakenPrevious,
                           ALBalancePrevious = r.ALBalancePrevious,
                           LeaveForward = r.LeaveForward,
                           LeaveForwardTaken = r.LeaveForwardTaken,
                           Year = r.Year,
                           CompanyName = c.Name,
                           CompanyNameVN = c.VNName
                       };
            }
            catch
            {
                return new List<ReconciliationViewModel>();
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Timesheet_tblReconciliation modelUpdate = Find(id);
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

        public Timesheet_tblReconciliation Find(int id)
        {
            return context.Timesheet_tblReconciliation.Find(id);
        }
    }
}
