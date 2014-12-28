using Eagle.Common.Settings;
using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Eagle.Repository.Sys.Menus
{
    public class MenuPermissionRepository
    {
        SqlConnection con = new SqlConnection(SystemSettings.ConnectionString);
        DataTable dt = new DataTable();
        public EntityDataContext context { get; set; }

        public MenuPermissionRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public MenuPermission GetDetails(int MenuPermissionId)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {
               
                MenuPermission entity = dbContext.MenuPermissions.Where(x => x.MenuPermissionId == MenuPermissionId).FirstOrDefault();
                return entity;
            }
        }

        public static List<MenuPermission> GetListByMenuId(int MenuId, int RoleId)
        {
            using (EntityDataContext dbContext = new EntityDataContext())
            {               
                var query = from x in dbContext.MenuPermissions
                            where x.MenuId == MenuId && x.RoleId == RoleId
                            select x;
                return query.ToList();
            }
        }


        ////INSERT- UPDATE - DELETE 
        public int InsertMenuPermission(int MenuId, int PermissionId, int RoleId, int UserId, bool AllowAccess)
        {
            int returnValue = 0;
            using (EntityDataContext dbContext = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    MenuPermission entity = new MenuPermission();
                    entity.MenuId = MenuId;
                    entity.PermissionId = PermissionId;
                    entity.RoleId = RoleId;
                    entity.UserId = UserId;
                    entity.AllowAccess = AllowAccess;
                    dbContext.MenuPermissions.Add(entity);
                    returnValue = dbContext.SaveChanges();
                    transcope.Complete();
                }
            }
            return returnValue;
        }

        public static int Delete(int MenuId)
        {
            int returnValue = 0;
            using (EntityDataContext context = new EntityDataContext())
            {
                List<MenuPermission> entity = context.MenuPermissions.Where(x => x.MenuId == MenuId).ToList();
                for (int i = 0; i < entity.Count; i++)
                {
                    context.MenuPermissions.Remove(entity[i]);
                    returnValue = context.SaveChanges();
                }
            }
            return returnValue;
        }
    }
}
