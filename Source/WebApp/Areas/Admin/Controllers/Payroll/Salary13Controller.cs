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
    public class Salary13Controller : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/Salary13/_Reset");
            }
            else
            {
                return View("../Payroll/Salary13/Index");
            }
        }               

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new Salary13ViewModel();
            this.CreateViewBag(model);
            return PartialView("../Payroll/Salary13/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult List(Salary13ViewModel model)
        {
            var errorMessage = String.Empty;            
            var repository = new Salary13Repository(this.db);
            var listOfSalary13 = repository.GetListOfSalary13(model, out errorMessage);
            if (listOfSalary13 == null)
            {
                return this.Error(model, errorMessage);
            }
            this.CreateViewBag(model);
            return this.PartialView("../Payroll/Salary13/_List", listOfSalary13);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateSalary13(Salary13ViewModel model)
        {
            var errorMessage = String.Empty;
            var repository = new Salary13Repository(this.db);
            var listOfSalary13 = repository.CreateListOfSalary13(model, out errorMessage);
            if (listOfSalary13 == null)
            {
                return this.Error(model, errorMessage);
            }
            this.CreateViewBag(model);
            return this.PartialView("../Payroll/Salary13/_List", listOfSalary13);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(List<Salary13ViewModel> listOfSalary13)
        {            
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }
            var model = new Salary13ViewModel();
            string errorMessage = String.Empty;
            var listOfSalaryChecked = listOfSalary13.Where(p => p.Checked).ToList();
            if (listOfSalaryChecked.Count == 0)
            {
                errorMessage += Eagle.Resource.LanguageResource.PleaseEnterSalary13Information;
            }
            
            if (this.ModelState.IsValid && String.IsNullOrEmpty(errorMessage))
            {
                
                var repository = new Salary13Repository(this.db);
                bool result = repository.SaveListOfSalary13(listOfSalaryChecked, out errorMessage);
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
        public ActionResult Error(Salary13ViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new Salary13ViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            this.CreateViewBag(model);
            return PartialView("../Payroll/Salary13/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag(Salary13ViewModel model)
        {            
            // Loading DropDownList Of CalMonths            
            var listOfCalMonths = model.ListOfCalMonths(this.LanguageId);
            this.ViewBag.CalMonth = new SelectList(listOfCalMonths, "Key", "Text", model.CalMonth);

            // Loading DropDownList Of Years
            var years = new List<int>();
            for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year + 5; i++)
            {
                years.Add(i);
            }
            this.ViewBag.Year = new SelectList(years, model.Year);
            return; 
        }
    }
}