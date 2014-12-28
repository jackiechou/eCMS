using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class InsuranceInformationRepository
    {
        public EntityDataContext context { get; set; }
        public InsuranceInformationRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<InsuranceInformationSearchViewModel> GetAll(InsuranceInformationSearchViewModel modelSearch, int LanguageId = 10001)
        {
            var result = from p in context.SI_tblInsuranceInformation
                         join emp in context.HR_tblEmp on p.EmpID equals emp.EmpID
                         where (modelSearch.FullName == null || (emp.LastName + " " + emp.FirstName).Contains(modelSearch.FullName)) &&
                         (modelSearch.HIBook == null ||  p.HIBook.Contains(modelSearch.HIBook)) &&
                          (modelSearch.SIBook == null || p.SIBook.Contains(modelSearch.SIBook)) &&
                          (modelSearch.FromDate == null || p.StartDateSI > modelSearch.FromDate) &&
                          (modelSearch.ToDate == null || p.StartDateSI < modelSearch.ToDate) 
                         select new InsuranceInformationSearchViewModel()
                         {
                             EmpID = p.EmpID,
                             HIBook = p.HIBook,
                             SIBook = p.SIBook,
                             FullName = emp.LastName + " " + emp.FirstName,
                             StartDateSI = p.StartDateSI
                         };
            return result.ToList();
        }

        public bool Add(SI_tblInsuranceInformation model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                ////Kiểm tra xem Quy trình này đã được thiết lập chưa
                //if (CodeIsExists(model.LSHospitalCode))
                //{
                //    ErrorMessage = Eagle.Resource.LanguageResource.CodeIsExists;
                //    return false;
                //}

                //nếu chưa có mới thêm, không thì cập nhật
                int check = context.SI_tblInsuranceInformation.Where(p => p.EmpID == model.EmpID).Select(p => p.EmpID).FirstOrDefault();
                if (check == 0)
                {
                    model.LSHospitalIDNew = model.LSHospitalIDOriginal;
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                }
                else
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                }
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        //private bool CodeIsExists(string LSHospitalCode, int? LSHospitalID = null)
        //{
        //    if (LSHospitalID == null)
        //    {
        //        var OnlineProcess = context
        //                    .SI_tblInsuranceInformation
        //                    .FirstOrDefault(p => p.LSHospitalCode == LSHospitalCode);
        //        return OnlineProcess != null;
        //    }
        //    else
        //    {
        //        var OnlineProcess = context
        //                    .SI_tblInsuranceInformation
        //                    .FirstOrDefault(p => p.LSHospitalCode == LSHospitalCode &&
        //                        p.LSHospitalID != LSHospitalID);
        //        return OnlineProcess != null;
        //    }
        //}

        public SI_tblInsuranceInformation Find(int? id)
        {
            return context.SI_tblInsuranceInformation.Find(id);
        }

        public SI_tblInsuranceInformation FindByEmpId(int? EmpId)
        {
            return context.SI_tblInsuranceInformation.Where(p => p.EmpID == EmpId).FirstOrDefault();

        }

        public InsuranceInformationViewModel FindEdit(int? EmpID, int LanguageId = 10001)
        {
            var result = (from p in context.SI_tblInsuranceInformation
                          join sip in context.LS_tblProvince on p.LSProvinceIDIssuedSI equals sip.LSProvinceID into tmpList1
                          from sip in tmpList1.DefaultIfEmpty()
                        
                          join hip in context.LS_tblProvince on p.LSProvinceIDIssuedHI equals hip.LSProvinceID into tmpList2
                          from hip in tmpList2.DefaultIfEmpty()

                          join ohospital in context.LS_tblHospital on p.LSHospitalIDOriginal equals ohospital.LSHospitalID into tmpList3
                          from ohospital in tmpList3.DefaultIfEmpty()

                          join nhospital in context.LS_tblHospital on p.LSHospitalIDNew equals nhospital.LSHospitalID into tmpList4
                          from nhospital in tmpList4.DefaultIfEmpty()
                          where p.EmpID == EmpID
                          select new InsuranceInformationViewModel()
                          {
                              InsuranceID = p.InsuranceID,
                              EmpID = p.EmpID,
                              SIBook = p.SIBook,
                              StartDateSI = p.StartDateSI,
                              IssuedDateSIBook = p.IssuedDateSIBook,
                              JoinDateAtCompany = p.JoinDateAtCompany,
                              LSProvinceIDIssuedSI = p.LSProvinceIDIssuedSI,
                              HIBook = p.HIBook,
                              LSHospitalIDOriginal = p.LSHospitalIDOriginal,
                              IssuedDateHIBook = p.IssuedDateHIBook,
                              LSProvinceIDIssuedHI = p.LSProvinceIDIssuedHI,
                              StartDateHI = p.StartDateHI,
                              LSHospitalIDNew = p.LSHospitalIDNew,
                              ReturnDateHI = p.ReturnDateHI,
                              Note = p.Note,

                              JoinDateAtCompanyAlowNull = p.JoinDateAtCompany,
                              LSProvinceIDIssuedSIName = (LanguageId == 124) ? sip.Name : sip.VNName,
                              LSHospitalIDOriginalName = (LanguageId == 124) ? ohospital.Name : ohospital.VNName,
                              LSProvinceIDIssuedHIName = (LanguageId == 124) ? hip.Name : hip.VNName,
                              LSHospitalIDNewName = (LanguageId == 124) ? nhospital.Name : nhospital.VNName,

                          }).FirstOrDefault();
            return result;
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
            catch( Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

        }


        public bool Update(SI_tblInsuranceInformation ModelUpdate)
        {
            try
            {
                context.Entry(ModelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
