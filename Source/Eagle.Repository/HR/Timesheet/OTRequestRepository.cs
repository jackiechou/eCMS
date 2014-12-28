using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Eagle.Model;
using Eagle.Core;
using System.Transactions;
using System.Data.Entity.Validation;
namespace Eagle.Repository
{
    public class OTRequestRepository
    {
        public EntityDataContext context { get; set; }
        public OTRequestRepository(EntityDataContext context)
        {
            this.context = context; 
        }

        public OTRequestFillDataViewModel OTFillData(int? EmpID, DateTime? DateID)
        {
            OTRequestFillDataViewModel ret = new OTRequestFillDataViewModel();
            // kiem tra trong lich thay doi truoc 
            var strTypeDateChange = (from d in context.TimeSheet_tblScheduleDetail
                                     join c in context.Timesheet_tblScheduleChange on d.ScheduleID equals c.ScheduleID_To
                                     where c.EmpID == EmpID
                                     && (c.FromDate <= DateID && DateID <= c.ToDate)
                                     select d.LSShiftID).FirstOrDefault();

            
            if( strTypeDateChange == null)
            {
                // thuc hien kiem tra DateID thuoc ngay di lam hay ngay T7, CN, LE
                strTypeDateChange = (from d in context.TimeSheet_tblScheduleDetail
                                   join m in context.TimeSheet_tblAssignEmpSchedule 
                                            on d.ScheduleID equals m.ScheduleID
                                   where m.EmpID == EmpID && d.DateID == DateID
                                   select d.LSShiftID).FirstOrDefault();

            }
            // neu != null là co trong lich lam viec thi tra ve type date = rong la ngay thuong
            if (strTypeDateChange != null)
                ret.TypeDate = "";
            else
            {
                // kiem tra ngay do trong bang Holidays la T7, CN hay ngay le (S, H)
                ret.TypeDate = "date-" + (from d in context.Timesheet_tblHolidays
                                where d.DateID == DateID
                                select d.TypeDatestr).FirstOrDefault();
            }
            
            return ret;
        }
        public OTRequestViewModel FindEdit(int id)
        {
            try
            {
                var FunctionID = 478; 
                OTRequestViewModel ret = (from m in context.Timesheet_tblOTRecordMaster
                                          join d in context.Timesheet_tblOTRecordDetail on m.OTRecordID equals d.OTRecordID

                                          join op in context.LS_tblOnlineProcess on FunctionID equals op.FunctionID
                                          join pom in context.SYS_tblProcessOnlineMaster on op.DMQuiTrinhID equals pom.DMQuiTrinhID into tmpListzz1
                                          from list1 in tmpListzz1.DefaultIfEmpty()
                                          where m.OTRecordID == id
                                          select new OTRequestViewModel()
                                           {
                                            OTRecordID = m.OTRecordID,
                                            Year = m.Year,
                                            Month = m.Month,
                                            EmpID = m.EmpID,
                                            Status = m.Status,
                                            LevelApprove = m.LevelApprove,
                                            Comment = m.Comment,
                                            Creater = m.Creater,
                                            isFirstComment = m.isFirstComment,
                                            CurrentComment = m.CurrentComment,
                                            //// hien thi trang thai 
                                            s2 = list1.StatusLevel1A,
                                            s3 = list1.StatusLevel1U,
                                            s4 = list1.StatusLevel2A,
                                            s5 = list1.StatusLevel2U,
                                            s6 = list1.StatusLevel3A,
                                            s7 = list1.StatusLevel3U,
                                            s8 = list1.StatusLevel4A,
                                            s9 = list1.StatusLevel4U,
                                            s10 = list1.StatusLevel5A,
                                            s11 = list1.StatusLevel5U
                                            }).FirstOrDefault();             



                ret.MonthYear = new DateTime(ret.Year, ret.Month, 1);
                return ret;
            }
            catch
            {
                return new OTRequestViewModel();
            }
        }
        public List<OTRequestListViewModel> ListDetail(int id)
        {
            try
            {
                List<OTRequestListViewModel> ret = new List<OTRequestListViewModel>();
                ret = (from m in context.Timesheet_tblOTRecordMaster
                       join d in context.Timesheet_tblOTRecordDetail on m.OTRecordID equals d.OTRecordID
                       where m.OTRecordID == id
                       select new OTRequestListViewModel() { 
                          OTRecordDetailID =  d.OTRecordDetailID,
                          OTRecordID = d.OTRecordID,
                          DateID = d.DateID,
                          TimeInAM = d.TimeInAM,
                          TimeOutAM = d.TimeOutAM,
                          TimeInPM = d.TimeInPM,
                          TimeOutPM = d.TimeOutPM,
                          TotalHours = d.TotalHours,
                          NightOT = d.NightOT,
                          HolidayOT = d.HolidayOT,
                          Rate100 = d.Rate100,
                          Rate150= d.Rate150,
                          Rate200 = d.Rate200,
                          Rate300 = d.Rate300,
                          TOIL = d.TOIL
                       }).ToList();

                return ret;
                   
            }
            catch 
            {
                return new List<OTRequestListViewModel>();      
            }
        }
        public OTRequestViewModel OTDetail(int? EmpID, int? Year)
        {
            try
            {
                OTRequestViewModel ret = new OTRequestViewModel();
                // nguoi thuc hien tao Request
                ret.Creater = (from e in context.HR_tblEmp
                               where e.EmpID == EmpID
                               select e.LastName + " " + e.FirstName).FirstOrDefault();
                // thuc hien tinh gioi han OT ma nhan vien phai thuc hien. Neu trong bang Timesheet_tblOTLimit ko co thi sẽ dung
                // giới hạn mặc định trong phần thiết lập tham số hệ thống
                
                    var result = (from p in context.Timesheet_tblOTLimitByStaff
                               where p.EmpID == EmpID
                               select new { Hours = p.Hours }).FirstOrDefault();
                    if (result == null)
                    {
                        ret.OTLimit = null;
                    }
                    else
                    {
                        ret.OTLimit = result.Hours;
                    }
                if (ret.OTLimit == null)
                {
                    ret.OTLimit = (from p in context.SYS_tblParameter                                   
                                   select p.OTLimit).FirstOrDefault();
                }
               // Tinh so gio OT da thuc hien request
               ret.OTAccumulated =  (from m in context.Timesheet_tblOTRecordMaster
                                     where m.EmpID == EmpID && m.Year == Year
                                     && ( m.Status == 0 || m.Status == 1 || m.Status == 2 || m.Status == 4 || m.Status == 6 || m.Status == 8 || m.Status == 10)
                                     select m.TotalHours).ToList().Sum();

                // ket qua tra ve
                return ret;
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
                return new OTRequestViewModel();
            }
        }
        public bool Add(Timesheet_tblOTRecordMaster model, out string errorMessage)
        {
            try
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();

                errorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
            //(DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
               
        }
        public bool Update(Timesheet_tblOTRecordMaster model, out string errorMessage)
        {
            try
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                errorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        // xoa du lieu tren luoi danh sach leave record
        public bool Delete(int id)
        {
            try
            {
                var ListFromDatabase = context.Timesheet_tblOTRecordDetail.Where(p => p.OTRecordID == id).ToList();
                foreach (var deleteitem in ListFromDatabase)
                {
                    context.Entry(deleteitem).State = System.Data.Entity.EntityState.Deleted;
                }
                Timesheet_tblOTRecordMaster modelUpdate = Find(id);
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
        public Timesheet_tblOTRecordMaster Find(int id)
        {
            return context.Timesheet_tblOTRecordMaster.Include("Timesheet_tblOTRecordDetail").Where(p => p.OTRecordID == id).FirstOrDefault();
        }
        public bool CheckExist(List<OTRequestListViewModel> lstModel, OTRequestViewModel modelMaster)
        {
            foreach (var item in lstModel)
            {
                var ret = (from m in context.Timesheet_tblOTRecordMaster
                           join d in context.Timesheet_tblOTRecordDetail on  m.OTRecordID equals d.OTRecordID
                           where m.EmpID == modelMaster.EmpID
                           && d.DateID == item.DateID
                           select d.OTRecordID).FirstOrDefault();

                if (ret !=0)
                    return false;
            }

            return true;
        }
        public bool CheckExistUpdate(List<OTRequestListViewModel> lstModel, OTRequestViewModel modelMaster)
        {
            foreach (var item in lstModel)
            {
                var ret = (from m in context.Timesheet_tblOTRecordMaster
                           join d in context.Timesheet_tblOTRecordDetail on m.OTRecordID equals d.OTRecordID
                           where m.EmpID == modelMaster.EmpID
                           && d.DateID == item.DateID && d.OTRecordID != modelMaster.OTRecordID
                           select d.OTRecordID).FirstOrDefault();

                if (ret != 0)
                    return false;
            }

            return true;
        }
        /// <summary>
        ///  the hien danh sach cac don OT
        /// </summary>
        /// <param name="isAdmin"></param>
        /// <param name="userGroupID"></param>
        /// <param name="moduleID"></param>
        /// <param name="CurrentEmpID"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<OTRequestViewModel> ListRecord(bool isAdmin, string userGroupID, int? moduleID, int? CurrentEmpID, OTRequestViewModel model)
        {

            try
            {
                var FunctionID = 478; // functionID trong bang SYS_tblFunctionList
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();

                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => model.LSCompanyID == 0 || p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();


                var tmp = from p in context.Timesheet_tblOTRecordMaster
                          join e in context.HR_tblEmp on p.EmpID equals e.EmpID
                          
                          join op in context.LS_tblOnlineProcess on FunctionID equals op.FunctionID
                          join pom in context.SYS_tblProcessOnlineMaster on op.DMQuiTrinhID equals pom.DMQuiTrinhID into tmpListzz1
                          from list1 in tmpListzz1.DefaultIfEmpty()
                          where jointable.Contains(p.EmpID) // join voi bang phan quyen du lieu
                          && (model.LSCompanyID == 0 || model.LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                          && (p.EmpID == CurrentEmpID || (p.EmpID != CurrentEmpID && p.Status != 0))
                          && (p.Year == model.Year || model.Year == 0)
                          && (model.EmpCode == null || e.EmpCode.Contains(model.EmpCode))
                          && (model.FullName == null || (e.LastName + " " + e.FirstName).Contains(model.FullName))
                          select new OTRequestViewModel()
                          {
                              OTRecordID = p.OTRecordID,
                              Year = p.Year,
                              Month = p.Month,
                              Creater = e.LastName + " " + e.FirstName,
                              CreateTime = p.CreateTime,
                              Status = p.Status,
                              TotalHours = p.TotalHours,
                              NightOT = p.NightOT,
                              HolidaysOT = p.HolidaysOT,
                              //// hien thi trang thai 
                              s2 = list1.StatusLevel1A,
                              s3 = list1.StatusLevel1U,
                              s4 = list1.StatusLevel2A,
                              s5 = list1.StatusLevel2U,
                              s6 = list1.StatusLevel3A,
                              s7 = list1.StatusLevel3U,
                              s8 = list1.StatusLevel4A,
                              s9 = list1.StatusLevel4U,
                              s10 = list1.StatusLevel5A,
                              s11 = list1.StatusLevel5U

                          };

                List<OTRequestViewModel> ret = new List<OTRequestViewModel>();
                foreach (var item in tmp)
                {
                    OTRequestViewModel s = new OTRequestViewModel()
                    {
                        OTRecordID = item.OTRecordID,
                        Year = item.Year,
                        Month = item.Month,
                        Creater = item.Creater,
                        CreateTime = item.CreateTime,
                        Status = item.Status,
                        TotalHours = item.TotalHours,
                        NightOT = item.NightOT,
                        HolidaysOT = item.HolidaysOT,
                        // hien thi trang thai 
                        s2 = item.s2,
                        s3 = item.s3,
                        s4 = item.s4,
                        s5 = item.s5,
                        s6 = item.s6,
                        s7 = item.s7,
                        s8 = item.s8,
                        s9 = item.s9,
                        s10 = item.s10,
                        s11 = item.s11
                    };
                    ret.Add(s);
                }
                return ret;
            }
            catch
            {
                return new List<OTRequestViewModel>();
            }
        }
    }
}
