using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.IO;
using Eagle.Common;

using System.Runtime.Serialization;
using System.Linq.Expressions;
using Eagle.ShopCart.Data;
using Eagle.Common.Utilities;
using Eagle.Common.Settings;


namespace Eagle.ShopCart.Products
{
    public partial class ProductController : IDeserializationCallback
    {
        [NonSerialized]
        SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable(); 

        public ProductController()
        {
        }

        public static List<Product> GetListByManufacturerIdCultureCodeDiscontinued(int ManufacturerId, string CultureCode, int Discontinued, int TotalItemCount)
        {
            using (ShopCartEntities db = new ShopCartEntities())
            {
                IQueryable<Product> query = from x in db.Products
                                            orderby x.CreatedOnDate descending
                                            select x;
               
                if (ManufacturerId >= 0)
                    query = query.Where(x => x.Manufacturer_Id == ManufacturerId);
                if (CultureCode != "")
                    query = query.Where(x => x.CultureCode == CultureCode);               
                if (Discontinued >= 0)
                    query = query.Where(x => x.Discontinued == Discontinued);

                List<Product> list = query.Take(TotalItemCount).ToList();
                int count = list.Count;
                return list;
            }
        }

        public static List<Product> GetListByVendorIdCategoryCodeCultureCode(int VendorId, string CategoryCode, string CultureCode)
        {
            using (ShopCartEntities db = new ShopCartEntities())
            {
                IQueryable<Product> query = from x in db.Products
                                            orderby x.CreatedOnDate descending
                                            select x;
                if (VendorId > 0)
                    query = query.Where(x => x.Vendor_Id == VendorId);
                if (CultureCode != "")
                    query = query.Where(x => x.CultureCode.Trim() == CultureCode.Trim());
                if (CategoryCode != "")
                    query = query.Where(x => x.Category_Code.Trim() == CategoryCode.Trim());
                return query.ToList();
            }
        }

        public static List<Product> GetListByVendorIdCultureCodeDiscontinued(int VendorId, string CultureCode, int Discontinued, int TotalItemCount)
        {
            using (ShopCartEntities db = new ShopCartEntities())
            {
                IQueryable<Product> query = from x in db.Products
                                            orderby x.CreatedOnDate descending
                                            select x;

                if (VendorId >= 0)
                    query = query.Where(x => x.Vendor_Id == VendorId);
                if (CultureCode != "")
                    query = query.Where(x => x.CultureCode == CultureCode);                
                if (Discontinued >= 0)
                    query = query.Where(x => x.Discontinued == Discontinued);

                List<Product> list = query.Take(TotalItemCount).ToList();
                int count = list.Count;
                return list;
            }
        }

        public static List<Product> GetListByVendorIdCategoryCodeCultureCodeDiscontinued(int VendorId, string CultureCode, string CategoryCode, int Discontinued, int TotalItemCount)
        {
            using (ShopCartEntities db = new ShopCartEntities())
            {
               List<Product> list;
                List<Product_Categories> list_cate;


                IQueryable<Product> query = from x in db.Products
                                            orderby x.CreatedOnDate descending
                                            select x;

                if (VendorId >= 0)
                    query = query.Where(x => x.Vendor_Id == VendorId);
                if (CultureCode != "")
                    query = query.Where(x => x.CultureCode == CultureCode);
                if (Discontinued >= 0)
                    query = query.Where(x => x.Discontinued == Discontinued);
                if (CategoryCode != "")
                    query = query.Where(x => x.Category_Code == CategoryCode);

                if (CategoryCode != "")
                {
                    int cateid = (from ze in db.Product_Categories where ze.Category_Code == CategoryCode select ze).Single().Category_Id;
                    
                    list_cate = (from z in db.Product_Categories
                                 where z.Parent_Id == cateid
                                 select z).ToList();

                    if (list_cate.Count() > 0)
                    {

                       
                       
                        foreach (var cat in list_cate)
                        {
                            string _CategoryCode = (from ze in db.Product_Categories where ze.Category_Id == cat.Category_Id select ze).SingleOrDefault().Category_Code;

                            if (CategoryCode != "")
                                query = query.Where(c => c.Category_Code == _CategoryCode); 
                        }

                        
                    }
                }

                list = query.Take(TotalItemCount).ToList();
                int count = list.Count;

                return list;
                

            }
        }
        


