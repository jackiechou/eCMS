using Eagle.Common.Settings;
using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Model.Common;
using Eagle.Model.SYS.Menu;
using Eagle.Repository.Sys.Menus;
using Eagle.Repository.Sys.Permissions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Eagle.Repository.SYS.Menus
{
    public class MenuRepository
    {
        SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable();


        public EntityDataContext context { get; set; }
        public MenuRepository(EntityDataContext context)
        {
            this.context = context;
        }

        #region HIEARACHY IEnumerable ================================================================
        //public static IEnumerable<NodeDataIEnumerable> PopulateNormalList(FlatData[] elements)
        //{
        //    IEnumerable<NodeDataIEnumerable> nodes = elements.RecursiveJoin(element => element.MenuId,
        //       element => element.ParentId,
        //       (FlatData element, IEnumerable<NodeDataIEnumerable> children) => new NodeDataIEnumerable()
        //       {
        //           MenuName = element.MenuName,
        //           Children = children
        //       });
        //    return nodes;
        //}
        //public static IEnumerable<DeepNodeDataIEnumerable> PopulateDeepList(FlatData[] elements)
        //{
        //    IEnumerable<DeepNodeDataIEnumerable> nodes = elements.RecursiveJoin(element => element.MenuId,
        //       element => element.ParentId,
        //       (FlatData element, int index, int depth, IEnumerable<DeepNodeDataIEnumerable> children) =>
        //       {
        //           return new DeepNodeDataIEnumerable()
        //           {
        //               MenuName = element.MenuName,
        //               Children = children,
        //               Depth = depth
        //           };
        //       });
        //    return nodes;
        //}
        #endregion HIEARACHY IEnumerable =============================================================

        #region HIEARACHY List =======================================================================
        public static List<NodeData> PopulateHierachyList(int TypeId, int Status, bool IsAdmin)
        {
            List<Menu> list = GetMenuListByTypeIdStatus(TypeId, Status);
            //Output recursive data to list :           
            var recursiveObjects = FillRecursive(list, 0);
            return recursiveObjects;
        }
        private static List<NodeData> FillRecursive(List<Menu> elements, int parentId)
        {
            List<NodeData> recursiveObjects = new List<NodeData>();

            foreach (var item in elements.Where(x => x.ParentId.Equals(parentId)))
            {
                recursiveObjects.Add(new NodeData
                {
                    MenuId = item.MenuId,
                    ParentId = item.ParentId,
                    TypeId = item.MenuTypeId,
                    ListOrder = item.ListOrder,
                    PageId = (int)item.PageId,
                    MenuName = item.MenuName,
                    Alias = item.MenuAlias,
                    Depth = (int)item.Depth,
                    IconFile = (int)item.IconFile,
                    IconClass = item.IconClass,
                    CssClass = item.CssClass,
                    Status = item.MenuStatus,
                    Description = item.Description,
                    Children = FillRecursive(elements, item.MenuId)
                });
            }

            return recursiveObjects;
        }
        #endregion ===================================================================================

        #region MENU - XSLT ======================================================================================================================
        public string GenerateXmlFormat(int TypeId, int Status, bool IsAdmin)
        {
            string strResult = string.Empty;
            List<NodeData> menu_list = PopulateHierachyList(TypeId, Status, IsAdmin);
            if (menu_list.Count > 0)
            {
                var elements = from x in menu_list
                               select new XElement("Menu",
                                        new XElement("MenuId", x.MenuId),
                                        new XElement("ParentId", x.ParentId),
                                        new XElement("Depth", x.Depth),
                                        new XElement("TypeId", x.TypeId),
                                        new XElement("ListOrder", x.ListOrder),
                                        new XElement("MenuName", x.MenuName),
                                        new XElement("Alias", x.Alias),
                                        new XElement("PageId", x.PageId),
                                        new XElement("Target", x.Target),
                                        new XElement("IconFile", x.IconFile),
                                        new XElement("CssClass", x.CssClass),
                                        new XElement("Description", x.Description),
                                        new XElement("Status", x.Status)
                                );

                XDocument document = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment("XML Source Data for Menu"),
                    new XElement("Menus", elements));

                strResult = document.ToString();
            }
            return strResult;
        }

        public static string ConvertListToXMLFormat(List<Menu> menu_list)
        {
            string strResult = string.Empty;
            if (menu_list.Count > 0)
            {
                var elements = new XElement("Menus", from x in menu_list
                                                     select new XElement("Menu",
                                                             new XElement("MenuId", x.MenuId),
                                                             new XElement("ParentId", x.ParentId),
                                                             new XElement("TypeId", x.MenuTypeId),
                                                             new XElement("Depth", x.Depth),
                                                             new XElement("SortKey", x.ListOrder),
                                                             new XElement("Name", x.MenuName),
                                                             new XElement("Alias", x.MenuAlias),
                                                             new XElement("PageId", x.PageId),
                                                             new XElement("Target", x.Target),
                                                             new XElement("IconFile", x.IconFile),
                                                             new XElement("IconClass", x.IconClass),
                                                             new XElement("CssClass", x.CssClass),
                                                             new XElement("Description", x.Description),
                                                             new XElement("Status", x.MenuStatus)
                                                     )
                                            );
                XDocument document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), elements);

                strResult = document.ToString();
            }
            return strResult;
        }

        public string GenerateXmlFormat_Stored(string RoleId, string UserId, int TypeId, string Status)
        {
            string result = string.Empty;
            SqlCommand cmd = new SqlCommand("Menu_GetHierarchicalTreeByRoleIdUserIdTypeIdStatus", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@RoleId", RoleId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@TypeId", TypeId);
            cmd.Parameters.AddWithValue("@Status", Status);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);


            if (ds.Tables.Count > 0)
            {
                ds.DataSetName = "Menus";
                ds.Tables[0].TableName = "Menu";
                //create Relation Parent and Child
                DataRelation relation = new DataRelation("ParentChild", ds.Tables[0].Columns["MenuId"], ds.Tables[0].Columns["ParentId"], false);
                relation.Nested = true;
                ds.Relations.Add(relation);
                result = ds.GetXml();
            }
            con.Close();
            return result;
        }

        public string GenerateXmlFormat_ByTypeId(int TypeId)
        {
            string result = string.Empty;
            DataSet ds = GetHierarchicalTreeByTypeId(TypeId);
            if (ds.Tables.Count > 0)
            {
                ds.DataSetName = "Menus";
                ds.Tables[0].TableName = "Menu";
                //create Relation Parent and Child
                DataRelation relation = new DataRelation("ParentChild", ds.Tables[0].Columns["MenuId"], ds.Tables[0].Columns["ParentId"], false);
                relation.Nested = true;
                ds.Relations.Add(relation);
                result = ds.GetXml();
            }
            con.Close();
            return result;
        }

        public string ExecuteMenuToXSLTransformationByTypeId(int TypeId, string xslt_filepath)
        {
            string ErrorMsg, HtmlTags = string.Empty, XsltPath;
            MemoryStream DataStream = default(MemoryStream);
            StreamReader streamReader = default(StreamReader);


            byte[] bytes = null;
            //Path of XSLT file
            XsltPath = HttpContext.Current.Server.MapPath(xslt_filepath);

            if (File.Exists(XsltPath))
            {

                //Encode all Xml format string to bytes
                string strXML = GenerateXmlFormat_ByTypeId(TypeId);
                if (strXML != string.Empty)
                {
                    try
                    {

                        bytes = Encoding.UTF8.GetBytes(strXML);

                        DataStream = new MemoryStream(bytes);

                        //Create Xmlreader from memory stream

                        XmlReader reader = XmlReader.Create(DataStream);

                        // Load the XML 
                        XPathDocument document = new XPathDocument(reader);


                        XslCompiledTransform XsltFormat = new XslCompiledTransform();

                        // Load the style sheet.
                        XsltFormat.Load(XsltPath);

                        DataStream = new MemoryStream();

                        XmlTextWriter writer = new XmlTextWriter(DataStream, Encoding.UTF8);


                        //Apply transformation from xml format to html format and save it in xmltextwriter
                        XsltFormat.Transform(document, writer);

                        streamReader = new StreamReader(DataStream);

                        DataStream.Position = 0;

                        HtmlTags = streamReader.ReadToEnd();
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg = ex.Message;
                        return ErrorMsg;
                    }
                    //finally
                    //{
                    //    //Release the resources 
                    //    streamReader.Close();
                    //    DataStream.Close();
                    //}

                }

            }
            return HtmlTags;
        }

        public string ExecuteXSLTransformation(string RoleId, string UserId, int TypeId, string Status, string xslt_filepath)
        {
            string ErrorMsg, HtmlTags = string.Empty, XsltPath;
            MemoryStream DataStream = default(MemoryStream);
            StreamReader streamReader = default(StreamReader);


            byte[] bytes = null;
            //Path of XSLT file
            XsltPath = HttpContext.Current.Server.MapPath(xslt_filepath);

            if (File.Exists(XsltPath))
            {

                //Encode all Xml format string to bytes
                string strXML = GenerateXmlFormat_Stored(RoleId, UserId, TypeId, Status);
                if (strXML != string.Empty)
                {
                    try
                    {

                        bytes = Encoding.UTF8.GetBytes(strXML);

                        DataStream = new MemoryStream(bytes);

                        //Create Xmlreader from memory stream

                        XmlReader reader = XmlReader.Create(DataStream);

                        // Load the XML 
                        XPathDocument document = new XPathDocument(reader);


                        XslCompiledTransform XsltFormat = new XslCompiledTransform();

                        // Load the style sheet.
                        XsltFormat.Load(XsltPath);

                        DataStream = new MemoryStream();

                        XmlTextWriter writer = new XmlTextWriter(DataStream, Encoding.UTF8);


                        //Apply transformation from xml format to html format and save it in xmltextwriter
                        XsltFormat.Transform(document, writer);

                        streamReader = new StreamReader(DataStream);

                        DataStream.Position = 0;

                        HtmlTags = streamReader.ReadToEnd();
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg = ex.Message;
                        return ErrorMsg;
                    }
                    //finally
                    //{
                    //    //Release the resources 
                    //    streamReader.Close();
                    //    DataStream.Close();
                    //}

                }

            }
            return HtmlTags;
        }
        #endregion ===============================================================================================================================


        #region Desktop Mega Menu ====================================================================================================================
        public static string LoadDesktopMegaMenu(int MenuTypeId, int ScopeTypeId)
        {
            string strHtml = string.Empty, strResult = string.Empty, MenuLink = string.Empty, Icon = string.Empty, IconUrl = string.Empty, IconClass = string.Empty, liStyle = string.Empty; ;
            int? iPageId = 0;
            string signal = GetUrl().ToLower();
            signal = signal == "home" ? "" : signal;

            if (HttpContext.Current.Session[SettingKeys.MenuList] == null || HttpContext.Current.Session[SettingKeys.MenuList].ToString() == string.Empty)
            {
                List<MenuViewModel> menuList = MenuRepository.GetListByScopeTypeStatus(MenuTypeId, ScopeTypeId, ThreeStatusString.Active);
                if (menuList != null && menuList.Count > 0)
                {
                    bool first = true;
                    var parentList = menuList.Where(p => p.ParentId == null || p.ParentId == 0);
                    if (parentList.Count() > 0)
                    {
                        int i = 1;
                        foreach (var item in parentList)
                        {
                            if (first)
                            {
                                liStyle = "class='current'";
                                first = false;
                            }
                            else
                                liStyle = string.Empty;

                            IconUrl = "<img style='width: 13px; height: 13px; margin-right: 2px;' sr='" + item.IconUrl + "' />";
                            IconClass = (!string.IsNullOrEmpty(item.IconClass)) ? "<i class='" + item.IconClass + "'></i>" : "<i class='glyphicon glyphicon-th-large'></i>";

                            Icon = (item.IconFile != null) ? IconUrl : IconClass;

                            if (item.IsExtenalLink != null && item.IsExtenalLink == true)
                                MenuLink = item.PageUrl;
                            else
                            {
                                if (!string.IsNullOrEmpty(item.PagePath))
                                    MenuLink = item.PagePath.ToLower();
                            }
                            iPageId = (item.PageId != null) ? item.PageId : 0;
                            MenuLink += "/" + iPageId + "/" + item.MenuId;

                            strResult += "<li " + liStyle + ">";                           
                            strResult += "<a href='" + MenuLink + "' >" + Icon + "  " + item.MenuTitle + "</a>";
                            strResult += LoopMegaMenu(item.MenuId, item.MenuTitle, menuList);
                            strResult += "</li>";
                        }
                    }

                    strHtml = "<ul id='nav' style='display: -moz-box; width: 100%' class='sf-menu'>" + strResult + "</ul>";
                }
                HttpContext.Current.Session[SettingKeys.MenuList] = strHtml;
            }
            else
                strHtml = HttpContext.Current.Session[SettingKeys.MenuList].ToString();
            return strHtml;
        }
        private static string LoopMegaMenu(int PageId, string PageName, List<MenuViewModel> lst)
        {
            string strHtml = string.Empty, strResult = string.Empty, MenuName= string.Empty, MenuLink = string.Empty, Icon = string.Empty, IconUrl = string.Empty, IconClass = string.Empty, divStyle = string.Empty;
            int? iPageId = 0;
            var _lst = lst.Where(p => p.ParentId == PageId);
            if (_lst != null && _lst.Count() > 0)
            {
                foreach (var item in _lst)
                {
                    IconUrl = "<img style='width: 13px; height: 13px; margin-right: 2px;' sr='" + item.IconUrl + "' />";
                    IconClass = (!string.IsNullOrEmpty(item.IconClass)) ? "<i class='" + item.IconClass + "'></i>" : "<i class='glyphicon glyphicon-th-large'></i>";

                    Icon = (item.IconFile != null) ? IconUrl : IconClass;
                    if (item.IsExtenalLink != null && item.IsExtenalLink == true)
                        MenuLink = item.PageUrl;
                    else
                    {
                        if (!string.IsNullOrEmpty(item.PagePath))
                            MenuLink = item.PagePath.ToLower();
                    }

                    iPageId = (item.PageId != null) ? item.PageId : 0;
                    MenuLink += "/" + PageId + "/" + item.MenuId;
                    MenuName = item.MenuTitle;
          
                    strResult += "<div class='sf-mega'>";
                    strResult += "<div class='sf-mega-section'>";
                    strResult += "<h2>" + MenuName + "</h2>";
                    strResult += "<ul>";
                    strResult += "<li>";
                    strResult += "<a href='" + MenuLink + "'><span> " + Icon + "  " + item.MenuTitle + "</span></a>";
                    strResult += LoopMegaMenu(item.MenuId, item.MenuTitle, lst);
                    strResult += "</li>";
                    strResult += "</ul>";
                    strResult += "</div>";
                    strResult += "</div>";         
                }
            }
            return strResult;
        }

        private static string LoopSubMegaMenu(int PageId, string PageName, List<MenuViewModel> lst)
        {
            string strHtml = string.Empty, strResult = string.Empty, MenuName = string.Empty, MenuLink = string.Empty, Icon = string.Empty, IconUrl = string.Empty, IconClass = string.Empty, divStyle = string.Empty;
            int? iPageId = 0;
            var _lst = lst.Where(p => p.ParentId == PageId);
            if (_lst != null && _lst.Count() > 0)
            {

                foreach (var item in _lst)
                {
                    IconUrl = "<img style='width: 13px; height: 13px; margin-right: 2px;' sr='" + item.IconUrl + "' />";
                    IconClass = (!string.IsNullOrEmpty(item.IconClass)) ? "<i class='" + item.IconClass + "'></i>" : "<i class='glyphicon glyphicon-th-large'></i>";

                    Icon = (item.IconFile != null) ? IconUrl : IconClass;
                    if (item.IsExtenalLink != null && item.IsExtenalLink == true)
                        MenuLink = item.PageUrl;
                    else
                    {
                        if (!string.IsNullOrEmpty(item.PagePath))
                            MenuLink = item.PagePath.ToLower();
                    }

                    iPageId = (item.PageId != null) ? item.PageId : 0;
                    MenuLink += "/" + PageId + "/" + item.MenuId;
                    MenuName = item.MenuTitle;

                    strResult += "<div class='sf-mega-section'>";
                    strResult += "<h2>" + MenuName + "</h2>";
                    strResult += "<ul>";
                    strResult += "<li>";
                    strResult += "<a href='" + MenuLink + "'><span> " + Icon + "  " + item.MenuTitle + "</span></a>";
                    strResult += LoopSubMegaMenu(item.MenuId, item.MenuTitle, lst);
                    strResult += "</li>";
                    strResult += "</ul>";
                    strResult += "</div>";
                }
            }
            return strResult;
        }
        #endregion =================================================================================================================================

        #region Desktop Bootstrap Menu =============================================================================================================
        public static string LoadDesktopBootstrapMenu(int MenuTypeId, int ScopeTypeId)
        {
            string strHtml = string.Empty, strResult = string.Empty, MenuLink = string.Empty, Icon = string.Empty, IconUrl = string.Empty, IconClass = string.Empty, liStyle = string.Empty; ;
            int? iPageId = 0;
            string signal = GetUrl().ToLower();
            signal = signal == "home" ? "" : signal;

            if (HttpContext.Current.Session[SettingKeys.MenuList] == null || HttpContext.Current.Session[SettingKeys.MenuList].ToString() == string.Empty)
            {
                List<MenuViewModel> menuList = MenuRepository.GetListByScopeTypeStatus(MenuTypeId, ScopeTypeId, ThreeStatusString.Active);
                if (menuList != null && menuList.Count > 0)
                {
                    bool first = true;
                    var parentList = menuList.Where(p => p.ParentId == null || p.ParentId == 0);
                    if (parentList.Count() > 0)
                    {
                        int i=1;
                        foreach (var item in parentList)
                        {
                            if (first)
                            {
                                liStyle = "class='current'";
                                first = false;
                            }
                            else
                                liStyle = string.Empty;

                            IconUrl = "<img style='width: 13px; height: 13px; margin-right: 2px;' sr='" + item.IconUrl + "' />";
                            IconClass = (!string.IsNullOrEmpty(item.IconClass)) ? "<i class='" + item.IconClass + "'></i>" : "<i class='glyphicon glyphicon-th-large'></i>";

                            Icon = (item.IconFile != null) ? IconUrl : IconClass;

                            if (item.IsExtenalLink != null && item.IsExtenalLink == true)
                                MenuLink = item.PageUrl;
                            else
                            {
                                if (!string.IsNullOrEmpty(item.PagePath))
                                    MenuLink = item.PagePath.ToLower();
                            }
                            iPageId = (item.PageId != null) ? item.PageId : 0;
                            MenuLink += "/" + iPageId + "/" + item.MenuId;

                            strResult += "<div id='navbar-collapse-" + (i++) + "' class='navbar-collapse collapse'>";
                            strResult += "<ul class='nav navbar-nav'>";       
                            strResult += "<li class='dropdown'>";
                            if(item.HasChild)
                                strResult += "<a href='" + MenuLink + "' data-toggle='dropdown' class='dropdown-toggle'>" + Icon + "  " + item.MenuTitle + "<b class='caret'></b></a>";
                            else
                                strResult += "<a href='" + MenuLink + "' >" + Icon + "  " + item.MenuTitle + "</a>";
                            strResult += LoopMegaBootstrapMenu(item.MenuId, item.MenuTitle, menuList);
                            strResult += "</li>";
                            strResult += "</ul>";
                            strResult += "</div>";
                        }
                    }

                    strHtml = "<div class='navbar yamm navbar-default'><div class='container'>" + strResult + "</div></div>";
                }
                HttpContext.Current.Session[SettingKeys.MenuList] = strHtml;
            }
            else
                strHtml = HttpContext.Current.Session[SettingKeys.MenuList].ToString();
            return strHtml;
        }

        private static string LoopMegaBootstrapMenu(int PageId, string PageName, List<MenuViewModel> lst)
        {
            string strHtml = string.Empty, strResult = string.Empty, MenuLink = string.Empty, Icon = string.Empty, IconUrl = string.Empty, IconClass = string.Empty, divStyle = string.Empty;
            int? iPageId = 0;
            var _lst = lst.Where(p => p.ParentId == PageId);
            if (_lst != null && _lst.Count() > 0)
            {                
                foreach (var item in _lst)
                {
                    IconUrl = "<img style='width: 13px; height: 13px; margin-right: 2px;' sr='" + item.IconUrl + "' />";
                    IconClass = (!string.IsNullOrEmpty(item.IconClass)) ? "<i class='" + item.IconClass + "'></i>" : "<i class='glyphicon glyphicon-th-list'></i>";

                    Icon = (item.IconFile != null) ? IconUrl : IconClass;
                    if (item.IsExtenalLink != null && item.IsExtenalLink == true)
                        MenuLink = item.PageUrl;
                    else
                    {
                        if (!string.IsNullOrEmpty(item.PagePath))
                            MenuLink = item.PagePath.ToLower();
                    }

                    iPageId = (item.PageId != null) ? item.PageId : 0;
                    MenuLink += "/" + PageId + "/" + item.MenuId;

                    strResult += "<ul class='dropdown-menu'>";
                    strResult += "<li>";
                    strResult += "<div class='yamm-content'>";
                    strResult += "<div class='row'>";
                    strResult += "<ul class='col-sm-4 list-unstyled'>";
                    strResult += "<li>";
                    strResult += "<a href='" + MenuLink + "'><span> " + Icon + "  " + item.MenuTitle + "</span></a>";
                    strResult += LoopSubMegaBootstrapMenu(item.MenuId, item.MenuTitle, lst);
                    strResult +="</li>";
                    strResult += "</ul>";          
                    strResult += "</div>";
                    strResult += "</div>";
                    strResult += "</li>";
                    strResult += "</ul>";
                }
            }
            return strResult;
        }

        private static string LoopSubMegaBootstrapMenu(int PageId, string PageName, List<MenuViewModel> lst)
        {
            string strHtml = string.Empty, strResult = string.Empty, MenuLink = string.Empty, Icon = string.Empty, IconUrl = string.Empty, IconClass = string.Empty, divStyle = string.Empty;
            int? iPageId = 0;
            var _lst = lst.Where(p => p.ParentId == PageId);
            if (_lst != null && _lst.Count() > 0)
            {

                foreach (var item in _lst)
                {
                    IconUrl = "<img style='width: 13px; height: 13px; margin-right: 2px;' sr='" + item.IconUrl + "' />";
                    IconClass = (!string.IsNullOrEmpty(item.IconClass)) ? "<i class='" + item.IconClass + "'></i>" : "<i class='glyphicon glyphicon-th-list'></i>";

                    Icon = (item.IconFile != null) ? IconUrl : IconClass;
                    if (item.IsExtenalLink != null && item.IsExtenalLink == true)
                        MenuLink = item.PageUrl;
                    else
                    {
                        if (!string.IsNullOrEmpty(item.PagePath))
                            MenuLink = item.PagePath.ToLower();
                    }

                    iPageId = (item.PageId != null) ? item.PageId : 0;
                    MenuLink += "/" + PageId + "/" + item.MenuId;
                   
                    strResult += "<ul>";
                    strResult += "<li class='list-unstyled'>";
                    strResult += "<a href='" + MenuLink + "'><span> " + Icon + "  " + item.MenuTitle + "</span></a>";                   
                    strResult += "</li>";
                    strResult += "</ul>";
                }
            }
            return strResult;
        }
        #endregion =======================================================================================================================

        #region Menu =====================================================================================================================
      
        public static string LoadMenu(int RoleId, int MenuTypeId, int ScopeTypeId)
        {
            string strResult = string.Empty, MenuLink = string.Empty, Icon = string.Empty, IconUrl = string.Empty, IconClass = string.Empty;
            int? iPageId = 0;
            string signal = GetUrl().ToLower();
            signal = signal == "home" ? "" : signal;

            if (HttpContext.Current.Session[SettingKeys.MenuList] == null || HttpContext.Current.Session[SettingKeys.MenuList].ToString() == string.Empty)
            {
                List<MenuViewModel> menuList = MenuRepository.GetListByRoleIdScopeTypeIdStatus(RoleId, MenuTypeId, ScopeTypeId, ThreeStatusString.Active);
                if (menuList != null && menuList.Count > 0)
                {
                    foreach (var item in menuList.Where(p => p.ParentId == null || p.ParentId == 0))
                    {
                        IconUrl = "<img style='width: 13px; height: 13px; margin-right: 2px;' sr='" + item.IconUrl + "' />";
                        IconClass = (!string.IsNullOrEmpty(item.IconClass)) ? "<i class='" + item.IconClass + "'></i>" : "<i class='glyphicon glyphicon-th-large'></i>";

                        Icon = (item.IconFile != null) ? IconUrl : IconClass;

                        if (item.IsExtenalLink != null && item.IsExtenalLink == true)
                            MenuLink = item.PageUrl;
                        else
                        {
                            if (!string.IsNullOrEmpty(item.PagePath))
                                MenuLink = item.PagePath.ToLower();
                        }
                        iPageId = (item.PageId != null) ? item.PageId : 0;
                        MenuLink += "/" + iPageId + "/" + item.MenuId;

                        strResult += "<li>";
                        strResult += "<a href='" + MenuLink + "'><span> " + Icon + "  " + item.MenuTitle + "</span></a>";
                        strResult += LoopPage(item.MenuId, menuList);
                        strResult += "</li>";
                    }
                }
                HttpContext.Current.Session[SettingKeys.MenuList] = strResult;
            }
            else
                strResult = HttpContext.Current.Session[SettingKeys.MenuList].ToString();            
            strResult = strResult.Replace("/" + signal + "'", "/" + signal + "' isview='1' ");
            return strResult;
        }
        private static string GetUrl()
        {
            var flag = false;
            foreach (var item in HttpContext.Current.Request.Url.AbsolutePath.Split('/'))
            {
                if (flag)
                    return item;
                if (item.ToLower().Contains("admin"))
                    flag = true;
            }
            return "";
        }
        private static string LoopPage(int PageId, List<MenuViewModel> lst)
        {
            string strResult = string.Empty, MenuLink = string.Empty, Icon = string.Empty, IconUrl = string.Empty, IconClass = string.Empty;
            int? iPageId = 0;
            var _lst = lst.Where(p => p.ParentId == PageId);
            if (_lst != null && _lst.Count() > 0)
            {
                strResult = "<ul style='display:none;'>";
                foreach (var item in lst.Where(p => p.ParentId == PageId))
                {
                    IconUrl = "<img style='width: 13px; height: 13px; margin-right: 2px;' sr='" + item.IconUrl + "' />";
                    IconClass = (!string.IsNullOrEmpty(item.IconClass)) ? "<i class='" + item.IconClass + "'></i>" : "<i class='glyphicon glyphicon-th-list'></i>";

                    Icon = (item.IconFile != null) ? IconUrl : IconClass;
                    //MenuLink = (item.IsExtenalLink != null && item.IsExtenalLink == true) ? item.PageUrl : item.PagePath;
                    if (item.IsExtenalLink != null && item.IsExtenalLink == true)
                        MenuLink = item.PageUrl;
                    else
                    {
                        if (!string.IsNullOrEmpty(item.PagePath))
                            MenuLink = item.PagePath.ToLower();
                    }

                    iPageId = (item.PageId != null) ? item.PageId : 0;
                    MenuLink += "/" + PageId + "/" + item.MenuId;

                    strResult += "<li>";
                    strResult += "<a href='" + MenuLink + "'><span> " + Icon + "  " + item.MenuTitle + "</span></a>";
                    strResult += LoopPage(item.MenuId, lst);
                    strResult += "</li>";
                }

                //strResult += "</li>";
                strResult += "</ul>";
            }
            return strResult;
        }
        #endregion =======================================================================================================================
        public static IEnumerable<MenuViewModel> Search(string strSearch)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    return context.Menus
                        .Where(p => p.MenuName.Contains(strSearch))
                        .OrderBy(p => p.ListOrder)
                        .Select(p => new MenuViewModel() { MenuTitle = p.MenuTitle, MenuId = p.MenuId, IconFile = p.IconFile, IconClass = p.IconClass, ListOrder = p.ListOrder });
                }
                catch
                {
                    return new List<MenuViewModel>();
                }
            }
        }

        public int UpdateMenuOrder(int MenuId, int ParentId, int Depth, int ListOrder)
        {
            SqlCommand cmd = new SqlCommand("Menu_UpdateOrderList", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@MenuId", MenuId);
            cmd.Parameters.AddWithValue("@ParentId", ParentId);
            cmd.Parameters.AddWithValue("@Depth", Depth);
            cmd.Parameters.AddWithValue("@ListOrder", ListOrder);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int, 2) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public static Menu GetDetailsById(int MenuId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                Menu entity = context.Menus.Where(x => x.MenuId == MenuId).FirstOrDefault();
                return entity;
            }
        }
        public static MenuViewModel GetDetails(int MenuId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    var query = (from m in context.Menus
                                 join t in context.MenuTypes on m.MenuTypeId equals t.MenuTypeId into menu_lst
                                 from menulist in menu_lst.DefaultIfEmpty()

                                 join p in context.Pages on m.PageId equals p.PageId into page_lst
                                 from pagelist in page_lst.DefaultIfEmpty()
                                 where m.MenuId == MenuId
                                 orderby m.ListOrder
                                 select new MenuViewModel
                                 {
                                     PageId = m.PageId,
                                     PageUrl = pagelist.PageUrl,
                                     IsExtenalLink = pagelist.IsExtenalLink,
                                     MenuTypeId = m.MenuTypeId,
                                     MenuId = m.MenuId,
                                     ParentId = m.ParentId,
                                     Depth = m.Depth,
                                     ListOrder = m.ListOrder,
                                     MenuName = m.MenuName,
                                     MenuTitle = m.MenuTitle,
                                     MenuAlias = m.MenuAlias,
                                     Target = m.Target,
                                     IconFile = m.IconFile,
                                     IconClass = m.IconClass,
                                     CssClass = m.CssClass,
                                     Description = m.Description,
                                     MenuStatus = m.MenuStatus
                                 }).FirstOrDefault();
                    return query;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    return new MenuViewModel();
                }
            }
        }

         public static MenuViewModel GetDetailsByMenuCode(Guid MenuCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    var query = (from m in context.Menus
                                 join t in context.MenuTypes on m.MenuTypeId equals t.MenuTypeId into menu_lst
                                 from menulist in menu_lst.DefaultIfEmpty()

                                 join p in context.Pages on m.PageId equals p.PageId into page_lst
                                 from pagelist in page_lst.DefaultIfEmpty()
                                 where m.MenuCode == MenuCode
                                 orderby m.ListOrder
                                 select new MenuViewModel
                                 {
                                     PageId = m.PageId,
                                     PageUrl = pagelist.PageUrl,
                                     IsExtenalLink = pagelist.IsExtenalLink,
                                     MenuTypeId = m.MenuTypeId,
                                     MenuId = m.MenuId,
                                     ParentId = m.ParentId,
                                     Depth = m.Depth,
                                     ListOrder = m.ListOrder,
                                     MenuName = m.MenuName,
                                     MenuTitle = m.MenuTitle,
                                     MenuAlias = m.MenuAlias,
                                     Target = m.Target,
                                     IconFile = m.IconFile,
                                     IconClass = m.IconClass,
                                     CssClass = m.CssClass,
                                     Description = m.Description,
                                     MenuStatus = m.MenuStatus
                                 }).FirstOrDefault();
                    return query;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    return new MenuViewModel();
                }
            }
        }

         public static List<MenuViewModel> GetListByScopeTypeStatus(int MenuTypeId, int ScopeTypeId, string Status)
         {
             using (EntityDataContext context = new EntityDataContext())
             {
                 List<MenuViewModel> lst = new List<MenuViewModel>();
                 string strCommand = @"EXEC Cms.Menu_GetListByTypeStatus @MenuTypeId = {0}, @ScopeTypeId = {1},@Status = {2}";
                 lst = context.Database.SqlQuery<MenuViewModel>(strCommand, MenuTypeId, ScopeTypeId, Status).ToList();
                 return lst;
             }
         }
        public static List<MenuViewModel> GetListByRoleIdScopeTypeIdStatus(int RoleId, int MenuTypeId, int ScopeTypeId, string Status)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<MenuViewModel> lst = new List<MenuViewModel>();
                string strCommand = @"EXEC Cms.Menu_GetListByRoleTypeStatus @RoleId={0},@MenuTypeId = {1}, @ScopeTypeId = {2},@Status = {3}";
                lst = context.Database.SqlQuery<MenuViewModel>(strCommand, RoleId, MenuTypeId, ScopeTypeId, Status).ToList();
                return lst;
            }
        }

         public static List<MenuViewModel> GetListByRoleIdUserIdTypeIdStatus(int RoleId, int UserId, int MenuTypeId, int ScopeTypeId, string Status)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<MenuViewModel> lst = new List<MenuViewModel>();
                string strCommand = @"EXEC Cms.Menu_GetListByRoleUserTypeStatus  @RoleId={0},@UserId={1},@MenuTypeId = {2}, @ScopeTypeId = {3},@Status = {4}";
                lst = context.Database.SqlQuery<MenuViewModel>(strCommand, RoleId, UserId, MenuTypeId, ScopeTypeId, Status).ToList();
                return lst;
            }
        }

        public static List<MenuViewModel> GetPublishedList(string LanguageCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    var query = (from m in context.Menus
                                 join t in context.MenuTypes on m.MenuTypeId equals t.MenuTypeId into menu_lst
                                 from menulist in menu_lst.DefaultIfEmpty()

                                 join p in context.Pages on m.PageId equals p.PageId into page_lst
                                 from pagelist in page_lst.DefaultIfEmpty()
                                 //where m.Status > 0
                                 orderby m.ListOrder
                                 select new MenuViewModel
                                 {
                                     PageId = m.PageId,
                                     PageUrl = pagelist.PageUrl,
                                     IsExtenalLink = pagelist.IsExtenalLink,
                                     MenuTypeId = m.MenuTypeId,
                                     MenuId = m.MenuId,
                                     ParentId = m.ParentId,
                                     Depth = m.Depth,
                                     ListOrder = m.ListOrder,
                                     MenuName = m.MenuName,
                                     MenuTitle = m.MenuTitle,
                                     MenuAlias = m.MenuAlias,
                                     Target = m.Target,
                                     IconFile = m.IconFile,
                                     IconClass = m.IconClass,
                                     CssClass = m.CssClass,
                                     Description = m.Description,
                                     MenuStatus = m.MenuStatus
                                 }).ToList();
                    return query;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    return new List<MenuViewModel>();
                }
            }
        }

        public static List<MenuTreeModel> GetListByScopeTypeId(int ScopeTypeId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<MenuTreeModel> list = context.Menus.Where(m => m.ScopeTypeId == ScopeTypeId).OrderBy(m => m.ListOrder).Select(m => new MenuTreeModel()
                {
                    id = m.MenuId,
                    key = m.MenuId,
                    parentId = m.ParentId,
                    name = m.MenuName,
                    text = m.MenuTitle,
                    title = m.MenuTitle,
                    tooltip = m.Description,
                    isParent = m.HasChild,
                    open = (m.ParentId == 0) ? true : false
                }).ToList();
                return list;
            }
        }

        public static List<MenuTreeModel> GetTreeList(int ScopeTypeId)
        { 
            using (EntityDataContext context = new EntityDataContext())
            {
                List<MenuTreeModel> list = context.Menus.Where(m => m.ScopeTypeId == ScopeTypeId).OrderBy(m => m.ListOrder).Select(m => new MenuTreeModel()
                    {
                        id = m.MenuId,
                        key = m.MenuId,
                        parentId = m.ParentId,
                        name = m.MenuName,
                        text = m.MenuTitle,
                        title = m.MenuTitle,
                        tooltip = m.Description,
                        isParent = m.HasChild,
                        open = (m.ParentId == 0)?true:false
                    }).ToList();
                List<MenuTreeModel> recursiveObjects = RecursiveFillTree(list, 0);
                return recursiveObjects; 
            }            
        }

        public static List<MenuTreeModel> RecursiveFillTree(List<MenuTreeModel> list, int? id)
        {
            List<MenuTreeModel> items = new List<MenuTreeModel>();
            List<MenuTreeModel> nodes = list.Where(m => m.parentId == id).Select(
               m => new MenuTreeModel
               {
                   id = m.id,
                   key = m.key,
                   parentId = m.parentId,
                   name = m.name,
                   text = m.text,
                   title = m.title,
                   tooltip = m.tooltip,
                   isParent = m.isParent,
                   open = m.open
               }).ToList();

            if (nodes.Count > 0)
            {
                foreach (var child in nodes)
                {
                    //child.children = RecursiveFillTree(list, child.id);
                    MenuTreeModel node = new MenuTreeModel()
                    {
                        id = child.id,
                        key = child.key,
                        parentId = child.parentId,
                        name = child.name,
                        text = child.text,
                        title = child.title,
                        tooltip = child.tooltip,
                        isParent = child.isParent,
                        open = child.open,
                        children = RecursiveFillTree(list, child.id)
                    };
                    items.Add(node);
                }                
            }
            return items;
        }
        public Menu Find(int id)
        {
            return context.Menus.Find(id);
        }   

        //=================================================================================================================================================================

        public string PopulateSiteMapByMenuCode(string MenuCode)
        {
            string strHTML = "", result = string.Empty;
            if (!string.IsNullOrEmpty(MenuCode) && MenuCode != "Index")
            {
                DataTable dt = GetParentNodesOfSelectedNodeByMenuCode(new Guid(MenuCode)).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string MenuLink = string.Empty;
                        string MenuTitle = dt.Rows[i]["MenuTitle"].ToString();
                        string PageUrl = dt.Rows[i]["PageUrl"].ToString();
                        bool? IsExtenalLink = Convert.ToBoolean(dt.Rows[i]["IsExtenalLink"]);
                        if (IsExtenalLink != null && IsExtenalLink == true)
                            MenuLink = PageUrl;
                        else
                            MenuLink = dt.Rows[i]["PagePath"].ToString().ToLower() + "/" + dt.Rows[i]["MenuId"].ToString();

                        strHTML += "<li>";
                        strHTML += "<a href='" + MenuLink + "'><span>" + MenuTitle + "</span></a> <small><i class='glyphicon glyphicon-chevron-right'></i></small>";
                        strHTML += "</li>";
                    }
                }
            }
            result = "<ul class='breadcrumb'>";
            result += "<li><a href='/Admin/Home/Index'>";
            result += "<span><i class='glyphicon glyphiconicon-home'></i> " + Eagle.Resource.LanguageResource.Home + "</span></a> <small><i class='glyphicon glyphicon-chevron-right'></i></small>";
            result += "</li>";
            result += strHTML;
            result += "</ul>";
            return result;
        }

        public string PopulateSiteMapByMenuId(string MenuId)
        {
            string strHTML = "", result = string.Empty;
            int _MenuId;
            bool flag = Int32.TryParse(MenuId, out _MenuId);
            if (flag)
            {
                DataTable dt = GetParentNodesOfSelectedNodeByMenuId(Convert.ToInt32(MenuId)).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string MenuLink = string.Empty;
                        string MenuTitle = dt.Rows[i]["MenuTitle"].ToString();
                        string PageUrl = dt.Rows[i]["PageUrl"].ToString();
                        bool? IsExtenalLink = Convert.ToBoolean(dt.Rows[i]["IsExtenalLink"]);
                        if (IsExtenalLink != null && IsExtenalLink == true)
                            MenuLink = PageUrl;
                        else
                            MenuLink = dt.Rows[i]["PagePath"].ToString().ToLower() + "/" + dt.Rows[i]["MenuId"].ToString();

                        strHTML += "<li>";
                        strHTML += "<a href='" + MenuLink + "'><span>" + MenuTitle + "</span></a> <small><i class='glyphicon glyphicon-chevron-right'></i></small>";
                        strHTML += "</li>";
                    }
                }
            }

            result = "<ul class='breadcrumb'>";
            result += "<li><a href='/Admin/Home/Index'>";
            result += "<span><i class='glyphicon glyphicon-home'></i> " + Eagle.Resource.LanguageResource.Home + "</span></a> <small><i class='glyphicon glyphicon-chevron-right'></i></small>";
            result += "</li>";
            result += strHTML;
            result += "</ul>";
            return result;
        }

        public DataSet GetParentNodesOfSelectedNodeByMenuCode(Guid MenuCode)
        {
            SqlCommand cmd = new SqlCommand("[Cms].[Menu_GetParentNodesOfSelectedNodeByMenuCode]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@MenuCode", MenuCode);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        public DataSet GetParentNodesOfSelectedNodeByMenuId(int? MenuId)
        {
            SqlCommand cmd = new SqlCommand("[Cms].[Menu_GetParentNodesOfSelectedNodeByMenuId]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@MenuId", MenuId);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        public DataSet GetParentNodesOfSelectedNodeByPagePath(string PagePath)
        {
            SqlCommand cmd = new SqlCommand("[Cms].[Menu_GetParentNodesOfSelectedNodeByPagePath]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@PagePath", PagePath);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        public DataSet GetHierarchicalTreeByTypeId(int TypeId)
        {
            SqlCommand cmd = new SqlCommand("Menu_GetHierarchicalTreeByTypeId", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@TypeId", TypeId);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        public static DataSet GetTreeListByRoleIdUserIdTypeIdStatus(int RoleId, int UserId, int MenuTypeId, int MenuStatus)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query_by_role = (from x in context.Menus
                                     join t in context.MenuTypes on x.MenuTypeId equals t.MenuTypeId
                                     join p in context.MenuPermissions on x.MenuId equals p.MenuId
                                     where x.MenuTypeId == MenuTypeId && x.MenuStatus == MenuStatus && p.RoleId == RoleId
                                     select x).ToList();
                var query_by_user = (from x in context.Menus
                                     join t in context.MenuTypes on x.MenuTypeId equals t.MenuTypeId
                                     join p in context.MenuPermissions on x.MenuId equals p.MenuId
                                     where x.MenuTypeId == MenuTypeId && x.MenuStatus == MenuStatus && p.UserId == UserId
                                     select x).ToList();

                var menu_list = query_by_role.Union(query_by_user).OrderBy(x => x.ListOrder).ToList();
                DataTable dt = Eagle.Common.Helpers.LinqHelper.ToDataTable(menu_list);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                return ds;            }
          
        }

        public static XmlDocument GetTreeMenuListByUserIdRoleIdTypeIdStatus(int RoleId, int UserId, int MenuTypeId, int MenuStatus)
        {
            DataSet ds = GetTreeListByRoleIdUserIdTypeIdStatus(RoleId, UserId, MenuTypeId, MenuStatus);
            // This relation will yield an "Orders" node in each Customer node
            DataRelation relation = new DataRelation("MenuRelation",// relation name
                ds.Tables[0].Columns["ParentId"],     // parent column
                ds.Tables[0].Columns["MenuId"]); // child column
            // Set the "Nested" property on all the relations we created
            relation.Nested = true;

            XmlDocument xd = new XmlDocument();
            xd.LoadXml(ds.GetXml());
            return xd;
        }

        public static List<Menu> GetListByUserIdRoleIdTypeIdStatus(int RoleId, int UserId, int MenuTypeId, int MenuStatus)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var menu_list_by_role = from x in context.Menus
                                        join y in context.MenuPermissions
                                        on x.MenuId equals y.MenuId
                                        where x.MenuTypeId == MenuTypeId && x.MenuStatus == MenuStatus && y.RoleId == RoleId
                                        select x;

                var menu_list_by_user = from x in context.Menus
                                        join y in context.MenuPermissions
                                        on x.MenuId equals y.MenuId
                                        where x.MenuTypeId == MenuTypeId && x.MenuStatus == MenuStatus && y.UserId == UserId
                                        select x;

                var menu_list = menu_list_by_role.Union(menu_list_by_user).OrderBy(x => x.ListOrder).ToList();
                return menu_list;
            }
        }

        public static List<Menu> GetMenuListByTypeIdStatus(int MenuTypeId, int MenuStatus)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var menu_list = (from x in context.Menus
                                 where x.MenuTypeId == MenuTypeId && x.MenuStatus == MenuStatus
                                 orderby x.ListOrder ascending
                                 select x).ToList();
                return menu_list;
            }
        }

        public static List<Menu> GetListByTypeIdParentIdStatus(int MenuTypeId, int ParentId, int MenuStatus)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = from x in context.Menus
                            orderby x.ListOrder ascending
                            where x.MenuTypeId == MenuTypeId
                                && x.MenuStatus == MenuStatus
                            select x;

                if (!string.IsNullOrEmpty(ParentId.ToString()) && ParentId > 0)
                    query = query.Where(x => x.ParentId == ParentId);
                return query.ToList();
            }
        }
           
        public static int GetLastItemInfoByMenuTypeId(int MenuTypeId)
        {
            int MenuId = 0;
            using (EntityDataContext context = new EntityDataContext())
            {
                var lst = (from x in context.Menus
                           join y in context.MenuTypes
                           on x.MenuTypeId equals y.MenuTypeId
                           orderby x.MenuId ascending
                           where x.MenuTypeId == MenuTypeId
                           select x.MenuId).ToList();
                if (lst.Count > 0)
                    MenuId = lst.Last();
                return MenuId;
            }
        }

        public int GetMenuLevel(int? MenuId)
        {
            SqlCommand cmd = new SqlCommand("Menu_GetMenuLevelByMenuId", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@MenuId", MenuId);
            cmd.Parameters.Add(new SqlParameter("@Depth", SqlDbType.Int, 2) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int Depth = 1;
            if (!string.IsNullOrEmpty(cmd.Parameters["@Depth"].Value.ToString()))
                Depth = (int)cmd.Parameters["@Depth"].Value;
            con.Close();
            return Depth;
        }

        public static int GetMenuDepth(int? MenuId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                int Depth = 0;
                Depth = (from x in context.Menus where x.MenuId == MenuId select x.Depth).FirstOrDefault();
                return Depth;
            }
        }

        ////INSERT- UPDATE - DELETE 
        public static bool Insert(MenuViewModel add_model, out int MenuId , out string message)
        {            
            using (EntityDataContext context = new EntityDataContext())
            {
                string PageAlias = StringUtils.convertTitle2Link(add_model.MenuName);
                int Depth = GetMenuDepth(add_model.ParentId) + 1;
                bool result = false;
                message = string.Empty;
                MenuId = 0;

                try
                {
                    using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                    {
                        int ListOrder = (from u in context.Menus select u.ListOrder).DefaultIfEmpty(0).Max() + 1;
                        Menu entity = new Menu();
                        entity.MenuTypeId = add_model.MenuTypeId;
                        entity.ParentId = add_model.ParentId;
                        entity.Depth = Depth;
                        entity.ListOrder = ListOrder;
                        entity.PageId = add_model.PageId;
                        entity.MenuName = add_model.MenuName;
                        entity.MenuAlias = PageAlias;
                        entity.Target = add_model.Target;
                        entity.IconFile = add_model.IconFile;
                        entity.IconClass = add_model.IconClass;
                        entity.CssClass = add_model.CssClass;
                        entity.Description = add_model.Description;
                        entity.MenuStatus = add_model.MenuStatus;
                        context.Menus.Add(entity);
                        int i = context.SaveChanges();
                        if (i > 0)
                        {
                            result = true;
                            MenuId = entity.MenuId;
                            foreach (var item in add_model.RolePermissionList)
                            {
                                MenuPermissionRepository.Insert(MenuId, item.PermissionId, item.RoleId, add_model.UserId, true);
                            }
                            message = Eagle.Resource.LanguageResource.CreateSuccess;
                        }
                        else
                            message = Eagle.Resource.LanguageResource.CreateFailure;
                        transcope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    message = Eagle.Resource.LanguageResource.SystemError;
                }
                return result;
            }
        }

        public static bool Update(MenuViewModel edit_model, out string message)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                message = string.Empty;
                bool result = false;
                string MenuAlias = StringUtils.convertTitle2Link(edit_model.MenuName);
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    Menu entity = context.Menus.FirstOrDefault(x => x.MenuId ==edit_model.MenuId);
                    if(entity !=null)
                    {
                        entity.PageId = edit_model.PageId;
                        entity.MenuName = edit_model.MenuName;
                        entity.MenuAlias = edit_model.MenuAlias;
                        entity.Target = edit_model.Target;
                        entity.IconFile = edit_model.IconFile;
                        entity.IconClass = edit_model.IconClass;
                        entity.CssClass = edit_model.CssClass;
                        entity.Description = edit_model.Description;
                        entity.MenuStatus = edit_model.MenuStatus;
                        int i = context.SaveChanges();
                        if(i>0)
                        {
                            result = true;
                            message = Eagle.Resource.LanguageResource.UpdateSuccess;
                        }else
                        {
                            message = Eagle.Resource.LanguageResource.UpdateFailure;
                        }
                        transcope.Complete();
                    }
                }
                return result;
            }            
        }

        public bool UpdatePosition(MenuPositionModel edit_model, out string message)
        {
            message = string.Empty;
            bool result = false;

            SqlCommand cmd = new SqlCommand("[Cms].[Menu_UpdatePosition]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@MenuId", edit_model.MenuId);
            cmd.Parameters.AddWithValue("@ParentId", edit_model.ParentId);
            cmd.Parameters.AddWithValue("@LastModifiedByUserId", edit_model.LastModifiedByUserId);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            if (retunvalue > 0)
            {
                result = true;
                message = Eagle.Resource.LanguageResource.UpdateSuccess;
            }else
                message = Eagle.Resource.LanguageResource.UpdateFailure;
            con.Close();
            return result;
        }
        //public JsonResult UpdateListOrder(string Ids)
        //{
        //    string message = string.Empty;
        //    bool flag = false;
        //    List<int> lst = Ids.Split(',').Select(s => int.Parse(s)).ToList();
        //    if (lst.Count() > 0)
        //    {
        //        int id = 0;
        //        for (int i = 0; i < lst.Count(); i++)
        //        {
        //            id = lst[i];
        //            flag = PageRepository.UpdateListOrder(id, i + 1, out message);
        //        }
        //    }
        //    return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        //}
        public static bool Delete(int id, out string message)
        {
            MenuPermissionRepository.Delete(id);
            DeleteChildByParentId(id);

            using (EntityDataContext context = new EntityDataContext())
            {               
                bool result = false;
                message = string.Empty;

                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {                    
                    Menu entity = context.Menus.Single(x => x.MenuId == id);
                    if (entity != null)
                    {
                        context.Menus.Remove(entity);
                        int i = context.SaveChanges();
                        if (i > 0)
                        {
                            result = true;
                            message = Eagle.Resource.LanguageResource.DeleteSuccess;
                        }
                    }else
                    {
                        message = Eagle.Resource.LanguageResource.IDNoExistsErrorMessage;
                    }                   
                    transcope.Complete();                    
                }
                return result;
            }
        }

        public static void DeleteChildByParentId(int ParentId)
        { 
            using (EntityDataContext context = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    var menu_list = (from x in context.Menus
                                     where x.ParentId == ParentId
                                     select x.MenuId).ToList();
                    foreach (var menu_id in menu_list)
                    {
                        MenuPermissionRepository.Delete(menu_id);
                        Menu entity = context.Menus.Single(x => x.MenuId == menu_id);
                        context.Menus.Remove(entity);
                        context.SaveChanges();
                    }
                    transcope.Complete();
                }
            }
        }

        public static void DeleteByTypeId(int MenuTypeId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    var menu_list = (from x in context.Menus
                                     where x.MenuTypeId == MenuTypeId
                                     select x.MenuId).ToList();
                    foreach (var menu_id in menu_list)
                    {
                        Menu entity = context.Menus.Single(x => x.MenuId == menu_id);
                        context.Menus.Remove(entity);
                        context.SaveChanges();
                    }
                    transactionScope.Complete();
                }
            }
        }

      
    }
}
