using Eagle.Common.Helpers;
using Eagle.Model;
using Eagle.Model.SYS.Roles;
using Eagle.Repository.SYS.Roles;
using Eagle.Repository.SYS.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.SYS.Roles
{
    public class RoleController : BaseController
    {
        [SessionExpiration]
        public ActionResult GetRolePermission()
        {
            List<RoleModulePermissionViewModel> sources = RoleRepository.GetRolePermissions();
            return PartialView("../Sys/Roles/_RolePermission", sources);
        }


        //
        // GET: /Admin/Role/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Dùng cho viec binding du lieu cho dropdownlist autocomplete
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
        {
            RoleRepository _repository = new RoleRepository(db);
            List<AutoCompleteViewModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum).ToList();
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
