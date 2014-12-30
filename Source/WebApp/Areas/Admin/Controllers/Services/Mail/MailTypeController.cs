using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.Common;

using Eagle.Repository.SYS.Session;
using Eagle.Model.Mail;
using Eagle.Common.Settings;
using Eagle.Repository;

namespace Eagle.WebApp.Areas.Admin.Controllers.Services.Mail
{
    public class MailTypeController : BaseController
    {
        //
        // GET: /Admin/MailType/
        [SessionExpiration]
        public ActionResult Index(int? Mode)
        {           
            if (Request.IsAjaxRequest())
                return PartialView("../Services/Mail/MailType/_Edit");
            else
                return View("../Services/Mail/MailType/Index");            
        }

        // Get Companies
        [HttpGet]
        public JsonResult GetActiveList()
        {
            string result = MailTypeRespository.SerializeMailTypeToJson(1, LanguageCode, true);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Admin/MailType/Create

        [SessionExpiration]
        public ActionResult Create()
        {
            MailTypeViewModel model = new MailTypeViewModel();
            model.ParentId = 0;
            ViewBag.Status = CommonRepository.GenerateTwoStatusModeList(null, true);

            return PartialView("../Services/Mail/MailType/_Edit", model);
        }
        
        //
        // POST: /Admin/MailType/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(MailTypeViewModel add_model)
        {
            bool flag = false;
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    flag = MailTypeRespository.Insert(add_model, out message);
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
        // GET: /Admin/MailType/Details/5
        [SessionExpiration]
        [HttpGet]
        public ActionResult Details(int id)
        {
            MailTypeViewModel model = MailTypeRespository.GetDetails(id);
            // PopulateHierachyTypeList(model.ParentId);
            //ViewBag.ParentId = model.ParentId;
            ViewBag.Status = CommonRepository.GenerateTwoStatusModeList(model.Status.ToString(), true);
            return PartialView("../Services/Mail/MailType/_Edit", model);
        }
        //
        // POST: /Admin/MailType/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(MailTypeViewModel edit_model)
        {
            bool flag = false;
            string message = string.Empty;
            //try
            //{

            if (ModelState.IsValid)
            {
                flag = MailTypeRespository.Update(edit_model, out message);
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

        [HttpPost]
        public ActionResult UpdateStatus(int id)
        {
            string message = string.Empty;
            bool flag = MailTypeRespository.UpdateStatus(id, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateListOrder(int id, int listorder)
        {
            string message = string.Empty;
            bool flag = MailTypeRespository.UpdateListOrder(id, listorder, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }
        //
        // POST: /Admin/MailType/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            bool flag = MailTypeRespository.Delete(id, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }



        #region Hierachy Type List ===============================================================================================
        public ActionResult PopulateHierachyTypeList()
        {
            List<TreeNode> _list = MailTypeRespository.PopulateHierachyDropDownList(1, LanguageCode, true);
            if (_list.Count == 0)
                _list.Insert(0, new TreeNode() { id = 0, text = " --- " + @Eagle.Resource.LanguageResource.None + " --- " });
            else
            {
                _list.Insert(0, new TreeNode() { id = 0, text = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            }
            return base.Json(_list, JsonRequestBehavior.AllowGet);
        }

      
        #endregion ===============================================================================================================

        
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetList()
        {
            var list = db.Mail_Types
                            .Select(p => new MailTypeViewModel()
                            {
                                TypeId = p.TypeId,
                                ParentId = p.ParentId,
                                Name = p.Name,
                                CultureCode = p.CultureCode,
                                SortKey = p.SortKey,
                                Depth = p.Depth,
                                Status = p.Status,
                                Description = p.Description
                            }).ToList();
            list.Insert(0, new MailTypeViewModel() { TypeId = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            var retData = list.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.TypeId.ToString(),
            });
            return Json(retData, JsonRequestBehavior.AllowGet);
        }
    }
}
