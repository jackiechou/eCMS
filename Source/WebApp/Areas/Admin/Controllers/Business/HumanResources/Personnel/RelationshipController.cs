using Eagle.Common.Utilities;
using Eagle.Repository.HR;
using Eagle.Repository.LS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model.HR;
using Eagle.Model.LS;
using Eagle.Repository.SYS.Session;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers.HR
{
    public class RelationshipController : BaseController
    {
        //
        // GET: /Admin/Relationship/

        [SessionExpiration]
        public ActionResult Index()
        {            
            if (Request.IsAjaxRequest())
                return PartialView("../HR/Relationship/_Reset");
            else
                return View("../HR/Relationship/Index");
        }

        [SessionExpiration]
        public ActionResult List()
        {
            List<RelationshipViewModel> sources = new List<RelationshipViewModel>();            
            int? ModuleId = 2;
            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            RelationshipRespository _repository = new RelationshipRespository(db);
            sources = _repository.GetList(CurrentEmpId, ModuleId, UserName, isAdmin, LanguageId);           
            return PartialView("../HR/Relationship/_List", sources);
        }

        [AjaxSessionActionFilter]
        public ActionResult GetList(int? EmpID, int? ModuleID)
        {
            List<RelationshipViewModel> sources = new List<RelationshipViewModel>();              
            if (EmpID != null && ModuleID != null)
            {          
                RelationshipRespository _repository = new RelationshipRespository(db);
                sources = _repository.GetList(EmpID, ModuleID, UserName, isAdmin,LanguageId);
            }
            else
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);           
            return PartialView("../HR/Relationship/_List", sources);
        }    
        //
        // GET: /Admin/Relationship/Details/5
        [SessionExpiration]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            RelationshipRespository _repository = new RelationshipRespository(db);
            RelationshipViewModel model = _repository.Details(id);
            Session[SettingKeys.EmpInfo] = EmployeeRepository.GetBriefDetails(model.EmpID,LanguageId);
            PopulateGenderToDropdownList(model.Gender);
            return PartialView("../HR/Relationship/_Edit", model);            
        }


        //
        // GET: /Admin/Relationship/Create

        [SessionExpiration]
        public ActionResult Create()
        {           
            RelationshipViewModel model = new RelationshipViewModel();
            RelationshipRespository _repository = new RelationshipRespository(db);
            PopulateGenderToDropdownList(null);
            return PartialView("../HR/Relationship/_Create", model);
        }


        #region MethodPIT -------------------------------------------------------------------------------------------------------------
        public void PopulateRelationshipToDropDownList(int? selected_value)
        {
            LS_tblRelationshipRepository _respository = new LS_tblRelationshipRepository(db);
            List<RelationshipEntity> _list = _respository.GetListForDropDownList(LanguageId);
            if (_list.Count == 0)
                _list.Insert(0, new RelationshipEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
            else
                _list.Insert(0, new RelationshipEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });

            ViewBag.LSLocationID = new SelectList(_list, "Id", "Name", selected_value);
        }

        public void PopulateGenderToDropdownList(int? selectedValue)
        {            
            ViewBag.Gender = new SelectList(Repository.GenderClass.GetGenderList(), "Key", "Value", selectedValue);            
        }
        #endregion -----------------------------------------------------------------------------------------------------------------

        //
        // POST: /Admin/Relationship/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(RelationshipViewModel add_model)
        {
            bool flag = false;
            string message = string.Empty;
           
            try
            {
                if (ModelState.IsValid)
                {
                    DateTime? startDate = add_model.FromDatePIT;
                    DateTime? endDate = add_model.ToDatePIT;
                    if (startDate.HasValue && endDate.HasValue)
                    {
                        if (DateTime.Compare(startDate.Value, endDate.Value) > 0)
                        {
                            flag = false;
                            message = Eagle.Resource.LanguageResource.ValidateStartEndDate;
                            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                        }
                    }
                    RelationshipRespository _repository = new RelationshipRespository(db);
                    add_model.Creater = CurrentEmpId;
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
        // POST: /Admin/Relationship/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(RelationshipViewModel edit_model)
        {
            bool flag = false;
            string message = string.Empty;           
            //try
            //{
            if (ModelState.IsValid)
            {
                DateTime? startDate = edit_model.FromDatePIT;
                DateTime? endDate = edit_model.ToDatePIT;
                if (startDate.HasValue && endDate.HasValue)
                {
                    if (DateTime.Compare(startDate.Value, endDate.Value) > 0)
                    {
                        flag = false;
                        message = Eagle.Resource.LanguageResource.ValidateStartEndDate;
                        return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                    }
                }

                RelationshipRespository _repository = new RelationshipRespository(db);
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
            //}
            //catch (Exception ex)
            //{
            //    ex.ToString();
            //    flag = false;
            //}
           
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }


        //
        // POST: /Admin/Relationship/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            bool flag = false;
           
            RelationshipRespository _repository = new RelationshipRespository(db);
            flag = _repository.Delete(id, out message);
            
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }      

    }
}
