using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Repository
{
    public class SYS_tblParameterRepository
    { 
        public EntityDataContext context { get; set; }
        public SYS_tblParameterRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public SYS_tblParameter FindFirst()
        {
            return context.SYS_tblParameter.FirstOrDefault();
        }

        public bool Add(SYS_tblParameter model)
        {
            try
            {
                try
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                { 
                    string strerr = ex.Message;
                    return false;
                }
            }
            catch// (Exception ex)
            {
                return false;
            }
        }

        public bool Update(SYS_tblParameter model)
        {
            try
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string strerr = ex.Message;
                return false;
            }
        }
    }
}
