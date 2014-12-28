using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Eagle.Core;
using Eagle.Repository;
using Eagle.Model;
using Eagle.Common.Utilities;
using Eagle.Common.Helpers;

using System.Web.Routing;
using Eagle.Repository.HR;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class LeaveApplicationController : BaseController
    {
        //
        // GET: /Admin/LeaveApplication/

        const int FunctionID = 457;

        public ActionResult Index(int? LeaveApplicationMasterID)
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?returnUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            if (LeaveApplicationMasterID == null)
            {
                return View("../Timesheet/LeaveApplication/Index");
            }else
            {
                return View("../Timesheet/LeaveApplication/Edit", LeaveApplicationMasterID);
            }
        }
        /// <summary>
        /// Load màn hình danh sach các leave request
        /// </summary>
        /// <returns>list</returns>
        public ActionResult List()
        {
            return View("../Timesheet/LeaveApplication/ListRecord");
        }
        /// <summary>
        /// danh sach đơn xin nghi phép cua nhan vien
        /// </summary>
        /// <returns></returns>
        public ActionResult _ListRecord(LeaveApplicationCreateViewModel model, int? ModuleID =9)
        {
            AccountViewModel acc = (AccountViewModel)Session["acc"];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            if ( ModuleID == null)
                ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"].ToString());

            LeaveEntitlementRepository _repository = new LeaveEntitlementRepository(db);
            List<LeaveApplicationCreateViewModel> sources = _repository.ListRecord(false, acc.UserName, ModuleID, CurrentEmpId, model).ToList();
            return PartialView("../Timesheet/LeaveApplication/_ListRecord", sources);
        }
        /// <summary>
        /// tra ve partial view de xem thong tin phep nam cua nhan vien 
        /// và thực hiện update thong tin request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _ViewDetail(int LeaveApplicationMasterID)
        {
            LeaveEntitlementRepository _repository = new LeaveEntitlementRepository(db);
            LeaveApplicationCreateViewModel modelEdit = _repository.FindEdit(LeaveApplicationMasterID);
            LeaveEntitlementViewModel model = _repository.LeaveEntitlement(modelEdit.EmpID, modelEdit.Year);
            ViewBag.Year = modelEdit.Year;
            ViewData["EmployeeCreate"] = modelEdit.Creater;
            ViewData["LeaveEntitlement"] = model.LeaveEntitlement;
            ViewData["Seniority"] = model.Seniority;
            ViewData["TotalLeave"] = model.TotalLeave;

            ViewData["LeaveForward"] = model.LeaveForward;
            ViewData["LeaveTakenInfor"] = model.LeaveTaken;
            ViewData["Balance"] = model.Balance;

            #region xet phan quyen edit va approve                
                int status = modelEdit.Status;
                int empID = modelEdit.EmpID;
                var CurrentLevelApprove = modelEdit.LevelApprover;
                int ID = modelEdit.LeaveApplicationMasterID;

                bool DisabledSaveAndSend = true;
                bool DisabledApproveAndUnapprove = true;
                int maxLevelApprove = db.LS_tblOnlineProcess.Single(p => p.FunctionID == FunctionID).NoOfLevel;
                CommonRepository _commonRepository = new CommonRepository(db);
                _commonRepository.CheckPermission(status, CurrentEmpId, empID, FunctionID, CurrentLevelApprove, maxLevelApprove, ID, ref DisabledSaveAndSend, ref DisabledApproveAndUnapprove);
                CreateViewBag(DisabledSaveAndSend, DisabledApproveAndUnapprove);
            #endregion

            return PartialView("../Timesheet/LeaveApplication/_Create", modelEdit);
        }
        /// <summary>
        /// Thong tin phep cua nhan vien: tong phep, tham nien, da nghi , phep con lai ....
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        public ActionResult _Create(int? Year)
        {
            AccountViewModel acc = (AccountViewModel)Session["acc"];
            if (acc != null)
            {
                

                ViewData["EmployeeCreate"] =  acc.UserName;

                LeaveEntitlementRepository _Repository = new LeaveEntitlementRepository(db);
                if (Year == null)
                    Year = System.DateTime.Now.Year;

                LeaveEntitlementViewModel model = _Repository.LeaveEntitlement(CurrentEmpId, Year);
                ViewBag.Year = Year;
                ViewData["LeaveEntitlement"] = model.LeaveEntitlement;
                ViewData["Seniority"] = model.Seniority;
                ViewData["TotalLeave"] = model.TotalLeave;

                ViewData["LeaveForward"] = model.LeaveForward;
                ViewData["LeaveTakenInfor"] = model.LeaveTaken;
                ViewData["Balance"] = model.Balance;
                // khoi tao viewbag de thiet lap cho cac nut save, send for approval, Approve, Unapprove
                CreateViewBag();   
            }

            return PartialView("../Timesheet/LeaveApplication/_Create", null);
        }
        /// <summary>
        /// tra ve so ngay nghi phep theo ngay duoc chon
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <returns></returns>
        public ActionResult _GetDate(DateTime FromDate, DateTime ToDate)
        {
            LeaveEntitlementRepository _Repository = new LeaveEntitlementRepository(db);
            string strReturn = _Repository.LeaveTaken(FromDate, ToDate, CurrentEmpId);
            return Content(strReturn);
        }
        /// <summary>
        /// Tra ve danh sach ngay nghi phep sau khi chon ngay 
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <returns></returns>
        public ActionResult _ListDays(DateTime? FromDate, DateTime? ToDate)
        {
            if (FromDate == null || ToDate == null)
            {
                List<LeaveApplicationDetailListViewModel> sources1 = new List<LeaveApplicationDetailListViewModel>();
                return PartialView("../Timesheet/LeaveApplication/_ListDays", sources1);
            }
            else
            {
                LeaveEntitlementRepository _Repository = new LeaveEntitlementRepository(db);
                List<LeaveApplicationDetailListViewModel> sources = _Repository.ListLeaveTaken(FromDate.Value, ToDate.Value, CurrentEmpId).ToList();
                return PartialView("../Timesheet/LeaveApplication/_ListDays", sources);
            }
        }
        /// <summary>
        /// Tra ve danh sach ngay nghỉ phép khi edit
        /// </summary>
        /// <param name="id">LeaveApplicationMasterID</param>
        /// <returns>List<LeaveApplicationDetailListViewModel></returns>
        public ActionResult _GetListDays(int id)
        {
            if (id != 0)
            {
                LeaveEntitlementRepository _Repository = new LeaveEntitlementRepository(db);
                List<LeaveApplicationDetailListViewModel> sources = _Repository.ListLeaveTakenEdit(id).ToList();
                return PartialView("../Timesheet/LeaveApplication/_ListDays", sources);
            }
            else
            {
                return Content("");
            }
        }
        /// <summary>
        /// them mới với nút send for approval set trang thai status va level approve = 1
        /// </summary>
        /// <param name="lstDays"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save_SendForApproval(List<LeaveApplicationDetailListViewModel> lstDays, LeaveApplicationCreateViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session["acc"];
            string errorMessage = "";
            if (acc != null)
            {
                int? EmpID = CurrentEmpId;

                LeaveEntitlementRepository _Repository = new LeaveEntitlementRepository(db);
                if (ModelState.IsValid)
                {

                    Timesheet_tblLeaveApplicationMaster add = new Timesheet_tblLeaveApplicationMaster()
                    {
                        LeaveApplicationMasterID = model.LeaveApplicationMasterID,
                        Year = model.Year,
                        EmpID =EmpID.Value,
                        Creater = acc.UserName,
                        CreateTime = DateTime.Now,
                        LSLeaveDayTypeID = model.LSLeaveDayTypeID,
                        Status = 1,
                        LevelApprover = 1,
                        FromDate = model.FromDate,
                        ToDate = model.ToDate,
                        Days = model.LeaveTaken,
                        Comment = model.Comment,
                        isFirstComment = false,
                        CurrentComment = ""
                    };
                    if (!string.IsNullOrEmpty(add.Comment))
                    {
                        string template = "<div class='row-fluid reset-margin-top-bottom'><div class='span8 offset2 reset-margin-top-bottom'><strong>{0}</strong>:<br />{1}</div><div class='span2 reset-margin-top-bottom'><i>{2} <br /> {3}</i></div></div>";
                        add.Comment = string.Format(template, CurrentAcc.UserName, model.Comment, Eagle.Resource.LanguageResource.btnSendForApproval, DateTime.Now.ToString("dd/MM/yyy: hh:mm"));
                    }
                    add.Timesheet_tblLeaveApplicationDetail = new List<Timesheet_tblLeaveApplicationDetail>();

                    foreach (var item in lstDays)
                    {
                        if (item.Days != 0)
                        {
                            Timesheet_tblLeaveApplicationDetail addlist = new Timesheet_tblLeaveApplicationDetail()
                            {
                                LeaveApplicationMasterID = add.LeaveApplicationMasterID,
                                LeaveDate = item.LeaveDate,
                                Days = item.Days,
                                TypeLeave = item.TypeLeave
                            };
                            add.Timesheet_tblLeaveApplicationDetail.Add(addlist);
                        }
                    }

                    if (!_Repository.CheckExist(model.FromDate, model.ToDate, model.EmpID, model.LSLeaveDayTypeID, model.LeaveApplicationMasterID))
                    {
                        if (_Repository.Add(add, model.BalanceCompare, out errorMessage))
                        {
                            #region Gửi mail cho cấp trên
                            SendMailApproved(add);
                            #endregion
                            return Content("success");
                        }
                        else
                            return Content(errorMessage);
                    }
                    else
                    {
                        return Content(Eagle.Resource.LanguageResource.ExistData);
                    }
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return Content(errorMessage);
        }

        /// <summary>
        /// update  cho truong hop nut send for approval
        /// </summary>
        /// <param name="lstDays"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update_SendForApproval(List<LeaveApplicationDetailListViewModel> lstDays, LeaveApplicationCreateViewModel model)
        {

            AccountViewModel acc = (AccountViewModel)Session["acc"];
            string errorMessage = "";
            if (acc != null)
            {
                LeaveEntitlementRepository _Repository = new LeaveEntitlementRepository(db);
                if (ModelState.IsValid)
                {

                    Timesheet_tblLeaveApplicationMaster modelUpdate = _Repository.Find(model.LeaveApplicationMasterID);
                    modelUpdate.LSLeaveDayTypeID = model.LSLeaveDayTypeID;
                    modelUpdate.Status = 1;
                    modelUpdate.LevelApprover = 1;
                    modelUpdate.FromDate = model.FromDate;
                    modelUpdate.ToDate = model.ToDate;
                    modelUpdate.Days = model.LeaveTaken;
                    modelUpdate.isFirstComment = false;
                    modelUpdate.CurrentComment = "";
                    if (!string.IsNullOrEmpty(model.Comment))
                    {
                        if (modelUpdate.isFirstComment == true)
                        {
                            string template = "<div class='row-fluid reset-margin-top-bottom'><div class='span8 offset2 reset-margin-top-bottom'><strong>{0}</strong>:<br />{1}</div><div class='span2 reset-margin-top-bottom'><i>{2} <br /> {3}</i></div></div>";
                            modelUpdate.Comment = string.Format(template, CurrentAcc.UserName, model.Comment, Eagle.Resource.LanguageResource.btnSendForApproval, DateTime.Now.ToString("dd/MM/yyy: hh:mm"));
                        }
                        else
                        {
                            string template = "<div class='row-fluid reset-margin-top-bottom'><div class='span8 offset2 reset-margin-top-bottom'><strong>{0}</strong>:<br />{1}</div><div class='span2 reset-margin-top-bottom'><i>{2} <br /> {3}</i></div></div>";
                            modelUpdate.Comment += string.Format(template, CurrentAcc.UserName, model.CurrentComment, Eagle.Resource.LanguageResource.btnSendForApproval, DateTime.Now.ToString("dd/MM/yyy: hh:mm"));
                       
                        }
                   }


                    List<DateTime> lst = lstDays.Where(p => p.Days != 0).Select(p => p.LeaveDate).ToList();
                    List<Timesheet_tblLeaveApplicationDetail> lstDelete = new List<Timesheet_tblLeaveApplicationDetail>();
                    foreach (var item in modelUpdate.Timesheet_tblLeaveApplicationDetail)
                    {
                        //Có thì cập nhật
                        if (lst.Contains(item.LeaveDate))
                        {
                            var modelDetailUpdate = lstDays.Single(p => p.LeaveDate == item.LeaveDate);
                            item.TypeLeave = modelDetailUpdate.TypeLeave;
                            item.Days = modelDetailUpdate.Days;
                        }
                        else
                        {
                            // nếu không có thì xóa
                            lstDelete.Add(item);
                        }
                    }
                    //Xóa
                    foreach (var item in lstDelete)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }

                    //Chưa có trong database thì thêm
                    List<DateTime> lstInDataBase = modelUpdate.Timesheet_tblLeaveApplicationDetail.Select(p => p.LeaveDate).ToList();
                    List<LeaveApplicationDetailListViewModel> lstAdd = lstDays.Where(p => p.Days != 0 && !lstInDataBase.Contains(p.LeaveDate)).ToList();
                    foreach (var item in lstAdd)
                    {
                        modelUpdate.Timesheet_tblLeaveApplicationDetail.Add(new Timesheet_tblLeaveApplicationDetail()
                        {
                            LeaveDate = item.LeaveDate,
                            LeaveApplicationMasterID = modelUpdate.LeaveApplicationMasterID,
                            TypeLeave = item.TypeLeave,
                            Days = item.Days
                        });
                    }

                    //if (!_Repository.CheckExist(dConvertFrom, dConvertTo, model.EmpID, model.LSLeaveDayTypeID))
                    //{
                    if (_Repository.Update(modelUpdate, out errorMessage))
                    {
                        #region Gửi mail cho cấp trên
                        SendMailApproved(modelUpdate);
                        #endregion
                        return Content("success");
                    }
                    else
                        return Content(errorMessage);
                    //}
                    //else
                    //{
                    //    return Content(Eagle.Resource.LanguageResource.ExistData);
                    //}
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return Content(errorMessage);
        }
        /// <summary>
        /// Khi moi bat dau vao trang tao moi thuc hien save
        /// </summary>
        /// <param name="lstDays"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(List<LeaveApplicationDetailListViewModel> lstDays, LeaveApplicationCreateViewModel model)
        {

            AccountViewModel acc = (AccountViewModel)Session["acc"];
            string errorMessage = ""; 
            if (acc != null)
            {
                int? EmpID = CurrentEmpId;

                LeaveEntitlementRepository _Repository = new LeaveEntitlementRepository(db);
                if (ModelState.IsValid)
                {

                    Timesheet_tblLeaveApplicationMaster add = new Timesheet_tblLeaveApplicationMaster()
                    {
                        LeaveApplicationMasterID = model.LeaveApplicationMasterID,
                        Year = model.Year,
                        EmpID = EmpID.Value,
                        Status = 0, 
                        Creater = acc.UserName,
                        CreateTime = DateTime.Now,
                        LSLeaveDayTypeID = model.LSLeaveDayTypeID,
                        LevelApprover = 0,
                        FromDate = model.FromDate,
                        ToDate = model.ToDate,
                        Days = model.LeaveTaken,
                        //Comment = model.Comment,
                        isFirstComment = true,
                        CurrentComment = model.Comment
                        
                    };
                    add.Timesheet_tblLeaveApplicationDetail = new List<Timesheet_tblLeaveApplicationDetail>();

                    foreach (var item in lstDays)
                    {
                        if (item.Days != 0)
                        {
                            Timesheet_tblLeaveApplicationDetail addlist = new Timesheet_tblLeaveApplicationDetail()
                            {
                                LeaveApplicationMasterID = add.LeaveApplicationMasterID,
                                LeaveDate = item.LeaveDate,
                                Days = item.Days,
                                TypeLeave = item.TypeLeave
                            };
                            add.Timesheet_tblLeaveApplicationDetail.Add(addlist);
                        }
                    }

                    if (!_Repository.CheckExist(model.FromDate, model.ToDate, model.EmpID, model.LSLeaveDayTypeID, model.LeaveApplicationMasterID))
                    {
                        if (_Repository.Add(add, model.BalanceCompare, out errorMessage))
                            return Content("success");
                        else
                            return Content(errorMessage); 
                    }
                    else
                    {
                        return Content(Eagle.Resource.LanguageResource.ExistData);   
                    }
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return Content(errorMessage); 
        }
        /// <summary>
        /// thuc hien update sau khi save
        /// </summary>
        /// <param name="lstDays"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(List<LeaveApplicationDetailListViewModel> lstDays, LeaveApplicationCreateViewModel model)
        {

            AccountViewModel acc = (AccountViewModel)Session["acc"];
            string errorMessage = "";
            if (acc != null)
            {
                LeaveEntitlementRepository _Repository = new LeaveEntitlementRepository(db);
                if (ModelState.IsValid)
                {

                    Timesheet_tblLeaveApplicationMaster modelUpdate = _Repository.Find(model.LeaveApplicationMasterID);
                    modelUpdate.LSLeaveDayTypeID = model.LSLeaveDayTypeID;                        
                    modelUpdate.FromDate = model.FromDate;
                    modelUpdate.ToDate = model.ToDate;
                    modelUpdate.Days = model.LeaveTaken;
                    if (modelUpdate.isFirstComment == true)
                        modelUpdate.CurrentComment = model.Comment;
                    else
                        modelUpdate.CurrentComment = model.CurrentComment;


                    List<DateTime> lst = lstDays.Where(p => p.Days != 0).Select(p => p.LeaveDate).ToList();
                    List<Timesheet_tblLeaveApplicationDetail> lstDelete = new List<Timesheet_tblLeaveApplicationDetail>();
                    foreach (var item in modelUpdate.Timesheet_tblLeaveApplicationDetail)
                    {
                        //Có thì cập nhật
                        if (lst.Contains(item.LeaveDate))
                        {
                            var modelDetailUpdate = lstDays.Single(p => p.LeaveDate == item.LeaveDate);
                            item.TypeLeave = modelDetailUpdate.TypeLeave;
                            item.Days = modelDetailUpdate.Days;
                        }
                        else
                        {
                            // nếu không có thì xóa
                            lstDelete.Add(item);
                        }
                    }
                    //Xóa
                    foreach (var item in lstDelete)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }

                   //Chưa có trong database thì thêm
                    List<DateTime> lstInDataBase = modelUpdate.Timesheet_tblLeaveApplicationDetail.Select(p => p.LeaveDate).ToList();
                    List<LeaveApplicationDetailListViewModel> lstAdd = lstDays.Where(p => p.Days != 0 && !lstInDataBase.Contains(p.LeaveDate)).ToList();
                    foreach (var item in lstAdd)
                    {
                        modelUpdate.Timesheet_tblLeaveApplicationDetail.Add(new Timesheet_tblLeaveApplicationDetail (){
                            LeaveDate = item.LeaveDate,
                            LeaveApplicationMasterID = modelUpdate.LeaveApplicationMasterID,
                            TypeLeave = item.TypeLeave,
                            Days = item.Days
                        });
                    }

                    //if (!_Repository.CheckExist(dConvertFrom, dConvertTo, model.EmpID, model.LSLeaveDayTypeID))
                    //{
                        if (_Repository.Update(modelUpdate, out errorMessage))
                            return Content("success");
                        else
                            return Content(errorMessage);
                    //}
                    //else
                    //{
                    //    return Content(Eagle.Resource.LanguageResource.ExistData);
                    //}
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return Content(errorMessage);
        }
        /// <summary>
        /// Thong bao loi
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _CreateError(LeaveApplicationCreateViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LeaveApplicationCreateViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Timesheet/LeaveApplication/_Create", model);

        }
        ///
        private void CreateViewBag(bool DisabledSaveAndSend = true, bool DisabledApproveAndUnapprove = false)
        {
            ViewBag.EmpId = CurrentEmpId;
            //Ẩn hiện các nút Approve
            ViewBag.DisabledSaveAndSend = DisabledSaveAndSend ? "" : "disabled";
            ViewBag.DisabledApproveAndUnapprove = DisabledApproveAndUnapprove ? "" : "disabled";
        }
        /// <summary>
        /// thuc hien approve /  unapprove leave request
        /// </summary>
        /// <param name="model">LeaveApplicationCreateViewModel</param>
        /// <param name="mode">Approve hay Unapprove</param>
        /// <returns>string</returns>
        public ActionResult Approve(LeaveApplicationCreateViewModel model, string mode)
        {
            AccountViewModel acc = (AccountViewModel)Session["acc"];
            string errorMessage = "";
            if (acc != null)
            {
                LeaveEntitlementRepository _Repository = new LeaveEntitlementRepository(db);
                var modelEdit = _Repository.Find(model.LeaveApplicationMasterID);
                if (model.LevelApprover == modelEdit.LevelApprover)
                {
                    LS_tblOnlineProcess opModel = db.LS_tblOnlineProcess.Where(p => p.FunctionID == FunctionID).FirstOrDefault();
                    #region xet trang thai, leaveapprove khi approve va unapprove
                    if (mode == "approve")
                    {
                        //Nếu line manager approve phải xét thêm
                        //nếu họ có quyền approve cấp 2 thì tăng thẳng lên 1 cấp
                        if (model.LevelApprover == 1)
                        {
                            //Kiểm tra quyền approve cấp 2
                            //SELECT  oe.ApproveLevel2
                            //FROM  dbo.SYS_tblOnlineProcessEmp oe 
                            //INNER JOIN dbo.SYS_tblProcessOnlineMaster m ON oe.OnlineProcessID = m.OnlineProcessID
                            //INNER JOIN dbo.LS_tblOnlineProcess op ON m.DMQuiTrinhID = op.DMQuiTrinhID
                            //WHERE oe.EmpID = 29 and op.FunctionID = 457 and oe.ApproveLevel2 > 0 
                            //and oe.LSCompanyID = @LSCompanyID)

                            int LSCopanyID = db.HR_tblEmp.Where(p => p.EmpID == model.EmpID).Select(p => p.LSCompanyID).FirstOrDefault();
                            var approveLevel2 = (from oe in db.SYS_tblOnlineProcessEmp
                                                 join m in db.SYS_tblProcessOnlineMaster on oe.OnlineProcessID equals m.OnlineProcessID
                                                 join op in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals op.DMQuiTrinhID
                                                 where oe.EmpID == CurrentAcc.EmpID 
                                                       && op.FunctionID == FunctionID 
                                                       && oe.ApproveLevel2 == true
                                                       && oe.LSCompanyID == LSCopanyID
                                                 select oe.ApproveLevel2).FirstOrDefault();
                                                
                            //Nếu có quyền approve cấp 2
                            if (approveLevel2 == true)
                            {
                                //Nếu chỉ cho duyệt 1 cấp
                                if (opModel.NoOfLevel == 1)
                                {
                                    modelEdit.LevelApprover = 1;
                                    modelEdit.Status = 2;
                                }
                                //Nếu chỉ cho duyệt 2 cấp
                                else if (opModel.NoOfLevel == 2)
                                {
                                    modelEdit.LevelApprover = 2;
                                    modelEdit.Status = 4;
                                }
                                //Nếu cho duyệt 3 cấp trở lên
                                else
                                {
                                    modelEdit.LevelApprover = 3;
                                    modelEdit.Status = 4; 
                                }

                            }else
                            {
                                //Current user Không có quyền approve cấp 2
                                if (model.LevelApprover < opModel.NoOfLevel)
                                {
                                    modelEdit.LevelApprover += 1;
                                }
                                modelEdit.Status = model.LevelApprover * 2; // sẽ bằng 2,4,6,8 hoặc 10
                            }
                        }
                        else
                        {
                            if (model.LevelApprover < opModel.NoOfLevel)
                            {
                                modelEdit.LevelApprover += 1;
                            }
                            modelEdit.Status = model.LevelApprover * 2; // sẽ bằng 2,4,6,8 hoặc 10
                        }
                        
                    }
                    else //if(mode == "unapprove")
                    {
                        // == true => back to prev process
                        // == false => back to first
                        if (opModel.IsStart ==1)
                        {
                            modelEdit.Status = model.LevelApprover * 2 + 1; // sẽ bằng 3,5,7,9 hoặc 11
                            modelEdit.LevelApprover -= 1;
                        }
                        else
                        {
                            modelEdit.Status = model.LevelApprover * 2 + 1; // sẽ bằng 3,5,7,9 hoặc 11
                            modelEdit.LevelApprover = 0;
                        }
                    }
                    #endregion
                    #region get cac status khi approve , unapprove,  ca các comment
                    if (!string.IsNullOrEmpty(model.CurrentComment))
                    {
                        string statusName = "";
                        var ProcessOnlineMaster = db.LS_tblOnlineProcess.Where(p => p.FunctionID == FunctionID).Select(p => p.SYS_tblProcessOnlineMaster).FirstOrDefault();
                        if (ProcessOnlineMaster != null)
                        {
                            var statusModel = ProcessOnlineMaster.FirstOrDefault();
                            if (statusModel != null)
                            {
                                switch (modelEdit.Status)
                                {
                                    case 2:
                                        statusName = statusModel.StatusLevel1A;
                                        break;
                                    case 3:
                                        statusName = statusModel.StatusLevel1U;
                                        break;
                                    case 4:
                                        statusName = statusModel.StatusLevel2A;
                                        break;
                                    case 5:
                                        statusName = statusModel.StatusLevel2U;
                                        break;
                                    case 6:
                                        statusName = statusModel.StatusLevel3A;
                                        break;
                                    case 7:
                                        statusName = statusModel.StatusLevel3U;
                                        break;
                                    case 8:
                                        statusName = statusModel.StatusLevel4A;
                                        break;
                                    case 9:
                                        statusName = statusModel.StatusLevel4U;
                                        break;
                                    case 10:
                                        statusName = statusModel.StatusLevel5A;
                                        break;
                                    case 11:
                                        statusName = statusModel.StatusLevel4U;
                                        break;
                                }
                            }
                        }
                        string template = "<div class='row-fluid reset-margin-top-bottom'><div class='span8 offset2 reset-margin-top-bottom'><strong>{0}</strong>:<br />{1}</div><div class='span2 reset-margin-top-bottom'><i>{2} <br /> {3}</i></div></div>";
                        modelEdit.Comment += string.Format(template, CurrentAcc.UserName, model.CurrentComment, statusName, DateTime.Now.ToString("dd/MM/yyy: hh:mm"));

                    }
                    #endregion
                    #region thuc hien update va send mail 
                    if (_Repository.Update(modelEdit, out errorMessage))
                    {
                        //Gửi mail
                        if (mode == "approve")
                        {
                            //Gửi mail tới cấp tiếp theo
                            SendMailApproved(modelEdit);
                            //Gửi báo cho người tạo
                            SendForCreateUser(modelEdit);
                        }
                        else
                        {
                            //Gửi mail tới cấp trước nếu  được trả về cấp trước
                            if (modelEdit.LevelApprover != 0)
                            {
                                SendMailUnapproved(modelEdit);
                            }

                        }
                        return Content("success");
                    }
                    else
                    {
                        return Content(errorMessage);
                    }
                    #endregion
                }
                else
                {
                    //Đã duyệt rồi thì thôi không được duyệt nữa
                    return Content(Eagle.Resource.LanguageResource.OnlineProcessApproved);
                }
            }
            return Content("success");
        }




        /// <summary>
        /// tra ve danh sach leave request khi vao man hinh Leave record  ung voi phan quyen du lieu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult _Search()
        {
            ViewBag.Year = System.DateTime.Now.Year;
            return PartialView("../Timesheet/LeaveApplication/_InputSearch");
        }
        /// <summary>
        /// Delete  dòng tren lưới
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            LeaveEntitlementRepository _repository = new LeaveEntitlementRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }


        private void SendMailUnapproved(Timesheet_tblLeaveApplicationMaster modelEdit)
        {
            try
            {
                Mail_SendMailCalendar mailSend = new Mail_SendMailCalendar();
                int? ToEmpID = null;
                string cc = "";

                string Link = domainUrl + "Admin/LeaveApplication/Index?LeaveApplicationMasterID=" + modelEdit.LeaveApplicationMasterID.ToString() + "&ModuleID=9";

                if (modelEdit.LevelApprover == 0)//Chỉ gửi cho người tạo
                {
                    ToEmpID = modelEdit.EmpID;
                }
                else if (modelEdit.LevelApprover == 1)//Gửi cho line manager, cc cho người tạo
                {
                    ToEmpID = db.HR_tblEmp.Where(p => p.EmpID == modelEdit.EmpID).Select(p => p.LineManagerID).FirstOrDefault();
                    cc = modelEdit.EmpID.ToString();
                }
                else // gửi cho người được quyền approve level này, cc cho người tạo
                {

                    var employee = db.HR_tblEmp.Find(modelEdit.EmpID);
                    switch (modelEdit.LevelApprover)
                    {
                        case 2:
                            var result = (from p in db.SYS_tblOnlineProcessEmp
                                          join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                          join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                          join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                          where p.ApproveLevel2 == true && p.LSCompanyID == employee.LSCompanyID &&
                                          o.FunctionID == FunctionID
                                          select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();
                            ToEmpID = result.EmpID;
                            break;
                        case 3:
                            var result2 = (from p in db.SYS_tblOnlineProcessEmp
                                          join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                          join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                          join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                          where p.ApproveLevel3 == true && p.LSCompanyID == employee.LSCompanyID &&
                                          o.FunctionID == FunctionID
                                          select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();
                            ToEmpID = result2.EmpID;
                            break;
                        case 4:
                            var result3 = (from p in db.SYS_tblOnlineProcessEmp
                                          join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                          join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                          join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                          where p.ApproveLevel4 == true && p.LSCompanyID == employee.LSCompanyID &&
                                          o.FunctionID == FunctionID
                                          select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();
                            ToEmpID = result3.EmpID;
                            break;
                    }
                    cc = modelEdit.EmpID.ToString();
                }

                 //Thông tin onlineProcessID, CC, Bcc
                var OptionTmp = (from op in db.LS_tblOnlineProcess
                                    join pom in db.SYS_tblProcessOnlineMaster on op.DMQuiTrinhID equals pom.DMQuiTrinhID
                                    where op.FunctionID == FunctionID
                                    select new { pom.OnlineProcessID, op.DMQuiTrinhID }).FirstOrDefault();
                if (OptionTmp != null)
                {
                    //lấy thông tin mail cần cc
                    List<int> EmpIDMailCC = (from mailCC in db.LS_tblOnlineProcessMailCc
                                             where mailCC.DMQuiTrinhID == OptionTmp.DMQuiTrinhID
                                             select mailCC.EmpID).ToList();
                    string MailCC = String.Join(",", EmpIDMailCC.ToArray());
                    if (!string.IsNullOrEmpty(cc))
                    {
                        if (string.IsNullOrEmpty(MailCC))
                        {
                            MailCC = cc;
                        }
                        else
                        {
                            MailCC += ","+ cc;
                        }
                    }
                    //lấy thông tin mail cần Bcc
                    List<int> EmpIDMailBCC = (from mailCC in db.LS_tblOnlineProcessMailBcc
                                              where mailCC.DMQuiTrinhID == OptionTmp.DMQuiTrinhID
                                              select mailCC.EmpID).ToList();
                    string MailBCC = String.Join(",", EmpIDMailBCC.ToArray());

                    var FromEmp = db.HR_tblEmp.Where(p => p.EmpID == CurrentAcc.EmpID).Select(p => new { FullName = p.LastName + " " + p.FirstName}).FirstOrDefault();
                    var ToEmp = db.HR_tblEmp.Where(p => p.EmpID == ToEmpID).Select(p => new { FullName = p.LastName + " " + p.FirstName }).FirstOrDefault();

                    mailSend.ToEmpID = ToEmpID;
                    mailSend.CcEmpID = MailCC;
                    mailSend.BccEmpID = MailBCC;
                    mailSend.FunctionID = FunctionID;
                    mailSend.OnlineProcessID = OptionTmp.OnlineProcessID;
                    mailSend.SendTime = DateTime.Now;
                    mailSend.MailTemplateID = 33;
                    mailSend.Param = "FullName|" + ToEmp.FullName + "|From|" + FromEmp.FullName + "|Link|" + Link;
                    mailSend.isSent = false;
                    mailSend.Actived = true;

                    db.Entry(mailSend).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                }
            }
            catch
            {
            }
            
        }

        //Mail này gửi lên cấp tiếp theo
        //Được gửi theo mỗi lần duyệt
        //Nếu là cấp cuối cùng thì không gửi
        private void SendMailApproved(Timesheet_tblLeaveApplicationMaster model, bool isSendToApproval = true)
        {
            // được gửi từ nhân viên
            #region Gửi lên cấp trên
            var employee = db.HR_tblEmp.Find(model.EmpID);
            var from = "";
            var dearToFullName = "";
            var toEmail = "";
            
            int? ToEmpID = null;
            if (employee != null)
            {
                from = employee.LastName + " " + employee.FirstName;
             

                switch (model.LevelApprover)
                {
                    case 1:
                        // gửi tới nhân viên approve
                        dearToFullName = db.HR_tblEmp.Where(p => p.EmpID == employee.LineManagerID).Select(p => p.LastName + " " + p.FirstName).FirstOrDefault();
                        ToEmpID = employee.LineManagerID;
                        break;
                    case 2:
                        // gửi tới nhân viên approve
                        var result2 = (from p in db.SYS_tblOnlineProcessEmp
                                      join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                      join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                      join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                      where p.ApproveLevel2 == true &&
                                            p.LSCompanyID == employee.LSCompanyID &&
                                      
                                            o.FunctionID == FunctionID
                                      select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (result2 != null)
                        {
                            dearToFullName = result2.LastName + " " + result2.FirstName;
                            toEmail = result2.Email;
                            ToEmpID = result2.EmpID;
                        }
                        break;
                    case 3:
                        // gửi tới nhân viên approve
                        var result3 = (from p in db.SYS_tblOnlineProcessEmp
                                       join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                       join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                       join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                       where p.ApproveLevel3 == true &&
                                             p.LSCompanyID == employee.LSCompanyID &&
                                       o.FunctionID == FunctionID
                                       select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (result3 != null)
                        {
                            dearToFullName = result3.LastName + " " + result3.FirstName;
                            toEmail = result3.Email;
                            ToEmpID = result3.EmpID;
                        }
                        break;
                    case 4:
                        // gửi tới nhân viên approve
                        var result4 = (from p in db.SYS_tblOnlineProcessEmp
                                       join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                       join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                       join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                       where p.ApproveLevel4 == true &&
                                             p.LSCompanyID == employee.LSCompanyID &&
                                       o.FunctionID == FunctionID
                                       select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (result4 != null)
                        {
                            dearToFullName = result4.LastName + " " + result4.FirstName;
                            toEmail = result4.Email;
                            ToEmpID = result4.EmpID;
                        }
                        break;
                    case 5:    
                        // gửi tới nhân viên approve
                        var result5 = (from p in db.SYS_tblOnlineProcessEmp
                                       join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                       join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                       join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                       where p.ApproveLevel5 == true &&
                                             p.LSCompanyID == employee.LSCompanyID &&
                                       o.FunctionID == FunctionID
                                       select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (result5 != null)
                        {
                            dearToFullName = result5.LastName + " " + result5.FirstName;
                            toEmail = result5.Email;
                            ToEmpID = result5.EmpID;
                        }
                        break;
                }
                
            }
            
            string FullName = dearToFullName;
            string From = from;
            string Link = domainUrl + "Admin/LeaveApplication/Index?LeaveApplicationMasterID=" + model.LeaveApplicationMasterID.ToString() + "&ModuleID=9";
           
            //Thông tin onlineProcessID, CC, Bcc
            var OptionTmp = (from op in db.LS_tblOnlineProcess
                                join pom in db.SYS_tblProcessOnlineMaster on op.DMQuiTrinhID equals pom.DMQuiTrinhID
                                where op.FunctionID == FunctionID
                                select new { pom.OnlineProcessID, op.DMQuiTrinhID }).FirstOrDefault();
            if (OptionTmp != null)
            {
                //lấy thông tin mail cần cc
                List<int> EmpIDMailCC = (from mailCC in db.LS_tblOnlineProcessMailCc
                                         where mailCC.DMQuiTrinhID == OptionTmp.DMQuiTrinhID
                                         select mailCC.EmpID).ToList();
                string MailCC = String.Join(",", EmpIDMailCC.ToArray());
               
                //lấy thông tin mail cần Bcc
                List<int> EmpIDMailBCC = (from mailCC in db.LS_tblOnlineProcessMailBcc
                                          where mailCC.DMQuiTrinhID == OptionTmp.DMQuiTrinhID
                                          select mailCC.EmpID).ToList();
                string MailBCC = String.Join(",", EmpIDMailBCC.ToArray());

                //Nếu người gửi tới khác với chính mình thì mới gửi
                //(khả năng này xuất hiện trong trường hợp cấp cuối cùng duyệt)
                if (ToEmpID != CurrentAcc.EmpID)
                {
                    Mail_SendMailCalendar mailSend = new Mail_SendMailCalendar();
                    mailSend.ToEmpID = ToEmpID;
                    mailSend.CcEmpID = MailCC;
                    mailSend.BccEmpID = MailBCC;
                    mailSend.FunctionID = FunctionID;
                    mailSend.OnlineProcessID = OptionTmp.OnlineProcessID;
                    mailSend.SendTime = DateTime.Now;
                    mailSend.MailTemplateID = 32;
                    mailSend.Param = "FullName|" + FullName + "|From|" + From + "|Link|" + Link;
                    mailSend.isSent = false;
                    mailSend.Actived = true;

                    db.Entry(mailSend).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                }

                //Nếu mà gửi lên cấp 2,3,4,5 mail thông báo
          
            }  
            #endregion
           
        }

        private void SendForCreateUser(Timesheet_tblLeaveApplicationMaster modelEdit)
        {
            try
            {
                var Approver = db.HR_tblEmp.Where(p => p.EmpID == CurrentAcc.EmpID).Select(p => new { FullName = p.LastName + " " + p.FirstName }).FirstOrDefault();
                var emp = db.HR_tblEmp.Where(p => p.EmpID == modelEdit.EmpID).Select(p => new { FullName = p.LastName + " " + p.FirstName }).FirstOrDefault();
                string Link = domainUrl + "Admin/LeaveApplication/Index?LeaveApplicationMasterID=" + modelEdit.LeaveApplicationMasterID.ToString() + "&ModuleID=9";

                var OptionTmp = (from op in db.LS_tblOnlineProcess
                                 join pom in db.SYS_tblProcessOnlineMaster on op.DMQuiTrinhID equals pom.DMQuiTrinhID
                                 where op.FunctionID == FunctionID
                                 select new { pom.OnlineProcessID, op.DMQuiTrinhID }).FirstOrDefault();

                Mail_SendMailCalendar mailSend = new Mail_SendMailCalendar();
                mailSend.ToEmpID = modelEdit.EmpID;
                mailSend.CcEmpID = ""; // cần cc thêm cho thằng trước nó
                mailSend.BccEmpID = "";
                mailSend.FunctionID = FunctionID;
                mailSend.OnlineProcessID = OptionTmp.OnlineProcessID;
                mailSend.SendTime = DateTime.Now;
                mailSend.MailTemplateID = 34;
                mailSend.Param = "FullName|" + emp.FullName + "|Approver|" + Approver.FullName + "|Link|" + Link;
                mailSend.isSent = false;
                mailSend.Actived = true;

                db.Entry(mailSend).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
