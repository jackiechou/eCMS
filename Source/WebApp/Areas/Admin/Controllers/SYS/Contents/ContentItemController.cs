using Eagle.Common.Helpers;
using Eagle.Common.Utilities;
using Eagle.Model;
using Eagle.Model.SYS.Content;
using Eagle.Repository.Sys.Contents;
using Eagle.Repository.SYS.Contents;
using Eagle.Repository.SYS.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.SYS.Contents
{
    public class ContentItemController : BaseController
    {
        //
        // GET: /Admin/ContentItem/
         [SessionExpiration]
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView("../Sys/Contents/_Reset");
            else
                return View("../Sys/Contents/Index");
        }

         // GET: /Admin/ContentItem/Edit/5        
         [HttpGet]
         [SessionExpiration]
         public ActionResult Create()
         {
             ContentItemViewModel model = new ContentItemViewModel();
             ViewBag.ContentTypeId = ContentTypeRepository.PopulateContentTypesToDropDownList(null, false);
             return PartialView("../Sys/Contents/_Edit", model);
         }

         // GET: /Admin/ContentItem/Edit/5        
         [HttpGet]
         [SessionExpiration]
         public ActionResult Details(int Id)
         {
             ContentItemViewModel model = ContentItemRepository.Details(Id);
             ViewBag.ContentTypeId = ContentTypeRepository.PopulateContentTypesToDropDownList(model.ContentTypeId.ToString(), false);
             return PartialView("../Sys/Contents/_Edit", model);
         }

         [SessionExpiration]
         [HttpGet]
         public ActionResult List(int ContentType)
         {
             List<ContentItemViewModel> lst = ContentItemRepository.GetList(ContentType);
             ViewBag.ContentType = ContentTypeRepository.PopulateContentTypesToDropDownList(ContentType.ToString(), false);
             return PartialView("../Sys/Contents/_List", lst);
         }

         [SessionExpiration]
         [HttpGet]
         public ActionResult GetListByPage()
         {
             List<ContentTreeModel> lst = ContentItemRepository.GetTreeList((int)ContentTypeSetting.Page, ScopeTypeId);
             return base.Json(lst, JsonRequestBehavior.AllowGet);
         }

         [SessionExpiration]
         [HttpGet]
         public ActionResult GetListByModule()
         {
             List<ContentTreeModel> lst = ContentItemRepository.GetTreeList((int)ContentTypeSetting.Module, ScopeTypeId);
             return base.Json(lst, JsonRequestBehavior.AllowGet);
         }

         #region autocomplete DropDownList ============================================================================================
         /// <summary>
         /// Dùng cho viec binding du lieu cho dropdownlist autocomplete
         /// </summary>
         /// <param name="searchTerm"></param>
         /// <param name="pageSize"></param>
         /// <param name="pageNum"></param>
         /// <returns></returns>
         [HttpGet]
         public ActionResult DropDownListByPage(string searchTerm, int pageSize, int pageNum)
         {
             List<AutoCompleteModel> sources = ContentItemRepository.GetContentDropDownListByPage(searchTerm, pageSize, pageNum).ToList();
             int iTotal = sources.Count;

             //Translate the list into a format the select2 dropdown expects
             Select2PagedResultViewModel pagedList = ConvertAutoListToSelect2Format(sources, iTotal);

             //Return the data as a jsonp result
             return new JsonpResult
             {
                 Data = pagedList,
                 JsonRequestBehavior = JsonRequestBehavior.AllowGet
             };
         }

         public ActionResult DropDownListByModule(string searchTerm, int pageSize, int pageNum)
         {
             List<AutoCompleteModel> sources = ContentItemRepository.GetContentDropDownListByModule(searchTerm, pageSize, pageNum).ToList();
             int iTotal = sources.Count;

             //Translate the list into a format the select2 dropdown expects
             Select2PagedResultViewModel pagedList = ConvertAutoListToSelect2Format(sources, iTotal);

             //Return the data as a jsonp result
             return new JsonpResult
             {
                 Data = pagedList,
                 JsonRequestBehavior = JsonRequestBehavior.AllowGet
             };
         }

         #endregion ===================================================================================================================
        


        // POST: /Admin/Contract/Create
        [AcceptVerbs(HttpVerbs.Post)]
         public ActionResult Create(ContentItemViewModel add_model)
        {
            bool flag = false;
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    flag = ContentItemRepository.Insert(add_model, out message);
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
        // POST: 

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(ContentItemViewModel edit_model)
        {
            bool flag = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                flag = ContentItemRepository.Update(edit_model, out message);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var modelError in errors)
                {
                    message += modelError.ErrorMessage + "-";
                }
            }
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }


        //
        // POST: /Admin/Contract/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            bool flag = false;
            flag = ContentItemRepository.Delete(id, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }
    }
}
