using Eagle.Repository.SYS.Session;
using Eagle.Repository.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.HR;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class ProfileController : BaseController
    {
        //
        // GET: /Admin/Profile/

        [SessionExpiration]
        public ActionResult Index()
        {           
            if (Request.IsAjaxRequest())
            {
                return PartialView("../HR/Profile/Index");
            }
            else
            {
                return View("../HR/Profile/Index");
            }           
        }

        public ActionResult List()
        {
            return null;
        }

        public ActionResult GetEmpSummary()
        {   
            return PartialView("../HR/Profile/_EmpSummary", (EmployeeViewModel)Session[SettingKeys.EmpInfo]);          
        }


        public ActionResult GetEmpDetails()
        {
            EmployeeViewModel model = new EmployeeViewModel();           
            if(Session[SettingKeys.EmpInfo] !=null)
                model = (EmployeeViewModel)Session[SettingKeys.EmpInfo];           
            return PartialView("../HR/Profile/_EmpDetails", model); 
        }

        [HttpGet]
        [AjaxSessionActionFilter]
        public ActionResult Details()
        {
            EmployeeViewModel model = new EmployeeViewModel();
            if (Session[SettingKeys.EmpInfo] != null)
                model = (EmployeeViewModel)Session[SettingKeys.EmpInfo];          
            return PartialView("../HR/Profile/_EmpDetails", model); 
        }
    }
}
