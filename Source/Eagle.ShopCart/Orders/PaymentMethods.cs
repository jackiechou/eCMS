using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Eagle.Common;
using Eagle.Common.Settings;

namespace Eagle.ShopCart.Orders
{
    public class PaymentMethods
    {
        SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable(); 

        public PaymentMethods()
        {
        }

        public DataTable GetListByVendorIdStatus(int VendorId, int Status)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Payment_Methods_GetListByVendorIdStatus]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@Status", Status);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }
    }
}
