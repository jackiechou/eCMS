using Eagle.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
//
using Eagle.Model;
using Eagle.Model.HR;
using Eagle.Repository.SYS.Session;
using Eagle.Common.Settings;
using Eagle.Model.SYS.Permission;
using System.Web.Routing;
using CommonLibrary.UI.Skins;
using Eagle.Model.UI.Skins;
using CommonLibrary.Modules.Dashboard.Components.Skins;
using Eagle.Repository.UI.Layout;
//namespace Eagle.WebApp.Areas.Admin.Controllers
//{
    // trong này có chứa vài hàm viết sãn
    // hàm count cho IEnumerable dùng để count cho lớp IEnumerable
    //public static class EnumerableExtensions
    //{
    //    public static int Count(this IEnumerable source)
    //    {

    //        if (source != null)
    //        {
    //            int res = 0;

    //            foreach (var item in source)
    //                res++;

    //            return res;
    //        }
    //        else
    //        {
    //            return 0;
    //        }
            
    //    }
    //}

    public class BaseController : Controller
    {
        // entity
        protected EntityDataContext db = new EntityDataContext();

        // override dispose 
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //public new HttpContextBase HttpContext
        //{
        //    get
        //    {
        //        HttpContextWrapper context =
        //            new HttpContextWrapper(System.Web.HttpContext.Current);
        //        return (HttpContextBase)context;
        //    }
        //}

        // LanguageId => nó nấy session trả về mã ngôn ngữ hiệu tại
        protected string domainUrl { get { return ConfigurationManager.AppSettings["DOMAIN_URL"].ToString(); } }
        public BaseController()
        {
            System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            System.Globalization.DateTimeFormatInfo dateformat = new System.Globalization.DateTimeFormatInfo();
            dateformat.ShortDatePattern = "MM/dd/yyyy";
            culture.DateTimeFormat = dateformat;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
        }
        
        protected static Guid DefaultApplicationCode = new Guid("c25a74cc-2e61-43ae-86c2-ce608c40b65a");
        protected static Guid DefaultRoleCode = new Guid("c3318a71-d9cc-4c92-a1ae-66e0eefde765");
        protected static Guid DefaultUserCode = new Guid("76119582-0be5-4400-b9ee-2a94ea27dd99");
        protected static Guid DefaultMenuTypeCode = new Guid("205c59cc-4a2a-4ea2-8843-a58a0cdd0d3e");
        protected static bool DefaultIsSuperUser = false;
        protected int DefaultApplicationId = 1;
        protected int DefaultRoleId = 1;
        private int DefaultLanguageId = 124; //LanguageId = 41; =>en_US or LanguageId=124 => vi-VN
        private string DefaultLanguageCode = "vi-VN";
        protected int DefaultPageSize = 10;
        private int DefaultScopeTypeId = 2;
        //string rd = ControllerContext.ParentActionViewContext.RouteData;
        //string currentAction = rd.GetRequiredString(“action”);
        //string currentController = rd.GetRequiredString(“controller”);

        protected string CurrentController
        {
            get
            {
                return RouteData.Values["controller"].ToString();
            }
        }
        protected string CurrentAction
        {
            get
            {
                return RouteData.Values["action"].ToString();
            }
        }
        protected string CurrentArea
        {
            get
            {
                return RouteData.DataTokens["area"].ToString();
            }
        }

        protected Guid CurrentMenuCode
        {
            get
            {

                //return (RouteData.Values["id"] != null) ? RouteData.Values["id"].ToString() : string.Empty;
                Guid MenuCode = new Guid();
                if (RouteData.Values["id"] != null)
                    MenuCode = new Guid(RouteData.Values["id"].ToString());
                return MenuCode;
            }
        }

        protected int ApplicationId
        {
            get
            {
                if (base.Session[SettingKeys.ApplicationId] != null)
                    DefaultApplicationId = (int)(base.Session[SettingKeys.ApplicationId]);
                else
                    Session[SettingKeys.ApplicationId] = DefaultApplicationId;
                return DefaultApplicationId;
            }
        }
        
        protected Guid ApplicationCode
        {
            get
            {
                if (base.Session[SettingKeys.ApplicationCode] == null)
                {
                    Session[SettingKeys.ApplicationCode] = DefaultApplicationCode;
                    return DefaultApplicationCode;
                }
                else
                {
                    DefaultApplicationCode = new Guid(base.Session[SettingKeys.ApplicationCode].ToString());
                    return DefaultApplicationCode;
                }
            }
        }

        protected Guid RoleCode
        {
            get
            {
                if (base.Session[SettingKeys.RoleCode] == null)
                {
                    Session[SettingKeys.RoleCode] = DefaultRoleCode;
                    return DefaultRoleCode;
                }
                else
                {
                    DefaultRoleCode = new Guid(base.Session[SettingKeys.RoleCode].ToString());
                    return DefaultRoleCode;
                }
            }
        }
        
        protected Guid UserCode
        {
            get
            {
                if (base.Session[SettingKeys.UserCode] == null)
                {
                    Session[SettingKeys.UserCode] = DefaultUserCode;
                    return DefaultUserCode;
                }
                else
                {
                    DefaultUserCode = new Guid(base.Session[SettingKeys.UserCode].ToString());
                    return DefaultUserCode;
                }
            }
        }

        protected Guid MenuTypeCode
        {
            get
            {
                if (base.Session[SettingKeys.MenuTypeCode] == null)
                {
                    Session[SettingKeys.MenuTypeCode] = DefaultMenuTypeCode;
                    return DefaultMenuTypeCode;
                }
                else
                {
                    DefaultMenuTypeCode = new Guid(base.Session[SettingKeys.MenuTypeCode].ToString());
                    return DefaultMenuTypeCode;
                }
            }
        }
        
        //protected int RoleId
        //{
        //    get
        //    {
        //        if (base.Session[SettingKeys.RoleId] != null)
        //            DefaultRoleId = (int)(base.Session[SettingKeys.RoleId]);
        //        else
        //            Session[SettingKeys.RoleId] = DefaultRoleId;
        //        return DefaultRoleId;
        //    }
        //}

        protected int RoleId
        {
            get
            {
                return Convert.ToInt32(base.Session[SettingKeys.RoleId]);
            }
            set
            {
                this.Session[SettingKeys.RoleId] = value;
            }
        }

        protected int ScopeTypeId
        {
            get
            {
                bool _IsSuperUser = Convert.ToBoolean(base.Session[SettingKeys.IsSuperUser]);
                if (base.Session[SettingKeys.IsSuperUser] != null)
                    DefaultScopeTypeId = (_IsSuperUser == true) ? 1 : 2;  
                else
                    Session[SettingKeys.ScopeTypeId] = DefaultScopeTypeId;
                return DefaultScopeTypeId;
            }
        }
        protected bool IsSuperUser
        {
            get
            {
                if (base.Session[SettingKeys.IsSuperUser] != null)
                    DefaultIsSuperUser = (bool)(base.Session[SettingKeys.IsSuperUser]);
                else
                    Session[SettingKeys.IsSuperUser] = DefaultIsSuperUser;
                return DefaultIsSuperUser;
            }
        }
        
        protected int LanguageId
        {
            get
            {
                if (base.Session[SettingKeys.LanguageId] == null)
                {
                    base.Session[SettingKeys.LanguageId] = DefaultLanguageId;
                    return DefaultLanguageId;
                }
                else
                {
                    DefaultLanguageId = Convert.ToInt32(base.Session[SettingKeys.LanguageId]);
                    return DefaultLanguageId;
                }
            }
        }

        protected string LanguageCode
        {
            get
            {
                if (base.Session[SettingKeys.LanguageCode] == null)
                {
                    Session[SettingKeys.LanguageCode] = DefaultLanguageCode;
                    return DefaultLanguageCode;
                }
                else
                {
                    DefaultLanguageCode = base.Session[SettingKeys.LanguageCode].ToString();
                    return DefaultLanguageCode;
                }
            }
        }
                
        protected int? CurrentEmpId
        {         
            get
            {
                int? _EmpId = null;
                if (base.Session[SettingKeys.EmpId] != null)
                    _EmpId = Convert.ToInt32(base.Session[SettingKeys.EmpId]);
                else
                {
                    if (base.Session[SettingKeys.UserId] != null)
                        _EmpId = Convert.ToInt32(base.Session[SettingKeys.UserId]);
                }
                return _EmpId;
            }
            set
            {
                this.Session[SettingKeys.EmpId] = value;
            }
        }

        protected string CurrentEmpCode
        {
            get
            {
                string _EmpCode = string.Empty;
                if (base.Session[SettingKeys.EmpCode] != null)
                    _EmpCode = base.Session[SettingKeys.EmpCode].ToString();
                return _EmpCode;
            }
            set
            {
                this.Session[SettingKeys.EmpCode] = value;
            }
        }

        protected int? UserId
        {
            get
            {
                int? _UserId = null;
                if (base.Session[SettingKeys.UserId] !=  null)
                    _UserId = Convert.ToInt32(base.Session[SettingKeys.UserId]);
                return _UserId;
            }
        }

        protected string UserName
        {
            get
            {
                string _UserName = string.Empty;
                if (base.Session[SettingKeys.UserName] != null && base.Session[SettingKeys.UserName].ToString() != string.Empty)
                    _UserName = base.Session[SettingKeys.UserName].ToString();
                return _UserName;
            }
        }

      

        #region Check Permission ==========================================================
        [SessionExpiration]
        public void CheckPagePermision()
        {  
            List<SYS_tblFunctionPermissionViewModel> lst = (List<SYS_tblFunctionPermissionViewModel>)Session[SettingKeys.MenuList];
            if (lst != null)
            {
                string url = Request.Url.AbsolutePath.ToString().Substring(1);
                var fp = lst.Where(p => p.Url.Contains(url.TrimEnd('/'))).FirstOrDefault();
                //nếu không phải là admin thì check quyền
                if (fp != null)
                {
                    if (((AccountViewModel)Session[SettingKeys.AccountInfo]).FAdm != 1)
                    {
                        //Nếu không phải là  admin => check quyền
                        PermissionKey.Delete = fp.FDelete;
                        PermissionKey.View = fp.FView;
                        PermissionKey.Create = fp.FEdit;
                        PermissionKey.Edit = fp.FEdit;
                        PermissionKey.Details = fp.FEdit;                        
                        PermissionKey.Validate = fp.FEdit;
                        PermissionKey.InValidate = fp.FEdit;
                        PermissionKey.Print = fp.FExport;                       
                        PermissionKey.Import = fp.FExport;
                        PermissionKey.Export = fp.FExport;
                        PermissionKey.Upload = fp.FExport;
                    }
                    else
                    {
                        //Nếu là admin full quyền
                        PermissionKey.View = true;
                        PermissionKey.Delete = true;
                        PermissionKey.Edit = true;
                        PermissionKey.Create = true;
                        PermissionKey.Details = true;
                        PermissionKey.Validate = true;
                        PermissionKey.InValidate = true;
                        PermissionKey.Print = true;
                        PermissionKey.Import = true;
                        PermissionKey.Export = true;
                        PermissionKey.Upload = true;
                    }

                }
                else
                {
                    //Nếu không có quyền hoặc hết session acc => return
                    Response.RedirectToRoute("Admin_PermissionDenied");
                }
            }
            else
            {
                //Hết session => nhảy ra trang ngoài
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
            }

        }
        #endregion ========================================================================

        protected int? FAdm
        {         
            get
            {
                int? _FAdm = 0;
                if (base.Session[SettingKeys.AccountInfo] != null)
                {
                    AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                    _FAdm = Convert.ToInt32(acc.FAdm);
                }
                return _FAdm;
            }
        }

        protected bool isAdmin
        {
            get
            {
                bool _isAdmin = (FAdm == 1)?true:false;
                return _isAdmin;
            }
        }     
        public AccountViewModel CurrentAcc
        {
            get
            {
                if (base.Session[SettingKeys.AccountInfo] != null)
                {
                    return ((AccountViewModel)Session[SettingKeys.AccountInfo]);
                }
                else
                {
                    return null;
                }
            }
        }

        EmployeeViewModel _CurrentEmp = new EmployeeViewModel();
        public EmployeeViewModel CurrentEmp
        {
            get
            {                
                if (base.Session[SettingKeys.EmpInfo] != null)
                    _CurrentEmp = (EmployeeViewModel)Session[SettingKeys.EmpInfo];
                return _CurrentEmp;
            }
            set
            {
                _CurrentEmp = value;
            }
        }

        public Select2PagedResultViewModel ConvertAutoListToSelect2Format(List<AutoCompleteModel> lst, int total)
        {
            Select2PagedResultViewModel jsonList = new Select2PagedResultViewModel();
            jsonList.Results = new List<Select2ResultViewModel>();

            //Loop through our attendees and translate it into a text value and an id for the select list
            foreach (AutoCompleteModel a in lst)
            {
                jsonList.Results.Add(new Select2ResultViewModel { id = a.id.ToString(), name = a.name, text = a.text });
            }
            //Set the total count of the results from the query.
            jsonList.Total = total;

            return jsonList;
        }

        public Select2PagedResultViewModel ConvertListToSelect2Format(List<AutoCompleteViewModel> lst, int total)
        {
            Select2PagedResultViewModel jsonList = new Select2PagedResultViewModel();
            jsonList.Results = new List<Select2ResultViewModel>();

            //Loop through our attendees and translate it into a text value and an id for the select list
            foreach (AutoCompleteViewModel a in lst)
            {
                jsonList.Results.Add(new Select2ResultViewModel { id = a.id.ToString(), name = a.name, text = a.text });
            }
            //Set the total count of the results from the query.
            jsonList.Total = total;

            return jsonList;
        }

        public void ShowMessageBox(string MessageType, string Message)
        {
            string CssClass = string.Empty;
            if (MessageType == "warning")
                CssClass = "alert alert-warning";
            else if (MessageType == "error")
                CssClass = "alert alert-error";
            else if (MessageType == "success")
                CssClass = "alert alert-success";
            else
                CssClass = "alert alert-warning";

            ViewBag.DisplayErrorMessage = true;
            ViewBag.PopupTitle = MessageType.ToUpper();
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = CssClass;
            ViewBag.Message = Message;
        }

        //Default Theme 
        public string DefaultThemeName = "Default";

        protected string ThemeName
        {
            get
            {
                if (base.Session[SettingKeys.ThemeName] != null)
                    DefaultThemeName = base.Session[SettingKeys.ThemeName].ToString();
                return DefaultThemeName;
            }
            set
            {
                this.Session[SettingKeys.ThemeName] = value;
            }
        }

        //Default Master - Layout
        protected int PageId
        {  
            get
            {
                int _PageId = 0;
                if (RouteData.DataTokens["pageid"]!=null)
                    _PageId = Convert.ToInt32(RouteData.DataTokens["pageid"].ToString());
                return _PageId;
            }
        }

        //public string DefaultMasterName = LayoutType.MainLayout;
        //protected string MasterName
        //{
        //    get
        //    {
        //        if (base.Session[SettingKeys.MasterName] != null)
        //            DefaultMasterName = base.Session[SettingKeys.MasterName].ToString();
        //        return DefaultMasterName;
        //    }
        //    set
        //    {                
        //        this.Session[SettingKeys.MasterName] = value;
        //    }
        //}
        

        //protected override void Execute(System.Web.Routing.RequestContext requestContext)
        //{
        //    string themeName = string.Empty, themeSrc = string.Empty;
        //    if (Session[SettingKeys.ThemeName] == null || Session[SettingKeys.ThemeName].ToString() == string.Empty)
        //    {
        //        SkinViewModel skin_obj = SkinRepository.GetSelectedTheme(ApplicationId);
        //        themeName = skin_obj.SkinPackageName;
        //        themeSrc = skin_obj.SkinPackageSrc;               
        //    }
        //    else
        //    {
        //        themeName = Session[SettingKeys.ThemeName].ToString();
        //        themeSrc = Session[SettingKeys.ThemeSrc].ToString();
        //    }

        //    if (requestContext.HttpContext.Items[themeName] == null)
        //    {
        //        //first time load
        //        requestContext.HttpContext.Items[themeName] = requestContext.HttpContext.Request.Cookies.Get("theme").Value;
        //    }
        //    else
        //    {
        //        requestContext.HttpContext.Items[themeName] = themeSrc;

        //        var previewTheme = requestContext.RouteData.GetRequiredString("theme");

        //        if (!string.IsNullOrEmpty(previewTheme))
        //        {
        //            requestContext.HttpContext.Items[themeName] = previewTheme;
        //        }
        //    }

        //    base.Execute(requestContext);
        //}
}
