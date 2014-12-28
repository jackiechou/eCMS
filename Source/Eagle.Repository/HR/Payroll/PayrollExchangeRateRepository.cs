using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



using System.Web.Routing;
using Eagle.Common.Helpers;
using System.Data.Entity.Validation;
using Eagle.Model;
using Eagle.Core;
namespace Eagle.Repository
{
    public class PayrollExchangeRateRepository
    {
        public EntityDataContext context { get; set; }
        public PayrollExchangeRateRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(DateTime dValidate, int? LSCurrencyID)
        {
            var code = context.PR_tblPayrollExchangeRate.FirstOrDefault(p => p.EffectiveDate.Equals(dValidate) && p.LSCurrencyID == LSCurrencyID);
            return (code != null);
        }
        public IEnumerable<PayrollExchangeRateViewModels> List(int LanguageId)
        {
            try
            {
                return (from p in context.PR_tblPayrollExchangeRate
                        select new PayrollExchangeRateViewModels()
                          {
                              LSPayrollExchangeRateID = p.LSPayrollExchangeRateID,
                              EffectiveDate = p.EffectiveDate,
                              LSCurrencyID = p.LSCurrencyID,
                              LSCurrencyName = LanguageId == 124 ? p.LS_tblCurrency.Name : p.LS_tblCurrency.VNName,
                              ExchangeRate = p.ExchangeRate,
                              Note = p.Note
                          }).ToList();
            }
            catch
            {
                return new List<PayrollExchangeRateViewModels>();
            }
        }
        public bool CheckExistEdit(DateTime dValidate, int? LSCurrencyID, int LSPayrollExchangeRateID)
        {
            var code = context.PR_tblPayrollExchangeRate
                        .FirstOrDefault(p => p.EffectiveDate.Equals(dValidate) && p.LSCurrencyID == LSCurrencyID && p.LSPayrollExchangeRateID != LSPayrollExchangeRateID);
            return (code != null);
        }
        public PR_tblPayrollExchangeRate Find(int id)
        {
            return context.PR_tblPayrollExchangeRate.Find(id);
        }
        public string Add(PR_tblPayrollExchangeRate model)
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
        public string Update(PayrollExchangeRateViewModels model)
        {
            try
            {
                if (CheckExistEdit(model.EffectiveDate,model.LSCurrencyID,  model.LSPayrollExchangeRateID))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                PR_tblPayrollExchangeRate modelUpdate = Find(model.LSPayrollExchangeRateID);
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
        public PayrollExchangeRateViewModels FindEdit(int id, int LanguageId)
        {
            try
            {
                PayrollExchangeRateViewModels ret = context.PR_tblPayrollExchangeRate
                                        .Where(p => p.LSPayrollExchangeRateID == id)
                                        .Select(p => new PayrollExchangeRateViewModels()
                                        {
                                            LSPayrollExchangeRateID = p.LSPayrollExchangeRateID,
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
                return new PayrollExchangeRateViewModels();
            }

        }
        public bool Delete(int id)
        {
            try
            {
                PR_tblPayrollExchangeRate modelUpdate = Find(id);
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
