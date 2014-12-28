using Eagle.Common.Helpers;
using Eagle.Core;
using Eagle.Repository;
using Eagle.Repository.LS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Eagle.Model;
using Eagle.Model.Common;
using Eagle.Model.LS;
using Eagle.Common.Settings;


namespace Eagle.WebApp.Areas.Admin.Controllers
{
 
    public class LS_tblCompanyController : BaseController
    {
        //
        // GET: /Admin/LS_tblCompany/

        public ActionResult Index()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblCompanyRepository _repository = new LS_tblCompanyRepository(db);
            IList<LS_tblCompanyViewModel> sources = _repository.ListCompany().ToList();
            return View(sources);
        }

        public ActionResult Details(int Id)
        {
             LS_tblCompanyRepository _repository = new LS_tblCompanyRepository(db);
             LS_tblCompanyViewModel entity = _repository.GetDetails(Id);
             return PartialView("../HR_MasterData/_LS_tblCompanyDetails", entity);
        }
       
        [HttpPost]
        public ActionResult Save(LS_tblCompanyViewModel companyModel)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LS_tblCompanyRepository _repository = new LS_tblCompanyRepository(db);
                if (ModelState.IsValid)
                {
                    LS_tblCompany LocationAdd = new LS_tblCompany()
                    {
                        LSCompanyCode = companyModel.LSCompanyCode,
                        Name = companyModel.Name,
                        VNName = companyModel.VNName,
                        Address = companyModel.Address,
                        Phone =companyModel.Phone,
                        Fax = companyModel.Fax,
                        TaxCode = companyModel.TaxCode,
                        Rank = companyModel.Rank,
                        Used = companyModel.Used,
                        Creater = acc.UserName,
                        CreateTime = System.DateTime.Today
                        
                    };
                    string  strResult = _repository.AddCompany(LocationAdd);
                    if (strResult == "success")
                    {
                        return Content("success");
                    }
                    else
                    {
                        errorMessage = strResult;
                    }
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _Error(companyModel, errorMessage);
        }


        [HttpPost]
        public ActionResult Update(LS_tblCompanyViewModel companyModel)
        {
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                LS_tblCompanyRepository _repository = new LS_tblCompanyRepository(db);
                if (_repository.Update(companyModel, out errorMessage))
                {
                    return Content("success");
                }
                else
                {
                    errorMessage = Eagle.Resource.LanguageResource.SystemError;
                    return _Error(companyModel, errorMessage);
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }

            return _Error(companyModel, errorMessage);
        }

        public ActionResult _Error(LS_tblCompanyViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblCompanyViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            return PartialView("../HR_MasterData/_LS_tblCompanyCreate", model);
        }

