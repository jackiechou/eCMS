using Eagle.Model;
using Eagle.Repository;
using Eagle.Repository.SYS;
using Eagle.Repository.SYS.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.SYS.ModuleFunctions
{
    public class ModuleFunctionController : BaseController
    {
        //
        // GET: /Admin/FunctionList/
        [HttpGet]
        public ActionResult ListFunctionsByModuleID(int ModuleID)
        {
            FunctionPermissionRepository _respository = new FunctionPermissionRepository(db);
            List<FunctionListViewModel> functionList = _respository.GetFunctionListByModuleIDAndUserID(ModuleID, UserId, LanguageId);
            return PartialView("../Sys/FunctionList/_VocationalFunctionsByModule", functionList);
        }

        [HttpGet]
        public ActionResult EditListFunctionsByModuleID(int ModuleID)
        {
            FunctionPermissionRepository _respository = new FunctionPermissionRepository(db);
            List<FunctionListViewModel> functionList = _respository.GetFunctionListByModuleIDAndUserID(ModuleID, UserId, LanguageId);
            return PartialView("../Sys/FunctionList/_VocationalFunctionsByModuleEdit", functionList);
        }


        [HttpGet]
        public ActionResult GetFunctionListByModuleIDAndUserID(int ModuleID, int UserID)
        {
            FunctionPermissionRepository _respository = new FunctionPermissionRepository(db);
            List<FunctionListViewModel> functionList = _respository.GetFunctionListByModuleIDAndUserID(ModuleID, UserID, LanguageId);
            return PartialView("../Sys/FunctionList/_VocationalFunctionsByModule", functionList);
        }


        public ActionResult Index()
        {
            if (CurrentEmpId != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("../Sys/FunctionList/_Reset");
                }
                else
                {
                    if (CurrentAcc == null)
                    {
                        Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                        return null;
                    }
                    return View("../Sys/FunctionList/Index");
                }
            }
            else
            {
                Response.RedirectToRoute("Admin_Login");
                return null;
            }
        }

        // [SessionExpiration]
        //public ActionResult List()
        //{
        //    if (EmpId != null)
        //    {
        //        IList<FunctionListViewModel> sources = FunctionListRespository.List();
        //        return PartialView("../Sys/FunctionList/_List", sources);
        //    }
        //    else
        //    {
        //        Response.RedirectToRoute("Admin_Login");
        //        return null;
        //    }
        //}
        #region Reference =================================================================
        public void PopulateHiearchialFunctionsToDropDownList(bool? isSelectOption, int? selected_value)
        {
            List<FunctionEntity> list = FunctionListRespository.PopulateHiearchialDataToDropDownList(LanguageId);

            if (list.Count == 0)
                list.Insert(0, new FunctionEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });

            if (isSelectOption != null && isSelectOption == true)
                list.Insert(0, new FunctionEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });


            SelectList selectList = new SelectList(list, "Id", "Name", selected_value);
            ViewBag.Parent = selectList;
        }



        //public void PopulateTerminationTypesToDropDownList(bool? isSelectOption, int? selected_value)
        //{
        //    ViewBag.LSTerminationTypeID = LS_tblTerminationTypeRespository.PopulateDataToDropDownList(isSelectOption, selected_value, LanguageId);
        //}

        //public void PopulateTerminationReasonsToDropDownList(bool? isSelectOption, int? selected_value)
        //{
        //    ViewBag.LSTerminationReasonID = LS_tblTerminationReasonRespository.PopulateDataToDropDownList(isSelectOption, selected_value, LanguageId);
        //}

        #endregion ========================================================================

        //[HttpGet]
        //public ActionResult CreateDownloadLink(int? FileId)
        //{
        //    FileRepository _respository = new FileRepository(db);
        //    string DownloadFileLink = _respository.GenerateDownloadLink(FileId);
        //    return PartialView("../Sys/FunctionList/_DownloadLink", DownloadFileLink);
        //}
        ////
        //// GET: /Admin/LS_tblTerminationInfo/Create

        [SessionExpiration]
        public ActionResult Create()
        {
            //if (Session[SettingKeys.AccountInfo] != null)
            //{
            //ViewBag.Mode = 0;
            PopulateHiearchialFunctionsToDropDownList(true, null);
            //PopulateTerminationReasonsToDropDownList(true, null);
            FunctionListViewModel model = new FunctionListViewModel();
            return PartialView("../Sys/FunctionList/_Create", model);
            //}
            //else
            //{
            //    Response.RedirectToRoute("Admin_Login");
            //    return null;
            //}
        }

        ////
        //// GET: /Admin/LS_tblTerminationInfo/Details/5
        //[SessionExpiration]
        //[HttpGet]
        //public ActionResult Details(int id)
        //{
        //    if (EmpId != null)
        //    {
        //        ViewBag.Mode = 1;
        //        TerminationViewModel entity = TerminationRespository.Details(id);
        //        PopulateTerminationTypesToDropDownList(true, entity.LSTerminationTypeID);
        //        PopulateTerminationReasonsToDropDownList(true, entity.LSTerminationReasonID);
        //        return PartialView("../Sys/FunctionList/_Edit", entity);
        //    }
        //    else
        //    {
        //        Response.RedirectToRoute("Admin_Login");
        //        return null;
        //    }
        //}


        //[HttpPost]
        //public ActionResult Create(TerminationViewModel model)
        //{
        //    string message = "";
        //    bool flag = false;
        //    try
        //    {
        //        if (EmpId != null)
        //        {
        //            if (ModelState.ContainsKey("TerminationID"))
        //            {
        //                ModelState["TerminationID"].Errors.Clear();
        //            }
        //            //ModelState.Remove(field)

        //            if (ModelState.IsValid)
        //            {

        //                DateTime? InformedDate = model.InformedDate;
        //                DateTime? LastWorkingDate = model.LastWorkingDate;
        //                DateTime? EffectiveDate = model.EffectiveDate;

        //                if (InformedDate.HasValue && LastWorkingDate.HasValue)
        //                {
        //                    if (DateTime.Compare(InformedDate.Value, LastWorkingDate.Value) > 0)
        //                    {
        //                        flag = false;
        //                        message = Eagle.Resource.LanguageResource.ValidateJoinedSignedDate;
        //                        return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        //                    }
        //                }

        //                if (LastWorkingDate.HasValue && EffectiveDate.HasValue)
        //                {
        //                    if (DateTime.Compare(LastWorkingDate.Value, EffectiveDate.Value) > 0)
        //                    {
        //                        flag = false;
        //                        message = Eagle.Resource.LanguageResource.ValidateSignedEffectiveDate;
        //                        return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        //                    }
        //                }

        //                flag = TerminationRespository.Add(model, out message);
        //            }
        //            else
        //            {
        //                var errors = ModelState.Values.SelectMany(v => v.Errors);
        //                foreach (var modelError in errors)
        //                {
        //                    message += modelError.ErrorMessage + "-";
        //                }
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


        //[HttpPost]
        //public ActionResult Edit(TerminationViewModel model)
        //{
        //    string message = "";
        //    bool flag = false;
        //    try
        //    {
        //        if (EmpId != null)
        //        {
        //            //ModelState["Code"].Errors.Clear();
        //            if (ModelState.IsValid)
        //            {
        //                DateTime? InformedDate = model.InformedDate;
        //                DateTime? LastWorkingDate = model.LastWorkingDate;
        //                DateTime? EffectiveDate = model.EffectiveDate;

        //                if (InformedDate.HasValue && LastWorkingDate.HasValue)
        //                {
        //                    if (DateTime.Compare(InformedDate.Value, LastWorkingDate.Value) > 0)
        //                    {
        //                        flag = false;
        //                        message = Eagle.Resource.LanguageResource.ValidateJoinedSignedDate;
        //                        return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        //                    }
        //                }

        //                if (LastWorkingDate.HasValue && EffectiveDate.HasValue)
        //                {
        //                    if (DateTime.Compare(LastWorkingDate.Value, EffectiveDate.Value) > 0)
        //                    {
        //                        flag = false;
        //                        message = Eagle.Resource.LanguageResource.ValidateSignedEffectiveDate;
        //                        return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        //                    }
        //                }

        //                flag = TerminationRespository.Update(model, out message);
        //            }
        //            else
        //            {
        //                var errors = ModelState.Values.SelectMany(v => v.Errors);
        //                foreach (var modelError in errors)
        //                {
        //                    message += "&lt;br /&gt;" + modelError.ErrorMessage;
        //                }
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

        ////
        //// POST: /Admin/TerminationParameter/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    string message = string.Empty;
        //    bool flag = TerminationRespository.Delete(id, out message);
        //    return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        //}

    }
}
