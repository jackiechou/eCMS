using Eagle.Common.Settings;
using Eagle.Common.Utilities;
using Eagle.Model;
using Eagle.Model.Common;
using Eagle.Model.SYS.Menu;
using Eagle.Model.SYS.Permission;
using Eagle.Repository;
using Eagle.Repository.Sys.Menus;
using Eagle.Repository.Sys.Permissions;
using Eagle.Repository.SYS;
using Eagle.Repository.SYS.Menus;
using Eagle.Repository.SYS.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        //
        // GET: /Admin/Menu/
        [SessionExpiration]
        public ActionResult Index()
        {
            ViewBag.ScopeTypeId = CommonRepository.PopulateScopeTypeList(ScopeTypeId.ToString(), false);
           return View("../Sys/Menu/Index");
        }


      

        #region LOAD MENU =====================================================================================================
        [SessionExpiration]
        [HttpGet]
        public JsonResult PopulateSiteMapByMenuCode(string MenuCode)
        {
            string result = string.Empty;
            MenuRepository _repository = new MenuRepository(db);
            result = _repository.PopulateSiteMapByMenuCode(MenuCode);
            return base.Json(result, JsonRequestBehavior.AllowGet);
        }

        [SessionExpiration]
        [HttpGet]
        public JsonResult PopulateSiteMapByMenuId(string MenuId)
        {
            string result = string.Empty;
            MenuRepository _repository = new MenuRepository(db);
            result = _repository.PopulateSiteMapByMenuId(MenuId);
            return base.Json(result, JsonRequestBehavior.AllowGet);
        }


        [SessionExpiration]
        [HttpGet]
        public JsonResult PopulateListBox(int? iScopeTypeId)
        {
            int _iScopeTypeId = (iScopeTypeId != null && iScopeTypeId > 0) ? (int)iScopeTypeId : ScopeTypeId;
            List<MenuTreeModel> sources = MenuRepository.GetListByScopeTypeId(_iScopeTypeId);
            return base.Json(sources, JsonRequestBehavior.AllowGet);
        }

        //[SessionExpiration]
        //public ActionResult LoadDesktopMenu()
        //{
        //    string strResult = string.Empty, MenuLink = string.Empty, Icon = string.Empty, IconUrl = string.Empty, IconClass = string.Empty;
        //    int? iPageId = 0;
        //    string signal = getUrl().ToLower();
        //    signal = signal == "home" ? "" : signal;
        //    int MenuTypeId = 0;

        //    if (ScopeTypeId == 1)
        //        MenuTypeId = 3;  //Host
        //    else if (ScopeTypeId == 2)
        //        MenuTypeId = 10; //Site Admin
        //    else
        //        MenuTypeId = 0;
        //    if (Session[SettingKeys.MenuList] == null || Session[SettingKeys.MenuList].ToString() == string.Empty)
        //    {
        //        string Status = ThreeStatusString.Active;
        //        List<MenuViewModel> menuList = MenuRepository.GetListByRoleIdScopeTypeIdStatus(RoleId, MenuTypeId, ScopeTypeId, Status);
        //        if (menuList != null && menuList.Count > 0)
        //        {
        //            // Tim nhung node cha truoc
        //            foreach (var item in menuList.Where(p => p.ParentId == null || p.ParentId == 0))
        //            {
        //                IconUrl = "<img style='width: 13px; height: 13px; margin-right: 2px;' sr='" + item.IconUrl + "' />";
        //                IconClass = (!string.IsNullOrEmpty(item.IconClass)) ? "<i class='" + item.IconClass + "'></i>" : "<i class='glyphicon glyphicon-th-large'></i>";

        //                Icon = (item.IconFile != null) ? IconUrl : IconClass;

        //                //MenuLink = (item.IsExtenalLink != null && item.IsExtenalLink == true) ? item.PageUrl : item.PagePath.ToLower();     
        //                if (item.IsExtenalLink != null && item.IsExtenalLink == true)
        //                    MenuLink = item.PageUrl;
        //                else
        //                {
        //                    if (!string.IsNullOrEmpty(item.PagePath))
        //                        MenuLink = item.PagePath.ToLower();
        //                }
        //                iPageId = (item.PageId != null) ? item.PageId : 0;
        //                MenuLink += "/" + iPageId + "/" + item.MenuId;

        //                strResult += "<li>";
        //                strResult += "<a href='" + MenuLink + "'><span> " + Icon + "  " + item.MenuTitle + "</span></a>";
        //                strResult += LoopPage(item.MenuId, menuList);
        //                strResult += "</li>";
        //            }
        //        }
        //        Session[SettingKeys.MenuList] = strResult;
        //    }
        //    else
        //    {
        //        strResult = Session[SettingKeys.MenuList].ToString();

        //    }
        //    strResult = strResult.Replace("/" + signal + "'", "/" + signal + "' isview='1' ");
        //    return PartialView("_MenuPartial", strResult);
        //}

        public ActionResult LoadDesktopMainMenu()
        {
            string strHTML = MenuRepository.LoadDesktopBootstrapMenu(13, 3);
            return PartialView("Desktop/_DesktopMainMenu", strHTML);
        }

        public ActionResult LoadJsonDesktopMainMenu()
        {
            string strHTML = MenuRepository.LoadDesktopMegaMenu(13, 3);
            return Json(strHTML, JsonRequestBehavior.AllowGet);
        }


        [ChildActionOnly]
        [SessionExpiration]
        public ActionResult LoadAdminMainMenu()
        {
            int MenuTypeId = 0;
            if (ScopeTypeId == 1)
                MenuTypeId = 3;  //Host
            else if (ScopeTypeId == 2)
                MenuTypeId = 10; //Site Admin
            else
                MenuTypeId = 15; //Desktop

            string strHTML = MenuRepository.LoadMenu(RoleId, MenuTypeId, ScopeTypeId);
            return PartialView("_MenuPartial", strHTML);
        }


        [ChildActionOnly]
        [SessionExpiration]
        public ActionResult LoadLeftMainMenu()
        {
            int MenuTypeId = 0;
            if (ScopeTypeId == 1)
                MenuTypeId = 3;  //Host
            else if (ScopeTypeId == 2)
                MenuTypeId = 10; //Site Admin
            else
                MenuTypeId = 15;//Desktop
            string strHTML = MenuRepository.LoadMenu(RoleId, MenuTypeId, ScopeTypeId);
            return PartialView("_LeftMainMenu", strHTML);
        }



        #endregion LOAD MENU ==================================================================================================


       #region XU LY INSERT UPDATE DELETE MENU ==================================================================================================
        [SessionExpiration]
        public ActionResult Create()
        {
            ViewBag.MenuTypeId = MenuTypeRepository.PopulateActiveMenuTypeSelectList(ScopeTypeId, LanguageCode, null, true);
            ViewBag.PageId = PageRepository.PopulateActivePageSelectList(ScopeTypeId, LanguageCode, null, true);
            ViewBag.MenuStatus = CommonRepository.GenerateThreeStatusModeList(null, false);
            ViewBag.Target = CommonRepository.PopulateLinkTargets(null, false);
            
            return PartialView("../Sys/Menu/_Edit");
        }


        [SessionExpiration]
        public ActionResult Edit(int id)
        {
            MenuViewModel entity = MenuRepository.GetDetails((int)id);
            ViewBag.MenuTypeId = MenuTypeRepository.PopulateActiveMenuTypeSelectList(ScopeTypeId, LanguageCode, entity.MenuTypeId.ToString(), true);
            ViewBag.PageId = PageRepository.PopulateActivePageSelectList(ScopeTypeId, LanguageCode, entity.PageId.ToString(), true);
            ViewBag.MenuStatus = CommonRepository.GenerateThreeStatusModeList(entity.MenuStatus.ToString(), false);
            ViewBag.Target = CommonRepository.PopulateLinkTargets(null, false);
           // ViewBag.ScopeType = CommonRepository.PopulateScopeTypeList(ScopeTypeId.ToString(), false);
            return PartialView("../Sys/Menu/_Edit", entity);
        }


        // POST: /Admin/Menu/Insert
        [HttpPost]
        public ActionResult Insert(MenuViewModel add_model)
        {
            bool flag = false;
            string message = string.Empty;
            int id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    flag = MenuRepository.Insert(add_model, out id, out message);
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

        // POST: /Admin/Menu/Update
        [HttpPost]
        public ActionResult Update(MenuViewModel edit_model)
        {
            bool flag = false;
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    flag = MenuRepository.Update(edit_model, out message);
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
        public ActionResult UpdatePosition(MenuPositionModel edit_model)
        {
            bool flag = false;
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    MenuRepository _repository = new MenuRepository(db);
                    edit_model.LastModifiedByUserId = UserId;
                    flag = _repository.UpdatePosition(edit_model, out message);
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
        // POST: /Admin/Menu/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            bool flag = MenuRepository.Delete(id, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }  
       #endregion XU LY INSERT UPDATE DELETE MENU ==================================================================================================


        //string xslt_filepath = "~/Areas/Admin/Views/Home/MenuTransformer.xslt";
        //public string ExecuteXSLTransformation(int LanguageId = 41)
        //{
        //    string ErrorMsg, HtmlTags = string.Empty, XsltPath;
        //    MemoryStream DataStream = default(MemoryStream);
        //    StreamReader streamReader = default(StreamReader);


        //    byte[] bytes = null;
        //    //Path of XSLT file            
        //    XsltPath = System.Web.HttpContext.Current.Server.MapPath(xslt_filepath);

        //    if (System.IO.File.Exists(XsltPath))
        //    {

        //        //Encode all Xml format string to bytes
        //        MenuRepository _repository = new MenuRepository(db);
        //        string strXML = _repository.GenerateXmlFormat(LanguageId);
        //        if (strXML != string.Empty)
        //        {
        //            try
        //            {

        //                bytes = Encoding.UTF8.GetBytes(strXML);

        //                DataStream = new MemoryStream(bytes);

        //                //Create Xmlreader from memory stream

        //                XmlReader reader = XmlReader.Create(DataStream);

        //                // Load the XML 
        //                XPathDocument document = new XPathDocument(reader);


        //                XslCompiledTransform XsltFormat = new XslCompiledTransform();

        //                // Load the style sheet.
        //                XsltFormat.Load(XsltPath);

        //                DataStream = new MemoryStream();

        //                XmlTextWriter writer = new XmlTextWriter(DataStream, Encoding.UTF8);


        //                //Apply transformation from xml format to html format and save it in xmltextwriter
        //                XsltFormat.Transform(document, writer);

        //                streamReader = new StreamReader(DataStream);

        //                DataStream.Position = 0;

        //                HtmlTags = streamReader.ReadToEnd();
        //            }
        //            catch (Exception ex)
        //            {
        //                ErrorMsg = ex.Message;
        //                return ErrorMsg;
        //            }
        //            //finally
        //            //{
        //            //    //Release the resources 
        //            //    streamReader.Close();
        //            //    DataStream.Close();
        //            //}

        //        }

        //    }
        //    return HtmlTags;
        //}



    }
}
