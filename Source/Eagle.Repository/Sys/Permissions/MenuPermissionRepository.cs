using Eagle.Common.Settings;
using Eagle.Core;
using Eagle.Model.SYS.Permission;
using Eagle.Model.SYS.Roles;
using Eagle.Repository.Sys.Permissions;
using Eagle.Repository.SYS.Roles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Eagle.Repository.Sys.Permissions
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

        public static string GenerateDynamicMenuPermissionTable(int ApplicationId, int MenuId)
        {
            string strHTML = string.Empty, strTableHeader = string.Empty, strTableBody = string.Empty;
            strTableHeader = "";
            List<PermissionViewModel> permission_lst = PermissionRepository.GetActiveList(PermissionCodeStatus.SYSTEM_MENU);

            foreach (var permission in permission_lst)
            {
                strTableHeader += "<th  class='text_center'><input  id='chkAll" + permission.PermissionKey + "' name='chkAll" + permission.PermissionKey + "' type='checkbox' value='" + permission.PermissionId + "'/>" + permission.PermissionName + "</th>";
            }

            List<RoleViewModel> role_lst = RoleRepository.GetList(ApplicationId);
            int i = 0;
            foreach (var role in role_lst)
            {
                strTableBody += "<tr>";
                strTableBody += "<td><input type='hidden' id='hdnRoleId_" + i + "' class='hdnRoleId excluded' name='hdnRoleId_" + i + "' value='" + role.RoleId + "' /> " + role.RoleName + "</td>";
                foreach (var item in permission_lst)
                {
                    if (i == 0)
                        strTableBody += "<td class='corlor_td center'><input data-rowid='" + i + "' class='chk" + item.PermissionKey + " excluded' id='chk" + item.PermissionKey + "_" + i + "' name='chk" + item.PermissionKey + "_" + i + "' type='checkbox' disabled='disabled' checked='checked' value='" + item.PermissionId + "'/></td>";
                    else
                        strTableBody += "<td class='corlor_td center'><input data-rowid='" + i + "' class='chk" + item.PermissionKey + " excluded' id='chk" + item.PermissionKey + "_" + i + "' name='chk" + item.PermissionKey + "_" + i + "' type='checkbox' value='" + item.PermissionId + "'/></td>";
                }
                strTableBody += "</tr>";
                i++;
            }
            strHTML = "<table id='tblRolePermission' class='use-dataTable table table-bordered table-condensed table-hover table-striped'>";
            strHTML += "<thead>";
            strHTML += "<tr>";
            strHTML += "<th class='text_center'>" + Eagle.Resource.LanguageResource.Role + "</th>";
            strHTML += strTableHeader;
            strHTML += "</tr>";
            strHTML += "</thead>";
            strHTML += "<tbody>";
            strHTML += strTableBody;
            strHTML += "</tbody>";
            strHTML += "</table>";
            return strHTML;
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
        //public static void InsertMenuPermission(List<RolePermissionViewModel> RolePermissionList, int MenuId, int UserId, bool AllowAccess)
        //{
        //    int RoleId = 0;
        //    List<PermissionViewModel> PermissionList = new List<PermissionViewModel>();
        //    foreach (var element in RolePermissionList)
        //    {
        //        RoleId = element.RoleId;
        //        PermissionList = element.PermissionList;
        //        foreach(var item in element.PermissionList)
        //        {
        //            Insert(MenuId, item.PermissionId, element.RoleId, UserId, AllowAccess);
        //        }                
        //    }
        //}

        public static int Insert(int MenuId, int PermissionId, int RoleId, int UserId, bool AllowAccess)
        {
            int returnValue = 0;
            using (EntityDataContext context = new EntityDataContext())
            {
                using (System.Transactions.TransactionScope transcope = new System.Transactions.TransactionScope())
                {
                    var query = from p in context.MenuPermissions where p.MenuId == MenuId && p.PermissionId == PermissionId && p.RoleId == RoleId select p;
                    if (query == null)
                    {
                        MenuPermission entity = new MenuPermission();
                        entity.MenuId = MenuId;
                        entity.PermissionId = PermissionId;
                        entity.RoleId = RoleId;
                        entity.UserId = UserId;
                        entity.AllowAccess = AllowAccess;
                        context.MenuPermissions.Add(entity);
                        returnValue = context.SaveChanges();
                        transcope.Complete();
                    }
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
