using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class BaseMenuController : BaseController
    {
        protected List<SYS_tblFunctionPermissionViewModel> CreateViewBag(int ModuleId)
        {
            List<SYS_tblFunctionPermissionViewModel> allPage = (List<SYS_tblFunctionPermissionViewModel>)Session[SettingKeys.MenuList];
            ViewBag.ModuleId = ModuleId;
            if (allPage != null)
            {
                var module = allPage.SingleOrDefault(p => p.FunctionID == ModuleId);
                if (module.FunctionNameE != null)
                {
                    ViewBag.Title = module.FunctionNameE;
                }
	        }
            return allPage;
        }
    }

    public class SystemAdminController : BaseMenuController
    {
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return PartialView("../Common/Menu/Menu", CreateViewBag(1));
        }
    }

    public class InsuranceController : BaseMenuController
    {
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return PartialView("../Common/Menu/Menu", CreateViewBag(10));
        }
    }

    public class PayrollController : BaseMenuController
    {
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return PartialView("../Common/Menu/Menu", CreateViewBag(11));
        }
    }
    public class CMSController : BaseMenuController
    {
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return PartialView("../Common/Menu/Menu", CreateViewBag(22));
        }
    }
    public class PersonnelController : BaseMenuController
    {
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return PartialView("../Common/Menu/Menu", CreateViewBag(2));
        }
    }
    public class RecruitmentController : BaseMenuController
    {
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return PartialView("../Common/Menu/Menu", CreateViewBag(8));
        }
    }
    public class TerminationController : BaseMenuController
    {
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return PartialView("../Common/Menu/Menu", CreateViewBag(12));
        }
    }

    public class TimesheetController : BaseMenuController
    {
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return PartialView("../Common/Menu/Menu", CreateViewBag(9));
        }
    }
    public class TrainingController : BaseMenuController
    {
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return PartialView("../Common/Menu/Menu", CreateViewBag(3));
        }
    }
    
}
