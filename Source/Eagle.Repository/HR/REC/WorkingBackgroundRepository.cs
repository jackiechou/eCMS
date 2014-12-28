using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class WorkingBackgroundRepository
    {
        public EntityDataContext context { get; set; }
        public WorkingBackgroundRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<REC_tblWorkingBackground> GetBy(int? CandidateID)
        {
            var result = context.REC_tblWorkingBackground.Where(p => (CandidateID != 0 && p.CandidateID == CandidateID));
            return result.ToList();
        }

        public bool Add(REC_tblWorkingBackground modelAdd, out string errorMessage)
        {
            try
            {
                context.Entry(modelAdd).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                errorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool Update(REC_tblWorkingBackground modelUpdate, out string errorMessage)
        {
            try
            {
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                errorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public REC_tblWorkingBackground Find(int id)
        {
            return context.REC_tblWorkingBackground.Find(id);
        }

        public bool Delete(int id, out string errorMessage)
        {
            try
            {
                var model = Find(id);
                context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                errorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
