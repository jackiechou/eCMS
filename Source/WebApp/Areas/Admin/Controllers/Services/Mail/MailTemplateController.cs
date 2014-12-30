using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.Mail;
using Eagle.Repository.SYS.Session;
using Eagle.Common.Settings;
using Eagle.Model.SYS.User;

namespace Eagle.WebApp.Areas.Admin.Controllers.Services.Mail
{
    public class MailTemplateController : BaseController
    {
        //
        // GET: /Admin/MailTemplate/
        [SessionExpiration]
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView("../Services/Mail/MailTemplate/_Reset");
            else
                return View("../Services/Mail/MailTemplate/Index");
        }

        [SessionExpiration]
        public ActionResult List(int? TypeId)
        {
            IList<MailTemplateViewModel> sources = MailTemplateRespository.GetListByTypeId(TypeId);
            return PartialView("../Services/Mail/MailTemplate/_List", sources);
        }

        //
        // GET: /Admin/MailTemplate/Create

        [SessionExpiration]
        public ActionResult Create()
        {
            ViewBag.Mail_Template_Discontinued = Eagle.Repository.CommonRepository.GenerateTwoStatusModeList(null, false);
            return PartialView("../Services/Mail/MailTemplate/_Create");
        }

       

        //
        // POST: /Admin/MailTemplate/Create
    //    [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(MailTemplateViewModel add_model)
        {
            bool flag = false;
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    add_model.Mail_Template_Content = add_model.Mail_Template_Content;                 
                    flag = MailTemplateRespository.Insert(add_model, out message);
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
        // GET: /Admin/MailTemplate/Details/5
        [SessionExpiration]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            MailTemplateViewModel model = MailTemplateRespository.GetDetails(id);
            //ViewBag.Contents = HttpContext.Server.HtmlDecode(model.Mail_Template_Content);
            ViewBag.Contents = model.Mail_Template_Content;
            ViewBag.Mail_Template_Discontinued = Eagle.Repository.CommonRepository.GenerateTwoStatusModeList(model.Mail_Template_Discontinued.ToString(), false);
            return PartialView("../Services/Mail/MailTemplate/_Edit", model);
        }
        //
        // POST: /Admin/MailTemplate/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(MailTemplateViewModel edit_model)
        {
            bool flag = false;
            string message = string.Empty;
            //try
            //{

            if (ModelState.IsValid)
            {
                edit_model.Mail_Template_Content = edit_model.Mail_Template_Content; 
                flag = MailTemplateRespository.Update(edit_model, out message);
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
        // POST: /Admin/MailTemplate/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string result = string.Empty;
            bool flag = MailTemplateRespository.Delete(id);
            if (flag)
                result = "success";
            return Content(result);
        }

    }
}
