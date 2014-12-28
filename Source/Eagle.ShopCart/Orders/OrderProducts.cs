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


namespace Eagle.ShopCart.Orders
{
    public class OrderProducts
    {
        SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable();
        string Ip = NetworkUtils.GetIP4Address();
        public OrderProducts(){}

        public DataTable GetOrderDetailsByOrderID(string Order_ID)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Order_Details_GetOrderDetailsByOrderID]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Order_ID", Order_ID);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetListByOrderNo(string Order_No)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Order_Products_GetListByOrderNo]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Order_No", Order_No);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetOrderProductsByOrderNoProductNo(string Order_No, string Product_No)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Order_Products_GetDetailsByOrderNoProductNo]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Order_No", Order_No);
            cmd.Parameters.AddWithValue("@Product_No", Product_No);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public int Insert_OrderProducts(string Order_No, string Product_No, int OrderQty, decimal NetPrice, decimal TaxRate, decimal Discount, decimal GrossPrice, int VendorId)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Order_Products_Insert]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Order_No", Order_No);
            cmd.Parameters.AddWithValue("@Product_No", Product_No);
            cmd.Parameters.AddWithValue("@OrderQty", OrderQty);
            cmd.Parameters.AddWithValue("@NetPrice", NetPrice);
            cmd.Parameters.AddWithValue("@TaxRate", TaxRate);
            cmd.Parameters.AddWithValue("@Discount", Discount);
            cmd.Parameters.AddWithValue("@GrossPrice", GrossPrice);
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int UpdateProductQuantity_OrderDetails(string Order_No, int OrderQty)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Order_Details_UpdateProductQuantity]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Order_No", Order_No);           
            cmd.Parameters.AddWithValue("@OrderQty", OrderQty);           
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int Update_OrderProducts(string Order_No, string Product_No, int NewOrderQty, decimal NetPrice, decimal TaxRate, decimal Discount, decimal GrossPrice)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Order_Products_Update]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Order_No", Order_No);
            cmd.Parameters.AddWithValue("@Product_No", Product_No);
            cmd.Parameters.AddWithValue("@NewOrderQty", NewOrderQty);
            cmd.Parameters.AddWithValue("@NetPrice", NetPrice);
            cmd.Parameters.AddWithValue("@TaxRate", TaxRate);
            cmd.Parameters.AddWithValue("@Discount", Discount);
            cmd.Parameters.AddWithValue("@GrossPrice", GrossPrice);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int Delete_OrderProducts(string Order_No, string Product_No)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Order_Products_Delete]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Order_No", Order_No);
            cmd.Parameters.AddWithValue("@Product_No", Product_No);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }
    }
}
