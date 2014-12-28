using Eagle.Common.Entities;
using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository;
using Eagle.Repository.HR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.HR;
using Eagle.Model.SYS;
using Eagle.Repository.SYS.Session;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class QualificationController : BaseController
    {
        #region Reference =================================================================
        public void PopulateQualificationToDropDownList(int? selected_value)
        {
            LS_tblQualificationRepository _respository = new LS_tblQualificationRepository(db);
            List<QualificationEntity> _list = _respository.GetListForDropDownList();
            if (_list.Count == 0)
                _list.Insert(0, new QualificationEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
            else
                _list.Insert(0, new QualificationEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            ViewBag.LSQualificationID = new SelectList(_list, "Id", "Name", selected_value);
        }

        public void PopulateTrainingPlaceToDropDownList(int? selected_value)
        {
            TrainingPlaceModelList lst = new TrainingPlaceModelList(LanguageId);
            ViewBag.TrainingPlace = new SelectList(lst.TrainingPlaceList, "TrainingPlaceId", "TrainingPlaceName", selected_value);
        }

        #endregion ========================================================================

      
        //[HttpGet]
        //public ActionResult CreateDownloadLink(int? FileId)
        //{
        //    FileRepository _respository = new FileRepository(db);
        //    string DownloadFileLink = _respository.GenerateDownloadLink(FileId);
        //    return PartialView("../HR/Qualification/_DownloadLink", DownloadFileLink);
        //}
        //
        // GET: /Admin/Qualification/

        [SessionExpiration]
        public ActionResult Index()
        {           
            if (Request.IsAjaxRequest())
            {
                return PartialView("../HR/Qualification/_Reset");
            }
            else
            {
                return View("../HR/Qualification/Index");
            }               
        }

        [SessionExpiration]
        public ActionResult List()
        {
            List<QualificationViewModel> sources = new List<QualificationViewModel>();
           
            int? ModuleId = 2;
            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);    
            QualificationRespository _repository = new QualificationRespository(db);
            sources = _repository.GetList(CurrentEmpId, ModuleId, UserName, isAdmin);
            
            return PartialView("../HR/Qualification/_List", sources);
        }

        [AjaxSessionActionFilter]
        public ActionResult GetList(int? EmpID, int? ModuleID)
        {
            List<QualificationViewModel> sources = new List<QualificationViewModel>();            
            if (EmpID != null && ModuleID != null)
            {
                QualificationRespository _repository = new QualificationRespository(db);
                sources = _repository.GetList(EmpID, ModuleID, UserName, isAdmin);
            }
            else
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);           
            return PartialView("../HR/Qualification/_List", sources);
        }    
     
        //
        // GET: /Admin/Qualification/Create
        [SessionExpiration]
        public ActionResult Create()
        {
            QualificationViewModel model = new QualificationViewModel();            
            PopulateQualificationToDropDownList(null);
            PopulateTrainingPlaceToDropDownList(null);               
            model.Priority = QualificationRespository.GeneratePriority(CurrentEmpId);                
            // model.QualificationNo = QualificationRespository.GenerateQualificationNo(10);
            ViewBag.DivCountryStaus = "none";           
            return PartialView("../HR/Qualification/_Create", model);
        }
              
        //
        // POST: /Admin/Qualification/Create       
        [HttpPost]
        [SessionExpiration]
        public ActionResult Create(QualificationViewModel add_model)
        {
            bool flag = false;
            string message = string.Empty;
            
            try
            {
                if (ModelState.IsValid)
                {
                    DateTime? startDate = add_model.FromMonth;
                    DateTime? endDate = add_model.ToMonth;
                    if (startDate.HasValue && endDate.HasValue)
                    {
                        if (DateTime.Compare(startDate.Value, endDate.Value) > 0)
                        {
                            //ModelState.AddModelError("Date", Eagle.Resource.LanguageResource.ValidateStartEndMonth);
                            flag = false;
                            message = Eagle.Resource.LanguageResource.ValidateStartEndMonth;
                        }
                    }
                    QualificationRespository _repository = new QualificationRespository(db);
                    bool isPriorityExists = _repository.IsPriorityExists(add_model.EmpID, add_model.Priority);                    
                    if (isPriorityExists)
                        add_model.Priority = QualificationRespository.GeneratePriority(add_model.EmpID);
                   
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

        //
        // GET: /Admin/Qualification/Edit/5
         [SessionExpiration]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            QualificationViewModel model = new QualificationViewModel();           
            QualificationRespository _repository = new QualificationRespository(db);
            model = _repository.Details(id, LanguageId);
            Session[SettingKeys.EmpInfo] = EmployeeRepository.GetBriefDetails(model.EmpID, LanguageId);
            PopulateQualificationToDropDownList(model.LSQualificationID);
            PopulateTrainingPlaceToDropDownList(model.TrainingPlace);

            if(model.TrainingPlace == 2)
                ViewBag.DivCountryStaus = "block";
            else
                ViewBag.DivCountryStaus = "none";
             
            return PartialView("../HR/Qualification/_Edit", model);
        }
        
        //
        // POST: /Admin/Qualification/Edit/5
        [HttpPost]
        [SessionExpiration]
         public ActionResult Edit(QualificationViewModel edit_model)
        {
            bool flag = false;
            string message = string.Empty;
           
            try
            {
                if (ModelState.IsValid)
                {
                    DateTime? startDate = edit_model.FromMonth;
                    DateTime? endDate = edit_model.ToMonth;
                    if (startDate.HasValue && endDate.HasValue)
                    {
                        if (DateTime.Compare(startDate.Value, endDate.Value) > 0)
                        {
                            //ModelState.AddModelError("Date", "start date must be before end date");
                            flag = false;                           
                            message = Eagle.Resource.LanguageResource.ValidateStartEndMonth;
                        }
                    }
                                       
                    QualificationRespository _repository = new QualificationRespository(db);
                    flag = _repository.Edit(edit_model, out message);    
                    //bool isPriorityExists = true;
                    //if (edit_model.Priority != edit_model.InitialPriority)
                    //{
                    //    isPriorityExists = _repository.IsPriorityExists(edit_model.EmpID, edit_model.Priority);
                    //    if (isPriorityExists == false)
                    //        flag = _repository.Update(edit_model, out message);
                    //    else
                    //    {
                    //        flag = false;
                    //        message = Eagle.Resource.LanguageResource.InValidPriority;
                    //        ModelState.AddModelError("Priority", "Priority existed");
                    //    }
                    //}                                            
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
        
        //POST - UpdateFileIds
        public ActionResult UpdateFileIds(int Id, string FileIds)
        {
            bool flag = false;
            string message = string.Empty;           
            flag = QualificationRespository.UpdateFileIds(Id, FileIds, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        [SessionExpiration]
        [HttpPost]
        public ActionResult UpdateListOrder(int id, int listorder)
        {
            string message = string.Empty;
            bool flag = QualificationRespository.UpdateListOrder(id, listorder, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Admin/Qualification/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            bool flag = false;
                          
            QualificationRespository _repository = new QualificationRespository(db);
            flag = _repository.Delete(id, out message);
           
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        #region FILE MANAGER ======================================================================
        [SessionExpiration]
        [HttpGet]
        public ActionResult AddFileToFileList(int FileId, string FileIds)
        {
            string message = string.Empty;
            bool flag = QualificationRespository.AddFileToFileList(FileId, FileIds, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDownloadedFileList(int id)
        {
            List<FileViewModel> downloadList = QualificationRespository.GetDownloadedFileList(id);
            return PartialView("../Qualification/_DownloadFileList", downloadList);
        }

        [HttpGet]
        public ActionResult GetDownloadFileList(string FileIds)
        {
            List<FileViewModel> downloadList = QualificationRespository.GetDownloadFileList(FileIds);
            return PartialView("../Qualification/_DownloadFileList", downloadList);
        }

        [HttpPost]
        public ActionResult DeleteFileInFileList(int Id, int FileId)
        {
            string message = string.Empty;
            bool flag = false;
            QualificationRespository _repository = new QualificationRespository(db);
            flag = _repository.DeleteFileInFileList(Id, FileId, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }        
        #endregion ===============================================================================

        #region BRIEF - FORM ---------------------------------------------------------------------
        [AjaxSessionActionFilter]
        [HttpGet]
        public ActionResult GetListById(int? EmpID)
        {
            List<QualificationViewModel> sources = new List<QualificationViewModel>();
            if (EmpID != null)
            {
                QualificationRespository _repository = new QualificationRespository(db);
                sources = _repository.GetListById(EmpID, LanguageId);
            }
            return PartialView("../HR/Qualification/_BriefList", sources);
        }  

        [SessionExpiration]
        [HttpGet]
        public ActionResult CreateBriefForm(int EmpID)
        {
            QualificationCreateModel model = new QualificationCreateModel();           
            PopulateQualificationToDropDownList(null);
            PopulateTrainingPlaceToDropDownList(null);
            model.Priority = QualificationRespository.GeneratePriority(CurrentEmpId);
            model.QualificationNo = QualificationRespository.GenerateQualificationNo(10);
            model.EmpID = EmpID;
            ViewBag.DivCountryStaus = "none";           
            return PartialView("../HR/Qualification/_CreateBriefForm", model);
        }


        [SessionExpiration]
        [HttpGet]
        public ActionResult EditBriefForm(int id)
        {
            QualificationEditModel model = new QualificationEditModel();            
            QualificationRespository _repository = new QualificationRespository(db);
            model = _repository.GetDetails(id, LanguageId);
            Session[SettingKeys.EmpInfo] = EmployeeRepository.GetBriefDetails(model.EmpID, LanguageId);
            PopulateQualificationToDropDownList(model.LSQualificationID);
            PopulateTrainingPlaceToDropDownList(model.TrainingPlace);

            if (model.TrainingPlace == 2)
                ViewBag.DivCountryStaus = "block";
            else
                ViewBag.DivCountryStaus = "none";
           
            return PartialView("../HR/Qualification/_EditBriefForm", model);
        }

        //
        // POST: /Admin/Qualification/Create       
        [HttpPost]
        public ActionResult Insert(QualificationCreateModel add_model)
        {
            bool flag = false;
            string Message = string.Empty;
            if (Session[SettingKeys.AccountInfo] != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        DateTime? startDate = add_model.FromMonth;
                        DateTime? endDate = add_model.ToMonth;
                        if (startDate.HasValue && endDate.HasValue)
                        {
                            if (DateTime.Compare(startDate.Value, endDate.Value) > 0)
                            {
                                //ModelState.AddModelError("Date", Eagle.Resource.LanguageResource.ValidateStartEndMonth);
                                flag = false;
                                Message = Eagle.Resource.LanguageResource.ValidateStartEndMonth;
                            }
                        }
                        QualificationRespository _repository = new QualificationRespository(db);
                        bool isPriorityExists = _repository.IsPriorityExists(add_model.EmpID, add_model.Priority);
                        if (isPriorityExists)
                            add_model.Priority = QualificationRespository.GeneratePriority(add_model.EmpID);

                        flag = _repository.Insert(add_model, out Message);
                    }
                    else
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        foreach (var modelError in errors)
                        {
                            Message += modelError.ErrorMessage + "-";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Message = ex.ToString();
                    flag = false;
                }
            }
            else
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return Json(JsonUtils.SerializeResult(flag, Message), JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Admin/Qualification/Edit/5
        [HttpPost]
        public ActionResult Update(QualificationEditModel edit_model)
        {
            bool flag = false;
            string Message = string.Empty;
            if (Session[SettingKeys.AccountInfo] != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        DateTime? startDate = edit_model.FromMonth;
                        DateTime? endDate = edit_model.ToMonth;
                        if (startDate.HasValue && endDate.HasValue)
                        {
                            if (DateTime.Compare(startDate.Value, endDate.Value) > 0)
                            {
                                //ModelState.AddModelError("Date", "start date must be before end date");
                                flag = false;
                                Message = Eagle.Resource.LanguageResource.ValidateStartEndMonth;
                            }
                        }

                        QualificationRespository _repository = new QualificationRespository(db);
                        flag = _repository.Update(edit_model, out Message);
                    }
                    else
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        foreach (var modelError in errors)
                        {
                            Message += modelError.ErrorMessage + "-";
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    flag = false;
                }
            }
            else
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
            return Json(JsonUtils.SerializeResult(flag, Message), JsonRequestBehavior.AllowGet);
        }
        #endregion ------------------------------------------------------------------------------

        [HttpGet]
        [SessionExpiration]
        public JsonResult GenerateCode()
        {
            string Code = QualificationRespository.GenerateQualificationNo(10);
            return Json(Code, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidateCode(string QualificationNo)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(QualificationNo))
                result = QualificationRespository.IsCodeExisted(QualificationNo);
            return base.Json((result==true)?false:true, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult ValidationPriority(int Priority, int EmpID, int? InitialPriority)
        {
            QualificationRespository _repository = new QualificationRespository(db);
            if (InitialPriority != null)
            {
                if ((InitialPriority != Priority) && _repository.IsPriorityExists(EmpID, Priority))
                    return base.Json(Eagle.Resource.LanguageResource.InValidPriority, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (_repository.IsPriorityExists(EmpID, Priority))
                    return base.Json(Eagle.Resource.LanguageResource.InValidPriority, JsonRequestBehavior.AllowGet);
            }
            return base.Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
