using Eagle.Core;
using Eagle.Model.SYS.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Repository.SYS.Permissions
{
    public class ModulePermissionRepository
    {
        public List<ModulePermissionViewModel> GetModulePermissionList(Guid RoleCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                List<ModulePermissionViewModel> lst = new List<ModulePermissionViewModel>();
                try
                {

                    string strCommand = @"EXEC [Cms].[ModulePermissions_GetPivotList] @RoleCode={0}";
                    lst = context.Database.SqlQuery<ModulePermissionViewModel>(strCommand, RoleCode).ToList();
                }
                catch (Exception ex) { ex.ToString(); }
                return lst;
            }
        }
    }
}
