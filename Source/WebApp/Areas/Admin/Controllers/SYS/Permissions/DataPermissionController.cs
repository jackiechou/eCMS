using Eagle.Common.Helpers;
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
   
    public class DataPermissionController : BaseController
    {
        //
        // GET: /Admin/DataPermission/

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                CreateViewBag();
                return PartialView("../SYS/DataPermission/_Reset");
            }else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                CreateViewBag();
                return View("../SYS/DataPermission/Index");
            }
        }
        private void CreateViewBag()
        {
            var allGroup = db.SYS_tblGroup.Select(p => new { p.GroupID, p.GroupName }).ToList();
            ViewBag.GroupID = new SelectList(allGroup, "GroupID", "GroupName");

            SYS_tblFunctionListRepository _flrepository = new SYS_tblFunctionListRepository(db);
            List<FunctionListViewModel> allfl = _flrepository.GetAllModules(LanguageId);
            ViewBag.Function = new SelectList(allfl, "FunctionID", "FunctionNameE");

            List<int?> allDefine = db.LS_tblCompanyDefine.Select(p => p.Parent).ToList();
            var lastDefine = db.LS_tblCompanyDefine.Where(p => !allDefine.Contains(p.LSCompanyDefineID)).Select(p => p.Parent).FirstOrDefault();
            var NearLast = db.LS_tblCompanyDefine.Where(p => p.LSCompanyDefineID == lastDefine).FirstOrDefault();

            if (LanguageId == 124)
            {
                ViewBag.NearLast = NearLast.Name;
            }
            else
            {
                ViewBag.NearLast = NearLast.VNName;
            }
        }
        public ActionResult SaveDataPermisstion(int GroupID, int Function, int? LSCompanyID, string[] Checked)
        {
            //Kiểm tra công ty phải được chọn
            SYS_tblDataPermissionRepository _repository = new SYS_tblDataPermissionRepository(db);
            if (LSCompanyID == null)
            {
                return Content(Eagle.Resource.LanguageResource.PleaseInputCompanyName);
            }

            if (Checked != null)
            {
                Checked = Checked.Distinct().ToArray();
            }
            
            List<int?> listCheck = new List<int?>();

            foreach (var item in Checked)
            {
                int tmp = 0;
                if (int.TryParse(item, out tmp))
                {
                    listCheck.Add(tmp);
                }
            }

            
            // nếu mà không check section nào thì xóa hết
            string errorMessage = string.Empty;
            if (Checked == null)// || CheckDepartment == null)
            {
                // xóa hết 
                if (_repository.Remove(GroupID, Function,LSCompanyID, out errorMessage))
                {
                    return Content("success");
                }
                else
                {
                    return Content(errorMessage);
                }
            }
            else if (Checked != null)
            {
                if (_repository.Update(GroupID, Function,LSCompanyID, listCheck, out errorMessage))
                {
                    return Content("success");
                }
                else
                {
                    return Content(errorMessage);
                }
            }
            return Content("success");

        }



        public ActionResult GetSettings(int GroupID, int Function, int NearLastCompanyID)
        {
            //Last Define in LS_tblCompanyDefine
            List<int?> allDefine = db.LS_tblCompanyDefine.Select(p => p.Parent).ToList();
            var lastDefine = db.LS_tblCompanyDefine.Where(p => !allDefine.Contains(p.LSCompanyDefineID)).Select(p => p.LSCompanyDefineID).FirstOrDefault();
            //Select LSCompany Get from View
            var LsCompany = db.LS_tblCompany.Where(p => p.LSCompanyID == NearLastCompanyID).FirstOrDefault();
            
            //Get all childen of LsCompany where level like lastDefine
            List<DataPermissionViewModel> result = (from company in db.LS_tblCompany
                                                    join p in db.SYS_tblDataPermission.Where(q => q.ModuleID == Function && q.GroupID == GroupID) on company.LSCompanyID equals p.LSCompanyID into tmpList1
                                                    from list1 in tmpList1.DefaultIfEmpty()
                                                    where company.Lineage.Contains(LsCompany.Lineage) &&
                                                          company.LSCompanyDefineID == lastDefine
                                                          
                                                    select new DataPermissionViewModel() { LSCompanyID = company.LSCompanyID, LSCompanyName = company.Name, Checked = list1 != null, GroupID = GroupID, ModuleID = Function, DataPermissionID = (list1 != null ? list1.DataPermissionID : 0) })
                         .ToList();

            return PartialView("../SYS/DataPermission/_List", result);
        }




        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetDepartmentsByCompanyID(int? LSCompanyID,int? ModuleId,int? GroupId )
        {
            //var departments = db.SYS_spfrmDataPermission_GetDepartmentByCompanyId(LSCompanyID,ModuleId,GroupId);

            //var retData = departments.Select(m => new SelectListItem()
            //{
            //    Text = m.Name,
            //    Value = m.LSLevel1ID.ToString(),
            //    Selected = m.Checked != 0,
            //});
            //return Json(retData, JsonRequestBehavior.AllowGet);
            return null;
        }
        //Hàm lấy tất cả các Section (level2)
        //chỉ load những section mà deparment được check
        //Sử dụng: load lại company
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetSectionByCompanyId(int? LSCompanyID, int? ModuleId, int? GroupId)
        {
            //var sections = db.SYS_spfrmDataPermission_GetSectionByCompanyId(LSCompanyID, ModuleId, GroupId);

            //var retData = sections.Select(m => new SelectListItem()
            //{
            //    Text = m.Name,
            //    Value = m.LSLevel2ID.ToString(),
            //    Selected = m.Checked != 0,
            //});
            //return Json(retData, JsonRequestBehavior.AllowGet);
            return null;
        }

        //Hàm lấy tất cả các Section (level2)
        //Load những section ngay cả deparment không được check
        //Sử dụng: khi nhấn nút check all
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetSectionWithUncheckByCompanyId(int? LSCompanyID, int? ModuleId, int? GroupId)
        {
            //var sections = db.SYS_spfrmDataPermission_GetSectionWithUncheckByCompanyId(LSCompanyID, ModuleId, GroupId);

            //var retData = sections.Select(m => new SelectListItem()
            //{
            //    Text = m.Name,
            //    Value = m.LSLevel2ID.ToString(),
            //    Selected = m.Checked != 0,
            //});
            //return Json(retData, JsonRequestBehavior.AllowGet);
            return null;
        }
        //[AcceptVerbs(HttpVerbs.Get)]
        //public JsonResult GetSectionByDepartmentID(int? CompanyId, int? Company, int? DepartmentID, int? ModuleId, int? GroupId)
        //{
        //    var departments = db.SYS_spfrmDataPermission_GetSectionByDepartmentId(Company, DepartmentID, ModuleId, GroupId).ToList();

        //    var retData = departments.Select(m => new SelectListItem()
        //    {
        //        Text = m.Name,
        //        Value = m.LSLevel2ID.ToString(),
        //        Selected = m.Checked != 0,
        //    });
        //    return Json(retData, JsonRequestBehavior.AllowGet);
        //}
        /*Autocomplete*/
        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
        {
            LS_tblCompanyRepository _repository = new LS_tblCompanyRepository(db);
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
