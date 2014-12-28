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
using Eagle.Common.Helpers;
using AutoMapper;
using Eagle.Common.Settings;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class LeaveInsuranceController : BaseController
    {
        //
        // GET: /Admin/LeaveInsurance/

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../SI/LeaveInsurance/_Index");
            }
            else
            {

                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../SI/LeaveInsurance/Index");
            }
        }
        public ActionResult _Create()
        {
            InsuranceLeaveViewModel model = new InsuranceLeaveViewModel();
            model.Stage = 1;
            model.EmpID = CurrentEmp.EmpID;
            CreateViewBag(model.Stage);
            return PartialView("../SI/LeaveInsurance/_Create", model);
        }

        public ActionResult _Edit(int id)
        {
            InsuranceLeaveRepository _repository = new InsuranceLeaveRepository(db);
            InsuranceLeaveViewModel model = _repository.FindEdit(id);
            CreateViewBag(model.Stage);
            return PartialView("../SI/LeaveInsurance/_Create", model);
        }
       
        private void CreateViewBag(int? Stage = 1)
        {
            List<int> list = new List<int>(){1,2,3,4,5,6,7,8,9,10};
            ViewBag.Stage = new SelectList(list,null,null,Stage);
        }

        public ActionResult _List()
        {
            List<InsuranceLeaveViewModel> list = new List<InsuranceLeaveViewModel>();
            if (CurrentEmp != null)
            {
                InsuranceLeaveRepository _repository = new InsuranceLeaveRepository(db);
                list = _repository.GetByEmpID(CurrentEmp.EmpID);
            }

            return PartialView("../SI/LeaveInsurance/_List", list);
        }
        public ActionResult Save(InsuranceLeaveViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";

            if (acc != null)
            {
                if (ModelState.IsValid)
                {
                    //Kiểm tra tới ngày phải lớn hơn từ ngày
                    int i = model.dToDate.Value.CompareTo(model.dFromDate.Value);
                    if (i < 0)
                    {
                        errorMessage = string.Format(Eagle.Resource.LanguageResource.DateCompareInvalid,
                                                     Eagle.Resource.LanguageResource.ToDate,
                                                     Eagle.Resource.LanguageResource.FromDate);
                        return _Error(model, errorMessage);
                    }
                    else
                    {
                        //Lưu vào CSDL
                        SI_tblInsuranceLeave modelAdd = new SI_tblInsuranceLeave();
                        Mapper.CreateMap<InsuranceLeaveViewModel, SI_tblInsuranceLeave>();
                        Mapper.Map(model, modelAdd);
                        //Gán kết quả ngày
                        modelAdd.FromDate = model.dFromDate.Value;
                        modelAdd.ToDate = model.dToDate.Value;
                        modelAdd.LSLeaveDayTypeID = model.LSLeaveDayTypeIDAlowNull.Value;

                        InsuranceLeaveRepository _repository = new InsuranceLeaveRepository(db);
                        if (_repository.Add(modelAdd, out errorMessage))
                        {
                            return Content("success");
                        }
                        else
                        {
                            return _Error(model, errorMessage);
                        }
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var modelError in errors)
                    {
                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            errorMessage += "&lt;br /&gt;"; 
                        }
                        errorMessage += modelError.ErrorMessage;
                    }
                    return _Error(model, errorMessage);
                }
            }
            else
            {
                errorMessage = Eagle.Resource.LanguageResource.TimeOutError;
                return _Error(model, errorMessage);
            }
 
        }
        public ActionResult Update(InsuranceLeaveViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                if (ModelState.IsValid)
                {
                    //Lưu vào CSDL
                    SI_tblInsuranceLeave modelUpdate = new SI_tblInsuranceLeave();
                    Mapper.CreateMap<InsuranceLeaveViewModel, SI_tblInsuranceLeave>();
                    Mapper.Map(model, modelUpdate);
                    //Gán kết quả ngày
                    modelUpdate.FromDate = model.dFromDate.Value;
                    modelUpdate.ToDate = model.dToDate.Value;
                    modelUpdate.LSLeaveDayTypeID = model.LSLeaveDayTypeIDAlowNull.Value;

                    InsuranceLeaveRepository _repository = new InsuranceLeaveRepository(db);
                    if (_repository.Update(modelUpdate, out errorMessage))
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _Error(model, errorMessage);
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var modelError in errors)
                    {
                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            errorMessage += "&lt;br /&gt;";
                        }
                        errorMessage += modelError.ErrorMessage;
                    }
                    return _Error(model, errorMessage);
                }
            }
            else
            {
                errorMessage = Eagle.Resource.LanguageResource.TimeOutError;
                return _Error(model, errorMessage);
            }
        }
        public ActionResult _Delete(int id)
        {
            InsuranceLeaveRepository _repository = new InsuranceLeaveRepository(db);
            string errorMessage = "";
            if (_repository.Delete(id, out errorMessage))
            {
                return Content("success");
            }
            else
            {
                return Content(errorMessage);
            }
        }
        private ActionResult _Error(InsuranceLeaveViewModel model, string errorMessage)
        {
            if (model == null)
            {
                model = new InsuranceLeaveViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = errorMessage;
            CreateViewBag(model.Stage);
            return PartialView("../SI/LeaveInsurance/_Create", model);
        }

        #region Common

        public ActionResult CheckLSLeaveTypeCode(int LSLeaveDayTypeID)
        {
            string TyleCode = db.Timesheet_tbLeaveDayType.Where(p => p.LSLeaveDayTypeID == LSLeaveDayTypeID).Select(p => p.LS_tblLeaveType.LSLeaveTypeCode).FirstOrDefault();
            return Content(TyleCode);
        }

        private class InsuranceLeaveInfo
        {
            public decimal? AvgSalary { get; set; }
            public decimal? BenifitSalary { get; set; }
            public decimal? Percent { get; set; }
            public string AvgSalaryText { get; set; }
            public string PercentText { get; set; }
            public string BenifitSalaryText { get; set; }
            public int Total { get; set; }
        }
        public ActionResult GetInfo(int EmpID, DateTime FromDate, DateTime ToDate, int LSLeaveDayTypeID, DateTime? BabyDOB, DateTime? BabyDOD)
        {
            string MoneyFormat = "#,##0";
            InsuranceLeaveInfo model = new InsuranceLeaveInfo();
            string PreMonth = FromDate.AddMonths(-1).ToString("MM/yyyy");
            string ThisMonth = FromDate.ToString("MM/yyyy");           
            //Leave Type Code trong bảng LS_tblLeaveType
            var tmp = db.Timesheet_tbLeaveDayType.Where(p => p.LSLeaveDayTypeID == LSLeaveDayTypeID).Select(p => new { p.PercentSI, p.LS_tblLeaveType.LSLeaveTypeCode }).FirstOrDefault();
            string leaveTypeCode = tmp.LSLeaveTypeCode;
            //Phần trăm hưởng lương
            decimal Percent = 0;
            if (tmp.PercentSI != null)
            {
                Percent = tmp.PercentSI.Value;
            }
            
            //Tổng số ngày nghỉ
            int Total = (ToDate - FromDate).Days + 1;

            model.Total = Total;
            model.Percent = Percent;
            model.PercentText = Percent.ToString() + "%";
            
            //Khai báo biến tạm
            string sqlQuery = "";
            decimal AvgSalary = 0;
            Object[] parameters = { EmpID, PreMonth };
            Object[] parameters2 = { EmpID, ThisMonth };
            decimal MinSalary = 0;
            try
            {
                var payrollModel = (from p in db.SYS_tblPayrollParameter
                                    where p.EffectiveDate <= FromDate
                                    orderby p.EffectiveDate descending
                                    select p).FirstOrDefault();
                if (payrollModel != null)
                {
                    MinSalary = payrollModel.MinSalary.Value;
                }
            }
            catch { }
            switch (leaveTypeCode)
            {
                case "CHILDSICK":
                //Nghỉ con ốm
                case "SLST":
                //Nghỉ ốm dài ngày
                case "SLLT":
                //Nghỉ ốm ngắn ngày
                case "ANTENATAL":
                //Nghỉ khám thai

                    //Function lấy lương cơ bản của tháng trước tháng nghỉ 
                    //VD nghỉ từ ngày 05/09/2014 thì là lương của tháng 08/2014
                    sqlQuery = "SELECT [dbo].[SI_fnGetBasicSalarySI] ({0},{1})";
                    //Lương cơ bản ~ lương bình quân tháng trước
                    AvgSalary = db.Database.SqlQuery<decimal>(sqlQuery, parameters).FirstOrDefault();
                    if (AvgSalary != 0)
	                {
                        decimal BenifitSalary = ((AvgSalary / 26) * Total * Percent) / 100;

                        model.AvgSalary = AvgSalary;
                        model.BenifitSalary = BenifitSalary;
                        model.AvgSalaryText = AvgSalary.ToString(MoneyFormat);
                        model.BenifitSalaryText = BenifitSalary.ToString(MoneyFormat);
	                }
                    break;
                case "MAT":
                    //Nghỉ sinh con nhưng con mất
                    //Tiền bảo hiểm = Lương bình quân 6 tháng * (số ngày nghỉ) + 2 tháng lương tối thiểu
                    //Function lấy lương bình quân của nghỉ thai sản
                    sqlQuery = "SELECT [dbo].[SI_fnGetAvgSalarySI] ({0},{1})";
                    //Lương cơ bản ~ lương bình quân tháng trước
                    AvgSalary = db.Database.SqlQuery<decimal>(sqlQuery, parameters2).FirstOrDefault();
                    if (AvgSalary != 0)
                    {
                        decimal BenifitSalary = ((AvgSalary/26)* Total)  + 2 * MinSalary;

                        model.AvgSalary = AvgSalary;
                        model.BenifitSalary = BenifitSalary;
                        model.AvgSalaryText = AvgSalary.ToString(MoneyFormat);
                        model.BenifitSalaryText = BenifitSalary.ToString(MoneyFormat);
                    }
                    break;
                case "MTL1":
                    //Nghỉ thai sản (sinh 1)
                    //Tiền bảo hiểm = Lương bình quân 6 tháng * 6 tháng nghỉ + 2 tháng lương tối thiểu
                    //Lưu ý nếu chưa đi làm đủ 6 tháng tức (1,2,3,4,5 tháng) thì tính bình quân các tháng đã đi làm
                    //Function lấy lương bình quân của nghỉ thai sản
                    sqlQuery = "SELECT [dbo].[SI_fnGetAvgSalarySI] ({0},{1})";
                    //Lương cơ bản ~ lương bình quân tháng trước
                    AvgSalary = db.Database.SqlQuery<decimal>(sqlQuery, parameters2).FirstOrDefault();
                    if (AvgSalary != 0)
                    {
                        decimal BenifitSalary = AvgSalary * 6 + 2 * MinSalary;

                        model.AvgSalary = AvgSalary;
                        model.BenifitSalary = BenifitSalary;
                        model.AvgSalaryText = AvgSalary.ToString(MoneyFormat);
                        model.BenifitSalaryText = BenifitSalary.ToString(MoneyFormat);
                    }
                    break;
                case "MTL2":
                    //Nghỉ thai sản (sinh 2)
                    //Tiền bảo hiểm = Lương bình quân 6 tháng * 7 tháng nghỉ + 4 tháng lương tối thiểu
                    //Function lấy lương bình quân của nghỉ thai sản
                    sqlQuery = "SELECT [dbo].[SI_fnGetAvgSalarySI] ({0},{1})";
                    //Lương cơ bản ~ lương bình quân tháng trước
                    AvgSalary = db.Database.SqlQuery<decimal>(sqlQuery, parameters2).FirstOrDefault();
                    if (AvgSalary != 0)
                    {
                        decimal BenifitSalary = AvgSalary * 7 + 4 * MinSalary;

                        model.AvgSalary = AvgSalary;
                        model.BenifitSalary = BenifitSalary;
                        model.AvgSalaryText = AvgSalary.ToString(MoneyFormat);
                        model.BenifitSalaryText = BenifitSalary.ToString(MoneyFormat);
                    }

                    break;
                case "MTL3":
                    //Nghỉ thai sản (sinh 3) 
                    //Tiền bảo hiểm = Lương bình quân 6 tháng * 8 tháng nghỉ + 6 tháng lương tối thiểu
                    //Function lấy lương bình quân của nghỉ thai sản
                    sqlQuery = "SELECT [dbo].[SI_fnGetAvgSalarySI] ({0},{1})";
                    //Lương cơ bản ~ lương bình quân tháng trước
                    AvgSalary = db.Database.SqlQuery<decimal>(sqlQuery, parameters2).FirstOrDefault();
                    if (AvgSalary != 0)
                    {
                        decimal BenifitSalary = AvgSalary * 8 + 6 * MinSalary;

                        model.AvgSalary = AvgSalary;
                        model.BenifitSalary = BenifitSalary;
                        model.AvgSalaryText = AvgSalary.ToString(MoneyFormat);
                        model.BenifitSalaryText = BenifitSalary.ToString(MoneyFormat);
                    }
                    break;
                case "MTL4":
                    //Nghỉ thai sản (sinh 4) 
                    //Tiền bảo hiểm = Lương bình quân 6 tháng * 9 tháng nghỉ + 8 tháng lương tối thiểu
                    //Function lấy lương bình quân của nghỉ thai sản
                    sqlQuery = "SELECT [dbo].[SI_fnGetAvgSalarySI] ({0},{1})";
                    //Lương cơ bản ~ lương bình quân tháng trước
                    AvgSalary = db.Database.SqlQuery<decimal>(sqlQuery, parameters2).FirstOrDefault();
                    if (AvgSalary != 0)
                    {
                        decimal BenifitSalary = AvgSalary * 9 + 8 * MinSalary;

                        model.AvgSalary = AvgSalary;
                        model.BenifitSalary = BenifitSalary;
                        model.AvgSalaryText = AvgSalary.ToString(MoneyFormat);
                        model.BenifitSalaryText = BenifitSalary.ToString(MoneyFormat);
                    }
                    break;
                case "MTL5":
                    //Nghỉ thai sản (sinh 5) 
                    //Tiền bảo hiểm = Lương bình quân 6 tháng * 10 tháng nghỉ + 10 tháng lương tối thiểu
                    //Function lấy lương bình quân của nghỉ thai sản
                    sqlQuery = "SELECT [dbo].[SI_fnGetAvgSalarySI] ({0},{1})";
                    //Lương cơ bản ~ lương bình quân tháng trước
                    AvgSalary = db.Database.SqlQuery<decimal>(sqlQuery, parameters2).FirstOrDefault();
                    if (AvgSalary != 0)
                    {
                        decimal BenifitSalary = AvgSalary * 10 + 10 * MinSalary;

                        model.AvgSalary = AvgSalary;
                        model.BenifitSalary = BenifitSalary;
                        model.AvgSalaryText = AvgSalary.ToString(MoneyFormat);
                        model.BenifitSalaryText = BenifitSalary.ToString(MoneyFormat);
                    }
                    break;
                case "MIS":
                    //Nghỉ sảy thai
                    //Tiền bảo hiểm = Lương Bình Quân / 26 * Số ngày nghỉ
                    //Function lấy lương bình quân của nghỉ thai sản
                    sqlQuery = "SELECT [dbo].[SI_fnGetAvgSalarySI] ({0},{1})";
                    //Lương cơ bản ~ lương bình quân tháng trước
                    AvgSalary = db.Database.SqlQuery<decimal>(sqlQuery, parameters2).FirstOrDefault();
                    if (AvgSalary != 0)
                    {
                        decimal BenifitSalary = (AvgSalary / 26) * Total;

                        model.AvgSalary = AvgSalary;
                        model.BenifitSalary = BenifitSalary;
                        model.AvgSalaryText = AvgSalary.ToString(MoneyFormat);
                        model.BenifitSalaryText = BenifitSalary.ToString(MoneyFormat);
                    }
                    break;
                default:
                    break;
            }
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
