using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Eagle.Core;
using Eagle.Repository;
using Eagle.Model;
using System.Web.Routing;
using Eagle.Common.Helpers;
using System.Data.Entity.Validation;
using Eagle.Common.Utilities;
namespace Eagle.Repository
{
    public class SalaryDeductionByStaffRepository
    {
        public EntityDataContext context { get; set; }
        public SalaryDeductionByStaffRepository(EntityDataContext context)
        {
            this.context = context;
        }        
        public List<EmployeeForPayrollEagle.Model> List(EmployeeForPayrollEagle.Model model, bool isAdmin, string userGroupID, int moduleID, List<int> StaffIDSelectedList, int LanguageID)
        {
            try
            {
                // phan quyen du lieu
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
                // tim kiem theo cay thu muc cong ty
                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();


                return (from e in context.HR_tblEmp
                       join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID into JoinPosition from p in JoinPosition.DefaultIfEmpty()
                       join c in context.LS_tblCompany on e.LSCompanyID equals c.LSCompanyID
                       where !StaffIDSelectedList.Contains(e.EmpID) && (model.LSCompanyID == 0 || model.LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                       && (model.EmpCode == null || e.EmpCode.Contains(model.EmpCode))
                       && (model.FullName == null || (e.LastName + " " + e.FirstName).Contains(model.FullName))
                       && jointable.Contains(e.EmpID) // join voi bang phan quyen du lieu
                       
                       select new EmployeeForPayrollEagle.Model()
                       {
                           EmpID = e.EmpID,
                           EmpCode = e.EmpCode,
                           FullName = e.LastName + " " + e.FirstName,
                           Department = LanguageID == 10001 ? c.Name : c.VNName,
                           LSPositionName = LanguageID == 10001 ? p.Name : p.VNName
                       }).ToList();
            }
            catch
            {
                return new List<EmployeeForPayrollEagle.Model>();
            }
        }
        public List<EmployeeForPayrollEagle.Model> GetByListID(List<int> StaffIDListAdd, int LanguageID)
        {
            var result = from e in context.HR_tblEmp
                         join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID into JoinPosition from p in JoinPosition.DefaultIfEmpty()
                         join c in context.LS_tblCompany on e.LSCompanyID equals c.LSCompanyID
                         where StaffIDListAdd.Contains(e.EmpID)
                         select new EmployeeForPayrollEagle.Model()
                         {
                             EmpID = e.EmpID,
                             EmpCode = e.EmpCode,
                             FullName = e.LastName + " " + e.FirstName,
                             Department = LanguageID == 10001 ? c.Name : c.VNName,
                             LSPositionName = LanguageID == 10001 ? p.Name : p.VNName
                         };
            return result.ToList();

        }
        public List<DeductionSalaryEagle.Model> ListSearch(DeductionSalarySearchEagle.Model modelSearch, bool isAdmin, string userGroupID, int moduleID,  int LanguageID)
        {
            try
            {
                // phan quyen du lieu
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
                // tim kiem theo cay thu muc cong ty, phong ban, to nhom
                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == modelSearch.LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();

                var ret =
                        (from p in context.PR_tblDeductionSalary
                        join e in context.HR_tblEmp on  p.EmpID equals e.EmpID
                        join l in context.LS_tblPosition on e.LSPositionID equals l.LSPositionID into JoinPosition from l in JoinPosition.DefaultIfEmpty()
                        join c in context.LS_tblCurrency on p.LSCurrencyID equals c.LSCurrencyID into JoinCurrency from c in JoinCurrency.DefaultIfEmpty()
                        where (modelSearch.LSCompanyID == 0 || modelSearch.LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                        && (modelSearch.LSDeductSalaryIDNullSearch == 0 || modelSearch.LSDeductSalaryIDNullSearch == null || p.LSDeductSalaryID == modelSearch.LSDeductSalaryIDNullSearch)
                        && (modelSearch.EmpCode == null || e.EmpCode.Contains(modelSearch.EmpCode))
                        && (modelSearch.FullName == null || (e.LastName + " " + e.FirstName).Contains(modelSearch.FullName))
                        && jointable.Contains(e.EmpID)

                        select new DeductionSalaryEagle.Model()
                        {
                            DeductionSalaryID = p.DeductionSalaryID,     
                            FullName = e.LastName + " " + e.FirstName,
                            LSPositionName = LanguageID == 10001 ? l.Name : l.VNName,
                            LSDeductSalaryID = p.LSDeductSalaryID,
                            LSDeductSalaryName = LanguageID == 10001 ? p.LS_tblSalaryDeduction.Name : p.LS_tblSalaryDeduction.VNName,
                            PaymentMethod = p.PaymentMethod,
                            FromMonth = p.FromMonth,
                            ToMonth = p.ToMonth,
                            Amount = p.Amount,
                            isGross = p.isGross,
                            LSCurrencyName = LanguageID == 10001 ? p.LS_tblCurrency.Name : p.LS_tblCurrency.VNName
                        }).ToList();

                List<DeductionSalaryEagle.Model> lst = new List<DeductionSalaryEagle.Model>();
                
                DateTime? dFromMonthModel = new DateTime();
                DateTimeUtils.TryConvertToDate("01/" + modelSearch.FromMonth, out dFromMonthModel);
                DateTime? dToMonthModel = new DateTime();
                DateTimeUtils.TryConvertToDate("01/" + modelSearch.ToMonth, out dToMonthModel);

                foreach (var item in ret)
                {
                    DateTime? dFromMonth = new DateTime();
                    DateTimeUtils.TryConvertToDate("01/" + item.FromMonth, out dFromMonth);
                    DateTime? dToMonth = new DateTime();
                    DateTimeUtils.TryConvertToDate("01/" + item.ToMonth, out dToMonth);
                    if (
                          (dFromMonthModel <= dFromMonth && dFromMonth <= dToMonthModel)  ||
                          (dFromMonthModel <= dToMonth && dToMonth <= dToMonthModel) ||
                          ( 
                          dFromMonth <= dFromMonthModel && dFromMonthModel <= dToMonth
                          &&
                          dFromMonth <= dToMonthModel && dToMonthModel <= dToMonth
                          )  
                       )
                    {
                        lst.Add(item);
                    }
                }
                return lst;
            }
            catch
            {
                return new List<DeductionSalaryEagle.Model>();
            }
        }
        public PR_tblDeductionSalary Find(int id)
        {
            return context.PR_tblDeductionSalary.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                PR_tblDeductionSalary modelUpdate = Find(id);
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
        public DeductionSalaryEagle.Model FindEdit(int id, int LanguageID)
        {
            try
            {
                DeductionSalaryEagle.Model ret = context.PR_tblDeductionSalary
                                        .Where(p => p.DeductionSalaryID == id)
                                        .Select(p => new DeductionSalaryEagle.Model()
                                        {
                                            DeductionSalaryID = p.DeductionSalaryID,
                                            LSDeductSalaryID = p.LSDeductSalaryID,
                                            LSDeductSalaryIDNull = p.LSDeductSalaryID,
                                            LSDeductSalaryName = LanguageID == 10001 ? p.LS_tblSalaryDeduction.Name : p.LS_tblSalaryDeduction.VNName,
                                            Amount = p.Amount,
                                            PaymentMethod = p.PaymentMethod,
                                            FromMonth = p.FromMonth,
                                            ToMonth = p.ToMonth,
                                            LSCurrencyID = p.LSCurrencyID,
                                            LSCurrencyName = LanguageID == 10001 ? p.LS_tblCurrency.Name : p.LS_tblCurrency.VNName,
                                            isGross = p.isGross,
                                            GROSSNET = p.isGross,
                                            Note = p.Note
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new DeductionSalaryEagle.Model();
            }
        }
        public string Update(DeductionSalaryEagle.Model model)
        {
            try
            {

                PR_tblDeductionSalary modelUpdate = Find(model.DeductionSalaryID);
                modelUpdate.PaymentMethod = model.PaymentMethod;
                modelUpdate.LSDeductSalaryID = model.LSDeductSalaryIDNull.Value;
                modelUpdate.FromMonth = model.FromMonth;
                modelUpdate.ToMonth = model.ToMonth;
                modelUpdate.Amount = model.Amount;
                modelUpdate.LSCurrencyID = model.LSCurrencyID;
                modelUpdate.isGross = model.GROSSNET.Value;
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
    }
}
