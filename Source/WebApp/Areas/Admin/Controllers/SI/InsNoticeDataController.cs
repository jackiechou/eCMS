using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.HR;
//
using Eagle.Core;
using Eagle.Repository;
using System.Web.Routing;
using Eagle.Common.Helpers;
using Eagle.Common.Settings;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class InsNoticeDataController : BaseController
    {
        //
        // GET: /Admin/InsNoticeData/
        

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../SI/InsNoticeData/Index");
        }
        public ActionResult _SearchHeaderEmp()
        {
            InsNoticeDataCreateViewModels model = new InsNoticeDataCreateViewModels();
            model.Stage = 1;
            CreateViewBag(model.Stage);
            return PartialView("../SI/InsNoticeData/_HeaderSearchEmp");
        }
        public ActionResult _List()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            InsNoticeDataRepository _repository = new InsNoticeDataRepository(db);
            List<SI_spfrmInsNoticeData_Result> sources = new List<SI_spfrmInsNoticeData_Result>();
            return PartialView("../SI/InsNoticeData/_List", sources);
        }
        public ActionResult _Search(InsNoticeDataCreateViewModels model, string Status, int ModuleID)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            List<SI_spfrmInsNoticeData_Result> sources = new List<SI_spfrmInsNoticeData_Result>();
            List<SI_spfrmInsNoticeData_Search_Result> sources_search = new List<SI_spfrmInsNoticeData_Search_Result>();
            if (Status == "True")
            {
                sources = db.SI_spfrmInsNoticeData(LanguageId, 10, acc.UserName, null, model.EmpCode, model.FullName, model.strAriseFromDate, model.strAriseToDate, model.SourceID, model.strInsNoticeFromDate, model.strInsNoticeToDate, 1, model.Stage).ToList();
                return PartialView("../SI/InsNoticeData/_List", sources);
            }
            else
            {
                sources_search = db.SI_spfrmInsNoticeData_Search(LanguageId, 10, acc.UserName, model.EmpCode, model.FullName, model.strAriseFromDate, model.strAriseToDate, model.SourceID, model.strInsNoticeFromDate, model.strInsNoticeToDate, 1, model.Stage).ToList();
                return PartialView("../SI/InsNoticeData/_ListSearch", sources_search);
            }

            
        }
        public ActionResult _Delete(int id)
        {
            InsNoticeDataRepository _repository = new InsNoticeDataRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult Save(List<InsNoticeDataListViewModels> model)
        {
            List<SI_tblInsNoticeData> ret = new List<SI_tblInsNoticeData>();
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
        
            if (acc != null)
            {
                InsNoticeDataRepository _repository = new InsNoticeDataRepository(db);
                if (ModelState.IsValid)
                {
                    System.DateTime? dArriseDate;
                    System.DateTime? dToDate;
                    System.DateTime? dInsNoticeDate;
                    System.DateTime? dIncNoticeDate;
                    System.DateTime? dRightNoticeDate;
                    System.DateTime? dFromMonth;
                    System.DateTime? dToMonth;
                    
                    foreach (var item in model)
                    {
                        if (item.chkCheck)
                        {

                            Eagle.Common.Utilities.DateTimeUtils.TryConvertToDate(item.AriseDate, out dArriseDate);
                            Eagle.Common.Utilities.DateTimeUtils.TryConvertToDate(item.ToDate, out dToDate);
                            Eagle.Common.Utilities.DateTimeUtils.TryConvertToDate(item.InsNoticeDate, out dInsNoticeDate);
                            Eagle.Common.Utilities.DateTimeUtils.TryConvertToDate(item.IncNoticeDate, out dIncNoticeDate);
                            Eagle.Common.Utilities.DateTimeUtils.TryConvertToDate(item.IncNoticeDate, out dIncNoticeDate);
                            Eagle.Common.Utilities.DateTimeUtils.TryConvertToDate(item.RightNoticeDate, out dRightNoticeDate);
                            Eagle.Common.Utilities.DateTimeUtils.TryConvertToDate("01/" + item.FromMonth, out dFromMonth);
                            Eagle.Common.Utilities.DateTimeUtils.TryConvertToDate("01/" + item.ToMonth, out dToMonth);
                            
                            

                            SI_tblInsNoticeData add = new SI_tblInsNoticeData()
                            {
                                EmpID = item.EmpID,
                                AriseDate = dArriseDate.Value,
                                ToDate = dToDate,
                                SourceID = item.SourceID,
                                IsInsNotice = true,
                                Stage = item.Stage.Value, 
                                InsNoticeDate = dInsNoticeDate,
                                IncNoticeDate = dIncNoticeDate,
                                RightNoticeDate = dRightNoticeDate,
                                OldSalaryCoef = item.OldSalaryCoef,
                                OldSalary  = item.OldSalary,
                                NewSalaryCoef = item.NewSalaryCoef,
                                NewSalary = item.NewSalary,
                                FromMonth = dFromMonth,
                                ToMonth = dToMonth,
                                Percent =  item.Percent,
                                ReturnHICard = item.ReturnHICard,
                                NoteInc = item.NoteInc,
                                NoteDesc = item.NoteDesc,
                                PercentSI = item.PercentSI,
                                PercentHI = item.PercentHI,
                                PercentUI =item.PercentUI,
                                SIIncrease = item.SIIncrease,
                                SIDecrease = item.SIDecrease,
                                HIIncrease =item.HIIncrease,
                                HIDecrease = item.HIDecrease,
                                UIIncrease = item.UIIncrease,
                                UIDecrease = item.UIDecrease,
                                AdjustedMonths = item.AdjustedMonths,
                                SIIncreaseAdjusted = item.SIIncreaseAdjusted,
                                SIDecreaseAdjusted = item.SIIncreaseAdjusted,
                                HIIncreaseAdjusted = item.HIIncreaseAdjusted,
                                HIDecreaseAdjusted = item.HIDecreaseAdjusted,
                                UIIncreaseAdjusted = item.UIIncreaseAdjusted,
                                UIDecreaseAdjusted = item.UIDecreaseAdjusted,
                                PercentSupplement = item.PercentSupplement,
                                SupplementType = item.SupplementType,
                                TotalMonth = item.TotalMonth,
                                IsSupplement = item.IsSupplement
                            };
                            ret.Add(add);
                        }
                    }
                    //add du lieu vao database
                    string bResult = _repository.Add(ret);
                    if (bResult == "success")
                    {
                        return Content("success");
                    }
                    else
                    {
                        return Content("error");
                    }
                }
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));

                    // Breakpoint, Log or examine the list with Exceptions.

                }
            }
            return Content("success");
        }
        private void CreateViewBag(int? Stage = 1)
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            ViewBag.Stage = new SelectList(list, null, null, Stage);
        }
        public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
        {
            InsNoticeDataRepository _repository = new InsNoticeDataRepository(db);
            List<AutoCompleteViewModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum, LanguageId).ToList();
            int iTotal = sources.Count;

            //Translate the list into a format the select2 dropdown expects
            Select2PagedResultViewModel pagedList = ConvertListToSelect2Format(sources, iTotal);

            //Return the data as a jsonp result
            return new JsonpResult
            {
                Data = pagedList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }   
    }
}

