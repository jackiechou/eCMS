using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Model;
using Eagle.Model.LS;

namespace Eagle.Repository.LS
{
    public class LS_tblGradeRepository
    {
         public EntityDataContext context { get; set; }
         public LS_tblGradeRepository(EntityDataContext context)
        {
            this.context = context;
        }
         public IEnumerable<LS_tblGradeViewModel> List()
        {
            try
            {
                return context.LS_tblGrade
                            .Select(p => new LS_tblGradeViewModel()
                            {
                                LSGradeID = p.LSGradeID,
                                LSGradeCode = p.LSGradeCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Note = p.Note,
                                Used = p.Used
                            });
            }
            catch
            {
                return new List<LS_tblGradeViewModel>();
            }
        }

         public List<GradeEntity> GetListForDropDownList()
         {
             try
             {
                 return (context.LS_tblGrade.Select(p => new GradeEntity()
                 {
                     Id = p.LSGradeID,
                     Name = p.Name
                 })).ToList();
             }
             catch
             {
                 return new List<GradeEntity>();
             }
         }

         public List<LS_tblGradeViewModel> GetList()
        {
            try
            {
                return context.LS_tblGrade
                           .Select(p => new LS_tblGradeViewModel()
                           {
                               LSGradeID = p.LSGradeID,
                               LSGradeCode = p.LSGradeCode,
                               Name = p.Name,
                               VNName = p.VNName,
                               Rank = p.Rank,
                               Note = p.Note,
                               Used = p.Used
                           }).ToList();
            }
            catch
            {
                return new List<LS_tblGradeViewModel>();
            }
        }
        public bool IsCodeExists(string strCode)
        {
            var code = context.LS_tblGrade.FirstOrDefault(p => p.LSGradeCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblGrade model)
        {
            if (IsCodeExists(model.LSGradeCode))
            {
                string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            return true;
        }

        public LS_tblGrade Find(int id)
        {
            return context.LS_tblGrade.Find(id);
        }

        public bool Update(LS_tblGradeViewModel model, out string errorMessage)
        {
            try
            {
                LS_tblGrade modelUpdate = Find(model.LSGradeID);
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

        #region Bind DropdownList ========================================================================     
        // dùng cho bind dropdownlist
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNume)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblGrade
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSGradeID,
                               name = c.Name,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion ======================================================================================

    }
}
