using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.ShopCart.Data;

namespace Eagle.ShopCart.Shippers
{
    public class ShippingCarrierController
    {
        public static IQueryable<ShippingCarrier> GetListByDiscontinued(bool Discontinued)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                IQueryable<ShippingCarrier> _list = from sCarrier in dbContext.ShippingCarriers
                                               where sCarrier.Discontinued == Discontinued
                                               select sCarrier;
                return _list;
            }
        }

        public static ShippingCarrier GetDetails(int ShippingCarrier_Id)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                ShippingCarrier entity = (from sCarrier in dbContext.ShippingCarriers
                                          where sCarrier.ShippingCarrier_Id == ShippingCarrier_Id
                                          select sCarrier).Single();
                return entity;
            }
        }

        public static int Insert(string ShippingCarrier_Name, bool Discontinued)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                System.Nullable<Int32> Max_Id = (from u in dbContext.ShippingCarriers select u.ShippingCarrier_Id).DefaultIfEmpty(0).Max() + 1;
                ShippingCarrier shipping_carrier_obj = new ShippingCarrier();
                shipping_carrier_obj.ShippingCarrier_Name = ShippingCarrier_Name;
                shipping_carrier_obj.ListOrder = Max_Id;
                shipping_carrier_obj.Discontinued = Discontinued;

                dbContext.ShippingCarriers.Add(shipping_carrier_obj);
                int i = dbContext.SaveChanges();
                return i;
            }
        }

        public static int Update(int ShippingCarrier_Id, string ShippingCarrier_Name, int ListOrder, bool Discontinued)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                ShippingCarrier entity = (from sCarrier in dbContext.ShippingCarriers
                             where sCarrier.ShippingCarrier_Id == ShippingCarrier_Id
                             select sCarrier).First();
                entity.ShippingCarrier_Name = ShippingCarrier_Name;
                entity.ListOrder = ListOrder;
                entity.Discontinued = Discontinued;
                int i = dbContext.SaveChanges();
                return i;
            }
        }
    }
}