        //public static List<Product> GetInfoList(int VendorId, string CultureCode, string CategoryCode, int Discontinued)
        //{
        //    using (ShopCartEntities db = new ShopCartEntities())
        //    {
        //        var lst = (from p in db.Products
        //                    where p.Vendor_Id == VendorId && p.CultureCode == CultureCode
        //                    && p.Discontinued >= 0 && p.Category_Code == CategoryCode
        //                    select p).ToList();
        //        return lst;
        //     }
        //}


        //public static List<Product> GetChildCategoryList(string Category_Code)
        //{
        //    using (ShopCartEntities db = new ShopCartEntities())
        //    {
        //        List<string> ChildCategoryList = new List<string>();
        //        int Category_Id = (from c in db.Product_Categories where c.Category_Code.Trim() == Category_Code select c.Category_Id).SingleOrDefault();
        //        var cats = (from c in db.Product_Categories
        //                    where c.Parent_Id == Category_Id
        //                    select new { c.Category_Code });
        //        if (cats.Count() > 0)
        //        {
        //            foreach (var cat in cats)
        //            {                      
        //                string _CategoryCode = cat.Category_Code;
        //                ChildCategoryList.Add(_CategoryCode);
        //                GetChildCategoryList(_CategoryCode);
        //            }
        //        }
               
        //        return cats.ToList();
        //    }
        //}

        void IDeserializationCallback.OnDeserialization(object sender)
        {
            //Recreate your connection here
            con = new SqlConnection();
            con.ConnectionString = SystemSettings.ConnectionString;
        }

        public static int GetUnitsInStockByProductId(int Idx)
        {
            using (ShopCartEntities db = new ShopCartEntities())
            {
                var query = (from x in db.Products where x.Product_Id == Idx select x).Single();
                int result = Convert.ToInt32(query.UnitsInStock.ToString()); 
                return result;
            }
        }

        public static int GetUnitsInStockByProductNo(string Product_No)
        {
            System.Guid Guid_Product_No = new Guid(Product_No);
            using (ShopCartEntities db = new ShopCartEntities())
            {
                var query = (from x in db.Products where x.Product_No == Guid_Product_No select x).Single();
                int result = Convert.ToInt32(query.UnitsInStock.ToString());
                return result;
            }
        }

        public int UpdateUnitsOnOrderUnitsInStock(string Product_No, int Quantity)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_UpdateUnitsOnOrderUnitsInStock]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Product_No", Product_No);
            cmd.Parameters.AddWithValue("@OrderedQuantity", Quantity);
            cmd.Parameters.Add("@o_return", SqlDbType.Int, 2).Direction = ParameterDirection.Output;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

       

