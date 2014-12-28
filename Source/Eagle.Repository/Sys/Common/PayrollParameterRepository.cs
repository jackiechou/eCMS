using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
//
using Eagle.Model;
using Eagle.Core;
namespace Eagle.Repository
{
    public class PayrollParameterRepository
    {
        public EntityDataContext context { get; set; }
        public PayrollParameterRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<SYS_tblPayrollParameter> GetAll()
        {
            try
            {
                return context.SYS_tblPayrollParameter.ToList();
            }
            catch
            {
                return new List<SYS_tblPayrollParameter>();
            }
        }

        public PayrollParameterViewModel FindEdit(int id)
        {
            try
            {
                var model = context.SYS_tblPayrollParameter.Find(id);
                if (model != null)
                {
                    return new PayrollParameterViewModel()
                    {
                        ID = model.ID,
                        EffectiveDate = model.EffectiveDate,
                        CoefInsS_E = model.CoefInsS_E,
                        CoefInsS_C = model.CoefInsS_C,
                        CoefInsH_E = model.CoefInsH_E,
                        CoefInsH_C = model.CoefInsH_C,
                        CoefInsE_E = model.CoefInsE_E,
                        CoefInsE_C = model.CoefInsE_C,
                        MinSalary = model.MinSalary,
                        CoefOver = model.CoefOver,
                        SelfDeduction = model.SelfDeduction,
                        DependDeduction = model.DependDeduction,
                        Description = model.Description
                    };
                }
            }
            catch{}

            return new PayrollParameterViewModel();

        }
        public SYS_tblPayrollParameter Find(int id)
        {
            return context.SYS_tblPayrollParameter.Find(id);
        }
        public bool Add(SYS_tblPayrollParameter model)
        {
            try
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(PayrollParameterViewModel model)
        {
            try
            {
                DateTime EffectiveDate = new DateTime();
                decimal DependDeduction = 0;
                if (model.EffectiveDate != null)
                {
                    EffectiveDate = Convert.ToDateTime(model.EffectiveDate);
                }
                else
                {
                    return false;
                }

                if (model.DependDeduction != null)
                {
                    DependDeduction = Convert.ToDecimal(model.DependDeduction);
                }
                else
                {
                    return false;
                }

                SYS_tblPayrollParameter modelUpdate = Find(model.ID);
                modelUpdate.EffectiveDate = EffectiveDate;
                modelUpdate.CoefInsS_E = model.CoefInsS_E;
                modelUpdate.CoefInsS_C = model.CoefInsS_C;
                modelUpdate.CoefInsH_E = model.CoefInsH_E;
                modelUpdate.CoefInsH_C = model.CoefInsH_C;
                modelUpdate.CoefInsE_E = model.CoefInsE_E;
                modelUpdate.CoefInsE_C = model.CoefInsE_C;
                modelUpdate.MinSalary = model.MinSalary;
                modelUpdate.CoefOver = model.CoefOver;
                modelUpdate.SelfDeduction = model.SelfDeduction;
                modelUpdate.DependDeduction = DependDeduction;
                modelUpdate.Description = model.Description;

                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return false;
            }
            
        }
        public bool Delete(int id)
        {
            try
            {
                SYS_tblPayrollParameter modelUpdate = Find(id);
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
