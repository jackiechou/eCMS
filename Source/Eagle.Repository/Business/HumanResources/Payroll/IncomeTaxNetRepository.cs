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
    public class IncomeTaxNetRepository
    {
        public EntityDataContext context { get; set; }
        public IncomeTaxNetRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(DateTime dValidate)
        {
            var code = context.PR_tblIncometaxNET.FirstOrDefault(p => p.EffectiveDate.Equals(dValidate));
            return (code != null);
        }
        public IEnumerable<IncometaxNetViewModels> List()
        {

            try
            {
                return  (from p in context.PR_tblIncometaxNET
                          select new IncometaxNetViewModels()
                          { 
                              IncometaxNetID = p.IncometaxNetID,
                              EffectiveDate = p.EffectiveDate,
                              IncomeTo1 = p.IncomeTo1,
                              IncomeTo2 = p.IncomeTo2,
                              IncomeTo3 = p.IncomeTo3,
                              IncomeTo4 = p.IncomeTo4,
                              IncomeTo5 = p.IncomeTo5,
                              IncomeTo6 = p.IncomeTo6,
                              IncomeTo7 = p.IncomeTo7,
                              Subtract1 = p.Subtract1,
                              Subtract2 = p.Subtract2,
                              Subtract3 = p.Subtract3,
                              Subtract4 = p.Subtract4,
                              Subtract5 = p.Subtract5,
                              Subtract6 = p.Subtract6,
                              Subtract7 = p.Subtract7,
                              Divide1 = p.Divide1,
                              Divide2 = p.Divide2,
                              Divide3 = p.Divide3,
                              Divide4 = p.Divide4,
                              Divide5 = p.Divide5,
                              Divide6 = p.Divide5,
                              Divide7 = p.Divide7
                          }).ToList();

                //List<ScaleCreateViewModels> ret = new List<ScaleCreateViewModels>();
                //foreach (var item in tmp)
                //{
                //    ScaleCreateViewModels s = new ScaleCreateViewModels()
                //    {
                //        SalaryScaleID = item.SalaryScaleID,
                //        SalaryScaleCode = item.SalaryScaleCode,
                //        VNName = item.VNName,
                //        Used = item.Used,
                //        Rank = item.Rank,
                //        Name = item.Name,
                //        Note = item.Note
                //    };
                //    ret.Add(s);
                //}
                //return ret;
            }
            catch
            {
                return new List<IncometaxNetViewModels>();
            }
        }
        public bool CheckExistEdit(DateTime dValidate, int IncometaxNetID)
        {
            var code = context.PR_tblIncometaxNET
                        .FirstOrDefault(p => p.EffectiveDate.Equals(dValidate) && p.IncometaxNetID != IncometaxNetID);
            return (code != null);
        }
        public PR_tblIncometaxNET Find(int id)
        {
            return context.PR_tblIncometaxNET.Find(id);
        }
        public string Add(PR_tblIncometaxNET model)
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
        public string Update(IncometaxNetViewModels model)
        {
            try
            {
                if (CheckExistEdit(model.EffectiveDate, model.IncometaxNetID))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                PR_tblIncometaxNET modelUpdate = Find(model.IncometaxNetID);
                modelUpdate.EffectiveDate = model.EffectiveDate;
                modelUpdate.IncomeTo1 = model.IncomeTo1;
                modelUpdate.IncomeTo2 = model.IncomeTo2;
                modelUpdate.IncomeTo3 = model.IncomeTo3;
                modelUpdate.IncomeTo4 = model.IncomeTo4;
                modelUpdate.IncomeTo5 = model.IncomeTo5;
                modelUpdate.IncomeTo6 = model.IncomeTo6;
                modelUpdate.IncomeTo7 = model.IncomeTo7;
                modelUpdate.Subtract1 = model.Subtract1;
                modelUpdate.Subtract2 = model.Subtract2;
                modelUpdate.Subtract3 = model.Subtract3;
                modelUpdate.Subtract4 = model.Subtract4;
                modelUpdate.Subtract5 = model.Subtract5;
                modelUpdate.Subtract6 = model.Subtract6;
                modelUpdate.Subtract7 = model.Subtract7;
                modelUpdate.Divide1 = model.Divide1;
                modelUpdate.Divide2 = model.Divide2;
                modelUpdate.Divide3 = model.Divide3;
                modelUpdate.Divide4 = model.Divide4;
                modelUpdate.Divide5 = model.Divide5;
                modelUpdate.Divide6 = model.Divide6;
                modelUpdate.Divide7 = model.Divide7;

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
        public IncometaxNetViewModels FindEdit(int id)
        {
            try
            {
                IncometaxNetViewModels ret = context.PR_tblIncometaxNET
                                        .Where(p => p.IncometaxNetID == id)
                                        .Select(p => new IncometaxNetViewModels()
                                        {
                                            IncometaxNetID = p.IncometaxNetID,
                                            dEffectiveDate = p.EffectiveDate,
                                            EffectiveDate = p.EffectiveDate,
                                            IncomeTo1 = p.IncomeTo1,
                                            IncomeTo2 = p.IncomeTo2,
                                            IncomeTo3 = p.IncomeTo3,
                                            IncomeTo4 = p.IncomeTo4,
                                            IncomeTo5 = p.IncomeTo5,
                                            IncomeTo6 = p.IncomeTo6,
                                            IncomeTo7 = p.IncomeTo7,
                                            Subtract1 = p.Subtract1,
                                            Subtract2 = p.Subtract2,
                                            Subtract3 = p.Subtract3,
                                            Subtract4 = p.Subtract4,
                                            Subtract5 = p.Subtract5,
                                            Subtract6 = p.Subtract6,
                                            Subtract7 = p.Subtract7,
                                            Divide1 = p.Divide1,
                                            Divide2 = p.Divide2,
                                            Divide3 = p.Divide3,
                                            Divide4 = p.Divide4,
                                            Divide5 = p.Divide5,
                                            Divide6 = p.Divide6,
                                            Divide7 = p.Divide7
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new IncometaxNetViewModels();
            }

        }
        public bool Delete(int id)
        {
            try
            {
                PR_tblIncometaxNET modelUpdate = Find(id);
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
