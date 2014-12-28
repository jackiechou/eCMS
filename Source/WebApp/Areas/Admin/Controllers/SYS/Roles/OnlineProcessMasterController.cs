using Eagle.Core;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class OnlineProcessMasterController : BaseController
    {
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Sys/OnlineProcessMaster/_Reset");
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../Sys/OnlineProcessMaster/Index");
            }
        }
        // load danh sach grid nhom
        public ActionResult _List()
        {
            OnlineProcessMasterRepository _repository = new OnlineProcessMasterRepository(db);
            List<OnlineProcessMasterViewModel> list = _repository.GetAll();
            return PartialView("../Sys/OnlineProcessMaster/_List", list);
        }
        public ActionResult _Create()
        {
            SYS_tblProcessOnlineMaster model = new SYS_tblProcessOnlineMaster();
            CreateViewBag();
            return PartialView("../Sys/OnlineProcessMaster/_Create", model);
        }
        public ActionResult _Edit(int id)
        {
            OnlineProcessMasterRepository _repository = new OnlineProcessMasterRepository(db);
            SYS_tblProcessOnlineMaster model = _repository.Find(id);
            CreateViewBag(model.DMQuiTrinhID);
            return PartialView("../Sys/OnlineProcessMaster/_Create", model);
        }

        [HttpPost]
        public ActionResult _Delete(int id)
        {
            OnlineProcessMasterRepository _repository = new OnlineProcessMasterRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }

        [HttpPost]
        public ActionResult Update(SYS_tblProcessOnlineMaster model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                OnlineProcessMasterRepository _repository = new OnlineProcessMasterRepository(db);
                if (ModelState.IsValid)
                {
                    bool bResult = _repository.Update(model,out errorMessage);
                    if (bResult)
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
            return _Error(model, errorMessage);
        }
        [HttpPost]
        public ActionResult Save(SYS_tblProcessOnlineMaster model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                OnlineProcessMasterRepository _repository = new OnlineProcessMasterRepository(db);
                if (ModelState.IsValid)
                {
                    bool bResult = _repository.Add(model, out errorMessage);
                    if (bResult)
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
            return _Error(model, errorMessage);
        }

        public ActionResult _Error(SYS_tblProcessOnlineMaster model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new SYS_tblProcessOnlineMaster();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            CreateViewBag(model.DMQuiTrinhID);
            return PartialView("../Sys/OnlineProcessMaster/_Create", model);
        }
        private void CreateViewBag(int? DMQuiTrinhID = null)
        {
            var lst = db.LS_tblOnlineProcess.Select(p => new { p.DMQuiTrinhID, p.NameProcessOnline }).ToList();
            ViewBag.DMQuiTrinhID = new SelectList(lst, "DMQuiTrinhID", "NameProcessOnline", DMQuiTrinhID);

        }

    }
}
