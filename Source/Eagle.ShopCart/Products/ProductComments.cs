using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Common;
using System.Data;
using Eagle.ShopCart.Data;
using Eagle.Common.Utilities;
using Eagle.Common.Settings;


namespace Eagle.ShopCart.Products
{
    public class ProductComments
    {
        private static string IP = NetworkUtils.GetAddress(AddressType.IPv4);

        public static Product_Comments GetDetail(int CommentId)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                var query = (from pc in dbContext.Product_Comments
                            where pc.CommentId==CommentId
                            select pc).Single();
                return query;
            }
        }

        public static List<Product_Comments> GetListByProductNoStatus(string Product_No, string status)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                var query= from pc in dbContext.Product_Comments 
                           where pc.Publish==status 
                           select pc;
                if (!string.IsNullOrEmpty(Product_No))
                {
                    System.Guid product_no = new Guid(Product_No);
                    query.Where(x => x.Product_No == product_no);
                }
                return query.ToList();
            }
        }

        public static bool InsertData(string Product_No, string CommentName, string CommentEmail, string CommentText, bool IsReply, string Publish)
        {
            using (ShopCartEntities db = new ShopCartEntities())
            {
                System.Guid product_no = new Guid(Product_No);
                Product_Comments entity = new Product_Comments();
                entity.Product_No = product_no;
                entity.CommentName = CommentName;
                entity.CommentEmail = CommentEmail;
                entity.CommentText = CommentText;
                entity.IsReply = IsReply;
                entity.IPLog = IP;
                entity.Publish = Publish;
                entity.DatePosted = System.DateTime.Now;

                db.Product_Comments.Add(entity);
                db.SaveChanges();
                int CommentId = entity.CommentId;
                if (CommentId > 0) return true;
                else return false;
            }
        }

        public static int UpdateStatus(int CommentId, string Publish, string UserLastUpdated)
        {
            using (ShopCartEntities db = new ShopCartEntities())
            {
                System.Guid User = new Guid(UserLastUpdated);
                Product_Comments entity = db.Product_Comments.Single(p => p.CommentId == CommentId);          
                entity.Publish = Publish;
                entity.UserLastUpdated = User;
                entity.IPLastUpdated = IP;             
                int i = db.SaveChanges();
                return i;
            }
        }

        public static int Delete(int idx)
        {
            using (ShopCartEntities db = new ShopCartEntities())
            {
                Product_Comments entity = GetDetail(idx);
                db.Product_Comments.Remove(entity);
                int i = db.SaveChanges();
                return i;
            }
        }
    }
}
