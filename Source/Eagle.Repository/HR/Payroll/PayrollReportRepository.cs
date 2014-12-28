using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;
using Eagle.Model;

namespace Eagle.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class PayrollReportRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public EntityDataContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        public PayrollReportRepository(EntityDataContext Context)
        {
            this.Context = Context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public List<PayrollReportViewModel> GetListOfPayroll(int EmpID, int Year, out string errorMessage)
        {
            try
            {
                var found = (from pay in this.Context.PR_vPayRoll
                             join emp in this.Context.HR_tblEmp on pay.EmpID equals emp.EmpID

                             join com in this.Context.LS_tblCompany on emp.LSCompanyID equals com.LSCompanyID into EmployeeCompany
                             from com in EmployeeCompany.DefaultIfEmpty()

                             join pos in this.Context.LS_tblPosition on emp.LSPositionID equals pos.LSPositionID into EmployeePosition
                             from pos in EmployeeCompany.DefaultIfEmpty()

                             join cur in this.Context.LS_tblCurrency on pay.LSCurrencyIDSalary equals cur.LSCurrencyID into PayCurrency
                             from cur in PayCurrency.DefaultIfEmpty()

                             where (pay.EmpID == EmpID && pay.Year == Year)
                             select new PayrollReportViewModel()
                             {
                                 Month = pay.Month,
                                 Year = pay.Year,
                                 Salary = pay.Salary,
                                 AdditionSalaryBeforTax = pay.AdditionSalaryBeforTax,
                                 OT = pay.OT,
                                 OT_NoneTax = pay.OT_NoneTax,
                                 AdditionSalaryAfterTax = pay.AdditionSalaryAfterTax,
                                 InS = pay.InS,
                                 InH = pay.InH,
                                 InE = pay.InE,
                                 SelfDeduction = pay.SelfDeduction,
                                 Dependent = pay.Dependent,
                                 DependentDeduction = pay.DependentDeduction,
                                 IncomeTax = pay.IncomeTax,
                                 NetTakeHome = pay.NetTakeHome,
                                 CompanyName = com.Name,
                                 PositionName = pos.Name,
                                 CurrencyName = cur.Name
                             }).ToList();

                errorMessage = String.Empty;
                return found;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<PR_sprptPaySlipReport_Result> GetOfEmployeePayroll(int EmpID, int Month, int Year, out string errorMessage)
        {
            try
            {
                var found = this.Context.PR_sprptPaySlipReport(EmpID, Month, Year).ToList();
                errorMessage = String.Empty;

                return found;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<PR_sprptPaySlipReport_Result> GetListOfEmployeePayroll(int EmpID, int Year, out string errorMessage)
        {
            try
            {
                var found = this.Context.PR_sprptPaySlipReport(EmpID, 0, Year).ToList();
                errorMessage = String.Empty;

                return found;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<PR_sprptPayrollSummaryReport_Result> GetPayrollSummary(string Username, int ModuleID, PayrollSummaryViewModel model, out string errorMessage)
        {
            try
            {
                //var found = this.Context.PR_sprptPayrollSummaryReport(Username, ModuleID, model.Month, model.Year, model.LSCompanyID, model.LSPositionID, model.EmployeeCode, model.EmployeeName).ToList();
                var found = this.Context.PR_sprptPayrollSummaryReport(Username, ModuleID, model.Month, model.Year, model.LSCompanyID, model.LSPositionID, model.EmployeeCode, model.EmployeeName).ToList();
                errorMessage = String.Empty;

                return found;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<PR_sprptPayrollSummaryReport_Result> GetPayrollSummaryReport(string Username, int ModuleID, int? LSCompanyID, int? LSPositionID, string EmployeeCode, string EmployeeName, string MonthYear, out string errorMessage)
        {
            try
            {
                var Month = -1;
                var Year = -1;
                if (!String.IsNullOrEmpty(MonthYear))
                {
                    var MonthInfo = MonthYear.Substring(0, 2);
                    var YearInfo = MonthYear.Substring(6, 4);
                    if (Eagle.Common.Utilities.ValidatorUtils.CheckNumber(MonthInfo))
                    {
                        Month = Convert.ToInt16(MonthInfo);
                    }
                    if (Eagle.Common.Utilities.ValidatorUtils.CheckNumber(YearInfo))
                    {
                        Year = Convert.ToInt16(YearInfo);
                    }
                }
                var found = this.Context.PR_sprptPayrollSummaryReport(Username, ModuleID, Month, Year, LSCompanyID, LSPositionID, EmployeeCode, EmployeeName).ToList();
                errorMessage = String.Empty;

                return found;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LSCompanyID"></param>
        /// <param name="Quarter"></param>
        /// <param name="Year"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<PR_sprpt_02KKTNCN_Result> GetPIT02KKReport(int? LSCompanyID, string Quarter, int? Year, out string errorMessage)
        {
            try
            {
                var found = this.Context.PR_sprpt_02KKTNCN(LSCompanyID, Quarter, Year).ToList();
                errorMessage = String.Empty;

                return found;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }
    }
}
