using Eagle.Common.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Eagle.Repository.ShopCart.Manufacturers
{
    public class ManufacturerCategoryRepository
    {
        SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable();

        public ManufacturerCategoryRepository() { }

        public DataTable GetListByVendorIdDiscontinued(int VendorId, int Discontinued)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Manufacturer_Categories_GetListByVendorIdDiscontinued]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout =SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@Manufacturer_CategoryDiscontinued", Discontinued);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetDetails(int Manufacturer_CategoryId)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Manufacturer_Categories_GetDetails]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout =SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Manufacturer_CategoryId", Manufacturer_CategoryId);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public int Insert(int VendorId, string Manufacturer_CategoryName, string Manufacturer_CategoryDesc, int Discontinued)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Manufacturer_Categories_Insert]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout =SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@VendorId", VendorId);
            cmd.Parameters.AddWithValue("@Manufacturer_CategoryName", Manufacturer_CategoryName);
            cmd.Parameters.AddWithValue("@Manufacturer_CategoryDesc", Manufacturer_CategoryDesc);
            cmd.Parameters.AddWithValue("@Manufacturer_CategoryDiscontinued", Discontinued);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int Update(int Manufacturer_CategoryId, string Manufacturer_CategoryName, string Manufacturer_CategoryDesc, int Discontinued)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Manufacturer_Categories_Update]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout =SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Manufacturer_CategoryId", Manufacturer_CategoryId);
            cmd.Parameters.AddWithValue("@Manufacturer_CategoryName", Manufacturer_CategoryName);
            cmd.Parameters.AddWithValue("@Manufacturer_CategoryDesc", Manufacturer_CategoryDesc);
            cmd.Parameters.AddWithValue("@Manufacturer_CategoryDiscontinued", Discontinued);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int UpdateDiscontinued(int Manufacturer_CategoryId, string Discontinued)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Manufacturer_Categories_UpdateDiscontinued]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout =SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Manufacturer_CategoryId", Manufacturer_CategoryId);
            cmd.Parameters.AddWithValue("@Manufacturer_CategoryDiscontinued", Discontinued);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        } 
    }
}
