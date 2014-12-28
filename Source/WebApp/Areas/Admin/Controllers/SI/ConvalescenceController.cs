using AutoMapper;
using Eagle.Core;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.HR;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class ConvalescenceController : BaseController
    {
        //
        // GET: /Admin/Index/
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../SI/Convalescence/Index");
        }

        public ActionResult _CreateDataPartial()
        {
            ConvalescenceSearchViewModel model = new ConvalescenceSearchViewModel();
            return PartialView("../SI/Convalescence/_CreateDataPartial", model);
        }
        public ActionResult _SearchPartial()
        {
            ConvalescenceSearch2ViewModel model = new ConvalescenceSearch2ViewModel();
            return PartialView("../SI/Convalescence/_SearchPartial", model);
        }

        public ActionResult _Create()
        {
            ConvalescenceViewModel model = new ConvalescenceViewModel();
            return PartialView("../SI/Convalescence/_Create", model);
        }

        public ActionResult _List()
        {
            List<ConvalescenceViewModel> List = new List<ConvalescenceViewModel>();
            return PartialView("../SI/Convalescence/_List", List);
        }

        public ActionResult Search(ConvalescenceSearch2ViewModel model)
        {
            List<ConvalescenceViewModel> List = new List<ConvalescenceViewModel>();
            ConvalescenceRepository _repository = new ConvalescenceRepository(db);
            List = _repository.Search(model,LanguageId);

            return PartialView("../SI/Convalescence/_SearchResultList", List);
        }


        public ActionResult CreateData(ConvalescenceSearchViewModel model)
        {
            List<ConvalescenceViewModel> List = new List<ConvalescenceViewModel>();
            ConvalescenceRepository _repository = new ConvalescenceRepository(db);
            List = _repository.GetBy(model,LanguageId);

            /*Loại: Tập trung - Không tập trung*/
            Dictionary<bool, string> ConcentrateList = new Dictionary<bool,string>();
            ConcentrateList.Add(true, Eagle.Resource.LanguageResource.Centralization);
            ConcentrateList.Add(false, Eagle.Resource.LanguageResource.Inattention);
            ViewBag.ConcentrateList = new SelectList(ConcentrateList, "Key", "Value", false);
            
            /*Kỳ báo cáo từ 1 tới 10*/
            Dictionary<int, int> StageList = new Dictionary<int, int>();
            for (int i = 1; i <= 10; i++)
            {
                StageList.Add(i, i);
            };
            ViewBag.StageList = new SelectList(StageList, "Key", "Value", 1);

            return PartialView("../SI/Convalescence/_List", List);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            string errorMessage = "";
            ConvalescenceRepository _repository = new ConvalescenceRepository(db);
            if (_repository.Delete(id, out errorMessage))
            {
                return Content("success");
            }
            else
            {
                return Content(errorMessage);
            }
        }

        public ActionResult Save(List<ConvalescenceViewModel> list)
        {
            if (list != null && list.Count > 0)
            {
                string errorMessage = "";
                ConvalescenceRepository _repository = new ConvalescenceRepository(db);
                if (_repository.Insert(list, out errorMessage))
                {
                    return Content("success");
                }
                else
                {
                    return Content(errorMessage);
                }
            }
            else
            {
                return Content(Eagle.Resource.LanguageResource.NoDataUpdate);
            }
            
        }

        //Truyền vào ngày lấy ra lương tối thiểu
        class MinSalary
        {
            public string minSalary { get; set; }
            public string minSalaryWithFormat { get; set; }
        }
        public ActionResult CheckMinSalary(string strDate)
        {
            DateTime? date;
            decimal minSalary = 0;
            MinSalary model = new MinSalary();
            Eagle.Common.Utilities.DateTimeUtils.TryConvertToDate(strDate, out date);

            if (date != null)
            {
                var payrollModel = (from p in db.SYS_tblPayrollParameter
                                    where p.EffectiveDate <= date
                                    orderby p.EffectiveDate descending
                                    select p).FirstOrDefault();

                if (payrollModel != null)
                {
                    minSalary = payrollModel.MinSalary.Value;
                }
            }

            model.minSalary = minSalary.ToString();
            model.minSalaryWithFormat = minSalary.ToString("#,##0");

            return base.Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
