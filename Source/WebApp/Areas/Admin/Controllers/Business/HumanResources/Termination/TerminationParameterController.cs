using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository.Termination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.TER;
using Eagle.Repository.SYS.Session;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers.Termination
{
    public class TerminationParameterController : BaseController
    {
        //
        // GET: /Admin/TerminationParameter/

        [SessionExpiration]
        public ActionResult Index()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc != null)
            {
                List<TerminationParameterViewModel> lst = TerminationParameterRespository.GetList();
                TerminationParameterViewModel model = new TerminationParameterViewModel();
                if (lst.Count() == 1)
                {
                    ViewBag.Mode = 1;
                    model = lst.FirstOrDefault();
                    ViewBag.ID = model.ID;
                    PopulateIsPaidForAnnualLeaveToDropDownList(model.IsPaidForAnnualLeave);
                    PopulateIsPaidForMandatoryTrainingFeeToDropDownList(model.IsPaidForMandatoryTrainingFee);
                    PopulateIsManipulatedLeaveDayRemainsToDropDownList(model.IsManipulatedLeaveDayRemains);
                    PopulateIsManipulatedForPaidLeaveToDropDownList(model.IsManipulatedForPaidLeave);
                    PopulateIsDeletedAfterTerminationSettlementToDropDownList(model.IsDeletedAfterTerminationSettlement);
                    PopulateIsAutomatedDecisionNoToDropDownList(model.IsAutomatedDecisionNo);

                }
                else
                {
                    ViewBag.Mode = 0;
                    PopulateIsPaidForAnnualLeaveToDropDownList(null);
                    PopulateIsPaidForMandatoryTrainingFeeToDropDownList(null);
                    PopulateIsManipulatedLeaveDayRemainsToDropDownList(null);
                    PopulateIsManipulatedForPaidLeaveToDropDownList(null);
                    PopulateIsDeletedAfterTerminationSettlementToDropDownList(null);
                    PopulateIsAutomatedDecisionNoToDropDownList(null);
                }

                if (Request.IsAjaxRequest())
                {
                    return PartialView("../Business/HumanResources/Termination/TerminationParameter/_Reset");
                }
                else
                {
                    return View("../Business/HumanResources/Termination/TerminationParameter/Index", model);
                } 
            }
            else
            {
                Response.RedirectToRoute("Admin_Login");
                return null;
            }
        }


        #region REFERENCES -------------------------------------------------------------------------------------------------------------
        //IsPaidForAnnualLeave ==========================================================================
        public void PopulateIsPaidForAnnualLeaveToDropDownList(int? selected_value)
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>();
            dict.Add(1, Eagle.Resource.LanguageResource.PaidForAnnualLeave);
            dict.Add(0, Eagle.Resource.LanguageResource.NoPaidForAnnualLeave);
            if (selected_value == null)
                selected_value = 0;

            ViewBag.IsPaidForAnnualLeave = new SelectList(dict, "Key", "Value", selected_value);
        }

        //IsPaidForMandatoryTrainingFee =================================================================
        public void PopulateIsPaidForMandatoryTrainingFeeToDropDownList(int? selected_value)
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>();
            dict.Add(1, Eagle.Resource.LanguageResource.PaidForMandatoryTrainingFee);
            dict.Add(0, Eagle.Resource.LanguageResource.NoPaidForMandatoryTrainingFee);
            if (selected_value == null)
                selected_value = 0;

            ViewBag.IsPaidForMandatoryTrainingFee = new SelectList(dict, "Key", "Value", selected_value);
        }

        //IsManipulatedLeaveDayRemains ==================================================================
        public void PopulateIsManipulatedLeaveDayRemainsToDropDownList(int? selected_value)
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>();
            dict.Add(1, Eagle.Resource.LanguageResource.ManipulatedLeaveDayRemains);
            dict.Add(0, Eagle.Resource.LanguageResource.NoManipulatedLeaveDayRemains);
            if (selected_value == null)
                selected_value = 0;

            ViewBag.IsManipulatedLeaveDayRemains = new SelectList(dict, "Key", "Value", selected_value);
        }

        //IsManipulatedForPaidLeave =====================================================================
        public void PopulateIsManipulatedForPaidLeaveToDropDownList(int? selected_value)
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>();
            dict.Add(1, Eagle.Resource.LanguageResource.ManipulatedForPaidLeave);
            dict.Add(0, Eagle.Resource.LanguageResource.NoManipulatedForPaidLeave);
            if (selected_value == null)
                selected_value = 0;

            ViewBag.IsManipulatedForPaidLeave = new SelectList(dict, "Key", "Value", selected_value);
        }

        //IsDeletedAfterTermination =====================================================================
        public void PopulateIsDeletedAfterTerminationSettlementToDropDownList(int? selected_value)
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>();
            dict.Add(1, Eagle.Resource.LanguageResource.DeletedAfterTerminationSettlement);
            dict.Add(0, Eagle.Resource.LanguageResource.NoDeletedAfterTerminationSettlement);
            if (selected_value == null)
                selected_value = 0;

            ViewBag.IsDeletedAfterTerminationSettlement = new SelectList(dict, "Key", "Value", selected_value);
        }

        //IsAutomatedDecisionNo =========================================================================
        public void PopulateIsAutomatedDecisionNoToDropDownList(int? selected_value)
        {
            Dictionary<int?, string> dict = new Dictionary<int?, string>();
            dict.Add(1, Eagle.Resource.LanguageResource.AutomatedDecisionNo);
            dict.Add(0, Eagle.Resource.LanguageResource.NoAutomatedDecisionNo);
            if (selected_value == null)
                selected_value = 0;

            ViewBag.IsAutomatedDecisionNo = new SelectList(dict, "Key", "Value", selected_value);
        }

        #endregion -----------------------------------------------------------------------------------------------------------------

        //
        // POST: /Admin/TerminationParameter/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(TerminationParameterViewModel add_model)
        {
            bool flag = false;
            string Message = string.Empty;
            //try
            //{
                if (ModelState.ContainsKey("ID"))
                {
                    ModelState["ID"].Errors.Clear();
                }
                //ModelState.Remove(field)
                if (ModelState.IsValid)
                {
                    flag = TerminationParameterRespository.Add(add_model, out Message);
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var modelError in errors)
                    {
                        Message += modelError.ErrorMessage + "-";
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    Message = ex.ToString();
            //    flag = false;
            //}
            return Json(JsonUtils.SerializeResult(flag, Message), JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Admin/TerminationParameter/Edit/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(TerminationParameterViewModel edit_model)
        {
            bool flag = false;
            string Message = string.Empty;
            //try
            //{
            if (ModelState.IsValid)
            {
                flag = TerminationParameterRespository.Update(edit_model, out Message);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var modelError in errors)
                {
                    Message += modelError.ErrorMessage + "-";
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    ex.ToString();
            //    flag = false;
            //}
            return Json(JsonUtils.SerializeResult(flag, Message), JsonRequestBehavior.AllowGet);
        }


        //
        // POST: /Admin/TerminationParameter/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string result = string.Empty;
            bool flag = TerminationParameterRespository.Delete(id);
            if (flag)
                result = "success";
            return Content(result);
        }

    }
}
