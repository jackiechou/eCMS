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
    public class LS_tblDistrictRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblDistrictRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public List<LS_tblDistrictViewModel> List()
        {
            try
            {
                return (from p in context.LS_tblDistrict
                          join c in context.LS_tblProvince on p.LSProvinceID equals c.LSProvinceID
                          select new LS_tblDistrictViewModel()
                                {
                                    LSDistrictID = p.LSDistrictID,
                                    LSDistrictCode = p.LSDistrictCode,
                                    Name = p.Name,
                                    VNName = p.VNName,
                                    Rank = p.Rank,
                                    Used = p.Used,
                                    Note = p.Note,
                                    strProvinceName = c.Name
                                }).ToList();
            }
            catch
            {
                return new List<LS_tblDistrictViewModel>();
            }
        }

        public List<ProvinceEntity> GetListForDropDownList()
        {
            try
            {
                return (context.LS_tblDistrict.Select(p => new ProvinceEntity()
                {
                    Id = p.LSDistrictID,
                    Name = p.Name
                })).ToList();
            }
            catch
            {
                return new List<ProvinceEntity>();
            }
        }

        public LS_tblDistrictViewModel GetDetails(int? LSDistrictID)
        {
            LS_tblDistrictViewModel entity = null;
            if (LSDistrictID != null && LSDistrictID > 0)
            {
                var query = (from p in context.LS_tblDistrict
                             where p.LSDistrictID == LSDistrictID
                             select new LS_tblDistrictViewModel()
                             {
                                 LSDistrictID = p.LSDistrictID,
                                 LSDistrictCode = p.LSDistrictCode,
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
            var code = context.LS_tblDistrict.FirstOrDefault(p => p.LSDistrictCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblDistrict model, out string outMesssage)
        {
            if (CheckExistCode(model.LSDistrictCode))
            {
                outMesssage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            try
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }
            catch(Exception ex)
            { string strerr = ex.Message; }
            outMesssage = "";
            return true;
        }

        public LS_tblDistrict Find(int id)
        {
            return context.LS_tblDistrict.Find(id);
        }
        public bool Update(LS_tblDistrictViewModel model, out string errorMessage)
        {
            try
            {
                LS_tblDistrictViewModel modelUpdate = FindEditModel(model.LSDistrictID);
                modelUpdate.LSProvinceID = model.LSProvinceID;
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

        public bool Update(LS_tblDistrict modelUpdate)
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
        public LS_tblDistrictViewModel FindEditModel(int id)
        {
            LS_tblDistrictViewModel ret = context.LS_tblDistrict
                                        .Where(p => p.LSDistrictID == id)                                        
                                        .Select(p => new LS_tblDistrictViewModel() {
                                            LSDistrictID = p.LSDistrictID,
                                            LSDistrictCode = p.LSDistrictCode,
                                            LSProvinceID = p.LSProvinceID,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note,
                                            strProvinceName =  p.LS_tblProvince.Name
                                        }).FirstOrDefault();
            return ret;
        }

        #region Bind DropdownList      
        // dùng cho bind dropdownlist
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int? ProvinceID, int pageSize, int pageNum, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblDistrict.Where(c => c.LSProvinceID == ProvinceID && (searchTerm == null || searchTerm == string.Empty || (c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSDistrictID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNum - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion
    }
}
