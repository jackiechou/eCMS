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


namespace Eagle.ShopCart.Vendors
{
    public class ContactTypes
    {
        string IP = NetworkUtils.GetIP4Address();
        SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable();    

        public ContactTypes()
        {
        }

        public DataTable GetAll()
        {
            SqlCommand cmd = new SqlCommand("[Person].[ContactType_GetAll]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetListById(int ContactTypeId)
        {
            SqlCommand cmd = new SqlCommand("[Person].[ContactType_GetDetailById]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@ContactTypeId", ContactTypeId);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }
    }
}
