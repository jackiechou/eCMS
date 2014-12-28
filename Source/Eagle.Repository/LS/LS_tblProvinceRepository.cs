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
    public class LS_tblProvinceRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblProvinceRepository(EntityDataContext context)
        {
            this.context = context;
        }
        
        public IEnumerable<LS_tblProvinceViewModel> List()
        {
            try
            {
                return from p in context.LS_tblProvince
                       join c in context.LS_tblCountry on p.LSCountryID equals c.LSCountryID
                       select new LS_tblProvinceViewModel()
                                {
                                    LSProvinceID = p.LSProvinceID,
                                    LSProvinceCode = p.LSProvinceCode,
                                    Name = p.Name,
                                    VNName = p.VNName,
                                    Rank = p.Rank,
                                    Used = p.Used,
                                    Note = p.Note,
                                    strCountryName = c.Name
                                };
            }
            catch
            {
                return new List<LS_tblProvinceViewModel>();
            }            
        }
        public List<ProvinceEntity> GetListForDropDownList(int LanguageId = 10001)
        {
            try
            {
                if (LanguageId == 124)
                {
                    return (context.LS_tblProvince.Select(p => new ProvinceEntity()
                    {
                        Id = p.LSProvinceID,
                        Name = p.Name
                    })).ToList();
                }
                else
                {
                    return (context.LS_tblProvince.Select(p => new ProvinceEntity()
                    {
                        Id = p.LSProvinceID,
                        Name = p.VNName
                    })).ToList();
                }
            }
            catch
            {
                return new List<ProvinceEntity>();
            }
        }

        public List<LS_tblProvinceViewModel> GetList()
        {
            try
            {
                return (from p in context.LS_tblProvince
                        join c in context.LS_tblCountry on p.LSCountryID equals c.LSCountryID
                        select new LS_tblProvinceViewModel()
                        {
                            LSProvinceID = p.LSProvinceID,
                            LSProvinceCode = p.LSProvinceCode,
                            Name = p.Name,
                            VNName = p.VNName,
                            Rank = p.Rank,
                            Used = p.Used,
                            Note = p.Note,
                            strCountryName = c.Name
                        }).ToList();
            }
            catch
            {
                return new List<LS_tblProvinceViewModel>();
            }
        }

        public LS_tblProvinceViewModel GetDetails(int? LSProvinceID)
        {
            LS_tblProvinceViewModel entity = null;
            if (LSProvinceID != null && LSProvinceID > 0)
            {
                var query = (from p in context.LS_tblProvince
                             where p.LSProvinceID == LSProvinceID
                             select new LS_tblProvinceViewModel()
                             {
                                 LSProvinceID = p.LSProvinceID,
                                 LSProvinceCode = p.LSProvinceCode,
                                 Name = p.Name,
                                 VNName = p.VNName,
                                 Rank = p.Rank,
                                 Used = p.Used,
                                 Note = p.Note
                             }).ToList();
                entity = query.FirstOrDefault(); 
            }
            return entity;
        }

        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblProvince.FirstOrDefault(p => p.LSProvinceCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblProvince model, out string errorMessage)
        {
            if (CheckExistCode(model.LSProvinceCode))
            {
                errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            try
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }
            catch(Exception ex)
            { string strerr = ex.Message; }

            errorMessage = "";
            return true;
        }

        public LS_tblProvince Find(int id)
        {
            return context.LS_tblProvince.Find(id);
        }

        public bool Update(LS_tblProvinceViewModel model, out string errorMessage)
        {
            try
            {
                LS_tblProvince modelUpdate = Find(model.LSProvinceID);
                modelUpdate.LSCountryID = model.LSCountryID;
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

        public bool Update(LS_tblProvince modelUpdate)
        {
            try
            {
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
        public LS_tblProvinceViewModel FindEditModel(int id)
        {
            LS_tblProvinceViewModel ret = context.LS_tblProvince
                                        .Where(p => p.LSProvinceID == id)
                                        .Select(p => new LS_tblProvinceViewModel()
                                        {
                                            LSProvinceID = p.LSProvinceID,
                                            LSProvinceCode = p.LSProvinceCode,
                                            LSCountryID = p.LSCountryID,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note,
                                            strCountryName =  p.LS_tblCountry.Name
                                        }).FirstOrDefault();
            return ret;
        }

        #region Bind DropdownList ===================================================================================================================
        // dùng cho bind dropdownlist
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int? CountryID, int pageSize, int pageNume, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = (from c in context.LS_tblProvince
                      where (c.LSCountryID == CountryID || CountryID == null) && 
                            (searchTerm == null || searchTerm == string.Empty || (c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm)))
                      select new AutoCompleteModel
                           {
                               id = c.LSProvinceID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion =================================================================================================================================

        #region khong dung contryid ================================================================================================================
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNume, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblProvince.Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSProvinceID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion ==============================================================================================================================
    }
}
