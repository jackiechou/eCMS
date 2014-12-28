using Eagle.Common.Utilities;
using Eagle.Repository;
using Eagle.Repository.Contents.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model.Common;
using Eagle.Model.Contents;
using Eagle.Model;
using Eagle.Repository.SYS.Session;
using Eagle.Model.Contents.News;


namespace Eagle.WebApp.Areas.Admin.Controllers.Contents
{
    public class NewsCategoryController : BaseController
    {
        //
        // GET: /Admin/NewsCategory/
        [SessionExpiration]
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView("../Contents/NewsCategory/_Reset");
            else
                return View("../Contents/NewsCategory/Index");
        }

        [SessionExpiration]
        public ActionResult List(int? Status)
        {
            List<NewsCategoryViewModel> sources = new List<NewsCategoryViewModel>();

            int ModuleId = 2;
            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            sources = NewsCategoryRepository.GetList(ApplicationId, LanguageCode, Status);

            return PartialView("../Contents/NewsCategory/_List", sources);
        }

        // Get Hierachical List 
        public JsonResult GetTreeList()
        {
            List<TreeEntity> _list = NewsCategoryRepository.GetTreeList(LanguageCode);
            _list.Insert(0, new TreeEntity() { key = 0, parentid = null, title = " --- " + @Eagle.Resource.LanguageResource.Root + " --- " });
            return base.Json(_list, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Admin/NewsCategory/Details/5        
        [HttpGet]
        [SessionExpiration]
        public ActionResult Edit(int id)
        {
            NewsCategoryViewModel model = NewsCategoryRepository.GetDetails(id);
            ViewBag.Status = CommonRepository.GenerateThreeStatusModeList(model.Status.ToString(), null, true);
            return PartialView("../Contents/NewsCategory/_Edit", model);
        }


        //
        // GET: /Admin/NewsCategory/Create

        [SessionExpiration]
        public ActionResult Create()
        {
            NewsCategoryViewModel model = new NewsCategoryViewModel();
            model.CategoryCode = NewsCategoryRepository.GenerateCategoryCode(10);
            ViewBag.Status = CommonRepository.GenerateThreeStatusModeList(null, true);
            return PartialView("../Contents/NewsCategory/_Edit", model);
        }
                
        //
        // POST: /Admin/NewsCategory/Create-
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(NewsCategoryViewModel add_model)
        {
            bool flag = false;
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    add_model.CreatedByUserId = UserId;
                    add_model.LanguageCode = LanguageCode;                    

                    NewsCategoryRepository _repository = new NewsCategoryRepository(db);
                    flag = NewsCategoryRepository.Insert(add_model, out message);
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

        ////
        //// POST: /Admin/NewsCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(NewsCategoryViewModel edit_model)
        {
            bool flag = false;
            string message = string.Empty;
            //try
            //{
                if (ModelState.IsValid)
                {
                    edit_model.LastModifiedByUserId = UserId;
                    edit_model.LanguageCode = LanguageCode;  
                    flag = NewsCategoryRepository.Update(edit_model, out message);
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
        // POST: /Admin/NewsCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            bool flag = false;      
            flag = NewsCategoryRepository.Delete(id, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ValidateCode(string NewsCategoryNo)
        {
            bool flag = NewsCategoryRepository.IsCodeExisted(NewsCategoryNo);
            bool result = (flag == true) ? false : true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }       

    }
}
