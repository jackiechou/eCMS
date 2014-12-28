using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ChangingInsuranceInformationLogRepository
    {
        public EntityDataContext context { get; set; }
        public ChangingInsuranceInformationLogRepository(EntityDataContext context)
        {
            this.context = context;
        }

        //public bool Add(SI_tblChangingInsuranceInformationLog model, out string ErrorMessage)
        //{
        //    ErrorMessage = "";
        //    try
        //    {
        //        context.Entry(model).State = System.Data.Entity.EntityState.Added;
        //        context.SaveChanges();
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        ErrorMessage = ex.Message;
        //        return false;
        //    }
        //}

    }
}
