using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Repository;
using Eagle.Core;
using Eagle.Repository.HR;
using System.Collections;
using Eagle.Repository.Mail;
using AutoMapper;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class RecruitmentPlanController : BaseController
    {
        private const int FunctionID = 456;
        #region Send Mail Function
        //Mail này gửi lên cấp tiếp theo
        //Được gửi theo mỗi lần duyệt
        //Nếu là cấp cuối cùng thì không gửi
        private void SendMailToApprover(REC_tblPlan model)
        {
            // được gửi từ nhân viên
            var employee = db.HR_tblEmp.Find(model.EmpIDPlan);
            var from = "";
            var dearToFullName = "";
            var toEmail = "";
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
                                      where p.ApproveLevel1 == true && p.LSCompanyID == employee.LSCompanyID &&
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
                                            p.LSCompanyID == employee.LSCompanyID &&
                                      
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
                                             p.LSCompanyID == employee.LSCompanyID &&
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
                                             p.LSCompanyID == employee.LSCompanyID &&
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
                                             p.LSCompanyID == employee.LSCompanyID &&
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

            SendMailToApprover(toEmail, "", "", dearToFullName, from, model.PlanID);
        }

        //Mail này gửi về người tạo để thông báo đa được duyệt đến cấp mấy
        //Được gửi theo mỗi lần duyệt
        private void SendMailApproved(REC_tblPlan model)
        {
            // được gửi từ nhân viên
            var employee = db.HR_tblEmp.Find(model.EmpIDPlan);
            var DearFullName = "";
            var toEmail = "";
            var Approver = "";
     
            string ccMail = "";
            string bccMail = "";
            MailConfigRepository lib = new MailConfigRepository(db);
            lib.GetCcAndBcc(out ccMail, out bccMail, FunctionID);

            if (employee != null)
            {
                DearFullName = employee.LastName + " " + employee.FirstName;
                toEmail = employee.Email;
                Approver = CurrentAcc.FullName;
            }

            SendMailApproved(toEmail, ccMail, bccMail, DearFullName, Approver, model.PlanID);
        }
        private void SendMailUnapproved(REC_tblPlan model)
        {
            string ccMail = "";
            string bccMail = "";
            MailConfigRepository lib = new MailConfigRepository(db);
            lib.GetCcAndBcc(out ccMail, out bccMail, FunctionID);
            var employee = db.HR_tblEmp.Find(model.EmpIDPlan);
            //gửi mail cho người tạo
            try
            {
                SendMailUnapproved(employee.Email, ccMail, bccMail, employee.LastName + " " + employee.FirstName, CurrentAcc.FullName, model.PlanID);
            }
            catch
            {}
            try
            {
                //Gửi cho người được trả về nếu người được trả về không phải là người tạo
                if (model.LevelApprove != 0)
                {
                    HR_tblEmp Employee = new HR_tblEmp();
                    int OnlineProcessID = db.LS_tblOnlineProcess.Where(p => p.FunctionID == FunctionID).FirstOrDefault().SYS_tblProcessOnlineMaster.FirstOrDefault().OnlineProcessID;
                    switch (model.LevelApprove)
                    {
                        case 1:
                            Employee = (from opm in db.SYS_tblOnlineProcessEmp
                                        join em in db.HR_tblEmp on opm.EmpID equals em.EmpID
                                        where opm.OnlineProcessID == OnlineProcessID &&
                                        opm.LSCompanyID == employee.LSCompanyID && opm.ApproveLevel1 == true
                                        select em).FirstOrDefault();
                            break;
                        case 2:
                            Employee = (from opm in db.SYS_tblOnlineProcessEmp
                                        join em in db.HR_tblEmp on opm.EmpID equals em.EmpID
                                        where opm.OnlineProcessID == OnlineProcessID &&
                                        opm.LSCompanyID == employee.LSCompanyID && opm.ApproveLevel2 == true
                                        select em).FirstOrDefault();
                            break;
                        case 3:
                            Employee = (from opm in db.SYS_tblOnlineProcessEmp
                                        join em in db.HR_tblEmp on opm.EmpID equals em.EmpID
                                        where opm.OnlineProcessID == OnlineProcessID &&
                                        opm.LSCompanyID == employee.LSCompanyID && opm.ApproveLevel3 == true
                                        select em).FirstOrDefault();
                            break;
                        case 4:
                            Employee = (from opm in db.SYS_tblOnlineProcessEmp
                                        join em in db.HR_tblEmp on opm.EmpID equals em.EmpID
                                        where opm.OnlineProcessID == OnlineProcessID &&
                                        opm.LSCompanyID == employee.LSCompanyID && opm.ApproveLevel4 == true
                                        select em).FirstOrDefault();
                            break;
                    }

                    if (Employee != null)
                    {
                        SendMailUnapproved(Employee.Email, "", "", Employee.LastName + " " + Employee.FirstName, CurrentAcc.FullName, model.PlanID);
                    }
                }
                    
            }
            catch
            {

            }

 
        }
        //#endregion

        //#region Send Mail Function
        //Mail này gửi lên cấp tiếp theo
        //Được gửi theo mỗi lần duyệt
        //Nếu là cấp cuối cùng thì không gửi
        private bool SendMailToApprover(string MailTo, string Cc, string Bcc, string DearFullName, string FromFullName, int PlanId)
        {
            string link = domainUrl + "Admin/RecruitmentPlan/Index?PlanId="+PlanId.ToString()+"&ModuleID=8";
            Hashtable TemplateVariables = new Hashtable();
            TemplateVariables.Add("FullName", DearFullName);
            TemplateVariables.Add("From", FromFullName);
            TemplateVariables.Add("Link", link);

            int Mail_Template_Id = 12;

            bool result = MailTemplateRespository.SendGMailByTemplate(TemplateVariables, Mail_Template_Id, MailTo, Cc, Bcc);
            return result;
        }

        //Mail này gửi về người tạo để thông báo đa được duyệt đến cấp mấy
        //Được gửi theo mỗi lần duyệt
        private bool SendMailApproved(string MailTo,  string Cc, string Bcc, string DearFullName, string Approver, int PlanId)
        {
            string link = domainUrl + "Admin/RecruitmentPlan/Index?PlanId=" + PlanId.ToString() + "&ModuleID=8";
            Hashtable TemplateVariables = new Hashtable();
            TemplateVariables.Add("FullName", DearFullName);
            TemplateVariables.Add("Approver", Approver);
            TemplateVariables.Add("Link", link);

            int Mail_Template_Id = 13;

            bool result = MailTemplateRespository.SendGMailByTemplate(TemplateVariables, Mail_Template_Id, MailTo, Cc, Bcc);
            return result;
        }

        //Mail này dùng để gửi khi không duyệt
        //Gửi cho cấp người tạo và cấp được trả về
        //Nếu cấp được trả về chính là người tạo => chỉ gửi 1 lần
        private bool SendMailUnapproved(string MailTo, string Cc, string Bcc, string DearFullName, string Approver, int PlanId)
        {
            string link = domainUrl + "Admin/RecruitmentPlan/Index?PlanId=" + PlanId.ToString() + "&ModuleID=8";
            Hashtable TemplateVariables = new Hashtable();
            TemplateVariables.Add("FullName", DearFullName);
            TemplateVariables.Add("Approver", Approver);
            TemplateVariables.Add("Link", link);

            int Mail_Template_Id = 14;

            bool result = MailTemplateRespository.SendGMailByTemplate(TemplateVariables, Mail_Template_Id, MailTo, Cc, Bcc);
            return result;
        }
        #endregion
        #region Get
        public ActionResult Index(int? PlanId)
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            if (PlanId == null)
            {
                return View("../Business/HumanResources/REC/RecruitmentPlan/Index");
            }
            else
            {
                return View("../Business/HumanResources/REC/RecruitmentPlan/Edit", PlanId);
            }
        }
        
        public ActionResult _Create()
        {
            
            RecruitmentPlanViewModel model = new RecruitmentPlanViewModel();

            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc != null)
            {
                model.EmpIDPlanName = acc.UserName;
            }
            model.RecruitedQuantity = 0;
            CreateViewBag();
            
            return PartialView("../Business/HumanResources/REC/RecruitmentPlan/_Create", model);
        }
        public ActionResult _Edit(int? PlanId)
        {

            RecruitmentPlanRepository _repository = new RecruitmentPlanRepository(db);
            RecruitmentPlanViewModel model = _repository.FindEdit(PlanId);

            int statusPlan = model.StatusPlan;
            int empIDPlan = model.EmpIDPlan;
            var CurrentLevelApprove = model.LevelApprove;
            int planID = model.PlanID;

            bool DisabledSaveAndSend = true;
            bool DisabledApproveAndUnapprove = true;
            int maxLevelApprove = db.LS_tblOnlineProcess.Single(p => p.FunctionID == FunctionID).NoOfLevel;
            CommonRepository _commonRepository = new CommonRepository(db);
            _commonRepository.CheckPermission(statusPlan, CurrentEmpId, empIDPlan, FunctionID, CurrentLevelApprove, maxLevelApprove, planID, ref DisabledSaveAndSend, ref DisabledApproveAndUnapprove, model.StatusProcess);

            LS_tblOnlineProcess opModel = db.LS_tblOnlineProcess.Where(p => p.FunctionID == FunctionID).FirstOrDefault();
            if (opModel != null)
            {
                ViewBag.IsStart = opModel.IsStart;
            }
            

            CreateViewBag(model.LSCompanyID, model.PlanID,model.LSPositionID, true, DisabledSaveAndSend, DisabledApproveAndUnapprove);
            return PartialView("../Business/HumanResources/REC/RecruitmentPlan/_Create", model);
        }
        public PartialViewResult _popupUnapprove(int LevelApprove)
        {
            // trả về người tạo và các cấp dưới "LevelApprove"
            //VD: nếu là cấp 3 trả về người tạo và cấp 1 2
            //    nếu là cấp 1 chỉ trả về người tạo
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(0, Eagle.Resource.LanguageResource.CreateUser);
            var LS_tblOnlineProcessModel = db.LS_tblOnlineProcess.Where(p => p.FunctionID == FunctionID).FirstOrDefault();
            if (LS_tblOnlineProcessModel != null)
            {
                var SYS_tblProcessOnlineMasterModel = LS_tblOnlineProcessModel.SYS_tblProcessOnlineMaster.FirstOrDefault();
                if (SYS_tblProcessOnlineMasterModel != null)
                {
                    switch (LevelApprove)
                    {
                        case 2: //Add cấp 1
                            dic.Add(1, SYS_tblProcessOnlineMasterModel.StatusLevel1A);
                            break;
                        case 3: //Add cấp 1,2
                            dic.Add(1, SYS_tblProcessOnlineMasterModel.StatusLevel1A);
                            dic.Add(2, SYS_tblProcessOnlineMasterModel.StatusLevel2A);
                            break;
                        case 4: //Add cấp 1,2,3
                            dic.Add(1, SYS_tblProcessOnlineMasterModel.StatusLevel1A);
                            dic.Add(2, SYS_tblProcessOnlineMasterModel.StatusLevel2A);
                            dic.Add(3, SYS_tblProcessOnlineMasterModel.StatusLevel3A);
                            break;
                        case 5: //Add cấp 1,2,3,4
                            dic.Add(1, SYS_tblProcessOnlineMasterModel.StatusLevel1A);
                            dic.Add(2, SYS_tblProcessOnlineMasterModel.StatusLevel2A);
                            dic.Add(3, SYS_tblProcessOnlineMasterModel.StatusLevel3A);
                            dic.Add(4, SYS_tblProcessOnlineMasterModel.StatusLevel4A);
                            break;
                    }
                }
            }
            ViewBag.ReturnLevelApproveList = new SelectList(dic, "Key", "Value");
            return PartialView("../Business/HumanResources/REC/RecruitmentPlan/_popupUnapprove");
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(RecruitmentPlanViewModel model, string mode, string[] sourceSelected)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";

            if (acc != null)
            {
                string template = Eagle.Resource.LanguageResource.htmlComment;
                RecruitmentPlanRepository _repository = new RecruitmentPlanRepository(db);
                if (ModelState.IsValid)
                {
                    if (acc.EmpId == null)
                    {
                        errorMessage = Eagle.Resource.LanguageResource.SystemAccountNotWorking;
                        return _Error(model, errorMessage, sourceSelected);
                    }
                    REC_tblPlan modelAdd = new REC_tblPlan();
                    Mapper.CreateMap<RecruitmentPlanViewModel, REC_tblPlan>();
                    modelAdd = Mapper.Map<REC_tblPlan>(model);
                    modelAdd.EmpIDPlan = acc.EmpId.Value;
                    modelAdd.CreateDate = DateTime.Now;
                   

                    if (mode == "save")
                    {
                        modelAdd.StatusPlan = 0;
                        modelAdd.LevelApprove = 0;
                    }
                    else if (mode == "sendforapproval")
                    {
                        modelAdd.StatusPlan = 1;
                        modelAdd.LevelApprove = 1;

                        REC_tblPlanComment commentModel = new REC_tblPlanComment()
                        {
                            EmpID = CurrentAcc.EmpId,
                            Time = DateTime.Now,
                            StatusName = Eagle.Resource.LanguageResource.btnSendForApproval
                        };
                        // Gán comment
                        if (string.IsNullOrEmpty(modelAdd.Comment))
                        {
                            //Nếu không điền vào ô comment thì gán mặc định
                            commentModel.Comment = Eagle.Resource.LanguageResource.btnSendForApproval;
                        }
                        else
                        {
                            commentModel.Comment = Server.HtmlEncode(modelAdd.Comment);
                        }
                        modelAdd.REC_tblPlanComment.Add(commentModel);
                        //Sau khi gửi thì gán comment lại về rỗng
                        modelAdd.Comment = "";
                       
                    }
                    modelAdd.StatusProcess = 1;

                    // thêm các thông tin nguồn tuyển dụng
                    UpdateSourecesForPlan(sourceSelected, modelAdd);

                    if (_repository.Add(modelAdd, out errorMessage))
                    {
                        if (mode == "sendforapproval")
                        {
                            SendMailToApprover(modelAdd);
                        }
                        return Content("success");
                    }
                    else
                    {
                        return _Error(model, errorMessage, sourceSelected);
                    }
                }
            }
            else
            {
                errorMessage = Eagle.Resource.LanguageResource.TimeOutError;
                return _Error(model, errorMessage, sourceSelected);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "<br />" + modelError.ErrorMessage;
            }
            return _Error(model, errorMessage, sourceSelected);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(RecruitmentPlanViewModel model, string mode, string[] sourceSelected)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                string template = Eagle.Resource.LanguageResource.htmlComment;
                RecruitmentPlanRepository _repository = new RecruitmentPlanRepository(db);
                if (ModelState.IsValid)
                {
                    var modelEdit = _repository.Find(model.PlanID);
                    

                    if (mode == "save")
                    {
                        modelEdit.StatusPlan = 0;
                        modelEdit.LevelApprove = 0;                        
                    }
                    else if (mode == "sendforapproval")
                    {
                        modelEdit.StatusPlan = 1;
                        modelEdit.LevelApprove = 1;
                    }
                    modelEdit.Year = model.Year;
                    modelEdit.LSCompanyID = model.LSCompanyID;
                    modelEdit.LSPositionID = model.LSPositionID;
                    modelEdit.PlanQuantity = model.PlanQuantity;
                    
                    

                    UpdateSourecesForPlan(sourceSelected, modelEdit);


                    if (mode == "sendforapproval")
                    {
                        REC_tblPlanComment commentModel = new REC_tblPlanComment()
                        {
                            EmpID = CurrentAcc.EmpId,
                            Time = DateTime.Now,
                            StatusName = Eagle.Resource.LanguageResource.btnSendForApproval
                        };
                        // Gán comment
                        if (string.IsNullOrEmpty(model.Comment))
                        {
                            //Nếu không điền vào ô comment thì gán mặc định
                            commentModel.Comment = Eagle.Resource.LanguageResource.btnSendForApproval;
                        }
                        else
                        {
                            commentModel.Comment = Server.HtmlEncode(model.Comment);
                        }
                        modelEdit.REC_tblPlanComment.Add(commentModel);
                        //Sau khi gửi thì gán comment lại về rỗng
                        modelEdit.Comment = "";
                    }


                    if (_repository.Update(modelEdit, out errorMessage))
                    {
                        if (mode == "sendforapproval")
                        {
                            SendMailToApprover(modelEdit);
                        }
                        return Content("success");
                    }
                    else
                    {
                        return Content(errorMessage);
                    }
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var modelError in errors)
                {
                    errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
                }
                return Content(errorMessage);
            }
            else
            {
                return Content("Time out error!");
            }
        }

        //approve unapprove
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Approve(RecruitmentPlanViewModel model, string mode, int IsStart, string[] sourceSelected, int? ReturnLevelApprove)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                string template = Eagle.Resource.LanguageResource.htmlComment;
                RecruitmentPlanRepository _repository = new RecruitmentPlanRepository(db);
                var modelEdit = _repository.Find(model.PlanID);
                if (model.LevelApprove == modelEdit.LevelApprove)
                {
                    LS_tblOnlineProcess opModel = db.LS_tblOnlineProcess.Where(p => p.FunctionID == FunctionID).FirstOrDefault();
                    if (mode == "approve")
                    {
                        if (model.LevelApprove < opModel.NoOfLevel)
                        {
                            modelEdit.LevelApprove += 1;
                        }
                        modelEdit.StatusPlan = model.LevelApprove * 2; // sẽ bằng 2,4,6,8 hoặc 10
                    }
                    else //if(mode == "unapprove")
                    {
                        modelEdit.StatusPlan = model.LevelApprove * 2 + 1; // sẽ bằng 3,5,7,9 hoặc 11
                        if (opModel.IsStart == 1)// trở về cấp trước
                        {
                            modelEdit.LevelApprove -= 1;
                        } 
                        if (opModel.IsStart == 0)//trở về cấp đầu
                        {
                            modelEdit.LevelApprove = 0;
                        }
                        else if (opModel.IsStart == 2)//trở về cấp được chọn
                        {
                            if (ReturnLevelApprove != null)
                            {
                                modelEdit.LevelApprove = ReturnLevelApprove.Value;
                            }
                        }
                    }
                   
                    string statusName = "";
                    var ProcessOnlineMaster = db.LS_tblOnlineProcess.Where(p => p.FunctionID == FunctionID).Select(p => p.SYS_tblProcessOnlineMaster).FirstOrDefault();
                    if (ProcessOnlineMaster != null)
                    {
                        var statusModel = ProcessOnlineMaster.FirstOrDefault();
                        if (statusModel != null)
                        {
                            switch (modelEdit.StatusPlan)
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

                    REC_tblPlanComment comment = new REC_tblPlanComment()
                    {
                        Time = DateTime.Now,
                        EmpID = CurrentAcc.EmpId,
                        StatusName = statusName
                    };
                    if (!string.IsNullOrEmpty(model.Comment))
                    {
                        comment.Comment = Server.HtmlEncode(model.Comment);
                    }
                    else
                    {
                        if ((mode == "approve"))
                        {
                            comment.Comment = Server.HtmlEncode("<i>" + Eagle.Resource.LanguageResource.Approve + "</i>");
                        }
                        else
                        {
                            comment.Comment = Server.HtmlEncode("<i>" + Eagle.Resource.LanguageResource.btnUnapprove + "</i>");
                        }

                    }

                    modelEdit.REC_tblPlanComment.Add(comment);

                    if (_repository.Update(modelEdit, out errorMessage))
                    {
                        //Gửi mail
                        var onlineprocess = db.LS_tblOnlineProcess.Where(p => p.FunctionID == FunctionID).FirstOrDefault();
                        // nếu thành công
                        
                        if (mode == "approve")
                        {
                            int MaxOfLevelApprove = 0;
                            
                            if (onlineprocess != null)
                            {
                                MaxOfLevelApprove = onlineprocess.NoOfLevel;
                            }
                            //Gửi mail cho thông báo người gửi quy trình và Cc & Bcc
                            //Gửi mail tới cấp tiếp theo
                            //Nếu là cấp cuối cùng thì không cần gửi nữa
                            if ((modelEdit.LevelApprove * 2) != modelEdit.StatusPlan)
                            {
                                SendMailToApprover(modelEdit);
                            }

                            //Gửi mail cho người tạo
                            SendMailApproved(modelEdit);
                        }
                        else// nếu unapprove
                        {
                            //Gửi mail cho thông báo người gửi quy trình và Cc & Bcc
                            //Gửi mail tới cấp trước nếu plan này được trả về cấp trước
                            SendMailUnapproved(modelEdit);
                           
                        }
                        return Content("success");
                    }
                    else
                    {
                        return Content(errorMessage);
                    }
                }
                else
                {
                    //Đã duyệt rồi thì thôi không được duyệt nữa
                    return Content(Eagle.Resource.LanguageResource.OnlineProcessApproved);
                }
            }
            else {
                return Content(Eagle.Resource.LanguageResource.TimeOutError);
            }
        }
        private void UpdateSourecesForPlan(string[] sourceSelected, REC_tblPlan modelAdd)
        {
            // nếu post sourceSelected lên == null
            if (sourceSelected == null)
            {
                modelAdd.LS_tblRecruitmentSource = new List<LS_tblRecruitmentSource>();
                return;
            }
            // có dữ liệu
            //selectedPagesHS : danh sách ID được chọn từ giao diện
            var selectedPagesHS = new HashSet<string>(sourceSelected);
            //roles: là tất cả các ID trong database (đã liên kết nhiều nhiều)
            var roles = new HashSet<int>
                (modelAdd.LS_tblRecruitmentSource.Select(c => c.LSRecruitmentSourceID));
            
            //for each tất cả các phần tử
            foreach (var source in db.LS_tblRecruitmentSource)
            {
                // nếu mà được chọn có trong database
                if (selectedPagesHS.Contains(source.LSRecruitmentSourceID.ToString()))
                {
                    //chưa có thì add
                    if (!roles.Contains(source.LSRecruitmentSourceID))
                    {
                        modelAdd.LS_tblRecruitmentSource.Add(source);
                    }
                }
                else
                {
                    //không có this xóa
                    if (roles.Contains(source.LSRecruitmentSourceID))
                    {
                        modelAdd.LS_tblRecruitmentSource.Remove(source);
                    }
                }
            }
        }
        #endregion
        #region common

        public ActionResult GetQuota(int Year, int LSCompanyID, int LSPosition)
        {
            QuotaRecruitmentRepository _repository = new QuotaRecruitmentRepository(db);
            int quota = _repository.GetQuotaRecruitmentListByYear(Year, LSCompanyID, LSPosition);
            return Content(quota.ToString());
        }
        
        public ActionResult _Error(RecruitmentPlanViewModel model, string ErrorMessage,string[] sourceSelected)
        {
            if (model == null)
            {
                model = new RecruitmentPlanViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

           CreateViewBag(model.LSCompanyID, model.PlanID);
           CreateSourecesViewBag(sourceSelected);

            return PartialView("../Business/HumanResources/REC/RecruitmentPlan/_Create", model);
        }
        private void CreateViewBag(int? LSCompanyID = null, int? PlanId = null,int? LSPositionID = null, bool showStatus = false, bool DisabledSaveAndSend = true, bool DisabledApproveAndUnapprove = false)
        {
            ViewBag.ShowStatus = showStatus ? "" : "Hidden";
            ViewBag.EmpId = CurrentEmpId;
            SelectData(PlanId);
            Dictionary<int, string> list = db.LS_tblPosition.ToDictionary(p => p.LSPositionID,p => p.Name);

            ViewBag.LSPositionID = new SelectList(list, "Key", "Value", LSPositionID);
            //Ẩn hiện các nút Approve
            ViewBag.DisabledSaveAndSend = DisabledSaveAndSend ? "" : "disabled";
            ViewBag.DisabledApproveAndUnapprove = DisabledApproveAndUnapprove ? "" : "disabled";

            if (PlanId != null)
            {
                var comment = (from cmd in db.REC_tblPlanComment
                               join emp in db.HR_tblEmp on cmd.EmpID equals emp.EmpID into empTmpList
                               from employee in empTmpList.DefaultIfEmpty()
                               where cmd.PlanID == PlanId
                               select new CommentViewModel()
                               {
                                   Comment = cmd.Comment,
                                   StatusName = cmd.StatusName,
                                   Time = cmd.Time,
                                   EmpComment = employee.LastName + " " + employee.FirstName,
                                   AvatarId = employee.FileId
                               }).ToList();
                ViewBag.CommentList = comment;
            }

        }
        private void SelectData(int? PlanId = null)
        {
            REC_tblPlan model = null;
            if (PlanId != null)
            {
                RecruitmentPlanRepository _repository = new RecruitmentPlanRepository(db);
                model = _repository.Find(PlanId);
            }
            List<LS_tblRecruitmentSource> allRecruitmentSource = db.LS_tblRecruitmentSource.OrderBy(p => p.Rank).ToList();
            var viewModel = new List<RecruitmentSourceSelectedViewModel>();
            if (model != null)
            {
                var RecruitmentSources = new HashSet<int>(model.LS_tblRecruitmentSource.Select(c => c.LSRecruitmentSourceID));
                foreach (var r in allRecruitmentSource)
                {
                    viewModel.Add(new RecruitmentSourceSelectedViewModel
                    {
                        LSRecruitmentSourceID = r.LSRecruitmentSourceID,
                        LSRecruitmentSourceName = r.Name,
                        isSelected = RecruitmentSources.Contains(r.LSRecruitmentSourceID)
                    });
                }
            }
            else
            {
                foreach (var r in allRecruitmentSource)
                {
                    viewModel.Add(new RecruitmentSourceSelectedViewModel
                    {
                        LSRecruitmentSourceID = r.LSRecruitmentSourceID,
                        LSRecruitmentSourceName = r.Name,
                        isSelected = false
                    });
                }
            }

            ViewBag.RecruitmentSources = viewModel;
        }

        private void CreateSourecesViewBag(string[] sourceSelected)
        {
            var RecruitmentSource = new List<RecruitmentSourceSelectedViewModel>();
            var selectedPagesHS = new HashSet<string>();
            if (sourceSelected != null)
            {
                selectedPagesHS = new HashSet<string>(sourceSelected);
            }

            foreach (var source in db.LS_tblRecruitmentSource)
            {
                RecruitmentSourceSelectedViewModel model = new RecruitmentSourceSelectedViewModel()
                {
                    LSRecruitmentSourceID = source.LSRecruitmentSourceID,
                    LSRecruitmentSourceName = source.Name,
                    isSelected = selectedPagesHS.Contains(source.LSRecruitmentSourceID.ToString())
                };
                RecruitmentSource.Add(model);
            }

            ViewBag.RecruitmentSources = RecruitmentSource;
        }
        #endregion
    }
}
