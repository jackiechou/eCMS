using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model.SYS;
using Eagle.Model;
using System.Collections;
using Eagle.Repository.Mail;
using Eagle.Repository.HR;
using Eagle.Model.HR;

namespace Eagle.WebApp.Controllers
{
    public class HomeController : BasicController
    {
        public ActionResult Index()
        {
            //PopulateMonthsToDropDownList(DateTime.Today.Month.ToString());
            //return View("../Home/Index");
            Response.RedirectToRoute("Admin_Login");
            return null;
        }

        //public ActionResult GetEmpListByBirthday(int? Month, int? Type)
        //{
        //    List<EmployeeViewModel> lst = EmployeeRepository.GetEmpListByBirthday(Month, Type, LanguageId);
        //    return PartialView("../Home/_BirthdayList", lst);
        //}

        //#region Month---------------------------------------------------------------------------------------
        //public void PopulateMonthsToDropDownList(string selected_value)
        //{
        //    SelectList lst = LoadMonthList(selected_value);
        //    ViewBag.Month = lst;
        //}
        //public ActionResult _TopMenuPartial()
        //{
        //    if (Session[SettingKeys.MenuList] != null)
        //    {
        //        List<SYS_tblFunctionPermissionViewModel> allPage = (List<SYS_tblFunctionPermissionViewModel>)Session[SettingKeys.MenuList];
        //        var RootPage = allPage.Where(p => p.Parent == null).ToList();
        //        return PartialView("../Shared/_NavigationPartial", RootPage);
        //    }
        //    else
        //    {
        //        return PartialView("../Shared/_NavigationPartial", new List<SYS_tblFunctionPermissionViewModel>());
        //    }
        //}

       
        //#endregion -------------------------------------------------------------------------------------------------------------------

    }
}
