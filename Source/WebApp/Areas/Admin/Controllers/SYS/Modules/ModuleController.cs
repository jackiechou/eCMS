using Eagle.Common.Utilities;
using Eagle.Model.SYS.Modules;
using Eagle.Model.SYS.Roles;
using Eagle.Repository;
using Eagle.Repository.SYS;
using Eagle.Repository.SYS.Contents;
using Eagle.Repository.SYS.Modules;
using Eagle.Repository.SYS.Roles;
using Eagle.Repository.SYS.Session;
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
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView("../SYS/Modules/_Reset");
            else
                return View("../SYS/Modules/Index");
        }

        [SessionExpiration]
        public ActionResult List()
        {
            List<ModuleViewModel> lst = ModuleRepository.GetList(true);
            return PartialView("../SYS/Modules/_List", lst);
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
            return PartialView("../SYS/Modules/_ModuleList", lst);
        }

        // GET: /Admin/Module/Details/5        
        [HttpGet]
        [SessionExpiration]
        public ActionResult Edit(int Id)
        {
            ModuleViewModel model = ModuleRepository.Details(Id);
            ViewBag.ContentItemId = ContentItemRepository.PopulateContentItemsByModuleToDropDownList(model.ContentItemId.ToString(), false);
            return PartialView("../SYS/Modules/_Edit", model);
        }

        // GET: /Admin/Module/Create
        [SessionExpiration]
        public ActionResult Create()
        {
            string sCurrentController = CurrentController;
            string sAction = CurrentAction;
            string sCurrentMenuCode = CurrentMenuCode.ToString();
           
            return PartialView("../SYS/Modules/_Create");
        }

        [SessionExpiration]
        [HttpGet]
        public ActionResult AddNewModule()
        {
            ViewBag.PageId = PageRepository.PopulateActivePageSelectList(ScopeTypeId, LanguageCode);           
            ViewBag.Visibility = ModuleRepository.PopulateVisibilityList(null, false);
            ViewBag.ContentItemId = ContentItemRepository.PopulateContentItemsByModuleToDropDownList(null, false);
            ViewBag.Pane = ModuleRepository.PopulatePaneList(null, false);
            ViewBag.InsertedPosition = ModuleRepository.PopulateInsertedPositionList(null, false);
            ViewBag.Alignment = ModuleRepository.PopulateAlignmentList(null, false);
            return PartialView("../SYS/Modules/_AddNewModule");
        }

        [SessionExpiration]
        [HttpGet]
        public ActionResult AddExistingModule()
        {
            ViewBag.PageId = PageRepository.PopulateActivePageSelectList(ScopeTypeId, LanguageCode);
            ViewBag.ModuleId = ModuleRepository.PopulateModuleList(null, false);
            ViewBag.Visibility = ModuleRepository.PopulateVisibilityList(null, false);
            ViewBag.ContentItemId = ContentItemRepository.PopulateContentItemsByModuleToDropDownList(null, false);
            ViewBag.Pane = ModuleRepository.PopulatePaneList(null, false);
            ViewBag.InsertedPosition = ModuleRepository.PopulateInsertedPositionList(null, false);
            ViewBag.Alignment = ModuleRepository.PopulateAlignmentList(null, false);
            return PartialView("../SYS/Modules/_AddExistingModule");
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
