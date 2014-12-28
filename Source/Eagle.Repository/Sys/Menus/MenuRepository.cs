using Eagle.Common.Settings;
using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Model.SYS.Menu;
using Eagle.Repository.Sys.Menus;
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

        public static List<MenuTreeModel> GetTreeList(int ScopeTypeId)
        { 
            using (EntityDataContext context = new EntityDataContext())
            {
                List<MenuTreeModel> list = context.Menus.OrderBy(m => m.ListOrder).Where(m => m.ScopeTypeId == ScopeTypeId).Select(m => new MenuTreeModel()
               {
                   id = m.MenuId,
                   parentid = m.ParentId,
                   text = m.MenuTitle,
                   value = m.MenuCode
               }).ToList();
                List<MenuTreeModel> recursiveObjects = RecursiveFillTree(list, 0);
                return recursiveObjects;          
            }
            
        }

        public static List<MenuTreeModel> RecursiveFillTree(List<MenuTreeModel> list, int? id)
        {
            List<MenuTreeModel> nodes = list.Where(m => m.parentid == id).Select(
               m => new MenuTreeModel
               {
                   id = m.id,
                   parentid = m.parentid,
                   text = m.text,
                   value = m.value
               }).ToList();

            if (nodes.Count > 0)
            {
                foreach (var child in nodes)
                {
                    //child.children = RecursiveFillTree(list, child.id);
                    MenuTreeModel node = new MenuTreeModel()
                    {
                        id = child.id,
                        parentid = child.parentid,
                        text = child.text,
                        value = child.value,
                        children = RecursiveFillTree(list, child.id)
                    };
                }                
            }
            return nodes;
        }
        public Menu Find(int id)
        {
            return context.Menus.Find(id);
        }
        public bool Add(MenuViewModel add_model)
        {
            try
            {
                Menu model = new Menu();
                model.MenuId = add_model.MenuId;
                model.MenuName = add_model.MenuName;
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(MenuViewModel edit_model)
        {
            bool flag = false;
            try
            {
                Menu model = Find(edit_model.MenuId);
                if (model != null)
                {
                    model.MenuId = edit_model.MenuId;                  
                    model.MenuName = edit_model.MenuName;
                    context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    flag = true;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
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
                        strHTML += "<a href='" + MenuLink + "'><span>" + MenuTitle + "</span></a> <small><i class='icon-chevron-right'></i></small>";
                        strHTML += "</li>";
                    }
                }
            }
            result = "<ul class='breadcrumb'>";
            result += "<li><a href='/Admin/Home/Index'>";
            result += "<span><i class='icon-home'></i> " + Eagle.Resource.LanguageResource.Home + "</span></a> <small><i class='icon-chevron-right'></i></small>";
            result += "</li>";
            result += strHTML;
            result += "</ul>";
            return result;
        }

        public string PopulateSiteMapByMenuId(string MenuId)
        {
            string strHTML = "", result = string.Empty;
            if (!string.IsNullOrEmpty(MenuId) && MenuId != "Index")
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
                        strHTML += "<a href='" + MenuLink + "'><span>" + MenuTitle + "</span></a> <small><i class='icon-chevron-right'></i></small>";
                        strHTML += "</li>";
                    }
                }
            }
            result = "<ul class='breadcrumb'>";
            result += "<li><a href='/Admin/Home/Index'>";
            result += "<span><i class='icon-home'></i> " + Eagle.Resource.LanguageResource.Home + "</span></a> <small><i class='icon-chevron-right'></i></small>";
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

        public int GetMenuDepth(int MenuId)
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

        ////INSERT- UPDATE - DELETE 
        public int Insert(int MenuTypeId, int ParentId, string MenuName, int? PageId, string Target, int IconFile, string IconClass, string CssClass, string Description, int Status)
        {
            string PageAlias = StringUtils.convertTitle2Link(MenuName);
            int Depth = GetMenuDepth(ParentId) + 1;
            int id = 0;
            int returnValue = 0;

            using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
            {
                int ListOrder = (from u in context.Menus select u.ListOrder).DefaultIfEmpty(0).Max() + 1;
                Menu entity = new Menu();
                entity.MenuTypeId = MenuTypeId;
                entity.ParentId = ParentId;
                entity.Depth = Depth;
                entity.ListOrder = ListOrder;
                entity.PageId = PageId;
                entity.MenuName = MenuName;
                entity.MenuAlias = PageAlias;
                entity.Target = Target;
                entity.IconFile = IconFile;
                entity.IconClass = IconClass;
                entity.CssClass = CssClass;
                entity.Description = Description;
                entity.MenuStatus = Status;
                context.Menus.Add(entity);
                returnValue = context.SaveChanges();
                transcope.Complete();
                id = entity.MenuId;
            }

            return id;
        }

        public static int Update(int MenuId, string MenuName, int PageId, string Target, int IconFile, string IconClass, string CssClass, string Description, int Status)
        {
            string MenuAlias = StringUtils.convertTitle2Link(MenuName);
            int i = 0;
            using (EntityDataContext context = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    Menu entity = context.Menus.Single(x => x.MenuId == MenuId);
                    entity.PageId = PageId;
                    entity.MenuName = MenuName;
                    entity.MenuAlias = MenuAlias;
                    entity.Target = Target;
                    entity.IconFile = IconFile;
                    entity.IconClass = IconClass;
                    entity.CssClass = CssClass;
                    entity.Description = Description;
                    entity.MenuStatus = Status;
                    i = context.SaveChanges();                   
                    transcope.Complete();
                }
            }
            return i;
        }

        public static int Delete(int MenuId)
        {
            MenuPermissionRepository.Delete(MenuId);
            DeleteChildByParentId(MenuId);
            using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    Menu entity = context.Menus.Single(x => x.MenuId == MenuId);
                    context.Menus.Remove(entity);
                    int i = context.SaveChanges();
                    transcope.Complete();
                    return i;
                }
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
