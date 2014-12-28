using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Eagle.Model;
using Eagle.Core;
namespace Eagle.Repository
{
    public class LS_tblEducationRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblEducationRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<LS_tblEducationViewModel> List()
        {
            try
            {
                return context.LS_tblEducation
                            .Select(p => new LS_tblEducationViewModel()
                            {
                                LSEducationID = p.LSEducationID,
                                LSEducationCode = p.LSEducationCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            });
            }
            catch
            {
                return new List<LS_tblEducationViewModel>();
            }
        }

        public List<EducationEntity> GetListForDropDownList()
        {
            try
            {
                return (context.LS_tblEducation.Select(p => new EducationEntity()
                {
                    Id = p.LSEducationID,
                    Name = p.Name
                })).ToList();
            }
            catch
            {
                return new List<EducationEntity>();
            }
        }

        public List<LS_tblEducationViewModel> GetList()
        {
            try
            {
                return context.LS_tblEducation
                            .Select(p => new LS_tblEducationViewModel()
                            {
                                LSEducationID = p.LSEducationID,
                                LSEducationCode = p.LSEducationCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).ToList();
            }
            catch
            {
                return new List<LS_tblEducationViewModel>();
            }
        }
        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblEducation.FirstOrDefault(p => p.LSEducationCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblEducation model, out string outMesssage)
        {
            if (CheckExistCode(model.LSEducationCode))
            {
                outMesssage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            outMesssage = "";
            return true;
        }

        public LS_tblEducation Find(int id)
        {
            return context.LS_tblEducation.Find(id);
        }

        public LS_tblEducationViewModel FindEditModel(int id)
        {
            LS_tblEducationViewModel ret = context.LS_tblEducation
                                        .Where(p => p.LSEducationID == id)
                                        .Select(p => new LS_tblEducationViewModel()
                                        {
                                            LSEducationID = p.LSEducationID,
                                            LSEducationCode = p.LSEducationCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }

        public bool Update(LS_tblEducationViewModel model, out string errorMessage)
        {
            try
            {
                LS_tblEducation modelUpdate = Find(model.LSEducationID);
                modelUpdate.Name = model.Name;
                modelUpdate.VNName = model.VNName;
                modelUpdate.Rank = model.Rank;
                modelUpdate.Used = model.Used;
                modelUpdate.Note = model.Note;
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

        #region Bind DropdownList ================================================================================     

        // dùng cho bind dropdownlist
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNum, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblEducation
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSEducationID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNum - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion ================================================================================================

    }
}
