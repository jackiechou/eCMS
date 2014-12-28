using Eagle.Core;
using Eagle.Repository.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Model;

namespace Eagle.Repository
{
    public class OnlineProcessDetailRepository
    {
        public EntityDataContext context { get; set; }
        public OnlineProcessDetailRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public SYS_tblOnlineProcessEmp Find(int id)
        {
            return context.SYS_tblOnlineProcessEmp.Find(id);
        }
        public bool Update(int EmpID, int OnlineProcessID, int? CompanyId, List<int?> listCheck, bool ApproveLevel1, bool ApproveLevel2, bool ApproveLevel3, bool ApproveLevel4, bool ApproveLevel5, string Note, out string ErrorMessage)
        {
            try
            {
                //Tạo tất cả các SYS_tblOnlineProcessEmp từ dữ liệu truyền lên
                List<SYS_tblOnlineProcessEmp> lstAdd = new List<SYS_tblOnlineProcessEmp>();
                foreach (var departmentId in listCheck)
                {
                    SYS_tblOnlineProcessEmp modeladd = new SYS_tblOnlineProcessEmp()
                    {
                        OnlineProcessID = OnlineProcessID,
                        LSCompanyID = (int)departmentId,                                
                        EmpID = EmpID,
                        ApproveLevel1 = ApproveLevel1,
                        ApproveLevel2 = ApproveLevel2,
                        ApproveLevel3 = ApproveLevel3,
                        ApproveLevel4 = ApproveLevel4,
                        ApproveLevel5 = ApproveLevel5,
                        Note = Note
                    };
                    lstAdd.Add(modeladd);
                }

                //Kiểm tra có trùng thì 
                #region
                foreach (SYS_tblOnlineProcessEmp model in lstAdd)
                {
                    var Emp = context.SYS_tblOnlineProcessEmp.Where(
                        p => p.EmpID == model.EmpID &&
                            p.OnlineProcessID == model.OnlineProcessID &&
                            p.LSCompanyID == model.LSCompanyID 
                        )
                        .Select(p => new {FullName = p.HR_tblEmp.LastName + " " +  p.HR_tblEmp.FirstName, p.ApproveLevel1, p.ApproveLevel2, p.ApproveLevel3, p.ApproveLevel4, p.ApproveLevel5})
                        .FirstOrDefault();
                        if (Emp != null)
                        {
                            ErrorMessage = string.Format(Eagle.Resource.LanguageResource.Error_AddOnlineProcessDetail1, Emp.FullName);
                            if (Emp.ApproveLevel1 == true)
                            {
                                ErrorMessage += Eagle.Resource.LanguageResource.aLevel1 + ",";
                            }
                            if (Emp.ApproveLevel2 == true)
                            {
                                ErrorMessage += Eagle.Resource.LanguageResource.aLevel2 + ",";
                            }
                            if (Emp.ApproveLevel3 == true)
                            {
                                ErrorMessage += Eagle.Resource.LanguageResource.aLevel3 + ",";
                            }
                            if (Emp.ApproveLevel4 == true)
                            {
                                ErrorMessage += Eagle.Resource.LanguageResource.aLevel4 + ",";
                            }
                            if (Emp.ApproveLevel5 == true)
                            {
                                ErrorMessage += Eagle.Resource.LanguageResource.aLevel5 + ".";
                            }

                            //ErrorMessage += " Tại phòng: " + Emp. + ".";
                            ErrorMessage += "<br />";
                            ErrorMessage += Eagle.Resource.LanguageResource.Error_AddOnlineProcessDetail2;
                            
                            return false;
                        }
            
                    //Kiểm tra bước 2

                    List<SYS_tblOnlineProcessEmp> checkModelList = context.SYS_tblOnlineProcessEmp
                        .Where(p => p.EmpID != model.EmpID &&
                                              p.OnlineProcessID == model.OnlineProcessID &&
                                              p.LSCompanyID == model.LSCompanyID &&
                                              (
                                              (model.ApproveLevel1 == true && p.ApproveLevel1 == model.ApproveLevel1) ||
                                               (model.ApproveLevel2 == true && p.ApproveLevel2 == model.ApproveLevel2) ||
                                               (model.ApproveLevel3 == true && p.ApproveLevel3 == model.ApproveLevel3) ||
                                               (model.ApproveLevel4 == true && p.ApproveLevel4 == model.ApproveLevel4) ||
                                               (model.ApproveLevel5 == true && p.ApproveLevel5 == model.ApproveLevel5)
                                               )
                                            )
                        .ToList();
                    if (checkModelList != null && checkModelList.Count > 0)
                    {
                        ErrorMessage = "";
                        foreach (SYS_tblOnlineProcessEmp checkModel in checkModelList)
                        {                           
                            var emloyee = EmployeeRepository.Find(checkModel.EmpID);
                            ErrorMessage += string.Format(Eagle.Resource.LanguageResource.Error_AddOnlineProcessDetail1, emloyee.LastName + " " + emloyee.FirstName);
                            if (checkModel.ApproveLevel1 == true)
                            {
                                ErrorMessage += Eagle.Resource.LanguageResource.aLevel1 + ",";
                            }
                            if (checkModel.ApproveLevel2 == true)
                            {
                                ErrorMessage += Eagle.Resource.LanguageResource.aLevel2 + ",";
                            }
                            if (checkModel.ApproveLevel3 == true)
                            {
                                ErrorMessage += Eagle.Resource.LanguageResource.aLevel3 + ",";
                            }
                            if (checkModel.ApproveLevel4 == true)
                            {
                                ErrorMessage += Eagle.Resource.LanguageResource.aLevel4 + ",";
                            }
                            if (checkModel.ApproveLevel5 == true)
                            {
                                ErrorMessage += Eagle.Resource.LanguageResource.aLevel5 + ".";
                            }
                            ErrorMessage += "<br />";
                        }

                        return false;
                    }
                }
                #endregion
                //Delete tất cả các cài đặt theo 3 thông số: EmpID, OnlineProcessId, companyId
                #region
                var list = context.SYS_tblOnlineProcessEmp
                                .Where(p => p.EmpID == EmpID && p.OnlineProcessID == OnlineProcessID && p.LSCompanyID == CompanyId)
                                .ToList();
                foreach (var model in list)
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                }
                #endregion
                //Thêm vào db
                #region
                foreach (SYS_tblOnlineProcessEmp model in lstAdd)
                {
                    if (model.ApproveLevel1 == true ||model.ApproveLevel2 == true ||model.ApproveLevel3 == true ||model.ApproveLevel4 == true ||model.ApproveLevel5 == true)
                    {
                        context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    }
                }
                context.SaveChanges();
                #endregion
                ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public List<OnlineProcessEmpViewModel> GetAll()
        {

            var result = context.SYS_tblOnlineProcessEmp
                        .Select(p => new OnlineProcessEmpViewModel() {
                          OnlineProcessEmpID = p.OnlineProcessEmpID,
                          OnlineProcessID = p.OnlineProcessID,
                          OnlineProcessName = p.SYS_tblProcessOnlineMaster.LS_tblOnlineProcess.NameProcessOnline,
                          LSCompanyID = p.LSCompanyID,
                          CompanyName = p.LS_tblCompany.Name,
                          EmpID = p.EmpID,
                          strEmpName = p.HR_tblEmp.LastName + " " +p.HR_tblEmp.FirstName, 
                          ApproveLevel1 = p.ApproveLevel1,
                          ApproveLevel2 = p.ApproveLevel2,
                          ApproveLevel3 = p.ApproveLevel3,
                          ApproveLevel4 = p.ApproveLevel4,
                          ApproveLevel5 = p.ApproveLevel5
                        })
                        .OrderBy(p => p.OnlineProcessName)
                        .ThenBy(p => p.CompanyName)
                        .ThenByDescending(p => p.ApproveLevel1)
                        .ThenByDescending(p => p.ApproveLevel2)
                        .ThenByDescending(p => p.ApproveLevel3)
                        .ThenByDescending(p => p.ApproveLevel4)
                        .ThenByDescending(p => p.ApproveLevel5)
                        ;

            return result.ToList();
        }

        public bool Delete(int id, out string ErrorMessage)
        {
            try
            {
                var model = Find(id);
                if (model != null)
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
                ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public OnlineProcessEmpViewModel FindEdit(int id)
        {
            var ret = context.SYS_tblOnlineProcessEmp
                .Where(p => p.OnlineProcessEmpID == id)
                        .Select(p => new OnlineProcessEmpViewModel()
                        {
                            OnlineProcessEmpID = p.OnlineProcessEmpID,
                            OnlineProcessID = p.OnlineProcessID,
                            OnlineProcessName = p.SYS_tblProcessOnlineMaster.LS_tblOnlineProcess.NameProcessOnline,
                            LSCompanyID = p.LSCompanyID,
                            CompanyName = p.LS_tblCompany.Name,
                            EmpID = p.EmpID,
                            strEmpName = p.HR_tblEmp.LastName + " " + p.HR_tblEmp.FirstName,
                            ApproveLevel1 = p.ApproveLevel1,
                            ApproveLevel2 = p.ApproveLevel2,
                            ApproveLevel3 = p.ApproveLevel3,
                            ApproveLevel4 = p.ApproveLevel4,
                            ApproveLevel5 = p.ApproveLevel5
                        }).FirstOrDefault();

            if (ret != null && ret.LSCompanyID != 0)
            {
                string lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == ret.LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                if (!string.IsNullOrEmpty(lineage))
                {
                    string tmp = lineage.Substring(0, lineage.IndexOf('.'));
                    try
                    {
                        ret.RootLevel = Convert.ToInt32(tmp);
                    }
                    catch{}
                }
            }
            return ret;
        }

        public bool Update(SYS_tblOnlineProcessEmp modelEdit,SYS_tblOnlineProcessEmp oldModel, out string errorMessage)
        {
            try
            {
                //check
                bool check = false;
                errorMessage = "";
                
                if (oldModel.ApproveLevel1 == false && modelEdit.ApproveLevel1 == true)
                {
                    //xét Approve Level 1
                    var checkmodel = (from p in context.SYS_tblOnlineProcessEmp
                                     join emp in context.HR_tblEmp on p.EmpID equals emp.EmpID
                                     where p.ApproveLevel1 == true &&
                                             p.OnlineProcessID == oldModel.OnlineProcessID &&
                                             p.LSCompanyID == oldModel.LSCompanyID &&
                                             //p.LSLevel1ID == oldModel.LSLevel1ID &&
                                             //p.LSLevel2ID == oldModel.LSLevel2ID &&
                                             p.EmpID != oldModel.EmpID
                                      select new { FullName = emp.LastName + " " + emp.FirstName }).FirstOrDefault();
                    //đã tồn tại
                    if (checkmodel != null)
                    {
                        errorMessage += string.Format(Eagle.Resource.LanguageResource.Error_UpdateOnlineProcessDetail, checkmodel.FullName, Eagle.Resource.LanguageResource.aLevel1 + " <br />");
                        check = true;
                    }
                }
                if (oldModel.ApproveLevel2 == false && modelEdit.ApproveLevel2 == true)
                {
                    //xét Approve Level 2
                    var checkmodel = (from p in context.SYS_tblOnlineProcessEmp
                                      join emp in context.HR_tblEmp on p.EmpID equals emp.EmpID
                                      where p.ApproveLevel2 == true &&
                                              p.OnlineProcessID == oldModel.OnlineProcessID &&
                                              p.LSCompanyID == oldModel.LSCompanyID &&
                                              //p.LSLevel1ID == oldModel.LSLevel1ID &&
                                              //p.LSLevel2ID == oldModel.LSLevel2ID &&
                                              p.EmpID != oldModel.EmpID
                                      select new { FullName = emp.LastName + " " + emp.FirstName }).FirstOrDefault();
                    //đã tồn tại
                    if (checkmodel != null)
                    {
                        errorMessage += string.Format(Eagle.Resource.LanguageResource.Error_UpdateOnlineProcessDetail, checkmodel.FullName, Eagle.Resource.LanguageResource.aLevel2 + " <br />");
                        check = true;
                    }
                }
                if (oldModel.ApproveLevel3 == false && modelEdit.ApproveLevel3 == true)
                {
                    //xét Approve Level 3
                    var checkmodel = (from p in context.SYS_tblOnlineProcessEmp
                                      join emp in context.HR_tblEmp on p.EmpID equals emp.EmpID
                                      where p.ApproveLevel3 == true &&
                                              p.OnlineProcessID == oldModel.OnlineProcessID &&
                                              p.LSCompanyID == oldModel.LSCompanyID &&
                                              //p.LSLevel1ID == oldModel.LSLevel1ID &&
                                              //p.LSLevel2ID == oldModel.LSLevel2ID &&
                                              p.EmpID != oldModel.EmpID
                                      select new { FullName = emp.LastName + " " + emp.FirstName }).FirstOrDefault();
                    //đã tồn tại
                    if (checkmodel != null)
                    {
                        errorMessage += string.Format(Eagle.Resource.LanguageResource.Error_UpdateOnlineProcessDetail, checkmodel.FullName, Eagle.Resource.LanguageResource.aLevel3 + " <br />");
                        check = true;
                    }
                }
                if (oldModel.ApproveLevel4 == false && modelEdit.ApproveLevel4 == true)
                {
                    //xét Approve Level 4
                    var checkmodel = (from p in context.SYS_tblOnlineProcessEmp
                                      join emp in context.HR_tblEmp on p.EmpID equals emp.EmpID
                                      where p.ApproveLevel4 == true &&
                                              p.OnlineProcessID == oldModel.OnlineProcessID &&
                                              p.LSCompanyID == oldModel.LSCompanyID &&
                                              //p.LSLevel1ID == oldModel.LSLevel1ID &&
                                              //p.LSLevel2ID == oldModel.LSLevel2ID &&
                                              p.EmpID != oldModel.EmpID
                                      select new { FullName = emp.LastName + " " + emp.FirstName }).FirstOrDefault();
                    //đã tồn tại
                    if (checkmodel != null)
                    {
                        errorMessage += string.Format(Eagle.Resource.LanguageResource.Error_UpdateOnlineProcessDetail, checkmodel.FullName, Eagle.Resource.LanguageResource.aLevel4 + " <br />");
                        check = true;
                    }
                }
                if (oldModel.ApproveLevel5 == false && modelEdit.ApproveLevel5 == true)
                {
                    //xét Approve Level 5
                    var checkmodel = (from p in context.SYS_tblOnlineProcessEmp
                                      join emp in context.HR_tblEmp on p.EmpID equals emp.EmpID
                                      where p.ApproveLevel5 == true &&
                                              p.OnlineProcessID == oldModel.OnlineProcessID &&
                                              p.LSCompanyID == oldModel.LSCompanyID &&
                                              //p.LSLevel1ID == oldModel.LSLevel1ID &&
                                              //p.LSLevel2ID == oldModel.LSLevel2ID &&
                                              p.EmpID != oldModel.EmpID
                                      select new { FullName = emp.LastName + " " + emp.FirstName }).FirstOrDefault();
                    //đã tồn tại
                    if (checkmodel != null)
                    {
                        errorMessage += string.Format(Eagle.Resource.LanguageResource.Error_UpdateOnlineProcessDetail, checkmodel.FullName, Eagle.Resource.LanguageResource.aLevel5 +" <br />");
                        check = true;
                    }
                }
                // return nếu đã tồn tại 1 ai đó có quyền duyệt cấp bất kỳ
                if (check)
                {
                    return false;
                }

                //Cập nhật nếu có chọn, xóa bỏ nếu không chọn, 
                if (modelEdit.ApproveLevel1 == true || modelEdit.ApproveLevel2 == true || modelEdit.ApproveLevel3 == true || modelEdit.ApproveLevel4 == true || modelEdit.ApproveLevel5 == true)
                {
                    context.Entry(modelEdit).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    context.Entry(modelEdit).State = System.Data.Entity.EntityState.Deleted;
                }
                context.SaveChanges();
                errorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
