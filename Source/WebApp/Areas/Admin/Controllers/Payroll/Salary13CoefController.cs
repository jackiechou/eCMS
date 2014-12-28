using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Eagle.Core;
using Eagle.Repository;
using Eagle.Model;
using Eagle.Common.Helpers;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class Salary13CoefController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/Salary13Coef/_Reset");
            }
            else
            {
                return View("../Payroll/Salary13Coef/Index");
            }
        }               

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new Salary13CoefViewModel();
            this.CreateViewBag(model);
            return PartialView("../Payroll/Salary13Coef/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult List(Salary13CoefViewModel model)
        {
            var errorMessage = String.Empty;            
            var repository = new Salary13CoefRepository(this.db);
            if (model.IsSetting == 1)
            {
                var listOfSalary13Coef = repository.GetListOfSalary13Coef(model, out errorMessage);
                if (listOfSalary13Coef == null)
                {
                    return this.Error(model, errorMessage);
                }
                this.CreateViewBag(model);
                return this.PartialView("../Payroll/Salary13Coef/_List", listOfSalary13Coef);
            }
            var listOfSalary13CoefForAdding = repository.CreateListOfSalary13Coef(model, out errorMessage);
            if (listOfSalary13CoefForAdding == null)
            {
                return this.Error(model, errorMessage);
            }
            this.CreateViewBag(model);
            return this.PartialView("../Payroll/Salary13Coef/_List", listOfSalary13CoefForAdding); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(List<Salary13CoefViewModel> listOfSalary13Coef)
        {            
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }

            var model = new Salary13CoefViewModel();
            string errorMessage = String.Empty;
            var listOfSalaryChecked = listOfSalary13Coef.Where(p => p.Checked).ToList();
            if (listOfSalaryChecked.Count == 0)
            {
                errorMessage += Eagle.Resource.LanguageResource.PleaseEnterSalary13CoefInformation;
            }
            if (this.ModelState.IsValid && String.IsNullOrEmpty(errorMessage))
            {
                var repository = new Salary13CoefRepository(this.db);
                bool result = repository.SaveListOfSalary13Coef(listOfSalaryChecked, out errorMessage);
                if (result)
                {
                    return this.Content("success");
                }
                else
                {
                    return this.Error(model, errorMessage);
                }
            }

            var errors = this.ModelState.Values.SelectMany(e => e.Errors);            
            foreach (var errorModel in errors)
            {
                errorMessage += String.Format("{0}{1}", "&lt;br /&gt;", errorModel.ErrorMessage);
            }
            return this.Error(model, errorMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult Error(Salary13CoefViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new Salary13CoefViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            this.CreateViewBag(model);
            return PartialView("../Payroll/Salary13Coef/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag(Salary13CoefViewModel model)
        {            
            // Loading DropDownList Of CalMonths            
            var listOfCalMonths = model.ListOfCalMonths(this.LanguageId);
            this.ViewBag.CalMonth = new SelectList(listOfCalMonths, "Key", "Text", model.CalMonth);

            // Loading DropDownList Of Years
            var years = new List<int>();
            for (int i = DateTime.Now.Year - 10; i <= DateTime.Now.Year + 10; i++)
            {
                years.Add(i);
            }
            this.ViewBag.Year = new SelectList(years, model.Year);
            return; 
        }
    }
}