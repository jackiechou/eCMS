using Eagle.Common;
using Eagle.Core;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.Common;
using Eagle.Common.Extensions.DataGrid;
using Eagle.Repository.SYS;
using Eagle.Repository.Common;
using Eagle.Model.SYS.Pages;
using Eagle.Repository.Sys.Languages;
using Eagle.Repository.SYS.Session;
using Eagle.Common.Utilities;
using Eagle.Repository.SYS.Contents;
using CommonLibrary.UI.Skins;

namespace Eagle.WebApp.Areas.Admin.Controllers.SYS
{
    public class PageController : BaseController
    {

       

        //
        // GET: /Administrator/Page/

        public ActionResult Index()
        {
            //AccessFunctionPolicyViewModel PageAccess = CheckPageAccess(Request.CurrentExecutionFilePath);
            //if (PageAccess == null || PageAccess.Read == 0)
            //{
            //    return Redirect("/Administrator/Login");
            //}
            //else
            //{
            //Session[ConstantClass.Session_Read] = PageAccess.Read;
            //Session[ConstantClass.Session_Write] = PageAccess.Write;
            //Session[ConstantClass.Session_Edit] = PageAccess.Edit;

           // ViewBag.ParentId_Combobox = PageRepository.PopulateSelecuredListByScopeTypeIdAndStatus(ScopeTypeId, null, false);
            //return View();
            ViewBag.LanguageCode = LanguageRepository.PopulateActiveLanguages(null, false);
            return View("../Sys/Pages/Index");
        }

        // GET: /Admin/Contract/Create
        [HttpGet]
        [SessionExpiration]
        public ActionResult Create()
        {
            PageViewModel model = new PageViewModel();
            ViewBag.ContentItemId = ContentItemRepository.PopulateContentItemsByPageToDropDownList(null, false);
            ViewBag.SkinId = SkinRepository.PopulateActiveSkinSelectList(model.SkinId.ToString(), false);
            ViewBag.LanguageCode = LanguageRepository.PopulateActiveLanguages(null, false);
            return PartialView("../Sys/Pages/_Edit", model);
        }


        [HttpGet]
        [SessionExpiration]
        public ActionResult Edit(int Id)
        {
            PageViewModel model = PageRepository.GetDetails(Id);
            ViewBag.ContentItemId = ContentItemRepository.PopulateContentItemsByPageToDropDownList(model.ContentItemId.ToString(), false);
            ViewBag.SkinId = SkinRepository.PopulateActiveSkinSelectList(model.SkinId.ToString(), false);
            ViewBag.LanguageCode = LanguageRepository.PopulateActiveLanguages(null, false);
            return PartialView("../Sys/Pages/_Edit", model);
        }

       
        [SessionExpiration]
        public ActionResult List()
        {
            List<PageViewModel> sources = PageRepository.GetListByScopeTypeId(ScopeTypeId);
            return PartialView("../Sys/Pages/_List", sources);
        }

        [SessionExpiration]
        public JsonResult GetList(string Keywords, bool? IsSecured)
        {
            List<PageViewModel> lst = PageRepository.GetListByKeywordsScopeTypeIdAndStatus(Keywords, ScopeTypeId, IsSecured);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [SessionExpiration]
        public JsonResult UpdateListOrder(string Ids)
        {
            string message = string.Empty;
            bool flag = false;
            List<int> lst = Ids.Split(',').Select(s => int.Parse(s)).ToList();
            if (lst.Count() > 0)
            {
                int id = 0;
                for (int i = 0; i < lst.Count(); i++)
                {
                    id = lst[i];
                    flag = PageRepository.UpdateListOrder(id, i + 1, out message);
                }
            }           
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        [SessionExpiration]
        [HttpPost]
        public ActionResult UpdateStatus(int Id, bool Status)
        {
            string message = string.Empty;
            bool flag = PageRepository.UpdateStatus(Id, Status, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult Main(GridSettings GridRequest, PageViewModel obj)
        {
            try
            {
                List<PageViewModel> sources = PageRepository.GetAll(obj, LanguageCode.ToString());

                var list = sources.AsQueryable();

                // Xu ly doan search
                if (null != GridRequest.where && GridRequest.where.rules.Count() > 0)
                {
                    string strWhere = LinqExtensionsMethod.GetAllStringFiltersExpression(GridRequest);
                    if (string.IsNullOrEmpty(strWhere) == false)
                    {
                        list = list.Where<PageViewModel>(strWhere);
                    }
                }

                int totalrecord = list.Count();

                return Json(new
                {
                    pageSize = totalrecord,
                    page = 1,
                    total = 1,
                    records = totalrecord,
                    rows = (from record in list
                            // where staff.STAFF_ID != Session[ConstantClass.Session_USER_ID].ToString()
                            select new
                            {
                                id = record.PageId,
                                cell = new[] {
                                    record.PageId.ToString(),
                                    record.PageTitle,
                                    record.PageName,
                                    record.ParentList,
                                    record.PageUrl,
                                    record.ListOrder.ToString(),
                                    record.Icon.ToString(),
                                    record.IsVisible.ToString(),
                                    record.tree_level,
                                    record.tree_parent,
                                    record.tree_isLeaf,
                                    "true"
                                }
                            }
                     )
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ActionResult UpdatePage(PageViewModel rowdata)
        {
            return Json(PageRepository.Update(rowdata, (int)Session[CommonRepository.Session_Write], (int)Session[CommonRepository.Session_Edit]), JsonRequestBehavior.AllowGet);
        }
    }
}
