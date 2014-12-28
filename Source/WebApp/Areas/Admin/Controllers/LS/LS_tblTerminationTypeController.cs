using Eagle.Common.Helpers;
using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Repository.LS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.LS;
using Eagle.Repository.SYS.Session;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers.LS
{
    public class LS_tblTerminationTypeController : BaseController
    {
        //
        // GET: /Admin/LS_tblTerminationType/
        public ActionResult Index()
        {
            if (Session[SettingKeys.AccountInfo] != null)
            {
                if (Request.IsAjaxRequest())
                    return PartialView("../Termination/TerminationType/_Reset");
                else
                    return View("../Termination/TerminationType/Index");
            }
            else
            {
                Response.RedirectToRoute("Admin_Login");
                return null;
            }
        }

        [SessionExpiration]
        public ActionResult List()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc != null)
            {
                LS_tblTerminationTypeRespository _repository = new LS_tblTerminationTypeRespository(db);
                IList<LS_tblTerminationTypeViewModel> sources = _repository.List().ToList();
                return PartialView("../Termination/TerminationType/_List", sources);
            }
            else
            {
                Response.RedirectToRoute("Admin_Login");
                return null;
            }
        }

        [HttpGet]
        [SessionExpiration]
        public string GenerateCode()
        {
            return LS_tblTerminationTypeRespository.GenerateCode(10);
        }

        //
        // GET: /Admin/LS_tblTerminationType/Create

        [SessionExpiration]
        public ActionResult Create()
        {
            if (Session[SettingKeys.AccountInfo] != null)
            {
                ViewBag.Mode = 0;
                LS_tblTerminationTypeViewModel model = new LS_tblTerminationTypeViewModel();
                model.Code = LS_tblTerminationTypeRespository.GenerateCode(10);
                return PartialView("../Termination/TerminationType/_Edit", model);
            }
            else
            {
                Response.RedirectToRoute("Admin_Login");
                return null;
            }
        }

        //
        // GET: /Admin/LS_tblTerminationType/Details/5
        [SessionExpiration]
        [HttpGet]
        public ActionResult Details(int id)
        {
            if (Session[SettingKeys.AccountInfo] != null)
            {
                ViewBag.Mode = 1;
                LS_tblTerminationTypeRespository _repository = new LS_tblTerminationTypeRespository(db);
                LS_tblTerminationTypeViewModel model = _repository.Details(id);
                return PartialView("../Termination/TerminationType/_Edit", model);
            }
             else
             {
                 Response.RedirectToRoute("Admin_Login");
                 return null;
             }
        }

        [HttpPost]
        public ActionResult Create(LS_tblTerminationTypeViewModel model)
        {           
            string message = "";
            bool flag = false;
            try
            {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                if (acc != null)
                {
                    if (ModelState.ContainsKey("LSTerminationTypeID"))
                    {
                        ModelState["LSTerminationTypeID"].Errors.Clear();
                    }
                    //ModelState.Remove(field)

                    if (ModelState.IsValid)
                    {
                        flag = LS_tblTerminationTypeRespository.Add(model, out message);
                    }
                    else
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        foreach (var modelError in errors)
                        {
                            message += modelError.ErrorMessage + "-";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                flag = false;
            }
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Edit(LS_tblTerminationTypeViewModel model)
        {  
            string message = "";
            bool flag = false;
            try
            {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                if (acc != null)
                {
                    if (ModelState.IsValid)
                    {                   
                        flag = LS_tblTerminationTypeRespository.Update(model, out message);
                    }
                    else
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        foreach (var modelError in errors)
                        {
                            message += "&lt;br /&gt;" + modelError.ErrorMessage;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                flag = false;
            }
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Admin/TerminationParameter/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            bool flag = LS_tblTerminationTypeRespository.Delete(id, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateStatus(int id)
        {
            string message = string.Empty;
            bool flag = LS_tblTerminationTypeRespository.UpdateStatus(id, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateListOrder(int id, int listorder)
        {
            string message = string.Empty;
            bool flag = LS_tblTerminationTypeRespository.UpdateListOrder(id, listorder, out message);
            return Json(JsonUtils.SerializeResult(flag, message), JsonRequestBehavior.AllowGet);
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
            LS_tblTerminationTypeRespository _repository = new LS_tblTerminationTypeRespository(db);
            List<AutoCompleteModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum, LanguageId).ToList();
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


        // Get Countries
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetList()
        {
            var list = db.LS_tblTerminationType
                            .Select(p => new LS_tblTerminationTypeViewModel()
                            {
                                LSTerminationTypeID = p.LSTerminationTypeID,
                                Code = p.Code,
                                Name = p.Name,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).ToList();
            list.Insert(0, new LS_tblTerminationTypeViewModel() { LSTerminationTypeID = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            var retData = list.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.LSTerminationTypeID.ToString(),
            });
            return Json(retData, JsonRequestBehavior.AllowGet);
        }

    }
}
