using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Eagle.Core;
using Eagle.Repository;
using Eagle.Model;

using System.Web.Routing;
using Eagle.Common.Helpers;
using Eagle.Repository.SYS.Session;
using Eagle.Model.LS;
using Eagle.Common.Utilities;
using Eagle.Model.SYS;
using Eagle.Common.Settings;
using Eagle.Model.SYS.Permission;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class LS_tblBankController : BaseController
    {
        private int MasterDataID = 18;
        //
        // GET: /Admin/LS_tblBank/
        [SessionExpiration]
        //[Layout(LayoutType.FullMainLayout)]
        public ActionResult Index()
        {
            FunctionPermissionRepository _respository = new FunctionPermissionRepository(db);
            PermissionSettingViewModel permission_entity = _respository.CheckMasterDataPermission(UserId, MasterDataID);
            if (permission_entity.View == true)       
                return View("../LS/Bank/Index");
            else
                return PartialView("../Common/Admin/PermissionsError");
        }

         [SessionExpiration]
         public ActionResult List()
         {  
            List<LS_tblBankViewModel> sources = new List<LS_tblBankViewModel>();
            LS_tblBankRepository _repository = new LS_tblBankRepository(db);
            sources = _repository.GetList(LanguageId);
            return PartialView("../LS/Bank/_List", sources);
         }


        [SessionExpiration]
        public ActionResult Create()
        {
            FunctionPermissionRepository _respository = new FunctionPermissionRepository(db);
            PermissionSettingViewModel permission_entity = _respository.CheckMasterDataPermission(UserId, MasterDataID);
            if (permission_entity.Edit == true) 
                return PartialView("../LS/Bank/_Create");
            else
                return PartialView("../Common/Admin/PermissionsError");
        }

        [SessionExpiration]
        public ActionResult Edit(int? id)
        { 
            FunctionPermissionRepository _respository = new FunctionPermissionRepository(db);
            PermissionSettingViewModel permission_entity = _respository.CheckMasterDataPermission(UserId, MasterDataID);
            if (permission_entity.Edit == true) 
            {
                LS_tblBankRepository _repository = new LS_tblBankRepository(db);
                LS_tblBankCreateModel entity = _repository.Details(id, LanguageId);
                return PartialView("../LS/Bank/_Create", entity);
            }
            else
                return PartialView("../Common/Admin/PermissionsError");
        }

        [HttpGet]
        [SessionExpiration]
        public JsonResult GenerateCode()
        {
            string Code = LS_tblBankRepository.GenerateCode(10);
            return Json(Code, JsonRequestBehavior.AllowGet);
        }       

        [HttpGet]
        public JsonResult ValidateCode(string Code)
        {
            LS_tblBankRepository _repository = new LS_tblBankRepository(db);
            bool flag = _repository.CheckExistCode(Code);
            bool result = (flag == true) ? false : true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ValidateEditCode(string NewCode, string InitialCode)
        {
            LS_tblBankRepository _repository = new LS_tblBankRepository(db);
            bool result = true;
            if (NewCode != InitialCode)
            {
                bool flag = _repository.CheckExistCode(NewCode);
                if (flag == true)
                    result = false;
            }
            return base.Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ValidateVNName(string VNName)
        {
            LS_tblBankRepository _repository = new LS_tblBankRepository(db);
            bool flag = _repository.IsVNNameExisted(VNName);
            bool result = (flag == true) ? false : true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ValidateName(string Name)
        {
            LS_tblBankRepository _repository = new LS_tblBankRepository(db);
            bool flag = _repository.IsNameExisted(Name);
            bool result = (flag == true) ? false : true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(LS_tblBankViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LS_tblBankRepository _repository = new LS_tblBankRepository(db);
                if (ModelState.IsValid)
                {
                    LS_tblBank add = new LS_tblBank()
                    {
                        LSBankCode = model.LSBankCode,
                        LSProvinceID = model.LSProvinceID,
                        Name = model.Name,
                        VNName = model.VNName,
                        Rank = model.Rank,
                        Used = model.Used,
                        Note = model.Note
                    };
                    bool bResult = _repository.Add(add, out errorMessage);
                    if (bResult)
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _Error(model, errorMessage);
                    }
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }

            return _Error(model, errorMessage);
        }

        [HttpPost]
        [SessionExpiration]
        public ActionResult Insert(LS_tblBankCreateModel model)
        {
            string message = "";
            bool flag = false;   
            if (ModelState.IsValid)
            {
                LS_tblBankRepository _repository = new LS_tblBankRepository(db);
                flag = _repository.Insert(model, out message);
            }         
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                message += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

       


        [HttpPost]
        [SessionExpiration]
        public ActionResult Update(LS_tblBankCreateModel model)
        {            
             string message = "";
             bool flag = false;
             if (ModelState.IsValid)
             {
                 LS_tblBankRepository _repository = new LS_tblBankRepository(db);
                 flag = _repository.Update(model, out message);
             }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                message += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionExpiration]
        public ActionResult UpdateListOrder(int id, int listOrder)
        {
            bool flag = false;
            string message = string.Empty;
            try
            {
                flag = LS_tblBankRepository.UpdateListOrder(id, Convert.ToInt16(listOrder), out message);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionExpiration]
        public ActionResult Edit(LS_tblBankViewModel model)
        {
            LS_tblBankRepository _repository = new LS_tblBankRepository(db);
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                if (_repository.Edit(model, out errorMessage))
                {
                    return Content("success");
                }
                else
                {
                    return RedirectToAction("_CreateLS_tblBankError", "HR_MasterData", new { id = model, ErrorMessage = errorMessage });
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }

            return _Error(model, errorMessage);
        }
        public ActionResult _Error(LS_tblBankViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblBankViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            return PartialView("../HR_MasterData/_LS_tblBankCreate", model);
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
            LS_tblBankRepository _repository = new LS_tblBankRepository(db);
            List<AutoCompleteModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum, LanguageId);
            int iTotal = sources.Count;

            //Translate the list into a format the select2 dropdown expects
            Select2PagedResultViewModel pagedList = ConvertAutoListToSelect2Format(sources, iTotal);

            //Return the data as a jsonp result
            return new JsonpResult
            {
                Data = pagedList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

         // Get Companies
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetBanks()
        {            
            List<LS_tblBankViewModel> lst = new List<LS_tblBankViewModel>();                
            lst = db.LS_tblBank
                .Select(p => new LS_tblBankViewModel()
                {
                    LSBankID = p.LSBankID,
                    LSBankCode = p.LSBankCode,
                    Name = p.Name,
                    VNName = p.VNName
                }).OrderBy(p => p.Name).ToList();

            List<SelectListItem> selectlist = new List<SelectListItem>();
            selectlist = lst.Select(m => new SelectListItem()
            {
                Text = (LanguageId == 124)?m.Name: m.VNName,
                Value = m.LSBankID.ToString(),
            }).ToList();
            return Json(selectlist, JsonRequestBehavior.AllowGet);           
        }
    }
}
