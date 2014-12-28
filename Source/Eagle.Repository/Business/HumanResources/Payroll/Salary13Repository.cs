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
    public class Salary13Repository
    {
        public EntityDataContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        public Salary13Repository(EntityDataContext Context)
        {
            this.Context = Context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<Salary13ViewModel> GetListOfSalary13(Salary13ViewModel model, out string errorMessage)
        {
            try
            {
                var listOfFound = (from salary in this.Context.PR_tblSalary13
                                   join emp in this.Context.HR_tblEmp.Where(p => p.Active == true) on salary.EmpID equals emp.EmpID into UserEmployee
                                   from emp in UserEmployee.DefaultIfEmpty()

                                   join com in this.Context.LS_tblCompany on emp.LSCompanyID equals com.LSCompanyID into EmployeeCompany
                                   from com in EmployeeCompany.DefaultIfEmpty()

                                   join pos in this.Context.LS_tblPosition on emp.LSPositionID equals pos.LSPositionID into EmployeePosition
                                   from pos in EmployeePosition.DefaultIfEmpty()
                                   where (salary.Year == model.Year) &&
                                   (salary.CalMonth == model.CalMonth || !model.CalMonth.HasValue) &&
                                   (emp.EmpCode.ToUpper().Contains(model.EmployeeCode.ToUpper()) || String.IsNullOrEmpty(model.EmployeeCode)) &&
                                   ((emp.FirstName + " " + emp.LastName).ToUpper().Contains(model.EmloyeepName.ToUpper()) || String.IsNullOrEmpty(model.EmloyeepName)) &&
                                   (emp.LSCompanyID == model.LSCompanyIDTree || model.LSCompanyIDTree == 0) &&
                                   (emp.LSPositionID == model.LSPositionID || model.LSPositionID == 0) &&
                                   (salary.CalMonth == model.CalMonth || !model.CalMonth.HasValue) &&
                                   (salary.Coef == model.Coef || !model.Coef.HasValue)
                                                                       
                                   select new Salary13ViewModel
                                   {
                                       Salary13ID = salary.Salary13ID,
                                       Year = salary.Year,
                                       EmpID = salary.EmpID,                                       
                                       LSCompanyID = com.LSCompanyID,
                                       LSPositionID = pos.LSPositionID,
                                       EmloyeepName = emp.FirstName + " " + emp.LastName,
                                       CompanyName = com.Name,
                                       PositionName = pos.Name,
                                       Checked = true,
                                       Coef = salary.Coef,
                                       BasicSalary = salary.BasicSalary,
                                       CalMonth = salary.CalMonth,
                                       Salary13 = salary.Salary13
                                       
                                   }).ToList();
                                    
                errorMessage = String.Empty;
                return listOfFound;
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
        public List<Salary13ViewModel> CreateListOfSalary13(Salary13ViewModel model, out string errorMessage)
        {
            try
            {                
                var listOfFound = (from salary13 in this.Context.PR_tblSalary13Coef.Where(p => p.Year == model.Year)
                                   join payroll in this.Context.PR_tblSalary on salary13.EmpID equals payroll.EmpID into Salary
                                   from payroll in Salary.DefaultIfEmpty()

                                   join emp in this.Context.HR_tblEmp.Where(p => p.Active == true) on salary13.EmpID equals emp.EmpID into Salary13
                                   from emp in Salary13.DefaultIfEmpty()                                   

                                   join com in this.Context.LS_tblCompany on emp.LSCompanyID equals com.LSCompanyID into EmployeeCompany
                                   from com in EmployeeCompany.DefaultIfEmpty()

                                   join pos in this.Context.LS_tblPosition on emp.LSPositionID equals pos.LSPositionID into EmployeePosition
                                   from pos in EmployeePosition.DefaultIfEmpty()
                                   where 
                                   (emp.EmpCode.ToUpper().Contains(model.EmployeeCode.ToUpper()) || String.IsNullOrEmpty(model.EmployeeCode)) &&
                                   ((emp.FirstName + " " + emp.LastName).ToUpper().Contains(model.EmloyeepName.ToUpper()) || String.IsNullOrEmpty(model.EmloyeepName)) &&
                                   (emp.LSCompanyID == model.LSCompanyIDTree || model.LSCompanyIDTree == 0) &&
                                   (emp.LSPositionID == model.LSPositionID || model.LSPositionID == 0) &&
                                   (this.Context.PR_tblSalary13.Where(p => p.Year == model.Year && p.EmpID == salary13.EmpID).FirstOrDefault() == null) &&
                                   (salary13.CalMonth == model.CalMonth || !model.CalMonth.HasValue) &&
                                   (salary13.Coef == model.Coef || !model.Coef.HasValue)
                                   select new Salary13ViewModel
                                   {
                                       LSCompanyID = com.LSCompanyID,
                                       LSPositionID = pos.LSPositionID,
                                       EmpID = salary13.EmpID,
                                       EmloyeepName = emp.LastName  + " " + emp.FirstName,
                                       EmployeeCode = emp.EmpCode,
                                       CompanyName = com.Name,
                                       PositionName = pos.Name,
                                       Year = salary13.Year,
                                       CalMonth = salary13.CalMonth, 
                                       Coef = salary13.Coef,                                       
                                       Checked = true,
                                       BasicSalary = payroll.ActualSalary                                       
                                   }).ToList();
                
                errorMessage = String.Empty;
                return listOfFound;
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
        /// <param name="listOfSalary13Coef"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool SaveListOfSalary13(List<Salary13ViewModel> listOfSalary13, out string errorMessage)
        {
            try
            {
                var year = DateTime.Now.Year;
                foreach (var item in listOfSalary13)
                {                    
                    var checking = this.Context.PR_tblSalary13.Where(p => p.Year == item.Year && p.EmpID == item.EmpID).FirstOrDefault();
                    if (checking == null)
                    {
                        var itemForAdding = new PR_tblSalary13();
                        AutoMapper.Mapper.CreateMap<Salary13ViewModel, PR_tblSalary13>();
                        itemForAdding = AutoMapper.Mapper.Map(item, itemForAdding);                        
                        this.Context.Entry(itemForAdding).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        checking.CalMonth = item.CalMonth;
                        checking.Coef = item.Coef;
                        this.Context.Entry(checking).State = System.Data.Entity.EntityState.Modified;
                    }
                    year = item.Year;
                }
                var listOfDeleted = this.Context.PR_tblSalary13.Where(p => p.Year == year).ToList();
                foreach (var itemFroDelete in listOfDeleted)
                {
                    var checking = listOfSalary13.Where(p => p.EmpID == itemFroDelete.EmpID).FirstOrDefault();
                    if (checking == null)
                    {
                        this.Context.Entry(itemFroDelete).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
                this.Context.SaveChanges();

                errorMessage = String.Empty;
                return true;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return false;
            }
        }        
    }
}
