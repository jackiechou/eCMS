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
    public class OnlineProcessController : BaseController
    {
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Sys/OnlineProcess/_Reset");
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../Sys/OnlineProcess/Index");
            }
        }
        // load danh sach grid nhom
        public ActionResult _List()
        {
            OnlineProcessRepository _repository = new OnlineProcessRepository(db);
            List<LS_tblOnlineProcessViewModel> list = _repository.GetAll(LanguageId);
            return PartialView("../Sys/OnlineProcess/_List", list);
        }
        public ActionResult _Create()
        {
            LS_tblOnlineProcessViewModel model = new LS_tblOnlineProcessViewModel();
            CreateViewBag();
            return PartialView("../Sys/OnlineProcess/_Create", model);
        }
        public ActionResult _Edit(int id)
        {
            OnlineProcessRepository _repository = new OnlineProcessRepository(db);
            LS_tblOnlineProcessViewModel model = _repository.Find(id);
            string BccJson = model.BccJson;
            string CcJson = model.CcJson;
            CreateViewBag(model.FunctionID,model.IsStart,model.NoOfLevel);
            return PartialView("../Sys/OnlineProcess/_Create", model);
        }

        [HttpPost]
        public ActionResult _Delete(int id)
        {
            OnlineProcessRepository _repository = new OnlineProcessRepository(db);
            string errorMessage = "";
            if (_repository.Delete(id, out errorMessage))
            {
                return Content("success");
            }else
            {
                return Content(errorMessage);
            }
        }

        [HttpPost]
        public ActionResult Update(LS_tblOnlineProcessViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                OnlineProcessRepository _repository = new OnlineProcessRepository(db);
                if (ModelState.IsValid)
                {
                    bool bResult = _repository.Update(model, out errorMessage);
                    if (bResult)
                        return Content("success");    
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
        public ActionResult Save(LS_tblOnlineProcessViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                OnlineProcessRepository _repository = new OnlineProcessRepository(db);
                if (ModelState.IsValid)
                {
                    if ( _repository.Add(model,out errorMessage))
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

        public ActionResult _Error(LS_tblOnlineProcess model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblOnlineProcess();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            CreateViewBag(model.FunctionID, model.IsStart, model.NoOfLevel);
            return PartialView("../Sys/OnlineProcess/_Create", model);
        }
        private void CreateViewBag(int? FunctionID = null, int IsStart = 0, int NoOfLevel = 1)
        {
            List<FunctionListViewModel> funcLst = new List<FunctionListViewModel>();
            Dictionary<int, string> Startlst = new Dictionary<int, string>();
            int[] NoOfLevellst = new int[] {1,2,3,4,5 };
            if (LanguageId == 124)
	        {
                funcLst = db.SYS_tblFunctionList
                            .Where(p => p.LookId == "OP")
                            .Select(p => new FunctionListViewModel() { FunctionID = p.FunctionID, FunctionNameE = p.FunctionNameE })
                            .ToList();
                Startlst.Add(2, "Back to selected process");
                Startlst.Add(1,"Back to previous process");
                Startlst.Add(0,"Back to first process");
	        }else
            {
                funcLst = db.SYS_tblFunctionList
                            .Where(p => p.LookId == "OP")
                            .Select(p => new FunctionListViewModel() { FunctionID = p.FunctionID, FunctionNameE = p.FunctionNameV })
                            .ToList();
                Startlst.Add(2, "Sẽ chọn cấp trả về.");
                Startlst.Add(1, "Quay về quy trình trước 1 cấp.");
                Startlst.Add(0, "Quay về từ đầu qui trình.");
            }
            ViewBag.FunctionId = new SelectList(funcLst, "FunctionID", "FunctionNameE", FunctionID);
            ViewBag.IsStart = new SelectList(Startlst, "Key", "Value", IsStart);
            ViewBag.NoOfLevel = new SelectList(NoOfLevellst, null, null, NoOfLevel);
            
        }

    }
}
