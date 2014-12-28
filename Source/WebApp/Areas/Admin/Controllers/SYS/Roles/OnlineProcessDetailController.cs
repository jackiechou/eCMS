using Eagle.Core;
using Eagle.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class OnlineProcessDetailController : BaseController
    {
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }

            #region Load Online Process List into  OnlineProcess
            OnlineProcessRepository _repository = new OnlineProcessRepository(db);
            List<CompanyEntity> list = new List<CompanyEntity>();
            list.Add(new CompanyEntity(){Id = null, Name = "-- " +  Eagle.Resource.LanguageResource.Select + " --"} );
            var listdb =  db.SYS_tblProcessOnlineMaster.Select(p => new { p.OnlineProcessID, p.LS_tblOnlineProcess.NameProcessOnline }).ToDictionary(p => p.OnlineProcessID, t => t.NameProcessOnline);
            foreach (var item in listdb)
            {
                list.Add(new CompanyEntity() { Id = item.Key, Name = item.Value });
            }
            ViewBag.OnlineProcessID = new SelectList(list, "Id", "Name");
            #endregion
            return View("../SYS/OnlineProcessDetail/Index");
        }
        public ActionResult _List()
        {
            OnlineProcessDetailRepository _repository = new OnlineProcessDetailRepository(db);

            return PartialView("../SYS/OnlineProcessDetail/_List", _repository.GetAll());
        }
       
        public ActionResult UpdateOnlineProcessDetail(int OnlineProcessEmpID, bool ApproveLevel1, bool ApproveLevel2, bool ApproveLevel3, bool ApproveLevel4, bool ApproveLevel5)
        {
            try
            {
                SYS_tblOnlineProcessEmp model = db.SYS_tblOnlineProcessEmp.Where(p => p.OnlineProcessEmpID == OnlineProcessEmpID).FirstOrDefault();
                SYS_tblOnlineProcessEmp oldModel = new SYS_tblOnlineProcessEmp()
                {
                    OnlineProcessID = model.OnlineProcessID,
                    EmpID = model.EmpID,
                    LSCompanyID = model.LSCompanyID,
                    ApproveLevel1 = model.ApproveLevel1,
                    ApproveLevel2 = model.ApproveLevel2,
                    ApproveLevel3 = model.ApproveLevel3,
                    ApproveLevel4 = model.ApproveLevel4,
                    ApproveLevel5 = model.ApproveLevel5
                };
                if (model != null)
                {

                    model.ApproveLevel1 = ApproveLevel1;
                    model.ApproveLevel2 = ApproveLevel2;
                    model.ApproveLevel3 = ApproveLevel3;
                    model.ApproveLevel4 = ApproveLevel4;
                    model.ApproveLevel5 = ApproveLevel5;

                    OnlineProcessDetailRepository _repository = new OnlineProcessDetailRepository(db);
                    string errorMessage = "";
                    if (_repository.Update(model, oldModel, out errorMessage))
                    {
                        return Content("success");
                    }
                    else
                    {
                        // Không thành công báo lỗi
                        return Content(errorMessage);
                    }
                }
                else
                {
                    return Content(Eagle.Resource.LanguageResource.SystemError);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public ActionResult SaveOnlineProcessDetail(int EmpID, int OnlineProcessID, int? CompanyId, string[] DepartmentChecked, bool ApproveLevel1, bool ApproveLevel2, bool ApproveLevel3, bool ApproveLevel4, bool ApproveLevel5, string Note)
        {
            //bool check = false;
            //Kiểm tra công ty
            if (CompanyId == null)
            {
                return Content(Eagle.Resource.LanguageResource.Error_Company);
                //check = true;
            }

            if (DepartmentChecked != null)
            {
                DepartmentChecked = DepartmentChecked.Distinct().ToArray();
            }
            else
            {
                return Content(Eagle.Resource.LanguageResource.PleaseChooseTmp, Eagle.Resource.LanguageResource.NearLastCompanyDefine);
            }

            List<int?> listCheck = new List<int?>();
            foreach (var item in DepartmentChecked)
            {
                int tmp = 0;
                if (int.TryParse(item, out tmp))
                {
                    listCheck.Add(tmp);
                }
            }

            
            //if (!(ApproveLevel1 || ApproveLevel2 || ApproveLevel3 || ApproveLevel4 || ApproveLevel5))
            //{
            //    return Content("Vui lòng phân quyền Duyệt cho nhân viên này!");
            //    //check = true;
            //}

            OnlineProcessDetailRepository _repository = new OnlineProcessDetailRepository(db);

            if (listCheck != null && listCheck.Count > 0)
            {
                string errorMessage = "";
                if (_repository.Update(EmpID, OnlineProcessID, CompanyId, listCheck, ApproveLevel1, ApproveLevel2, ApproveLevel3, ApproveLevel4, ApproveLevel5, Note, out errorMessage))
                {
                    return Content("success");
                }
                else
                {
                    // Không thành công báo lỗi
                    return Content(errorMessage);
                }
            }
            return Content("success");
        }

        [HttpPost]
        public ActionResult _Delete(int id)
        {
            OnlineProcessDetailRepository _repository = new OnlineProcessDetailRepository(db);
            string errorMessage = string.Empty;
            if (_repository.Delete(id, out errorMessage))
            {
                return Content("success");
            }
            else
            {
                return Content(errorMessage);
            }
            
        }
        public ActionResult _Edit(int id)
        {
            OnlineProcessDetailRepository _repository = new OnlineProcessDetailRepository(db);
            var model = _repository.FindEdit(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSettings(int CompanyID, int? CurrentDepartmentChecked)
        {
            if (CurrentDepartmentChecked == null)
            {
                ViewBag.strDisabled = "";
            }
            else
            {
                ViewBag.strDisabled = "disabled";
            }
            List<int?> allDefine = db.LS_tblCompanyDefine.Select(p => p.Parent).ToList();
            var lastDefine = db.LS_tblCompanyDefine.Where(p => !allDefine.Contains(p.LSCompanyDefineID)).Select(p => p.LSCompanyDefineID).FirstOrDefault();
            //Select LSCompany Get from View
            var LsCompany = db.LS_tblCompany.Where(p => p.LSCompanyID == CompanyID).FirstOrDefault();

            //Get all childen of LsCompany where level like lastDefine
            List<OnlineProcessEmpMiniViewModel> result = (from company in db.LS_tblCompany
                                                          where company.Lineage.Contains(LsCompany.Lineage) &&
                                                          company.LSCompanyDefineID == lastDefine
                                                          select new OnlineProcessEmpMiniViewModel() { 
                                                                      LSCompanyID = company.LSCompanyID, 
                                                                      LSCompanyName = (LanguageId == 124 ? company.Name : company.VNName),
                                                                      Checked = (CurrentDepartmentChecked != null && company.LSCompanyID == CurrentDepartmentChecked)
                                                          })
                                                        .ToList();
            return PartialView("../SYS/OnlineProcessDetail/_DepartmentList", result);
        }

        /*Description: Hàm này dùng để lấy xem Quy trình này có tổng bao nhiêu cấp duyệt
         * Return string {"0","1","2","3","4","5"}
         */
        #region
        public ActionResult GetTotalLevelApprove(int? id)
        {
            if (id == null)
            {
                return Content("0");
            }
            else
            {
                try
                {
                    var result = db.SYS_tblProcessOnlineMaster.Where(p => p.OnlineProcessID == id).Select(p => p.LS_tblOnlineProcess.NoOfLevel).FirstOrDefault();
                    return Content(result.ToString());
                }
                catch 
                {
                    return Content("0");
                }
            }
        }
        #endregion
    }
}
