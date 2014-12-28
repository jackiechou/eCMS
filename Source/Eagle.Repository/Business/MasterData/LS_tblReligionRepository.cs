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
    public class LS_tblReligionRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblReligionRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<LS_tblReligionViewModel> List()
        {
            try
            {
                return context.LS_tblReligion
                            .Select(p => new LS_tblReligionViewModel()
                            {
                                LSReligionID = p.LSReligionID,
                                LSReligionCode = p.LSReligionCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            });
            }
            catch
            {
                return new List<LS_tblReligionViewModel>();
            }
        }

        public List<ReligionEntity> GetListForDropDownList(int LanguageId = 10001)
        {
            try
            {
                if (LanguageId == 124)
                {
                    return (context.LS_tblReligion.Select(p => new ReligionEntity()
                    {
                        Id = p.LSReligionID,
                        Name = p.Name
                    })).ToList();
                }
                else
                {
                    return (context.LS_tblReligion.Select(p => new ReligionEntity()
                    {
                        Id = p.LSReligionID,
                        Name = p.VNName
                    })).ToList();
                }
            }
            catch
            {
                return new List<ReligionEntity>();
            }
        }

        public List<LS_tblReligionViewModel> GetList()
        {
            try
            {
                return context.LS_tblReligion
                            .Select(p => new LS_tblReligionViewModel()
                            {
                                LSReligionID = p.LSReligionID,
                                LSReligionCode = p.LSReligionCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).ToList();
            }
            catch
            {
                return new List<LS_tblReligionViewModel>();
            }
        }
        
        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblReligion.FirstOrDefault(p => p.LSReligionCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblReligion model , out string outMesssage)
        {
            if (CheckExistCode(model.LSReligionCode))
            {
                outMesssage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            outMesssage = "";
            return true;
        }

        public LS_tblReligion Find(int id)
        {
            return context.LS_tblReligion.Find(id);
        }

        public LS_tblReligionViewModel FindEditModel(int id)
        {
            LS_tblReligionViewModel ret = context.LS_tblReligion
                                        .Where(p => p.LSReligionID == id)
                                        .Select(p => new LS_tblReligionViewModel()
                                        {
                                            LSReligionID = p.LSReligionID,
                                            LSReligionCode = p.LSReligionCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }

        public bool Update(LS_tblReligionViewModel model, out string errorMessage)
        {
            try
            {
                LS_tblReligion modelUpdate = Find(model.LSReligionID);
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

        #region Bind DropdownList ==================================================================================       
        // dùng cho bind dropdownlist
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNume, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblReligion
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSReligionID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion ===============================================================================================
    }
}
