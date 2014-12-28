using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Eagle.Model;
using Eagle.Core;
using System.Data.Entity.Validation;
namespace Eagle.Repository
{
    public class ScheduleRepository
    {
        public EntityDataContext context { get; set; }
        public ScheduleRepository(EntityDataContext context)
        {
            this.context = context;
        }
        #region Master
            /// <summary>
            /// kiem tra trung khi save mới
            /// </summary>
            /// <param name="strValidate"></param>
            /// <returns></returns>
            public bool CheckExist(string strValidate)
            {
                var code = context.TimeSheet_tblSchedule.FirstOrDefault(p => p.ScheduleCode.Equals(strValidate));
                return (code != null);
            }
             /// <summary>
           /// Kiem tra duplicate code  khi edit cho truong hop cho chinh sua Code
           /// </summary>
           /// <param name="strValidate"></param>
           /// <returns></returns>
            public bool CheckExistEdit(string strValidate, int ScheduleID)
            {
                var code = context.TimeSheet_tblSchedule
                            .FirstOrDefault(p => p.ScheduleCode.Equals(strValidate) && p.ScheduleID != ScheduleID );
                return (code != null);
            }
            public bool Add(TimeSheet_tblSchedule model,out string outMessage)
            {
                try
                {
                    if (CheckExist(model.ScheduleCode))
                    {
                        outMessage = Eagle.Resource.LanguageResource.ExistCode;
                        return false; 
                    }
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    context.SaveChanges();
                    outMessage = "";
                    return true;
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    return false;
                
                }
            }
            public bool Update(ScheduleCreateViewModel model, out string outMessage)
            {

                try
                {
                    if (CheckExistEdit(model.ScheduleCode, model.ScheduleID))
                    {
                        outMessage = Eagle.Resource.LanguageResource.ExistCode;
                        return false;
                    }
                    TimeSheet_tblSchedule modelUpdate = Find(model.ScheduleID);
                    modelUpdate.ScheduleCode = model.ScheduleCode;
                    modelUpdate.ScheduleName = model.ScheduleName;
                    modelUpdate.Used = model.Used;
                    modelUpdate.Note = model.Note;
                    context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    outMessage = "";
                    return true;
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    return false;
                }
            }
            public IEnumerable<ScheduleViewModel> List()
            {

                try
                {
                    var tmp = from p in context.TimeSheet_tblSchedule
                              select new ScheduleViewModel()
                              {
                                  ScheduleID = p.ScheduleID,
                                  ScheduleCode = p.ScheduleCode,
                                  ScheduleName = p.ScheduleName,
                                  Used   = p.Used,
                                  Note = p.Note
                                  
                              };

                    List<ScheduleViewModel> ret = new List<ScheduleViewModel>();
                    foreach (var item in tmp)
                    {
                        ScheduleViewModel s = new ScheduleViewModel()
                        {
                            ScheduleID = item.ScheduleID,
                            ScheduleCode = item.ScheduleCode,
                            ScheduleName = item.ScheduleName,
                            Note = item.Note,
                            Used = item.Used
                        };
                        ret.Add(s);
                    }
                    return ret;
                }
                catch
                {
                    return new List<ScheduleViewModel>();
                }
            }
            public List<ScheduleDetailListViewModel> ListScheduleDetail(int iYear, int ScheduleID)
            {

                try
                {
                    var tmp = from p in context.TimeSheet_tblScheduleDetail 
                              join H in context.Timesheet_tblHolidays on p.DateID equals H.DateID
                              where p.ScheduleID == ScheduleID && p.Year == iYear

                              select new ScheduleDetailListViewModel()
                              {
                                  LSShiftID =  p.LSShiftID,
                                  Day = p.DateID.Day,
                                  M = p.DateID.Month,
                                  Year = p.DateID.Year,
                                  TypeDate = H.TypeDate,
                                  //strTypeDate = p.Timesheet_tblShift.LSShiftCode
                                  strTypeDate = H.TypeDatestr
                              };

                    List<ScheduleDetailListViewModel> ret = new List<ScheduleDetailListViewModel>();
                    foreach (var item in tmp)
                    {
                        ScheduleDetailListViewModel s = new ScheduleDetailListViewModel()
                        {
                            //ScheduleID = item.ScheduleID,
                            LSShiftID = item.LSShiftID,
                            Day = item.Day,
                            M = item.M,
                            Year = item.Year,
                            TypeDate= item.TypeDate,
                            strTypeDate = item.strTypeDate
                        };
                        ret.Add(s);
                    }
                    return ret;
                }
                catch
                {
                    return new List<ScheduleDetailListViewModel>();
                }
            }
            public List<ScheduleDetailListViewModel> ListFromHolidays(int iYear)
            {

                try
                {
                    var tmp = from p in context.Timesheet_tblHolidays
                                .Where(p => p.DateID.Year == iYear)

                              select new ScheduleDetailListViewModel()
                              {
                                  Day = p.DateID.Day,
                                  M = p.DateID.Month,
                                  Year = p.DateID.Year,
                                  TypeDate = p.TypeDate,
                                  strTypeDate = p.TypeDatestr
                              };

                    List<ScheduleDetailListViewModel> ret = new List<ScheduleDetailListViewModel>();
                    foreach (var item in tmp)
                    {
                        ScheduleDetailListViewModel s = new ScheduleDetailListViewModel()
                        {
                            Day = item.Day,
                            M = item.M,
                            Year = item.Year,
                            TypeDate = item.TypeDate,
                            strTypeDate = item.strTypeDate
                        };
                        ret.Add(s);
                    }
                    return ret;
                }
                catch
                {
                    return new List<ScheduleDetailListViewModel>();
                }
            }
            public TimeSheet_tblSchedule Find(int id)
            {
                return context.TimeSheet_tblSchedule.Find(id);
            }
            public ScheduleCreateViewModel FindEdit(int id)
            {
                try
                {
                    ScheduleCreateViewModel ret = context.TimeSheet_tblSchedule
                                            .Where(p => p.ScheduleID == id)
                                            .Select(p => new ScheduleCreateViewModel()
                                            {
                                                ScheduleID = p.ScheduleID,
                                                ScheduleCode = p.ScheduleCode,
                                                ScheduleName = p.ScheduleName,
                                                Used = p.Used,
                                                Note = p.Note
                                            }).FirstOrDefault();
                    return ret;
                }
                catch
                {
                    return new ScheduleCreateViewModel();
                }

            }
            public bool Delete(int id)
            {
                try
                {
                    TimeSheet_tblSchedule modelUpdate = Find(id);
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
        #endregion
        #region Detail
            public bool AddDetail(List<TimeSheet_tblScheduleDetail> List, out string outMesssage)
            {
                try
                {
                    var first = List.First();
                    if (first != null)
                    {
                        int year = first.DateID.Year;
                        int iScheduleID = first.ScheduleID;

                        var ListFromDatabase = context.TimeSheet_tblScheduleDetail.Where(p => p.DateID.Year == year && p.ScheduleID == iScheduleID).ToList();
                        foreach (var deleteitem in ListFromDatabase)
                        {
                            context.Entry(deleteitem).State = System.Data.Entity.EntityState.Deleted;
                        }
                        foreach (var additem in List)
                        {
                            //if(additem.LSShiftID != 0)
                                context.Entry(additem).State = System.Data.Entity.EntityState.Added;
                        }
                        context.SaveChanges();
                        outMesssage = "";
                        return true;
                    }
                }
                catch (Exception e)
                {
                    outMesssage = e.Message;
                    return false;
                }
                outMesssage = "";
                return true;
            }
        #endregion
        #region DropdownList
            private List<AutoCompleteViewModel> GetDropdownList(string searchTerm)
            {
                var tmp = context.TimeSheet_tblSchedule
                               .Where(c => c.ScheduleName.Contains(searchTerm))
                               .Select(c => new
                               {
                                   id = c.ScheduleID,
                                   name = c.ScheduleName,
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
