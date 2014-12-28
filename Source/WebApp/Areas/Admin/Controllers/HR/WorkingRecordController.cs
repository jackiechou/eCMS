using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository;
using Eagle.Repository.HR;
using Eagle.Repository.LS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.HR;
using Eagle.Model.LS;
using Eagle.Repository.SYS.Session;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class WorkingRecordController : BaseController
    {
        //
        // GET: /Admin/WorkingRecord/

        [SessionExpiration]
        public ActionResult Index()
        {             
            if (Request.IsAjaxRequest())
                return PartialView("../HR/WorkingRecord/_Reset");
            else
                return View("../HR/WorkingRecord/Index");            
        }

        [SessionExpiration]
        public ActionResult List()
        {
            List<WorkingRecordViewModel> sources = new List<WorkingRecordViewModel>();
                           
            if (Session[SettingKeys.EmpId] != null)
            {
                int? ModuleId = 2;
                if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                    ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
                WorkingRecordRespository _repository = new WorkingRecordRespository(db);
                sources = _repository.GetList(CurrentEmpId, ModuleId, UserName, isAdmin, LanguageId);                   
            }
           
            return PartialView("../HR/WorkingRecord/_List", sources);
        }


        [HttpGet]
        [AjaxSessionActionFilter]
        public ActionResult GetList(int? EmpID, int? ModuleID)
        {
            List<WorkingRecordViewModel> sources = new List<WorkingRecordViewModel>();           
            if (EmpID != null && ModuleID != null)
            {                
                WorkingRecordRespository _repository = new WorkingRecordRespository(db);
                sources = _repository.GetList(EmpID, ModuleID, UserName, isAdmin, LanguageId);
            }
            else
                Response.Redirect(Url.Action("Login", "User", new { desiredUrl = Request.Url.AbsoluteUri }));    
            return PartialView("../HR/WorkingRecord/_List", sources);
        }

        //
        // GET: /Admin/WorkingRecord/Details/5
        [HttpGet]
        [SessionExpiration]
        public ActionResult _Details(int id)
        {
            WorkingRecordViewModel entity = new WorkingRecordViewModel();
           
            WorkingRecordRespository _repository = new WorkingRecordRespository(db);
            entity = _repository.Details(id);                
            PopulateChangeStatusToDropDownList(entity.LSChangeStatusID);
            PopulateGradesToDropDownList(entity.LSGradeID);
            PopulateLocationsToDropDownList(entity.LSLocationID);
            PopulatePositionsToDropDownList(entity.LSPositionID);
            PopulateManagersToDropDownList(entity.LineManagerID);
            ViewBag.DecisionNo = entity.DecisionNo;
            Session[SettingKeys.EmpInfo] = EmployeeRepository.GetBriefDetails(entity.EmpID, LanguageId);
               
           
             return PartialView("../HR/WorkingRecord/_Details", entity);
        }
        [HttpGet]
        [SessionExpiration]
        public ActionResult _Create()
        {
            WorkingRecordViewModel entity = new WorkingRecordViewModel();
           
            //PopulateCompanyDepartmentSectionCascadingDropdownlist(null, null, null);
            PopulateChangeStatusToDropDownList(null);
            PopulateGradesToDropDownList(null);
            PopulateLocationsToDropDownList(null);
            PopulatePositionsToDropDownList(null);
            PopulateManagersToDropDownList(null);
            
            return PartialView("../HR/WorkingRecord/_Create", entity);
        }
        [HttpGet]
        public ActionResult _Edit(int id)
        {
            WorkingRecordViewModel entity = new WorkingRecordViewModel();
           
            WorkingRecordRespository _repository = new WorkingRecordRespository(db);
            entity = _repository.Details(id);

            PopulateChangeStatusToDropDownList(entity.LSChangeStatusID);
            PopulateGradesToDropDownList(entity.LSGradeID);
            PopulateLocationsToDropDownList(entity.LSLocationID);
            PopulatePositionsToDropDownList(entity.LSPositionID);
            PopulateManagersToDropDownList(entity.LineManagerID);
            ViewBag.DecisionNo = entity.DecisionNo;
           
            return PartialView("../HR/WorkingRecord/_Edit", entity);
        }


        [HttpPost]
        [SessionExpiration]
        public ActionResult Insert(WorkingRecordViewModel add_model)
        {
            //Thread.Sleep(1000);            
            bool flag = false;
            string message = string.Empty;
                         
            try
            {
                if (ModelState.IsValid)
                {
                    //string FileName = string.Empty;
                    //if (FileUpload != null && FileUpload.ContentLength > 0)
                    //{
                    //    FolderRepository folder_respository = new FolderRepository(db);
                    //    string UploadPath = "~" + folder_respository.GetFolderPathByFolderKey("UPLOAD_DOCUMENT_DIR");
                    //    FileName = FileUtils.UploadFile(FileUpload, UploadPath);
                    //}

                    DateTime? JoinDate = ((EmployeeViewModel)Session[SettingKeys.EmpInfo]).JoinDate;
                    if (JoinDate != null)
                    {
                        if (add_model.EffectiveDate >= JoinDate)
                        {
                            WorkingRecordRespository _repository = new WorkingRecordRespository(db);
                            add_model.Creater = CurrentEmpId;
                            flag = _repository.Add(add_model, out message);
                        }                       
                    }
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


        [HttpPost]
        [SessionExpiration]
        public ActionResult SaveAsCopy(WorkingRecordViewModel add_model)
        {
            //Thread.Sleep(1000);            
            bool flag = false;
            string message = string.Empty;
               
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = false;
                    DateTime? JoinDate = ((EmployeeViewModel)Session[SettingKeys.EmpInfo]).JoinDate;
                    if (JoinDate != null)
                    {
                        if (add_model.EffectiveDate >= JoinDate)
                            result = true;
                    }
                    else
                        result = true;

                    if(result)
                    {
                        add_model.WorkingID = 0;
                        WorkingRecordRespository _repository = new WorkingRecordRespository(db);
                        add_model.Creater = CurrentEmpId;
                        flag = _repository.Add(add_model, out message);
                    }
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

        [HttpPost]
        [SessionExpiration]
        public ActionResult Update(WorkingRecordViewModel edit_model, HttpPostedFileBase FileUpload)
        {
            //Thread.Sleep(1000);            
            bool flag = false;
            string message = string.Empty;
             
            try
            {
                if (ModelState.IsValid)
                {
                    //string FileName = string.Empty, OriginalFileName = string.Empty;
                    //OriginalFileName = edit_model.FileAttach;

                    //if (FileUpload != null && FileUpload.ContentLength > 0)
                    //{
                    //    FolderRepository folder_respository = new FolderRepository(db);
                    //    string UploadPath = "~" + folder_respository.GetFolderPathByFolderKey("UPLOAD_DOCUMENT_DIR");
                    //    string Original_File_Path = UploadPath + "/" + OriginalFileName;
                    //    FileName = FileUtils.UploadFileAndRemoveOldFile(FileUpload, Original_File_Path, UploadPath);
                    //}
                    //else
                    //    FileName = OriginalFileName;

                    DateTime? JoinDate = ((EmployeeViewModel)Session[SettingKeys.EmpInfo]).JoinDate;
                    if (JoinDate != null)
                    {
                        if (edit_model.EffectiveDate >= JoinDate)
                            flag = true;
                    }
                    else
                        flag = true;

                    if(flag == true)
                    {
                        WorkingRecordRespository _repository = new WorkingRecordRespository(db);
                        edit_model.Creater = CurrentEmpId;
                        flag = _repository.Update(edit_model, out message);
                        }                    
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
        
        //
        // POST: /Admin/WorkingRecord/Delete/5
        
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            bool flag = false;
                       
            WorkingRecordRespository _repository = new WorkingRecordRespository(db);
            flag = _repository.Delete(id, out message);  
           
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(FormCollection formCollection)
        {
            string message = string.Empty;
            foreach (var key in formCollection.AllKeys)
            {
                message += key +" = " + formCollection[key] + "<br/>";
            }
            return Content(message);
        }

       

        #region External Reference ================================================================================================
        public void PopulateManagersToDropDownList(int? selected_value)
        {
            EmployeeRepository _respository = new EmployeeRepository(db);
            List<EmployeeEntity> _list = _respository.GetListForDropDownList();
            if (_list.Count == 0)
                _list.Insert(0, new EmployeeEntity() { Id = 0, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
            else
                _list.Insert(0, new EmployeeEntity() { Id = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });

            ViewBag.LineManagerID = new SelectList(_list, "Id", "Name", selected_value);
        }
        public void PopulateGradesToDropDownList(int? selected_value)
        {
            LS_tblGradeRepository _respository = new LS_tblGradeRepository(db);
            List<LS_tblGradeViewModel> _list = _respository.GetList();
            if (_list.Count == 0)
                _list.Insert(0, new LS_tblGradeViewModel() { LSGradeID = 0, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
            else
                _list.Insert(0, new LS_tblGradeViewModel() { LSGradeID = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            ViewBag.LSGradeID = new SelectList(_list, "LSGradeID", "Name", selected_value);
        }

        public void PopulateChangeStatusToDropDownList(int? selected_value)
        {
            LS_tblChangeStatusRepository _respository = new LS_tblChangeStatusRepository(db);
            List<ChangeStatusEntity> _list = _respository.GetListForDropDownList();
            if (_list.Count == 0)
                _list.Insert(0, new ChangeStatusEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
            else
                _list.Insert(0, new ChangeStatusEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            ViewBag.LSChangeStatusID = new SelectList(_list, "Id", "Name", selected_value);
        }
        public void PopulateLocationsToDropDownList(int? selected_value)
        {
            LS_tblLocationRepository _respository = new LS_tblLocationRepository(db);
            List<LocationEntity> _list = _respository.GetListForDropDownList(LanguageId);
            if (_list.Count == 0)
                _list.Insert(0, new LocationEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
            else
                _list.Insert(0, new LocationEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });

            ViewBag.LSLocationID = new SelectList(_list, "Id", "Name", selected_value);
        }

        public void PopulatePositionsToDropDownList(int? selected_value)
        {
            LS_tblPositionRepository _respository = new LS_tblPositionRepository(db);
            List<PositionEntity> _list = _respository.GetListForDropDownList(LanguageId);
            if (_list.Count == 0)
                _list.Insert(0, new PositionEntity { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
            else
                _list.Insert(0, new PositionEntity { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            ViewBag.LSPositionID = new SelectList(_list, "Id", "Name", selected_value);
        }


        #region Company - Department - TeamGroup--------------------------------------------------------------------------
        public List<CompanyEntity> PopulateCompaniesToDropDownList(int? selected_value)
        {
            LS_tblCompanyRepository _respository = new LS_tblCompanyRepository(db);
            List<CompanyEntity> _list = _respository.GetListForDropDownList();
            if (_list.Count == 0)
                _list.Insert(0, new CompanyEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
            else
                _list.Insert(0, new CompanyEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });

            ViewBag.LSCompanyID = new SelectList(_list, "Id", "Name", selected_value);
            return _list;
        }

        //public List<Level1Entity> PopulateDepartmentsToDropDownList(int? LSCompanyID, int? selected_value)
        //{
        //    //LS_tblLevel1Repository _respository = new LS_tblLevel1Repository(db);
        //    List<Level1Entity> _list = new List<Level1Entity>();
        //    //if (LSCompanyID != null && LSCompanyID > 0)
        //    //{                
        //    //    _list = _respository.GetListByCompanyIDForDropDownList(LSCompanyID);
        //    //    if (_list.Count == 0)
        //    //        _list.Insert(0, new Level1Entity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
        //    //    else
        //    //        _list.Insert(0, new Level1Entity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });

        //    //    ViewBag.LSLevel1ID = new SelectList(_list, "Id", "Name", selected_value);
        //    //}
        //    //else
        //    //{
        //    //    _list.Insert(0, new Level1Entity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
        //    //    ViewBag.LSLevel1ID = new SelectList(_list, "Id", "Name", null);
        //    //}
        //    return _list;
        //}

        public void PopulateSectionsToDropDownList(int? LSLevel1ID, int? selected_value)
        {
            //LS_tblLevel2Repository _respository = new LS_tblLevel2Repository(db);
            //List<Level2Entity> _list = new List<Level2Entity>();
            //if (LSLevel1ID != null && LSLevel1ID > 0)
            //{
            //    _list = _respository.GetListByDepartmentIDForDropDownList(LSLevel1ID);
            //    if (_list.Count == 0)
            //        _list.Insert(0, new Level2Entity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
            //    else
            //        _list.Insert(0, new Level2Entity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });

            //    ViewBag.LSLevel2ID = new SelectList(_list, "Id", "Name", selected_value);
            //}
            //else
            //{
            //    _list.Insert(0, new Level2Entity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
            //    ViewBag.LSLevel2ID = new SelectList(_list, "Id", "Name", null);
            //}
        }

        //public void PopulateCompanyDepartmentSectionCascadingDropdownlist(int? selectedLSCompanyID, int? selectedselectLSLevel1ID, int? selectedselectLSLevel2ID)
        //{
            //int? LSCompanyID = null, LSLevel1ID = null;
            //List<CompanyEntity> companylst = PopulateCompaniesToDropDownList(selectedLSCompanyID);
            //if (selectedLSCompanyID != null && selectedLSCompanyID > 0)
            //    LSCompanyID = selectedLSCompanyID;
            //else
            //    LSCompanyID = companylst.First().Id;

            //List<Level1Entity> departmentlst = PopulateDepartmentsToDropDownList(LSCompanyID, selectedselectLSLevel1ID);
            //if (selectedselectLSLevel1ID != null && selectedselectLSLevel1ID > 0)
            //    LSLevel1ID = selectedselectLSLevel1ID;
            //else
            //    LSLevel1ID = departmentlst.First().Id;
            //PopulateSectionsToDropDownList(LSLevel1ID, selectedselectLSLevel2ID);
        //}
        #endregion -------------------------------------------------------------------------------------------------------

        #endregion =======================================================================================================
    }
}
