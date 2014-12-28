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
    public class LS_tblLocationRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblLocationRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<LS_tblLocationViewModel> ListLocation()
        {
            try
            {
                return context.LS_tblLocation
                            .Select(p => new LS_tblLocationViewModel()
                            {
                                LSLocationID = p.LSLocationID,
                                LSLocationCode = p.LSLocationCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note

                            });
            }catch
            {
                return new List<LS_tblLocationViewModel>();
            }
        }

        public List<LocationEntity> GetListForDropDownList(int? LanguageId)
        {           
            try
            {
                return (context.LS_tblLocation.Select(p => new LocationEntity()
                            {
                                Id = p.LSLocationID,
                                Name = (LanguageId==10001)?p.Name:p.VNName
                            })).ToList();
            }
            catch
            {
                return new List<LocationEntity>();
            }
        }


        public List<LS_tblLocationViewModel> GetList()
        {
            try
            {
                return context.LS_tblLocation
                    //.Where(p => p.Used.Value == true)
                            .Select(p => new LS_tblLocationViewModel()
                            {
                                LSLocationID = p.LSLocationID,
                                LSLocationCode = p.LSLocationCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).ToList();
            }
            catch
            {
                return new List<LS_tblLocationViewModel>();
            }
        }
        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblLocation.FirstOrDefault(p => p.LSLocationCode.Equals(strCode));
            return (code !=null);
        }
        
        public bool AddLocation(LS_tblLocation model, out string outMesssage)
        {
            if (CheckExistCode(model.LSLocationCode))
            {
                outMesssage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            outMesssage = "";
            return true;
        }



        public LS_tblLocation Find(int id)
        {
            return context.LS_tblLocation.Find(id);
        }

        public LS_tblLocationViewModel FindEditModel(int id)
        {
            LS_tblLocationViewModel ret = context.LS_tblLocation
                                        .Where(p => p.LSLocationID == id)
                                        .Select(p => new LS_tblLocationViewModel()
                                        {
                                            LSLocationID = p.LSLocationID,
                                            LSLocationCode = p.LSLocationCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }
        public bool Update(LS_tblLocationViewModel locationModel, out string errorMessage)
        {
            try
            {
                LS_tblLocation modelUpdate = Find(locationModel.LSLocationID);
                modelUpdate.Name = locationModel.Name;
                modelUpdate.VNName = locationModel.VNName;
                modelUpdate.Rank = locationModel.Rank;
                modelUpdate.Used = locationModel.Used;
                modelUpdate.Note = locationModel.Note;
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
            lst = context.LS_tblLocation
                             .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                             .OrderBy(c => c.Rank)
                             .Select(c => new AutoCompleteModel
                             {
                                 id = c.LSLocationID,
                                 name = (LanguageId == 124) ? c.Name : c.VNName,
                                 text = c.Note
                             }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion =============================================================================================
    }
    
}
