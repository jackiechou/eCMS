using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;
using Eagle.Model.HR;

namespace Eagle.Repository
{
    public class InsuranceLeaveRepository
    {
        public EntityDataContext context { get; set; }
        public InsuranceLeaveRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public bool Update(SI_tblInsuranceLeave model, out string errorMessage)
        {
            try
            {
                //Kiểm tra xem ngày nghỉ này có nằm trong khoảng thời gian ngày nghỉ khác không
                var check = (from si in context.SI_tblInsuranceLeave
                             where ((si.FromDate.CompareTo(model.FromDate) <= 0 && si.ToDate.CompareTo(model.FromDate) >= 0) ||
                                   (si.FromDate.CompareTo(model.ToDate) <= 0 && si.ToDate.CompareTo(model.ToDate) >= 0))
                                   && si.EmpID == model.EmpID 
                                   && si.ID != model.ID
                             select new { si.FromDate, si.ToDate }).FirstOrDefault();
                if (check != null)
                {
                    errorMessage = string.Format(Eagle.Resource.LanguageResource.InsuranceLeave_AddError,
                                    check.FromDate.ToString("dd/MM/yyyy"),
                                    check.ToDate.ToString("dd/MM/yyyy"));
                    return false;
                }
                else
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    errorMessage = "";
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }


        public bool Add(SI_tblInsuranceLeave model, out string ErrorMessage)
        {
            try
            {
                //Kiểm tra xem ngày nghỉ này có nằm trong khoảng thời gian ngày nghỉ khác không
                var check = (from si in context.SI_tblInsuranceLeave
                             where ((si.FromDate.CompareTo(model.FromDate) <= 0 && si.ToDate.CompareTo(model.FromDate) >=0 ) ||
                                   (si.FromDate.CompareTo(model.ToDate) <= 0 && si.ToDate.CompareTo(model.ToDate) >= 0))
                                   && si.EmpID == model.EmpID
                             select new { si.FromDate, si.ToDate }).FirstOrDefault();
                if (check != null)
                {
                    ErrorMessage = string.Format(Eagle.Resource.LanguageResource.InsuranceLeave_AddError,
                                    check.FromDate.ToString("dd/MM/yyyy"),
                                    check.ToDate.ToString("dd/MM/yyyy"));
                    return false;
                }
                else
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    context.SaveChanges();
                    ErrorMessage = "";
                    return true;
                }
            }
            catch( Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

        }



        public List<InsuranceLeaveViewModel> GetByEmpID(int EmpID)
        {
            var result = from li in context.SI_tblInsuranceLeave
                         where li.EmpID == EmpID
                         select new InsuranceLeaveViewModel()
                         {
                            ID = li.ID,
                            FromDate = li.FromDate,
                            ToDate = li.ToDate,
                            Stage = li.Stage,
                            Total = li.Total,
                            BenifitSalary = li.BenifitSalary
                         };
            return result.ToList();

        }
        public SI_tblInsuranceLeave Find(int? Id)
        {
            return context.SI_tblInsuranceLeave.Find(Id);
        }
        public bool Delete(int? id, out string errorMessage)
        {
            try
            {
                var modelDelete = Find(id);
                if (modelDelete != null)
                {
                    context.Entry(modelDelete).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
                errorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public InsuranceLeaveViewModel FindEdit(int? id)
        {
            var result = from li in context.SI_tblInsuranceLeave
                         join ldt in context.Timesheet_tbLeaveDayType on li.LSLeaveDayTypeID equals ldt.LSLeaveDayTypeID
                         where li.ID == id
                         select new InsuranceLeaveViewModel()
                         {
                             ID = li.ID,
                             EmpID = li.EmpID,
                             FromDate = li.FromDate,
                             dFromDate = li.FromDate,
                             ToDate = li.ToDate,
                             dToDate = li.ToDate,
                             LSLeaveDayTypeID = li.LSLeaveDayTypeID,
                             LSLeaveDayTypeIDAlowNull = li.LSLeaveDayTypeID,
                             LSLeaveDayTypeName = ldt.Name,
                             Total = li.Total,
                             IsInsNotice = li.IsInsNotice,
                             SIMonth = li.SIMonth,
                             BabyDOB = li.BabyDOB,
                             dBabyDOB = li.BabyDOB,
                             IsBenefits = li.IsBenefits,
                             AvgSalary = li.AvgSalary,
                             Percent = li.Percent,
                             BenifitSalary = li.BenifitSalary,
                             BabyDOD = li.BabyDOD,
                             dBabyDOD = li.BabyDOD,
                             Stage = li.Stage,
                             IsConvalescence = li.IsConvalescence,
                             ConditionEffect = li.ConditionEffect
                         };
            return result.FirstOrDefault();
        }

    }
}
