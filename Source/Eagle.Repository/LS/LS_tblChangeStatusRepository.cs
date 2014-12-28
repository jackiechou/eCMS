using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Model.LS;

namespace Eagle.Repository.LS
{
    public class LS_tblChangeStatusRepository
    {
         public EntityDataContext context { get; set; }
         public LS_tblChangeStatusRepository(EntityDataContext context)
        {
            this.context = context;
        }
         public IEnumerable<LS_tblChangeStatusViewModel> List()
        {
            try
            {
                return context.LS_tblChangeStatus
                            .Select(p => new LS_tblChangeStatusViewModel()
                            {
                                LSChangeStatusID = p.LSChangeStatusID,
                                LSChangeStatusCode = p.LSChangeStatusCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Note = p.Note,
                                Used = p.Used
                            });
            }
            catch
            {
                return new List<LS_tblChangeStatusViewModel>();
            }
        }

         public List<ChangeStatusEntity> GetListForDropDownList()
         {
             try
             {
                 return (context.LS_tblChangeStatus
                            .Select(p => new ChangeStatusEntity()
                            {
                                Id = p.LSChangeStatusID,
                                Name = p.Name
                            })).ToList();
             }
             catch
             {
                 return new List<ChangeStatusEntity>();
             }
         }

         public List<LS_tblChangeStatusViewModel> GetList()
        {
            try
            {
                return context.LS_tblChangeStatus
                           .Select(p => new LS_tblChangeStatusViewModel()
                           {
                               LSChangeStatusID = p.LSChangeStatusID,
                               LSChangeStatusCode = p.LSChangeStatusCode,
                               Name = p.Name,
                               VNName = p.VNName,
                               Rank = p.Rank,
                               Note = p.Note,
                               Used = p.Used
                           }).ToList();
            }
            catch
            {
                return new List<LS_tblChangeStatusViewModel>();
            }
        }
        public bool IsCodeExists(string strCode)
        {
            var code = context.LS_tblChangeStatus.FirstOrDefault(p => p.LSChangeStatusCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblChangeStatus model)
        {
            if (IsCodeExists(model.LSChangeStatusCode))
            {
                string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            return true;
        }

        public LS_tblChangeStatus Find(int id)
        {
            return context.LS_tblChangeStatus.Find(id);
        }

        public bool Update(LS_tblChangeStatusViewModel model, out string errorMessage)
        {
            try
            {
                LS_tblChangeStatus modelUpdate = Find(model.LSChangeStatusID);
                modelUpdate.Name = model.Name;
                modelUpdate.VNName = model.VNName;
                modelUpdate.Rank = model.Rank;                
                modelUpdate.Note = model.Note;
                modelUpdate.Used = model.Used;
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                errorMessage = "";
                return true;
            }
            catch
            {
                errorMessage = "Error";
                return false;
            }
        }
    }
}
