using Eagle.Common.Utilities;
using Eagle.Model.Contents.Banners;
using Eagle.Repository.Contents.Banners;
using Eagle.Repository.SYS;
using Eagle.Repository.SYS.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.Contents.Banners
{
    public class BannerController : BaseController
    {
        //
        // GET: /Admin/Banner/
        [SessionExpiration]
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView("../Contents/Banners/_Reset");
            else
                return View("../Contents/Banners/Index");
        }

        [SessionExpiration]
        public ActionResult List(int? BannerTypeId, int? BannerPositionId)
        {
            List<BannerViewModel> sources = BannerRepository.GetList(BannerTypeId, BannerPositionId, LanguageCode);
            return PartialView("../Contents/Banners/_List", sources);
        }


        // GET: /Admin/Banner/Details/5        
        [HttpGet]
        [SessionExpiration]
        public ActionResult Edit(int id)
        {
            BannerViewModel entity = BannerRepository.Details(id);
            if(entity.BannerCode !=null)
                entity.SelectedPageIds = BannerRepository.GetSelectedPageListByBannerId(entity.BannerId);
            return PartialView("../Contents/Banners/_Edit", entity);
        }

        // GET: /Admin/Banner/Create
        [SessionExpiration]
        public ActionResult Create()
        {
            BannerViewModel model = new BannerViewModel();
            model.PageList = PageRepository.PopulateActivePageSelectList(ScopeTypeId, LanguageCode,null, true);
            return PartialView("../Contents/Banners/_Create", model);
        }



        // POST: /Admin/Banner/Create
        [HttpPost]
        [SessionExpiration]
        public ActionResult Create(BannerViewModel add_model, bool chkContractStatus)
        {
            bool flag = false;
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    flag = BannerRepository.Insert(add_model, out message);
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


        [HttpPost]
        [SessionExpiration]
        public ActionResult Edit(BannerViewModel edit_model, bool chkContractStatus)
        {
            bool flag = false;
            string message = string.Empty;
            if (ModelState.IsValid)
            {               
                flag = BannerRepository.Update(edit_model, out message);
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
        [SessionExpiration]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            bool flag = BannerRepository.Delete(id, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

    }
}
