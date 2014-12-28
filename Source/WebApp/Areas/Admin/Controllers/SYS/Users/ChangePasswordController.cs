using Eagle.Common.Settings;
using Eagle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.SYS.Users
{
    public class ChangePasswordController : BaseController
    {
        //
        // GET: /Admin/ChangePassword/

        public ActionResult Index()
        {
            return View("../SYS/Users/ChangePassword/Index");
        }
        public ActionResult _Create()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];

            ChangePasswordViewModel model = new ChangePasswordViewModel();
            if (acc != null)
            {
                model.UserName = acc.UserName;
            }

            return PartialView("../SYS/Users/ChangePassword/_Create", model);
        }

        [HttpPost]
        public ActionResult Save(ChangePasswordViewModel model)
        {
            string errorMessage = "";
            if (model.NewPassword != model.RetypeNewPassword)
            {
                errorMessage = "Vui Lòng kiểm tra lại ô \"nhập lại mật khẩu mới\"";
                return _Error(model, errorMessage);
            }
            else
            {
                try
                {
                    var oldPassword = Eagle.Common.Utilities.StringUtils.GetMd5Sum(model.OldPassword);
                    var acc = db.SYS_tblUserAccount.Where(p => p.Password == oldPassword && p.UserName == model.UserName).FirstOrDefault();
                    if (acc != null)
                    {

                        acc.Password = Eagle.Common.Utilities.StringUtils.GetMd5Sum(model.NewPassword);
                        db.Entry(acc).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return Content("success");
                    }
                    else
                    {
                        errorMessage = "Không tìm thấy tài khoản để thay đổi mật khẩu!";
                        return _Error(model, errorMessage);
                    }
                }
                catch (Exception ex)
                {
                    return _Error(model, ex.Message);
                }
            }

        }

        public ActionResult _Error(ChangePasswordViewModel model, string ErrorMessage)
        {
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            return PartialView("../SYS/Users/ChangePassword/_Create", model);
        }

    }
}
