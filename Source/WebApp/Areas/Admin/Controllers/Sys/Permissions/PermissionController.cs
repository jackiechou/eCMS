using Eagle.Model.SYS.Permission;
using Eagle.Repository.Sys.Permissions;
using Eagle.Repository.SYS.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.Sys.Permissions
{
    public class PermissionController : BaseController
    {
        //
        // GET: /Admin/Permission/

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        [SessionExpiration]
        public ActionResult LoadMenuRolePermission()
        {
            string strHTML = PermissionRepository.GenerateDynamicRolePermissionTable(ApplicationId, PermissionCodeStatus.SYSTEM_MENU);
            return PartialView("../Sys/Permissions/_RolePermission", strHTML);
        }

        [ChildActionOnly]
        [SessionExpiration]
        public ActionResult LoadModuleRolePermission()
        {
            string strHTML = PermissionRepository.GenerateDynamicRolePermissionTable(ApplicationId, PermissionCodeStatus.SYSTEM_MODULE);
            return PartialView("../Sys/Permissions/_RolePermission", strHTML);
        }

        [ChildActionOnly]
        [SessionExpiration]
        public ActionResult LoadPageRolePermission()
        {
            string strHTML = PermissionRepository.GenerateDynamicRolePermissionTable(ApplicationId, PermissionCodeStatus.SYSTEM_PAGE);
            return PartialView("../Sys/Permissions/_RolePermission", strHTML);
        }

        [ChildActionOnly]
        [SessionExpiration]
        public ActionResult LoadFunctionRolePermission()
        {
            string strHTML = PermissionRepository.GenerateDynamicRolePermissionTable(ApplicationId, PermissionCodeStatus.SYSTEM_FUNCTION);
            return PartialView("../Sys/Permissions/_RolePermission", strHTML);
        }
    }
}
