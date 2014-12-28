using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.ShopCart.Data;

namespace Eagle.ShopCart.Shippers
{
    public class ShippingRateController
    {
        public static List<ShippingRate> GetList()
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                List<ShippingRate> _list = (from s in dbContext.ShippingRates select s).ToList();
                return _list;
            }
        }

        public static IQueryable<object> GetListByCarrierMethodCountry(int ShippingCarrier_Id, int ShippingMethod_Id, int ShippingRate_CountryId)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                var query = (from sRate in dbContext.ShippingRates
                             join sCarrier in dbContext.ShippingCarriers on sRate.ShippingCarrier_Id equals sCarrier.ShippingCarrier_Id
                             join sMethod in dbContext.ShippingMethods on sRate.ShippingMethod_Id equals sMethod.ShippingMethod_Id
                             join sCountry in dbContext.ShippingCountries on sRate.ShippingRate_CountryId equals sCountry.CountryId
                             orderby sRate.ShippingRate_ListOrder descending
                             select new
                             {
                                 sCarrier.ShippingCarrier_Name,
                                 sMethod.ShippingMethod_Name,
                                 sCountry.CountryName,
                                 sRate.ShippingCarrier_Id,
                                 sRate.ShippingMethod_Id,
                                 sRate.ShippingRate_Id,
                                 sRate.ShippingRate_Name,
                                 sRate.ShippingRate_CountryId,
                                 sRate.ShippingRate_CurrencyCode,
                                 sRate.ShippingRate_ZipStart,
                                 sRate.ShippingRate_ZipEnd,
                                 sRate.ShippingRate_WeightStart,
                                 sRate.ShippingRate_WeightEnd,
                                 sRate.ShippingRate_Value,
                                 sRate.ShippingRate_VAT,
                                 sRate.ShippingRate_PackageFee,
                                 sRate.ShippingRate_ListOrder,

                             });


                if (ShippingCarrier_Id > 0)
                    query = query.Where(x => x.ShippingCarrier_Id == ShippingCarrier_Id);
                if (ShippingMethod_Id > 0)
                    query = query.Where(x => x.ShippingMethod_Id == ShippingMethod_Id);
                if (ShippingRate_CountryId > 0)
                    query = query.Where(x => x.ShippingRate_CountryId == ShippingRate_CountryId);
                return query;
            }
        }

        public static ShippingRate GetDetails(int ShippingRate_Id)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {                
                ShippingRate entity = (from sRate in dbContext.ShippingRates
                             where sRate.ShippingRate_Id == ShippingRate_Id
                             select sRate).Single();
                return entity;
            }
        }


        public static int AddData(int ShippingCarrier_Id, int ShippingMethod_Id, int ShippingRate_CountryId, string ShippingRate_Name, string ShippingRate_CurrencyCode, decimal ShippingRate_PackageFee,
            decimal ShippingRate_Value, decimal ShippingRate_VAT, decimal ShippingRate_WeightStart, decimal ShippingRate_WeightEnd,
            string ShippingRate_ZipStart, string ShippingRate_ZipEnd)
        {
            int i = 0;
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                System.Nullable<Int32> Max_Id = (from u in dbContext.ShippingRates select u.ShippingRate_ListOrder).DefaultIfEmpty(0).Max() + 1;

                ShippingRate shipping_rate_obj = new ShippingRate();
                shipping_rate_obj.ShippingCarrier_Id = ShippingCarrier_Id;

                shipping_rate_obj.ShippingRate_Name = ShippingRate_Name;
                shipping_rate_obj.ShippingRate_CountryId = ShippingRate_CountryId;
                shipping_rate_obj.ShippingRate_CurrencyCode = ShippingRate_CurrencyCode;
                shipping_rate_obj.ShippingRate_PackageFee = ShippingRate_PackageFee;
                shipping_rate_obj.ShippingRate_Value = ShippingRate_Value;
                shipping_rate_obj.ShippingRate_VAT = ShippingRate_VAT;
                shipping_rate_obj.ShippingRate_WeightStart = ShippingRate_WeightStart;
                shipping_rate_obj.ShippingRate_WeightEnd = ShippingRate_WeightEnd;
                shipping_rate_obj.ShippingRate_ZipStart = ShippingRate_ZipStart;
                shipping_rate_obj.ShippingRate_ZipEnd = ShippingRate_ZipEnd;
                shipping_rate_obj.ShippingRate_ListOrder = Convert.ToInt32(Max_Id);
                shipping_rate_obj.ShippingMethod_Id = ShippingMethod_Id;
                dbContext.ShippingRates.Add(shipping_rate_obj);
                i = dbContext.SaveChanges();
            }
            return i;
        }

        public static int UpdateData(int ShippingRate_Id, int ShippingCarrier_Id, int ShippingMethod_Id, int ShippingMethod_ListOrder, string ShippingRate_Name, int ShippingRate_CountryId, string ShippingRate_CurrencyCode, decimal ShippingRate_PackageFee,
            decimal ShippingRate_Value, decimal ShippingRate_VAT, decimal ShippingRate_WeightStart, decimal ShippingRate_WeightEnd,
            string ShippingRate_ZipStart, string ShippingRate_ZipEnd)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                var query = (from sRate in dbContext.ShippingRates where sRate.ShippingRate_Id == ShippingRate_Id select sRate).First();

                query.ShippingCarrier_Id = ShippingCarrier_Id;
                query.ShippingMethod_Id = ShippingMethod_Id;
                query.ShippingRate_Name = ShippingRate_Name;
                query.ShippingRate_CountryId = ShippingRate_CountryId;
                query.ShippingRate_CurrencyCode = ShippingRate_CurrencyCode;
                query.ShippingRate_PackageFee = ShippingRate_PackageFee;
                query.ShippingRate_Value = ShippingRate_Value;
                query.ShippingRate_VAT = ShippingRate_VAT;
                query.ShippingRate_WeightStart = ShippingRate_WeightStart;
                query.ShippingRate_WeightEnd = ShippingRate_WeightEnd;
                query.ShippingRate_ZipStart = ShippingRate_ZipStart;
                query.ShippingRate_ZipEnd = ShippingRate_ZipEnd;
                query.ShippingRate_ListOrder = ShippingMethod_ListOrder;
                
                int i = dbContext.SaveChanges();
                return i;
            }
        }
    }
}
