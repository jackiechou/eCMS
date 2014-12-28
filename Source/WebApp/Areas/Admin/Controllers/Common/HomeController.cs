using Eagle.Core;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Repository.SYS.Session;
using Eagle.Model.Common;
using Eagle.Repository.Common;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Admin/Home/
        [SessionExpiration]
        public ActionResult Index(int id = 0)
        {
            var result = from c in db.LS_tblCompany
                         //where cd.Name == "Company"
                         where c.Parent == null
                         orderby new { c.Parent, c.Name }
                         select new CompanyViewModel()
                         {
                             LSCompanyID = c.LSCompanyID,
                             Name = LanguageId == 124 ? c.Name : c.VNName
                         };
            ViewBag.CompanyList = result.ToList();
            return View("../Common/Home/Index");
        }

        #region REMINDER =====================================================================================
        [SessionExpiration]
        public ActionResult LoadReminder()
        {
            //    List<Common_spRemind_GetAll_Result> list = new List<Common_spRemind_GetAll_Result>();
            //    if (CurrentAcc != null)
            //    {
            //        list = db.Common_spRemind_GetAll(CurrentAcc.EmpID, LanguageId).ToList();
            //    }
            //    return PartialView("../Shared/_Remind",list);
            return PartialView("../Shared/_Reminder");
        }
        #endregion ===========================================================================================

        private string getUrl()
        {
            var stop = false;
            foreach (var item in Request.Url.AbsolutePath.Split('/'))
            {
                if (stop)
                {
                    return item;
                }
                if (item.ToLower().Contains("Admin"))
                {
                    stop = true;
                }
            }
            return "";
        }

        //[ChildActionOnly]
        //[SessionExpiration]
        //public ActionResult PageMenu()
        //{
        //    string strResult = string.Empty;
        //    string signal = getUrl().ToLower();
        //    signal = signal == "home" ? "" : signal;
        //    if (Session[ConstantClass.Session_HtmlPage] == null || Boolean.Parse(Session[ConstantClass.Session_ChangeLanguage].ToString()) == true)
        //    {
        //        List<PageViewModel> allPage = (List<PageViewModel>)Session[ConstantClass.Session_Page];

        //        // Truong hop la Admin thi ko can Check
        //        if (UserName != "admin")
        //        {
        //            List<AccessFunctionPolicyViewModel> listAccessPage = (List<AccessFunctionPolicyViewModel>)Session[ConstantClass.Session_PageAccess];
        //            var PageIDs = listAccessPage.Where(p => p.Read == 1).Select(p => p.PageId).ToList();
        //            allPage = allPage.Where(o => PageIDs.Contains(o.PageId)).ToList();
        //        }
        //        ViewBag.allPage = allPage;

        //        // Tim nhung node cha truoc
        //        foreach (var item in allPage.Where(p => p.ParentId == null))
        //        {
        //            strResult += "<li class=''>";
        //            strResult += "<a href='" + item.PageUrl.ToLower() + "' class='sf-with-ul'><span class='pagename'>" + item.PageName + "</span></a>";
        //            // Truong hop ton tai con
        //            if (item.tree_isLeaf == "false")
        //            {
        //                strResult += LoopPage(item.PageId, allPage);
        //            }

        //            strResult += "</li>";
        //        }

        //        Session[ConstantClass.Session_HtmlPage] = strResult;
        //        Session[ConstantClass.Session_ChangeLanguage] = false;
        //    }
        //    else
        //    {
        //        strResult = Session[ConstantClass.Session_HtmlPage].ToString();

        //    }
        //    strResult = strResult.Replace("/" + signal + "'", "/" + signal + "' isview='1' ");
        //    return PartialView("_PageMenu", strResult);
        //}


        [ChildActionOnly]
        [SessionExpiration]
        public ActionResult Menu()
        {

            List<SYS_tblFunctionPermissionViewModel> allPage = (List<SYS_tblFunctionPermissionViewModel>)Session[SettingKeys.MenuList];

            ViewBag.allPage = allPage;
            return PartialView(allPage);
        }

        [ChildActionOnly]
        public ActionResult _Nav()
        {
            List<SYS_tblFunctionPermissionViewModel> allPage = (List<SYS_tblFunctionPermissionViewModel>)Session[SettingKeys.MenuList];
            //ViewBag.allPage = allPage;
            return PartialView(allPage);
        }
        public ActionResult _LeftBar()
        {
            List<SYS_tblFunctionPermissionViewModel> allPage = (List<SYS_tblFunctionPermissionViewModel>)Session[SettingKeys.MenuList];
            return PartialView("_LeftBar", allPage);
        }
       
        /*Layput 2*/
        public ActionResult _NavigationPartial()
        {
            if (Session[SettingKeys.MenuList] != null)
            {
                List<SYS_tblFunctionPermissionViewModel> allPage = (List<SYS_tblFunctionPermissionViewModel>)Session[SettingKeys.MenuList];
                var RootPage = allPage.Where(p => p.Parent == null).ToList();
                return PartialView("../Shared/_NavigationPartial", RootPage);
            }
            else
            {
                return PartialView("../Shared/_NavigationPartial", new List<SYS_tblFunctionPermissionViewModel>());
            }
        }
        /*Home page - Dashboard - Biểu đồ cơ cấu nhân viên */
        public class chart{
            public string label { get; set; }
            public string data { get; set; }
            public int? total { get; set; }
            public string percent { get { return data + "%"; } }
        }
        [SessionExpiration]
        public JsonResult PieChart(int? LSCompanyID)
        {
            List<chart> EmployeeList = new List<chart>();
            var result = db.Common_spGetTotalEmployee(LSCompanyID).ToList();
            if (result != null && result.Count > 0)
            {
                int? tmpTotal = result.Sum(p => p.Total);
                if (tmpTotal != null)
	            {
                    double Total = (double)tmpTotal.Value;
                    foreach (var item in result)
	                {
                        EmployeeList.Add(new chart() { label = LanguageId == 124 ? item.Name : item.VNName, data = (((double)item.Total) * 1000 / Total).ToString("#,##0.0"), total = item.Total });
	                }
	            }
            }
          
            return base.Json(EmployeeList, JsonRequestBehavior.AllowGet);
        }


        /*Home page - Dashboard - Biều đồ tăng giảm nhân viên */
        public class lineChart
        {
            public string label { get; set; }
            public List<double[]> data { get; set; }
        }
        [SessionExpiration]
        public JsonResult LineChart(int? Year,int? Type)
        {
            //Nếu type = 1: Lấy theo các tháng trong năm
            //Nếu type = 2: lấy tất cả theo từng năm
            if (Type == null)
            {
                Type = 1;
            }
            if (Year == null)
            {
                Year = DateTime.Now.Year;
            }

            List<lineChart> lineChartList = new List<lineChart>();
            var result = db.Common_spDashboard_StaffMovement(Year, Type).ToList();


            foreach (var item in result.Select(p => new { p.CompanyName, p.LSCompanyID }).Distinct())
            {
                //Tạo note cha
                lineChart c = new lineChart()
                {
                    label = item.CompanyName,
                    data = new List<double[]>()
                };
                //Tạo dữ liệu
                foreach (var data in result.Where(p => p.LSCompanyID == item.LSCompanyID))
                {
                    double[] tmp = new double[2];
                    tmp[0] = (double)data.Time;
                    tmp[1] = (double)data.TotalStaff;

                    c.data.Add(tmp);
                }
                lineChartList.Add(c);
            }
            return base.Json(lineChartList, JsonRequestBehavior.AllowGet);
        }
    }
}
