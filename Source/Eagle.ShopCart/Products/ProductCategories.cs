using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Eagle.Common;
using Eagle.Common.Settings;
using Eagle.Common.Utilities;


namespace Eagle.ShopCart.Products
{
    public class ProductCategories
    {       
        string ip = NetworkUtils.GetIP4Address();
        SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable(); 

        public ProductCategories(){}        

        public DataTable GetAll()
        {          
           SqlCommand cmd = new SqlCommand("[Production].[Product_Categories_GetAll]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
           con.Open();
           using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
           con.Close();
           return dt;
        }

        public DataTable GetDetailByID(int Category_Id)
        {          
            SqlCommand cmd = new SqlCommand("[Production].[Product_Categories_GetDetailByID]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Category_Id", Category_Id);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }
        
        public DataTable GetDetailByCode(string Category_Code)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Categories_GetDetailByCode]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public string GetImageByID(int Category_Id)
        {          
            SqlCommand cmd = new SqlCommand("[Production].[Product_Categories_GetImageByID]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Category_Id", Category_Id);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            string retunvalue = (string)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public DataTable GetList(int VendorId, string CultureCode)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Categories_GetListByVendorIdCultureCode]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetListByStatus(string Status)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Categories_GetListByStatus]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Status", Status);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataSet GetActiveList()
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Categories_GetActiveList]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();
            return ds;
        }

        public DataTable GetPublishedList()
        {         
            SqlCommand cmd = new SqlCommand("[Production].[Product_Categories_GetPublishedList]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetListByParentIDStatus(int VendorId, string CultureCode, int Parent_ID, string Status)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Categories_GetListByParentIDStatus]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId",VendorId);
            cmd.Parameters.AddWithValue("@CultureCode",CultureCode);
            cmd.Parameters.AddWithValue("@Parent_ID",Parent_ID);
            cmd.Parameters.AddWithValue("@Status",Status);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataSet GetListByVendorIdCultureCodeStatus(int VendorId, string CultureCode, string Status)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Categories_GetListByVendorIdCultureCodeStatus]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);            
            cmd.Parameters.AddWithValue("@Status", Status);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();
            return ds;
        }

        public DataSet GetListByVendorIDParentIDStatus(int VendorId, string CultureCode, int Parent_ID, string Status)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Categories_GetListByParentIDStatus]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            cmd.Parameters.AddWithValue("@Parent_ID", Parent_ID);
            cmd.Parameters.AddWithValue("@Status", Status);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();
            return ds;
        }

        public string GenerateXmlFormat_ByTypeId()
        {
            string result = string.Empty;
            DataSet ds = GetHierarchicalTreeBy();
            if (ds.Tables.Count > 0)
            {
                ds.DataSetName = "Product_Categories";
                ds.Tables[0].TableName = "Product_Category";
                //create Relation Parent and Child
                DataRelation relation = new DataRelation("ParentChild", ds.Tables[0].Columns["Category_Id"], ds.Tables[0].Columns["Parent_Id"], false);
                relation.Nested = true;
                ds.Relations.Add(relation);
                result = ds.GetXml();
            }
            con.Close();
            return result;
        }

        public DataSet GetHierarchicalTreeBy()
        {
            SqlCommand cmd = new SqlCommand("[Production].[ProductCategory_GetHierarchicalTree]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };           
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        
        public string ExecuteCategoryToXSLTransformationByTypeId(string xslt_filepath)
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
                string strXML = GenerateXmlFormat_ByTypeId();
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

        //INSERT- UPDATE - DELETE --------------------------------------------------------------------------       
        public int Insert(int VendorId, string CultureCode, int Parent_ID, string Category_Code, string Category_Name, string Category_Link, string CssClass, string Photo, string BriefDescription, string Description, string Status)
        {
            string Alias = StringUtils.convertTitle2Link(Category_Name);
            SqlCommand cmd = new SqlCommand("[Production].[Product_Categories_Insert]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@Category_Name", Category_Name);
            cmd.Parameters.AddWithValue("@Category_Link", Category_Link);
            cmd.Parameters.AddWithValue("@CssClass", CssClass);
            cmd.Parameters.AddWithValue("@Alias", Alias);
            cmd.Parameters.AddWithValue("@Parent_ID", Parent_ID);
            cmd.Parameters.AddWithValue("@Photo", Photo);
            cmd.Parameters.AddWithValue("@BriefDescription", BriefDescription);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int Update(int Category_Id, string CultureCode, int Parent_ID, string Category_Code, string Category_Name, string Category_Link, string CssClass, string Photo, string BriefDescription, string Description, string Status)
        {
            string Alias = StringUtils.convertTitle2Link(Category_Name);

            SqlCommand cmd = new SqlCommand("[Production].[Product_Categories_Update]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            cmd.Parameters.AddWithValue("@Category_Id", Category_Id);
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@Category_Name", Category_Name);
            cmd.Parameters.AddWithValue("@Category_Link", Category_Link);
            cmd.Parameters.AddWithValue("@CssClass", CssClass);
            cmd.Parameters.AddWithValue("@Alias", Alias);
            cmd.Parameters.AddWithValue("@Parent_ID", Parent_ID);
            cmd.Parameters.AddWithValue("@Photo", Photo);
            cmd.Parameters.AddWithValue("@BriefDescription", BriefDescription);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int UpdateStatus(int Category_Id, string Status)
        {          
            SqlCommand cmd = new SqlCommand("[Production].[Product_Categories_UpdateStatus]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Category_Id", Category_Id);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int UpdateOrder(int Category_Id, int ParentId, int Depth, int ListOrder)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Update_ProductCategory_OrderList]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Category_Id", Category_Id);
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
         
        public int Delete(int VendorId, int Category_Id, string ImgPath)
        {
            string dir_path = HttpContext.Current.Server.MapPath(ImgPath);
            if (System.IO.Directory.Exists(dir_path))
            {
                string img_file = GetImageByID(Category_Id);
                if (img_file != null || img_file != string.Empty)
                {
                    string file_path = dir_path + "/" + img_file;
                    if (System.IO.File.Exists(file_path))
                    {
                        FileUtils.DeleteFileWithFileNameAndPredefinedPath(img_file, dir_path);
                    }
                }
            }

            SqlCommand cmd = new SqlCommand("[Production].[Product_Categories_Delete]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@Category_Id", Category_Id);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }   
  

        #region MENU PRODUCT CATEGORIES ==========================================================
        public string GenerateXmlFormat(int VendorId,string CultureCode,string Status)
        {
            DataSet ds = GetListByVendorIdCultureCodeStatus(VendorId, CultureCode, Status);
            string result = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.DataSetName = "Menus";
                ds.Tables[0].TableName = "Production.Product_Categories";

                //create Relation Parent and Child
                DataRelation relation = new DataRelation("ParentChild", ds.Tables[0].Columns["Category_Id"], ds.Tables[0].Columns["Parent_Id"], false);
                relation.Nested = true;
                ds.Relations.Add(relation);

                result = ds.GetXml();
            }
            return result;
        }


        public string ExecuteXSLTransformation(string xslt_filepath, int VendorId, string CultureCode, string Status)
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
                string strXML = GenerateXmlFormat(VendorId, CultureCode,Status);
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
                    finally
                    {
                        //Release the resources 
                        streamReader.Close();
                        DataStream.Close();
                    }
                }
            }
            return HtmlTags;
        }
        #endregion ===============================================================================    
    }
}
