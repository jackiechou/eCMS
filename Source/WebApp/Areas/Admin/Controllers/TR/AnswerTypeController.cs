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
    public class AnswerTypeController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../TR/AnswerType/_Reset");
            }
            else
            {
                return View("../TR/AnswerType/Index");
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
            var repository = new AnswerTypeRepository(this.db);
            string errorMesage;
            var result = repository.GetListOfAnswerType(out errorMesage);
            if (result == null)
            {
                return this.Content("Error");
            }
            return this.PartialView("../TR/AnswerType/_List", result);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _Create()
        {
            var model = new AnswerTypeViewModel();
            model.Used = true;
            return PartialView("../TR/AnswerType/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _Edit(int id)
        {
            string errorMessage;
            var repository = new AnswerTypeRepository(db);
            var model = repository.FindForEdit(id, out errorMessage);
            if (model == null)
            {
                return this._Error(model, errorMessage);
            }
            return PartialView("../TR/AnswerType/_Create", model);
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(AnswerTypeViewModel model)
        {
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }

            string errorMessage;
            if (this.ModelState.IsValid)
            {
                var repository = new AnswerTypeRepository(this.db);
                AutoMapper.Mapper.CreateMap<AnswerTypeViewModel, LS_tblAnswerType>();
                var modelForAdd = AutoMapper.Mapper.Map<LS_tblAnswerType>(model);
                bool result = repository.Save(modelForAdd, out errorMessage);
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
        public ActionResult Update(AnswerTypeViewModel model)
        {
            AccountViewModel account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }

            string errorMessage;
            if (this.ModelState.IsValid)
            {                
                AutoMapper.Mapper.CreateMap<AnswerTypeViewModel, LS_tblAnswerType>();
                var modelForUpdate = AutoMapper.Mapper.Map<LS_tblAnswerType>(model);
                var repository = new AnswerTypeRepository(this.db);
                bool result = repository.Update(modelForUpdate, out errorMessage);
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
        public ActionResult Delete(int id)
        {
            string errorMessage;
            var repository = new AnswerTypeRepository(this.db);
            bool result = repository.Delete(id, out errorMessage);
            if (result)
            {
                return this.Content("success");
            }
            return this._Error(new AnswerTypeViewModel(), errorMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _Error(AnswerTypeViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new AnswerTypeViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            return PartialView("../TR/AnswerType/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
        {
            var repository = new AnswerTypeRepository(db);
            List<AutoCompleteViewModel> sources = repository.ListDropdown(searchTerm, pageSize, pageNum).ToList();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selected_value"></param>
        public SelectList PopulateAnswerTypeToDropDownList(int? selected_value)
        {
            string errorMessage = String.Empty;
            var respository = new AnswerTypeRepository(db);
            var listOfAnswerType = respository.GetListOfAnswerType(out errorMessage);
            if (listOfAnswerType != null)
            {
                listOfAnswerType.Insert(0, new AnswerTypeViewModel() { LSAnswerTypeID = 0, Name = Eagle.Resource.LanguageResource.PleaseInputAnswerType });
                return new SelectList(listOfAnswerType, "LSAnswerTypeID", "Name", selected_value.HasValue ? selected_value.Value : 0);            
            }
            return null;
        }
    }
               
}