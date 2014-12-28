using Eagle.Common;
using Eagle.Common.Entities;
using Eagle.Common.Extensions;
using Eagle.Common.Helpers;
using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository;
using Eagle.Repository.HR;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Eagle.Model;
using Eagle.Model.Common;
using Eagle.Model.HR;
using Eagle.Model.Report.HR.Department;
using Eagle.Model.SYS;
using Eagle.Repository.SYS.Session;
using Eagle.Model.LS;
using System.Xml;
using Eagle.Common.Data;
using Eagle.Common.Settings;
using Eagle.Model.SYS.Permission;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    [HandleError]
    public class EmployeeController : BaseController
    {
        // GET: /Admin/Employee/
        [SessionExpiration]
        public ActionResult Index(int? ID)
        {             
            if (ID != null)
            {
                EmployeeEditModel model = new EmployeeEditModel();
                EmployeeRepository _repository = new EmployeeRepository(db);
                model = _repository.GetDetailsById(ID, LanguageId);
                CreateViewBagReference(model, false);
                return View("../HR/Employee/EditInfo", model);
            }
            else
            {
                //PopulateCompanyDepartmentSectionCascadingDropdownlist(null, null, null);
                PopulateLocationsToDropDownList(null);
                PopulatePositionsToDropDownList(null);
                return View("../HR/Employee/Index");
            } 
        }
        

        //Dich vu chia se tin mung ngay sinh nhat theo type 1: today, 7:week, 30:1 thang
        public ActionResult GetEmpListByBirthday(int? Month, int? Type)
        {
            List<EmployeeViewModel> lst = EmployeeRepository.LoadEmpListByBirthday(Month, Type, LanguageId);
            return PartialView("../HR/Employee/_BirthdayList", lst);
        }


        ////Thong tin nhan vien thuc tap theo type 1: today, 7:week, 60: 2 thang
        //public ActionResult GetProbationaryList(int? Type)
        //{
        //    List<EmployeeViewModel> lst = EmployeeRepository.GetProbationaryList(Type, LanguageId);
        //    return PartialView("../HR/Employee/_ProbationaryList", lst);
        //}


        [SessionExpiration]
        [HttpGet]
        public ActionResult List(int? LSCompanyID, int? LSLocationID, int? LSPositionID, string Code, string SearchText, bool? Active)
        {
            int? ModuleId = 2;
            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);

            List<EmployeeViewModel> list = new List<EmployeeViewModel>();           
            EmployeeRepository _repository = new EmployeeRepository(db);
            list = _repository.Search(LSCompanyID, LSLocationID, LSPositionID, Code, SearchText, Active, ModuleId, UserName, isAdmin, LanguageId).ToList();
            
            return PartialView("../HR/Employee/_List", list);
        }

        [AjaxSessionActionFilter]
        public ActionResult GetList(int? EmpID, int? ModuleID)
        {
            EmployeeEditModel model = new EmployeeEditModel();           
            EmployeeRepository _repository = new EmployeeRepository(db);
            model = _repository.GetDetailsById(EmpID, LanguageId);
            CreateViewBagReference(model, false);
           
            return PartialView("../HR/Employee/_Edit", model);
        }      

        [AjaxSessionActionFilter]
        [HttpGet]
        public ActionResult GetDetailInfo(int? EmpID)
        {
            EmployeeViewModel emp_entity = new EmployeeViewModel();
            if (EmpID != null)
                emp_entity = EmployeeRepository.GetDetailInfo(EmpID, LanguageId);
            else
                emp_entity = (EmployeeViewModel)Session[SettingKeys.EmpInfo];
            return PartialView("../HR/Employee/_Details", emp_entity);
        }

        [SessionExpiration]
        public ActionResult Create(EmployeeEditModel model)
        {
            //ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            //ModelBinders.Binders.Add(typeof(DateTime?), new DateTimeModelBinder());

            int MasterDataID_Bank = 18;
            FunctionPermissionRepository _respository = new FunctionPermissionRepository(db);
            PermissionSettingViewModel bank_permission_entity = _respository.CheckMasterDataPermission(UserId, MasterDataID_Bank);
            if (bank_permission_entity.Edit == true)     
                ViewBag.btnAddBank = "hidden";

            int MasterDataID_BankBranch = 19;
            PermissionSettingViewModel bank_branch_permission_entity = _respository.CheckMasterDataPermission(UserId, MasterDataID_BankBranch);
            if (bank_branch_permission_entity.Edit == true)  
                ViewBag.btnAddBankBranch = "hidden";

            CreateViewBagReference(model);  
            return PartialView("../HR/Employee/_Create", model);
        }
             

        [HttpGet]
        [SessionExpiration]
        public ActionResult EditDetails(int ID)
        {
            EmployeeEditModel model = new EmployeeEditModel();          
            EmployeeRepository _repository = new EmployeeRepository(db);
            model = _repository.GetDetailsById(ID, LanguageId);
            CreateViewBagReference(model, false);           
            return PartialView("../HR/Employee/_Reset", model);
        }

        [HttpGet]
        [SessionExpiration]
        public ActionResult Edit(int? ID)
        {
            int MasterDataID_Bank = 18;
            FunctionPermissionRepository _respository = new FunctionPermissionRepository(db);
            PermissionSettingViewModel bank_permission_entity = _respository.CheckMasterDataPermission(UserId, MasterDataID_Bank);
            if (bank_permission_entity.Edit == true)     
                ViewBag.btnAddBank = "hidden";

            int MasterDataID_BankBranch = 19;
            PermissionSettingViewModel bank_branch_permission_entity = _respository.CheckMasterDataPermission(UserId, MasterDataID_BankBranch);
            if (bank_branch_permission_entity.Edit == true)     
                ViewBag.btnAddBankBranch = "hidden";

            EmployeeEditModel model = new EmployeeEditModel();          
            EmployeeRepository _repository = new EmployeeRepository(db);
            model = _repository.GetDetailsById(ID, LanguageId);
            CreateViewBagReference(model, false);          
            return PartialView("../HR/Employee/_Edit", model);
        }

        [HttpGet]
        [SessionExpiration]
        public ActionResult EditProfile(int? ID)
        {
            int MasterDataID_Bank = 18;
            FunctionPermissionRepository _respository = new FunctionPermissionRepository(db);
            PermissionSettingViewModel bank_permission_entity = _respository.CheckMasterDataPermission(UserId, MasterDataID_Bank);
            if (bank_permission_entity.Edit == true) 
                ViewBag.btnAddBank = "hidden";

            int MasterDataID_BankBranch = 19;
            PermissionSettingViewModel bank_branch_permission_entity = _respository.CheckMasterDataPermission(UserId, MasterDataID_BankBranch);
            if (bank_branch_permission_entity.Edit == true) 
                ViewBag.btnAddBankBranch = "hidden";

            EmployeeEditModel model = new EmployeeEditModel();
            EmployeeRepository _repository = new EmployeeRepository(db);
            if (ID != null)
                model = _repository.GetDetailsById(ID, LanguageId);
            else
            {
                if (CurrentEmpId!=null)
                    model = _repository.GetDetailsById(CurrentEmpId, LanguageId);
            }
                
            CreateViewBagReference(model, false);
            return View("../HR/Employee/_Edit", model);
        }

        [HttpPost]
        [SessionExpiration]
        public ActionResult Insert(EmployeeEditModel add_model)
        {
            //Thread.Sleep(1000);            
            bool flag = false;
            string message = string.Empty;
            
            try
            {
                if (String.IsNullOrEmpty(add_model.FirstName))
                    ModelState.AddModelError("FirstName", Eagle.Resource.LanguageResource.Required);
                if (String.IsNullOrEmpty(add_model.LastName))
                    ModelState.AddModelError("LastName", Eagle.Resource.LanguageResource.Required);

                if (ModelState.IsValid)
                {
                    //string FileName = string.Empty;
                    //if (FileUpload != null && FileUpload.ContentLength > 0)
                    //{                            
                    //    FolderRepository folder_respository = new FolderRepository(db);
                    //    string UploadPath = "~" + folder_respository.GetFolderPathByFolderKey("UPLOAD_EMPLOYEE_IMAGE_DIR");
                    //    FileName = FileUtils.UploadFile(FileUpload, UploadPath);
                    //}

                    DateTime? JoinDate = add_model.JoinDate;
                    DateTime? StartDate = add_model.StartDate;

                    if (JoinDate.HasValue && StartDate.HasValue)
                    {
                        if (DateTime.Compare(JoinDate.Value, StartDate.Value) > 0)
                        {
                            flag = false;
                            message = Eagle.Resource.LanguageResource.ValidateJoinDateStartDate;
                            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                        }
                    }

                    EmployeeRepository _repository = new EmployeeRepository(db);
                    flag = _repository.Add(add_model, out message);
                    if (flag == true)
                    {
                        bool returnValue = AddEmpToWorking(add_model);
                        if (returnValue)
                            flag = true;
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
                message = ex.ToString();
                flag = false;
            }
                         
            //return RedirectToAction("Index"); 
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        [SessionExpiration]
        public bool AddEmpToWorking(EmployeeEditModel add_model)
        { 
            WorkingRecordViewModel model = new WorkingRecordViewModel();
            model.EmpID = add_model.EmpID;
            model.LSChangeStatusID = 1;
            model.EffectiveDate = (DateTime)add_model.JoinDate;
            model.LineManagerID = add_model.LineManagerID;
            model.LSCompanyID = add_model.LSCompanyID;
            model.LSLocationID = add_model.LSLocationID;
            model.LSPositionID = add_model.LSPositionID;
            model.SignerEmpID = add_model.EmpID;
            model.Creater = CurrentEmpId;

            bool result = false;
            string message = string.Empty;
            WorkingRecordRespository working_entity = new WorkingRecordRespository(db);
            result = working_entity.Add(model, out message);
            return result;
        }

        [HttpPost]
        [SessionExpiration]
        public ActionResult Update(EmployeeEditModel edit_model)
        {
            //Thread.Sleep(1000);            
            bool flag = false;
            string message = string.Empty;          
            try
            {
                if (ModelState.IsValid)
                {
                    //string FileName = string.Empty, OriginalFileName = string.Empty;
                    //OriginalFileName =  edit_model.FileImage;

                    //if (FileUpload != null && FileUpload.ContentLength > 0)
                    //{
                    //    FolderRepository folder_respository = new FolderRepository(db);
                    //    string UploadPath = "~" + folder_respository.GetFolderPathByFolderKey("UPLOAD_EMPLOYEE_IMAGE_DIR");
                    //    string Original_File_Path = UploadPath + "/" + OriginalFileName;
                    //    FileName = FileUtils.UploadFileAndRemoveOldFile(FileUpload, Original_File_Path, UploadPath);
                    //}else
                    //    FileName = OriginalFileName;               

                    DateTime? JoinDate = edit_model.JoinDate;
                    DateTime? StartDate = edit_model.StartDate;

                    if (JoinDate.HasValue && StartDate.HasValue)
                    {
                        if (DateTime.Compare(JoinDate.Value, StartDate.Value) > 0)
                        {
                            flag = false;
                            message = Eagle.Resource.LanguageResource.ValidateJoinedSignedDate;
                            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                        }
                    }

                    EmployeeRepository _repository = new EmployeeRepository(db);
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
                message = ex.ToString();
                flag = false;
            }
          
            //return RedirectToAction("Index");
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxSessionActionFilter]
        public ActionResult UpdateStatus(int id)
        {
            string message = string.Empty;
            bool flag = false;
            flag = EmployeeRepository.UpdateStatus(id, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        #region Pick data from grid on popup ============================================================================
        [SessionExpiration]
        public PartialViewResult GetBriefEmpInfoByEmpID(int? EmpID)
        {            
            EmployeeViewModel entity = new EmployeeViewModel();
            if (EmpID != null && EmpID != 0)
            {
                EmployeeRepository _repository = new EmployeeRepository(db);
                entity = _repository.GetBrief(EmpID, LanguageId);
                CurrentEmp = entity;
                CurrentEmpId = entity.EmpID;
            }
            return PartialView("../HR/Employee/_BriefEmpInfo", entity);          
        }

        [SessionExpiration]
        public PartialViewResult GetBriefEmpInfo()
        {            
            if (Session[SettingKeys.EmpInfo] != null)
            {
                EmployeeViewModel emp_entity = (EmployeeViewModel)Session[SettingKeys.EmpInfo];
                return PartialView("../HR/Employee/_BriefEmpInfo", emp_entity);
            }
            else
            {
                AccountViewModel account_entity = (AccountViewModel)Session[SettingKeys.AccountInfo];
                return PartialView("../SYS/Account/_BriefAccountInfo", account_entity);
            }           
        }

        [SessionExpiration]
        public PartialViewResult GetEmployeeInfo()
        {           
            EmployeeViewModel entity = new EmployeeViewModel();
            if (Session[SettingKeys.EmpInfo] != null)
            {
                entity = (EmployeeViewModel)Session[SettingKeys.EmpInfo];
                CurrentEmpId = entity.EmpID;
            }
            return PartialView("../HR/Employee/_EmployeeInfo", entity);
        }

        [SessionExpiration]
        public PartialViewResult GetEmployeeInfoByEmpID(int? EmpID)
        {            
            EmployeeViewModel entity = new EmployeeViewModel();
            if (EmpID != null && EmpID != 0)
            {
                entity = EmployeeRepository.GetDetails(EmpID, LanguageId);
                CurrentEmp = entity;
                CurrentEmpId = entity.EmpID;
            }
            else
                entity = (EmployeeViewModel)Session[SettingKeys.EmpInfo];

            return PartialView("../HR/Employee/_EmployeeInfo", entity);           
        }

        [SessionExpiration]
        public PartialViewResult GetTopEmpInfo(int? EmpID)
        {          
            EmployeeViewModel entity = new EmployeeViewModel();
            if (EmpID != null && EmpID != 0)
            {
                entity = EmployeeRepository.GetDetails(EmpID, LanguageId);
                CurrentEmp = entity;
                CurrentEmpId = entity.EmpID;
            }
            else
                entity = (EmployeeViewModel)Session[SettingKeys.EmpInfo];

            return PartialView("../HR/Employee/_EditTopEmpInfo", entity);           
        }
        

        public ActionResult _PopupEmployeeGrid()
        {
            return PartialView("../HR/Employee/_PopupEmployeeGrid");
        }

        [SessionExpiration]
        public ActionResult _SearchResultsForPopup(string EmpCode, string FullName, int? LSCompanyID, bool? Active, int? ModuleId)
        {
            // nếu gọi bằng ajax thì show thông báo không tìm thấy kết quả
            if (!Request.IsAjaxRequest())
                ViewBag.FirstRequest = true;
            EmployeeRepository _repository = new EmployeeRepository(db);
            List<EmployeeViewModel> Employeelst = new List<EmployeeViewModel>();
            Employeelst = _repository.FindEmployee(EmpCode, FullName, LSCompanyID, Active, UserName, ModuleId, FAdm == 1);
            return PartialView("../HR/Employee/_SearchResultsForPopup", Employeelst);
        }

        #endregion ======================================================================================================

        #region Gender -------------------------------------------------------------------------------------------------------------
        public static List<SelectListItem> GenderSelectList
        {
            get
            {
                List<SelectListItem> genders = new List<SelectListItem>();

                foreach (Gender gender in Enum.GetValues(typeof(Gender)))
                {
                    genders.Add(new SelectListItem { Text = gender.ToString(), Value = gender.ToString("D"), Selected = false });
                }

                return genders;
            }
        }
        public void PopulateGendersToDropDownList(int? selected_value)
        {
            //C1:          
            ViewBag.Gender = CommonController.PopulateGenderList(selected_value, LanguageId);

            //C2:
            //ViewBag.Gender = GenderClass.GenderSelectList;
        }
        #endregion -----------------------------------------------------------------------------------------------------------------


        #region Blood Type ----------------------------------------------------------------------------------------------
        public void PopulateBloodTypesToDropDownList(string selected_value)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "---" + Eagle.Resource.LanguageResource.Select + "---", Value = "" });
            if (LanguageId == 124)
            {
                list.Add(new SelectListItem { Text = "Group A", Value = "A" });
                list.Add(new SelectListItem { Text = "Group B", Value = "B" });
                list.Add(new SelectListItem { Text = "Group AB", Value = "AB" });
                list.Add(new SelectListItem { Text = "Group O", Value = "O" });
            }
            else
            {
                list.Add(new SelectListItem { Text = "Nhóm A", Value = "A" });
                list.Add(new SelectListItem { Text = "Nhóm B", Value = "B" });
                list.Add(new SelectListItem { Text = "Nhóm AB", Value = "AB" });
                list.Add(new SelectListItem { Text = "Nhóm O", Value = "O" });
            }
            ViewBag.BloodType = new SelectList(list, "Value", "Text", selected_value);
        }
        #endregion ------------------------------------------------------------------------------------------------------

        #region Company - Department - TeamGroup-------------------------------------------------------------------------
        public List<CompanyEntity> PopulateCompaniesToDropDownList(int? selected_value)
         {
             LS_tblCompanyRepository _respository = new LS_tblCompanyRepository(db);
             List<CompanyEntity> _list = _respository.GetListForDropDownList();
             if (_list.Count == 0)
                 _list.Insert(0, new CompanyEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
             else
                 _list.Insert(0, new CompanyEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });

             ViewBag.selectLSCompanyID = new SelectList(_list, "Id", "Name", selected_value);
             return _list;
         }
        #endregion ------------------------------------------------------------------------------

         #region EMPLOYEE REFERENCES -------------------------------------------------------
         [HttpPost]
         public JsonResult Search(int? LSCompanyID, int? LSLocationID, int? LSPositionID, string Code, string SearchText, bool? Active)
         {
             int? ModuleId = 2;
             if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                 ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);

             List<EmployeeViewModel> list = new List<EmployeeViewModel>();
             var account = (AccountViewModel)Session[SettingKeys.AccountInfo];
             if (account != null)
             {
                 EmployeeRepository _repository = new EmployeeRepository(db);
                 list = _repository.Search(LSCompanyID, LSLocationID, LSPositionID, Code, SearchText, Active, ModuleId, UserName, isAdmin, LanguageId).ToList();
             }

             JavaScriptSerializer serializer = new JavaScriptSerializer();
             string s = serializer.Serialize(list);
             return Json(s, JsonRequestBehavior.AllowGet);
         }

         protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
         {
             return new JsonResult()
             {
                 Data = data,
                 ContentType = contentType,
                 ContentEncoding = contentEncoding,
                 JsonRequestBehavior = behavior,
                 MaxJsonLength = Int32.MaxValue
             };
         }


         public JsonResult GetDetails(int ID)
         {
             EmployeeRepository _repository = new EmployeeRepository(db);
             EmployeeEditModel entity = _repository.GetDetailsById(ID, LanguageId);
             JavaScriptSerializer serializer = new JavaScriptSerializer();
             string s = serializer.Serialize(entity);
             return Json(s, JsonRequestBehavior.AllowGet);
         }

         [HttpGet]
         public JsonResult ValidateEmpCode(string EmpCode)
         {
             bool flag = EmployeeRepository.CheckExistCode(EmpCode);
             bool result = (flag == true) ? false : true;
             return Json(result, JsonRequestBehavior.AllowGet);
         }

         [HttpGet]
         public JsonResult ValidateCode(string NewCode, string InitialCode)
         {
             bool result = true;
             if (NewCode != InitialCode)
             {
                 bool flag = EmployeeRepository.CheckExistCode(NewCode);
                 if (flag == true)
                     result = false;
             }
             return base.Json(result, JsonRequestBehavior.AllowGet);
         }

        [SessionExpiration]
         private void CreateViewBagReference(EmployeeEditModel model, bool isNew = true)
         {
             if (isNew == true)
                 model.EmpCode = EmployeeRepository.GenerateEmployeeCode(10);
             else
                 model.EmpCode = model.EmpCode;

             //Country =============================================================================================
             string BornLSCountryName = string.Empty, PLSCountryName = string.Empty, TLSCountryName = string.Empty;
             LS_tblCountryRepository country_repository = new LS_tblCountryRepository(db);

             LS_tblCountryViewModel born_entity = country_repository.GetDetails(model.BornLSCountryID);
             if (born_entity != null) BornLSCountryName = born_entity.Name;
             ViewBag.BornLSCountryName = BornLSCountryName;
             ViewBag.BornLSCountryID = model.BornLSCountryID;

             LS_tblCountryViewModel pls_entity = country_repository.GetDetails(model.PLSCountryID);
             if (pls_entity != null) PLSCountryName = pls_entity.Name;
             ViewBag.PLSCountryName = PLSCountryName;
             ViewBag.PLSCountryID = model.PLSCountryID;

             LS_tblCountryViewModel tls_entity = country_repository.GetDetails(model.TLSCountryID);
             if (tls_entity != null) TLSCountryName = tls_entity.Name;
             ViewBag.TLSCountryName = TLSCountryName;
             ViewBag.TLSCountryID = model.TLSCountryID;

             //Province ===============================================================================================
             string BornLSProvinceName = string.Empty, PLSProvinceName = string.Empty, TLSProvinceName = string.Empty;
             LS_tblProvinceRepository province_repository = new LS_tblProvinceRepository(db);

             LS_tblProvinceViewModel born_province_entity = province_repository.GetDetails(model.BornLSProvinceID);
             if (born_province_entity != null) BornLSProvinceName = born_province_entity.Name;
             ViewBag.BornLSProvinceName = BornLSProvinceName;
             ViewBag.BornLSProvinceID = model.BornLSProvinceID;

             LS_tblProvinceViewModel pls_province_entity = province_repository.GetDetails(model.PLSProvinceID);
             if (pls_province_entity != null) PLSProvinceName = pls_province_entity.Name;
             ViewBag.PLSProvinceName = PLSProvinceName;
             ViewBag.PLSProvinceID = model.PLSProvinceID;

             LS_tblProvinceViewModel tls_province_entity = province_repository.GetDetails(model.TLSProvinceID);
             if (tls_province_entity != null) TLSProvinceName = tls_province_entity.Name;
             ViewBag.TLSProvinceName = TLSProvinceName;
             ViewBag.TLSProvinceID = model.TLSProvinceID;

             //District ================================================================================================
             string BornLSDistrictName = string.Empty, PLSDistrictName = string.Empty, TLSDistrictName = string.Empty;
             LS_tblDistrictRepository district_repository = new LS_tblDistrictRepository(db);

             LS_tblDistrictViewModel born_district_entity = district_repository.GetDetails(model.BornLSDistrictID);
             if (born_district_entity != null) BornLSDistrictName = born_district_entity.Name;
             ViewBag.BornLSDistrictName = BornLSDistrictName;
             ViewBag.BornLSDistrictID = model.BornLSDistrictID;

             LS_tblDistrictViewModel pls_district_entity = district_repository.GetDetails(model.PLSDistrictID);
             if (pls_district_entity != null) PLSDistrictName = pls_district_entity.Name;
             ViewBag.PLSDistrictName = PLSDistrictName;
             ViewBag.PLSDistrictID = model.PLSDistrictID;

             LS_tblDistrictViewModel tls_district_entity = district_repository.GetDetails(model.TLSDistrictID);
             if (tls_district_entity != null) TLSDistrictName = tls_district_entity.Name;
             ViewBag.TLSDistrictName = TLSDistrictName;
             ViewBag.TLSDistrictID = model.TLSDistrictID;
             //========================================================================================================
             PopulateBloodTypesToDropDownList(model.BloodType);
             PopulateGendersToDropDownList(model.Gender);
             PopulateMaritalToDropDownList(model.LSMaritalID);
             PopulatePositionsToDropDownList(model.LSPositionID);
         }


        [SessionExpiration]
         [HttpGet]
         public JsonResult GenerateEmpCode()
         {
             string EmpCode = EmployeeRepository.GenerateEmployeeCode(10);
             return Json(EmpCode, JsonRequestBehavior.AllowGet);
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

         public void PopulateManagersToDropDownList(int? selected_value)
         {
             EmployeeRepository _respository = new EmployeeRepository(db);
             List<EmployeeEntity> _list = _respository.GetListForDropDownList();
             if (_list.Count == 0)
                 _list.Insert(0, new EmployeeEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
             else
                 _list.Insert(0, new EmployeeEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });

             ViewBag.LineManagerID = new SelectList(_list, "Id", "Name", selected_value);
         }

         public void PopulateEthnicsToDropDownList(int? selected_value)
         {
             LS_tblEthnicRepository _respository = new LS_tblEthnicRepository(db);
             List<EthnicEntity> _list = _respository.GetListForDropDownList(LanguageId);
             if (_list.Count == 0)
                 _list.Insert(0, new EthnicEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
             else
                 _list.Insert(0, new EthnicEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
             //if (selected_value != null && selected_value > 0)
             ViewBag.LSEthnicID = new SelectList(_list, "Id", "Name", selected_value);
             //else
             //   ViewBag.LSPositionID = new SelectList(_list, "LSPositionID", "Name", _list.First());
         }

         public void PopulateReligionToDropDownList(int? selected_value)
         {
             LS_tblReligionRepository _respository = new LS_tblReligionRepository(db);
             List<ReligionEntity> _list = _respository.GetListForDropDownList(LanguageId);
             if (_list.Count == 0)
                 _list.Insert(0, new ReligionEntity() { Id = 0, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
             else
                 _list.Insert(0, new ReligionEntity() { Id = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
             ViewBag.LSReligionID = new SelectList(_list, "Id", "Name", selected_value);
         }

         public void PopulateNationalityToDropDownList(int? selected_value)
         {
             LS_tblNationalityRepository _respository = new LS_tblNationalityRepository(db);
             List<NationalityEntity> _list = _respository.GetListForDropDownList(LanguageId);
             if (_list.Count == 0)
                 _list.Insert(0, new NationalityEntity() { Id = 0, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
             else
                 _list.Insert(0, new NationalityEntity() { Id = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });

             ViewBag.LSNationalityID = new SelectList(_list, "Id", "Name", selected_value);
         }


         #region Permanent Countries  --------------------------------------------------------------------------------------------------------------------------
         public void PopulatePLSCountriesToDropDownList(int? selected_value)
         {
             LS_tblCountryRepository _respository = new LS_tblCountryRepository(db);
             List<CountryEntity> _list = _respository.GetListForDropDownList(LanguageId);
             if (_list.Count == 0)
                 _list.Insert(0, new CountryEntity() { Id = 0, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
             else
                 _list.Insert(0, new CountryEntity() { Id = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });

             ViewBag.PLSCountryID = new SelectList(_list, "Id", "Name", selected_value);
         }
         #endregion --------------------------------------------------------------------------------------------------------------------------------------------

         #region IDIssuedPlace - Province -----------------------------------------------------------------------------------------------------------------------
         public void PopulateProvinceToDropDownList(int? selected_value)
         {
             LS_tblProvinceRepository _respository = new LS_tblProvinceRepository(db);
             List<ProvinceEntity> _list = _respository.GetListForDropDownList(LanguageId);
             if (_list.Count == 0)
                 _list.Insert(0, new ProvinceEntity() { Id = 0, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
             else
                 _list.Insert(0, new ProvinceEntity() { Id = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });

             //if (selected_value != null && selected_value > 0)
             ViewBag.IDIssuedPlace = new SelectList(_list, "Id", "Name", selected_value);
             //else
             //   ViewBag.IDIssuedPlace = new SelectList(_list, "Id", "Name", _list.First());

         }
         #endregion ----------------------------------------------------------------------------------------------------------------------------------------------

         public void PopulateMaritalToDropDownList(int? selected_value)
         {
             LS_tblMaritalRepository _respository = new LS_tblMaritalRepository(db);
             List<MaritalEntity> _list = _respository.GetListForDropDownList(LanguageId);
             if (_list.Count == 0)
                 _list.Insert(0, new MaritalEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });

             //if (selected_value != null && selected_value > 0)
             ViewBag.LSMaritalID = new SelectList(_list, "Id", "Name", selected_value);
             //else
             //   ViewBag.LSMaritalID = new SelectList(_list, "Id", "Name", _list.First());
         }

         public void PopulateEducationToDropDownList(int? selected_value)
         {
             LS_tblEducationRepository _respository = new LS_tblEducationRepository(db);
             List<EducationEntity> _list = _respository.GetListForDropDownList();
             if (_list.Count == 0)
                 _list.Insert(0, new EducationEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
             else
                 _list.Insert(0, new EducationEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
             ViewBag.LSEducationID = new SelectList(_list, "Id", "Name", selected_value);
         }

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

         public void PopulateMajorsToDropDownList(int? selected_value)
         {
             LS_tblMajorRepository _respository = new LS_tblMajorRepository(db);
             List<MajorEntity> _list = _respository.GetListForDropDownList();
             if (_list.Count == 0)
                 _list.Insert(0, new MajorEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
             else
                 _list.Insert(0, new MajorEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
             ViewBag.LSMajor = new SelectList(_list, "Id", "Name", selected_value);
         }

         public void PopulateBanksToDropDownList(int? selected_value)
         {
             LS_tblBankRepository _respository = new LS_tblBankRepository(db);
             List<BankEntity> _list = _respository.GetListForDropDownList(LanguageId);
             if (_list.Count == 0)
                 _list.Insert(0, new BankEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
             else
                 _list.Insert(0, new BankEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
             ViewBag.LSBankID = new SelectList(_list, "Id", "Name", selected_value);
         }



         #endregion EMPLOYEE REFERENCES ------------------------------------------------------
         
        
         #region Employee DropdownList ==================================================================================
         /// <summary>
         /// Dùng cho viec binding du lieu cho dropdownlist autocomplete
         /// </summary>
         /// <param name="searchTerm"></param>
         /// <param name="pageSize"></param>
         /// <param name="pageNum"></param>
         /// <returns></returns>
         /// 
         [HttpGet]
         [AjaxSessionActionFilter]
         public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
         {
             EmployeeRepository _repository = new EmployeeRepository(db);
             List<AutoCompleteModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum).ToList();
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

         [HttpGet]
         [AjaxSessionActionFilter]
         public ActionResult DropdownListForSendingMail(string searchTerm, int pageSize, int pageNum)
         {
             EmployeeRepository _repository = new EmployeeRepository(db);
             List<AutoCompleteModel> sources = _repository.ListDropdownForSedingMail(searchTerm, pageSize, pageNum).ToList();
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
        
         [HttpGet]
         [AjaxSessionActionFilter]
         public ActionResult DropdownListOnSingleCode(string searchTerm, int pageSize, int pageNum, int? LSCompanyID)
         {
             EmployeeRepository _repository = new EmployeeRepository(db);
             List<AutoCompleteViewModel> sources = _repository.ListDropdownOnSingleCode(searchTerm, pageSize, pageNum, LSCompanyID, LanguageId).ToList();
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

         [HttpGet]
         [AjaxSessionActionFilter]
         public ActionResult DropdownListOnCode(string searchTerm, int pageSize, int pageNum, int? LSCompanyID)
         {
             EmployeeRepository _repository = new EmployeeRepository(db);
             List<AutoCompleteModel> sources = _repository.ListDropdownOnCode(searchTerm, pageSize, pageNum, LSCompanyID, LanguageId).ToList();
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

         [HttpGet]
         [AjaxSessionActionFilter]
         public ActionResult DropdownListOnSingleFullName(string searchTerm, int pageSize, int pageNum, int? LSCompanyID)
         {
             EmployeeRepository _repository = new EmployeeRepository(db);
             List<AutoCompleteViewModel> sources = _repository.ListDropdownOnSingleFullName(searchTerm, pageSize, pageNum, LSCompanyID, LanguageId).ToList();
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


         [HttpGet]
         [AjaxSessionActionFilter]
         public ActionResult DropdownListOnFullName(string searchTerm, int pageSize, int pageNum, int? LSCompanyID)
         {
             EmployeeRepository _repository = new EmployeeRepository(db);
             List<AutoCompleteModel> sources = _repository.ListDropdownOnFullName(searchTerm, pageSize, pageNum, LSCompanyID, LanguageId).ToList();
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


         [HttpGet]
         [AjaxSessionActionFilter]
         public ActionResult DropdownListOnFullNameDepartment(string searchTerm, int pageSize, int pageNum)
         {
             EmployeeRepository _repository = new EmployeeRepository(db);
             List<AutoCompleteModel> sources = _repository.ListDropdownOnFullNameDepartment(searchTerm, pageSize, pageNum, LanguageId).ToList();
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
        #endregion===============================================================================================================

        //[HttpGet]
        // public IEnumerable<SelectItem> GetEntity(string id)
        //{
        //    List<SelectItem> _lst = EmployeeRepository.GetEmailsForDropDownList();
        //    if (string.IsNullOrWhiteSpace(id)) return null;

         //    List<SelectItem> items = new List<SelectItem>(); 
        //    string[] idList = id.Split(new char[] { ',' });
        //    foreach (var idStr in idList)
        //    {
        //        //int idInt;
        //        //if (int.TryParse(idStr, out idInt))
        //        //{
        //        items.Add(_lst.FirstOrDefault(m => m.id == idStr));
        //        //}
        //    } 
        //    return items;
         //}
        //==================================================================================

         [HttpPost]
         public ActionResult ImportData(HttpPostedFileBase file)
         {
             DataSet ds = new DataSet();
             if (Request.Files["file"].ContentLength > 0)
             {
                 string fileName = Request.Files["file"].FileName;
                 string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);

                 if (fileExtension == ".xls" || fileExtension == ".xlsx")
                 {
                     DataTable dt = ExcelHelper.GetDataTable(fileName, "HR_tblEmp");
                     string Message = string.Empty;
                     for (int i = 0; i < dt.Rows.Count; i++)
                     {
                         EmployeeRepository.Insert(dt.Rows[i]["EmpCode"].ToString(), dt.Rows[i]["FirstName"].ToString(), dt.Rows[i]["LastName"].ToString(),
                             dt.Rows[i]["Gender"].ToString(), dt.Rows[i]["Email"].ToString(), dt.Rows[i]["Telephone"].ToString(), dt.Rows[i]["Mobile"].ToString(),
                             dt.Rows[i]["IDNo"].ToString(), dt.Rows[i]["IDIssuedDate"].ToString(), dt.Rows[i]["PAddress"].ToString(), dt.Rows[i]["TAddress"].ToString(), 
                             dt.Rows[i]["LSCompanyID"].ToString(), dt.Rows[i]["DOB"].ToString(), dt.Rows[i]["StartDate"].ToString(), dt.Rows[i]["JoinDate"].ToString(), out Message);
                     }
                 }
             }
             return View();
         }   
    }
}
