using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Eagle.Core;
using Eagle.Repository;
using Eagle.Model;

using System.Web.Routing;
using Eagle.Common.Settings;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class HolidaysController : BaseController
    {
        //
        // GET: /Admin/Holidays/

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Business/HumanResources/Timesheet/Holidays/_Index");
            }
            else
            {
                return View("../Business/HumanResources/Timesheet/Holidays/Index");
            }
        }
        public ActionResult _Search(int id)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            HolidaysRepository _repository = new HolidaysRepository(db);
            List<HolidayViewModel> list = _repository.List(id).ToList();
            ViewBag.iYear = id;
            return PartialView("../Business/HumanResources/Timesheet/Holidays/_Create", list);
        }
        public ActionResult _Create()
        {
            int iYear = System.DateTime.Now.Year ;
            List<HolidayViewModel> list = CreateList(iYear);
            return PartialView("../Business/HumanResources/Timesheet/Holidays/_Create", list);
        }
        public ActionResult _Reset(int id)
        {
            List<HolidayViewModel> list = CreateList(id);
            return PartialView("../Business/HumanResources/Timesheet/Holidays/_Create", list);
        }
        public ActionResult Save(List<HolidayViewModel> model)
        {

            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                HolidaysRepository _repository = new HolidaysRepository(db);
                if (ModelState.IsValid)
                {
                    List<Timesheet_tblHolidays> ret = new List<Timesheet_tblHolidays>();

                    foreach (var item in model)
                    {
                        // H: holidays, S: Saturday, Sunday
                        //if (item.strTypeDate == "H" || item.strTypeDate == "S")
                        //{
                            string strDate = "";
                            if (LanguageId == 124)
                            {
                                strDate = item.M.ToString() + "/" + item.Day.ToString() + "/" + item.Year.ToString();
                            }
                            else
                            {
                                strDate = item.Day.ToString() + "/" + item.M.ToString() + "/" + item.Year.ToString();
                            }
                            DateTime dDate = Convert.ToDateTime(strDate);
                            Timesheet_tblHolidays add = new Timesheet_tblHolidays()
                            {
                                DateID = dDate,
                                TypeDate = item.TypeDate,
                                TypeDatestr = item.strTypeDate
                            };

                            ret.Add(add);
                    //    }
                    }
                    
                    // add du lieu vao database
                    string bResult = _repository.Add(ret);
                    if (bResult == "success")
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _CreateError(model, bResult);
                    }

                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _CreateError(model, errorMessage);
        }
        private List<HolidayViewModel> CreateList(int year)
        {
            List<HolidayViewModel> ret = new List<HolidayViewModel>();
            for (int m = 1; m <= 12; m++)
            {
                for (int d = 1; d <= 31; d++)
                {
                    #region set type = 0 => ngay ko hop le
                    if ((m == 2))
                    {
                        if (d == 31 || d == 30)
                        {
                            HolidayViewModel day = new HolidayViewModel() { Day = d, M = m, TypeDate = 0, strTypeDate = "I", Year=year };
                            ret.Add(day);
                        }
                        else if (System.DateTime.DaysInMonth(year,2) == 28 && d == 29)
                        {
                            HolidayViewModel day = new HolidayViewModel() { Day = d, M = m, TypeDate = 0, strTypeDate = "I", Year = year };
                            ret.Add(day);
                        }
                        else
                        {
                            DateTime dt = new DateTime(year, m, d);
                            if (dt.DayOfWeek == DayOfWeek.Saturday)
                            {
                                HolidayViewModel day = new HolidayViewModel() { Day = d, M = m, TypeDate = 3, strTypeDate = "S", Year = year };
                                ret.Add(day);
                            }
                            else if (dt.DayOfWeek == DayOfWeek.Sunday)
                            {
                                HolidayViewModel day = new HolidayViewModel() { Day = d, M = m, TypeDate = 4, strTypeDate = "S", Year = year };
                                ret.Add(day);
                            }
                            else
                            {
                                HolidayViewModel day = new HolidayViewModel() { Day = d, M = m, TypeDate = 1, strTypeDate = "", Year = year };
                                ret.Add(day);
                            }
                        }
                        
                    }
                    else if (((m == 4) || (m == 6) || (m == 9) || (m == 11)) && d == 31)
                    {
                        HolidayViewModel day = new HolidayViewModel() { Day = d, M = m, TypeDate = 0, strTypeDate = "I", Year = year };
                        ret.Add(day);
                    }
                    #endregion
                    else 
                    {
                        // weekend
                        DateTime date = new DateTime(year, m, d);
                        //holidays
                        if (date.CompareTo(new DateTime(year, 04, 30)) == 0 || date.CompareTo(new DateTime(year, 05, 01)) == 0 || date.CompareTo(new DateTime(year, 01, 01)) == 0 || date.CompareTo(new DateTime(year, 09, 02)) == 0)
                        {
                            HolidayViewModel day = new HolidayViewModel() { Day = d, M = m, TypeDate = 2, strTypeDate = "H", Year = year };
                            ret.Add(day);
                        }
                        else
                        if (date.DayOfWeek == DayOfWeek.Saturday )
                        {
                            HolidayViewModel day = new HolidayViewModel() { Day = d, M = m, TypeDate = 3, strTypeDate = "S", Year = year };
                            ret.Add(day);
                        }else
                        if (date.DayOfWeek == DayOfWeek.Sunday)
                        {
                            HolidayViewModel day = new HolidayViewModel() { Day = d, M = m, TypeDate = 4, strTypeDate = "S", Year = year };
                            ret.Add(day);      
                        }
                        
                        else
                        {
                            HolidayViewModel day = new HolidayViewModel() { Day = d, M = m, TypeDate = 1, strTypeDate = "", Year = year };
                            ret.Add(day);
                        }
                    }

                }
            }
            return ret;
        }

        public ActionResult _List()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            //LeaveDayTypeRepository _repository = new LeaveDayTypeRepository(db);
            //IList<LeaveDayTypeViewModel> sources = _repository.List().ToList();
            return PartialView("../Business/HumanResources/Timesheet/Holidays/_List", null);
        }
        public ActionResult _CreateError(List<HolidayViewModel> model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new List<HolidayViewModel>();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Business/HumanResources/Timesheet/Holidays/_Create", model);

        }
    }
}
