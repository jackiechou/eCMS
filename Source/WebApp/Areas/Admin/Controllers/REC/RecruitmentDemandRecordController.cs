using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class RecruitmentDemandRecordController : BaseController
    {
        //
        // GET: /Admin/RecruitmentDemandRecord/
        private const int FunctionID = 456;
        private const int ModuleID = 8;

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }

            return View("../REC/RecruitmentDemandRecord/Index");
        }
        public ActionResult _Create()
        {
            RecruitmentDemandViewModel model = new RecruitmentDemandViewModel();
            model.Year = DateTime.Now.Year;
            CreateStatusViewBag();
            return PartialView("../REC/RecruitmentDemandRecord/_Create", model);
        }

        private void CreateStatusViewBag()
        {
            int? EmpID = CurrentAcc.EmpId;

            var result = (from op in db.LS_tblOnlineProcess
                          join pcm in db.SYS_tblProcessOnlineMaster on op.DMQuiTrinhID equals pcm.DMQuiTrinhID
                          where op.FunctionID == FunctionID
                          select new
                          {
                              op.NameProcessOnline,
                              op.NoOfLevel,
                              op.IsStart,
                              pcm.StatusLevel1A,
                              pcm.StatusLevel1U,
                              pcm.StatusLevel2A,
                              pcm.StatusLevel2U,
                              pcm.StatusLevel3A,
                              pcm.StatusLevel3U,
                              pcm.StatusLevel4A,
                              pcm.StatusLevel4U,
                              pcm.StatusLevel5A,
                              pcm.StatusLevel5U
                          }).FirstOrDefault();

            //Tạo danh sách các cấp duyệt. mặc định luôn có 3 cấp đầu
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(-1, "-- " + Eagle.Resource.LanguageResource.All + " --");
            dic.Add(0, Eagle.Resource.LanguageResource.Save);
            dic.Add(1, Eagle.Resource.LanguageResource.btnSendForApproval);
            //Phụ thuộc vào mức tối đa duyệt cấp 1,2,3,4,5 để add thêm trạng thái duyệt
            if (result != null)
            {
                switch (result.NoOfLevel)
                {
                    case 1:
                        dic.Add(2, result.StatusLevel1A);
                        dic.Add(3, result.StatusLevel1U);
                        break;
                    case 2:
                        dic.Add(2, result.StatusLevel1A);
                        dic.Add(3, result.StatusLevel1U);
                        dic.Add(4, result.StatusLevel2A);
                        dic.Add(5, result.StatusLevel2U);
                        break;
                    case 3:
                        dic.Add(2, result.StatusLevel1A);
                        dic.Add(3, result.StatusLevel1U);
                        dic.Add(4, result.StatusLevel2A);
                        dic.Add(5, result.StatusLevel2U);
                        dic.Add(6, result.StatusLevel3A);
                        dic.Add(7, result.StatusLevel3U);
                        break;
                    case 4:
                        dic.Add(2, result.StatusLevel1A);
                        dic.Add(3, result.StatusLevel1U);
                        dic.Add(4, result.StatusLevel2A);
                        dic.Add(5, result.StatusLevel2U);
                        dic.Add(6, result.StatusLevel3A);
                        dic.Add(7, result.StatusLevel3U);
                        dic.Add(8, result.StatusLevel4A);
                        dic.Add(9, result.StatusLevel4U);
                        break;
                    case 5:
                        dic.Add(2, result.StatusLevel1A);
                        dic.Add(3, result.StatusLevel1U);
                        dic.Add(4, result.StatusLevel2A);
                        dic.Add(5, result.StatusLevel2U);
                        dic.Add(6, result.StatusLevel3A);
                        dic.Add(7, result.StatusLevel3U);
                        dic.Add(8, result.StatusLevel4A);
                        dic.Add(9, result.StatusLevel4U);
                        dic.Add(10, result.StatusLevel5A);
                        dic.Add(11, result.StatusLevel5U);
                        break;
                }

            }
            //Xác định giá trị chọn mặc định theo user:
            var userRoles = (from o in db.SYS_tblOnlineProcessEmp
                             join m in db.SYS_tblProcessOnlineMaster on o.OnlineProcessID equals m.OnlineProcessID
                             join ls in db.LS_tblOnlineProcess on m.DMQuiTrinhID equals ls.DMQuiTrinhID
                             where o.EmpID == EmpID &&
                                 ls.FunctionID == FunctionID &&
                                 (o.ApproveLevel1 == true || o.ApproveLevel2 == true || o.ApproveLevel3 == true || o.ApproveLevel4 == true || o.ApproveLevel5 == true)
                             select new
                             {
                                 o.ApproveLevel1
                                 ,
                                 o.ApproveLevel2
                                 ,
                                 o.ApproveLevel3
                                 ,
                                 o.ApproveLevel4
                                 ,
                                 o.ApproveLevel5
                             }).FirstOrDefault();
            if (userRoles != null)
            {
                //Duyệt cấp 1 => chọn send forapprover
                if (userRoles.ApproveLevel1 == true)
                {
                    ViewBag.StatusDemand = new SelectList(dic, "Key", "Value", 1);
                }
                else if (userRoles.ApproveLevel2 == true)
                {
                    //Được duyệt cấp 2 => chọn đã duyệt cấp 1
                    ViewBag.StatusDemand = new SelectList(dic, "Key", "Value", 2);
                }
                else if (userRoles.ApproveLevel3 == true)
                {
                    //Được duyệt cấp 3 => chọn đã duyệt cấp 2
                    ViewBag.StatusDemand = new SelectList(dic, "Key", "Value", 4);
                }
                else if (userRoles.ApproveLevel4 == true)
                {
                    //Được duyệt cấp 4 => chọn đã duyệt cấp 3
                    ViewBag.StatusDemand = new SelectList(dic, "Key", "Value", 6);
                }
                else if (userRoles.ApproveLevel5 == true)
                {
                    //Được duyệt cấp 5 => chọn đã duyệt cấp 4
                    ViewBag.StatusDemand = new SelectList(dic, "Key", "Value", 8);
                }
                else
                {
                    ViewBag.StatusDemand = new SelectList(dic, "Key", "Value");
                }

            }
            else
            {
                ViewBag.StatusDemand = new SelectList(dic, "Key", "Value");
            }

        }
        public ActionResult _List(int? Year, int? LSCompanyID, int? LSPositionID, int? StatusDemand)
        {
            if (CurrentAcc != null)
            {
                DemandRepository _repository = new DemandRepository(db);

                List<RecruitmentDemandViewModel> List = _repository.Search(Year, LSCompanyID, LSPositionID, StatusDemand, CurrentEmpId, ModuleID, CurrentAcc.UserName, LanguageId);
                return PartialView("../REC/RecruitmentDemandRecord/_List", List);
            }
            else
            {
                return PartialView("../REC/RecruitmentDemandRecord/_List");
            }
        }
        public ActionResult _Delete(int Id)
        {
            string errorMessage = "";
            DemandRepository _repository = new DemandRepository(db);
            if (_repository.Delete(Id, out errorMessage))
            {
                return Content("success");
            }
            else
            {
                return Content(errorMessage);
            }

        }
        
    }
}
