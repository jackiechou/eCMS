using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using Eagle.Common.Utilities;
using Eagle.Common;
using Eagle.Common.Settings;

using System.Runtime.Serialization;
using Eagle.ShopCart.Data;
using Eagle.Common.Security.Cryptography;

namespace Eagle.ShopCart.Vendors
{
    public class VendorController : IDeserializationCallback
    {
        string IP = NetworkUtils.GetIP4Address();
        [NonSerialized] SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable();

        public VendorController() { }

        void IDeserializationCallback.OnDeserialization(object sender)
        {
            //Recreate your connection here
            con = new SqlConnection();
            con.ConnectionString = SystemSettings.ConnectionString;
        }

        public int CheckVendorEmail(string Email)
        {
            SqlCommand cmd = new SqlCommand("[Purchasing].[Vendors_CheckEmail]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public DataTable GetListByPortalId(int PortalId)
        {
            SqlCommand cmd = new SqlCommand("[Purchasing].[Vendors_GetListByPortalId]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@PortalId", PortalId);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetDetails(int VendorId)
        {
            SqlCommand cmd = new SqlCommand("[Purchasing].[Vendors_GetDetails]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }
        
        //INSERT- UPDATE - DELETE --------------------------------------------------------------------------
        public static int Insert_Vendor_Temp(Guid ApplicationCode, string VendorName, string CurrencyCode, string Address, string Telephone,
            string Email, string StoreName, string Description, string Category_Code,
            string StoreCompanyName, string Title, string FirstName, string MiddleName, string LastName,
            int CountryId)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                Vendor_Temp obj = new Vendor_Temp();
                obj.ApplicationCode = ApplicationCode;
                obj.VendorName = VendorName;
                obj.CurrencyCode = CurrencyCode;
                obj.Address = Address;
                obj.Telephone = Telephone;
                obj.Email = Email;
                obj.StoreName = StoreName;
                obj.Authorized = false;                
                obj.Description = Description;
                obj.Category_Code = Category_Code;
                obj.StoreCompanyName = StoreCompanyName;
                obj.Title = Title;
                obj.FirstName = FirstName;
                obj.MiddleName = MiddleName;
                obj.LastName = LastName;
                obj.CountryId = CountryId;
                obj.CreatedOnDate = System.DateTime.Now;
                dbContext.Vendor_Temp.Add(obj);
                int i = dbContext.SaveChanges();
                return i;
            }            
        }


        public int Insert(string CreatedByUserId, Guid ApplicationCode, string Category_Code, string VendorName, string AddressLine1, string AddressLine2,
                        int CountryId, string PostalCode, string Cell, string SupportOnline,
                        string Hotline, string Telephone, string Fax, string Email,
                        string Website, string KeyWords, int Authorized, string StoreName, string StoreCompanyName, string TaxCode,
                        string LogoFile, string Slogan, string CurrencyCode, string CurrencySymbol,
                        string Decimals, string DecimalSymbol, string ThousandSeparator, string PositiveFormat,
                        string NegativeFormat, string TermsOfService, string Description,
                        int ContactTypeId, string ContactEmail, string PasswordSalt, string Title, string FirstName, string MiddleName, string LastName,
                      string Phone)
        {
            string PasswordHash = MD5Crypto.GetMd5Hash(PasswordSalt);

            SqlCommand cmd = new SqlCommand("[Purchasing].[Vendors_Insert]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@ApplicationCode", ApplicationCode);
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            cmd.Parameters.AddWithValue("@AddressLine1", AddressLine1);
            cmd.Parameters.AddWithValue("@AddressLine2", AddressLine2);
            cmd.Parameters.AddWithValue("@CountryId", CountryId);
            cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
            cmd.Parameters.AddWithValue("@Cell", Cell);
            cmd.Parameters.AddWithValue("@SupportOnline", SupportOnline);
            cmd.Parameters.AddWithValue("@Hotline", Hotline);
            cmd.Parameters.AddWithValue("@Telephone", Telephone);
            cmd.Parameters.AddWithValue("@Fax", Fax);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Website", Website);
            cmd.Parameters.AddWithValue("@KeyWords", KeyWords);
            cmd.Parameters.AddWithValue("@Authorized", Authorized);
            cmd.Parameters.AddWithValue("@StoreName", StoreName);
            cmd.Parameters.AddWithValue("@StoreCompanyName", StoreCompanyName);
            cmd.Parameters.AddWithValue("@TaxCode", TaxCode);
            cmd.Parameters.AddWithValue("@LogoFile", LogoFile);
            cmd.Parameters.AddWithValue("@Slogan", Slogan);
            cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
            cmd.Parameters.AddWithValue("@CurrencySymbol", CurrencySymbol);
            cmd.Parameters.AddWithValue("@Decimals", Decimals);
            cmd.Parameters.AddWithValue("@DecimalSymbol", DecimalSymbol);
            cmd.Parameters.AddWithValue("@ThousandSeparator", ThousandSeparator);
            cmd.Parameters.AddWithValue("@PositiveFormat", PositiveFormat);
            cmd.Parameters.AddWithValue("@NegativeFormat", NegativeFormat);
            cmd.Parameters.AddWithValue("@TermsOfService", TermsOfService);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@CreatedIP", IP);
            cmd.Parameters.AddWithValue("@ContactTypeId", ContactTypeId);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@MiddleName", MiddleName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@EmailAddress", Email);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@PasswordHash", PasswordHash);
            cmd.Parameters.AddWithValue("@PasswordSalt", PasswordSalt);
            cmd.Parameters.AddWithValue("@CreatedByUserId", CreatedByUserId);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int Update(Guid ApplicationCode, int VendorId, string VendorName, int CountryId, string PostalCode, 
                        string Cell, string SupportOnline, string Hotline, string Telephone, string Fax,
                        string Email, string Website, string KeyWords, int Authorized, string StoreName,
                        string StoreCompanyName, string TaxCode, string LogoFile, string Slogan, string TermsOfService, 
                        string Description, string LastModifiedByUserId)
        { 
            SqlCommand cmd = new SqlCommand("[Purchasing].[Vendors_Update]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@ApplicationCode", ApplicationCode);
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            cmd.Parameters.AddWithValue("@CountryId", CountryId);
            cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
            cmd.Parameters.AddWithValue("@Cell", Cell);
            cmd.Parameters.AddWithValue("@SupportOnline", SupportOnline);
            cmd.Parameters.AddWithValue("@Hotline", Hotline);
            cmd.Parameters.AddWithValue("@Telephone", Telephone);
            cmd.Parameters.AddWithValue("@Fax", Fax);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Website", Website);
            cmd.Parameters.AddWithValue("@KeyWords", KeyWords);
            cmd.Parameters.AddWithValue("@Authorized", Authorized);
            cmd.Parameters.AddWithValue("@StoreName", StoreName);
            cmd.Parameters.AddWithValue("@StoreCompanyName", StoreCompanyName);
            cmd.Parameters.AddWithValue("@TaxCode", TaxCode);
            cmd.Parameters.AddWithValue("@LogoFile", LogoFile);
            cmd.Parameters.AddWithValue("@Slogan", Slogan);
            cmd.Parameters.AddWithValue("@TermsOfService", TermsOfService);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@LastModifiedIP", IP);
            cmd.Parameters.AddWithValue("@LastModifiedByUserId", LastModifiedByUserId);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int Update_ClickThroughs(int VendorId)
        {
            SqlCommand cmd = new SqlCommand("[Purchasing].[Vendors_Update_ClickThroughs]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        //==================================================================================================
        
        public int InsertProductType(int VendorId, int ProductTypeId)
        {
            SqlCommand cmd = new SqlCommand("[Purchasing].[Vendor_ProductTypes_Insert]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@ProductTypeId", ProductTypeId);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }
        //=================================================================================================

        #region ORDER =====================================================================================
        public int DeletePromotion(int VendorId, int PromotionId)
        {
            SqlCommand cmd = new SqlCommand("[Purchasing].[Vendor_OrderPromotions_Delete]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@PromotionId", PromotionId);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }
        #endregion ========================================================================================
    }
}
