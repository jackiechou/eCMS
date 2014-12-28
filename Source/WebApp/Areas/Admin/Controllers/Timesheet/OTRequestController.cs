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
using Eagle.Common.Utilities;
using Eagle.Common.Helpers;
using System.Collections;
using Eagle.Repository.Mail;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers.Timesheet
{
    public class OTRequestController : BaseController
    {
        //
        // GET: /Admin/OTRequest/
        private int FunctionID = 478;
        public ActionResult Index( int? OTRecordID)
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            if (OTRecordID == null)
            {
                return View("../Timesheet/OTRequest/Index");
            }
            else
            {
                return View("../Timesheet/OTRequest/Edit", OTRecordID);
            }
        }
        public ActionResult List()
        {
            return View("../Timesheet/OTRequest/ListRecord");
        }
        public ActionResult _ListRecord(OTRequestViewModel model, int? ModuleID = 9)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            ModuleID = 9;

            OTRequestRepository _repository = new OTRequestRepository(db);
            List<OTRequestViewModel> sources =_repository.ListRecord(false, acc.UserName, ModuleID, CurrentEmpId, model).ToList();
            return PartialView("../Timesheet/OTRequest/_ListRecord", sources);
        }
        public ActionResult _Search()
        {
            ViewBag.Year = System.DateTime.Now.Year;
            return PartialView("../Timesheet/OTRequest/_InputSearch");
        }
        // load thông tin 
        public ActionResult _Create(int? iYear)
        {

            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            if (iYear == null)
                iYear = System.DateTime.Now.Year;
            CreateViewBagInformation(iYear);
            // thuc hien set cac chuc nang cho cac button
            if (acc.FAdm == 1 && (acc.EmpId == null || acc.EmpId == 0))
                CreateViewBag(false,false);
            else
                CreateViewBag();
            return PartialView("../Timesheet/OTRequest/_Create", null);
        }
        /// <summary>
        /// Xem thong tin chi tiet khi chuyen tu List record sang man hinh xem chi tiet de view va approve / unapprove
        /// </summary>
        /// <param name="OTRecordID"></param>
        /// <returns></returns>
        public ActionResult _ViewDetail(int OTRecordID)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            OTRequestRepository _repository = new OTRequestRepository(db);
            OTRequestViewModel modelEdit = _repository.FindEdit(OTRecordID);

            if (OTRecordID != 0 && modelEdit != null)
            {
                OTRequestViewModel model = _repository.OTDetail(modelEdit.EmpID, modelEdit.Year);
                ViewBag.Creater = model.Creater;
                ViewBag.OTLimit = model.OTLimit;
                ViewBag.OTAccumulated = model.OTAccumulated;
            }
            #region xet phan quyen edit va approve
            int FunctionID = 478;
            int status = modelEdit.Status;
            int empID = modelEdit.EmpID;
            var CurrentLevelApprove = modelEdit.LevelApprove;
            int ID = modelEdit.OTRecordID;

            bool DisabledSaveAndSend = true;
            bool DisabledApproveAndUnapprove = true;
            int maxLevelApprove = db.LS_tblOnlineProcess.Single(p => p.FunctionID == FunctionID).NoOfLevel;
            CommonRepository _commonRepository = new CommonRepository(db);
            _commonRepository.CheckPermission(status, CurrentEmpId, empID, FunctionID, CurrentLevelApprove, maxLevelApprove, ID, ref DisabledSaveAndSend, ref DisabledApproveAndUnapprove);
            CreateViewBag(DisabledSaveAndSend, DisabledApproveAndUnapprove);
            CreateViewBagListLevel(CurrentLevelApprove);
            #endregion
            return PartialView("../Timesheet/OTRequest/_Create", modelEdit);
        }
        public ActionResult _ViewListDetail(int OTRecordID)
        {

            OTRequestRepository _repository = new OTRequestRepository(db);
            List<OTRequestListViewModel> Sources = _repository.ListDetail(OTRecordID);
            return PartialView("../Timesheet/OTRequest/_List", Sources);
        }
        /// <summary>
        /// Load luoi rỗng khi pageload
        /// </summary>
        /// <returns></returns>
        public ActionResult _List()
        {
            List<OTRequestListViewModel> Sources = new List<OTRequestListViewModel>();

            OTRequestListViewModel add = new OTRequestListViewModel()
            {
                DateID = System.DateTime.Now
            };
            Sources.Add(add);
            return PartialView("../Timesheet/OTRequest/_List", Sources);
        }
        /// <summary>
        /// Add dong tren luoi
        /// </summary>
        /// <param name="listModel"></param>
        /// <returns></returns>
        public ActionResult _AddRow(List<OTRequestListViewModel>  listModel)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            listModel.Add(new OTRequestListViewModel()
            {
                strDateID = null
            });
            return PartialView("../Timesheet/OTRequest/_List", listModel);        
        }
        /// <summary>
        /// Xoa dong tren luoi
        /// </summary>
        /// <param name="listModel"></param>
        /// <returns></returns>
        public ActionResult _DeleteRow(List<OTRequestListViewModel> listModel)
        {
            AccountViewModel acc = CurrentAcc;
            if (acc != null)
            {
                return PartialView("../Timesheet/OTRequest/_List", listModel.Where(p => p.chkCheck == false).ToList());
            }
            else
            {
                return Content("timeout");
            }
        }
        /// <summary>
        /// TOIL and cash
        /// </summary>
        /// <param name="lstModel"></param>
        /// <param name="modelMaster"></param>
        /// <returns></returns>
        public ActionResult _FillToilAndCash(List<OTRequestListViewModel> lstModel, OTRequestViewModel modelMaster)
        {
            AccountViewModel acc = CurrentAcc;
            OTRequestRepository _repository = new OTRequestRepository(db);
            if (acc != null)
            {
                if (ModelState.IsValid)
                {
                    #region lay thong tin gio tinh OT dem
                    // lay thong tin gioi han tinh gio lam them OT la gio OT dem vi du từ 22h -> 06h hôm sau
                    var NightOT = (from p in db.SYS_tblParameter
                                   select new
                                   {
                                       NightOTFrom = p.NightOTFrom,
                                       NightOTTo = p.NightOTTo
                                   }).FirstOrDefault();
                    var now = DateTime.Now;
                    var From = new DateTime(now.Year, now.Month, now.Day, NightOT.NightOTFrom.Value.Hour, NightOT.NightOTFrom.Value.Minute, NightOT.NightOTFrom.Value.Second);
                    var To = new DateTime(now.Year, now.Month, now.Day, NightOT.NightOTTo.Value.Hour, NightOT.NightOTTo.Value.Minute, NightOT.NightOTTo.Value.Second);
                    #endregion

                    List<OTRequestListViewModel> lstModelUdpate = new List<OTRequestListViewModel>();
                    foreach (var item in lstModel)
                    {
                        // neu thang nào có check cho thì mới thực hiện FillCash
                        if (item.chkCheck)
                        {
                            var EmpIDCreater = CurrentEmpId;
                            // thuc hien kiem tra ngay truyen vao co phai la ngay lam viec hay khong hay la ngay le, t7, cn
                            decimal TotalHrsAM = 0;
                            decimal TotalHrsPM = 0;
                            decimal NightOTAM = 0;
                            decimal NightOTPM = 0;
                            decimal Rate100 = 0;
                            var modelFill = _repository.OTFillData(EmpIDCreater, item.DateID);
                            #region tong so gio OT trong ngay
                            // Total Hours tinh TimeIn - TimeOut (AM)
                            if (item.TimeInAM != null && item.TimeOutAM != null)
                            {
                                if (item.TimeOutAM <= item.TimeInAM)
                                {
                                    return _CreateError(modelMaster, "ErrorTimeInOut");
                                }
                                TotalHrsAM = (decimal)item.TimeOutAM.Value.Subtract(item.TimeInAM.Value).TotalHours;

                                // tinh  OT night AM
                                if (item.TimeInAM <= To && item.TimeOutAM <= To) // neu ca 2 gio vao va gio ra AM  deu nho hon 6h
                                    NightOTAM = (decimal)item.TimeOutAM.Value.Subtract(item.TimeInAM.Value).TotalHours;
                                else if (item.TimeInAM <= To && item.TimeOutAM > To)
                                    NightOTAM = (decimal)To.Subtract(item.TimeInAM.Value).TotalHours;
                            }
                            if (item.TimeInPM != null && item.TimeOutPM != null)
                            {
                                if (item.TimeOutPM <= item.TimeInPM)
                                {
                                    return _CreateError(modelMaster, "ErrorTimeInOut");
                                }
                                TotalHrsPM = (decimal)item.TimeOutPM.Value.Subtract(item.TimeInPM.Value).TotalHours;

                                // tinh  OT night PM
                                if (item.TimeInPM >= From && item.TimeOutPM >= From) // neu ca 2 gio vao va gio ra PM  deu lon hon 22h
                                    NightOTPM = (decimal)item.TimeOutPM.Value.Subtract(item.TimeInPM.Value).TotalHours;
                                else if (item.TimeInPM < From && item.TimeOutPM > From)
                                    NightOTPM = (decimal)item.TimeOutPM.Value.Subtract(From).TotalHours;

                            }
                            #endregion
                            if (modelFill.TypeDate == "") // 150%
                                Rate100 = (TotalHrsAM + TotalHrsPM) * 50 / 100;
                            else if (modelFill.TypeDate == "date-S") // 200%
                                Rate100 = TotalHrsAM + TotalHrsPM;
                            else if (modelFill.TypeDate == "date-H") // 300%
                                Rate100 = (TotalHrsAM + TotalHrsPM) * 2;

                            OTRequestListViewModel modelUpdate = new OTRequestListViewModel()
                            {
                                TypeDate = modelFill.TypeDate,
                                DateID = item.DateID,
                                TotalHours = TotalHrsAM + TotalHrsPM,
                                TimeInAM = item.TimeInAM,
                                TimeOutAM = item.TimeOutAM,
                                TimeInPM = item.TimeInPM,
                                TimeOutPM = item.TimeOutPM,
                                NightOT = NightOTAM + NightOTPM,
                                Rate150 = 0,
                                Rate200 = 0,
                                Rate300 = 0,
                                TOIL = TotalHrsAM + TotalHrsPM,
                                Rate100 = Rate100
                            };
                            lstModelUdpate.Add(modelUpdate);
                        }
                        else
                        {
                            lstModelUdpate.Add(item);
                        }
                    }
                    return PartialView("../Timesheet/OTRequest/_List", lstModelUdpate);
                }
                return Content(Eagle.Resource.LanguageResource.SystemError);
            }
            else
            {
                return Content("timeout");
            }
        }
        public ActionResult _FillCash(List<OTRequestListViewModel> lstModel, OTRequestViewModel modelMaster)
        {
            AccountViewModel acc = CurrentAcc;
            OTRequestRepository _repository = new OTRequestRepository(db);
            string errorMessage = "";
            if (acc != null)
            {
                if (ModelState.IsValid)
                {
                    #region lay thong tin gio tinh OT dem
                    // lay thong tin gioi han tinh gio lam them OT la gio OT dem vi du từ 22h -> 06h hôm sau
                    var NightOT = (from p in db.SYS_tblParameter
                                   select new
                                   {
                                       NightOTFrom = p.NightOTFrom,
                                       NightOTTo = p.NightOTTo
                                   }).FirstOrDefault();
                    var now = DateTime.Now;
                    var From = new DateTime(now.Year, now.Month, now.Day, NightOT.NightOTFrom.Value.Hour, NightOT.NightOTFrom.Value.Minute, NightOT.NightOTFrom.Value.Second);
                    var To = new DateTime(now.Year, now.Month, now.Day, NightOT.NightOTTo.Value.Hour, NightOT.NightOTTo.Value.Minute, NightOT.NightOTTo.Value.Second);
                    #endregion

                    List<OTRequestListViewModel> lstModelUdpate = new List<OTRequestListViewModel>();
                    foreach (var item in lstModel)
                    {

                        // neu thang nào có check cho thì mới thực hiện FillCash
                        if (item.chkCheck)
                        {
                            var EmpIDCreater = CurrentEmpId;
                            // thuc hien kiem tra ngay truyen vao co phai la ngay lam viec hay khong hay la ngay le, t7, cn
                            decimal TotalHrsAM = 0;
                            decimal TotalHrsPM = 0;
                            decimal NightOTAM = 0;
                            decimal NightOTPM = 0;
                            var modelFill = _repository.OTFillData(EmpIDCreater, item.DateID);
                            #region tong so gio OT trong ngay
                            // Total Hours tinh TimeIn - TimeOut (AM)
                            if (item.TimeInAM != null && item.TimeOutAM != null)
                            {
                                if (item.TimeOutAM <= item.TimeInAM)
                                    return _CreateError(modelMaster, "ErrorTimeInOut");
                                TotalHrsAM = (decimal)item.TimeOutAM.Value.Subtract(item.TimeInAM.Value).TotalHours;

                                // tinh  OT night AM
                                if (item.TimeInAM <= To && item.TimeOutAM <= To) // neu ca 2 gio vao va gio ra AM  deu nho hon 6h
                                    NightOTAM = (decimal)item.TimeOutAM.Value.Subtract(item.TimeInAM.Value).TotalHours;
                                else if (item.TimeInAM <= To && item.TimeOutAM > To)
                                    NightOTAM = (decimal)To.Subtract(item.TimeInAM.Value).TotalHours;
                            }

                            if (item.TimeInPM != null && item.TimeOutPM != null)
                            {
                                if (item.TimeOutPM <= item.TimeInPM)
                                    return _CreateError(modelMaster, "ErrorTimeInOut");

                                TotalHrsPM = (decimal)item.TimeOutPM.Value.Subtract(item.TimeInPM.Value).TotalHours;

                                // tinh  OT night PM
                                if (item.TimeInPM >= From && item.TimeOutPM >= From) // neu ca 2 gio vao va gio ra PM  deu lon hon 22h
                                    NightOTPM = (decimal)item.TimeOutPM.Value.Subtract(item.TimeInPM.Value).TotalHours;
                                else if (item.TimeInPM < From && item.TimeOutPM > From)
                                    NightOTPM = (decimal)item.TimeOutPM.Value.Subtract(From).TotalHours;

                            }
                            #endregion

                            OTRequestListViewModel modelUpdate = new OTRequestListViewModel()
                            {
                                TypeDate = modelFill.TypeDate,
                                DateID = item.DateID,
                                TotalHours = TotalHrsAM + TotalHrsPM,
                                TimeInAM = item.TimeInAM,
                                TimeOutAM = item.TimeOutAM,
                                TimeInPM = item.TimeInPM,
                                TimeOutPM = item.TimeOutPM,
                                NightOT = NightOTAM + NightOTPM,
                                Rate150 = (modelFill.TypeDate == "" ? TotalHrsAM + TotalHrsPM : 0),
                                Rate200 = (modelFill.TypeDate == "date-S" ? TotalHrsAM + TotalHrsPM : 0),
                                Rate300 = (modelFill.TypeDate == "date-H" ? TotalHrsAM + TotalHrsPM : 0)
                            };
                            lstModelUdpate.Add(modelUpdate);
                        }
                        else
                        {
                            lstModelUdpate.Add(item);
                        }
                    }

                    return PartialView("../Timesheet/OTRequest/_List", lstModelUdpate);
                }
                else {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var modelError in errors)
                    {
                        errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
                    }
                    return Content(errorMessage);
                }
            }
            else
            {
                return Content("timeout");
            }
        }
        public ActionResult Save(List<OTRequestListViewModel> lstModel, OTRequestViewModel modelMaster,string mode)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            decimal? totalHours = 0;
            decimal? totalHoursNight = 0;
            decimal? totalHoursHoliday = 0;
            if (acc != null)
            {
                int EmpID = CurrentEmpId.Value;

                OTRequestRepository _Repository = new OTRequestRepository(db);
                if (ModelState.IsValid)
                {
                    foreach (var item in lstModel)
                    {
                        totalHours +=  item.TotalHours;
                        totalHoursNight += item.NightOT;
                        totalHoursHoliday += item.HolidayOT;
                    }
                    if (totalHours == null || totalHours == 0)
                        return _CreateError(modelMaster, Eagle.Resource.LanguageResource.ErrorOTHours);

                    if (modelMaster.OTLimit - modelMaster.OTAccumulated < totalHours)
                        return _CreateError(modelMaster, Eagle.Resource.LanguageResource.ErrorOTLimit);

                    Timesheet_tblOTRecordMaster addMaster = new Timesheet_tblOTRecordMaster()
                    {
                        OTRecordID = modelMaster.OTRecordID,
                        EmpID = EmpID,
                        Month =  modelMaster.MonthYear.Month,
                        Year = modelMaster.MonthYear.Year,
                        TotalHours = totalHours.Value,
                        NightOT = totalHoursNight.Value,
                        HolidaysOT = totalHoursHoliday.Value,
                        Creater = acc.UserName,
                        CreateTime = DateTime.Now,
                    };
                    if (mode == "Save")
                    {
                        addMaster.Status = 0;
                        addMaster.LevelApprove = 0;
                        addMaster.isFirstComment = true;
                        addMaster.CurrentComment = modelMaster.Comment;
                    }
                    else { // Send For Approval
                        addMaster.Status = 1;
                        addMaster.LevelApprove = 1;
                        addMaster.isFirstComment = false;
                        addMaster.CurrentComment = "";

                        if (!string.IsNullOrEmpty(modelMaster.Comment))
                        {
                            string template = "<div class=\"row-fluid reset-margin-top-bottom\"><div class=\"span8 offset2 reset-margin-top-bottom\"><strong>{0}</strong>:<br />{1}</div><div class=\"span2 reset-margin-top-bottom\"><i>{2} <br /> {3}</i></div></div>";
                            addMaster.Comment = Server.HtmlEncode(string.Format(template, CurrentAcc.UserName, modelMaster.Comment, Eagle.Resource.LanguageResource.btnSendForApproval, DateTime.Now.ToString("dd/MM/yyy: hh:mm")));
                        }
                    }

                    addMaster.Timesheet_tblOTRecordDetail = new List<Timesheet_tblOTRecordDetail>();
                    foreach (var item in lstModel)
                    {
                        // neu co thuc hien khai bao OT voi so gio lon hon 0
                        if (item.TotalHours > 0)
                        {
                            Timesheet_tblOTRecordDetail addlist = new Timesheet_tblOTRecordDetail()
                            {
                                OTRecordID = item.OTRecordID,
                                DateID = item.DateID,
                                TimeInAM = item.TimeInAM,
                                TimeOutAM = item.TimeOutAM,
                                TimeInPM = item.TimeInPM,
                                TimeOutPM = item.TimeOutPM,
                                TotalHours = item.TotalHours,
                                NightOT = item.NightOT,
                                HolidayOT = item.HolidayOT,
                                Rate100 = item.Rate100,
                                Rate150 = item.Rate150,
                                Rate200 =item.Rate200,
                                Rate300 = item.Rate300,
                                TOIL = item.TOIL
                            };
                            addMaster.Timesheet_tblOTRecordDetail.Add(addlist);
                        }
                    }  // end foreach
                    if (_Repository.CheckExist( lstModel, modelMaster))
                    {
                        if (_Repository.Add(addMaster, out errorMessage))
                        {
                            if (mode != "Save")
                            {
                                SendMailToApprover(addMaster, "REQUEST");
                            }
                            return Content("success");
                        }
                        else
                            return _CreateError(modelMaster, errorMessage);
                    }
                    else
                    {
                        return _CreateError(modelMaster, Eagle.Resource.LanguageResource.DataIsExists);
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
        [HttpPost]
        public ActionResult Update(List<OTRequestListViewModel> lstModel, OTRequestViewModel modelMaster, string mode)
        {

            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            decimal? totalHours = 0;
            decimal? totalHoursNight = 0;
            decimal? totalHoursHoliday = 0;
            if (acc != null)
            {
                OTRequestRepository _Repository = new OTRequestRepository(db);
                if (ModelState.IsValid)
                {
                    // tinh lai tong so gio OT, Gio OT dem, Gio OT Holidays
                    foreach (var item in lstModel)
                    {
                        totalHours += item.TotalHours;
                        totalHoursNight += item.NightOT;
                        totalHoursHoliday += item.HolidayOT;
                    }
                    if (totalHours == null || totalHours == 0)
                        return _CreateError(modelMaster, Eagle.Resource.LanguageResource.ErrorOTHours);

                    if (modelMaster.OTLimit - modelMaster.OTAccumulated < totalHours)
                        return _CreateError(modelMaster, Eagle.Resource.LanguageResource.ErrorOTLimit);

                    // tim kiem du lieu de update
                    Timesheet_tblOTRecordMaster modelUpdate = _Repository.Find(modelMaster.OTRecordID);
                    modelUpdate.Month = modelMaster.MonthYear.Month;
                    modelUpdate.Year = modelMaster.MonthYear.Year;
                    modelUpdate.TotalHours = totalHours.Value;
                    modelUpdate.NightOT = totalHoursNight.Value;
                    modelUpdate.HolidaysOT = totalHoursHoliday.Value;
                    if (mode == "Save") // save
                    {
                        if (modelUpdate.isFirstComment == true)
                            modelUpdate.CurrentComment = modelMaster.Comment;
                        else
                            modelUpdate.CurrentComment = modelMaster.CurrentComment;
                    }
                    else // send for approval 
                    {
                        modelUpdate.Status = 1;
                        modelUpdate.LevelApprove = 1;
                        modelUpdate.isFirstComment = false;
                        modelUpdate.CurrentComment = "";
                        if (!string.IsNullOrEmpty(modelMaster.Comment))
                        {
                            if (modelUpdate.isFirstComment == true)
                            {
                                string template = "<div class='row-fluid reset-margin-top-bottom'><div class='span8 offset2 reset-margin-top-bottom'><strong>{0}</strong>:<br />{1}</div><div class='span2 reset-margin-top-bottom'><i>{2} <br /> {3}</i></div></div>";
                                modelUpdate.Comment = string.Format(template, CurrentAcc.UserName, modelMaster.Comment, Eagle.Resource.LanguageResource.btnSendForApproval, DateTime.Now.ToString("dd/MM/yyy: hh:mm"));
                            }
                            else
                            {                            
                                string template = "<div class='row-fluid reset-margin-top-bottom'><div class='span8 offset2 reset-margin-top-bottom'><strong>{0}</strong>:<br />{1}</div><div class='span2 reset-margin-top-bottom'><i>{2} <br /> {3}</i></div></div>";
                                modelUpdate.Comment += Server.HtmlEncode(string.Format(template, CurrentAcc.UserName, modelMaster.CurrentComment, Eagle.Resource.LanguageResource.btnSendForApproval, DateTime.Now.ToString("dd/MM/yyy: hh:mm")));
                            }
                        }
                    }



                    // lay danh sach cac ngay co OT neu co ngay nay thi update lai thong tin con khong co thi them vao danh sach
                    List<DateTime?> lst = lstModel.Where(p => p.TotalHours != 0).Select(p => p.DateID).ToList();
                    // danh sach du lieu can xoa
                    List<Timesheet_tblOTRecordDetail> lstDelete = new List<Timesheet_tblOTRecordDetail>();
                    foreach (var item in modelUpdate.Timesheet_tblOTRecordDetail)
                    {
                        //Có thì cập nhật
                        if (lst.Contains(item.DateID))
                        {
                            var modelDetailUpdate = lstModel.Single(p => p.DateID == item.DateID);
                            item.TimeInAM = modelDetailUpdate.TimeInAM;
                            item.TimeOutAM = modelDetailUpdate.TimeOutAM;
                            item.TimeInPM = modelDetailUpdate.TimeInPM;
                            item.TimeOutPM = modelDetailUpdate.TimeOutPM;
                            item.TotalHours = modelDetailUpdate.TotalHours;
                            item.NightOT = modelDetailUpdate.NightOT;
                            item.HolidayOT = modelDetailUpdate.HolidayOT;
                            item.Rate100 = modelDetailUpdate.Rate100;
                            item.Rate150 = modelDetailUpdate.Rate150;
                            item.Rate200 = modelDetailUpdate.Rate200;
                            item.Rate300 = modelDetailUpdate.Rate300;
                            item.TOIL = modelDetailUpdate.TOIL;
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
                    List<DateTime?> lstInDataBase = modelUpdate.Timesheet_tblOTRecordDetail.Select(p => p.DateID).ToList();
                    List<OTRequestListViewModel> lstAdd = lstModel.Where(p => p.TotalHours != 0 && !lstInDataBase.Contains(p.DateID)).ToList();

                    foreach (var item in lstAdd)
                    {
                        modelUpdate.Timesheet_tblOTRecordDetail.Add(new Timesheet_tblOTRecordDetail()
                        {
                            DateID = item.DateID,
                            OTRecordID = modelUpdate.OTRecordID,
                            TimeInAM = item.TimeInAM,
                            TimeOutAM = item.TimeOutAM,
                            TimeInPM = item.TimeInPM,
                            TimeOutPM = item.TimeOutPM,
                            TotalHours = item.TotalHours,
                            NightOT = item.NightOT,
                            HolidayOT = item.HolidayOT,
                            Rate100 = item.Rate100,
                            Rate150 = item.Rate150,
                            Rate200 = item.Rate200,
                            Rate300 = item.Rate300,
                            TOIL = item.TOIL
                        });
                    }

                    if (_Repository.CheckExistUpdate(lstModel, modelMaster))
                    {
                        if (_Repository.Update(modelUpdate, out errorMessage))
                        {
                            if (mode != "Save")
                            {
                                SendMailToApprover(modelUpdate, "REQUEST");
                            }
                            return Content("success");
                        }
                        else
                            return Content(errorMessage);
                    }
                    else
                    {
                        return _CreateError(modelMaster, Eagle.Resource.LanguageResource.DataIsExists);
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
        /// Delete  dòng tren lưới
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            OTRequestRepository _repository = new OTRequestRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        /// <summary>
        /// thuc hien approve va unapprove request
        /// </summary>
        /// <param name="model"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public ActionResult Approve(OTRequestViewModel model, string mode, int cboLevelApprove)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                OTRequestRepository _Repository = new OTRequestRepository(db);
                var modelEdit = _Repository.Find(model.OTRecordID);
                if (model.LevelApprove == modelEdit.LevelApprove)
                {
                    LS_tblOnlineProcess opModel = db.LS_tblOnlineProcess.Where(p => p.FunctionID == FunctionID).FirstOrDefault();
                    #region xet trang thai, leaveapprove khi approve va unapprove
                    if (mode == "approve")
                    {
                        if (model.LevelApprove < opModel.NoOfLevel)
                        {
                            modelEdit.LevelApprove += 1;
                        }
                        modelEdit.Status = model.LevelApprove * 2; // sẽ bằng 2,4,6,8 hoặc 10
                    }
                    else //if(mode == "unapprove")
                    {
                        // == true => back to prev process
                        // == false => back to first
                        modelEdit.Status = model.LevelApprove * 2 + 1; // sẽ bằng 3,5,7,9 hoặc 11
                        if (opModel.IsStart == 1)
                        {
                            modelEdit.LevelApprove -= 1;
                        }
                        if (opModel.IsStart == 2)
                        {
                            modelEdit.LevelApprove = cboLevelApprove;
                        }
                        else
                        {
                            modelEdit.LevelApprove = 0;
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
                            //Gửi mail cho thông báo người gửi quy trình và Cc & Bcc
                            //Gửi mail tới cấp tiếp theo
                            if (model.LevelApprove != opModel.NoOfLevel)
                            {
                                SendMailToApprover(modelEdit, "REQUEST");
                            }
                            // Gui email lai cho cap truoc khi da approve
                            SendMailToApprover(modelEdit, "APPROVE");
                        }
                        else
                        {
                            //Gửi mail cho thông báo người gửi quy trình và Cc & Bcc
                            //Gửi mail tới cấp trước nếu  được trả về cấp trước
                            SendMailToApprover(modelEdit, "UNAPPROVE");

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
        /// phan gui mail khi approve hay unapprove
        /// </summary>
        /// <param name="model"></param>
        private void SendMailToApprover(Timesheet_tblOTRecordMaster model, string Status)
        {
            // được gửi từ nhân viên
            var employee = db.HR_tblEmp.Where(p => p.EmpID ==  model.EmpID).FirstOrDefault();
            var from = "";
            var dearToFullName = "";
            var toEmail = "";
            var resultCC = (from o in db.LS_tblOnlineProcess
                            join cc in db.LS_tblOnlineProcessMailCc on o.DMQuiTrinhID equals cc.DMQuiTrinhID
                            join emp in db.HR_tblEmp on cc.EmpID equals emp.EmpID
                            where o.FunctionID == 478
                            select emp.Email).ToArray();
            var resultBcc = (from o in db.LS_tblOnlineProcess
                            join cc in db.LS_tblOnlineProcessMailBcc on o.DMQuiTrinhID equals cc.DMQuiTrinhID
                            join emp in db.HR_tblEmp on cc.EmpID equals emp.EmpID
                            where o.FunctionID == 478
                            select emp.Email).ToArray();
            var ccMail = string.Join(";", resultCC);
            var bccMail = string.Join(";", resultBcc);
            if (employee != null)
            {
                from = employee.LastName + " " + employee.FirstName;
                switch (model.LevelApprove)
                {
                    case 1:
                        // gửi tới nhân viên approve
                        var result = (from p in db.SYS_tblOnlineProcessEmp
                                      join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                      join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                      join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                      where p.ApproveLevel1 == true && 
                                      o.FunctionID == FunctionID
                                      select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (result != null)
                        {
                            dearToFullName = result.LastName + " " + result.FirstName;
                            toEmail = result.Email;
                        }
                        break;
                    case 2:
                        // gửi tới nhân viên approve
                        var result2 = (from p in db.SYS_tblOnlineProcessEmp
                                       join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                       join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                       join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                       where p.ApproveLevel2 == true &&

                                       o.FunctionID == FunctionID
                                       select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (result2 != null)
                        {
                            dearToFullName = result2.LastName + " " + result2.FirstName;
                            toEmail = result2.Email;
                        }
                        break;
                    case 3:
                        // gửi tới nhân viên approve
                        var result3 = (from p in db.SYS_tblOnlineProcessEmp
                                       join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                       join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                       join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                       where p.ApproveLevel3 == true &&
                                       o.FunctionID == FunctionID
                                       select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (result3 != null)
                        {
                            dearToFullName = result3.LastName + " " + result3.FirstName;
                            toEmail = result3.Email;
                        }
                        break;
                    case 4:
                        // gửi tới nhân viên approve
                        var result4 = (from p in db.SYS_tblOnlineProcessEmp
                                       join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                       join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                       join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                       where p.ApproveLevel4 == true &&
                                       o.FunctionID == FunctionID
                                       select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (result4 != null)
                        {
                            dearToFullName = result4.LastName + " " + result4.FirstName;
                            toEmail = result4.Email;
                        }
                        break;
                    case 5:
                        // gửi tới nhân viên approve
                        var result5 = (from p in db.SYS_tblOnlineProcessEmp
                                       join m in db.SYS_tblProcessOnlineMaster on p.OnlineProcessID equals m.OnlineProcessID
                                       join o in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals o.DMQuiTrinhID
                                       join e in db.HR_tblEmp on p.EmpID equals e.EmpID
                                       where p.ApproveLevel5 == true &&
                                       o.FunctionID == FunctionID
                                       select new { e.EmpID, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (result5 != null)
                        {
                            dearToFullName = result5.LastName + " " + result5.FirstName;
                            toEmail = result5.Email;
                            
                        }
                        break;
                }

            }
            if (Status == "REQUEST")
            {
                SendMailToApprover(toEmail, bccMail, ccMail, dearToFullName, from, model.OTRecordID);
            }
            else if (Status == "APPROVE")
            {
                // ten nguoi approve chinh la ten cua account login
                var fromnew = (from e in db.HR_tblEmp
                               where e.EmpID == CurrentAcc.EmpId
                        select e.LastName + " " + e.FirstName).FirstOrDefault();
                // gui lai cho nguoi tao OT Request
                var dearToFullNameNew = employee.LastName + " " + employee.FirstName;
                toEmail = employee.Email;
                SendMailApproved(toEmail, bccMail, ccMail, dearToFullNameNew, fromnew, model.OTRecordID);
            }
            else
            {
                // ten nguoi approve chinh la ten cua account login
                var fromnew = (from e in db.HR_tblEmp
                               where e.EmpID == CurrentAcc.EmpId
                               select e.LastName + " " + e.FirstName).FirstOrDefault();
                // gui lai cho nguoi tao OT Request
                var dearToFullNameNew = employee.LastName + " " + employee.FirstName;
                toEmail = employee.Email;
                SendMailUnapproved(toEmail, bccMail, ccMail, dearToFullNameNew, fromnew, model.OTRecordID);
            }
        }
        #region Send Mail Function
        private bool SendMailToApprover(string MailTo, string Bcc, string Cc, string DearFullName, string FromFullName, int OTRecordID)
        {
            string link = domainUrl + "Admin/OTRequest/Index?OTRecordID=" + OTRecordID.ToString() + "&ModuleID=9";
            Hashtable TemplateVariables = new Hashtable();
            TemplateVariables.Add("FullName", DearFullName);
            TemplateVariables.Add("From", FromFullName);
            TemplateVariables.Add("Link", link);

            int Mail_Template_Id = 1;

            bool result = MailTemplateRespository.SendGMailByTemplate(TemplateVariables, Mail_Template_Id, MailTo, Cc, Bcc);
            return result;
        }
        private bool SendMailApproved(string MailTo, string Bcc, string Cc, string DearFullName, string Approver, int PlanId)
        {
            string link = domainUrl + "Admin/OTRequest/List";
            Hashtable TemplateVariables = new Hashtable();
            TemplateVariables.Add("FullName", DearFullName);
            TemplateVariables.Add("Approver", Approver);
            TemplateVariables.Add("Link", link);

            int Mail_Template_Id = 5;

            bool result = MailTemplateRespository.SendGMailByTemplate(TemplateVariables, Mail_Template_Id, MailTo, Cc, Bcc);
            return result;
        }
        private bool SendMailUnapproved(string MailTo, string Bcc, string Cc, string DearFullName, string Approver, int PlanId)
        {
            string link = domainUrl + "Admin/OTRequest/List";
            Hashtable TemplateVariables = new Hashtable();
            TemplateVariables.Add("FullName", DearFullName);
            TemplateVariables.Add("Approver", Approver);
            TemplateVariables.Add("Link", link);

            int Mail_Template_Id = 3;

            bool result = MailTemplateRespository.SendGMailByTemplate(TemplateVariables, Mail_Template_Id, MailTo, Cc, Bcc);
            return result;
        }
        #endregion
        public void CreateViewBagInformation(int? iYear)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null){
                Response.Redirect("/");
            }
            else {
                OTRequestRepository _repository = new OTRequestRepository(db);


                OTRequestViewModel model = _repository.OTDetail(CurrentEmpId, iYear);

                ViewBag.Creater = model.Creater;
                ViewBag.OTLimit = model.OTLimit;
                ViewBag.OTAccumulated = model.OTAccumulated;
            }

        }
        private void CreateViewBag(bool DisabledSaveAndSend = true, bool DisabledApproveAndUnapprove = false)
        {
            ViewBag.EmpId = CurrentEmpId;
            //Ẩn hiện các nút Approve
            ViewBag.DisabledSaveAndSend = DisabledSaveAndSend ? "" : "disabled";
            ViewBag.DisabledApproveAndUnapprove = DisabledApproveAndUnapprove ? "" : "disabled";
        }
        private void CreateViewBagListLevel(int CurrentLevelApprove)
        {
            //int maxLevelApprove = db.LS_tblOnlineProcess.Single(p => p.FunctionID == FunctionID).NoOfLevel;
            
            var lst = (from o in db.LS_tblOnlineProcess
                       join m in db.SYS_tblProcessOnlineMaster on o.DMQuiTrinhID equals m.DMQuiTrinhID
                       where o.FunctionID == FunctionID
                       select new { m.StatusLevel1A, m.StatusLevel2A, m.StatusLevel3A, m.StatusLevel4A, m.StatusLevel5A }).FirstOrDefault();

            var lstLevel = new Dictionary<int,string>();
            switch (CurrentLevelApprove)
            {
                case 1:
                    lstLevel.Add(0,"Creater");
                   break;
                case 2:
                   lstLevel.Add(0, "Creater");
                   lstLevel.Add(1, lst.StatusLevel1A);
                   break;
                case 3:
                   lstLevel.Add(0, "Creater");
                   lstLevel.Add(1, lst.StatusLevel1A);
                   lstLevel.Add(2, lst.StatusLevel2A);
                   break;
                case 4:
                   lstLevel.Add(0, "Creater");
                   lstLevel.Add(1, lst.StatusLevel1A);
                   lstLevel.Add(2, lst.StatusLevel2A);
                   lstLevel.Add(3, lst.StatusLevel3A);
                   
                   break;
                case 5:
                   lstLevel.Add(0, "Creater");
                   lstLevel.Add(1, lst.StatusLevel1A);
                   lstLevel.Add(2, lst.StatusLevel2A);
                   lstLevel.Add(3, lst.StatusLevel3A);
                   lstLevel.Add(4, lst.StatusLevel4A);
                   
                   break;
            }
            ViewBag.cboLevelApprove = new SelectList(lstLevel, "key", "value");
            
        }
        /// <summary>
        /// tao ra cac thong bao khi co loi he thong
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _CreateError(OTRequestViewModel model, string ErrorMessage)
        {
            //if (model == null)
            //{
            //    model = new OTRequestViewModel();
            //}
            //ViewBag.DisplayErrorMessage = true;
            //ViewBag.CssClass = "alert alert-warning";
            //ViewBag.SortMessage = "Error";
            //ViewBag.Message = ErrorMessage;
            //CreateViewBag(model.Year);
            //return PartialView("../Timesheet/OTRequest/_Create", model);
            return Content(ErrorMessage);

        }
    }
}
