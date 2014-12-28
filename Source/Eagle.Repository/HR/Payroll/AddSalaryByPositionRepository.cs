using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Eagle.Repository;
using System.Web.Routing;
using Eagle.Common.Helpers;
using System.Data.Entity.Validation;
using Eagle.Model;
using Eagle.Core;
namespace Eagle.Repository
{
    public class AddSalaryByPositionRepository
    {
        public EntityDataContext context { get; set; }
        public AddSalaryByPositionRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(int LSPositionID)
        {
            var code = context.PR_tblAddSalaryByPosition.FirstOrDefault(p => p.LSPositionID.Equals(LSPositionID));
            return (code != null);
        }
        public bool CheckExistEdit(int AddSalaryByPositionID, int LSPositionID)
        {
            var code = context.PR_tblAddSalaryByPosition
                        .FirstOrDefault(p => p.AddSalaryByPositionID.Equals(LSPositionID) && p.AddSalaryByPositionID != AddSalaryByPositionID);
            return (code != null);
        }
        public string Add(PR_tblAddSalaryByPosition model)
        {
            try
            {
                if (CheckExist(model.LSPositionID))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistData;
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
        public PR_tblAddSalaryByPosition Find(int id)
        {
            return context.PR_tblAddSalaryByPosition.Find(id);
        }
        public string Update(AddSalaryByPositionViewModels model)
        {
            try
            {
                if (CheckExistEdit(model.AddSalaryByPositionID, model.LSPositionID))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistData;
                    return errorMessage;
                }
                PR_tblAddSalaryByPosition modelUpdate = Find(model.AddSalaryByPositionID);
                modelUpdate.LSPositionID = model.LSPositionIDNull.Value;
                modelUpdate.PaymentMethod = model.PaymentMethod;
                modelUpdate.LSSalaryAdjustID = model.LSSalaryAdjustIDNull.Value;
                modelUpdate.FromMonth = model.FromMonth;
                modelUpdate.ToMonth = model.ToMonth;
                modelUpdate.Amount = model.Amount;
                modelUpdate.LSCurrencyID = model.LSCurrencyID;
                modelUpdate.isGross = model.GROSSNET.Value;
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
        public List<AddSalaryByPositionViewModels> List(int LanguageId)
        {
            try
            {
                return (from p in context.PR_tblAddSalaryByPosition
                        join l in context.LS_tblPosition on p.LSPositionID equals l.LSPositionID    
                        join c in context.LS_tblCurrency on p.LSCurrencyID equals c.LSCurrencyID into JoinCurrency from c in JoinCurrency.DefaultIfEmpty()

                        select new AddSalaryByPositionViewModels()
                        {
                            AddSalaryByPositionID = p.AddSalaryByPositionID,
                            LSPositionID = p.LSPositionID,
                            LSPositionName =  LanguageId == 124 ? p.LS_tblPosition.Name : p.LS_tblPosition.VNName,
                            LSSalaryAdjustID = p.LSSalaryAdjustID,
                            LSSalaryAdjustName = LanguageId == 124 ? p.LS_tblSalaryAdjust.Name : p.LS_tblSalaryAdjust.VNName,
                            PaymentMethod = p.PaymentMethod,
                            FromMonth = p.FromMonth,
                            ToMonth = p.ToMonth,
                            Amount = p.Amount,
                            isGross = p.isGross,
                            LSCurrencyName = LanguageId == 124 ? p.LS_tblCurrency.Name : p.LS_tblCurrency.VNName
                        }).ToList();
            }
            catch
            {
                return new List<AddSalaryByPositionViewModels>();
            }
        }
        public AddSalaryByPositionViewModels FindEdit(int id, int LanguageId)
        {
            try
            {
                AddSalaryByPositionViewModels ret = context.PR_tblAddSalaryByPosition
                                        .Where(p => p.AddSalaryByPositionID == id)
                                        .Select(p => new AddSalaryByPositionViewModels()
                                        {
                                            AddSalaryByPositionID = p.AddSalaryByPositionID,
                                            LSPositionID = p.LSPositionID,
                                            LSPositionIDNull = p.LSPositionID,
                                            LSPositionName = LanguageId == 124 ? p.LS_tblPosition.Name : p.LS_tblPosition.VNName,
                                            LSSalaryAdjustID = p.LSSalaryAdjustID,
                                            LSSalaryAdjustIDNull = p.LSSalaryAdjustID,
                                            LSSalaryAdjustName = LanguageId == 124 ? p.LS_tblSalaryAdjust.Name : p.LS_tblSalaryAdjust.VNName,
                                            Amount = p.Amount,
                                            PaymentMethod = p.PaymentMethod,
                                            FromMonth = p.FromMonth,
                                            ToMonth = p.ToMonth,
                                            LSCurrencyID = p.LSCurrencyID,
                                            LSCurrencyName = LanguageId==10001 ? p.LS_tblCurrency.Name : p.LS_tblCurrency.VNName,
                                            isGross = p.isGross,
                                            GROSSNET = p.isGross,
                                            Note = p.Note
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new AddSalaryByPositionViewModels();
            }
        }
        public bool Delete(int id)
        {
            try
            {
                PR_tblAddSalaryByPosition modelUpdate = Find(id);
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
