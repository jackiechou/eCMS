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
    public class ScheduleChangeRepository
    {
        public EntityDataContext context { get; set; }
        public ScheduleChangeRepository(EntityDataContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Save thong tin thay doi lich lam viec cua nhan vien
        /// </summary>
        /// <param name="model"> FromDate, ToDate, ScheduleID from, ScheduleID to</param>
        /// <param name="lstBox">Danh sach empID cua nhan vien</param>
        /// <returns>boolean</returns>
        public bool Add(Timesheet_tblScheduleChange model, List<int> lstBox,  out string outMessage)
        {

            #region add with transaction
            if ( CheckExist(model, lstBox))
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
                            Timesheet_tblScheduleChange addModel = new Timesheet_tblScheduleChange()
                            {
                                EmpID = item,
                                ScheduleID_To = model.ScheduleID_To,
                                ScheduleID_From = model.ScheduleID_From,
                                FromDate = model.FromDate,
                                ToDate = model.ToDate
                            };
                            
                            context.Entry(addModel).State = System.Data.Entity.EntityState.Added;
                            context.SaveChanges();
                        }
                        // ket thuc transaction thanh cong
                        tranScope.Complete();
                        outMessage = "";
                        return true;
                    }
                    catch( Exception ex)
                    {
                        outMessage = ex.Message;
                        return false;
                    }

                }
            }
            }
            else
            {
                outMessage = "Exist";
                return false;
            }
            #endregion
        }
        /// <summary>
        /// tra ve danh sach nhân viên  thuộc lịch được chọn được chọn
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns>Danh sach accout</returns>
        public IEnumerable<ScheduleChangeCreateViewModel> ListStaffFrom(int ScheduleID, int iYear,string strSearch = "")
        {
            try
            {
                return from p in context.TimeSheet_tblAssignEmpSchedule
                       join e in context.HR_tblEmp on p.EmpID equals e.EmpID
                       orderby e.FirstName
                       where (from u in context.TimeSheet_tblAssignEmpSchedule
                              where u.ScheduleID == ScheduleID && u.Year == iYear
                              select u.EmpID).Contains(e.EmpID)
                        && ( strSearch == "" || (e.LastName + " " + e.FirstName).Contains(strSearch) )
                       select new ScheduleChangeCreateViewModel()
                       {
                           EmpID = e.EmpID,
                           FullName = e.EmpCode + "--" + e.LastName + " " + e.FirstName
                       };
            }
            catch
            {
                return new List<ScheduleChangeCreateViewModel>();
            }
        }
        public Timesheet_tblScheduleChange Find(int id)
        {
            return context.Timesheet_tblScheduleChange.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                Timesheet_tblScheduleChange modelUpdate = Find(id);
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
        public bool CheckExist(Timesheet_tblScheduleChange model, List<int> lstBox)
        {
            foreach (var item in lstBox)
            {
                var code = context.Timesheet_tblScheduleChange
                       .FirstOrDefault(p => p.EmpID.Equals(item) 
                                            && p.ScheduleID_To == model.ScheduleID_To
                                            //&& 
                                            //(
                                            //    (p.FromDate >= model.FromDate && p.FromDate <= model.ToDate ) ||
                                            //    (p.ToDate >= model.FromDate && p.ToDate <= model.ToDate) ||
                                            //    (
                                            //        p.FromDate <= model.FromDate && model.FromDate <= p.ToDate
                                            //        &&
                                            //        p.FromDate <= model.ToDate && model.ToDate <= p.ToDate
                                            //    )
                                            //)
                                      );

                if (code != null)
                    return false;
                else
                    continue;

            }
            return true;
           
        }
        public List<ScheduleChangeCreateViewModel> List(bool isAdmin, string userGroupID, int moduleID)
        {

            try
            {
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();

                var tmp = from p in context.Timesheet_tblScheduleChange
                          join e in context.HR_tblEmp on p.EmpID equals e.EmpID
                          join f in context.TimeSheet_tblSchedule on p.ScheduleID_From equals f.ScheduleID
                          join t in context.TimeSheet_tblSchedule on p.ScheduleID_To equals t.ScheduleID
                          where (isAdmin == true || jointable.Contains(e.EmpID)) // join voi bang phan quyen du lieu
                          select new ScheduleChangeCreateViewModel()
                          {
                              ChangeScheduleID = p.ChangeScheduleID,
                              EmpID = p.EmpID,
                              FullName = e.LastName + " " + e.FirstName,
                              ScheduleName_From = f.ScheduleName,
                              ScheduleName_To = t.ScheduleName,
                              ScheduleID_From = p.ScheduleID_From,
                              ScheduleID_To = p.ScheduleID_To,
                              FromDate = p.FromDate,
                              ToDate = p.ToDate,
                              Note = p.Note
                          };

                List<ScheduleChangeCreateViewModel> ret = new List<ScheduleChangeCreateViewModel>();
                foreach (var item in tmp)
                {
                    ScheduleChangeCreateViewModel s = new ScheduleChangeCreateViewModel()
                    {
                        ChangeScheduleID = item.ChangeScheduleID,
                        EmpID =item.EmpID,
                        FullName = item.FullName,
                        ScheduleID_From = item.ScheduleID_From,
                        ScheduleID_To = item.ScheduleID_To,
                        ScheduleName_From = item.ScheduleName_From,
                        ScheduleName_To = item.ScheduleName_To,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Note = item.Note
                    };
                    ret.Add(s);
                }
                return ret;
            }
            catch
            {
                return new List<ScheduleChangeCreateViewModel>();
            }
        }
    }
}
