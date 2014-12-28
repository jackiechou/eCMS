using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModels;

namespace Eagle.Repository
{
    public class ChangingInsuranceInformationMasterRepository
    {
        public EntityDataContext context { get; set; }
        public ChangingInsuranceInformationMasterRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool Update(SI_tblChangingInsuranceInformationMaster model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                if (model.MasterID == null || model.MasterID == 0)
                {
                    context.Entry(model).State = System.Data.EntityState.Added;
                }
                else
                {
                    context.Entry(model).State = System.Data.EntityState.Modified;
                }
                
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public List<ChangingInsuranceInformationMasterViewModel> GetList(int? LSCompanyID, DateTime? MonthYear, bool? isNotified)
        {
            return context.SI_tblChangingInsuranceInformationMaster
                      .Where(p => (LSCompanyID == null || p.LSCompanyID == LSCompanyID) &&
                                  (MonthYear == null || (p.Month == MonthYear.Value.Month && p.Year == MonthYear.Value.Year)) &&
                                  (isNotified == null || p.isNotified == isNotified))
                      .Select(p => new ChangingInsuranceInformationMasterViewModel()
                      {
                          MasterID = p.MasterID,
                          Name = p.Name,
                          isNotified = p.isNotified,
                          Month = p.Month,
                          Year = p.Year,
                          CreateUser = p.CreateUser
                      }).ToList();
        }

        public bool Delete(int MasterID, out string errorMessage)
        {
            try
            {
                var modelDelete = context.SI_tblChangingInsuranceInformationMaster.Find(MasterID);
                if (modelDelete.isNotified == false)
                {
                    context.Entry(modelDelete).State = System.Data.EntityState.Deleted;
                    context.SaveChanges();
                    errorMessage = "";
                    return true;
                }
                else
                {
                    errorMessage = "Danh sách này đã được báo lên bảo hiểm nên không xóa được.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }



        public SI_tblChangingInsuranceInformationMaster Find(int? MasterID)
        {
            return context.SI_tblChangingInsuranceInformationMaster.Find(MasterID);
        }

        public bool UpdateIsNotified(int MasterID, out string errorMessage)
        {
            try
            {
                var modelUpdate = context.SI_tblChangingInsuranceInformationMaster.Find(MasterID);
                modelUpdate.isNotified = true;
                context.Entry(modelUpdate).State = System.Data.EntityState.Modified;
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
