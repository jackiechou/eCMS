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
    public class CurrencyClass
    {
        SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable();     

        public CurrencyClass()
        {
        }

        public DataTable GetList()
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Currency_GetList]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetListByDiscontinued(int Discontinued)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Currency_GetListByDiscontinued]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Discontinued", Discontinued);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }


        public DataTable GetListByVendorIdDiscontinued(int VendorId, int Discontinued)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Currency_GetListByVendorIdDiscontinued]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@Discontinued", Discontinued);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetDetails(string CurrencyCode)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Currency_GetDetails]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public int Insert(string CurrencyCode, string Name, string Discontinued)
        {     
            SqlCommand cmd = new SqlCommand("[Sales].[Currency_Insert]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Discontinued", Discontinued);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int Update(string CurrencyCode, string Name, string Discontinued)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Currency_Update]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Discontinued", Discontinued);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int UpdateDiscontinued(string CurrencyCode, string Discontinued)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Currency_UpdateDiscontinued]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);            
            cmd.Parameters.AddWithValue("@Discontinued", Discontinued);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        private string RemoveExtraText(string value)
        {
            //var allowedChars = "01234567890.,";
            var allowedChars = "01234567890";
            return new string(value.Where(c => allowedChars.Contains(c)).ToArray());
        }
    }
}
