using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Eagle.Common;
using Eagle.Common.Utilities;
using System.Data.Common;
using System.Data.Linq;
using System.Web;
using Eagle.ShopCart.Data;

namespace Eagle.ShopCart.Products
{
    public partial class ProductGallery
    {
        private static string IP = NetworkUtils.GetAddress(AddressType.IPv4);

        public static List<Product_GalleryCollections> GetProductGalleryCollectionListByProductId(int Product_Id)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                var query = from pgc in dbContext.Product_GalleryCollections
                                            where pgc.Product_Id == Product_Id
                                select pgc;                  
                return query.ToList();
                
            }
        }

        public static int GetCollectionIdByProductId(int Product_Id)
        {            
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {              
                var Product_CollectionId = from pgc in dbContext.Product_GalleryCollections
                                            join gc in dbContext.Gallery_Collections
                                            on pgc.Product_CollectionId equals gc.CollectionId
                                            where pgc.Product_Id == Product_Id
                                            select pgc.Product_CollectionId;
                return Product_CollectionId.FirstOrDefault();                
            }
        }

        public static List<Gallery_Collections> GetCollectionList(int CollectionId)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                var query = from gf in dbContext.Gallery_Collections
                            where gf.CollectionId == CollectionId
                            select gf;
                return query.ToList();
            }
        }

        public static List<Gallery_Files> GetFileList(int CollectionId)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                var query = from gf in dbContext.Gallery_Files
                            where gf.CollectionId == CollectionId
                            select gf;
                return query.ToList();
            }
        }

        public static List<Gallery_Files> GetActiveListFileByCollectionId(int CollectionId)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                var query = from gc in dbContext.Gallery_Collections
                            join gf in dbContext.Gallery_Files
                            on gc.CollectionId equals gf.CollectionId
                            where gc.Status == "1" && gf.Status == "1"
                            && gf.CollectionId == CollectionId 
                            select gf;               
                return query.ToList();
            }
        }

        public static int InsertProductGallery(int Product_Id, int TopicId, string Title, string IconFile, string Description, string Tags, string Url,
                               string UserLog, string Status)
        {
            int i = 0;
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                System.Nullable<Int32> ListOrder = (int?)dbContext.Gallery_Collections.Max(x => (int?)x.CollectionId) ?? 0;
                Gallery_Collections entity = new Gallery_Collections();
                entity.TopicId = TopicId;
                entity.Title = Title;
                entity.IconFile = IconFile;
                entity.Description = Description;
                entity.ListOrder = ListOrder + 1;
                entity.CreatedOnDate = System.DateTime.Now;
                entity.LastModifieddDate = System.DateTime.Now;
                entity.Status = Status;
                entity.Tags = Tags;
                entity.Url = Url;
                entity.IPLog = IP;
                entity.IPLastUpdate = IP;
                dbContext.Gallery_Collections.Add(entity);
                dbContext.SaveChanges();

                int newCollectionId = entity.CollectionId;
                if (newCollectionId > 0)
                    i = ProductGallery.InsertProductGalleryCollection(newCollectionId, Product_Id);
            }
            return i;
        }

        public static int InsertProductGalleryCollection(int CollectionId, int Product_Id)
        {
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                System.Nullable<Int32> ListOrder = (int?)dbContext.Product_GalleryCollections.Max(x => (int?)x.Product_GalleryCollectionId) ?? 0;
                Product_GalleryCollections collection = new Product_GalleryCollections();
                collection.Product_Id = Product_Id;
                collection.Product_CollectionId = CollectionId;
                dbContext.Product_GalleryCollections.Add(collection);
                dbContext.SaveChanges();
                int newCollectionId = collection.Product_GalleryCollectionId;
                return newCollectionId;
            }
        }

        public static void Delete(int Product_Id, int CollectionId)
        {
            //Product Collection========================================================
            using (ShopCartEntities dbContext = new ShopCartEntities())
            {
                Product_GalleryCollections entity = dbContext.Product_GalleryCollections.Single(p => p.Product_CollectionId == CollectionId && p.Product_Id == Product_Id);
                dbContext.Product_GalleryCollections.Remove(entity);
                dbContext.SaveChanges();
            }

        }    

        //public static void Delete(int Product_Id, string dir_path_collection, string dir_path_collection_file)
        //{           
        //    //Product Collection========================================================
        //    using (ShopCartEntities dbContext = new ShopCartEntities())
        //    {
        //        int CollectionId = GetCollectionIdByProductId(Product_Id);               
        //        //Files======================================================================
        //        List<Gallery_Files> lst = GetFileList(CollectionId);
        //        foreach (var x in lst)
        //        {                   
        //            GalleryLibrary.GalleryFile.Delete(x.FileId, dir_path_collection_file);
        //        }
        //        //Collection================================================================
        //        GalleryLibrary.GalleryCollection.Delete(CollectionId, dir_path_collection);

        //        Product_GalleryCollections entity = dbContext.Product_GalleryCollections.Single(p => p.Product_CollectionId == CollectionId && p.Product_Id == Product_Id);
        //        dbContext.Product_GalleryCollections.DeleteObject(entity);
        //        dbContext.SaveChanges();
        //    }

        //}     
    }
}
