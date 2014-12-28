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
    public class LS_tblCountryRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblCountryRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public List<LS_tblCountryViewModel> List()
        {
            try
            {
                return (context.LS_tblCountry
                            .Select(p => new LS_tblCountryViewModel()
                            {
                                LSCountryID = p.LSCountryID,
                                LSCountryCode = p.LSCountryCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            })).ToList();
            }
            catch
            {
                return new List<LS_tblCountryViewModel>();
            }
        }

        public List<CountryEntity> GetListForDropDownList(int LanguageId = 10001)
        {
            try
            {
                if (LanguageId == 124)
                {
                    return (context.LS_tblCountry.Select(p => new CountryEntity()
                    {
                        Id = p.LSCountryID,
                        Name = p.Name
                    })).ToList();
                }
                else
                {
                    return (context.LS_tblCountry.Select(p => new CountryEntity()
                    {
                        Id = p.LSCountryID,
                        Name = p.VNName
                    })).ToList();
                }
            }
            catch
            {
                return new List<CountryEntity>();
            }
        }

        public LS_tblCountryViewModel GetDetails(int? LSCountryID)
        {
             LS_tblCountryViewModel entity = null;
             if (LSCountryID != null && LSCountryID > 0)
             {
                 var query = (context.LS_tblCountry
                                 .Select(p => new LS_tblCountryViewModel()
                                 {
                                     LSCountryID = p.LSCountryID,
                                     LSCountryCode = p.LSCountryCode,
                                     Name = p.Name,
                                     VNName = p.VNName,
                                     Rank = p.Rank,
                                     Used = p.Used,
                                     Note = p.Note
                                 })).ToList();
                 entity = query.FirstOrDefault();
             }
             return entity;
        }


        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblCountry.FirstOrDefault(p => p.LSCountryCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblCountry model, out string errorMessage)
        {
            if (CheckExistCode(model.LSCountryCode))
            {
                errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            errorMessage = "";
            return true;
        }

        public LS_tblCountry Find(int id)
        {
            return context.LS_tblCountry.Find(id);
        }

        public bool Update(LS_tblCountryViewModel model, out string errorMessage)
        {
            try
            {
                LS_tblCountry modelUpdate = Find(model.LSCountryID);
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

        public LS_tblCountryViewModel FindEditModel(int id)
        {
            LS_tblCountryViewModel ret = context.LS_tblCountry
                                        .Where(p => p.LSCountryID == id)
                                        .Select(p => new LS_tblCountryViewModel()
                                        {
                                            LSCountryID = p.LSCountryID,
                                            LSCountryCode = p.LSCountryCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }

        #region Bind DropdownList      
        // dùng cho bind dropdownlist
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNume, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblCountry
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSCountryID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion
    }
}
