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
    public class HolidaysRepository
    {
        public EntityDataContext context { get; set; }
        public HolidaysRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public string Add(List<Timesheet_tblHolidays> List)
        {
            try
            {
                var first = List.First();
                if (first != null)
                {
                    int year = first.DateID.Year;

                    var ListFromDatabase = context.Timesheet_tblHolidays.Where(p => p.DateID.Year == year).ToList();
                    foreach (var deleteitem in ListFromDatabase)
                    {
                        context.Entry(deleteitem).State = System.Data.Entity.EntityState.Deleted;
                    }
                    foreach (var additem in List)
                    {
                        context.Entry(additem).State = System.Data.Entity.EntityState.Added;
                    }
                    context.SaveChanges();
                    return "success";
                }
                return "noupdate";
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

        public List<HolidayViewModel> List(int id)
        {

            try
            {
                var tmp = from p in context.Timesheet_tblHolidays
                            .Where( p => p.DateID.Year == id)

                          select new HolidayViewModel()
                          {
                             Day = p.DateID.Day,
                             M = p.DateID.Month,
                             Year = p.DateID.Year,
                             TypeDate = p.TypeDate,
                             strTypeDate = p.TypeDatestr
                          };

                List<HolidayViewModel> ret = new List<HolidayViewModel>();
                foreach (var item in tmp)
                {
                    HolidayViewModel s = new HolidayViewModel()
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
                return new List<HolidayViewModel>();
            }
        }
    }
}
