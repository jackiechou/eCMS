using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Eagle.Core;
using Eagle.Model;
namespace Eagle.Repository
{
    public class LS_tblNationalityRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblNationalityRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<LS_tblNationalityViewModel> List()
        {
            try
            {
                return context.LS_tblNationality
                            .Select(p => new LS_tblNationalityViewModel()
                            {
                                LSNationalityID = p.LSNationalityID,
                                LSNationalityCode = p.LSNationalityCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            });
            }
            catch
            {
                return new List<LS_tblNationalityViewModel>();
            }
        }

        public List<NationalityEntity> GetListForDropDownList(int LanguageId = 10001)
        {
            try
            {
                if (LanguageId == 124)
                {
                    return (context.LS_tblNationality.Select(p => new NationalityEntity()
                    {
                        Id = p.LSNationalityID,
                        Name = p.Name
                    })).ToList();
                }
                else
                {
                    return (context.LS_tblNationality.Select(p => new NationalityEntity()
                    {
                        Id = p.LSNationalityID,
                        Name = p.VNName
                    })).ToList();
                }
            }
            catch
            {
                return new List<NationalityEntity>();
            }
        }
        public List<LS_tblNationalityViewModel> GetList()
        {
            try
            {
                return context.LS_tblNationality
                            .Select(p => new LS_tblNationalityViewModel()
                            {
                                LSNationalityID = p.LSNationalityID,
                                LSNationalityCode = p.LSNationalityCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).ToList();
            }
            catch
            {
                return new List<LS_tblNationalityViewModel>();
            }
        }
        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblNationality.FirstOrDefault(p => p.LSNationalityCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblNationality model, out string outMesssage)
        {
            if (CheckExistCode(model.LSNationalityCode))
            {
                outMesssage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            outMesssage = "";
            return true;
        }

        public LS_tblNationality Find(int id)
        {
            return context.LS_tblNationality.Find(id);
        }
        public LS_tblNationalityViewModel FindEditModel(int id)
        {
            LS_tblNationalityViewModel ret = context.LS_tblNationality
                                        .Where(p => p.LSNationalityID == id)
                                        .Select(p => new LS_tblNationalityViewModel()
                                        {
                                            LSNationalityID = p.LSNationalityID,
                                            LSNationalityCode = p.LSNationalityCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }
        public bool Update(LS_tblNationalityViewModel model, out string errorMessage)
        {
            try
            {
                LS_tblNationality modelUpdate = Find(model.LSNationalityID);
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

        #region Bind DropdownList ===================================================================================
        // dùng cho bind dropdownlist
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNume, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblNationality
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSNationalityID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion ==================================================================================================

    }
}
