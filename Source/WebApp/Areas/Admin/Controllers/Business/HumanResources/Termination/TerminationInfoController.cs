using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository;
using Eagle.Repository.LS;
using Eagle.Repository.Termination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.TER;
using Eagle.Repository.SYS.Session;
using Eagle.Model.HR;
using Eagle.Repository.HR;
using Eagle.Common.Settings;
using Eagle.Repository.SYS.FileManager;

namespace Eagle.WebApp.Areas.Admin.Controllers.Termination
{
    public class TerminationInfoController : BaseController
    {
        //
        // GET: /Admin/Termination/
         [SessionExpiration]
        public ActionResult Index(int? LSCompanyID, string FullName, string EmpCode, string InformedDate, string EffectiveDate, int? ModuleId)
        {           
            if (Request.IsAjaxRequest())
            {              
                if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                    ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                TerminationRespository _respository = new TerminationRespository(db);
                List<TerminationViewModel> sources = new List<TerminationViewModel>();
                sources = _respository.Search(LSCompanyID, FullName, EmpCode, InformedDate, EffectiveDate, ModuleId, UserName, isAdmin);
                return PartialView("../Business/HumanResources/Termination/TerminationInfo/_SearchResults", sources);
            }
            else
                return View("../Business/HumanResources/Termination/TerminationInfo/Index");                    
        }


         //Show  termination list with Type: 1:Ngay hien tai, 7:tuan hien tai, 30:thang hien tai, mac dinh la thang hien tai
         [HttpGet]
         public ActionResult GetTerminationList(int? Type)
         {
             List<TerminationViewModel> lst = TerminationRespository.GetTerminationList(Type, LanguageId);
             return PartialView("../Business/HumanResources/Termination/TerminationInfo/_TerminationList", lst);
         }


         public ActionResult _SearchForm()
         {
             if (Session[SettingKeys.AccountInfo] != null)
             {
                 return PartialView("../Business/HumanResources/Termination/TerminationInfo/_SearchForm");
             }
             else
             {
                 Response.RedirectToRoute("Admin_Login");
                 return null;
             }
         }

        [SessionExpiration]
        public ActionResult _SearchResults(int? LSCompanyID, string FullName, string EmpCode, string InformedDate, string EffectiveDate, int? ModuleId)
        {               
            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            TerminationRespository _respository = new TerminationRespository(db);
            List<TerminationViewModel> sources = new List<TerminationViewModel>();         
            sources = _respository.Search(LSCompanyID, FullName, EmpCode, InformedDate, EffectiveDate, ModuleId, UserName, isAdmin);
            return PartialView("../Business/HumanResources/Termination/TerminationInfo/_SearchResults", sources);
        }

        #region Confirm box -----------------------------------------------
        public ActionResult ConfirmationPopup()
        {
            return PartialView("../Business/HumanResources/Termination/TerminationInfo/_ConfirmationPopup");
        }
        #endregion

          #region Reference =================================================================
        public void PopulateTerminationTypesToDropDownList(bool? isSelectOption, int? selected_value)
        {
            ViewBag.LSTerminationTypeID = LS_tblTerminationTypeRespository.PopulateDataToDropDownList(isSelectOption, selected_value, LanguageId);
        }

        public void PopulateTerminationReasonsToDropDownList(bool? isSelectOption, int? selected_value)
        {
            ViewBag.LSTerminationReasonID = LS_tblTerminationReasonRespository.PopulateDataToDropDownList(isSelectOption, selected_value, LanguageId);
        }

        #endregion ========================================================================

        [HttpGet]
        public ActionResult CreateDownloadLink(int? FileId)
        {
            FileRepository _respository = new FileRepository(db);
            string DownloadFileLink = _respository.GenerateDownloadLink(FileId);
            return PartialView("../Business/HumanResources/Termination/TerminationInfo/_DownloadLink", DownloadFileLink);
        }

        [SessionExpiration]
        public ActionResult GetTerminationInfoOfEmployee(int? EmpID)
        {
            EmployeeViewModel entity = new EmployeeViewModel();
            if (EmpID != null && EmpID > 0)
                entity = EmployeeRepository.GetDetailInfo(EmpID, LanguageId);
            else
                entity = (EmployeeViewModel)Session[SettingKeys.EmpInfo];
            return PartialView("../Business/HumanResources/Termination/TerminationInfo/_EmployeeTermination", entity);
        }

        //
        // GET: /Admin/LS_tblTerminationInfo/Create

        //[SessionExpiration]
        //public ActionResult Create()
        //{                     
        //    PopulateTerminationTypesToDropDownList(true, null);
        //    PopulateTerminationReasonsToDropDownList(true, null);
        //    TerminationViewModel model = new TerminationViewModel();
        //    return View("../Business/HumanResources/Termination/TerminationInfo/_Create", model);           
        //}



        [SessionExpiration]
        public PartialViewResult Create(int? TerminationID, int? EmpID)
        {
            TerminationViewModel entity = new TerminationViewModel();
            if (EmpID != null && EmpID > 0)             
                entity = TerminationRespository.GetDetailByEmpID(EmpID);
            if (TerminationID != null && TerminationID > 0)
                entity = TerminationRespository.Details(TerminationID);
            PopulateTerminationTypesToDropDownList(true, entity.LSTerminationTypeID);
            PopulateTerminationReasonsToDropDownList(true, entity.LSTerminationReasonID);
            return PartialView("../Business/HumanResources/Termination/TerminationInfo/_Create", entity);
        }

        [SessionExpiration]
        [HttpGet]
        public ActionResult LoadCreateForm()
        {
            TerminationViewModel entity = new TerminationViewModel();
            PopulateTerminationTypesToDropDownList(true, null);
            PopulateTerminationReasonsToDropDownList(true, null);
            return PartialView("../Business/HumanResources/Termination/TerminationInfo/_Edit", entity);
        }


