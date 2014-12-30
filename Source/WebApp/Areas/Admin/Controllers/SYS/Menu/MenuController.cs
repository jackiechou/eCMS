using Eagle.Common.Settings;
using Eagle.Model;
using Eagle.Model.Common;
using Eagle.Model.SYS.Menu;
using Eagle.Model.SYS.Permission;
using Eagle.Repository;
using Eagle.Repository.Sys.Menus;
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
           return View("../Sys/Menu/Index");
        }

       
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
        public JsonResult PopulateListBox()
        {  
            //List<MenuModel> sources = MenuRepository.GetTreeList(ScopeTypeId);
            //return PartialView("../Sys/Menu/_List", sources);

            List<MenuTreeModel> sources = MenuRepository.GetTreeList(ScopeTypeId);
            return base.Json(sources, JsonRequestBehavior.AllowGet);
            //return PartialView("../Sys/Menu/_List", sources);
        }
        
        [SessionExpiration]
        public ActionResult Edit(int? MenuId)
        {
            MenuViewModel entity = new MenuViewModel();
            if(MenuId > 0)
                entity = MenuRepository.GetDetails((int)MenuId);
            ViewBag.MenuTypeId = MenuTypeRepository.PopulateActiveMenuTypeSelectList(ScopeTypeId, LanguageCode, entity.MenuTypeId.ToString(), true);
            ViewBag.PageId = PageRepository.PopulateActivePageSelectList(ScopeTypeId, LanguageCode, entity.PageId.ToString(), true);
            
            return PartialView("../Sys/Menu/_Edit", entity);
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
        //                IconClass = (!string.IsNullOrEmpty(item.IconClass)) ? "<i class='" + item.IconClass + "'></i>" : "<i class='icon-th-large'></i>";

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


        //public static void AddMenuPermission(List<MenuPermissionInfo> lstMenuPermissions, int MenuID, int PortalID)
        //{
        //    try
        //    {
        //        MenuManagerDataProvider.AddMenuPermission(lstMenuPermissions, MenuID, PortalID);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

    }
}
