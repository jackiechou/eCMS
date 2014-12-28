﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Eagle.Common.Utilities;
using System.Data.Common;
using System.Data.SqlClient;
using Eagle.Common;
using Eagle.Common.Settings;


namespace Eagle.ShopCart.Taxes
{
    public class Taxes
    {
        SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable();
        string ip = NetworkUtils.GetIP4Address();

        public Taxes() { }

        public DataTable GetList()
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_TaxRates_GetList]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public DataTable GetDetails(int TaxRateId)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_TaxRates_GetDetails]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@TaxRateId", TaxRateId);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }

        public int Insert(decimal TaxRate, int IsPercent, string Description)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_TaxRates_Insert]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@TaxRate", TaxRate);
            cmd.Parameters.AddWithValue("@IsPercent", IsPercent);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }

        public int Update(int TaxRateId, decimal TaxRate, int IsPercent, string Description)
        {
            SqlCommand cmd = new SqlCommand("[Production].[Product_TaxRates_Update]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@TaxRateId", TaxRateId);
            cmd.Parameters.AddWithValue("@TaxRate", TaxRate);
            cmd.Parameters.AddWithValue("@IsPercent", IsPercent);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }      
    }
}
