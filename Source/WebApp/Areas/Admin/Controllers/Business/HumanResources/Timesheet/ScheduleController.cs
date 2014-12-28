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
using Eagle.Common.Utilities;
using Eagle.Common.Helpers;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class ScheduleController : BaseController
    {
        //
        // GET: /Admin/LeaveDayType/

        #region Master
            public ActionResult Index()
            {

                if (Request.IsAjaxRequest())
                {
                    return PartialView("../Business/HumanResources/Timesheet/Schedule/_Index");
                }
                else
                {
                    return View("../Business/HumanResources/Timesheet/Schedule/Index");
                }
            }
            public ActionResult _Create()
            {
                ScheduleCreateViewModel model = new ScheduleCreateViewModel();

                return PartialView("../Business/HumanResources/Timesheet/Schedule/_Create", model);
            }
            public ActionResult _List()
            {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                if (acc == null)
                {
                    Response.Redirect("/");
                    return null;
                }

                ScheduleRepository _repository = new ScheduleRepository(db);
                IList<ScheduleViewModel> sources = _repository.List().ToList();
                return PartialView("../Business/HumanResources/Timesheet/Schedule/_List", sources);
            }
            public ActionResult _Edit(int id)
            {
                ScheduleRepository _repository = new ScheduleRepository(db);
                ScheduleCreateViewModel model = _repository.FindEdit(id);
                return PartialView("../Business/HumanResources/Timesheet/Schedule/_Create", model);
            }
            [HttpPost]
            public ActionResult _Delete(int id)
            {
                ScheduleRepository _repository = new ScheduleRepository(db);
                bool bResult = _repository.Delete(id);
                return Content("success");
            }
            public ActionResult Save(ScheduleCreateViewModel model)
            {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                string errorMessage = "";
                if (acc != null)
                {
                    ScheduleRepository _repository = new ScheduleRepository(db);
                    if (ModelState.IsValid)
                    {
                        TimeSheet_tblSchedule add = new TimeSheet_tblSchedule()
                        {
                            ScheduleCode = model.ScheduleCode,
                            ScheduleName = model.ScheduleName,
                            Used = model.Used,
                            Note = model.Note
                        };
                        // add du lieu vao database
                        bool bResult = _repository.Add(add, out errorMessage);
                        if (bResult)
                        {
                            return Content("success");
                        }
                        else
                        {
                            return _CreateError(model, errorMessage);
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
            [HttpPost]
            public ActionResult Update(ScheduleCreateViewModel model)
            {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                string errorMessage = "";
                if (acc != null)
                {
                    ScheduleRepository _repository = new ScheduleRepository(db);
                    if (ModelState.IsValid)
                    {
                        bool bResult = _repository.Update(model, out errorMessage);
                        if (bResult)
                        {
                            return Content("success");
                        }
                        else
                        {
                            return _CreateError(model, errorMessage);
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
            /// <summary>
            /// Dung hien thi thong bao loi tren man hinh 
            /// </summary>
            /// <param name="model"></param>
            /// <param name="ErrorMessage"></param>
            /// <returns></returns>
            public ActionResult _CreateError(ScheduleCreateViewModel model, string ErrorMessage)
            {
                if (model == null)
                {
                    model = new ScheduleCreateViewModel();
                }

                ViewBag.DisplayErrorMessage = true;
                ViewBag.CssClass = "alert alert-warning";
                ViewBag.SortMessage = "Error";
                ViewBag.Message = ErrorMessage;

                return PartialView("../Business/HumanResources/Timesheet/Schedule/_Create", model);

            }
        #endregion

        #region Detail
            [HttpPost]
            public ActionResult SaveDetail(int ScheduleID, List<ScheduleDetailCreateViewModel> modelSave)
            {

                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                string errorMessage = "";
                if (acc != null)
                {
                    ScheduleRepository _repository = new ScheduleRepository(db);
                    if (ModelState.IsValid)
                    {
                        List<TimeSheet_tblScheduleDetail> ret = new List<TimeSheet_tblScheduleDetail>();

                        foreach (var item in modelSave)
                        {
                            DateTime dDate = new DateTime(Convert.ToInt32(item.Year), Convert.ToInt32(item.M), Convert.ToInt32(item.Day));
                            TimeSheet_tblScheduleDetail add = new TimeSheet_tblScheduleDetail()
                            {
                                DateID = dDate,
                                LSShiftID = item.LSShiftID,
                                ScheduleID = ScheduleID,
                                Year = Convert.ToInt32(item.Year)
                            };

                            ret.Add(add);
                        }

                        // add du lieu vao database

                        bool bResult = _repository.AddDetail(ret, out errorMessage);
                        if (bResult )
                        {
                            errorMessage = "";
                            return Content("success");
                        }
                        else
                        {
                            //return _CreateErrorDetail(model, errorMessage);
                            return Content("error");
                        }

                    }
                    return Content("error");
                    //var errors = ModelState.Values.SelectMany(v => v.Errors);
                    //foreach (var modelError in errors)
                    //{
                    //    errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
                    //}
                    //return _CreateErrorDetail(model, errorMessage);
                }
                errorMessage = "";
                return Content(errorMessage);
            }
            public ActionResult _CreateErrorDetail(ScheduleDetailCreateViewModel model, string ErrorMessage)
            {
                if (model == null)
                {
                    model = new ScheduleDetailCreateViewModel();
                }

                ViewBag.DisplayErrorMessage = true;
                ViewBag.CssClass = "alert alert-warning";
                ViewBag.SortMessage = "Error";
                ViewBag.Message = ErrorMessage;

                return PartialView("../Business/HumanResources/Timesheet/Schedule/ReloadInput", model);

            }
            // tra ve partial view popup
            public ActionResult _PopupDetail()
            {
                //ViewBag.iYear = DateTime.Now.Year;
                ScheduleFindViewModel model = new ScheduleFindViewModel();
                return PartialView("../Business/HumanResources/Timesheet/Schedule/_popupScheduleDetail", model);
            }
            public ActionResult _FillDataError(ScheduleFindViewModel model, int optInclude, List<ScheduleDetailListViewModel> List)
            {

                string errorMessage = string.Empty;
                // so sanh
                if (ModelState.IsValid)
                {
                    string[] arrayFromDate = model.FromDate.Split('/');
                    string[] arrayToDate = model.ToDate.Split('/');
                    if (arrayFromDate[2] != arrayToDate[2])
                    {
                        errorMessage = Eagle.Resource.LanguageResource.ErrorCompareYearEquals;
                        ViewBag.DisplayErrorMessage = true;
                        ViewBag.CssClass = "alert alert-warning";
                        ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
                        ViewBag.Message = errorMessage;
                        ViewBag.PasswordVis = true;

                        return PartialView("../Business/HumanResources/Timesheet/Schedule/ReloadInput", model);

                    }
                    return Content("success");
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var modelError in errors)
                {
                    errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
                }
                ViewBag.DisplayErrorMessage = true;
                ViewBag.CssClass = "alert alert-warning";
                ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
                ViewBag.Message = errorMessage;
                ViewBag.PasswordVis = true;

                return PartialView("../Business/HumanResources/Timesheet/Schedule/ReloadInput", model);
            }
            public ActionResult _FillData(ScheduleFindViewModel model, int optInclude, List<ScheduleDetailListViewModel> List)
            {
                // so sanh
                if (ModelState.IsValid)
                {

                    if (List == null)
                    {
                        ScheduleRepository _Repository = new ScheduleRepository(db);
                        List = _Repository.ListFromHolidays(int.Parse(model.Year.ToString()));
                    }

                    string[] arrayFromDate = model.FromDate.Split('/');
                    string[] arrayToDate = model.ToDate.Split('/');
                    if (arrayFromDate[2] != arrayToDate[2])
                        return Content("error");

                    if (model.LSShiftIDtmp != null)
                    {
                        try
                        {
                            model.LSShiftID = Convert.ToInt32(model.LSShiftIDtmp);
                        }
                        catch
                        {
                        }
                    }
                    //var dFromDate = new DateTime(int.Parse(arrayFromDate[2]), int.Parse(arrayFromDate[1]), int.Parse(arrayFromDate[0]));
                    //var dToDate = new DateTime(int.Parse(arrayToDate[2]), int.Parse(arrayToDate[1]), int.Parse(arrayToDate[0]));
                    DateTime? dFromDate;
                    DateTime? dToDate;
                    DateTimeUtils.TryConvertToDate(model.FromDate, out dFromDate);
                    DateTimeUtils.TryConvertToDate(model.ToDate, out dToDate);
                    foreach (var item in List)
                    {
                        DateTime date = new DateTime(int.Parse(arrayFromDate[2]), item.M, item.Day);
                        if (date.CompareTo(dFromDate) >= 0 && date.CompareTo(dToDate) <= 0)
                        {
                            if (optInclude == 1 && (item.strTypeDate == "H" || item.strTypeDate == "S"))
                            {
                                item.LSShiftID = 0;
                            }
                            else if (optInclude == 0 && (item.strTypeDate == "H" || item.strTypeDate == "S"))
                            {
                                item.LSShiftID = model.LSShiftID; ;
                            }
                            else
                            {
                                item.LSShiftID = model.LSShiftID;
                            }
                        }
                    }
                    return PartialView("../Business/HumanResources/Timesheet/Schedule/_TablePartial", List);
                }
                else {
                    return Content("error");
                }
            
            }
            // reset
            public ActionResult _Reset(ScheduleDetailViewModel model)
            {
                ScheduleRepository _Repository = new ScheduleRepository(db);
                int iYear = 0;
                if (model.Year != null)
                {
                    iYear = Convert.ToInt32(model.Year);
                }
                List<ScheduleDetailListViewModel> listNew = _Repository.ListFromHolidays(iYear);
                return PartialView("../Business/HumanResources/Timesheet/Schedule/_TablePartial", listNew);
            }
            // page load
            public ActionResult _TablePartial(int? Year, int  ScheduleID)
            {
                if (Request.IsAjaxRequest())
                {
                    ScheduleRepository _Repository = new ScheduleRepository(db);


                    int iYear = DateTime.Now.Year;
                    if (Year != null)
                        iYear = Convert.ToInt32(Year);
                    List<ScheduleDetailListViewModel> list = _Repository.ListScheduleDetail(iYear, ScheduleID);

                    List<ScheduleDetailListViewModel> listNew = _Repository.ListFromHolidays(iYear);

                    if (list != null && list.Count > 0)
                        return PartialView("../Business/HumanResources/Timesheet/Schedule/_TablePartial", list);
                    else
                        return PartialView("../Business/HumanResources/Timesheet/Schedule/_TablePartial", listNew);

                }
                else
                {
                    return Content("");
                }
            }
            // page search
            public ActionResult _Search(int ScheduleID, int Year)
            {
                if (Request.IsAjaxRequest())
                {
                    ScheduleRepository _Repository = new ScheduleRepository(db);
                    int iYear = DateTime.Now.Year;
                    iYear = Convert.ToInt32(Year);
                    List<ScheduleDetailListViewModel> list = _Repository.ListScheduleDetail(iYear, ScheduleID);

                    List<ScheduleDetailListViewModel> listNew = _Repository.ListFromHolidays(iYear);

                    if (list != null && list.Count > 0)
                        return PartialView("../Business/HumanResources/Timesheet/Schedule/_TablePartial", list);
                    else
                        return PartialView("../Business/HumanResources/Timesheet/Schedule/_TablePartial", listNew);

                }
                else
                {
                    return Content("");
                }
            }
            private List<ScheduleDetailViewModel> CreateList(DateTime? FromDate, DateTime? ToDate, int iLSShiftID, int year, int optInclude)
        {
            List<ScheduleDetailViewModel> ret = new List<ScheduleDetailViewModel>();
            var fromdate = (DateTime)FromDate;
            var todate = (DateTime)ToDate;
            int mfrom = fromdate.Month;
            int mTo = todate.Month;
            int dfrom = fromdate.Day;
            int dTo = todate.Day;
            #region add new
            for (int m = mfrom; m <= mTo; m++)
            {
                for (int d = dfrom; d <= 31; d++)
                {
                    try
                    {
                        DateTime dtIndex = new DateTime(year, m, d);
                       
                        if ((dtIndex.Month < todate.Month) || (dtIndex.Month == todate.Month && dtIndex.Day <= todate.Day))
                        {
                            #region set type = 0 => ngay ko hop le
                            if ((m == 2))
                            {
                                if (d == 31 || d == 30)
                                {
                                    ScheduleDetailViewModel day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 0, strTypeDate = "I", Year = year, LSShiftID = iLSShiftID };
                                    ret.Add(day);
                                }
                                else if (System.DateTime.DaysInMonth(year, 2) == 28 && d == 29)
                                {
                                    ScheduleDetailViewModel day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 0, strTypeDate = "I", Year = year, LSShiftID = iLSShiftID };
                                    ret.Add(day);
                                }
                                else
                                {
                                    DateTime dt = new DateTime(year, m, d);
                                    if (dt.DayOfWeek == DayOfWeek.Saturday)
                                    {
                                        ScheduleDetailViewModel day;
                                        if (optInclude == 1)
                                            day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 3, strTypeDate = "S", Year = year, LSShiftID = 0 };
                                        else
                                            day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 3, strTypeDate = "S", Year = year, LSShiftID = iLSShiftID };
                                        ret.Add(day);
                                    }
                                    else if (dt.DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        ScheduleDetailViewModel day;
                                        if (optInclude == 1)
                                            day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 4, strTypeDate = "S", Year = year, LSShiftID = 0 };
                                        else
                                            day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 4, strTypeDate = "S", Year = year, LSShiftID = iLSShiftID };
                                        ret.Add(day);
                                    }
                                    else
                                    {
                                        ScheduleDetailViewModel day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 1, strTypeDate = "", Year = year, LSShiftID = iLSShiftID };
                                        ret.Add(day);
                                    }
                                }

                            }
                            else if (((m == 4) || (m == 6) || (m == 9) || (m == 11)) && d == 31)
                            {
                                ScheduleDetailViewModel day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 0, strTypeDate = "I", Year = year, LSShiftID = iLSShiftID };
                                ret.Add(day);
                            }
                            #endregion
                            #region set ngay hop le
                            else
                            {
                                // weekend
                                DateTime date = new DateTime(year, m, d);
                                //holidays 30/4, 2/9, 1/1 , 1/5 ,
                                if (date.CompareTo(new DateTime(year, 04, 30)) == 0 || date.CompareTo(new DateTime(year, 05, 01)) == 0 || date.CompareTo(new DateTime(year, 01, 01)) == 0 || date.CompareTo(new DateTime(year, 09, 02)) == 0)
                                {
                                    ScheduleDetailViewModel day = new ScheduleDetailViewModel() {
                                        Day = d,
                                        TypeDate = 2,
                                        strTypeDate = "H",
                                        Year = year,
                                        M = m,
                                        LSShiftID = (optInclude == 1 ? 0 : iLSShiftID)
                                    };
                                    //if (optInclude == 1)
                                    //    day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 2, strTypeDate = "H", Year = year, LSShiftID = 0 };
                                    //else
                                    //    day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 2, strTypeDate = "H", Year = year, LSShiftID = iLSShiftID };
                                    ret.Add(day);

                                }
                                else
                                    if (date.DayOfWeek == DayOfWeek.Saturday)
                                    {
                                        ScheduleDetailViewModel day;
                                        if (optInclude == 1)
                                            day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 3, strTypeDate = "S", Year = year, LSShiftID = 0 };
                                        else
                                            day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 3, strTypeDate = "S", Year = year, LSShiftID = iLSShiftID };
                                        ret.Add(day);

                                    }
                                    else
                                        if (date.DayOfWeek == DayOfWeek.Sunday)
                                        {
                                            ScheduleDetailViewModel day;
                                            if (optInclude == 1)
                                                day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 4, strTypeDate = "S", Year = year, LSShiftID = 0 };
                                            else
                                                day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 4, strTypeDate = "S", Year = year, LSShiftID = iLSShiftID };
                                            ret.Add(day);
                                        }

                                        else
                                        {
                                            ScheduleDetailViewModel day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 1, strTypeDate = "", Year = year, LSShiftID = iLSShiftID };
                                            ret.Add(day);
                                        }
                            }
                            #endregion
                        }
                    }
                    catch
                    {
                        ScheduleDetailViewModel day = new ScheduleDetailViewModel() { Day = d, M = m, TypeDate = 0, strTypeDate = "I", Year = year, LSShiftID = 0 };
                        ret.Add(day);
                    }
                }
            }
            #endregion
            return ret;
        }
        #endregion

        #region  Dropdowmlist
        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
            {
                ScheduleRepository _repository = new ScheduleRepository(db);
                List<AutoCompleteViewModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum).ToList();
                int iTotal = sources.Count;

                //Translate the list into a format the select2 dropdown expects
                Select2PagedResultViewModel pagedList = ConvertListToSelect2Format(sources, iTotal);

                //Return the data as a jsonp result
                return new JsonpResult
                {
                    Data = pagedList,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        #endregion

            

    }
}
