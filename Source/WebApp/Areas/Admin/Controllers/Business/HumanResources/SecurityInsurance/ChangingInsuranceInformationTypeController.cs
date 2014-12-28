using Eagle.Common.Helpers;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Repository.SYS.Session;

namespace Eagle.WebApp.Areas.Administration.Controllers
{
    public class ChangingInsuranceInformationTypeController : BaseController
    {

        [HttpGet]
        [AjaxSessionActionFilter]
        public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
        {
            ChangingInsuranceInformationTypeRepository _repository = new ChangingInsuranceInformationTypeRepository(db);
            List<AutoCompleteModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum);
            int iTotal = sources.Count;

            //Translate the list into a format the select2 dropdown expects
            Select2PagedResultViewModel pagedList = ConvertAutoListToSelect2Format(sources, iTotal);

            //Return the data as a jsonp result
            return new JsonpResult
            {
                Data = pagedList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}
