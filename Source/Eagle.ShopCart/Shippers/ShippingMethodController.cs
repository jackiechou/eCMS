using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.ShopCart.Data;

namespace Eagle.ShopCart.Shippers
{
    public class ShippingMethodController
    {
        public static IQueryable<ShippingMethod> GetListByDiscontinued(string Discontinued)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                var _list = from m in dbContext.ShippingMethods
                                              where m.Discontinued == (Discontinued=="1"?true:false)
                                              select m;
                return _list;
            }
        }

        public static ShippingMethod GetDetails(int ShippingMethod_Id)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                ShippingMethod entity = (from sRate in dbContext.ShippingMethods
                         where sRate.ShippingMethod_Id == ShippingMethod_Id
                         select sRate).Single();
                return entity;
            }
        }

        public static int Insert(string ShippingMethod_Name, bool Discontinued)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                ShippingMethod shipping_method_obj = new ShippingMethod();
                shipping_method_obj.ShippingMethod_Name = ShippingMethod_Name;
                shipping_method_obj.Discontinued = Discontinued;
                dbContext.ShippingMethods.Add(shipping_method_obj);
                int i = dbContext.SaveChanges();
                return i;
            }
        }

        public static int Update(int ShippingMethod_Id, string ShippingMethod_Name, bool Discontinued)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                var shipping_method_obj = (from m in dbContext.ShippingMethods
                                           where m.ShippingMethod_Id == ShippingMethod_Id
                                           select m).First();
                shipping_method_obj.ShippingMethod_Name = ShippingMethod_Name;
                shipping_method_obj.Discontinued = Discontinued;
                int i = dbContext.SaveChanges();
                return i;
            }
        }
    }
}
