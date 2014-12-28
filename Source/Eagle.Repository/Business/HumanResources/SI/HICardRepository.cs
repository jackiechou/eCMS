using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;
using Eagle.Model.HR;

namespace Eagle.Repository
{
    public class HICardRepository
    {
        public EntityDataContext context { get; set; }
        public HICardRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<HICardViewModel> GetBy(int? EmpID, int LanguageId = 10001)
        {
            var result = from p in context.SI_tblHICard
                         join h in context.LS_tblHospital on p.LSHospitalID equals h.LSHospitalID into tmpList1
                         from h in tmpList1.DefaultIfEmpty()
                         where p.EmpID == EmpID
                         select new HICardViewModel
                         {
                             HICardID = p.HICardID,
                             EmpID = p.EmpID,
                             FromDate = p.FromDate,
                             ToDate = p.ToDate,
                             ChangePlace = p.ChangePlace,
                             LSHospitalID = p.LSHospitalID,
                             LSHospitalName = (LanguageId == 124) ? h.Name : h.VNName,
                             Note = p.Note
                         };
            return result.ToList();
        }

        public bool Add(SI_tblHICard model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public SI_tblHICard Find(int id)
        {
            return context.SI_tblHICard.Find(id);
        }
        public HICardViewModel FindEdit(int? HICardID, int LanguageId)
        {
            var result = from p in context.SI_tblHICard
                         join h in context.LS_tblHospital on p.LSHospitalID equals h.LSHospitalID into tmpList1
                         from h in tmpList1.DefaultIfEmpty()
                         where p.HICardID == HICardID
                         select new HICardViewModel
                         {
                             HICardID = p.HICardID,
                             EmpID = p.EmpID,
                             FromDate = p.FromDate,
                             ToDate = p.ToDate,
                             FromDateAllowNull = p.FromDate,
                             ToDateAllowNull = p.ToDate,
                             ChangePlace = p.ChangePlace,
                             LSHospitalID = p.LSHospitalID,
                             LSHospitalName = (LanguageId == 124) ? h.Name : h.VNName,
                             Note = p.Note
                         };
            return result.FirstOrDefault();
        }

        public bool Update(SI_tblHICard model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
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

        public bool ExtendAll(DateTime FromDate, DateTime ToDate, out string ErrorMessage)
        {
            try
            {
                //1. Lấy toàn bộ nhân viên đã có dữ liệu thông tin thẻ
                var employee = (from ii in context.SI_tblInsuranceInformation
                               where ii.LSHospitalIDNew != null
                                select new { ii.EmpID, ii.LSHospitalIDNew }).ToList();
                //2. Gia hạn thẻ bằng cách insert vào table SI_tblHICard
                if (employee != null && employee.Count != 0)
                {
                    foreach (var item in employee)
	                {
                        SI_tblHICard modelAdd = new SI_tblHICard() { 
                                                                     ChangePlace = false,
                                                                     EmpID = item.EmpID,
                                                                     Note = "",
                                                                     LSHospitalID = item.LSHospitalIDNew,
                                                                     FromDate = FromDate,
                                                                     ToDate = ToDate
                                                                   };
                        context.Entry(modelAdd).State = System.Data.Entity.EntityState.Added;

	                }
                }
                context.SaveChanges();
                ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool ExtendByEmployee(DateTime FromDate, DateTime ToDate, List<EmployeeSearchViewModel> EmployeeList, out string ErrorMessage)
        {
            
            try
            {
                ErrorMessage = "";
                //1. Lấy toàn bộ nhân viên đã có dữ liệu thông tin thẻ
                var employee = (from ii in context.SI_tblInsuranceInformation
                               where ii.LSHospitalIDNew != null
                                select new { ii.EmpID, ii.LSHospitalIDNew }).ToList();
                //2. Kiểm tra các nhân viên được chọn đã có Thẻ bảo hiểm
                var check = true;
                foreach (var emp in EmployeeList)
                {
                    if (employee.Where(p => p.EmpID == emp.EmpID).FirstOrDefault() == null)
                    {
                        if (!string.IsNullOrEmpty(ErrorMessage))
                        {
                            ErrorMessage += ",";
                        }
                        ErrorMessage += emp.FullName;
                        check = false;
                    }
                }
                //Nếu toàn bộ nhân viên được chọn đã có thẻ BHYT thì ta thêm
                if (check)
                {
                    if (employee != null && employee.Count != 0)
                    {
                        foreach (var item in employee)
                        {
                            if (EmployeeList.Where(p => p.EmpID == item.EmpID).FirstOrDefault() != null)
                            {
                                SI_tblHICard modelAdd = new SI_tblHICard()
                                {
                                    ChangePlace = false,
                                    EmpID = item.EmpID,
                                    Note = "",
                                    LSHospitalID = item.LSHospitalIDNew,
                                    FromDate = FromDate,
                                    ToDate = ToDate
                                };
                                context.Entry(modelAdd).State = System.Data.Entity.EntityState.Added;
                            }
                        }
                    }
                    context.SaveChanges();
                    ErrorMessage = "";
                    return true;
                }
                else
                {
                    //Nếu mà tồn tại 1 hoặc nhiều nhân viên chưa có thẻ thì thông báo lỗi
                    ErrorMessage += " chưa có thông tin về \"số thẻ BHYT\" và \"nơi khám chữa bệnh\" BHYT nên không thể gia hạn.";
                    return false;
                }

                
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
    }
}
