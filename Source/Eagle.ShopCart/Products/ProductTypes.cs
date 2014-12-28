using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Eagle.Common.Utilities;
using System.Data.Common;
using System.Data.SqlClient;
using Eagle.Common;
using Eagle.Common.Settings;

namespace Eagle.ShopCart.Products
{
    public class ProductTypes
    {
        string ip = NetworkUtils.GetIP4Address();
        SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable(); 

        public ProductTypes()
        {
        }

        public DataTable GetListByVendorIdStatus(int VendorId, string Product_TypePublish)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Types_GetListByVendorIdStatus]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@Product_TypePublish", Product_TypePublish);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetListByVendorIdCategoryCodeStatus(int VendorId, string Category_Code, string Product_TypePublish)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Types_GetListByVendorIdCategoryCodeStatus]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@Product_TypePublish", Product_TypePublish);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }  

        public DataTable GetDetails(int Product_TypeId)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Types_GetDetails]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Product_TypeId", Product_TypeId);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public int Insert(int VendorId, string Category_Code, string Product_TypeName, string Product_TypeDescription, string Product_TypePublish)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Types_Insert]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@Product_TypeName", Product_TypeName);
            cmd.Parameters.AddWithValue("@Product_TypeDescription", Product_TypeDescription);
            cmd.Parameters.AddWithValue("@Product_TypePublish", Product_TypePublish);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int Update(int Product_TypeId, string Category_Code, string Product_TypeName, string Product_TypeDescription, string Product_TypePublish)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Types_Update]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@Product_TypeId", Product_TypeId);
            cmd.Parameters.AddWithValue("@Product_TypeName", Product_TypeName);
            cmd.Parameters.AddWithValue("@Product_TypeDescription", Product_TypeDescription);
            cmd.Parameters.AddWithValue("@Product_TypePublish", Product_TypePublish);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

       
        public int UpdateSortKey(int option, int id, int previous_id)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Types_UpdateSortKey]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@option", option);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@previous_id", previous_id);        
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int Delete(int VendorId, int Product_TypeId)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_Types_Delete]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@Product_TypeId", Product_TypeId);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }
    }
}
