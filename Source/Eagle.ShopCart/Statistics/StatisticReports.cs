using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.ShopCart.Data;
using Eagle.Common;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;
using Eagle.Common.Settings;

namespace Eagle.ShopCart.Statistics
{
    public class StatisticReports
    {
        public StatisticReports(){ }

        SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable();

        #region PRODUCTS ================================================================================
        public SqlDataReader GetProductCountByProductCategory()
        {
            SqlCommand cmd = new SqlCommand("[Production].[Products_GetProductCountByProductCategory]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Close();
            con.Close();
            return dr;
        }
        #endregion PRODUCTS =============================================================================

        #region ORDERS ==================================================================================
        public static List<int> GetPublishedOrderYearList(){
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {              
                List<int> distinctOrderYearList = (from o in dbContext.Orders select o.OrderDate.Year).Distinct().ToList();
                return distinctOrderYearList;
            }
        }
        public static decimal GetSalesByPeriod(DateTime StartDate, DateTime EndDate)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                decimal SumTotalFee = 0;
                List<Order> list = new List<Order>();
                IQueryable<Order> query = from x in dbContext.Orders
                                          where x.OrderDate >= StartDate && x.OrderDate <= EndDate
                                          select x;
                list = query.ToList();
                if (list.Count > 0)
                    SumTotalFee = list.Sum(x => x.TotalFees);
                return SumTotalFee;
            }
        }

        public SqlDataReader GetSalesByProductCategoryOrderPeriod(string Category_Code, DateTime StartDate, DateTime EndDate)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Orders_GetSalesByProductCategoryOrderPeriod]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@StartDate", StartDate);            
            cmd.Parameters.AddWithValue("@EndDate", EndDate); 
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Close();
            con.Close();
            return dr;
        }

        public DataTable GetTotalOrdersByProductCategoryOrderYear(string Category_Code, string OrderYear)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Orders_GetTotalOrdersByProductCategoryOrderYear]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Category_Code", Category_Code);
            cmd.Parameters.AddWithValue("@OrderYear", OrderYear);
            con.Open();
            using (var dr = cmd.ExecuteReader()) { dt.Load(dr); }
            con.Close();
            return dt;
        }
        #endregion ORDERS ===============================================================================

      

    }
}
