using Eagle.Core;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class SystemParameterController : BaseController
    {
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?returnUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../SYS/SystemParameter/Index");
        }       

        #region Edit
        public PartialViewResult _Edit()
        {
            SystemParameterViewModel modelForEdit = new SystemParameterViewModel();
            SYS_tblParameterRepository _repository = new SYS_tblParameterRepository(db);
            SYS_tblParameter model = _repository.FindFirst();
            if (model != null)
            {
                modelForEdit.ParameterID = model.ParameterID;
                modelForEdit.CutOffSI = model.CutOffSI;
                modelForEdit.TypeOfSalary = model.TypeOfSalary;
                modelForEdit.LeaveType = model.LeaveType;
                modelForEdit.Day = model.DateExpireLeaveForward.Day;
                modelForEdit.Month = model.DateExpireLeaveForward.Month;
                modelForEdit.OTLimit = model.OTLimit;
                modelForEdit.StandardWorking = model.StandardWorking;
                modelForEdit.StandardAnnualLeave = model.StandardAnnualLeave;
                modelForEdit.NightOTFrom = model.NightOTFrom;
                modelForEdit.NightOTTo = model.NightOTTo;
                modelForEdit.DayOfLongTerm = model.DayOfLongTerm;

            }
            CreateViewBag(modelForEdit.Day, modelForEdit.Month, modelForEdit.CutOffSI);
            return PartialView("../SYS/SystemParameter/_Edit", modelForEdit);
        }

        private void CreateViewBag(int? Dayzz, int? Monthzz,int? Cutoffzz)
        {
            int Day = 1;
            int Month = 1;
            int CutOff = 1;
            if (Monthzz != null)
            {
                Month = (int)Monthzz;
            }
            if (Dayzz != null)
            {
                Day = (int)Dayzz;
            }
            if (Cutoffzz != null)
            {
                CutOff = (int)Cutoffzz;
            }
            List<int> months = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                months.Add(i);
            }
            ViewBag.Months = new SelectList(months, null, null, Month);

            List<int> days = new List<int>();
            for (int j = 1; j <= DateTime.DaysInMonth(DateTime.Now.Year,Month); j++)
            {
                days.Add(j);
            }
            ViewBag.Days = new SelectList(days, null, null, Day);

            List<int> cutoffs = new List<int>();
            for (int i = 1; i <= 31; i++)
            {
                cutoffs.Add(i);
            }
            ViewBag.CutOffSI = new SelectList(cutoffs, null, null, CutOff);

        }

        [HttpPost]
        public ActionResult _Edit(SystemParameterViewModel model,int Days, int Months)
        {
            DateTime DateExpireLeaveForward = new DateTime();

            if (model.OTLimit < 0)
            {
                string result = String.Format(Eagle.Resource.LanguageResource.IsGreaterOrEqualThan0, Eagle.Resource.LanguageResource.OTLimit);
                return Content(result);
            }
            if (model.StandardAnnualLeave < 0)
            {
                string result = String.Format(Eagle.Resource.LanguageResource.IsGreaterOrEqualThan0, Eagle.Resource.LanguageResource.StandardAnnualLeave);
                return Content(result);
            }

            try
            {
                DateExpireLeaveForward = new DateTime(DateTime.Now.Year, Months, Days);
            }
            catch 
            {
                //Thông báo lỗi ngày không đúng
                return Content("error");
            }

            SYS_tblParameterRepository _repository = new SYS_tblParameterRepository(db);
            SYS_tblParameter modelForEdit = _repository.FindFirst();
            /*Nếu đã tồn tại thì update nếu chưa có thì add*/
            //Update
            if (modelForEdit != null)
            {                
                modelForEdit.CutOffSI = model.CutOffSI;
                modelForEdit.TypeOfSalary = model.TypeOfSalary;
                modelForEdit.LeaveType = model.LeaveType;
                modelForEdit.OTLimit = model.OTLimit;
                modelForEdit.StandardWorking = model.StandardWorking;
                modelForEdit.DateExpireLeaveForward = DateExpireLeaveForward;
                modelForEdit.StandardAnnualLeave = model.StandardAnnualLeave;
                modelForEdit.NightOTFrom = model.NightOTFrom;
                modelForEdit.NightOTTo = model.NightOTTo;
                modelForEdit.DayOfLongTerm = model.DayOfLongTerm;
                _repository.Update(modelForEdit);
            }
            //Add
            else{
                modelForEdit = new SYS_tblParameter();
                modelForEdit.CutOffSI = model.CutOffSI;
                modelForEdit.TypeOfSalary = model.TypeOfSalary;
                modelForEdit.LeaveType = model.LeaveType;
                modelForEdit.OTLimit = model.OTLimit;
                modelForEdit.StandardWorking = model.StandardWorking;
                modelForEdit.DateExpireLeaveForward = DateExpireLeaveForward;
                modelForEdit.StandardAnnualLeave = model.StandardAnnualLeave;
                modelForEdit.NightOTFrom = model.NightOTFrom;
                modelForEdit.NightOTTo = model.NightOTTo;
                modelForEdit.DayOfLongTerm = model.DayOfLongTerm;
                _repository.Add(modelForEdit);
            }
            return Content("success");
        }
        #endregion



    }
}
