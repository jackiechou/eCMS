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
    public class Salary13CoefRepository
    {
        public EntityDataContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        public Salary13CoefRepository(EntityDataContext Context)
        {
            this.Context = Context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Save(Salary13CoefViewModel model, out string errorMessage)
        {
            try
            {
                AutoMapper.Mapper.CreateMap<Salary13CoefViewModel, PR_tblSalary13Coef>();
                var obj = AutoMapper.Mapper.Map<PR_tblSalary13Coef>(model);
                this.Context.Entry(model).State = System.Data.Entity.EntityState.Added;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Update(Salary13CoefViewModel model, out string errorMessage)
        {
            try
            {
                AutoMapper.Mapper.CreateMap<Salary13CoefViewModel, PR_tblSalary13Coef>();
                var obj = AutoMapper.Mapper.Map<PR_tblSalary13Coef>(model);
                this.Context.Entry(model).State = System.Data.Entity.EntityState.Modified;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Delete(int id, out string errorMessage)
        {
            try
            {
                var obj = this.Context.PR_tblSalary13Coef.Where(p => p.SalaryCoefID == id).FirstOrDefault();
                if (obj == null)
                {
                    errorMessage = Eagle.Resource.LanguageResource.ObjectNotExistDataForUpdatDelete;
                    return false;
                }
                this.Context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<Salary13CoefViewModel> GetListOfSalary13Coef(Salary13CoefViewModel model, out string errorMessage)
        {
            try
            {
                var listOfFound = (from coef in this.Context.PR_tblSalary13Coef
                                   join emp in this.Context.HR_tblEmp.Where(p => p.Active == true) on coef.EmpID equals emp.EmpID into UserEmployee
                                   from emp in UserEmployee.DefaultIfEmpty()

                                   join com in this.Context.LS_tblCompany on emp.LSCompanyID equals com.LSCompanyID into EmployeeCompany
                                   from com in EmployeeCompany.DefaultIfEmpty()

                                   join pos in this.Context.LS_tblPosition on emp.LSPositionID equals pos.LSPositionID into EmployeePosition
                                   from pos in EmployeePosition.DefaultIfEmpty()
                                   where (coef.Year == model.Year) &&
                                   (coef.CalMonth == model.CalMonth || !model.CalMonth.HasValue) &&
                                   (emp.EmpCode.ToUpper().Contains(model.EmployeeCode.ToUpper()) || String.IsNullOrEmpty(model.EmployeeCode)) &&
                                   ((emp.FirstName + " " + emp.LastName).ToUpper().Contains(model.EmloyeepName.ToUpper()) || String.IsNullOrEmpty(model.EmloyeepName)) &&
                                   (emp.LSCompanyID == model.LSCompanyIDTree || model.LSCompanyIDTree == 0) &&
                                   (emp.LSPositionID == model.LSPositionID || model.LSPositionID == 0)
                                                                       
                                   select new Salary13CoefViewModel
                                   {
                                       SalaryCoefID = coef.SalaryCoefID,
                                       Year = coef.Year,
                                       EmpID = coef.EmpID,
                                       Coef = coef.Coef,
                                       LSCompanyID = com.LSCompanyID,
                                       LSPositionID = pos.LSPositionID,
                                       EmloyeepName = emp.LastName + " " + emp.FirstName,
                                       CompanyName = com.Name,
                                       PositionName = pos.Name
                                       
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
        /// <param name="Year"></param>
        /// <returns></returns>
        private DateTime? GetCutOffSalary(int Year, out string errorMessage)
        {
            try
            {
                errorMessage = String.Empty;
                var found = this.Context.SYS_tblParameter.GroupBy(p => p.CutOffSalary).FirstOrDefault();
                return DateTime.ParseExact(String.Format("{0}/{1}", found.Key, Year), "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
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
        public List<Salary13CoefViewModel> CreateListOfSalary13Coef(Salary13CoefViewModel model, out string errorMessage)
        {
            try
            {
                var CuttOffSalaryDateTime = this.GetCutOffSalary(model.Year, out errorMessage);

                var listOfFound = (from emp in this.Context.HR_tblEmp.Where(p => p.Active == true)                                    
                                   join com in this.Context.LS_tblCompany on emp.LSCompanyID equals com.LSCompanyID into EmployeeCompany
                                   from com in EmployeeCompany.DefaultIfEmpty()

                                   join pos in this.Context.LS_tblPosition on emp.LSPositionID equals pos.LSPositionID into EmployeePosition
                                   from pos in EmployeePosition.DefaultIfEmpty()
                                   where
                                   (emp.EmpCode.ToUpper().Contains(model.EmployeeCode.ToUpper()) || String.IsNullOrEmpty(model.EmployeeCode)) &&
                                   ((emp.FirstName + " " + emp.LastName).ToUpper().Contains(model.EmloyeepName.ToUpper()) || String.IsNullOrEmpty(model.EmloyeepName)) &&
                                   (emp.LSCompanyID == model.LSCompanyIDTree || model.LSCompanyIDTree == 0) &&
                                   (emp.LSPositionID == model.LSPositionID || model.LSPositionID == 0) &&
                                   (this.Context.PR_tblSalary13Coef.Where(p => p.Year == model.Year && p.EmpID == emp.EmpID).FirstOrDefault() == null)
                                   select new Salary13CoefViewModel
                                   {
                                       LSCompanyID = emp.LSCompanyID,
                                       LSPositionID = emp.LSPositionID.HasValue ? emp.LSPositionID.Value : 0,
                                       EmpID = emp.EmpID,
                                       EmloyeepName = emp.FirstName + " " + emp.LastName,
                                       EmployeeCode = emp.EmpCode,
                                       CompanyName = com.Name,
                                       PositionName = pos.Name, 
                                       StartDate = emp.StartDate,
                                       Year = model.Year,
                                       CalMonth = model.CalMonth, 
                                       Checked = true
                                       
                                   }).ToList();

                foreach (var item in listOfFound)
                {
                    item.Coef = this.GetSalaryCoefOfEmployee(CuttOffSalaryDateTime.Value, item.StartDate, model.Year);
                }
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
        /// <param name="EmpID"></param>
        /// <returns></returns>
        private Decimal GetSalaryCoefOfEmployee(DateTime CuttOffSalary, DateTime? StartDate, int Year)
        {
            if (!StartDate.HasValue)
            {
                return 0;
            }
            var timespan = CuttOffSalary.Subtract(StartDate.Value); 
            var numberOfYears = Math.Abs(CuttOffSalary.Year - StartDate.Value.Year);
            if (numberOfYears == 0)
            {
                var numberOfMonths = CuttOffSalary.Month - StartDate.Value.Month + 1;
                var result = (numberOfMonths * 100) / 12;
                return Math.Round((decimal)result, 2);                 
            }
            return 100;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listOfSalary13Coef"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool SaveListOfSalary13Coef(List<Salary13CoefViewModel> listOfSalary13Coef, out string errorMessage)
        {
            try
            {
                var year = DateTime.Now.Year;
                foreach (var item in listOfSalary13Coef)
                {                    
                    var checking = this.Context.PR_tblSalary13Coef.Where(p => p.Year == item.Year && p.EmpID == item.EmpID).FirstOrDefault();
                    if (checking == null)
                    {
                        PR_tblSalary13Coef itemForAdding = new PR_tblSalary13Coef();
                        AutoMapper.Mapper.CreateMap<Salary13CoefViewModel, PR_tblSalary13Coef>();
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
                var listOfDeleted = this.Context.PR_tblSalary13Coef.Where(p => p.Year == year).ToList();
                foreach (var itemFroDelete in listOfDeleted)
                {
                    var checking = listOfSalary13Coef.Where(p => p.EmpID == itemFroDelete.EmpID).FirstOrDefault();
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
