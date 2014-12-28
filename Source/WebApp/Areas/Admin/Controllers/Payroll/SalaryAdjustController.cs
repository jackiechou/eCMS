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
    public class SalaryAdjustController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/SalaryAdjust/_Reset");
            }
            else
            {
                return View("../Payroll/SalaryAdjust/Index");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _List()
        {
            AccountViewModel accout = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (accout == null)
            {
                return this.Content("Timeout");
            }
            SalaryAjustRepository repository = new SalaryAjustRepository(this.db);
            string errorMesage;
            var result = repository.FindAll(out errorMesage);
            if (result == null)
            {
                return this.Content("Error");
            }
            return this.PartialView("../Payroll/SalaryAdjust/_List", result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _Create()
        {
            SalaryAdjustModelView model = new SalaryAdjustModelView();
            model.PIT = true;
            model.AddSalary = false;
            this.CreateViewBag();
            return PartialView("../Payroll/SalaryAdjust/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _Edit(int id)
        {
            string errorMessage;
            SalaryAjustRepository _repository = new SalaryAjustRepository(db);
            SalaryAdjustModelView model = _repository.FindForEdit(id, out errorMessage);
            if (model == null)
            {
                return this._Error(model, errorMessage);
            }
            this.CreateViewBag(model.TypeAdd, model.Type);
            return PartialView("../Payroll/SalaryAdjust/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(LS_tblSalaryAdjust model)
        {
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }

            string errorMessage;
            if (this.ModelState.IsValid)
            {
                SalaryAjustRepository repository = new SalaryAjustRepository(this.db);
                bool result = repository.Add(model, out errorMessage);
                if (result)
                {
                    return this.Content("success");
                }
                else
                {
                    return this._Error(model, errorMessage);
                }
            }

            var errors = this.ModelState.Values.SelectMany(e => e.Errors);
            errorMessage = String.Empty;
            foreach (var errorModel in errors)
            {
                errorMessage += String.Format("{0}{1}", "&lt;br /&gt;", errorModel.ErrorMessage);
            }
            return this._Error(model, errorMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(LS_tblSalaryAdjust model)
        {
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }

            string errorMessage;
            if (this.ModelState.IsValid)
            {
                SalaryAjustRepository repository = new SalaryAjustRepository(this.db);
                bool result = repository.Update(model, out errorMessage);
                if (result)
                {
                    return this.Content("success");
                }
                else
                {
                    return this._Error(model, errorMessage);
                }
            }

            var errors = this.ModelState.Values.SelectMany(e => e.Errors);
            errorMessage = String.Empty;
            foreach (var errorModel in errors)
            {
                errorMessage += String.Format("{0}{1}", "&lt;br /&gt;", errorModel.ErrorMessage);
            }
            return this._Error(model, errorMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            string errorMessage;
            SalaryAjustRepository repository = new SalaryAjustRepository(this.db);
            bool result = repository.Delete(id, out errorMessage);
            if (result)
            {
                return this.Content("success");
            }
            return this._Error(new LS_tblSalaryAdjust(), errorMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _Error(LS_tblSalaryAdjust model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblSalaryAdjust();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            this.CreateViewBag();
            return PartialView("../Payroll/SalaryAdjust/_Create", model);
        }
        private void CreateViewBag(int? defaultTypeAdd = 1, int? defaultType = 1)
        {
            SalaryAdjustModelView modelView = new SalaryAdjustModelView();
            if (defaultTypeAdd.HasValue)
            {
                this.ViewBag.TypeAdd = new SelectList(modelView.GetListOfTypeAdd(), "Key", "Value", defaultTypeAdd);
            }            
            if (defaultType.HasValue)
            {
                this.ViewBag.Type = new SelectList(modelView.GetListOfType(), "Key", "Value", defaultType);
            }            
            return;
        }
        /// <summary>
        /// Dùng cho viec binding du lieu cho dropdownlist autocomplete
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
        {
            SalaryAjustRepository _repository = new SalaryAjustRepository(db);
            List<AutoCompleteViewModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum, LanguageId).ToList();
            int iTotal = sources.Count;

            //Translate the list into a format the select2 dropdown expects
            Select2PagedResultViewModel pagedList = ConvertListToSelect2Format(sources, iTotal);

            //Return the data as a jsonp result
            return new JsonpResult
            {
                Data = pagedList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}