using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Eagle.Model;
using Eagle.Core;
using System.Transactions;
namespace Eagle.Repository.SYS.Roles
{
    public class UserRoleRepository
    {
        public EntityDataContext context { get; set; }
        public UserRoleRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<AccountViewModel> ListUserNotInGroup(int groupID)
        {
            try
            {
                return from p in context.SYS_tblUserAccount    
                        
                       select new AccountViewModel()
                       {
                           UserID = p.UserID,
                           UserName = p.UserName
                       };
            }
            catch
            {
                return new List<AccountViewModel>();
            }
        }

        public static List<int> GetGroupListByUserID(int? UserID)
        {
            using (EntityDataContext context = new EntityDataContext())
            {
                var groupList = (from g in context.SYS_tblUserGroup
                           where g.UserID == UserID
                           orderby g.GroupID
                             select g.GroupID).ToList();

                List<int> lst = new List<int>();
                if (groupList != null && groupList.Count() > 0)
                {
                    foreach (var item in groupList)
                    {
                        var exists = lst.Contains(item);
                        if (!exists)
                            lst.Add(item);
                    }
                }
                return lst;
            }
        }

        /// <summary>
        /// the hien danh sach tat ca account khi page load khi moi bat dau page load
        /// </summary>
        /// <returns> danh sach all account khi  page load</returns>
        public List<AccountViewModel> ListBox()
        {
            try
            {
                var tmp = from u in context.SYS_tblUserAccount
                          join e in context.HR_tblEmp on u.EmpID equals e.EmpID
                          orderby u.UserName
                          select new AccountViewModel()
                          {
                              UserID = u.UserID,
                              UserName = u.UserName + " -- " + e.LastName + " " + e.FirstName 
                          };

                List<AccountViewModel> ret = new List<AccountViewModel>();
                foreach (var item in tmp)
                {
                    AccountViewModel p = new AccountViewModel()
                    {
                        UserID = item.UserID,
                        UserName = item.UserName
                    };
                    ret.Add(p);
                }
                return ret;
            }
            catch
            {
                return new List<AccountViewModel>();
            }
        }
        /// <summary>
        /// tra ve danh sach account không thuộc group được chọn
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns>Danh sach accout</returns>
        public IEnumerable<AccountViewModel> ListUserNotIn(int? groupID)
        {
            try
            {
                return from p in context.SYS_tblUserAccount
                       join e in context.HR_tblEmp on p.EmpID equals e.EmpID
                       orderby p.UserName
                       where !(from u in context.SYS_tblUserGroup 
                                    where u.GroupID == groupID 
                                            select u.UserID).Contains(p.UserID)
                       select new AccountViewModel()
                       {
                           UserID = p.UserID,
                           UserName = p.UserName + " -- " + e.LastName + " " + e.FirstName
                       };
            }
            catch
            {
                return new List<AccountViewModel>();
            }
        }
        /// <summary>
        /// tra ve danh sach account không thuộc group được chọn
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns>Danh sach accout</returns>
        public IEnumerable<AccountViewModel> ListUserIn(int? groupID)
        {
            try
            {
                return from p in context.SYS_tblUserAccount
                       join e in context.HR_tblEmp on p.EmpID equals e.EmpID
                       orderby p.UserName
                       where (from u in context.SYS_tblUserGroup
                               where u.GroupID == groupID
                               select u.UserID).Contains(p.UserID)
                       select new AccountViewModel()
                       {
                           UserID = p.UserID,
                           UserName = p.UserName + " -- " + e.LastName + " " + e.FirstName
                       };
            }
            catch
            {
                return new List<AccountViewModel>();
            }
        }
        public SYS_tblUserGroup Find(int id)
        {
            return context.SYS_tblUserGroup.Find(id,null);
        }
        public List<SYS_tblUserGroup> FindToDelete(int id, List<int> lstBoxAssigned)
        {

            var tmp = context.SYS_tblUserGroup
                           .Where(c => c.GroupID == id && lstBoxAssigned.Contains(c.UserID))
                           .Select(c => new
                           {
                               UserID = c.UserID,
                               GroupID = c.GroupID
                           });


            List<SYS_tblUserGroup> ret = new List<SYS_tblUserGroup>();
            foreach (var item in tmp)
            {
                SYS_tblUserGroup p = new SYS_tblUserGroup()
                {
                    GroupID = item.GroupID,
                    UserID = item.UserID
                };
                ret.Add(p);
            }
            return ret;
        }
        public bool Add(SYS_tblUserGroup model)
        {
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            return true;
        }
        public bool Delete(int id, List<int> lstBox)
        {
            try
            {
                List<SYS_tblUserGroup> modelUpdate = FindToDelete(id, lstBox);

                foreach (var item in modelUpdate)
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return false;
            }

        }
        public bool Add(SYS_tblUserGroup model, IEnumerable<string> lstBox)
        {
            using (EntityDataContext contextTrans = new EntityDataContext())
            {
                using (TransactionScope tranScope = new TransactionScope())
                {
                    try
                    {
                           
                        // thuc hien xoa du lieu theo group truoc khi save du lieu
                       // int iResult = 0;
                        //bool bResult = Delete(model.GroupID);
                        //if (!bResult)
                        //    return false;
                        // thuc hien them lai du lieu
                        foreach ( var item in lstBox)
                        {
                            SYS_tblUserGroup addModel = new SYS_tblUserGroup()
                            {
                                GroupID = model.GroupID,
                                UserID = Int32.Parse(item)
                            };
                            context.Entry(addModel).State = System.Data.Entity.EntityState.Added;
                            context.SaveChanges();
                        }
                        // ket thuc transaction thanh cong
                        tranScope.Complete();
                        return true;
                    }
                    catch 
                    {
                        return false;
                    }
                  
                }
            }
        }
    }
}
