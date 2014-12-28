using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Eagle.Model;
using Eagle.Model.HR;
using Eagle.Core;
using System.Transactions;
namespace Eagle.Repository
{
    public class AssignEmpScheduleRepository
    {
        public EntityDataContext context { get; set; }
        public AssignEmpScheduleRepository(EntityDataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// the hien danh sach tat ca account khi page load khi moi bat dau page load
        /// </summary>
        /// <returns> danh sach all account khi  page load</returns>
        public List<EmployeeViewModel> ListBox(bool isAdmin, string userGroupID, int moduleID)
        {
            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();

                var tmp = from u in context.HR_tblEmp
                          where  jointable.Contains(u.EmpID) // join voi bang phan quyen du lieu
                          orderby u.FirstName
                          select new EmployeeViewModel()
                          {
                              EmpID = u.EmpID,
                              FullName = u.EmpCode + "--" + u.LastName+ " " + u.FirstName
                          };

                List<EmployeeViewModel> ret = new List<EmployeeViewModel>();
                foreach (var item in tmp)
                {
                    EmployeeViewModel p = new EmployeeViewModel()
                    {
                        EmpID = item.EmpID,
                        FullName = item.FullName
                    };
                    ret.Add(p);
                }
                return ret;
            }
            catch
            {
                return new List<EmployeeViewModel>();
            }
        }
        /// <summary>
        /// tra ve danh sach nhân viên không thuộc lịch được chọn ( lisbox ben trai)
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns>Danh sach accout</returns>
        public IEnumerable<AssignEmpScheduleCreateViewModel> ListUserNotIn(int ScheduleID, int iYear, bool isAdmin, string userGroupID, int moduleID)
        {
            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
                return from e in context.HR_tblEmp
                       orderby e.FirstName
                       where !(from u in context.TimeSheet_tblAssignEmpSchedule
                               where //u.ScheduleID == ScheduleID &&  
                               u.Year == iYear
                               select u.EmpID).Contains(e.EmpID)
                       && jointable.Contains(e.EmpID) // join voi bang phan quyen du lieu
                       select new AssignEmpScheduleCreateViewModel()
                       {
                           EmpID  = e.EmpID,
                           FullName = e.EmpCode + "--" + e.LastName + " " + e.FirstName
                       };
            }
            catch
            {
                return new List<AssignEmpScheduleCreateViewModel>();
            }
        }
        /// <summary>
        /// tra ve danh sach nhân viên  thuộc lịch được chọn được chọn
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns>Danh sach accout</returns>
        public IEnumerable<AssignEmpScheduleCreateViewModel> ListUserIn(int ScheduleID, int iYear, bool isAdmin, string userGroupID, int moduleID)
        {
            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
                return from p in context.TimeSheet_tblAssignEmpSchedule
                       join e in context.HR_tblEmp on p.EmpID equals e.EmpID
                       orderby e.FirstName
                       where (from u in context.TimeSheet_tblAssignEmpSchedule
                               where u.ScheduleID == ScheduleID &&  u.Year == iYear
                               select u.EmpID).Contains(e.EmpID)
                       && jointable.Contains(e.EmpID) // join voi bang phan quyen du lieu
                       select new AssignEmpScheduleCreateViewModel()
                       {
                           EmpID = e.EmpID,
                           FullName = e.EmpCode + "--" + e.LastName + " " + e.FirstName
                       };
            }
            catch
            {
                return new List<AssignEmpScheduleCreateViewModel>();
            }
        }
        public List<EmployeeViewModel> ListBox(string SearchTerm, int iYear, bool isAdmin, string userGroupID, int moduleID)
        {
            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();

                var tmp = from u in context.HR_tblEmp
                          where u.EmpCode.Contains(SearchTerm) || (u.LastName + " " + u.FirstName).Contains(SearchTerm)
                          orderby u.FirstName
                          where !(from u1 in context.TimeSheet_tblAssignEmpSchedule
                                  where u1.Year == iYear
                                  select u1.EmpID).Contains(u.EmpID)
                          && jointable.Contains(u.EmpID) // join voi bang phan quyen du lieu  
                          select new EmployeeViewModel()
                          {
                              EmpID = u.EmpID,
                              FullName = u.EmpCode + "--" + u.LastName + " " + u.FirstName
                          };

                List<EmployeeViewModel> ret = new List<EmployeeViewModel>();
                foreach (var item in tmp)
                {
                    EmployeeViewModel p = new EmployeeViewModel()
                    {
                        EmpID = item.EmpID,
                        FullName = item.FullName
                    };
                    ret.Add(p);
                }
                return ret;
            }
            catch
            {
                return new List<EmployeeViewModel>();
            }
        }
        /// <summary>
        /// Tra ve danh sach nhan vien co trong lich va nam duoc truyen vao
        /// </summary>
        /// <param name="SearchTerm"></param>
        /// <param name="iYear"></param>
        /// <returns></returns>
        public List<EmployeeViewModel> ListBoxInSchedule(string SearchTerm, int iYear, int ScheduleID, bool isAdmin, string userGroupID, int moduleID)
        {
            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
                var tmp = from u in context.HR_tblEmp
                          where u.EmpCode.Contains(SearchTerm) || (u.LastName + " " + u.FirstName).Contains(SearchTerm)
                          orderby u.FirstName
                          where (from u1 in context.TimeSheet_tblAssignEmpSchedule
                                  where u1.Year == iYear && u1.ScheduleID == ScheduleID
                                  select u1.EmpID).Contains(u.EmpID)
                          && jointable.Contains(u.EmpID) // join voi bang phan quyen du lieu
                          select new EmployeeViewModel()
                          {
                              EmpID = u.EmpID,
                              FullName = u.EmpCode + "--" + u.LastName + " " + u.FirstName
                          };

                List<EmployeeViewModel> ret = new List<EmployeeViewModel>();
                foreach (var item in tmp)
                {
                    EmployeeViewModel p = new EmployeeViewModel()
                    {
                        EmpID = item.EmpID,
                        FullName = item.FullName
                    };
                    ret.Add(p);
                }
                return ret;
            }
            catch
            {
                return new List<EmployeeViewModel>();
            }
        }
        public bool Add(TimeSheet_tblAssignEmpSchedule model, IEnumerable<string> lstBox)
        {
            using (EntityDataContext contextTrans = new EntityDataContext())
            {
                using (TransactionScope tranScope = new TransactionScope())
                {
                    try
                    {
                        // thuc hien them lai du lieu
                        foreach (var item in lstBox)
                        {
                            TimeSheet_tblAssignEmpSchedule addModel = new TimeSheet_tblAssignEmpSchedule()
                            {
                                EmpID =Int32.Parse(item),
                                ScheduleID = model.ScheduleID,
                                Year = model.Year,
                                Creater = model.Creater,
                                CreateTime = model.CreateTime
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
        public List<TimeSheet_tblAssignEmpSchedule> FindToDelete(int id,  List<int> lstBoxAssigned, int iYear)
        {

          
           return context.TimeSheet_tblAssignEmpSchedule
                           .Where(c => c.ScheduleID == id && c.Year == iYear && lstBoxAssigned.Contains(c.EmpID))
                           .ToList();
        }
        public bool Delete(int ScheduleID, int iYear,List<int> lstBox)
        {
            try
            {
                List<TimeSheet_tblAssignEmpSchedule> modelUpdate = FindToDelete(ScheduleID, lstBox, iYear);
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
    }

}
