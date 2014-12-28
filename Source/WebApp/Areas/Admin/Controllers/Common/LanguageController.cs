using Eagle.Core;
using Eagle.Repository;
using Eagle.Repository.HR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.HR;
using Eagle.Repository.SYS.Session;
using Eagle.Common.Settings;
using Eagle.Repository.Sys.Languages;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class LanguageController : BaseController
    {
       [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetLanguageList()
        {
            LanguageRepository _repository = new LanguageRepository(db);
            List<LanguageViewModel> lst = _repository.GetList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

       public ActionResult PopulateListBox()
       {
           SelectList lst = LanguageRepository.PopulateActiveLanguages(null,false);
           return View("../SYS/Pages/_List", lst);
       }



       public void SwitchLanguage(int LanguageId, string CurrentLanguage, string DesiredUrl)
       {

           //CultureInfo VNCIclone = new CultureInfo("vi-VN");

           CultureInfo myCIclone = new CultureInfo(CurrentLanguage);
           myCIclone.DateTimeFormat = myCIclone.DateTimeFormat; 

           Session["CurrentLanguage"] = myCIclone;

           Session["LanguageId"] = LanguageId;
           try
           {
               AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
               List<SYS_tblFunctionPermissionViewModel> moduleList = db.sp_clsCommon("ModuleList", CurrentLanguage, "", 0, 0, 1, acc.UserName).Select(p => new SYS_tblFunctionPermissionViewModel() { Url = p.Url, FunctionID = p.FunctionID, FunctionNameE = p.FunctionNameE, Rank = p.Rank, Parent = p.Parent, Tooltips = p.Tooltips, FView = (p.FView != 0 && p.FView != null), FEdit = (p.FEdit != 0 && p.FEdit != null), FDelete = (p.FDelete != 0 && p.FDelete != null), FExport = (p.FExport != 0 && p.FExport != null) }).ToList();
               moduleList.Add(new SYS_tblFunctionPermissionViewModel() { Url = "/Admin/ChangePassword" });
               EmployeeRepository _repository = new EmployeeRepository(db);

               EmployeeViewModel Emp = (EmployeeViewModel)Session[SettingKeys.EmpInfo];
               EmployeeViewModel model = new EmployeeViewModel();
               if (Emp != null)
               {
                   model= _repository.GetEmployee(Emp.EmpID, LanguageId);
                   if (model == null)
                   {
                       model = new EmployeeViewModel() { FullName = acc.UserName };
                   }
                   Session[SettingKeys.EmpInfo] = model;
               }

               EmployeeViewModel EmpId = (EmployeeViewModel)Session[SettingKeys.EmpInfo];
               if (EmpId != null)
               {
                   if (Emp != null && Emp.EmpID == EmpId.EmpID)
                   {
                       Session[SettingKeys.EmpInfo] = model;
                   }
                   else
                   {
                        var modelEmpId = _repository.GetEmployee(EmpId.EmpID, LanguageId);
                        if (modelEmpId != null)
                        {
                            modelEmpId = new EmployeeViewModel() { FullName = acc.UserName };
                        }
                        Session[SettingKeys.EmpInfo] = modelEmpId;
                        
                   }
               }


               Session[SettingKeys.MenuList] = moduleList;
           }
           catch (Exception ex)
           {
               ex.ToString();
           }
           Response.Redirect(DesiredUrl);
       }

       public void SwitchLang(int LanguageId, string CurrentLanguage, string DesiredUrl)
       {

           //CultureInfo VNCIclone = new CultureInfo("vi-VN");

           CultureInfo myCIclone = new CultureInfo(CurrentLanguage);
           myCIclone.DateTimeFormat = myCIclone.DateTimeFormat;

           Session["CurrentLanguage"] = myCIclone;
           Session["LanguageId"] = LanguageId;
           Response.Redirect(DesiredUrl);
       }

        //public void SwitchLanguage(int SelectedLanguageId, string SelectedLanguageCode, string DesiredUrl)
       //{
       //    CultureInfo myCIclone = new CultureInfo(SelectedLanguageCode);
       //    myCIclone.DateTimeFormat = myCIclone.DateTimeFormat;

       //    Session[SettingKeys.CurrentLanguage] = myCIclone;
       //    Session[SettingKeys.LanguageCode] = SelectedLanguageCode;
       //    Session[SettingKeys.LanguageId] = SelectedLanguageId;
       //    try
       //    {
       //        AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
       //        List<SYS_tblFunctionPermissionViewModel> moduleList = db.sp_clsCommon("ModuleList", SelectedLanguageCode, "", 0, 0, 1, acc.UserName).Select(p => new SYS_tblFunctionPermissionViewModel() { Url = p.Url, FunctionID = p.FunctionID, FunctionNameE = p.FunctionNameE, Rank = p.Rank, Parent = p.Parent, Tooltips = p.Tooltips, FView = (p.FView != 0 && p.FView != null), FEdit = (p.FEdit != 0 && p.FEdit != null), FDelete = (p.FDelete != 0 && p.FDelete != null), FExport = (p.FExport != 0 && p.FExport != null) }).ToList();
       //        moduleList.Add(new SYS_tblFunctionPermissionViewModel() { Url = "/Admin/ChangePassword" });
       //        EmployeeRepository _repository = new EmployeeRepository(db);

       //        EmployeeViewModel Emp = (EmployeeViewModel)Session[SettingKeys.EmpInfo];
       //        EmployeeViewModel model = new EmployeeViewModel();
       //        if (Emp != null)
       //        {
       //            model = _repository.GetEmployee(Emp.EmpID, LanguageId);
       //            if (model == null)
       //            {
       //                model = new EmployeeViewModel() { FullName = acc.UserName };
       //            }
       //            Session[SettingKeys.EmpInfo] = model;
       //        }

       //        EmployeeViewModel EmpId = (EmployeeViewModel)Session[SettingKeys.EmpInfo];
       //        if (EmpId != null)
       //        {
       //            if (Emp != null && Emp.EmpID == EmpId.EmpID)
       //            {
       //                Session[SettingKeys.EmpInfo] = model;
       //            }
       //            else
       //            {
       //                var modelEmpId = _repository.GetEmployee(EmpId.EmpID, LanguageId);
       //                if (modelEmpId != null)
       //                {
       //                    modelEmpId = new EmployeeViewModel() { FullName = acc.UserName };
       //                }
       //                Session[SettingKeys.EmpInfo] = modelEmpId;
       //            }
       //        }

        //        Session[SettingKeys.MenuList] = moduleList;
       //    }
       //    catch (Exception ex)
       //    {
       //        ex.ToString();
       //    }
        //    Response.Redirect(DesiredUrl);

       //    //if (CurrentLanguage.Contains(ConstantClass.Language_en)) CurrentLanguage = "en-US";
       //    //else CurrentLanguage = "vi-VN";
       //    //Session[ConstantClass.Session_CultureLanguage] = new CultureInfo(CurrentLanguage);
       //    //Session[ConstantClass.Session_Language] = CurrentLanguage.Substring(0, 2);
       //    //try
       //    //{
       //    //    HomeRepository _pageRepository = new HomeRepository(db);
       //    //    List<PageViewModel> pagelst = _pageRepository.GetMenu(Session[ConstantClass.Session_Language].ToString());

       //    //    Session[ConstantClass.Session_Page] = pagelst;
       //    //    Session[ConstantClass.Session_ChangeLanguage] = true;
       //    //}
       //    //catch (Exception ex)
       //    //{
       //    //    ex.ToString();
       //    //}
       //}

    }
}
