using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.ShopCart.Data;

namespace Eagle.ShopCart.Orders
{
    public class ShipOrderList
    {
        public int? Ship_CityId { get; set; }
        public string Ship_CityName { get; set; }
        public int Ship_TotalOrders { get; set; }
    }

    public class RevenueReports
    {
        public static List<ShipOrderList> GetTotalOrdersByCountryId(int CountryId)
        {
            using (ShopCartEntities db = new ShopCartEntities())
            {
                List<Order_Shipment> shipments = OrderShipmentController.GetList();                    
                //List<ShipOrderList> lst= (from s in shipments  
                //                          join c in db.ShippingCities
                //                          on s.Ship_CityId equals c.CityId
                //            where s.Ship_CountryId == CountryId
                //            group s by s.Ship_CityId into g
                //                  select new ShipOrderList { 
                //                      Ship_CityId = g.Key,                                       
                //                      Ship_TotalOrders = g.Count() 
                //                  }).ToList();

                List<ShipOrderList> lst = (from s in db.Order_Shipment
                                           join c in db.ShippingCities
                                           on s.Ship_CityId equals c.CityId
                                           where s.Ship_CountryId == CountryId
                                           group new {s,c} by new {s.Ship_CityId, c.CityName_Vi } into g
                                           select new ShipOrderList
                                           {
                                               Ship_CityId = g.Key.Ship_CityId,
                                               Ship_CityName = g.Key.CityName_Vi,                                                                                  
                                               Ship_TotalOrders = g.Select(s=>s.s.Order_No).Distinct().Count()                                               
                                               //SumPrice = (decimal?)g.Sum(pt => pt.p.UnitPrice),
                                           }).ToList();
                return lst;
            }
        }
    }
}
