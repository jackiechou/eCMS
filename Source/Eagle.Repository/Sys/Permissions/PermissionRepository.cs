using Eagle.Core;
using Eagle.Model.SYS.Permission;
using Eagle.Model.SYS.Roles;
using Eagle.Repository.SYS.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Repository.Sys.Permissions
{
    public static class PermissionGroupKey
    {
        public static string SYSTEM_PAGE = "SYSTEM_PAGE";
        public static string SYSTEM_MODULE = "SYSTEM_MODULE";
        public static string SYSTEM_MENU = "SYSTEM_MENU";
    }

    public class PermissionRepository
    {
        public static Permission Find(int? Id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                return context.Permissions.Find(Id);
            }
        }

        public static bool IsIdExisted(int Id)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.Permissions.FirstOrDefault(c => c.PermissionId == Id);
                return (query != null);
            }
        }

        public static bool IsCodeExisted(string Code)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.Permissions.FirstOrDefault(p => p.PermissionCode.Equals(Code));
                return (query != null);
            }
        }

        public static bool IsDataExisted(string PermissionName, string PermissionKey)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var query = context.Permissions.FirstOrDefault(p => p.PermissionName.Equals(PermissionName) 
                    && p.PermissionKey.Equals(PermissionKey));
                return (query != null);
            }
        }

        public static List<PermissionViewModel> GetList()
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var lst = (from p in context.Permissions
                           select new PermissionViewModel
                           {
                               PermissionId = p.PermissionId,
                               PermissionCode = p.PermissionCode,
                               PermissionKey = p.PermissionKey,
                               PermissionName = p.PermissionName,
                               DisplayOrder = p.DisplayOrder,
                               IsActive = p.IsActive
                           }).ToList();
                return lst;
            }
        }

        public static List<PermissionViewModel> GetActiveList(string PermissionCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var lst = (from p in context.Permissions
                           where p.IsActive == true && p.PermissionCode == PermissionCode
                           orderby p.DisplayOrder
                           select new PermissionViewModel
                           {
                               PermissionId = p.PermissionId,
                               PermissionCode = p.PermissionCode,
                               PermissionKey = p.PermissionKey,
                               PermissionName = p.PermissionName
                           }).ToList();
                return lst;
            }
        }
        public static List<PermissionViewModel> GetPermissionCode(string PermissionCode)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var lst = (from p in context.Permissions
                           where p.PermissionCode == PermissionCode
                           select new PermissionViewModel
                           {
                               PermissionId = p.PermissionId,
                               PermissionCode = p.PermissionCode,
                               PermissionKey = p.PermissionKey,
                               PermissionName = p.PermissionName,
                               DisplayOrder = p.DisplayOrder,
                               IsActive = p.IsActive
                           }).ToList();
                return lst;
            }
        }

        public static string GenerateDynamicRolePermissionTable(int ApplicationId, string PermissionType)
        {
            string strHTML = string.Empty, strTableHeader = string.Empty, strTableBody = string.Empty;
            strTableHeader = "";
            List<PermissionViewModel> permission_lst = PermissionRepository.GetActiveList(PermissionType);

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

        public static string GenerateDynamicRolePermissionTable(int ApplicationId, string PermissionType, int Id)
        {
            string strHTML = string.Empty, strTableHeader = string.Empty, strTableBody = string.Empty;
            strTableHeader = "";
            List<PermissionViewModel> permission_lst = PermissionRepository.GetActiveList(PermissionType);

            foreach (var permission in permission_lst)
            {
                strTableHeader += "<th  class='text_center'><input  id='chkAll" + permission.PermissionKey + "' name='chkAll" + permission.PermissionKey + "' type='checkbox' value='" + permission.PermissionId + "'/>" + permission.PermissionName + "</th>";
            }

            //if(PermissionType == PermissionCodeStatus.SYSTEM_MENU)
            //if(PermissionType == PermissionCodeStatus.SYSTEM_MODULE)
            //if(PermissionType == PermissionCodeStatus.SYSTEM_PAGE)
            //if(PermissionType == PermissionCodeStatus.SYSTEM_FUNCTION)

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


        public static PermissionViewModel Details(int BannerId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                PermissionViewModel entity = new PermissionViewModel();
                entity = (from p in context.Permissions
                          select new PermissionViewModel
                          {
                              PermissionId = p.PermissionId,
                              PermissionCode = p.PermissionCode,
                              PermissionKey = p.PermissionKey,
                              PermissionName = p.PermissionName,
                              DisplayOrder = p.DisplayOrder,
                              IsActive = p.IsActive
                          }).FirstOrDefault();
                return entity;
            }
        }

        public static bool Insert(PermissionViewModel add_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool isDataDuplicate = IsDataExisted(add_model.PermissionName, add_model.PermissionKey);
                if (isDataDuplicate == false)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        Permission model = new Permission();
                        model.PermissionCode = add_model.PermissionCode;
                        model.PermissionName = add_model.PermissionName;
                        model.PermissionKey = add_model.PermissionKey;
                        model.DisplayOrder = add_model.DisplayOrder;
                        model.IsActive = add_model.IsActive;

                        int affectedRow = 0;
                        context.Entry(model).State = System.Data.Entity.EntityState.Added;
                        affectedRow = context.SaveChanges();
                        if (affectedRow == 1)
                        {
                            add_model.PermissionId = model.PermissionId;
                            Message = Eagle.Resource.LanguageResource.CreateSuccess;
                            result = true;
                        }
                    }
                }
                else
                {
                    result = false;
                    Message = Eagle.Resource.LanguageResource.DuplicateError;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                Message = Eagle.Resource.LanguageResource.SystemError;
            }
            return result;
        }

        public static bool Update(PermissionViewModel edit_model, out string Message)
        {
            Message = string.Empty;
            bool result = false;
            try
            {
                bool IdExist = IsIdExisted(edit_model.PermissionId);
                if (IdExist == true)
                {
                    Permission model = Find(edit_model.PermissionId);
                    if (model != null)
                    {
                        using (EntityDataContext context = new EntityDataContext())
                        {
                            model.PermissionCode = edit_model.PermissionCode;
                            model.PermissionName = edit_model.PermissionName;
                            model.PermissionKey = edit_model.PermissionKey;
                            model.DisplayOrder = edit_model.DisplayOrder;
                            model.IsActive = edit_model.IsActive;

                            context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                            int affectedRows = context.SaveChanges();
                            if (affectedRows == 1)
                            {
                                Message = Eagle.Resource.LanguageResource.UpdateSuccess;
                                result = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                Message = Eagle.Resource.LanguageResource.SystemError;
            }
            return result;
        }

        public static bool Delete(int? Id, out string Message)
        {
            bool result = false;
            Message = string.Empty;
            try
            {
                Permission model = Find(Id);
                if (model != null)
                {
                    using (EntityDataContext context = new EntityDataContext())
                    {
                        context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                        result = true;
                        Message = Eagle.Resource.LanguageResource.DeleteSuccess;
                    }
                }
                else
                {
                    Message = Eagle.Resource.LanguageResource.IdExisted;
                    result = false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = false;
                Message = Eagle.Resource.LanguageResource.DeleteFailure;
            }

            return result;
        }
    }
}
