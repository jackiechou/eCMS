using Eagle.Common.Helpers;
using Eagle.Repository.SYS.Session;
using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.LS;
using Eagle.Model.SYS;
using Eagle.Model.SYS.Permission;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class LS_tblBankBranchController : BaseController
    {
        private int MasterDataID = 19;
        //
        // GET: /Admin/LS_tblBankBranch/

        //
        // GET: /Admin/LS_tblBank/
        [SessionExpiration]
        public ActionResult Index(int BankID, string BankName)
        {
            FunctionPermissionRepository _respository = new FunctionPermissionRepository(db);
            PermissionSettingViewModel permission_entity = _respository.CheckMasterDataPermission(UserId, MasterDataID);
            if (permission_entity.View == true)
            {
                LS_tblBankBranchEditModel model = new LS_tblBankBranchEditModel();
                model.BankID = BankID;
                model.BankName = BankName;
                return View("../LS/BankBranch/Index", model);
            }
            else
                return PartialView("../Common/Admin/PermissionsError");
        }
      
        [SessionExpiration]
        public ActionResult List(int? BankID)
        {
            List<LS_tblBankBranchEditModel> sources = new List<LS_tblBankBranchEditModel>();
            LS_tblBankBranchRepository _repository = new LS_tblBankBranchRepository(db);
            sources = _repository.GetList(BankID, LanguageId);
            return PartialView("../LS/BankBranch/_List", sources);
        }

        [SessionExpiration]
        public ActionResult Create(LS_tblBankBranchEditModel model)
        {
            FunctionPermissionRepository _respository = new FunctionPermissionRepository(db);
            PermissionSettingViewModel permission_entity = _respository.CheckMasterDataPermission(UserId, MasterDataID);
            if (permission_entity.Edit == true) 
                return PartialView("../LS/BankBranch/_Create", model);
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
                LS_tblBankBranchRepository _repository = new LS_tblBankBranchRepository(db);
                LS_tblBankBranchEditModel entity = _repository.Details(id, LanguageId);
                return PartialView("../LS/BankBranch/_Create", entity);
            }
            else
                return PartialView("../Common/Admin/PermissionsError");
        }

        [HttpPost]
        [SessionExpiration]
        public ActionResult Insert(LS_tblBankBranchEditModel model)
        {
            string message = "";
            bool flag = false;
            if (ModelState.IsValid)
            {
                LS_tblBankBranchRepository _repository = new LS_tblBankBranchRepository(db);
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
        public ActionResult Update(LS_tblBankBranchEditModel model)
        {            
            string message = "";
            bool flag = false;
            if (ModelState.IsValid)
            {
                LS_tblBankBranchRepository _repository = new LS_tblBankBranchRepository(db);
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
                flag = LS_tblBankBranchRepository.UpdateListOrder(id, Convert.ToInt16(listOrder), out message);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        //==============================================================================
        //REFERENCES ===================================================================
        //==============================================================================
        [HttpGet]
        [SessionExpiration]
        public JsonResult GenerateCode()
        {
            string Code = LS_tblBankBranchRepository.GenerateCode(10);
            return Json(Code, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ValidateCode(string Code)
        {
            LS_tblBankBranchRepository _repository = new LS_tblBankBranchRepository(db);
            bool flag = _repository.CheckExistCode(Code);
            bool result = (flag == true) ? false : true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ValidateEditCode(string NewCode, string InitialCode)
        {
            LS_tblBankBranchRepository _repository = new LS_tblBankBranchRepository(db);
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
            LS_tblBankBranchRepository _repository = new LS_tblBankBranchRepository(db);
            bool flag = _repository.IsVNNameExisted(VNName);
            bool result = (flag == true) ? false : true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ValidateName(string Name)
        {
            LS_tblBankBranchRepository _repository = new LS_tblBankBranchRepository(db);
            bool flag = _repository.IsNameExisted(Name);
            bool result = (flag == true) ? false : true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // Get Companies
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetBankBranchesByBankId(int id)
        {
            try
            {
                var lst = db.LS_tblBankBranch.Where(p=>p.LSBankID == id)
                    .Select(p => new LS_tblBankBranchViewModel()
                    {
                        LSBankID = p.LSBankID,
                        LSBankBranchID = p.LSBankBranchID,
                        LSBankBranchCode = p.LSBankBranchCode,
                        Name = p.Name,
                        VNName = p.VNName
                    }).OrderBy(p => p.Name).ToList();

                var retData = lst.Select(m => new SelectListItem()
                {
                    Text = (LanguageId == 124) ? m.Name : m.VNName,
                    Value = m.LSBankBranchID.ToString(),
                });
                return Json(retData, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Dùng cho viec binding du lieu cho dropdownlist autocomplete
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int? LSBankID, int pageSize, int pageNum)
        {
            LS_tblBankBranchRepository _repository = new LS_tblBankBranchRepository(db);
            List<AutoCompleteModel> sources = _repository.ListDropdown(searchTerm, LSBankID, pageSize, pageNum, LanguageId).ToList();
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

     
    }
}
