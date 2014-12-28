using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Eagle.Common;
using Eagle.Common.Settings;
using Eagle.Common.Utilities;
using Eagle.Common.Security.Cryptography;


namespace Eagle.ShopCart.Customers
{
    public class Customers
    {
        SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable();
        string ip = NetworkUtils.GetIP4Address();

        public Customers(){}

        public int CheckEmail(string Email)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Customers_CheckEmail]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int,2) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public string CheckExistedEmail(string Email)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Customers_CheckExistedEmail]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            string retunvalue = (string)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public void CheckLogin(string Email, string Password, out int status, out string o_userid, out string o_custname)
        {
            try
            {
                string HashedPassword = MD5Crypto.GetMd5Hash(Password);
                //md5_obj.verifyMd5Hash(source, hash);

                SqlCommand cmd = new SqlCommand("[Sales].[Customers_CheckLogin]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@HashedPassword", HashedPassword);
                cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int, 2) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@o_userid", SqlDbType.NVarChar, int.MaxValue) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@o_custname", SqlDbType.NVarChar, int.MaxValue) { Direction = ParameterDirection.Output });
                con.Open();
                cmd.ExecuteNonQuery();
                status = (int)cmd.Parameters["@o_return"].Value;
                o_userid = (string)cmd.Parameters["@o_userid"].Value;
                o_custname = (string)cmd.Parameters["@o_custname"].Value;
                con.Close();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        
        public DataTable GetDetailsByID(int idx)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Customers_GetDetailByID]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Customer_ID", idx);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetDetailsByCustomerNo(string Customer_No)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Customers_GetDetailsByCustomerNo]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Customer_No", Customer_No);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public string GetCustomerNoByEmail(string Email)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Customers_GetCustomerNoByEmail]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.Add(new SqlParameter("@Customer_No", SqlDbType.NVarChar, int.MaxValue) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            string retunvalue = (string)cmd.Parameters["@Customer_No"].Value;
            con.Close();
            return retunvalue;
        }

        public DataTable GetAll()
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Customers_GetAll]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetListByStatus(int Discontinued)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Customers_GetListByStatus]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Discontinued", Discontinued);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public int EditEmail(string Customer_No, string Email)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Customers_EditPassword]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Customer_No", Customer_No);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int, 2) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int EditProfile(int CustomerType_Id, string Customer_MemberCardNo, string Customer_No, string CustomerName, string ContactName, string IDCardNo, string PassPortNo, string TaxCode,
          string BirthDay, string Address, string Country, string PostalCode, string Phone, string Mobile, string Fax)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Customers_EditProfile]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@CustomerType_Id", CustomerType_Id);
            cmd.Parameters.AddWithValue("@Customer_MemberCardNo", Customer_MemberCardNo);
            cmd.Parameters.AddWithValue("@Customer_No", Customer_No);
            cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
            cmd.Parameters.AddWithValue("@ContactName", ContactName);
            cmd.Parameters.AddWithValue("@IDCardNo", IDCardNo);
            cmd.Parameters.AddWithValue("@PassPortNo", PassPortNo);
            cmd.Parameters.AddWithValue("@TaxCode", TaxCode);
            cmd.Parameters.AddWithValue("@BirthDay", BirthDay);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Country", Country);
            cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Mobile", Mobile);
            cmd.Parameters.AddWithValue("@Fax", Fax);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int, 2) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int EditPassword(string Customer_No, string PasswordSalt, string PasswordHash)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Customers_EditPassword]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Customer_No", Customer_No);
            cmd.Parameters.AddWithValue("@PasswordHash", PasswordHash);
            cmd.Parameters.AddWithValue("@PasswordSalt", PasswordSalt);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int, 2) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int UpdatePassword(string Email, string PasswordSalt, string PasswordHash)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Customers_UpdatePassword]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@PasswordHash", PasswordHash);
            cmd.Parameters.AddWithValue("@PasswordSalt", PasswordSalt);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int, 2) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public string CreateLostPassword(int PasswordLength)
        {
            string _allowedChars = "abcdefghijk0123456789mnopqrstuvwxyz";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
    
        public string MD5Crypt(string PasswordSalt)
        {
            string PasswordHash = MD5Crypto.GetMd5Hash(PasswordSalt);
            return PasswordHash;
        }

        public string SHA1Crypt(string PasswordSalt)
        {
            System.Text.UnicodeEncoding encoding = new System.Text.UnicodeEncoding();
            Byte[] hashBytes = encoding.GetBytes(PasswordSalt);
            // Compute the SHA-1 hash
            System.Security.Cryptography.SHA1CryptoServiceProvider sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            Byte[] cryptPassword = sha1.ComputeHash(hashBytes);
            return BitConverter.ToString(cryptPassword);
        }

        public string CreateRandomPassWord()
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Customers_CreateRandomPassword]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@len", 10);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.NVarChar, 10) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            string retunvalue = cmd.Parameters["@o_return"].Value.ToString();
            con.Close();
            return retunvalue;
        }

        public void Add(string CustomerName, string Address, string Phone, string Email,
          string PasswordSalt, out int status, out string o_customerno)
        {
            string PasswordHash = MD5Crypt(PasswordSalt);
            SqlCommand cmd = new SqlCommand("[Sales].[Customers_Add]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@PasswordHash", PasswordHash);
            cmd.Parameters.AddWithValue("@PasswordSalt", PasswordSalt);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int, 2) { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@o_CustomerNo", SqlDbType.NVarChar, int.MaxValue) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            status = (int)cmd.Parameters["@o_return"].Value;
            o_customerno = cmd.Parameters["@o_CustomerNo"].Value.ToString();
            con.Close();
        }

        public void Insert(int CustomerType_Id, string Customer_MemberCardNo, string CustomerName, string ContactName, string IDCardNo, string PassPortNo, string TaxCode, string BirthDay,
            string Address, string Country, string PostalCode, string Phone, string Mobile, string Fax, string Email,
            string PasswordHash, string PasswordSalt, int Verified, out int status, out string o_userid)
        {
            /*** BirthDay **************************************************************************************************/
            //System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            //dateInfo.ShortDatePattern = "MM/dd/yyyy";
            //IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
            //DateTime objDate = DateTime.Parse(BirthDay, culture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            //BirthDay = String.Format("{0:MM/dd/yyyy}", objDate);
            /***************************************************************************************************************/
           //int intDateDifference = DateDiff("yyyy", System.DateTime.Now, inputDate);
           //if strDifference < 18 Then
           //   Response.Write("Sorry you must be older than 18 years")
            SqlCommand cmd = new SqlCommand("[Sales].[Customers_Insert]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@CustomerType_Id", CustomerType_Id);
            cmd.Parameters.AddWithValue("@Customer_MemberCardNo", Customer_MemberCardNo);
            cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
            cmd.Parameters.AddWithValue("@ContactName", ContactName);
            cmd.Parameters.AddWithValue("@IDCardNo", IDCardNo);
            cmd.Parameters.AddWithValue("@PassPortNo", PassPortNo);
            cmd.Parameters.AddWithValue("@TaxCode", TaxCode);
            cmd.Parameters.AddWithValue("@BirthDay", BirthDay);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Country", Country);
            cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Mobile", Mobile);
            cmd.Parameters.AddWithValue("@Fax", Fax);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@PasswordHash", PasswordHash);
            cmd.Parameters.AddWithValue("@PasswordSalt", PasswordSalt);
            cmd.Parameters.AddWithValue("@Verified", Verified);
            cmd.Parameters.AddWithValue("@IPLog", ip);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int, 2) { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@o_userid", SqlDbType.NVarChar, int.MaxValue) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            status = (int)cmd.Parameters["@o_return"].Value;
            o_userid = cmd.Parameters["@o_userid"].Value.ToString();
            con.Close();
        }

        public int Update(int Customer_Id, int CustomerType_Id, string Customer_MemberCardNo, string CustomerName, string ContactName, string IDCardNo, string PassPortNo, string TaxCode, 
           string BirthDay, string Address, string Country, string PostalCode, string Phone, string Mobile, string Fax,
            string Email, string PasswordHash, string PasswordSalt, int Verified)
        {
            /*** BirthDay **************************************************************************************************/
            //System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            //dateInfo.ShortDatePattern = "MM/dd/yyyy";
            //IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
            //DateTime objDate = DateTime.Parse(BirthDay, culture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            //BirthDay = String.Format("{0:MM/dd/yyyy}", objDate);
            /***************************************************************************************************************/
            SqlCommand cmd = new SqlCommand("[Sales].[Customers_Update]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Customer_Id", Customer_Id);
            cmd.Parameters.AddWithValue("@CustomerType_Id", CustomerType_Id);
            cmd.Parameters.AddWithValue("@Customer_MemberCardNo", Customer_MemberCardNo);
            cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
            cmd.Parameters.AddWithValue("@ContactName", ContactName);
            cmd.Parameters.AddWithValue("@IDCardNo", IDCardNo);
            cmd.Parameters.AddWithValue("@PassPortNo", PassPortNo);
            cmd.Parameters.AddWithValue("@TaxCode", TaxCode);
            cmd.Parameters.AddWithValue("@BirthDay", BirthDay);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Country", Country);
            cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Mobile", Mobile);
            cmd.Parameters.AddWithValue("@Fax", Fax);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@PasswordHash", PasswordHash);
            cmd.Parameters.AddWithValue("@PasswordSalt", PasswordSalt);
            cmd.Parameters.AddWithValue("@Verified", Verified);
            cmd.Parameters.AddWithValue("@IPLog", ip);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int, 2) { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@o_userid", SqlDbType.NVarChar, int.MaxValue) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;    
        }
    }
}
