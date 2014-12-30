using Eagle.Common.Utilities;
using Eagle.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;
using Eagle.Model.Common;
using Eagle.Model.Mail;

namespace Eagle.Repository.Mail
{
    public class MailTypeRespository
    {
        public static SelectList PopulateStatusToDropDownList(string SelectedValue, bool IsShowSelectText = false)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.Active, Value = "True" });
            lst.Add(new SelectListItem { Text = Resource.LanguageResource.InActive, Value = "False" });
            if (IsShowSelectText)
                lst.Insert(0, new SelectListItem { Text = string.Format("--- {0} ---", Eagle.Resource.LanguageResource.Select), Value = "" });
            return new SelectList(lst, "Value", "Text", SelectedValue);
        }
        public static string SerializeMailTypeToJson(int PortalId, string CultureCode, bool Status)
        {
            Dictionary<string, object> result_dict = new Dictionary<string, object>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = string.Empty;
            int index = 0;
            List<MailTypeViewModel> lst = GetListByPortalIdStatus(PortalId, CultureCode, Status);
            if (lst.Count > 0)
            {
                foreach (var x in lst)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("id", x.TypeId);
                    dict.Add("text", x.Name);
                    dict.Add("parentid", x.ParentId);
                    result_dict.Add(index.ToString(), dict);
                    index++;
                }
                json = serializer.Serialize(result_dict);
            }
            return json;
        }

        public static List<MailTypeViewModel> GetListByPortalIdCultureCodeStatus(int PortalId, string CultureCode, bool Status)
        {
            List<MailTypeViewModel> lst = new List<MailTypeViewModel>();
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var query = from x in dbContext.Mail_Types
                            where x.PortalId == PortalId && x.CultureCode == CultureCode && x.Status == Status
                            orderby x.Depth ascending
                            select new MailTypeViewModel
                            {
                                TypeId = x.TypeId,
                                ParentId = (int)x.ParentId,
                                PortalId = (int)x.PortalId,
                                SortKey = (int)x.SortKey,
                                CultureCode = x.CultureCode,
                                Name = x.Name,
                                Depth = (int)x.Depth,
                                PostedDate = x.PostedDate,
                                LastUpdatedDate = x.LastUpdatedDate,
                                Status = x.Status,
                                Description = x.Description,
                            };
                return query.ToList();
            }
        }

        public static List<TreeNode> GetHierachicalListByPortalIdCultureCodeStatus(int PortalId, string CultureCode, bool Status)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                List<TreeNode> lst = (from x in dbContext.Mail_Types
                                             where x.PortalId == PortalId && x.CultureCode == CultureCode && x.Status == Status
                                             orderby x.Depth ascending
                                              select new TreeNode
                                             {
                                                 id = x.TypeId,
                                                 parentid = x.ParentId,
                                                 text = x.Name,
                                                 depth = x.Depth,
                                             }).ToList();
                return lst;
            }
        }

        #region HIEARACHY List ==========================================================================================================
        public static List<TreeNode> PopulateHierachyDropDownList(int PortalId, string CultureCode, bool Status)
        {
            List<TreeNode> list = GetHierachicalListByPortalIdCultureCodeStatus(PortalId, CultureCode, Status);
            List<TreeNode> recursiveObjects = RecursiveFillTree(list, 0);
            return recursiveObjects;
        }

        private static List<TreeNode> RecursiveFillTree(List<TreeNode> elements, int? parentid)
        {
            List<TreeNode> items = new List<TreeNode>();
            List<TreeNode> parentList = elements.Where(p => p.parentid == parentid).Select(
               p => new TreeNode
               {
                   id = p.id,
                   text = p.text,
                   parentid = p.parentid,
                   depth = p.depth
               }).ToList();

            if (parentList.Count > 0)
            {
                foreach (var child in parentList)
                {
                    TreeNode node = new TreeNode()
                    {
                        id = child.id,
                        parentid = child.parentid,
                        text = child.text,
                        depth = child.depth,
                        children = RecursiveFillTree(elements, child.id)
                    };
                    items.Add(node);
                }
            }
            return items;
        }
        #endregion =============================================================================================================================

        #region Mail Types - XSLT ======================================================================================================================

        //public string GenerateXmlFormat(int TypeId, string Status, bool IsAdmin)
        //{
        //    string strResult = string.Empty;
        //    List<NodeData> menu_list = PopulateHierachyList(TypeId, Status, IsAdmin);
        //    if (menu_list.Count > 0)
        //    {
        //        var elements = from x in menu_list
        //                       select new XElement("MailTypes",
        //                                new XElement("TypeId", x.TypeId),
        //                                new XElement("ParentId", x.ParentId),
        //                                new XElement("Depth", x.Depth),   
        //                                new XElement("Name", x.Name),
        //                                new XElement("SortKey", x.SortKey),
        //                                new XElement("PortalId", x.PortalId),
        //                                new XElement("PostedDate", x.PostedDate),
        //                                new XElement("LastUpdatedDate", x.LastUpdatedDate),
        //                                new XElement("Description", x.Description),
        //                                new XElement("Status", x.Status)
        //                        );

        //        XDocument document = new XDocument(
        //            new XDeclaration("1.0", "utf-8", "yes"),
        //            new XComment("XML Source Data for Mail Types"),
        //            new XElement("MailTypes", elements));

        //        strResult = document.ToString();
        //    }
        //    return strResult;
        //}

        //public static string ConvertListToXMLFormat(List<Mail_Types> menu_list)
        //{
        //    string strResult = string.Empty;
        //    if (menu_list.Count > 0)
        //    {
        //        var elements = new XElement("MailTypes", from x in menu_list
        //                                                 select new XElement("MailTypes",
        //                                                        new XElement("TypeId", x.TypeId),
        //                                                        new XElement("ParentId", x.ParentId),
        //                                                        new XElement("Depth", x.Depth),
        //                                                        new XElement("SortKey", x.SortKey),
        //                                                        new XElement("PortalId", x.PortalId),
        //                                                        new XElement("Name", x.Name),
        //                                                        new XElement("PostedDate", x.PostedDate),
        //                                                        new XElement("LastUpdatedDate", x.LastUpdatedDate),
        //                                                        new XElement("Description", x.Description),
        //                                                        new XElement("Status", x.Status)
        //                                             )
        //                                    );
        //        XDocument document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), elements);

        //        strResult = document.ToString();
        //    }
        //    return strResult;
        //}

        //public string ExecuteXSLTransformation(int PortalId, string CultureCode, bool Status)
        //{
        //    string ErrorMsg, HtmlTags = string.Empty, XsltPath;
        //    MemoryStream DataStream = default(MemoryStream);
        //    StreamReader streamReader = default(StreamReader);


        //    byte[] bytes = null;
        //    //Path of XSLT file
        //    string xslt_filepath = "~/Mail/MailTypes.xslt";
        //    XsltPath = HttpContext.Current.Server.MapPath(xslt_filepath);

        //    if (System.IO.File.Exists(XsltPath))
        //    {

        //        //Encode all Xml format string to bytes
        //        string strXML = GenerateXmlFormat(PortalId, CultureCode, Status);
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
        #endregion ===============================================================================================================================

        public static Mail_Types Find(int id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return context.Mail_Types.Find(id);
            }
        }

        public static int GetLastItemInfo(int PortalId, string CultureCode, bool Status)
        {
            int TypeId = 0;
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var lst = (from x in dbContext.Mail_Types
                           where x.PortalId == PortalId
                              && x.CultureCode == CultureCode
                              && x.Status == Status
                           select x.TypeId).ToList();
                if (lst.Count > 0)
                    TypeId = lst.Last();
                return TypeId;
            }
        }

        public static List<MailTypeViewModel> GetListByPortalIdStatus(int PortalId, string CultureCode, bool Status)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var query = from x in dbContext.Mail_Types
                            where x.PortalId == PortalId
                               && x.CultureCode == CultureCode
                               && x.Status == Status
                            select new MailTypeViewModel
                            {
                                TypeId = x.TypeId,
                                ParentId = (int)x.ParentId,
                                PortalId = (int)x.PortalId,
                                SortKey = (int)x.SortKey,
                                CultureCode = x.CultureCode,
                                Name = x.Name,
                                Depth = (int)x.Depth,
                                PostedDate = x.PostedDate,
                                LastUpdatedDate = x.LastUpdatedDate,
                                Status = x.Status,
                                Description = x.Description,
                            };

                return query.ToList();
            }
        }


        public static MailTypeViewModel GetDetails(int? TypeId)
        {
            MailTypeViewModel entity = new MailTypeViewModel();
            if (TypeId != null && TypeId > 0)
            {
                using (EntityDataContext dbContext = new EntityDataContext())
                {
                    entity = (from x in dbContext.Mail_Types
                                  where x.TypeId == TypeId
                                  select new MailTypeViewModel
                                  {
                                      TypeId = x.TypeId,
                                      ParentId = x.ParentId,
                                      PortalId = x.PortalId,
                                      SortKey = x.SortKey,
                                      CultureCode = x.CultureCode,
                                      Name = x.Name,
                                      Depth = x.Depth,
                                      PostedDate = x.PostedDate,
                                      LastUpdatedDate = x.LastUpdatedDate,
                                      Status = x.Status,
                                      Description = x.Description,
                                  }).FirstOrDefault();
                    
                }
            }
            return entity;
        }

        public static Mail_Types GetDetailsByName(string TypeName)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                Mail_Types entity = dbContext.Mail_Types.Where(x => x.Name == TypeName).FirstOrDefault();
                return entity;
            }
        }

        public static int GetDepth(int ParentId)
        {
            int Depth = 0;
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                var lst = (from x in dbContext.Mail_Types
                           where x.ParentId == ParentId
                           select x.Depth).ToList();
                if (lst.Count > 0)
                    Depth = lst.Last();
                return Depth;
            }
        }

        //////INSERT- UPDATE - DELETE 
        public static bool Insert(MailTypeViewModel model, out string Message)
        {
            bool result = false;
            Message = string.Empty;
            int Depth = GetDepth(model.ParentId) + 1;

            using (EntityDataContext dbContext = new EntityDataContext())
            {
                try
                {
                    List<Mail_Types> lst = (from x in dbContext.Mail_Types
                                            where x.PortalId == model.PortalId
                                               && x.CultureCode == model.CultureCode
                                               && x.Name == model.Name
                                            select x).ToList();
                    if (lst.Count <= 0)
                    {
                        using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                        {
                            int SortKey = (from u in dbContext.Mail_Types select u.SortKey).DefaultIfEmpty(0).Max() + 1;
                            Mail_Types entity = new Mail_Types();
                            entity.PortalId = model.PortalId;
                            entity.ParentId = model.ParentId;
                            entity.CultureCode = model.CultureCode;
                            entity.Name = model.Name;
                            entity.Description = model.Description;
                            entity.SortKey = model.SortKey;
                            entity.Depth = model.Depth;
                            entity.PostedDate = DateTime.Now;
                            entity.LastUpdatedDate = DateTime.Now;
                            entity.Status = model.Status;
                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                            int affectedRow = dbContext.SaveChanges();
                            if (affectedRow > 0)
                            {
                                model.TypeId = entity.TypeId;
                                Message = Eagle.Resource.LanguageResource.CreateSuccess;
                                result = true;
                            }
                            transcope.Complete();
                        }
                    }
                    else
                    {
                        result = false;
                        Message = Eagle.Resource.LanguageResource.DuplicateError;
                    }

                }
                catch (Exception ex)
                {
                    ex.ToString();
                    result = false;
                    Message = Eagle.Resource.LanguageResource.SystemError;
                }
            }
            return result;
        }

        public static bool Update(MailTypeViewModel model, out string Message)
        {
            bool result = false;
            Message = string.Empty;
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        Mail_Types entity = dbContext.Mail_Types.Single(x => x.TypeId == model.TypeId);
                        entity.TypeId = model.TypeId;
                        entity.PortalId = model.PortalId;
                        entity.CultureCode = model.CultureCode;
                        entity.Name = model.Name;
                        entity.Description = model.Description;
                        entity.LastUpdatedDate = DateTime.Now;
                        entity.Status = model.Status;
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        int affectedRows = dbContext.SaveChanges();
                        if (affectedRows == 1)
                        {
                            Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                            result = true;
                        }
                        transcope.Complete();
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        result = false;
                        Message = Eagle.Resource.LanguageResource.SystemError;
                    }
                }
            }
            return result;
        }


        public static bool UpdateListOrder(int id, int listOrder, out string message)
        {
            bool result = false;
            message = string.Empty;
            try
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    Mail_Types entity = Find(id);
                    entity.SortKey = listOrder;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int i = context.SaveChanges();
                    if (i == 1)
                    {
                        result = true;
                        message = Eagle.Resource.LanguageResource.UpdateSuccess;
                    }
                    else
                    {
                        result = false;
                        message = Eagle.Resource.LanguageResource.UpdateFailed;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                result = false;
            }
            return result;
        }

        public static bool UpdateStatus(int id, out string message)
        {
            bool result = false;
            message = string.Empty;
            try
            {
                using (EntityDataContext context = new EntityDataContext())
                {
                    Mail_Types entity = Find(id);
                    bool flag = (entity.Status == true) ? false : true;
                    entity.Status = flag;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    int i = context.SaveChanges();
                    if (i == 1)
                    {
                        result = true;
                        message = Eagle.Resource.LanguageResource.UpdateSuccess;
                    }
                    else
                    {
                        result = false;
                        message = Eagle.Resource.LanguageResource.UpdateFailed;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                result = false;
            }
            return result;
        }

        public static bool Delete(int TypeId, out string message)
        {
            bool result = false;
            using (EntityDataContext context = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        ////xoa tat ca cac mail co parent = typeid
                        DeleteChildByParentId(TypeId);
                        ////xoa mail typeid
                        Mail_Types entity = context.Mail_Types.Where(x => x.TypeId == TypeId).SingleOrDefault();
                        context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                        int i = context.SaveChanges();
                        if (i == 1)
                        {
                            result = true;
                            message = Eagle.Resource.LanguageResource.UpdateSuccess;
                        }
                        else
                        {
                            result = false;
                            message = Eagle.Resource.LanguageResource.UpdateFailed;
                        }
                        transcope.Complete();
                    }
                    catch (Exception ex)
                    {
                        message = ex.ToString();
                        result = false;
                    }
                }
            }
            return result;
        }

        public static int DeleteChildByParentId(int ParentId)
        {
            int i = 0;
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    var list = (from x in dbContext.Mail_Types
                                where x.ParentId == ParentId
                                select x.TypeId).ToList();
                    foreach (var id in list)
                    {
                        Mail_Types entity = dbContext.Mail_Types.Single(x => x.TypeId == id);
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                        i = dbContext.SaveChanges();
                    }
                    transcope.Complete();
                    return i;
                }
            }
        }
    }
}
