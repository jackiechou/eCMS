using Eagle.Common.Utilities;
using Eagle.Repository.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model.HR;
using Eagle.Repository.SYS.Session;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class WorkingHistoryController : BaseController
    {
        //
        // GET: /Admin/WorkingHistory/

        [SessionExpiration]
        public ActionResult Index()
        {    
            if (Request.IsAjaxRequest())
                return PartialView("../HR/WorkingHistory/_Reset");
            else
                return View("../HR/WorkingHistory/Index");          
        }

        [SessionExpiration]
        public ActionResult List()
        {
            List<WorkingHistoryViewModel> sources = new List<WorkingHistoryViewModel>();
                       
            int? ModuleId = 2;
            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            WorkingHistoryRespository _repository = new WorkingHistoryRespository(db);
            sources = _repository.GetList(CurrentEmpId, ModuleId, UserName, isAdmin,LanguageId);                         
            
            return PartialView("../HR/WorkingHistory/_List", sources);
        }

         [AjaxSessionActionFilter]
        public ActionResult GetList(int? EmpID, int? ModuleID)
        {
            List<WorkingHistoryViewModel> sources = new List<WorkingHistoryViewModel>();              
            if (EmpID != null && ModuleID != null)
            {         
                WorkingHistoryRespository _repository = new WorkingHistoryRespository(db);
                sources = _repository.GetList(EmpID, ModuleID, UserName, isAdmin, LanguageId);
            }
            else
                Response.Redirect(Url.Action("Login", "User", new { desiredUrl = Request.Url.AbsoluteUri })); 
           
            return PartialView("../HR/WorkingHistory/_List", sources);
        }    
       
        //
        // GET: /Admin/WorkingHistory/Details/5
        [SessionExpiration]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            WorkingHistoryViewModel model = new WorkingHistoryViewModel();
            if (Session[SettingKeys.AccountInfo] != null)
            {
                WorkingHistoryRespository _repository = new WorkingHistoryRespository(db);
                model = _repository.Details(id);
                Session[SettingKeys.EmpInfo] = EmployeeRepository.GetBriefDetails(model.EmpID, LanguageId);
            }
            else
                Response.Redirect(Url.Action("Login", "User", new { desiredUrl = Request.Url.AbsoluteUri })); 
            return PartialView("../HR/WorkingHistory/_Edit", model);
        }


        //
        // GET: /Admin/WorkingHistory/Create

        [SessionExpiration]
        public ActionResult Create()
        {     
            WorkingHistoryViewModel model = new WorkingHistoryViewModel();       
            return PartialView("../HR/WorkingHistory/_Create", model);
        }

      
        //
        // POST: /Admin/WorkingHistory/Create

        [HttpPost]
        public ActionResult Create(WorkingHistoryViewModel add_model, HttpPostedFileBase FileUpload)
        {
            bool flag = false;
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {


                    DateTime? joinedDate = add_model.JoinDate;
                    DateTime? startDate = add_model.FromDate;
                    DateTime? endDate = add_model.ToDate;
                    if (joinedDate.HasValue && startDate.HasValue)
                    {
                        if (DateTime.Compare(joinedDate.Value, startDate.Value) > 0)
                        {
                            flag = false;
                            message = Eagle.Resource.LanguageResource.ValidateJoinedDateEffectiveDate;
                            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (startDate.HasValue && endDate.HasValue)
                    {
                        if (DateTime.Compare(startDate.Value, endDate.Value) > 0)
                        {
                            flag = false;
                            message = Eagle.Resource.LanguageResource.ValidateStartEndDate;
                            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                        }
                    }
                    WorkingHistoryRespository _repository = new WorkingHistoryRespository(db);
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
        // POST: /Admin/WorkingHistory/Edit/5

        [HttpPost]
        public ActionResult Edit(WorkingHistoryViewModel edit_model)
        {
            bool flag = false;
            string message = string.Empty;
           //try
                //{
                if (ModelState.IsValid)
                {

                    DateTime? joinedDate = edit_model.JoinDate;
                    DateTime? startDate = edit_model.FromDate;
                    DateTime? endDate = edit_model.ToDate;
                    if (joinedDate.HasValue && startDate.HasValue)
                    {
                        if (DateTime.Compare(joinedDate.Value, startDate.Value) > 0)
                        {
                            flag = false;
                            message = Eagle.Resource.LanguageResource.ValidateJoinedDateEffectiveDate;
                            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                        }
                    }
               
                    if (startDate.HasValue && endDate.HasValue)
                    {
                        if (DateTime.Compare(startDate.Value, endDate.Value) > 0)
                        {
                            flag = false;
                            message = Eagle.Resource.LanguageResource.ValidateStartEndDate;
                            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                        }
                    }

                    WorkingHistoryRespository _repository = new WorkingHistoryRespository(db);
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
                //}
                //catch (Exception ex)
                //{
                //    ex.ToString();
                //    flag = false;
                //}
           
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }


        //
        // POST: /Admin/WorkingHistory/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            bool flag = false;
           
            WorkingHistoryRespository _repository = new WorkingHistoryRespository(db);
            flag = _repository.Delete(id, out message);
            
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }
    }
}
