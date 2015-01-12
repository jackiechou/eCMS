using Eagle.Common.Utilities;
using Eagle.Model.SYS.Modules;
using Eagle.Model.SYS.Permission;
using Eagle.Model.SYS.Roles;
using Eagle.Repository;
using Eagle.Repository.Sys.Permissions;
using Eagle.Repository.SYS;
using Eagle.Repository.SYS.Contents;
using Eagle.Repository.SYS.Modules;
using Eagle.Repository.SYS.Roles;
using Eagle.Repository.SYS.Session;
using Eagle.Repository.UI.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.SYS.Modules
{
    public class ModuleController : BaseController
    {
        //
        // GET: /Admin/Module/        
        [SessionExpiration]
        [PageLayoutAttribute]
        public ActionResult Index()
        {
            ViewBag.sPageId = PageRepository.PopulatePageSelectList(ScopeTypeId, null, null, true);

            if (Request.IsAjaxRequest())
                return PartialView("../Sys/Modules/_Reset");
            else
                return View("../Sys/Modules/Index");
        }

        [SessionExpiration]
        [HttpGet]
        public ActionResult List(int? sPageId)
        {
            List<ModuleViewModel> lst = ModuleRepository.GetListByPageIdAndIsAdmin(ScopeTypeId,sPageId, true);
            return PartialView("../Sys/Modules/_List", lst);
        }

        [SessionExpiration]
        [HttpGet]
        public JsonResult GetModuleList(bool IsSecured)
        {
            List<ModuleViewModel> lst = ModuleRepository.GetList(true);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [SessionExpiration]
        [HttpGet]
        public JsonResult GetList(string Keywords, int PageId, bool? IsSecured)
        {
            List<ModuleViewModel> lst = ModuleRepository.GetListByKeywordsPageIdIsAdmin(Keywords, PageId, IsSecured);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [SessionExpiration]
        [HttpGet]
        public ActionResult PopulateList()
        {
            List<ModuleViewModel> lst = ModuleRepository.GetList(true);
            return PartialView("../Sys/Modules/_ModuleList", lst);
        }

        // GET: /Admin/Module/Details/5        
        [HttpGet]
        [SessionExpiration]
        public ActionResult Edit(int Id)
        {
            ModuleViewModel model = ModuleRepository.Details(Id);
            ViewBag.PageId = PageRepository.PopulateActivePageSelectList(ScopeTypeId, LanguageCode, model.PageId.ToString(), true);
            ViewBag.ModuleId = ModuleRepository.PopulateModuleList(model.ModuleId.ToString(), true);
            ViewBag.Visibility = ModuleRepository.PopulateVisibilityList(model.Visibility.ToString(), true);
            ViewBag.ContentItemId = ContentItemRepository.PopulateContentItemsByModuleToDropDownList(model.ContentItemId.ToString(), true);
            ViewBag.Pane = ModuleRepository.PopulatePaneList(model.Pane, true);
            ViewBag.InsertedPosition = ModuleRepository.PopulateInsertedPositionList(model.InsertedPosition, true);
            ViewBag.Alignment = ModuleRepository.PopulateAlignmentList(model.Alignment, true);

            return PartialView("../Sys/Modules/_Edit", model);
        }

        // GET: /Admin/Module/Create
        [SessionExpiration]
        public ActionResult Create()
        {
            string sCurrentController = CurrentController;
            string sAction = CurrentAction;
            string sCurrentMenuCode = CurrentMenuCode.ToString();

            return PartialView("../Sys/Modules/_Create");
        }

        [SessionExpiration]
        [HttpGet]
        public ActionResult AddNewModule()
        {
            ViewBag.PageId = PageRepository.PopulateActivePageSelectList(ScopeTypeId, LanguageCode, null, true);
            ViewBag.Visibility = ModuleRepository.PopulateVisibilityList(null, true);
            ViewBag.ContentItemId = ContentItemRepository.PopulateContentItemsByModuleToDropDownList(null, true);
            ViewBag.Pane = ModuleRepository.PopulatePaneList(null, true);
            ViewBag.InsertedPosition = ModuleRepository.PopulateInsertedPositionList(null, true);
            ViewBag.Alignment = ModuleRepository.PopulateAlignmentList(null, true);
            return PartialView("../Sys/Modules/_AddNewModule");
        }

        [SessionExpiration]
        [HttpGet]
        public ActionResult AddExistingModule()
        {
            ViewBag.PageId = PageRepository.PopulateActivePageSelectList(ScopeTypeId, LanguageCode, null, true); 
            ViewBag.ModuleId = ModuleRepository.PopulateModuleList(null, true);
            ViewBag.Visibility = ModuleRepository.PopulateVisibilityList(null, true);
            ViewBag.ContentItemId = ContentItemRepository.PopulateContentItemsByModuleToDropDownList(null, true);
            ViewBag.Pane = ModuleRepository.PopulatePaneList(null, true);
            ViewBag.InsertedPosition = ModuleRepository.PopulateInsertedPositionList(null, true);
            ViewBag.Alignment = ModuleRepository.PopulateAlignmentList(null, true);
            return PartialView("../Sys/Modules/_AddExistingModule");
        }

 
        //// POST: /Admin/Module/Create
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Create(ModuleViewModel add_model, bool chkModuleStatus)
        //{
        //    bool flag = false;
        //    string message = string.Empty;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            DateTime? startDate = add_model.EffectiveDate;
        //            DateTime? endDate = add_model.ExpiredDate;
        //            if (startDate.HasValue && endDate.HasValue)
        //            {
        //                if (DateTime.Compare(startDate.Value, endDate.Value) > 0)
        //                {
        //                    flag = false;
        //                    message = Eagle.Resource.LanguageResource.ValidateEffectiveDateExpiredDate;
        //                    return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        //                }
        //            }
        //            ModuleRepository _repository = new ModuleRepository(db);
        //            add_model.Creater = CurrentEmpId;
        //            add_model.ModuleStatus = chkModuleStatus == true ? 1 : 0;
        //            flag = _repository.Add(add_model, out message);
        //        }
        //        else
        //        {
        //            var errors = ModelState.Values.SelectMany(v => v.Errors);
        //            foreach (var modelError in errors)
        //            {
        //                message += modelError.ErrorMessage + "-";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.ToString();
        //        flag = false;
        //    }
        //    return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        //}

        ////POST - UpdateFileIds
        ////[HttpPost]
        //public ActionResult UpdateFileIds(int Id, string Added_FileIds)
        //{
        //    bool flag = false;
        //    string message = string.Empty;
        //    flag = ModuleRepository.UpdateFileIds(Id, Added_FileIds, out message);
        //    return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        //}

        ////
        //// POST: 

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Edit(ModuleViewModel edit_model, bool chkModuleStatus)
        //{
        //    bool flag = false;
        //    string message = string.Empty;
        //    if (ModelState.IsValid)
        //    {
        //        DateTime? startDate = edit_model.EffectiveDate;
        //        DateTime? endDate = edit_model.ExpiredDate;
        //        if (startDate.HasValue && endDate.HasValue)
        //        {
        //            if (DateTime.Compare(startDate.Value, endDate.Value) > 0)
        //            {
        //                flag = false;
        //                message = Eagle.Resource.LanguageResource.ValidateEffectiveDateExpiredDate;
        //                return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        //            }
        //        }

        //        ModuleRepository _repository = new ModuleRepository(db);
        //        edit_model.Creater = CurrentEmpId;
        //        edit_model.ModuleStatus = chkModuleStatus == true ? 1 : 0;
        //        flag = _repository.Edit(edit_model, out message);
        //    }
        //    else
        //    {
        //        var errors = ModelState.Values.SelectMany(v => v.Errors);
        //        foreach (var modelError in errors)
        //        {
        //            message += modelError.ErrorMessage + "-";
        //        }
        //    }
        //    return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        //}


        //
        // POST: /Admin/Module/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            bool flag = false;
            ModuleRepository _repository = new ModuleRepository(db);
            flag = _repository.Delete(id, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

    }
}
