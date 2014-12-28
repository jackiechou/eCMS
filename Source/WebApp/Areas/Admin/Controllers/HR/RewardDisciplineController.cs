using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository;
using Eagle.Repository.HR;
using Eagle.Repository.LS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.HR;
using Eagle.Model.LS;
using Eagle.Model.SYS;
using Eagle.Repository.SYS.Session;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers.HR
{
    public class RewardDisciplineController : BaseController
    {
        //
        // GET: /Admin/RewardDiscipline/

        [SessionExpiration]
        public ActionResult Index()
        {          
            if (Request.IsAjaxRequest())
                return PartialView("../HR/RewardDiscipline/_Reset");
            else
                return View("../HR/RewardDiscipline/Index");           
        }

        [SessionExpiration]
        public ActionResult List()
        {
            List<RewardDisciplineViewModel> lst = new List<RewardDisciplineViewModel>();           
            int ModuleId = 2;
            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);                    
            RewardDisciplineRespository _repository = new RewardDisciplineRespository(db);
            lst = _repository.GetList(CurrentEmpId, ModuleId, UserName, isAdmin, LanguageId);               
           
           return PartialView("../HR/RewardDiscipline/_List", lst);
        }

        [AjaxSessionActionFilter]
        public ActionResult GetList(int? EmpID, int? ModuleID)
        {
            List<RewardDisciplineViewModel> lst = new List<RewardDisciplineViewModel>();           
            if (EmpID != null && ModuleID != null)
            {
                RewardDisciplineRespository _repository = new RewardDisciplineRespository(db);
                lst = _repository.GetList(EmpID, ModuleID, UserName, isAdmin, LanguageId);
            }
            else
                Response.Redirect(Url.Action("Login", "User", new { desiredUrl = Request.Url.AbsoluteUri }));             
            return PartialView("../HR/RewardDiscipline/_List", lst);
        }    
      
        //[HttpGet]
        //public ActionResult CreateDownloadLink(int? FileId)
        //{
        //    FileRepository _respository = new FileRepository(db);
        //    string DownloadFileLink = _respository.GenerateDownloadLink(FileId);
        //    return PartialView("../HR/RewardDiscipline/_DownloadLink", DownloadFileLink);
        //}

        public void PopulateRewardsToDropDownList(int? Type, int? selected_value)
        {
            LS_tblRewardRespository _respository = new LS_tblRewardRespository(db);
            List<RewardEntity> _list = _respository.GetListForDropDownList(Type, LanguageId);
            if (_list.Count == 0)
                _list.Insert(0, new RewardEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
            else
                _list.Insert(0, new RewardEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });

            ViewBag.LSRewardID = new SelectList(_list, "Id", "Name", selected_value);
        }

        public void PopulateRewardLevelsToDropDownList(int? Type, int? selected_value)
        {
            LS_tblRewardLevelRespository _respository = new LS_tblRewardLevelRespository(db);
            List<RewardLevelEntity> _list = _respository.GetListForDropDownList(Type, LanguageId);
            if (_list.Count == 0)
                _list.Insert(0, new RewardLevelEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
            else
                _list.Insert(0, new RewardLevelEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });

            ViewBag.LSRewardLevelID = new SelectList(_list, "Id", "Name", selected_value);
        }

        public void PopulateTypesToDropDownList(int? selected_value)
        {
            RewardDisciplineTypeModelList lst = new RewardDisciplineTypeModelList(LanguageId);
            //lst.Insert(0, new RewardLevelEntity() { Id = null, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            ViewBag.Type = new SelectList(lst.TypeList, "TypeId", "TypeName", selected_value);
        }

        //
        // GET: /Admin/RewardDiscipline/Create

        [SessionExpiration]
        public ActionResult Create()
        {
            RewardDisciplineViewModel model = new RewardDisciplineViewModel();    
            RewardDisciplineRespository _repository = new RewardDisciplineRespository(db);
            PopulateTypesToDropDownList(1);
            PopulateRewardsToDropDownList(1, null);
            PopulateRewardLevelsToDropDownList(1, null);
                    
            return PartialView("../HR/RewardDiscipline/_Create", model);
        }

        //
        // POST: /Admin/RewardDiscipline/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(RewardDisciplineViewModel add_model)
        {
            bool flag = false;
            string message = string.Empty;           
            try
            {
                if (ModelState.IsValid)
                {
                    //int FileId = 0;
                    //if (add_model.FileUpload != null && add_model.FileUpload.ContentLength > 0)
                    //{
                    //    FileRepository file_respository = new FileRepository(db);
                    //    FileId = file_respository.Insert(add_model.FileUpload, "UPLOAD_DOCUMENT_DIR", null, null, EmpId);
                    //}

                    DateTime? JoinDate = add_model.JoinDate;
                    DateTime? SignedDate = add_model.SignedDate;
                    DateTime? EffectiveDate = add_model.EffectiveDate;

                    if (JoinDate.HasValue && SignedDate.HasValue)
                    {
                        if (DateTime.Compare(JoinDate.Value, SignedDate.Value) > 0)
                        {
                            flag = false;
                            message = Eagle.Resource.LanguageResource.ValidateJoinedSignedDate;
                            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (SignedDate.HasValue && EffectiveDate.HasValue)
                    {
                        if (DateTime.Compare(SignedDate.Value, EffectiveDate.Value) > 0)
                        {
                            flag = false;
                            message = Eagle.Resource.LanguageResource.ValidateSignedEffectiveDate;
                            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                        }
                    }
                    RewardDisciplineRespository _repository = new RewardDisciplineRespository(db);
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
        // GET: /Admin/RewardDiscipline/Details/5
        [SessionExpiration]
        [HttpGet]
        public ActionResult Edit(int id)
        {           
            RewardDisciplineRespository _repository = new RewardDisciplineRespository(db);
            RewardDisciplineViewModel model = _repository.Details(id, LanguageId);
            Session[SettingKeys.EmpInfo] = EmployeeRepository.GetBriefDetails(model.EmpID, LanguageId);
            PopulateTypesToDropDownList(model.Type);
            PopulateRewardsToDropDownList(model.Type, model.LSRewardID);
            PopulateRewardLevelsToDropDownList(model.Type, model.LSRewardLevelID);
            return PartialView("../HR/RewardDiscipline/_Edit", model);             
        }
        //
        // POST: /Admin/RewardDiscipline/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(RewardDisciplineViewModel edit_model)
        {
            bool flag = false;
            string message = string.Empty;
           
            //try
            //{

            if (ModelState.IsValid)
            {
                int? FileId = edit_model.FileId;

                DateTime? JoinDate = edit_model.JoinDate;
                DateTime? SignedDate = edit_model.SignedDate;
                DateTime? EffectiveDate = edit_model.EffectiveDate;

                if (JoinDate.HasValue && SignedDate.HasValue)
                {
                    if (DateTime.Compare(JoinDate.Value, SignedDate.Value) > 0)
                    {
                        flag = false;
                        message = Eagle.Resource.LanguageResource.ValidateJoinedSignedDate;
                        return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                    }
                }

                if (SignedDate.HasValue && EffectiveDate.HasValue)
                {
                    if (DateTime.Compare(SignedDate.Value, EffectiveDate.Value) > 0)
                    {
                        flag = false;
                        message = Eagle.Resource.LanguageResource.ValidateSignedEffectiveDate;
                        return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
                    }
                }               

                RewardDisciplineRespository _repository = new RewardDisciplineRespository(db);
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
        // POST: /Admin/RewardDiscipline/Delete/5
     
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            bool flag = false;
          
            RewardDisciplineRespository _repository = new RewardDisciplineRespository(db);
            flag = _repository.Delete(id, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }      
    }
}
