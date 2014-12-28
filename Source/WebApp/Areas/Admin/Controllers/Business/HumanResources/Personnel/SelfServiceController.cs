using Eagle.Repository;
using Eagle.Repository.HR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model.HR;

namespace Eagle.WebApp.Areas.Admin.Controllers.HR
{
    public class SelfServiceController : BaseController
    {
        public ActionResult Index()
        {
            //PopulateMonthsToDropDownList(DateTime.Today.Month.ToString());
            PopulateMonthsToDropDownList(null);
            if (Request.IsAjaxRequest())
                return PartialView("../Services/SelfServices/_BirthdayServices");
            else
                return View("../Services/SelfServices/Index");
        }

        ////Dich vu chia se tin mung ngay sinh nhat
        //public ActionResult GetEmpListByBirthday(int? Type)
        //{
        //    List<EmployeeViewModel> lst = EmployeeRepository.GetEmpBirthdayList(Type, LanguageId);
        //    return PartialView("../Services/SelfServices/_BirthdayServices", lst);
        //}

        #region Month-------------------------------------------------------------------------------------------------------------------------------------------------

        public void PopulateMonthsToDropDownList(string SelectedValue)
        {
            SelectList lst = CommonRepository.LoadMonthList(SelectedValue, LanguageId);
            ViewBag.Month = lst;
        }
        #endregion ----------------------------------------------------------------------------------------------------------------------------------------------------

        #region Year --------------------------------------------------------------------------------------------------------------------------------------------------

        public void PopulateYearsToDropDownList(int NumberOfYears, string SelectedValue)
        {
            SelectList lst = LoadYearList(NumberOfYears, SelectedValue);
            ViewBag.Year = lst;
        }

        public SelectList LoadYearList(int NumberOfYears, string SelectedValue)
        {
            SelectList year_lst = new SelectList(new[] { new { Text = Eagle.Resource.LanguageResource.Select, Value = (string)SelectedValue } }.Concat(
                 Enumerable.Range(DateTime.Today.Year - NumberOfYears, DateTime.Today.Year).Select(r => new
                 {
                     Text = r.ToString(),
                     Value = r.ToString()
                 })), "Value", "Text", SelectedValue);
            return year_lst;
        }
        #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------

    }
}