        public ActionResult List()
        {
            List<TerminationViewModel> lst = new List<TerminationViewModel>();
            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
            {
                int ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                TerminationRespository _respository = new TerminationRespository(db);
                lst = _respository.Search(null, string.Empty,string.Empty, null, null, ModuleId, UserName, isAdmin);
            }
            return PartialView("../Business/HumanResources/Termination/TerminationInfo/_SearchResults", lst);
        }     

        //
        // GET: /Admin/LS_tblTerminationInfo/Details/5
        [SessionExpiration]
        [HttpGet]
        public ActionResult Details(int id)
        {          
            ViewBag.Mode = 1;               
            TerminationViewModel entity = TerminationRespository.Details(id);
            PopulateTerminationTypesToDropDownList(true, entity.LSTerminationTypeID);
            PopulateTerminationReasonsToDropDownList(true, entity.LSTerminationReasonID);
            return PartialView("../Business/HumanResources/Termination/TerminationInfo/_Edit", entity);
        }

        [SessionExpiration]
        [HttpGet]
        public ActionResult GetDetailsByEmpID(int? EmpID)
        {
            TerminationViewModel entity = TerminationRespository.GetDetailByEmpID(EmpID);
            PopulateTerminationTypesToDropDownList(true, entity.LSTerminationTypeID);
            PopulateTerminationReasonsToDropDownList(true, entity.LSTerminationReasonID);
            return PartialView("../Business/HumanResources/Termination/TerminationInfo/_Edit", entity);
        }

        [HttpPost]
        [SessionExpiration]
        public ActionResult Create(TerminationViewModel model)
        {
            string message = "";
            bool flag = false;
            try
            {               
                var culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                var dateformat = new System.Globalization.DateTimeFormatInfo();
                dateformat.ShortDatePattern = "MM/dd/yyyy";
                culture.DateTimeFormat = dateformat;
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;

                if (ModelState.ContainsKey("TerminationID"))                   
                    ModelState["TerminationID"].Errors.Clear();

                ModelState.Remove("EmpCode");
                ModelState.Remove("InformedDate");
                ModelState.Remove("LastWorkingDate");
                ModelState.Remove("EffectiveDate");
                ModelState.Remove("SignDate");
                //ModelState.Remove("ReturnDateForInsuranceCard");
                    
                if (ModelState.IsValid)
                {
                    DateTime? InformedDate = model.InformedDate;
                    DateTime? LastWorkingDate = model.LastWorkingDate;
                    DateTime? EffectiveDate = model.EffectiveDate;

                    if (InformedDate.HasValue && LastWorkingDate.HasValue)
                    {
                        if (DateTime.Compare(InformedDate.Value, LastWorkingDate.Value) > 0)
                        {
                            flag = false;
                            message = Eagle.Resource.LanguageResource.ValidateInformedDateEffectiveDate;
                            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (LastWorkingDate.HasValue && EffectiveDate.HasValue)
                    {
                        if (DateTime.Compare(LastWorkingDate.Value, EffectiveDate.Value) > 0)
                        {
                            flag = false;
                            message = Eagle.Resource.LanguageResource.ValidateLastWorkingDateEffectiveDate;
                            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                        }
                    }

                    flag = TerminationRespository.Add(model, out message);
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


        [HttpPost]
        public ActionResult Edit(TerminationViewModel model)
        {
            string message = "";
            bool flag = false;
            try
            {
                if (Session[SettingKeys.AccountInfo] != null)
                {
                    var culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                    var dateformat = new System.Globalization.DateTimeFormatInfo();
                    dateformat.ShortDatePattern = "MM/dd/yyyy";
                    culture.DateTimeFormat = dateformat;
                    System.Threading.Thread.CurrentThread.CurrentCulture = culture;

                    //ModelState["Code"].Errors.Clear();
                    ModelState.Remove("InformedDate");
                    ModelState.Remove("LastWorkingDate");
                    ModelState.Remove("EffectiveDate");
                    ModelState.Remove("SignDate");
                    ModelState.Remove("ReturnDateForInsuranceCard");

                    if (ModelState.IsValid){
                         DateTime? InformedDate = model.InformedDate;
                        DateTime? LastWorkingDate = model.LastWorkingDate;
                        DateTime? EffectiveDate = model.EffectiveDate;

                        if (InformedDate.HasValue && LastWorkingDate.HasValue)
                        {
                            if (DateTime.Compare(InformedDate.Value, LastWorkingDate.Value) > 0)
                            {
                                flag = false;
                                message = Eagle.Resource.LanguageResource.ValidateJoinedSignedDate;
                                return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                            }
                        }

                        if (LastWorkingDate.HasValue && EffectiveDate.HasValue)
                        {
                            if (DateTime.Compare(LastWorkingDate.Value, EffectiveDate.Value) > 0)
                            {
                                flag = false;
                                message = Eagle.Resource.LanguageResource.ValidateLastWorkingDateEffectiveDate;
                                return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                            }
                        }

                        flag = TerminationRespository.Update(model, out message);
                    }                      
                    else
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        foreach (var modelError in errors)
                        {
                            message += "&lt;br /&gt;" + modelError.ErrorMessage;
                        }
                    }
                }
                else
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
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
         [HttpPost]
        public ActionResult UpdateFileIds(int Id, string FileIds)
        {
            bool flag = false;
            string message = string.Empty;
            flag = TerminationRespository.UpdateFileIds(Id, FileIds, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }
        //
        // POST: /Admin/TerminationParameter/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            bool flag = false;
            if (Session[SettingKeys.AccountInfo] != null)
                flag = TerminationRespository.Delete(id, out message);
            else
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }
    }
}
