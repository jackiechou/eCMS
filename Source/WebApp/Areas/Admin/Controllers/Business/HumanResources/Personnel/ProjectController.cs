using Eagle.Repository.SYS.Session;
using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model.HR;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class ProjectController : BaseController
    {
        public void PopulateEmpployeesToDropDownList(int? selected_value)
        {
            EmployeeRepository _respository = new EmployeeRepository(db);
            List<EmployeeEntity> _list = _respository.GetListForDropDownList();
            if (_list.Count == 0)
                _list.Insert(0, new EmployeeEntity() { Id = 0, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
            else
                _list.Insert(0, new EmployeeEntity() { Id = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });

            ViewBag.EmpID = new SelectList(_list, "Id", "Name", selected_value);
        }
        //
        // GET: /Admin/Project/
        [SessionExpiration]
        public ActionResult Index()
        {
             if (Session[SettingKeys.AccountInfo] != null)
             {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("../HR/Project/_Reset");
                }
                else
                {
                    return View("../HR/Project/Index");
                }
             }
             else
             {
                 Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                 return null;
             } 
        }

        [SessionExpiration]
        public ActionResult List()
        {
            List<ProjectViewModel> sources = new List<ProjectViewModel>();
            if (Session[SettingKeys.AccountInfo] != null)
             {
                 int? ModuleId = 2;
                 if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                    ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);    
                 ProjectRespository _repository = new ProjectRespository(db);
                 sources = _repository.GetList(CurrentEmpId, ModuleId, UserName, isAdmin);                
             }
            else           
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
            return PartialView("../HR/Project/_List", sources);
        }

        [AjaxEmpSessionActionFilter]
        public ActionResult GetList(int? EmpID, int? ModuleID)
        {
            List<ProjectViewModel> sources = new List<ProjectViewModel>();          
            if (EmpID != null && ModuleID != null)
            {
                ProjectRespository _repository = new ProjectRespository(db);
                sources = _repository.GetList(EmpID, ModuleID, UserName, isAdmin);
            }else
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);  
            return PartialView("../HR/Project/_List", sources);
        }     

        //
        // GET: /Admin/Project/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Project/Create
        [AjaxSessionActionFilter]
        public ActionResult Create()
        {
            ProjectViewModel model = new ProjectViewModel();
            ProjectRespository _repository = new ProjectRespository(db);
            string ProjectCode = _repository.GenerateProjectCode(10);
            model.ProjectCode = ProjectCode;           
            return PartialView("../HR/Project/_Create", model);
        }
        //
        // GET: /Admin/Project/Edit/5
        [SessionExpiration]
        public ActionResult Edit(int id)
        {
            ProjectRespository _repository = new ProjectRespository(db);
            ProjectViewModel model = _repository.Details(id);
            Session[SettingKeys.EmpInfo] = EmployeeRepository.GetBriefDetails(model.EmpID, LanguageId);
            return PartialView("../HR/Project/_Edit", model);
        }
        //
        // POST: /Admin/Project/Create

        [HttpPost]
        public ActionResult Create(ProjectViewModel add_model)
        {
            bool flag = false;
            string message = string.Empty;
            if (Session[SettingKeys.UserId] != null)
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
                                //ModelState.AddModelError("Date", "start date must be before end date");
                                //result = false;
                                return Content("start date must be before end date");
                            }
                        }
                        ProjectRespository _repository = new ProjectRespository(db);
                        add_model.Creater = CurrentEmpId;
                        flag = _repository.Add(add_model, CurrentEmpId, out message);
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
            }
            else
            {
                message = "timeout";
                throw new Exception("SessionExpired");                
            }
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

       

        //
        // POST: /Admin/Project/Edit/5

        [HttpPost]
        public ActionResult Edit(ProjectViewModel edit_model)
        {
            bool flag = false;
            string message = string.Empty;
             if (Session[SettingKeys.UserId] != null)
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
                               // ModelState.AddModelError("Date", "start date must be before end date");
                               // result = false;
                                return Content("start date must be before end date");
                            }
                        }

                        ProjectRespository _repository = new ProjectRespository(db);
                        edit_model.Creater = CurrentEmpId;
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
            }
             else
             {
                 message = "timeout";
                 throw new Exception("SessionExpired");
             }
             return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
             //return Content(flag.ToString());

        }


        //
        // POST: /Admin/Project/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            bool flag = false;
            ProjectRespository _repository = new ProjectRespository(db);
            flag = _repository.Delete(id, out message);
            //return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
            return Content(message);
        }

        [HttpGet]
        public JsonResult ValidateCode(string ProjectCode)
        {
            ProjectRespository _repository = new ProjectRespository(db);
            bool flag = _repository.CheckExistCode(ProjectCode);
            bool result = (flag == true) ? false : true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