        public int RecoverQty4TblProduct(string Product_No, int Quantity)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_RecoverQty4TblProduct]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Product_No", Product_No);
            cmd.Parameters.AddWithValue("@Quantity", Quantity);
            cmd.Parameters.Add("@o_return", SqlDbType.Int, 2).Direction = ParameterDirection.Output;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public List<Product> GetListWithPagination(int VendorId, string CultureCode, string Category_Code, int Discontinued, int iTotalItemCount)
        {

            using (ShopCartEntities db = new ShopCartEntities())
            {
                var query = from x in db.Products select x;
                if (VendorId >= 0 && VendorId.ToString() != string.Empty)
                    query = query.Where(x => x.Vendor_Id == VendorId);
                if (CultureCode != "" && CultureCode != string.Empty)
                    query = query.Where(x => x.CultureCode == CultureCode);
                if (Category_Code != "" && Category_Code != string.Empty)
                    query = query.Where(x => x.Category_Code == Category_Code);
                if (Discontinued >= 0)
                    query = query.Where(x => x.Discontinued == Discontinued);
                query = query.OrderByDescending(x=>x.LastModifiedOnDate);
                int TotalItemCount = 0;
                if (query.Count() > 0)
                {
                    if (iTotalItemCount <= query.Count())
                        TotalItemCount = iTotalItemCount;
                    else
                        TotalItemCount = query.Count();
                }
                return query.Take(TotalItemCount).ToList();
            }
        }

        //=================================================

        public DataTable GetListByVendorIdTypeId(int VendorId, int TypeId)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetListByVendorIdTypeId]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@TypeId", TypeId);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetCompletionList(string prefixText)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetCompletionList]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@prefixText", prefixText);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }       

        public DataTable GetListByCodeDiscontinued(int VendorId, string CultureCode, string Category_Code, int Discontinued)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetListByCodeDiscontinued]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@Discontinued", Discontinued);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }
        public DataTable GetListByVendorIdCodeStatus(int VendorId,string Category_Code, string CultureCode, int Discontinued)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetListByVendorIdCategoryCultureStatus]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            cmd.Parameters.AddWithValue("@Discontinued", Discontinued);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }


        public DataTable Products_GetListByVendor(int Num_Rows, int Vendor_Id)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetListByVendor]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Num_Rows", Num_Rows);
            cmd.Parameters.AddWithValue("@Vendor_Id", Vendor_Id);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }
        
        public DataTable GetDetailByID(int Product_Id)
        {            
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetDetailByID]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };            
            cmd.Parameters.AddWithValue("@Product_Id", Product_Id);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetDetailByProductNo(string Product_No)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetDetailByProductNo]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Product_No", Product_No);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetDetailByProductName(string Product_Name)
        {          
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetDetailByProductName]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Product_Name", Product_Name);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetQuantityById(int Product_Id)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetQuantity]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Product_Id", Product_Id);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetPublishedListByCode(string Category_Code)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetPublishedListByCode]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }      

        public string[] GetFilesByID(int Product_Id)
        {
            string[] array_list = new string[3];
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetFilesByID]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Product_ID", Product_Id);
            cmd.Parameters.Add(new SqlParameter("@PhotoFileName", SqlDbType.NVarChar,int.MaxValue) { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@ThumbnailPhotoFileName", SqlDbType.NVarChar, int.MaxValue) { Direction = ParameterDirection.Output });            
            con.Open();
            cmd.ExecuteNonQuery();
            array_list[0]  = cmd.Parameters["@PhotoFileName"].Value.ToString();
            array_list[1] = cmd.Parameters["@ThumbnailPhotoFileName"].Value.ToString();
            con.Close();
            return array_list;
        }        

         public string[] GetFilesByProductNo(string Product_No)
        {
            string[] array_list = new string[3];
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetFilesByNo]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Product_No", Product_No);
            cmd.Parameters.Add(new SqlParameter("@PhotoFileName", SqlDbType.NVarChar, int.MaxValue) { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@ThumbnailPhotoFileName", SqlDbType.NVarChar, int.MaxValue) { Direction = ParameterDirection.Output });            
            con.Open();
            cmd.ExecuteNonQuery();
            array_list[0] = cmd.Parameters["@PhotoFileName"].Value.ToString();
            array_list[1] = cmd.Parameters["@ThumbnailPhotoFileName"].Value.ToString();
            con.Close();
            return array_list;
        } 

        public DataSet Search_Product(string Keywords)
        {   
            SqlCommand cmd = new SqlCommand("[Production].[Products_Search]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Keywords", Keywords);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();
            return ds;
        }

        public DataTable GetListByNumStatus(int Num_Rows, int Discontinued)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetListByNumStatus]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Num_Rows", Num_Rows);
            cmd.Parameters.AddWithValue("@Discontinued", Discontinued);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetListByNumType(int Num_Rows, string Category_Code)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetListByNumType]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Num_Rows", Num_Rows);
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetListByNumTypeStatus(int Num_Rows, int VendorId, string Category_Code, int Discontinued)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetListByNumTypeStatus]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Num_Rows", Num_Rows);
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@Discontinued", Discontinued);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetListByTypeStatus(string Category_Code, int Discontinued)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetListByTypeStatus]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@Discontinued", Discontinued);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetListByTime(string Category_Code, string SelectedDate)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetListByTime]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@SelectedDate", SelectedDate);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetListByConditions(string Begin_Date, string End_Date, int Discontinued, string Category_Code, int Product_TypeId)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetListByConditions]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Begin_Date", Begin_Date);
            cmd.Parameters.AddWithValue("@End_Date", End_Date);
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@Product_TypeId", Product_TypeId);
            cmd.Parameters.AddWithValue("@Discontinued", Discontinued);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public string[] Insert(string CultureCode, string Product_Code, string Category_Code, int Manufacturer_Id, int VendorId, 
                        int Product_TypeId, string Product_Name, string CurrencyCode, string NetPrice, string GrossPrice, int TaxRateId, 
                        int DiscountId, int UnitsInStock, int UnitsInAPackage, int UnitsInBox, string Unit,
                        decimal Weight, string UnitOfWeightMeasure, decimal Length, decimal Width, decimal Height,
                        string UnitOfDimensionMeasure, int MinPurchaseQty, int MaxPurchaseQty, string Availability,
                        string StartDate, string EndDate, string PhotoFileName, string ThumbnailPhotoFileName, string Url, 
                        string ShortDescription, string Specification,  int OnlineTransactionFlag, int Discontinued)
        {

            string Alias = StringUtils.convertTitle2Link(Product_Name);
            NetPrice = StringUtils.RemoveExtraTextWithoutPointOrComma(NetPrice);
            GrossPrice = StringUtils.RemoveExtraTextWithoutPointOrComma(GrossPrice);

            SqlCommand cmd = new SqlCommand("[Production].[Products_Insert]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            cmd.Parameters.AddWithValue("@Product_Code", Product_Code);
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@Manufacturer_Id", Manufacturer_Id);
            cmd.Parameters.AddWithValue("@Vendor_Id", VendorId);
            cmd.Parameters.AddWithValue("@Product_TypeId", Product_TypeId);
            cmd.Parameters.AddWithValue("@Product_Name", Product_Name);
            cmd.Parameters.AddWithValue("@Alias", Alias);
            cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
            cmd.Parameters.AddWithValue("@NetPrice", NetPrice);
            cmd.Parameters.AddWithValue("@GrossPrice", GrossPrice);
            cmd.Parameters.AddWithValue("@TaxRateId", TaxRateId);
            cmd.Parameters.AddWithValue("@Discount_Id", DiscountId);
            cmd.Parameters.AddWithValue("@UnitsInStock", UnitsInStock);
            cmd.Parameters.AddWithValue("@UnitsInAPackage", UnitsInAPackage);
            cmd.Parameters.AddWithValue("@UnitsInBox", UnitsInBox);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@Weight", Weight);
            cmd.Parameters.AddWithValue("@UnitOfWeightMeasure", UnitOfWeightMeasure);
            cmd.Parameters.AddWithValue("@Length", Length);
            cmd.Parameters.AddWithValue("@Width", Width);
            cmd.Parameters.AddWithValue("@Height", Height);
            cmd.Parameters.AddWithValue("@UnitOfDimensionMeasure", UnitOfDimensionMeasure);
            cmd.Parameters.AddWithValue("@MinPurchaseQty", MinPurchaseQty);
            cmd.Parameters.AddWithValue("@MaxPurchaseQty", MaxPurchaseQty);
            cmd.Parameters.AddWithValue("@Availability", Availability);
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.AddWithValue("@PhotoFileName", PhotoFileName);
            cmd.Parameters.AddWithValue("@ThumbnailPhotoFileName", ThumbnailPhotoFileName);
            cmd.Parameters.AddWithValue("@Url", Url);
            cmd.Parameters.AddWithValue("@ShortDescription", ShortDescription);
            cmd.Parameters.AddWithValue("@Specification", Specification);
            cmd.Parameters.AddWithValue("@OnlineTransactionFlag", OnlineTransactionFlag);
            cmd.Parameters.AddWithValue("@Discontinued", Discontinued);
            cmd.Parameters.Add("@o_return_Product_No", SqlDbType.NVarChar, int.MaxValue).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_return", SqlDbType.Int, 2).Direction = ParameterDirection.Output;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            string[] arr_result = new string[2];
            arr_result[0] = cmd.Parameters["@o_return"].Value.ToString();
            arr_result[1] = cmd.Parameters["@o_return_Product_No"].Value.ToString();
            con.Close();
            return arr_result;
        }

        public int Update(int Product_Id, string CultureCode, string Product_Code, string Category_Code, int Manufacturer_Id, int VendorId,
                        int Product_TypeId, string Product_Name, string CurrencyCode, string NetPrice, string GrossPrice, int TaxRateId,
                        int DiscountId, int UnitsInStock, int UnitsInAPackage, int UnitsInBox, string Unit,
                        decimal Weight, string UnitOfWeightMeasure, decimal Length, decimal Width, decimal Height,
                        string UnitOfDimensionMeasure, int MinPurchaseQty, int MaxPurchaseQty, string Availability,
                        string StartDate, string EndDate, string PhotoFileName, string ThumbnailPhotoFileName, string Url,
                        string ShortDescription, string Specification, int OnlineTransactionFlag, int Discontinued)
        {
            string Alias = StringUtils.convertTitle2Link(Product_Name);
            NetPrice = StringUtils.RemoveExtraTextWithoutPointOrComma(NetPrice);
            GrossPrice = StringUtils.RemoveExtraTextWithoutPointOrComma(GrossPrice);

            SqlCommand cmd = new SqlCommand("[Production].[Products_Update]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Product_Id", Product_Id);
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            cmd.Parameters.AddWithValue("@Product_Code", Product_Code);
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@Manufacturer_Id", Manufacturer_Id);
            cmd.Parameters.AddWithValue("@Vendor_Id", VendorId);
            cmd.Parameters.AddWithValue("@Product_TypeId", Product_TypeId);
            cmd.Parameters.AddWithValue("@Product_Name", Product_Name);
            cmd.Parameters.AddWithValue("@Alias", Alias);
            cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
            cmd.Parameters.AddWithValue("@NetPrice", NetPrice);
            cmd.Parameters.AddWithValue("@GrossPrice", GrossPrice);
            cmd.Parameters.AddWithValue("@TaxRateId", TaxRateId);
            cmd.Parameters.AddWithValue("@Discount_Id", DiscountId);
            cmd.Parameters.AddWithValue("@UnitsInStock", UnitsInStock);
            cmd.Parameters.AddWithValue("@UnitsInAPackage", UnitsInAPackage);
            cmd.Parameters.AddWithValue("@UnitsInBox", UnitsInBox);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@Weight", Weight);
            cmd.Parameters.AddWithValue("@UnitOfWeightMeasure", UnitOfWeightMeasure);
            cmd.Parameters.AddWithValue("@Length", Length);
            cmd.Parameters.AddWithValue("@Width", Width);
            cmd.Parameters.AddWithValue("@Height", Height);
            cmd.Parameters.AddWithValue("@UnitOfDimensionMeasure", UnitOfDimensionMeasure);
            cmd.Parameters.AddWithValue("@MinPurchaseQty", MinPurchaseQty);
            cmd.Parameters.AddWithValue("@MaxPurchaseQty", MaxPurchaseQty);
            cmd.Parameters.AddWithValue("@Availability", Availability);
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.AddWithValue("@PhotoFileName", PhotoFileName);
            cmd.Parameters.AddWithValue("@ThumbnailPhotoFileName", ThumbnailPhotoFileName);
            cmd.Parameters.AddWithValue("@Url", Url);
            cmd.Parameters.AddWithValue("@ShortDescription", ShortDescription);
            cmd.Parameters.AddWithValue("@Specification", Specification);
            cmd.Parameters.AddWithValue("@OnlineTransactionFlag", OnlineTransactionFlag);
            cmd.Parameters.AddWithValue("@Discontinued", Discontinued);  
            cmd.Parameters.Add("@o_return", SqlDbType.Int, 2).Direction = ParameterDirection.Output;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int Delete(int Product_Id, string front_dir_path, string main_dir_path)
        {
            ProductFiles product_file_obj = new ProductFiles();
            string dir_file_path = HttpContext.Current.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["upload_file_dir"] + "/products/");
            DataTable dt = product_file_obj.GetListByProductId(Product_Id);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int File_Id = Convert.ToInt32(dt.Rows[i]["File_Id"].ToString());
                product_file_obj.Delete(File_Id, dir_file_path);
            }


            string[] array_list = new string[3];
            array_list = GetFilesByID(Product_Id);
            string PhotoFileName = string.Empty, ThumbnailPhotoFileName = string.Empty;
            if (array_list.Length > 0)
            {
                PhotoFileName = array_list[0].ToString();
                ThumbnailPhotoFileName = array_list[1].ToString();
            }

           
            if (System.IO.File.Exists(front_dir_path))
                FileUtils.DeleteFileWithFileNameAndPredefinedPath(PhotoFileName, front_dir_path);
            if (System.IO.File.Exists(main_dir_path))
                FileUtils.DeleteFileWithFileNameAndPredefinedPath(ThumbnailPhotoFileName, main_dir_path);

            SqlCommand cmd = new SqlCommand("[Production].[Products_Delete]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Product_Id", Product_Id);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int, 2) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int DeleteByProductNo(string Product_No, string front_dir_path, string main_dir_path)
        {
            ProductFiles product_file_obj = new ProductFiles();
            string dir_file_path = HttpContext.Current.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["upload_file_dir"] + "/products/");
            DataTable dt = product_file_obj.GetListByProductNo(Product_No, 1);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int File_Id = Convert.ToInt32(dt.Rows[i]["File_Id"].ToString());
                product_file_obj.Delete(File_Id, dir_file_path);
            }


            string[] array_list = new string[3];
            array_list = GetFilesByProductNo(Product_No);
            string PhotoFileName = string.Empty, ThumbnailPhotoFileName = string.Empty;
            if (array_list.Length > 0)
            {
                PhotoFileName = array_list[0].ToString();
                ThumbnailPhotoFileName = array_list[1].ToString();
            }

            
            if (System.IO.File.Exists(front_dir_path))
                FileUtils.DeleteFileWithFileNameAndPredefinedPath(PhotoFileName, front_dir_path);
            if (System.IO.File.Exists(main_dir_path))
                FileUtils.DeleteFileWithFileNameAndPredefinedPath(ThumbnailPhotoFileName, main_dir_path);

            SqlCommand cmd = new SqlCommand("[Production].[Products_Delete_ByProductNo]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Product_No", Product_No);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int, 2) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int UpdateUnitsOnOrder(int Product_Id, int UnitsOnOrder)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_UpdateUnitsOnOrder]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Product_Id", Product_Id);
            cmd.Parameters.AddWithValue("@UnitsOnOrder", UnitsOnOrder);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int, 2) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int UpdateTotalViews(string Product_No, int View)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_UpdateTotalViews]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Product_No", Product_No);
            cmd.Parameters.AddWithValue("@NewView", View);
            cmd.Parameters.Add("@o_return", SqlDbType.Int, 2).Direction = ParameterDirection.Output;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int UpdateProductState(string Product_No, string ProductState)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_UpdateProductState]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Product_No", Product_No);
            cmd.Parameters.AddWithValue("@ProductState", ProductState);
            cmd.Parameters.Add("@o_return", SqlDbType.Int, 2).Direction = ParameterDirection.Output;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int UpdateStatus(string Product_No, int Discontinued)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_UpdateStatus]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Product_No", Product_No);
            cmd.Parameters.AddWithValue("@Discontinued", Discontinued);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int, 2) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public DataTable SearchProductByKeyTypeKeyWordCultureCode(string KeyType, string KeyWord, int CultureCode)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_SearchProductByKeyTypeKeyWordCultureCode]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@KeyType", KeyType);
            cmd.Parameters.AddWithValue("@KeyWord", KeyWord);
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public static List<Product> SearchProductByKeywordsCultureCodeTotalItemCount(string Keywords, string CultureCode, int iTotalItemCount)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                string LowerKeywords = @Keywords.ToLower();

                var query = from x in dbContext.Products
                            where x.Product_Code.Contains(LowerKeywords)
                            || x.Category_Code.Contains(LowerKeywords)
                            || x.Product_Name.ToLower().Contains(LowerKeywords)
                            || x.Alias.Replace("-", " ").Contains(LowerKeywords)                            
                            || x.ShortDescription.Contains(LowerKeywords)                     
                            && x.CultureCode.Trim() == CultureCode && x.Discontinued == 2
                            orderby x.Product_Id descending
                            select x;
                int TotalItemCount = 0;
                if (query.Count() > 0)
                {
                    if (iTotalItemCount <= query.Count())
                        TotalItemCount = iTotalItemCount;
                    else
                        TotalItemCount = query.Count();
                }
                return query.Take(TotalItemCount).ToList();
            }
        }



        private string RemoveExtraText(string value)
        {
            //var allowedChars = "01234567890.,";
            var allowedChars = "01234567890";
            return new string(value.Where(c => allowedChars.Contains(c)).ToArray());
        }
    }

    
}

