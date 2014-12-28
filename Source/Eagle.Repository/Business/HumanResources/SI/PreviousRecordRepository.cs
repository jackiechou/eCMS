using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class PreviousRecordRepository
    {
        public EntityDataContext context { get; set; }
        public PreviousRecordRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<SI_tblPreviousRecord> GetBy(int? EmpID, int LanguageId = 10001)
        {
            var result = from p in context.SI_tblPreviousRecord
                         where p.EmpID == EmpID
                         select p;
            return result.ToList();
        }

        public bool Add(SI_tblPreviousRecord model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public SI_tblPreviousRecord Find(int id)
        {
            return context.SI_tblPreviousRecord.Find(id);
        }

        public bool Update(SI_tblPreviousRecord model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool Delete(int id, out string ErrorMessage)
        {
            try
            {
                var model = Find(id);
                if (model != null)
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
                ErrorMessage = "";
                return true;
            }
            catch( Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

        }
    }
}
