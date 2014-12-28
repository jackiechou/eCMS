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
    public class AddSalaryByStaffRepository
    {
        public EntityDataContext context { get; set; }
        public AddSalaryByStaffRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public List<AdditionSalaryViewModels> GetData(AdditionSalaryViewModels model,int LanguageId,string strEmpCode, string strEmpName, int? LSCompanyID, int? LSPositionID, bool isAdmin, string userGroupID, int moduleID)
        {

            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();

                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();

                return (from e in context.HR_tblEmp
                       join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID into JoinPosition from p in JoinPosition.DefaultIfEmpty()
                       where (LSCompanyID == 0 || LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                       && (strEmpCode == null || e.EmpCode.Contains(strEmpCode))
                       && (strEmpName == null || (e.LastName + " " + e.FirstName).Contains(strEmpName))
                       && (isAdmin == true || jointable.Contains(e.EmpID)) // join voi bang phan quyen du lieu
                       && (LSPositionID == 0 || LSPositionID == null || e.LSPositionID == LSPositionID)
                       
                       select new AdditionSalaryViewModels()
                       {
                           EmpID = e.EmpID,
                           EmpCode = e.EmpCode,
                           FullName = e.LastName + " " + e.FirstName,
                           LSPositionName = LanguageId == 124 ? p.Name : p.VNName,
                           LSSalaryAdjustID = model .LSSalaryAdjustIDNull.Value 

                       }).ToList();
            }
            catch
            {
                return new List<AdditionSalaryViewModels>();
            }
        }

        public List<EmployeeForPayrollViewModels> List(EmployeeForPayrollViewModels model, bool isAdmin, string userGroupID, int moduleID, List<int> StaffIDSelectedList, int LanguageId)
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
                       
                       select new EmployeeForPayrollViewModels()
                       {
                           EmpID = e.EmpID,
                           EmpCode = e.EmpCode,
                           FullName = e.LastName + " " + e.FirstName,
                           Department = LanguageId == 124 ? c.Name : c.VNName,
                           LSPositionName = LanguageId == 124 ? p.Name : p.VNName
                       }).ToList();
            }
            catch
            {
                return new List<EmployeeForPayrollViewModels>();
            }
        }

        public List<EmployeeForPayrollViewModels> GetByListID(List<int> StaffIDListAdd, int LanguageId)
        {
            var result = from e in context.HR_tblEmp
                         join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID into JoinPosition from p in JoinPosition.DefaultIfEmpty()
                         join c in context.LS_tblCompany on e.LSCompanyID equals c.LSCompanyID
                         where StaffIDListAdd.Contains(e.EmpID)
                         select new EmployeeForPayrollViewModels()
                         {
                             EmpID = e.EmpID,
                             EmpCode = e.EmpCode,
                             FullName = e.LastName + " " + e.FirstName,
                             Department = LanguageId == 124 ? c.Name : c.VNName,
                             LSPositionName = LanguageId == 124 ? p.Name : p.VNName
                         };
            return result.ToList();

        }

        public List<AdditionSalaryViewModels> ListSearch(AdditionSalaryViewModels modelSearch, bool isAdmin, string userGroupID, int moduleID,  int LanguageId)
        {
            try
            {
                // phan quyen du lieu
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
                // tim kiem theo cay thu muc cong ty, phong ban, to nhom
                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == modelSearch.LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();

                var ret =
                        (from p in context.PR_tblAdditionSalary
                        join e in context.HR_tblEmp on  p.EmpID equals e.EmpID
                        join l in context.LS_tblPosition on e.LSPositionID equals l.LSPositionID into JoinPosition from l in JoinPosition.DefaultIfEmpty()
                        join c in context.LS_tblCurrency on p.LSCurrencyID equals c.LSCurrencyID into JoinCurrency from c in JoinCurrency.DefaultIfEmpty()
                         where (modelSearch.LSCompanyID == 0 || modelSearch.LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                         && (modelSearch.LSSalaryAdjustIDNullSearch == 0 || modelSearch.LSSalaryAdjustIDNullSearch == null || p.LSSalaryAdjustID == modelSearch.LSSalaryAdjustIDNullSearch)
                         && (modelSearch.EmpCode == null || e.EmpCode.Contains(modelSearch.EmpCode))
                         && (modelSearch.FullName == null || (e.LastName + " " + e.FirstName).Contains(modelSearch.FullName))
                         && jointable.Contains(e.EmpID)

                        select new AdditionSalaryViewModels()
                        {
                            SalaryAdjustID = p.SalaryAdjustID,     
                            FullName = e.LastName + " " + e.FirstName,
                            LSPositionName = LanguageId == 124 ? l.Name : l.VNName,
                            LSSalaryAdjustID = p.LSSalaryAdjustID,
                            LSSalaryAdjustName = LanguageId == 124 ? p.LS_tblSalaryAdjust.Name : p.LS_tblSalaryAdjust.VNName,
                            PaymentMethod = p.PaymentMethod,
                            FromMonth = p.FromMonth,
                            ToMonth = p.ToMonth,
                            Amount = p.Amount,
                            isGross = p.isGross,
                            LSCurrencyName = LanguageId == 124 ? p.LS_tblCurrency.Name : p.LS_tblCurrency.VNName
                        }).ToList();
                List<AdditionSalaryViewModels> lst = new List<AdditionSalaryViewModels>();
                
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
                return new List<AdditionSalaryViewModels>();
            }
        }
        public PR_tblAdditionSalary Find(int id)
        {
            return context.PR_tblAdditionSalary.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                PR_tblAdditionSalary modelUpdate = Find(id);
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
        public AdditionSalaryViewModels FindEdit(int id, int LanguageId)
        {
            try
            {
                AdditionSalaryViewModels ret = context.PR_tblAdditionSalary
                                        .Where(p => p.SalaryAdjustID == id)
                                        .Select(p => new AdditionSalaryViewModels()
                                        {
                                            SalaryAdjustID = p.SalaryAdjustID,
                                            LSSalaryAdjustID = p.LSSalaryAdjustID,
                                            LSSalaryAdjustIDNull = p.LSSalaryAdjustID,
                                            LSSalaryAdjustName = LanguageId == 124 ? p.LS_tblSalaryAdjust.Name : p.LS_tblSalaryAdjust.VNName,
                                            Amount = p.Amount,
                                            PaymentMethod = p.PaymentMethod,
                                            FromMonth = p.FromMonth,
                                            ToMonth = p.ToMonth,
                                            LSCurrencyID = p.LSCurrencyID,
                                            LSCurrencyName = LanguageId == 124 ? p.LS_tblCurrency.Name : p.LS_tblCurrency.VNName,
                                            isGross = p.isGross,
                                            GROSSNET = p.isGross,
                                            Note = p.Note
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new AdditionSalaryViewModels();
            }
        }
        public string Update(AdditionSalaryViewModels model)
        {
            try
            {
                
                PR_tblAdditionSalary modelUpdate = Find(model.SalaryAdjustID);
                modelUpdate.PaymentMethod = model.PaymentMethod;
                modelUpdate.LSSalaryAdjustID = model.LSSalaryAdjustIDNull.Value;
                modelUpdate.FromMonth = model.FromMonth;
                modelUpdate.ToMonth = model.ToMonth;
                modelUpdate.Amount = model.Amount;
                modelUpdate.LSCurrencyID = model.LSCurrencyID;
                modelUpdate.isGross = model.GROSSNET.Value;
                modelUpdate.Note = model.Note;
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
    }
}
