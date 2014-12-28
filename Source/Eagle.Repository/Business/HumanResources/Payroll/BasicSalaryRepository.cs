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
    public class BasicSalaryRepository
    {
        public EntityDataContext context { get; set; }
        public BasicSalaryRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(DateTime dValidate, int EmpID)
        {
            var code = context.PR_tblSalary.FirstOrDefault(p => p.EffectiveDate.Equals(dValidate) && p.EmpID == EmpID);
            return (code != null);
        }
        public bool CheckExistEdit(DateTime dValidate, int EmpID, int SalaryID)
        {
            var code = context.PR_tblSalary
                        .FirstOrDefault(p => p.EffectiveDate.Equals(dValidate) && p.EmpID == EmpID && p.SalaryID != SalaryID);
            return (code != null);
        }
        public string Add(PR_tblSalary model)
        {
            try
            {
                if (CheckExist(model.EffectiveDate, model.EmpID))
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
        public PR_tblSalary Find(int id)
        {
            return context.PR_tblSalary.Find(id);
        }
        public string Update(SalaryViewModels model)
        {
            try
            {
                if (CheckExistEdit(model.EffectiveDate,model.EmpID, model.SalaryID))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistData;
                    return errorMessage;
                }
                PR_tblSalary modelUpdate = Find(model.SalaryID);
                modelUpdate.EffectiveDate = model.EffectiveDate;
                modelUpdate.BasicSalary = model.BasicSalary;
                modelUpdate.PercentBasicSalary = model.PercentBasicSalary;
                modelUpdate.ActualSalary = model.ActualSalary;
                modelUpdate.LSCurrencyID = model.LSCurrencyID;
                modelUpdate.isGross = model.GROSSNET.Value;
                modelUpdate.InsuranceSalary = model.InsuranceSalary;
                modelUpdate.InsuranceLSCurrencyID = model.InsuranceLSCurrencyID;
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
        public List<SalaryViewModels> List(int? EmpID, int LanguageId)
        {
            try
            {
                return (from p in context.PR_tblSalary
                        //join c in context.LS_tblCurrency on p.LSCurrencyID equals c.LSCurrencyID into JoinCurrency from c in JoinCurrency.DefaultIfEmpty()
                        where p.EmpID == EmpID && p.TypeSalary == 1
                        orderby p.EffectiveDate descending
                        select new SalaryViewModels()
                        {
                            SalaryID = p.SalaryID,
                            EffectiveDate = p.EffectiveDate,
                            BasicSalary = p.BasicSalary,
                            PercentBasicSalary = p.PercentBasicSalary,
                            ActualSalary = p.ActualSalary,
                            isGross = p.isGross,
                            LSCurrencyName = LanguageId == 124 ? p.LS_tblCurrency.Name : p.LS_tblCurrency.VNName,
                            InsuranceSalary = p.InsuranceSalary,
                            InsuranceLSCurrencyName = LanguageId == 124 ? p.LS_tblCurrency1.Name : p.LS_tblCurrency1.VNName

                        }).ToList();
            }
            catch
            {
                return new List<SalaryViewModels>();
            }
        }
        public List<SalaryCoefViewModels> ListCoef(int? EmpID, int LanguageId)
        {
            try
            {
                return (from p in context.PR_tblSalary
                        join s in context.PR_tblSalaryScale on p.SalaryScaleID equals s.SalaryScaleID
                        join g in context.PR_tblSalaryGrade on p.SalaryGradeID equals g.SalaryGradeID
                        join r in context.PR_tblSalaryRank on p.SalaryRankID equals r.SalaryRankID
                        where p.EmpID == EmpID && p.TypeSalary == 2
                        orderby p.EffectiveDate descending
                        select new SalaryCoefViewModels()
                        {
                            SalaryID = p.SalaryID,
                            EffectiveDate = p.EffectiveDate,

                            BasicCoef = p.BasicCoef,
                            PercentBasicCoef = p.PercentBasicCoef,
                            ActualCoef = p.ActualCoef,
                            
                            isGross = p.isGross,
                            InsuranceSalary = p.InsuranceSalary
                            

                        }).ToList();
            }
            catch
            {
                return new List<SalaryCoefViewModels>();
            }
        }
        public SalaryViewModels FindEdit(int id, int LanguageId)
        {
            try
            {
                SalaryViewModels ret = context.PR_tblSalary
                                        .Where(p => p.SalaryID == id)
                                        .Select(p => new SalaryViewModels()
                                        {
                                            SalaryID = p.SalaryID,
                                            dEffectiveDate = p.EffectiveDate,
                                            EffectiveDate = p.EffectiveDate,
                                            BasicSalary = p.BasicSalary,
                                            PercentBasicSalary = p.PercentBasicSalary,
                                            ActualSalary = p.ActualSalary,
                                            LSCurrencyID = p.LSCurrencyID,
                                            LSCurrencyName = LanguageId==10001 ? p.LS_tblCurrency.Name : p.LS_tblCurrency.VNName,
                                            InsuranceLSCurrencyID = p.InsuranceLSCurrencyID,
                                            InsuranceLSCurrencyName = LanguageId == 124 ? p.LS_tblCurrency1.Name : p.LS_tblCurrency1.VNName,
                                            InsuranceSalary = p.InsuranceSalary,
                                            isGross = p.isGross,
                                            GROSSNET = p.isGross,
                                            Note = p.Note
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new SalaryViewModels();
            }
        }
        public bool Delete(int id)
        {
            try
            {
                PR_tblSalary modelUpdate = Find(id);
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
