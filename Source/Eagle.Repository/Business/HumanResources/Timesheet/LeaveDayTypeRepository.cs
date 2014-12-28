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
    public class LeaveDayTypeRepository
    {
        public EntityDataContext context { get; set; }
        public LeaveDayTypeRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(string strValidate)
        {
            var code = context.Timesheet_tbLeaveDayType.FirstOrDefault(p => p.LSLeaveDayTypeCode.Equals(strValidate));
            return (code != null);
        }
        /// <summary>
        /// Kiem tra duplicate code  khi edit cho truong hop cho chinh sua Code
        /// </summary>
        /// <param name="strValidate"></param>
        /// <returns></returns>
        public bool CheckExistEdit(string strValidate, int LSLeaveDayTypeID)
        {
            var code = context.Timesheet_tbLeaveDayType
                        .FirstOrDefault(p => p.LSLeaveDayTypeCode.Equals(strValidate) && p.LSLeaveDayTypeID != LSLeaveDayTypeID);
            return (code != null);
        }
        public List<LeaveDayTypeViewModel> List()
        {

            try
            {
                return (from p in context.Timesheet_tbLeaveDayType
                          orderby p.Name
                          select new LeaveDayTypeViewModel()
                          {
                              LSLeaveTypeID = p.LSLeaveTypeID,
                              LSLeaveDayTypeID = p.LSLeaveDayTypeID,
                              LSLeaveDayTypeCode = p.LSLeaveDayTypeCode,
                              Name = p.Name,
                              ExceptedHolidays  = p.ExceptedHolidays,
                              DaysPerPeriod = p.DaysPerPeriod,
                              DaysPerYear = p.DaysPerYear,
                              ConvalescenceLeave = p.ConvalescenceLeave,
                              strExceptedHolidays = p.ExceptedHolidays == true ? "x" :"",
                              PercentSI  = p.PercentSI,
                              strTypeName = p.LS_tblLeaveType.Name,
                              Note = p.Note
                          }).ToList();
              
            }
            catch
            {
                return new List<LeaveDayTypeViewModel>();
            }
        }
        public Timesheet_tbLeaveDayType Find(int id)
        {
            return context.Timesheet_tbLeaveDayType.Find(id);
        }
        public string Add(Timesheet_tbLeaveDayType model)
        {
            try
            {
                if (CheckExist(model.LSLeaveDayTypeCode))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return "success";
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return "error";
            }
        }

        public string Update(LeaveDayTypeCreateViewModel model)
        {

            try
            {
                if (CheckExistEdit(model.LSLeaveDayTypeCode, model.LSLeaveDayTypeID))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                Timesheet_tbLeaveDayType modelUpdate = Find(model.LSLeaveDayTypeID);
                modelUpdate.LSLeaveDayTypeCode = model.LSLeaveDayTypeCode;
                modelUpdate.LSLeaveTypeID = model.LSLeaveTypeID;
                modelUpdate.Name = model.Name;
                modelUpdate.DaysPerYear = model.DaysPerYear;
                modelUpdate.DaysPerPeriod = model.DaysPerPeriod;
                modelUpdate.ExceptedHolidays = model.ExceptedHolidays;
                modelUpdate.ConvalescenceLeave = model.ConvalescenceLeave;
                modelUpdate.PercentSI = model.PercentSI;
                modelUpdate.Note = model.Note;
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return strErr;
            }
        }

        public LeaveDayTypeCreateViewModel FindEdit(int id)
        {
            try
            {
                LeaveDayTypeCreateViewModel ret = context.Timesheet_tbLeaveDayType
                                        .Where(p => p.LSLeaveDayTypeID == id)
                                        .Select(p => new LeaveDayTypeCreateViewModel()
                                        {
                                            LSLeaveTypeID = p.LSLeaveTypeID,
                                            LSLeaveDayTypeID = p.LSLeaveDayTypeID,
                                            LSLeaveDayTypeCode = p.LSLeaveDayTypeCode,
                                            DaysPerPeriod = p.DaysPerPeriod,
                                            DaysPerYear = p.DaysPerYear,
                                            ConvalescenceLeave = p.ConvalescenceLeave,
                                            PercentSI = p.PercentSI,
                                            ExceptedHolidays = p.ExceptedHolidays,
                                            strLeaveTypeName = p.LS_tblLeaveType.Name,
                                            Name = p.Name,
                                            Note = p.Note
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new LeaveDayTypeCreateViewModel();
            }

        }
        public bool Delete(int id)
        {
            try
            {
                Timesheet_tbLeaveDayType modelUpdate = Find(id);
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
            var tmp = context.Timesheet_tbLeaveDayType
                           .Where(c => c.Name.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.LSLeaveDayTypeID,
                               name = c.Name,
                               text = c.Name
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


        private List<AutoCompleteViewModel> GetDropdownList2(string searchTerm)
        {
            var tmp = context.Timesheet_tbLeaveDayType
                           .Where(c => c.Name.Contains(searchTerm) && c.ForSI == true)
                           .Select(c => new
                           {
                               id = c.LSLeaveDayTypeID,
                               name = c.Name,
                               text = c.Name
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

        public List<AutoCompleteViewModel> ListDropdown2(string searchTerm, int pageSize, int pageNume)
        {
            return GetDropdownList2(searchTerm).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
        }

        #endregion
    }
}
