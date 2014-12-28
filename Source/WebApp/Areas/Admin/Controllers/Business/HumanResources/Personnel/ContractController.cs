using Eagle.Common.Entities;
using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository;
using Eagle.Repository.HR;
using Eagle.Repository.LS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Eagle.Model;
using Eagle.Model.Common;
using Eagle.Model.HR;
using Eagle.Model.LS;
using Eagle.Model.SYS;
using Eagle.Repository.SYS.Session;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class ContractController : BaseController
    {
        //
        // GET: /Admin/Contract/
        [SessionExpiration]
        public ActionResult Index()
        {                       
            if (Request.IsAjaxRequest())
                return PartialView("../HR/Contract/_Reset");
            else
                return View("../HR/Contract/Index");
        }

        [SessionExpiration]
        public ActionResult List()
        {
            List<ContractViewModel> sources = new List<ContractViewModel>();
           
            int ModuleId = 2;
            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);      
            ContractRespository _repository = new ContractRespository(db);
            sources = _repository.GetList(CurrentEmpId, ModuleId, UserName, isAdmin, LanguageId);             

            return PartialView("../HR/Contract/_List", sources);
        }

        [AjaxSessionActionFilter]
        public ActionResult GetList(int? EmpID, int? ModuleID)
        {
            List<ContractViewModel> sources = new List<ContractViewModel>();           
            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            if (EmpID != null && ModuleID != null)
            {
                ContractRespository _repository = new ContractRespository(db);
                sources = _repository.GetList(EmpID, ModuleID, UserName, isAdmin, LanguageId);
            }else
                Response.Redirect(Url.Action("Login", "User", new { desiredUrl = Request.Url.AbsoluteUri })); 
            return PartialView("../HR/Contract/_List", sources);
        }      

        [HttpGet]
        public ActionResult PopulateDownloadPage(string ItemId, string ItemTag, string FileIds)
        {
            FileModel download_page_model = new FileModel();
            download_page_model.ItemId = ItemId;
            download_page_model.ItemTag = ItemTag;
            download_page_model.FileIds = FileIds;
            return PartialView("../FileManager/_DownloadPage", download_page_model);
        }

        // GET: /Admin/Contract/Details/5        
        [HttpGet]
        [SessionExpiration]
        public ActionResult Edit(int id)
        {
            ContractRespository _repository = new ContractRespository(db);
            ContractViewModel model = _repository.Details(id, LanguageId);
            Session[SettingKeys.EmpInfo] = EmployeeRepository.GetBriefDetails(model.EmpID, LanguageId);
            PopulateMethodPITsToDropDownList(model.MethodPIT);

            string ProbationSalary = string.Empty, InsuranceSalary = string.Empty;
            double ProbationSalaryEdit = Convert.ToDouble(model.ProbationSalary);
            double InsuranceSalaryEdit = Convert.ToDouble(model.InsuranceSalary);
            model.ProbationSalaryEdit = string.Format("{0:#.###}",ProbationSalaryEdit);
            model.InsuranceSalaryEdit = string.Format("{0:#.###}", InsuranceSalaryEdit);      
            return PartialView("../HR/Contract/_Edit", model);
        }

        // GET: /Admin/Contract/Create
        [SessionExpiration]
        public ActionResult Create()
        {
            ContractViewModel model = new ContractViewModel();
            model.ContractStatus = 1;
            ContractRespository _repository = new ContractRespository(db);
            string ContractNo = _repository.GenerateContractNo(10);
            model.ContractNo = ContractNo;

            PopulateMethodPITsToDropDownList(null);
            return PartialView("../HR/Contract/_Create", model);
        }



        #region MethodPIT -------------------------------------------------------------------------------------------------------------
        public void PopulateMethodPITsToDropDownList(int? selected_value)
        {
            MethodPITModelList lst = new MethodPITModelList(LanguageId);
            ViewBag.MethodPIT = new SelectList(lst.MethodPITList, "MethodPITId", "MethodPITName", selected_value);
        }
        #endregion -----------------------------------------------------------------------------------------------------------------

        // POST: /Admin/Contract/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(ContractViewModel add_model, bool chkContractStatus)
        {
            bool flag = false;
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    DateTime? startDate = add_model.EffectiveDate;
                    DateTime? endDate = add_model.ExpiredDate;
                    if (startDate.HasValue && endDate.HasValue)
                    {
                        if (DateTime.Compare(startDate.Value, endDate.Value) > 0)
                        {
                            flag = false;
                            message = Eagle.Resource.LanguageResource.ValidateEffectiveDateExpiredDate;
                            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                        }
                    }
                    ContractRespository _repository = new ContractRespository(db);
                    add_model.Creater = CurrentEmpId;
                    add_model.ContractStatus = chkContractStatus == true ? 1 : 0;
                    flag = _repository.Add(add_model, out message);
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var modelError in errors)
                    {
                        message += modelError.ErrorMessage + "-";
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                flag = false;
            }
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        //POST - UpdateFileIds
        //[HttpPost]
        public ActionResult UpdateFileIds(int Id, string Added_FileIds)
        {
            bool flag = false;
            string message = string.Empty;
            flag = ContractRespository.UpdateFileIds(Id, Added_FileIds, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        //
        // POST: 

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(ContractViewModel edit_model, bool chkContractStatus)
        {
            bool flag = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                DateTime? startDate = edit_model.EffectiveDate;
                DateTime? endDate = edit_model.ExpiredDate;
                if (startDate.HasValue && endDate.HasValue)
                {
                    if (DateTime.Compare(startDate.Value, endDate.Value) > 0)
                    {
                        flag = false;
                        message = Eagle.Resource.LanguageResource.ValidateEffectiveDateExpiredDate ;
                        return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                    }
                }

                ContractRespository _repository = new ContractRespository(db);
                edit_model.Creater = CurrentEmpId;
                edit_model.ContractStatus = chkContractStatus == true ? 1 : 0;
                flag = _repository.Edit(edit_model, out message);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var modelError in errors)
                {
                    message += modelError.ErrorMessage + "-";
                }
            }
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }


        //
        // POST: /Admin/Contract/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            bool flag = false;
            ContractRespository _repository = new ContractRespository(db);
            flag = _repository.Delete(id, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ValidateCode(string ContractNo)
        {
            ContractRespository _repository = new ContractRespository(db);
            bool flag = _repository.CheckExistCode(ContractNo);
            bool result = (flag == true) ? false : true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region BRIEF - FORM --------------------------------------------------------------------
        [AjaxSessionActionFilter]
        [HttpGet]
        public ActionResult GetListByEmpID(int? EmpID)
        {
            List<ContractViewModel> sources = new List<ContractViewModel>();
            if (EmpID != null)
            {
                ContractRespository _repository = new ContractRespository(db);
                sources = _repository.GetListByEmpID(EmpID, LanguageId);
            }
            return PartialView("../HR/Contract/_BriefList", sources);
        }

        [SessionExpiration]
        [HttpGet]
        public ActionResult CreateBriefForm(int EmpID)
        {
            ContractEditModel model = new ContractEditModel();
            ContractRespository _repository = new ContractRespository(db);
            string ContractNo = _repository.GenerateContractNo(10);
            model.ContractNo = ContractNo;
            PopulateMethodPITsToDropDownList(null);
            return PartialView("../HR/Contract/_CreateBriefForm", model);
        }


        [SessionExpiration]
        [HttpGet]
        public ActionResult EditBriefForm(int id)
        {
            ContractRespository _repository = new ContractRespository(db);
            ContractEditModel model = _repository.GetDetails(id, LanguageId);
            Session[SettingKeys.EmpInfo] = EmployeeRepository.GetBriefDetails(model.EmpID, LanguageId);
            PopulateMethodPITsToDropDownList(model.MethodPIT);

            string ProbationSalary = string.Empty, InsuranceSalary = string.Empty;
            double ProbationSalaryEdit = Convert.ToDouble(model.ProbationSalary);
            double InsuranceSalaryEdit = Convert.ToDouble(model.InsuranceSalary);
            model.ProbationSalaryEdit = string.Format("{0:#.###}", ProbationSalaryEdit);
            model.InsuranceSalaryEdit = string.Format("{0:#.###}", InsuranceSalaryEdit);
            return PartialView("../HR/Contract/_EditBriefForm", model);
        }

        //
        // POST: /Admin/Contract/Create       
        [HttpPost]
        public ActionResult Insert(ContractEditModel add_model)
        {
            bool flag = false;
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    DateTime? startDate = add_model.EffectiveDate;
                    DateTime? endDate = add_model.ExpiredDate;
                    if (startDate.HasValue && endDate.HasValue)
                    {
                        if (DateTime.Compare(startDate.Value, endDate.Value) > 0)
                        {
                            flag = false;
                            message = Eagle.Resource.LanguageResource.ValidateEffectiveDateExpiredDate;
                            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                        }
                    }
                    ContractRespository _repository = new ContractRespository(db);
                    add_model.Creater = CurrentEmpId;
                    flag = _repository.Insert(add_model, out message);
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var modelError in errors)
                    {
                        message += modelError.ErrorMessage + "-";
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                flag = false;
            }
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Admin/Contract/Edit/5
        [HttpPost]
        public ActionResult Update(ContractEditModel edit_model)
        {
            bool flag = false;
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    DateTime? startDate = edit_model.EffectiveDate;
                    DateTime? endDate = edit_model.ExpiredDate;
                    if (startDate.HasValue && endDate.HasValue)
                    {
                        if (DateTime.Compare(startDate.Value, endDate.Value) > 0)
                        {
                            flag = false;
                            message = Eagle.Resource.LanguageResource.ValidateEffectiveDateExpiredDate;
                            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                        }
                    }

                    ContractRespository _repository = new ContractRespository(db);
                    flag = _repository.Update(edit_model, out message);
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var modelError in errors)
                    {
                        message += modelError.ErrorMessage + "-";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                flag = false;
            }
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }
        #endregion ------------------------------------------------------------------------------

        
    }
}
