using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//

using System.Web.Routing;
using Eagle.Common.Helpers;
using System.Data.Entity.Validation;
using Eagle.Core;
using Eagle.Model;
namespace Eagle.Repository
{
    public class IncomeTaxGrossRepository
    {

        public EntityDataContext context { get; set; }
        public IncomeTaxGrossRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(DateTime dValidate)
        {
            var code = context.PR_tblIncomeTaxGROSS.FirstOrDefault(p => p.EffectiveDate.Equals(dValidate));
            return (code != null);
        }
        public IEnumerable<IncometaxGrossViewModels> List()
        {
            try
            {
                return  (from p in context.PR_tblIncomeTaxGROSS
                         select new IncometaxGrossViewModels()
                          {
                              IncomeTaxGrossID = p.IncomeTaxGrossID,
                              EffectiveDate = p.EffectiveDate,
                              IncomeTo1 = p.IncomeTo1,
                              IncomeTo2 = p.IncomeTo2,
                              IncomeTo3 = p.IncomeTo3,
                              IncomeTo4 = p.IncomeTo4,
                              IncomeTo5 = p.IncomeTo5,
                              IncomeTo6 = p.IncomeTo6,
                              IncomeTo7 = p.IncomeTo7,
                              Rate1 = p.Rate1,
                              Rate2 = p.Rate2,
                              Rate3 = p.Rate3,
                              Rate4 = p.Rate4,
                              Rate5 = p.Rate5,
                              Rate6 = p.Rate6,
                              Rate7 = p.Rate7
                          }).ToList();
            }
            catch
            {
                return new List<IncometaxGrossViewModels>();
            }
        }
        public bool CheckExistEdit(DateTime dValidate, int IncomeTaxGrossID)
        {
            var code = context.PR_tblIncomeTaxGROSS
                        .FirstOrDefault(p => p.EffectiveDate.Equals(dValidate) && p.IncomeTaxGrossID != IncomeTaxGrossID);
            return (code != null);
        }
        public PR_tblIncomeTaxGROSS Find(int id)
        {
            return context.PR_tblIncomeTaxGROSS.Find(id);
        }
        public string Add(PR_tblIncomeTaxGROSS model)
        {
            try
            {
                if (CheckExist(model.EffectiveDate))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return "success";
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public string Update(IncometaxGrossViewModels model)
        {
            try
            {
                if (CheckExistEdit(model.EffectiveDate, model.IncomeTaxGrossID))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                PR_tblIncomeTaxGROSS modelUpdate = Find(model.IncomeTaxGrossID);
                modelUpdate.EffectiveDate = model.EffectiveDate;
                modelUpdate.IncomeTo1 = model.IncomeTo1;
                modelUpdate.IncomeTo2 = model.IncomeTo2;
                modelUpdate.IncomeTo3 = model.IncomeTo3;
                modelUpdate.IncomeTo4 = model.IncomeTo4;
                modelUpdate.IncomeTo5 = model.IncomeTo5;
                modelUpdate.IncomeTo6 = model.IncomeTo6;
                modelUpdate.IncomeTo7 = model.IncomeTo7;
                modelUpdate.Rate1 = model.Rate1;
                modelUpdate.Rate2 = model.Rate2;
                modelUpdate.Rate3 = model.Rate3;
                modelUpdate.Rate4 = model.Rate4;
                modelUpdate.Rate5 = model.Rate5;
                modelUpdate.Rate6 = model.Rate6;
                modelUpdate.Rate7 = model.Rate7;
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
        public IncometaxGrossViewModels FindEdit(int id)
        {
            try
            {
                IncometaxGrossViewModels ret = context.PR_tblIncomeTaxGROSS
                                        .Where(p => p.IncomeTaxGrossID == id)
                                        .Select(p => new IncometaxGrossViewModels()
                                        {
                                            IncomeTaxGrossID = p.IncomeTaxGrossID,
                                            dEffectiveDate = p.EffectiveDate,
                                            EffectiveDate = p.EffectiveDate,
                                            IncomeTo1 = p.IncomeTo1,
                                            IncomeTo2 = p.IncomeTo2,
                                            IncomeTo3 = p.IncomeTo3,
                                            IncomeTo4 = p.IncomeTo4,
                                            IncomeTo5 = p.IncomeTo5,
                                            IncomeTo6 = p.IncomeTo6,
                                            IncomeTo7 = p.IncomeTo7,
                                            Rate1 = p.Rate1,
                                            Rate2 = p.Rate2,
                                            Rate3 = p.Rate3,
                                            Rate4 = p.Rate4,
                                            Rate5 = p.Rate5,
                                            Rate6 = p.Rate6,
                                            Rate7 = p.Rate7
                                            
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new IncometaxGrossViewModels();
            }

        }
        public bool Delete(int id)
        {
            try
            {
                PR_tblIncomeTaxGROSS modelUpdate = Find(id);
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
