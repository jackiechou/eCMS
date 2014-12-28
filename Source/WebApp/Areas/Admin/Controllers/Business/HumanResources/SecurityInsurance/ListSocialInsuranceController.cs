using Eagle.Repository;
using Eagle.Repository.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.HR;
using Eagle.Common.Settings;
using Eagle.Repository.SYS.Users;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class ListSocialInsuranceController : BaseController
    {
        //
        // GET: /Admin/ListSocialInsurance/

        public ActionResult Index(string Mode)
        {
            if (string.IsNullOrEmpty(Mode))
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("../Business/HumanResources/SecurityInsurance/ListSocialInsurance/_Reset");
                }
                else
                {
                    if (CurrentAcc == null)
                    {
                        Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                        return null;
                    }
                    return View("../Business/HumanResources/SecurityInsurance/ListSocialInsurance/Index");
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("../Business/HumanResources/SecurityInsurance/ListSocialInsurance/RenewHICard/_HICardReset");
                }
                else
                {
                    if (CurrentAcc == null)
                    {
                        Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                        return null;
                    }
                    return View("../Business/HumanResources/SecurityInsurance/ListSocialInsurance/RenewHICard/Index");
                }
            }
        }
        #region Search
        public ActionResult _List(InsuranceInformationSearchViewModel modelSearch)
        {
            InsuranceInformationRepository _repository = new InsuranceInformationRepository(db);
            List<InsuranceInformationSearchViewModel> list = _repository.GetAll(modelSearch,LanguageId);
            return PartialView("../Business/HumanResources/SecurityInsurance/ListSocialInsurance/_List", list);
        }
        public ActionResult _Create()
        {
            InsuranceInformationSearchViewModel model = new InsuranceInformationSearchViewModel();

            return PartialView("../Business/HumanResources/SecurityInsurance/ListSocialInsurance/_Create", model);
        }
        #endregion

        #region Renew HI Card
        public ActionResult _HICardCreate()
        {
            return PartialView("../Business/HumanResources/SecurityInsurance/ListSocialInsurance/RenewHICard/_HICardCreate");
        }

        public ActionResult _HICardList()
        {
            List<EmployeeSearchViewModel> list = new List<EmployeeSearchViewModel>();
            return PartialView("../Business/HumanResources/SecurityInsurance/ListSocialInsurance/RenewHICard/_HICardList", list);
        }

        public ActionResult _EmployeeCreatePartial()
        {
            EmployeeSearchViewModel model = new EmployeeSearchViewModel();
            return PartialView("../Business/HumanResources/SecurityInsurance/ListSocialInsurance/RenewHICard/_EmployeeCreatePartial", model);
        }

        public ActionResult _EmployeeCreateSearchResultPartial(string EmpCode, string FullName, int? SearchLSCompanyID, bool? Searchactive1)
        {
            UserRepository _repository = new UserRepository(db);
            //Tìm trong db
            List<EmployeeViewModel> Employeelst = new List<EmployeeViewModel>();
            var account = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (account != null)
            {
                Employeelst = _repository.FindEmployee(EmpCode, FullName, SearchLSCompanyID, Searchactive1, account.UserName, 10, account.FAdm == 1);
            }
            return PartialView("../Business/HumanResources/SecurityInsurance/ListSocialInsurance/RenewHICard/_EmployeeCreateSearchResultPartial", Employeelst);
        }

        public ActionResult _AddEmployeeToList(string EmployeeSelectedList, List<EmployeeSearchViewModel> EmployeeList)
        {
            if (EmployeeList == null)
            {
                EmployeeList = new List<EmployeeSearchViewModel>();
            }
            List<int> ListIDAdd = new List<int>();
            if (EmployeeSelectedList != null)
            {
                string[] strTmp = EmployeeSelectedList.Split(';');
                int tmp;
                foreach (var item in strTmp)
                {
                    try
                    {
                        tmp = Convert.ToInt32(item);
                        if (!ListIDAdd.Contains(tmp))
                        {
                            ListIDAdd.Add(tmp);
                        }
                    }
                    catch { }
                }
            }

            EmployeeRepository _repository = new EmployeeRepository(db);
            List<EmployeeSearchViewModel> modelAdds = _repository.GetByListID(ListIDAdd,LanguageId);
            foreach (var emp in modelAdds)
            {
                if (EmployeeList.Where(p => p.EmpID == emp.EmpID).FirstOrDefault() == null)
                {
                    EmployeeList.Add(emp);
                }
            }
            

            return PartialView("../Business/HumanResources/SecurityInsurance/ListSocialInsurance/RenewHICard/_HICardList", EmployeeList);
        }

        public ActionResult _DeleteEmployeeList(string EmployeeSelectedList, List<EmployeeSearchViewModel> EmployeeList)
        {
            if (EmployeeList == null)
            {
                EmployeeList = new List<EmployeeSearchViewModel>();
            }
            List<int> ListIDAdd = new List<int>();
            if (EmployeeSelectedList != null)
            {
                string[] strTmp = EmployeeSelectedList.Split(';');
                int tmp;
                foreach (var item in strTmp)
                {
                    try
                    {
                        tmp = Convert.ToInt32(item);
                        ListIDAdd.Add(tmp);
                    }
                    catch { }
                }
            }
            //xóa
            foreach (var ID in ListIDAdd)
            {
                var modelDelete = EmployeeList.FirstOrDefault(p => p.EmpID == ID);
                if (modelDelete != null)
                {
                    EmployeeList.Remove(modelDelete);
                }
            }

            return PartialView("../Business/HumanResources/SecurityInsurance/ListSocialInsurance/RenewHICard/_HICardList", EmployeeList);
        }

        public ActionResult Extend(HICardViewModel model,int AddMode, List<EmployeeSearchViewModel> EmployeeList)
        {
            string ErrorMessage = "";
            if (ModelState.IsValid)
            {
                HICardRepository _repository = new HICardRepository(db);
                if (AddMode == 1)
                {
                    //Tất cả
                    if (_repository.ExtendAll(model.FromDateAllowNull.Value, model.ToDateAllowNull.Value,out ErrorMessage))
                    {
                        return Content("success");
                    }
                    else
                    {
                        return Content(ErrorMessage);
                    }
                }
                else
                {
                    //Insert theo List
                    if (EmployeeList == null || EmployeeList.Count == 0)
                    {
                        return Content(Eagle.Resource.LanguageResource.ExtendErrorMessage_ChooseEmployees);
                    }
                    else
                    {
                        if (_repository.ExtendByEmployee(model.FromDateAllowNull.Value, model.ToDateAllowNull.Value, EmployeeList, out ErrorMessage))
                        {
                            return Content("success");
                        }
                        else
                        {
                            return Content(ErrorMessage);
                        }
                    }
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var modelError in errors)
                {
                    if (!string.IsNullOrEmpty(ErrorMessage))
                    {
                        ErrorMessage += "<br />";
                    }
                    ErrorMessage += modelError.ErrorMessage;
                }
                return Content(ErrorMessage);
            }
        }
        #endregion
    }
}
