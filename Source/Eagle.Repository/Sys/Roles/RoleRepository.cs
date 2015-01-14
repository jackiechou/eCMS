using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Model;
using Eagle.Core;
using Eagle.Model.SYS.Roles;

namespace Eagle.Repository.SYS.Roles
{
    public class RoleRepository
    {
          public EntityDataContext context { get; set; }
        public RoleRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(string strValidate)
        {
            var code = context.SYS_tblGroup.FirstOrDefault(p => p.GroupCode.Equals(strValidate));
            return (code != null);
        }

        public static List<RoleModulePermissionViewModel> GetRolePermissions()
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    return (from p in context.Roles
                            select new RoleModulePermissionViewModel()
                            {
                                RoleName = p.RoleName,
                                RoleCode = p.RoleCode
                            }).ToList();
                }
                catch
                {
                    return new List<RoleModulePermissionViewModel>();
                }
            }
        }

        public static List<RoleViewModel> GetList(int ApplicationId)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                try
                {
                    return (from p in context.Roles
                            where p.ApplicationId == ApplicationId
                            select new RoleViewModel()
                            {
                                ApplicationId = p.ApplicationId,
                                RoleGroupId = p.RoleGroupId,
                                RoleId = p.RoleId,
                                RoleCode = p.RoleCode,
                                RoleName = p.RoleName,
                                LoweredRoleName = p.LoweredRoleName,
                                Description = p.Description
                            }).OrderBy(p=>p.RoleId).ToList();
                }
                catch
                {
                    return new List<RoleViewModel>();
                }
            }
        }
        public IEnumerable<GroupViewModel> List()
        {

            try
            {
                return from p in context.SYS_tblGroup
                       select new GroupViewModel()
                       {
                           GroupID = p.GroupID,
                           GroupCode = p.GroupCode,
                           GroupName = p.GroupName,
                           Used = p.Used,
                           Note = p.Note
                       };
            }
            catch
            {
                return new List<GroupViewModel>();
            }


        }
        /// <summary>
        /// the hien danh sach tat ca group khi page load khi moi bat dau page load
        /// </summary>
        /// <returns> danh sach all group khi  page load</returns>
        public List<GroupViewModel> ListBox()
        {
            try
            {
               
                    return (from u in context.SYS_tblGroup
                            orderby u.GroupName
                            where u.Used == true
                            select new GroupViewModel()
                            {
                                GroupID = u.GroupID,
                                GroupName = u.GroupName
                            }).ToList();
                //List<GroupViewModel> ret = new List<GroupViewModel>();
                //foreach (var item in tmp)
                //{
                //    GroupViewModel p = new GroupViewModel()
                //    {
                //        GroupID = item.GroupID,
                //        GroupName = item.GroupName
                //    };
                //    ret.Add(p);
                //}
                //return result.ToList();
            }
            catch
            {
                return new List<GroupViewModel>();
            }
        }
        public GroupCreateViewModel FindEdit(int id)
        {
            try
            {
                return  context.SYS_tblGroup
                       .Where (p => p.GroupID == id)
                       .Select(p => new GroupCreateViewModel()
                       {
                           GroupID = p.GroupID,
                           GroupCode = p.GroupCode,
                           GroupName = p.GroupName,
                           Used = p.Used,
                           Note = p.Note
                       }).FirstOrDefault();
            }
            catch
            {
                return new GroupCreateViewModel();
            }

        }
        public SYS_tblGroup Find(int id)
        {
            return context.SYS_tblGroup.Find(id);
        }
        public bool Add(SYS_tblGroup model)
        {
            model.GroupCode = model.GroupCode.ToUpper();
            if (CheckExist(model.GroupCode))
            {
                string errorMessage = Eagle.Resource.LanguageResource.ExistUser;
                return false;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            return true;
        }
        public bool Update(GroupCreateViewModel model,out string ErrorMessage)
        {
            try
            {
                model.GroupCode = model.GroupCode.ToUpper();
                if (CodeisExists(model.GroupCode,model.GroupID))
                {
                    ErrorMessage = Eagle.Resource.LanguageResource.CodeIsExists;
                    return false;
                }
                SYS_tblGroup modelUpdate = Find(model.GroupID);
                modelUpdate.GroupCode = model.GroupCode;
                modelUpdate.GroupName = model.GroupName;
                modelUpdate.Used = model.Used ;
                modelUpdate.Note = model.Note; 
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
            
        }

        private bool CodeisExists(string Code, int ID)
        {
            var result = context.SYS_tblGroup.Where(p => p.GroupCode == Code && p.GroupID != ID).FirstOrDefault();
            return result != null;
        }
        public bool Delete(int id)
        {
            try
            {
                SYS_tblGroup modelUpdate = Find(id);
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return false;
            }

        }
        #region Bind DropdownList
        private List<AutoCompleteViewModel> GetDropdownList(string searchTerm)
        {
            var tmp = context.SYS_tblGroup
                           .Where(c => c.GroupName.Contains(searchTerm) && c.Used == true)
                           .Select(c => new
                           {
                               id = c.GroupID,
                               name = c.GroupName,
                               text = ""
                           });
            List<AutoCompleteViewModel> ret = new List<AutoCompleteViewModel>();
            foreach (var item in tmp)
            {
                AutoCompleteViewModel p = new AutoCompleteViewModel()
                {
                    id = item.id.ToString(),
                    name = item.name,
                    text = item.text
                };
                ret.Add(p);
            }
            return ret;
        }
        // dùng cho bind dropdownlist
        public List<AutoCompleteViewModel> ListDropdown(string searchTerm, int pageSize, int pageNume)
        {
            return GetDropdownList(searchTerm).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
        }
        #endregion

    }
}
