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
    public class ShiftRepository
    {
        public EntityDataContext context { get; set; }
        public ShiftRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(string strValidate)
        {
            var code = context.Timesheet_tblShift.FirstOrDefault(p => p.LSShiftCode.Equals(strValidate));
            return (code != null);
        }
        public Timesheet_tblShift Find(int id)
        {
            return context.Timesheet_tblShift.Find(id);
        }
        public IEnumerable<ShiftViewModel> List()
        {

            try
            {
                 var tmp = from p in context.Timesheet_tblShift
                       select new ShiftViewModel()
                       {
                           LSShiftID = p.LSShiftID,
                           LSShiftCode = p.LSShiftCode,
                           ShiftName = p.ShiftName,
                           WorkTimeBegin = p.WorkTimeBegin,
                           WorkTimeEnd = p.WorkTimeEnd,
                           BreakTimeBegin = p.BreakTimeBegin,
                           BreakTimeEnd =p.BreakTimeEnd,
                           TimeLate = p.TimeLate,
                           TimeEarly = p.TimeEarly,
                           isNextDate_OTTimeEnd = p.isNextDate_OTTimeEnd,
                           OTNightBegin = p.OTNightBegin,
                           OTNightEnd = (DateTime)p.OTNightEnd,
                           IsOTNightEnd = p.IsOTNightEnd,
                           WorkHour = p.WorkHour,
                           Note = p.Note
                       };

                 List<ShiftViewModel> ret = new List<ShiftViewModel>();
                 foreach (var item in tmp)
                 {
                     ShiftViewModel s = new ShiftViewModel()
                     {
                         LSShiftID = item.LSShiftID,
                         LSShiftCode = item.LSShiftCode,
                         ShiftName = item.ShiftName,
                         strWorkTimeBegin = item.WorkTimeBegin.ToString("H:mm") + "-" + item.WorkTimeEnd.ToString("H:mm"),
                         strBreakTimeBegin = item.BreakTimeBegin.ToString("H:mm") + "-" + item.BreakTimeEnd.ToString("H:mm"),
                         strTimeEarly = item.TimeEarly.ToString("H:mm"),
                         strTimeLate = item.TimeLate.ToString("H:mm"),
                         isNextDate_OTTimeEnd = item.isNextDate_OTTimeEnd,
                         strisNextDate_OTTimeEnd =  item.isNextDate_OTTimeEnd == true ? "x" : "",
                         IsOTNightEnd = item.IsOTNightEnd,
                         strIsOTNightEnd =  item.IsOTNightEnd ==  true ? "x" : "",
                         strOTNightBegin = item.OTNightBegin == null ? "" : ((DateTime)item.OTNightBegin).ToString("H:mm") + "-" + item.OTNightEnd.ToString("H:mm") ,
                         WorkHour = item.WorkHour,
                         Note = item.Note
                     };
                     ret.Add(s);
                 }

                 return ret;
            }
            catch
            {
                return new List<ShiftViewModel>();
            }


        }
        public bool Add(Timesheet_tblShift model)
        {
            try
            {
                if (CheckExist(model.LSShiftCode))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return false;
                }
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return true;
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
                throw;
            }
        }
        public bool Update(ShiftCreateViewModel model)
        {
            try
            {
                Timesheet_tblShift modelUpdate = Find(model.LSShiftID);
                modelUpdate.LSShiftCode = model.LSShiftCode;
                modelUpdate.ShiftName = model.ShiftName;
                modelUpdate.WorkTimeBegin = System.DateTime.Today.Add(model.WorkTimeBegin);
                modelUpdate.WorkTimeEnd = System.DateTime.Today.Add(model.WorkTimeEnd);
                modelUpdate.BreakTimeBegin = System.DateTime.Today.Add(model.BreakTimeBegin);
                modelUpdate.BreakTimeEnd = System.DateTime.Today.Add(model.BreakTimeEnd);
                modelUpdate.TimeLate = System.DateTime.Today.Add(model.TimeLate);
                modelUpdate.TimeEarly = System.DateTime.Today.Add(model.TimeEarly);
                modelUpdate.OTNightBegin = System.DateTime.Today.Add(model.OTNightBegin);
                modelUpdate.OTNightEnd = System.DateTime.Today.Add(model.OTNightEnd);
                modelUpdate.isNextDate_OTTimeEnd = model.isNextDate_OTTimeEnd;
                modelUpdate.IsOTNightEnd = model.IsOTNightEnd;
                modelUpdate.WorkHour = model.WorkHour;
                modelUpdate.Note = "";
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return false;
            }

        }
        public ShiftCreateViewModel FindEdit(int id)
        {
            try
            { 
                var model = context.Timesheet_tblShift
                       .Where(p => p.LSShiftID == id)
                       .FirstOrDefault();
                
                TimeSpan OTNightBegin = new TimeSpan(0);
                if (model.OTNightBegin != null)
	            {
                    OTNightBegin = TimeSpan.Parse(((DateTime)model.OTNightBegin).ToString("H:mm"));
	            }
                TimeSpan OTNightEnd = new TimeSpan(0);
                if (model.OTNightEnd != null)
                {
                    OTNightEnd = TimeSpan.Parse(((DateTime)model.OTNightEnd).ToString("H:mm"));
                }
                return new ShiftCreateViewModel() 
                { 
                     LSShiftID = model.LSShiftID,
                     LSShiftCode = model.LSShiftCode,
                     ShiftName = model.ShiftName,
                     WorkTimeBegin = TimeSpan.Parse(model.WorkTimeBegin.ToString("H:mm")),
                     WorkTimeEnd = TimeSpan.Parse(model.WorkTimeEnd.ToString("H:mm")),
                     BreakTimeBegin = TimeSpan.Parse(model.BreakTimeBegin.ToString("H:mm")),
                     BreakTimeEnd = TimeSpan.Parse(model.BreakTimeEnd.ToString("H:mm")),
                     TimeEarly = TimeSpan.Parse(model.TimeEarly.ToString("H:mm")),
                     TimeLate = TimeSpan.Parse(model.TimeLate.ToString("H:mm")),
                     OTNightBegin = OTNightBegin,
                     OTNightEnd = OTNightEnd,
                     isNextDate_OTTimeEnd = model.isNextDate_OTTimeEnd == true,
                     IsOTNightEnd = model.IsOTNightEnd,
                     Note = model.Note,
                     WorkHour = model.WorkHour
                };
            }
            catch
            {
                return new ShiftCreateViewModel();
            }

        }
        public bool Delete(int id)
        {
            try
            {
                Timesheet_tblShift modelUpdate = Find(id);
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
            var tmp = context.Timesheet_tblShift
                           .Where(c => c.ShiftName.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.LSShiftID,
                               name = c.ShiftName,
                               text = ""
                           });
            List<AutoCompleteViewModel> ret = new List<AutoCompleteViewModel>();
            foreach (var item in tmp)
            {
                AutoCompleteViewModel p = new AutoCompleteViewModel()
                {
                    id = item.id.ToString(),
                    name = item.id.ToString() + " = " +item.name,
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
