using Eagle.Common.Settings;
using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Model;
using Eagle.Model.HR;
using Eagle.Repository;
using Eagle.Repository.HR;
using Eagle.Repository.Sys.Languages;
using Eagle.Repository.SYS.Session;
using Eagle.Repository.SYS.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.SYS.Users
{
    public class UserController : BaseController
    {
        //
        // GET: /Admin/User/
        [SessionExpiration]
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Sys/Account/_Reset");
            }
            else
            {                
                return View("../Sys/Account/Index");
            }
        }
        // load danh sach grid nhom
        [SessionExpiration]
        public ActionResult _List()
        {
            UserRepository _repository = new UserRepository(db);
            List<AccountViewModel> sources = new List<AccountViewModel>();
            int moduleId = 1;
            sources = _repository.Search(UserName, moduleId, (FAdm == 1)?true:false).ToList();
            return PartialView("../Sys/Account/_List", sources);
        }
        public ActionResult _Create()
        {
            AccountCreateModel acc = new AccountCreateModel();
            return PartialView("../Sys/Account/_Create", acc);
        }
        public ActionResult _Edit(int id)
        {
            UserRepository _repository = new UserRepository(db);
            AccountCreateModel model = _repository.FindEditModel(id);
            ViewBag.PasswordVis = false;
            return PartialView("../Sys/Account/_Create", model);
        }
        
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            string errorMessage = "";
            UserRepository _repository = new UserRepository(db);
            if (_repository.Delete(id, out errorMessage))
            {
                return Content("success");
            }
            else
            {
                return Content(errorMessage);
            }
        }

        //[HttpPost]
        //public ActionResult ResetPassword(string email)
        //{
        //    UserRepository _repository = new UserRepository(db);

        //    AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
        //    string errorMessage = "";
        //    if (acc != null)
        //    {
        //        model.Editer = acc.UserName;
        //        UserRepository _repository = new UserRepository(db);
        //        if (ModelState.IsValid)
        //        {
        //            if (_repository.Update(model, out errorMessage))
        //            {
        //                return Content("success");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        errorMessage = "Time out error!";
        //        return _Error(model, errorMessage);
        //    }
        //    var errors = ModelState.Values.SelectMany(v => v.Errors);
        //    foreach (var modelError in errors)
        //    {
        //        errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
        //    }
        //    return _Error(model, errorMessage, true);
        //}


        [HttpPost]
        public ActionResult Update(AccountCreateModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                model.Editer = acc.UserName;
                UserRepository _repository = new UserRepository(db);
                if (ModelState.IsValid)
                {
                    if (_repository.Update(model, out errorMessage))
                    {
                        return Content("success");
                    }
                }
            }
            else
            {
                errorMessage = "Time out error!";
                return _Error(model, errorMessage);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _Error(model, errorMessage, true);
        }
        [HttpPost]
        public ActionResult Save(AccountCreateModel accountModel)
        {
            //Nếu tồn tại session account thì tiến hành thêm account
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                UserRepository _repository = new UserRepository(db);
                if (ModelState.IsValid && accountModel.EmpID != null)
                {
                    SYS_tblUserAccount accountAdd = new SYS_tblUserAccount()
                    {
                        UserName = accountModel.UserName,
                        Password = accountModel.Password,
                        EmpID = accountModel.EmpID,
                        FromDate = accountModel.FromDate,
                        ToDate = accountModel.ToDate,
                        Creater = acc.UserName,
                        FAdm = accountModel.FAdm == true ? 1 : 0,
                        FLock = accountModel.FLock == true ? 1 : 0,
                        IsLDAP = accountModel.IsLDAP ? 1 : 0,
                        LockDate = accountModel.LockDate,
                        Editer = acc.UserName
                    };
                    List<string> errorMessageArr = new List<string>();
                    bool bResult = _repository.AddAccount(accountAdd, out errorMessageArr);
                    if (bResult)
                    {
                        return Content("success");
                    }
                    else
                    {
                        foreach (var error in errorMessageArr)
                        {
                            errorMessage += "&lt;br /&gt;" + error;
                        }
                    }

                }
            }
            //thông báo lỗi
            var errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            if (accountModel.EmpID == null)
            {
                errorMessage += "&lt;br /&gt;" + Eagle.Resource.LanguageResource.SelectEmployee;
            }
            return _Error(accountModel, errorMessage);
        }

        public ActionResult _Error(AccountCreateModel acc, string ErrorMessage, bool EditMode = false)
        {
           
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            ViewBag.PasswordVis = !EditMode;

            if (acc == null)
                acc = new AccountCreateModel();
            return PartialView("../Sys/Account/_Create", acc);
        }

        /// <summary>
        /// Popup nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult _PopupEmployeePartial()
        {
            return PartialView("../Sys/Account/_PopupEmployeePartial");
        }
        public ActionResult _SearchAreasPartial()
        {
            return PartialView("../Sys/Account/_SearchAreasPartial");
        }

        public ActionResult _SearchResultsForPopup(string EmpCode, string FullName, int? LSCompanyID, bool? Active, int? moduleId)
        {
            // nếu gọi bằng ajax thì show thông báo không tìm thấy kết quả
            if (!Request.IsAjaxRequest())
            {
                ViewBag.FirstRequest = true;
            }
            UserRepository _repository = new UserRepository(db);
            //Tìm trong db
            List<EmployeeViewModel> Employeelst = new List<EmployeeViewModel>();
            Employeelst = _repository.FindEmployee(EmpCode, FullName, LSCompanyID, Active, UserName, moduleId, (FAdm == 1) ? true : false);
            return PartialView("../Sys/Account/_SearchResultsForPopup", Employeelst);
        }

        #region USER  ------------------------------------------------------------------------------------------------------
        public ActionResult Login(string desiredUrl)
        {
            if (Session[SettingKeys.AccountInfo] == null)
            {
                var url = Request.Url.AbsoluteUri;
                if (url.Contains("?desiredUrl="))
                {
                    url = url.Substring(url.IndexOf("?desiredUrl=") + 11);
                    ViewBag.DesiredUrl = url;
                }

                HttpCookie userInfo = Request.Cookies["userInfo"];
                LoginViewModel log = new LoginViewModel() { RememberMe = true, Password = "", UserName = "" };
                string username = "";
                if (userInfo != null)
                {
                    try
                    {
                        username = userInfo["username"].ToString();
                        if (username != "")
                        {
                            log.RememberMe = true;
                            log.UserName = username;
                            string password = StringUtils.DecodePassword(userInfo["password"].ToString());
                            log.Password = password;
                        }
                        Session["SessionId"] = System.Web.HttpContext.Current.Session.SessionID;
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
                ViewBag.LanguageCode = CommonRepository.PopulateLanguageTypes(null, false);
                return View("../Sys/Users/Login", log);
            }
            else
            {
                Response.Redirect("/Admin/Home/Index");
                return null;
            }
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            string UserName = model.UserName;
            string Password = model.Password;
            string DesiredUrl = model.DesiredUrl;
            string SelectedLanguageCode = model.LanguageCode;

            //Remember Cookies
            if (model.RememberMe == true)
            {
                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo.HttpOnly = true;
                userInfo["username"] = UserName;
                userInfo["password"] = StringUtils.EncodePassword(Password);
                userInfo.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(userInfo);
                Request.Cookies["userInfo"].Expires = DateTime.Now.AddDays(30);
            }
            else
            {
                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo["username"] = "";
                userInfo["password"] = "";
                Response.Cookies.Add(userInfo);
            }

            //bool chk = CheckLogin(userName, password);
            bool chk = UserRepository.CheckLogin(UserName, Password, SelectedLanguageCode);
            if (chk)
            {

                if (!string.IsNullOrEmpty(DesiredUrl))
                    Response.Redirect(DesiredUrl);
                else
                {
                    //Response.Redirect("/Admin/");
                    Response.Redirect("/Admin/Home/Index");
                }
                return null;
            }
            else
            {
                ViewBag.Title = Eagle.Resource.LanguageResource.Login;
                ViewBag.Message = Eagle.Resource.LanguageResource.LoginFailedNotification;
                ViewBag.LanguageCode = LanguageRepository.PopulateActiveLanguages(SelectedLanguageCode);                
                return View("../Sys/Users/Login");
            }

        }
        public ActionResult LogOff(string desiredUrl)
        {
            Session[SettingKeys.AccountInfo] = null;
            Session[SettingKeys.UserId] = null;
            Session[SettingKeys.MenuList] = null;
            Session["CandidateSelected"] = null;
            Session["ApprovePermission"] = null;
            if (string.IsNullOrEmpty(desiredUrl))
            {
                Response.Redirect("/Admin/Login");
                return null;
            }
            else
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + desiredUrl);
                return null;
            }
        }

        //private bool CheckLogin(string userName, string password)
        //{
        //    try
        //    {
        //        AccountViewModel acc = UserRepository.GetDetailsByUserNameAndPassword(userName, password);
        //        if (acc != null)
        //        {
        //            Session[SettingKeys.AccountInfo] = acc;
        //            Session[SettingKeys.UserId] = acc.UserID;
        //            Session[SettingKeys.UserName] = acc.UserName;

        //            EmployeeViewModel Emp = new EmployeeViewModel();
        //            int? id = acc.EmpId;
        //            if (id != null)
        //            {
        //                Emp = EmployeeRepository.GetDetails(id, LanguageId);
        //                if (Emp != null)
        //                {
        //                    Session[SettingKeys.EmpInfo] = Emp;
        //                    Session[SettingKeys.EmpId] = Emp.EmpID;
        //                    Session[SettingKeys.EmpCode] = Emp.EmpCode;
        //                    acc.EmployeeInfo = Emp;
        //                }
        //            }
        //            else
        //            {
        //                Emp = new EmployeeViewModel() { FullName = acc.UserName };
        //                Session[SettingKeys.EmpInfo] = Emp;
        //                Session[SettingKeys.EmpId] = Emp.EmpID;
        //                Session[SettingKeys.EmpCode] = Emp.EmpCode;
        //            }

        //            // lấy tất cả các menu / Module mà user có quyền truy cập FuntionID level1
        //            string Lang = LanguageId == 124 ? "en-US" : "vi-VN";
        //            List<SYS_tblFunctionPermissionViewModel> moduleList = db.sp_clsCommon("ModuleList", Lang, "", 0, 0, 1, userName).Select(p => new SYS_tblFunctionPermissionViewModel() { Url = p.Url, FunctionID = p.FunctionID, FunctionNameE = p.FunctionNameE, Rank = p.Rank, Parent = p.Parent, Tooltips = p.Tooltips, FView = (p.FView != 0 & p.FView != null), FEdit = (p.FEdit != 0 && p.FEdit != null), FDelete = (p.FDelete != 0 && p.FDelete != null), FExport = (p.FExport != 0 && p.FExport != null) }).ToList();
        //            moduleList.Add(new SYS_tblFunctionPermissionViewModel() { Url = "/Admin/ChangePassword" });
        //            Session[SettingKeys.MenuList] = moduleList;
        //            Session[SettingKeys.ScopeTypeId] = 1;

        //            //Session["PortalId"] = 1;
        //            //PortalInfo portal_entity = PortalRespository.GetPortal(Convert.ToInt32(Session["PortalId"]));
        //            //Session["PortalInfo"] = portal_entity;
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string strErr = ex.Message;
        //        return false;
        //    }
        //}
        #endregion USER  ---------------------------------------------------------------------------------------------------
    }
}
