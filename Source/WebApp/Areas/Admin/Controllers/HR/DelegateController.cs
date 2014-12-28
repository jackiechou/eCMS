using Eagle.Common.Helpers;
using Eagle.Core;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Common.Settings;
using Eagle.Repository.SYS.Users;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class DelegateController : BaseController
    {
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../SYS/Delegate/_Reset");
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../SYS/Delegate/Index");
            }
        }
        // load danh sach grid nhom
        public ActionResult _List()
        {
            DelegateRepository _repository = new DelegateRepository(db);
            List<DelegateViewModel> list = _repository.GetAll();
            return PartialView("../SYS/Delegate/_List", list);
        }
        public ActionResult _Create()
        {
            DelegateViewModel model = new DelegateViewModel();
            if (Session[SettingKeys.AccountInfo] != null)
            {
                var acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                model.Account = acc.UserName;
            }
            else
            {
                Response.Redirect("/");
                return null;
            }
            CreateViewBag();
            return PartialView("../SYS/Delegate/_Create", model);
        }
        public ActionResult _Edit(int id)
        {
            DelegateRepository _repository = new DelegateRepository(db);
            DelegateViewModel model = _repository.FindEdit(id);
            CreateViewBag(model.DMQuiTrinhID);
            return PartialView("../SYS/Delegate/_Create", model);
        }

        [HttpPost]
        public ActionResult _Delete(int id)
        {
            string errorMessage = "";
            DelegateRepository _repository = new DelegateRepository(db);
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
        public ActionResult Update(DelegateViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                DelegateRepository _repository = new DelegateRepository(db);
                if (ModelState.IsValid)
                {

                    model.FromDate = Convert.ToDateTime(model.FromDateNullable);
                    model.ToDate = Convert.ToDateTime(model.ToDateNullable);
                    model.Editer = acc.UserName;

                    if (_repository.Update(model,out errorMessage))
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
        public ActionResult Save(DelegateViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                DelegateRepository _repository = new DelegateRepository(db);
                if (ModelState.IsValid)
                {
                    model.FromDate = Convert.ToDateTime(model.FromDateNullable);
                    model.ToDate = Convert.ToDateTime(model.ToDateNullable);

                    if (model.FromDate.CompareTo(model.ToDate) > 0)
                    {
                        errorMessage = Eagle.Resource.LanguageResource.FromDateToDateVal;
                    }
                    else 
                    {
                        SYS_tblDelegate modelAdd = new SYS_tblDelegate()
                        {
                            Account = acc.UserName,
                            Account_delegate = model.Account_delegate,
                            DMQuiTrinhID = model.DMQuiTrinhID,
                            FromDate = model.FromDate,
                            ToDate = model.ToDate,
                            Note = model.Note,
                            Creater = acc.UserName,
                            CreateTime = DateTime.Now
                        };
                        if (_repository.Add(modelAdd, out errorMessage))
                        {
                        
                        return Content("success");
                        }
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

        public ActionResult _Error(SYS_tblDelegate model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new SYS_tblDelegate();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            CreateViewBag(model.DMQuiTrinhID);
            return PartialView("../SYS/Delegate/_Create", model);
        }
       
        private void CreateViewBag(int? DMQuiTrinhID = null)
        {
            if (Session[SettingKeys.AccountInfo] != null)
	        {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                var dmid = (from pe in db.SYS_tblOnlineProcessEmp
                            where pe.EmpID == acc.EmpId
                                 select pe.SYS_tblProcessOnlineMaster.DMQuiTrinhID).ToList();

                var kq = db.LS_tblOnlineProcess
                            .Where(p => dmid.Contains(p.DMQuiTrinhID))
                            .Select(p => new SelectListItem() { Text = p.NameProcessOnline, Value = p.DMQuiTrinhID.ToString() })
                            .ToList();

                kq.Insert(0, new SelectListItem() { Text = "--" + Eagle.Resource.LanguageResource.Choose + "--", Value = null });
                ViewBag.DMQuiTrinhID = new SelectList(kq, "Value", "Text", DMQuiTrinhID);
	        }else
            {
                Dictionary<int?,string> lst = new Dictionary<int?,string>();
                lst.Add(null,"--"+Eagle.Resource.LanguageResource.Choose+"--" );
                ViewBag.DMQuiTrinhID = new SelectList(lst, "Value", "Text");                
            }
        }


        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
        {
            UserRepository _repository = new UserRepository(db);
            List<AutoCompleteViewModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum).ToList();
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
    }
}