        #region Bind DropdownList ========================================================================
        /// <summary>
        /// Dùng cho viec binding du lieu cho dropdownlist autocomplete
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpGet]
        //public List<AutoCompleteViewModel> DropdownList(string searchTerm, int pageSize, int pageNum)
        //{
        //    LS_tblCompanyRepository _repository = new LS_tblCompanyRepository(db);
        //    List<AutoCompleteViewModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum).ToList();
        //    int iTotal = sources.Count;

        //    //Translate the list into a format the select2 dropdown expects
        //    Select2PagedResultViewModel pagedList = ConvertListToSelect2Format(sources, iTotal);

        //    var result = new JsonpResult
        //    {
        //        Data = pagedList,
        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //    };          

        //    List<AutoCompleteViewModel> lst = result.Data as List<AutoCompleteViewModel>;
        //    //Return the data as a jsonp result
        //    return lst;
        //}

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
        #endregion ======================================================================================
        
        // Get Companies  
        public JsonResult GetHierachicalList()
        {
            List<TreeNode> _list = LS_tblCompanyRepository.PopulateHierachicalDropDownList(LanguageId);
            if (_list.Count == 0)
                _list.Insert(0, new TreeNode() { id = 0, text = " --- " + @Eagle.Resource.LanguageResource.None + " --- ", });
            else
            {
                _list.Insert(0, new TreeNode() { id = 0, text = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            }
            return base.Json(_list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHierachicalCompanyList()
        {
            List<TreeNode> _list = LS_tblCompanyRepository.PopulateHierachicalDropDownList(LanguageId);
            if (_list.Count == 0)
                _list.Insert(0, new TreeNode() { id = 0, text = " --- " + @Eagle.Resource.LanguageResource.None + " --- ", });
            else
            {
                _list.Insert(0, new TreeNode() { id = 0, text = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            }
            return base.Json(_list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetParentList()
        {
            List<LS_tblCompany> lst = new List<LS_tblCompany>();
            lst = db.LS_tblCompany.Where(p => p.Parent == null || p.Parent == 0).OrderBy(p => p.Name).ToList();

            var retData = lst.Select(m => new SelectListItem()
            {
                Text = (LanguageId == 124) ? m.Name : m.VNName,
                Value = m.LSCompanyID.ToString(),
            });
            return Json(retData, JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult GetChildren(int? ParentId)
        {
            List<TreeNode> _list = LS_tblCompanyRepository.GetChildren(ParentId, LanguageId);
            if (_list.Count == 0)
                _list.Insert(0, new TreeNode() { id = 0, text = " --- " + @Eagle.Resource.LanguageResource.None + " --- ", });
            else
            {
                _list.Insert(0, new TreeNode() { id = 0, text = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            }
            return base.Json(_list, JsonRequestBehavior.AllowGet);
        }


        public List<LS_tblCompany> GetCompanyList()
        {
            List<LS_tblCompany> _list = LS_tblCompanyRepository.GetParentList();
            if (_list.Count == 0)
                _list.Insert(0, new LS_tblCompany() { LSCompanyID = 0, Name = " --- " + @Eagle.Resource.LanguageResource.None + " --- ", });
            else
            {
                _list.Insert(0, new LS_tblCompany() { LSCompanyID = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            }
            return _list;
        }

        public List<TreeNode> GetDepartmentList(int? CompanyID)
        {
            List<TreeNode> _list = LS_tblCompanyRepository.GetChildren(CompanyID, LanguageId);
            if (_list.Count == 0)
                _list.Insert(0, new TreeNode() { id = 0, text = " --- " + @Eagle.Resource.LanguageResource.None + " --- ", });
            else
            {
                _list.Insert(0, new TreeNode() { id = 0, text = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            }
            return _list;
        }

        public List<TreeNode> GetSectionList(int? DepartmentID)
        {
            List<TreeNode> _list = LS_tblCompanyRepository.GetChildren(DepartmentID,LanguageId);
            if (_list.Count == 0)
                _list.Insert(0, new TreeNode() { id = 0, text = " --- " + @Eagle.Resource.LanguageResource.None + " --- ", });
            else
            {
                _list.Insert(0, new TreeNode() { id = 0, text = " --- " + @Eagle.Resource.LanguageResource.All + " --- " });
            }
            return _list;
        }

        #region VALIDATION - LS_tblCompany - LSCompanyID =======================================================================================
         [HttpGet]
        public JsonResult ValidateLSCompanyID(int LSCompanyID)
        {
            string message = string.Empty;
            if (LSCompanyID > 0)
            {
                List<int?> allDefine = db.LS_tblCompanyDefine.Select(p => p.Parent).ToList();
                var lastDefine = db.LS_tblCompanyDefine.Where(p => !allDefine.Contains(p.LSCompanyDefineID)).FirstOrDefault();

                if (lastDefine != null)
                {
                    var modelCheck = db.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID && p.LSCompanyDefineID == lastDefine.LSCompanyDefineID).Select(p => p.LSCompanyDefineID).FirstOrDefault();
                    if (modelCheck != null)
                        message = "true";
                    if (LanguageId == 124)
                        message = string.Format(Eagle.Resource.LanguageResource.ANotIsB, Eagle.Resource.LanguageResource.LastCompanyDefine, lastDefine.Name);
                    else
                        message = string.Format(Eagle.Resource.LanguageResource.ANotIsB, Eagle.Resource.LanguageResource.LastCompanyDefine, lastDefine.VNName);
                }
                else
                    message = Eagle.Resource.LanguageResource.DataError;
            }
            else
                message = Eagle.Resource.LanguageResource.InValid;
            return base.Json(message, JsonRequestBehavior.AllowGet);
        }
        #endregion ==============================================================================================================

    }
}
