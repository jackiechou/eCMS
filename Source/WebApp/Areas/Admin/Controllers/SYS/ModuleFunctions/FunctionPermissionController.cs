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
using Eagle.Model.SYS;
using Eagle.Repository.SYS.Session;
using System.Web.Script.Serialization;
using Eagle.Common.Settings;
using Eagle.Repository.SYS.Roles;
using Eagle.Repository.SYS.Menus;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class FunctionPermissionController : BaseController
    {
        //
        // GET: /Admin/UserGroup/
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../SYS/FunctionPermission/Index");
        }

        [HttpGet]
        [SessionExpiration]
        public ActionResult setUpPermisionsForMasterData(int MasterDataID)
        {
            string result = string.Empty;         
            FunctionPermissionRepository _respository = new FunctionPermissionRepository(db);
            result = _respository.CheckMasterDataPermissionResult(UserId, MasterDataID);            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [SessionExpiration]
        public ActionResult setupPermissionForFunction()
        {
            string result = string.Empty, s = string.Empty;  
            //Set Permission
            int FView = 1; int FDelete = 1; int FEdit = 1; int FExport = 1;
            List<SYS_tblFunctionPermissionViewModel> lst = (List<SYS_tblFunctionPermissionViewModel>)Session[SettingKeys.MenuList];
            if (lst != null)
            {
                string url = Request.Url.AbsolutePath.ToString().Substring(1);
                var fp = lst.Where(p => p.Url.Contains(url)).FirstOrDefault();
                //neu không phai là admin thì check quyen
                if (fp != null)
                {
                    if (((AccountViewModel)Session[SettingKeys.AccountInfo]).FAdm != 1)
                    {
                        //Neu không phai là  admin => check quyen
                        FView = (fp.FView == true) ? 1 : 0;
                        FDelete = (fp.FDelete == true) ? 1 : 0;
                        FEdit = (fp.FEdit == true) ? 1 : 0;
                        FExport = (fp.FExport == true) ? 1 : 0;
                    }
                    else
                    {
                        //Neu là admin full quyen
                        FView = 1;
                        FDelete = 1;
                        FEdit = 1;
                        FExport = 1;
                    }
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("LanguageId", Session["LanguageId"] == null ? "10001" : Session["LanguageId"].ToString());
                    dict.Add("FView", FView.ToString());
                    dict.Add("FEdit", FEdit.ToString());
                    dict.Add("FDelete", FDelete.ToString());
                    dict.Add("FExport", FExport.ToString());
                    s = serializer.Serialize(dict) + ",";
                    result = s.Remove(s.LastIndexOf(","), 1);
                }
                else
                {
                    //Neu không có quyen hoac het session acc => return
                   Response.Redirect(Url.Action("Login", "User", true));
                }
            }           
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// page load
        /// </summary>
        /// <returns>Tra tra ve ViewData cho list box group</returns>
        public ActionResult _Create()
        {
            RoleRepository _repository = new RoleRepository(db);
            List<GroupViewModel> sources = _repository.ListBox();
            ViewData["lstGroup"] = new SelectList(sources, "GroupID", "GroupName"); ;

            return PartialView("../SYS/FunctionPermission/_Create");
        }

        /// <summary>
        /// Cập nhật lại thông tin bảng "SYS_tblFunctionPermission"
        /// </summary>
        /// <param name="AllItem">Tất cả các item cần update</param>
        /// <returns>
        ///     success => Thành công
        ///     error => thất bại
        ///     donotupdate => không có item nào truyền lên nên không cập nhật
        /// </returns>
        public ActionResult _UpdatePermission(int GroupID, int ModuleID, string[] chkFView, string[] chkFEdit, string[] chkFDelete, string[] chkFExport, string[] hTypeTable, string[] AllItem)
        {
            if (chkFView != null)
            {
                var checkedView = new HashSet<string>(chkFView);
                var checkedEdit = new HashSet<string>(chkFEdit);
                var checkedDelete = new HashSet<string>(chkFDelete);
                var checkedExport = new HashSet<string>(chkFExport);
                var sTypeTable = new HashSet<string>(hTypeTable);
                var sAllItem = new HashSet<string>(AllItem);

                List<SYS_tblFunctionPermission> updateList = new List<SYS_tblFunctionPermission>();
                //Update Function
                //Function nằm trong sAllItem có từ đầu tiên bắt đầu là a 
                foreach (var item in sAllItem)
                {
                    var checkedViewForFunc = checkedView.Where(p => p.Substring(0, 1) == "a").Select(p => Convert.ToInt32(p.Substring(1)));
                    var checkedEditForFunc = checkedEdit.Where(p => p.Substring(0, 1) == "a").Select(p => Convert.ToInt32(p.Substring(1)));
                    var checkedDeleteForFunc = checkedDelete.Where(p => p.Substring(0, 1) == "a").Select(p => Convert.ToInt32(p.Substring(1)));
                    var checkedExportForFunc = checkedExport.Where(p => p.Substring(0, 1) == "a").Select(p => Convert.ToInt32(p.Substring(1)));

                    if (item.Substring(0, 1) == "a")
                    {
                        int id = Convert.ToInt32(item.Substring(1));
                        SYS_tblFunctionPermission fPermisstion = new SYS_tblFunctionPermission() { FunctionID = id, GroupID = GroupID, ModuleID = ModuleID };
                        if (checkedViewForFunc.Contains(id))
                        {
                            fPermisstion.FView = true;
                        }
                        else
                        {
                            fPermisstion.FView = false;
                        }

                        if (checkedEditForFunc.Contains(id))
                        {
                            fPermisstion.FEdit = true;
                        }
                        else
                        {
                            fPermisstion.FEdit = false;
                        }

                        if (checkedDeleteForFunc.Contains(id))
                        {
                            fPermisstion.FDelete = true;
                        }
                        else
                        {
                            fPermisstion.FDelete = false;
                        }

                        if (checkedExportForFunc.Contains(id))
                        {
                            fPermisstion.FExport = true;
                        }
                        else
                        {
                            fPermisstion.FExport = false;
                        }

                        updateList.Add(fPermisstion);
                    }
                }
                //Update MasterData
                //MasterData nằm trong sAllItem có từ đầu tiên bắt đầu là b 
                foreach (var item in sAllItem)
                {
                    var checkedViewForFunc = checkedView.Where(p => p.Substring(0, 1) == "b").Select(p => Convert.ToInt32(p.Substring(1)));
                    var checkedEditForFunc = checkedEdit.Where(p => p.Substring(0, 1) == "b").Select(p => Convert.ToInt32(p.Substring(1)));
                    var checkedDeleteForFunc = checkedDelete.Where(p => p.Substring(0, 1) == "b").Select(p => Convert.ToInt32(p.Substring(1)));
                    var checkedExportForFunc = checkedExport.Where(p => p.Substring(0, 1) == "b").Select(p => Convert.ToInt32(p.Substring(1)));

                    if (item.Substring(0, 1) == "b")
                    {
                        int id = Convert.ToInt32(item.Substring(1));
                        SYS_tblFunctionPermission fPermisstion = new SYS_tblFunctionPermission() { MasterDataID = id, GroupID = GroupID, ModuleID = ModuleID };
                        if (checkedViewForFunc.Contains(id))
                        {
                            fPermisstion.FView = true;
                        }
                        else
                        {
                            fPermisstion.FView = false;
                        }

                        if (checkedEditForFunc.Contains(id))
                        {
                            fPermisstion.FEdit = true;
                        }
                        else
                        {
                            fPermisstion.FEdit = false;
                        }

                        if (checkedDeleteForFunc.Contains(id))
                        {
                            fPermisstion.FDelete = true;
                        }
                        else
                        {
                            fPermisstion.FDelete = false;
                        }

                        if (checkedExportForFunc.Contains(id))
                        {
                            fPermisstion.FExport = true;
                        }
                        else
                        {
                            fPermisstion.FExport = false;
                        }

                        updateList.Add(fPermisstion);
                    }
                }

                //Update lại table
                // Nếu chưa có thì thêm mới
                // Nếu đã có thì update
                //
                foreach (var item in updateList)
                {
                    //Kiểm tra SYS_tblFunctionPermission trong csdl
                    SYS_tblFunctionPermission fp = new SYS_tblFunctionPermission();
                    if (item.FunctionID != null)
                    {
                        fp = db.SYS_tblFunctionPermission.Where(p => p.GroupID == item.GroupID && p.FunctionID == item.FunctionID).FirstOrDefault();
                    }
                    else
                    {
                        fp = db.SYS_tblFunctionPermission.Where(p => p.GroupID == item.GroupID && p.MasterDataID == item.MasterDataID).FirstOrDefault();
                    }

                    //Nếu có ít nhất một thuộc tính được chọn (cập nhật lại csdl)
                    //Nếu k chọn thuộc tính nào cả => xóa bỏ (nếu có)
                    if (item.FDelete == true || item.FEdit == true || item.FExport == true || item.FView == true)
                    {
                        // Nếu chưa có thêm mới      
                        if (fp == null)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Added;
                        }
                        else
                        {
                            fp.FView = item.FView;
                            fp.FEdit = item.FEdit;
                            fp.FDelete = item.FDelete;
                            fp.FExport = item.FExport;
                            db.Entry(fp).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                    else
                    {
                        if (fp != null)
                        {
                            db.Entry(fp).State = System.Data.Entity.EntityState.Deleted;
                        }
                    }
                }
                try
                {
                    
                    db.SaveChanges();
                    return Content("success");
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    return Content("error");
                }

            }
            else
            {
                return Content("warning");
            }
        }
        /// <summary>
        /// the hien danh sach chuc nang can phan quyen thuoc 1 module
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns>tra ve danh sach cac function thuoc module duoc chon de phan quyen chuc nang</returns>
        [HttpGet]
        public ActionResult _FunctionByModule(int? ModuleID, int? GroupID) 
        {
            MenuRepository _repository = new MenuRepository(db);

            List<FunctionListCreateViewModel> functionList = db.SYS_spfrmFunctionPermission("ListFunction", "EN", null, ModuleID, GroupID, null, null, null)
                                        .Select(p => new FunctionListCreateViewModel()
                                        {
                                            FunctionID = p.FunctionID,
                                            FunctionNameE = p.Name,
                                            FunctionNameV = p.VNName,
                                            strTypeTable = p.TypeTable,
                                            FExport = p.FExport == true,
                                            FDelete = p.FDelete == true,
                                            FView = p.FView == true,
                                            FEdit = p.FEdit == true
                                            
                                        }).ToList();
            return PartialView("../SYS/FunctionPermission/_FunctionList", functionList);
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
            FunctionPermissionRepository _repository = new FunctionPermissionRepository(db);
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
