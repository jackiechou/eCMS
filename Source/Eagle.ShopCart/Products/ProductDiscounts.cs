using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Eagle.Common;

using Eagle.Common.Utilities;
using Eagle.Common.Settings;

namespace Eagle.ShopCart.Products
{
    public class ProductDiscounts
    {
        SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable(); 

        public ProductDiscounts(){}

        public DataTable GetListByVendorId(int VendorId)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Discounts_GetListByVendorId]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetDetails(int DiscountId)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Discounts_GetDetails]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@DiscountId", DiscountId);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetDetailByVendorIdDiscountId(int VendorId, int DiscountId)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Discounts_GetDetailByVendorIdDiscountId]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@DiscountId", DiscountId);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public int Insert(int VendorId, int DiscountQty, string DiscountRate, int IsPercent, string Description, string StartDate, string EndDate)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Discounts_Insert]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@DiscountQty", DiscountQty);
            cmd.Parameters.AddWithValue("@DiscountRate", DiscountRate);
            cmd.Parameters.AddWithValue("@IsPercent", IsPercent);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int Update(int VendorId, int DiscountId, int DiscountQty, string DiscountRate, int IsPercent, string Description, string StartDate, string EndDate)
        {
            DiscountRate = StringUtils.RemoveExtraTextWithoutPointOrComma(DiscountRate);
            SqlCommand cmd = new SqlCommand("[Production].[Product_Discounts_Update]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@DiscountId", DiscountId);
            cmd.Parameters.AddWithValue("@DiscountQty", DiscountQty);
            cmd.Parameters.AddWithValue("@DiscountRate", DiscountRate);
            cmd.Parameters.AddWithValue("@IsPercent", IsPercent);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int Delete(int VendorId, int DiscountId)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Discounts_Delete]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@DiscountId", DiscountId);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }
    }
}
