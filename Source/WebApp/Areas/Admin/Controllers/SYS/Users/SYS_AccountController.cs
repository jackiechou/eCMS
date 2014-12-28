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
using Eagle.Common.Extensions;
using Eagle.Model.HR;
using Eagle.Common.Settings;
using Eagle.Repository.SYS.Users;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class SYS_AccountController : BaseController
    {
        //
        // GET: /Admin/SYS_Account/

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../SYS/Account/_Reset");
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../SYS/Account/Index");
            }
        }
        // load danh sach grid nhom
        public ActionResult _List()
        {
            UserRepository _repository = new UserRepository(db);
            List<AccountViewModel> sources = new List<AccountViewModel>();

            int moduleId = 1;
            //Tìm trong db
            var account = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (account != null)
            {
                sources = _repository.Search(account.UserName, moduleId, account.FAdm == 1).ToList();
            }

            return PartialView("../SYS/Account/_List", sources);
        }
        public ActionResult _Create()
        {
            AccountCreateModel acc = new AccountCreateModel();

            return PartialView("../SYS/Account/_Create", acc);
        }

        public ActionResult _Edit(int id)
        {
            UserRepository _repository = new UserRepository(db);
            AccountCreateModel model = _repository.FindEditModel(id);
            ViewBag.PasswordVis = false;
            return PartialView("../SYS/Account/_Create", model);
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
            if (acc == null)
            {
                acc = new AccountCreateModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;


            ViewBag.PasswordVis = !EditMode;

            return PartialView("../SYS/Account/_Create", acc);
        }

        /// <summary>
        /// Popup nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult _PopupEmployeePartial()
        {
            return PartialView("../SYS/Account/_PopupEmployeePartial");
        }
        public ActionResult _SearchAreasPartial()
        {
            return PartialView("../SYS/Account/_SearchAreasPartial");
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

            var account = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (account != null)
            {
                Employeelst = _repository.FindEmployee(EmpCode, FullName, LSCompanyID, Active, account.UserName, moduleId, account.FAdm == 1);
            }
	        

            return PartialView("../SYS/Account/_SearchResultsForPopup", Employeelst);
        }
        
    }
}
