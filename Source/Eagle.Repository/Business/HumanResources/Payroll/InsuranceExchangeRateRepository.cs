using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//


using System.Web.Routing;
using Eagle.Common.Helpers;
using System.Data.Entity.Validation;
using Eagle.Model;
using Eagle.Core;
namespace Eagle.Repository
{
    public class InsuranceExchangeRateRepository
    {
        public EntityDataContext context { get; set; }
        public InsuranceExchangeRateRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(DateTime dValidate, int? LSCurrencyID)
        {
            var code = context.PR_tblInsuranceExchangeRate.FirstOrDefault(p => p.EffectiveDate.Equals(dValidate) && p.LSCurrencyID == LSCurrencyID);
            return (code != null);
        }
        public IEnumerable<InsuranceExchangeRateViewModels> List(int LanguageId)
        {
            try
            {
                return (from p in context.PR_tblInsuranceExchangeRate
                        select new InsuranceExchangeRateViewModels()
                          {
                              LSInsuranceExchangeRateID = p.LSInsuranceExchangeRateID,
                              EffectiveDate = p.EffectiveDate,
                              LSCurrencyID = p.LSCurrencyID,
                              LSCurrencyName = LanguageId == 124 ? p.LS_tblCurrency.Name : p.LS_tblCurrency.VNName,
                              ExchangeRate = p.ExchangeRate,
                              Note = p.Note
                          }).ToList();
            }
            catch
            {
                return new List<InsuranceExchangeRateViewModels>();
            }
        }
        public bool CheckExistEdit(DateTime dValidate, int? LSCurrencyID, int LSInsuranceExchangeRateID)
        {
            var code = context.PR_tblInsuranceExchangeRate
                        .FirstOrDefault(p => p.EffectiveDate.Equals(dValidate) && p.LSCurrencyID == LSCurrencyID && p.LSInsuranceExchangeRateID != LSInsuranceExchangeRateID);
            return (code != null);
        }
        public PR_tblInsuranceExchangeRate Find(int id)
        {
            return context.PR_tblInsuranceExchangeRate.Find(id);
        }
        public string Add(PR_tblInsuranceExchangeRate model)
        {
            try
            {
                if (CheckExist(model.EffectiveDate, model.LSCurrencyID))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return "success";
            }
            catch //(Exception e)
            {
                return "error";
            }
        }
        public string Update(InsuranceExchangeRateViewModels model)
        {
            try
            {
                if (CheckExistEdit(model.EffectiveDate,model.LSCurrencyID,  model.LSInsuranceExchangeRateID))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                PR_tblInsuranceExchangeRate modelUpdate = Find(model.LSInsuranceExchangeRateID);
                modelUpdate.EffectiveDate = model.EffectiveDate;
                modelUpdate.LSCurrencyID = model.LSCurrencyID;
                modelUpdate.ExchangeRate = model.ExchangeRate;
                modelUpdate.Note = model.Note;
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return strErr;
            }
        }
        public InsuranceExchangeRateViewModels FindEdit(int id, int LanguageId)
        {
            try
            {
                InsuranceExchangeRateViewModels ret = context.PR_tblInsuranceExchangeRate
                                        .Where(p => p.LSInsuranceExchangeRateID == id)
                                        .Select(p => new InsuranceExchangeRateViewModels()
                                        {
                                            LSInsuranceExchangeRateID = p.LSInsuranceExchangeRateID,
                                            dEffectiveDate = p.EffectiveDate,
                                            EffectiveDate = p.EffectiveDate,
                                            ExchangeRate = p.ExchangeRate,
                                            LSCurrencyID = p.LSCurrencyID,
                                            LSCurrencyName = LanguageId == 124 ? p.LS_tblCurrency.Name : p.LS_tblCurrency.VNName,
                                            Note = p.Note
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new InsuranceExchangeRateViewModels();
            }

        }
        public bool Delete(int id)
        {
            try
            {
                PR_tblInsuranceExchangeRate modelUpdate = Find(id);
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return false;
            }

        }
    }
}
