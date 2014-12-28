using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Eagle.Common.Utilities;
using Eagle.Common;
using Eagle.ShopCart.Data;
using Eagle.Common.Settings;

namespace Eagle.ShopCart.Orders
{
    public class OrderShipCountry
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }

    public class OrderShipCity
    {
        public int CityId { get; set; }
        public string CityName_Vi { get; set; }
    }

    public class OrderShipmentController
    {
        SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable(); 

        public OrderShipmentController(){}

        public static List<OrderShipCountry> GetShipCountryListInOrders()
        {
            using (ShopCartEntities db = new ShopCartEntities())
            {
                List<OrderShipCountry> lst = (from s in db.Order_Shipment
                            join c in db.ShippingCountries
                            on s.Ship_CountryId equals c.CountryId
                            orderby s.Ship_CountryId descending
                            select new OrderShipCountry { 
                                CountryId = c.CountryId, CountryName = c.CountryName 
                            }).Distinct().ToList();
                return lst;
            }
        }

        public static List<OrderShipCity> GetShipCityListInOrders()
        {
            using (ShopCartEntities db = new ShopCartEntities())
            {
                List<OrderShipCity> lst = (from s in db.Order_Shipment
                                              join c in db.ShippingCities
                                              on s.Ship_CityId equals c.CityId
                                              orderby s.Ship_CityId descending
                                              select new OrderShipCity
                                              {
                                                  CityId = c.CityId,
                                                  CityName_Vi = c.CityName_Vi
                                              }).Distinct().ToList();
                return lst;
            }
        }

        public static List<Order_Shipment> GetList()
        {
            using (ShopCartEntities db = new ShopCartEntities())
            {
                var lst = (from c in db.Order_Shipment select c).ToList();
                return lst;
            }
        }

        public int Insert(string Order_No, string Customer_No, string Ship_ReceiverName, string Ship_ReceiverEmail, string Ship_ReceiverPhone, string Ship_ReceiverAddress, decimal Ship_Weight, int Ship_IsInternational)
        {
            SqlCommand cmd = new SqlCommand("[Sales].[Order_Shipment_Insert]", con) { CommandType = CommandType.StoredProcedure, CommandTimeout = SystemSettings.CommandTimeout };
            cmd.Parameters.AddWithValue("@Order_No", Order_No);
            cmd.Parameters.AddWithValue("@Customer_No", Customer_No);
            cmd.Parameters.AddWithValue("@Ship_ReceiverName", Ship_ReceiverName);
            cmd.Parameters.AddWithValue("@Ship_ReceiverEmail", Ship_ReceiverEmail);
            cmd.Parameters.AddWithValue("@Ship_ReceiverPhone", Ship_ReceiverPhone);
            cmd.Parameters.AddWithValue("@Ship_ReceiverAddress", Ship_ReceiverAddress);
            cmd.Parameters.AddWithValue("@Ship_Weight", Ship_Weight);
            cmd.Parameters.AddWithValue("@Ship_IsInternational", Ship_IsInternational);
            cmd.Parameters.Add(new SqlParameter("@o_return", SqlDbType.Int) { Direction = ParameterDirection.Output });
            con.Open();
            int i = cmd.ExecuteNonQuery();
            int retunvalue = (int)cmd.Parameters["@o_return"].Value;
            con.Close();
            return retunvalue;
        }
    }
}
