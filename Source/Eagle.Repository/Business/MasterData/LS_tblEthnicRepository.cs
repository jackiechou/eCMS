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
    public class LS_tblEthnicRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblEthnicRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<LS_tblEthnicViewModel> List()
        {
            try
            {
                return context.LS_tblEthnic
                            .Select(p => new LS_tblEthnicViewModel()
                            {
                                LSEthnicID = p.LSEthnicID,
                                LSEthnicCode = p.LSEthnicCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note

                            });
            }
            catch
            {
                return new List<LS_tblEthnicViewModel>();
            }
        }
        public List<EthnicEntity> GetListForDropDownList(int LanguageId = 10001)
        {
            try
            {
                if (LanguageId == 124)
                {
                    return (context.LS_tblEthnic.Select(p => new EthnicEntity()
                    {
                        Id = p.LSEthnicID,
                        Name = p.Name
                    })).ToList();
                }
                else
                {

                    return (context.LS_tblEthnic.Select(p => new EthnicEntity()
                    {
                        Id = p.LSEthnicID,
                        Name = p.VNName
                    })).ToList();
                }
            }
            catch
            {
                return new List<EthnicEntity>();
            }
        }
        public List<LS_tblEthnicViewModel> GetList()
        {
            try
            {
                return context.LS_tblEthnic.Select(p => new LS_tblEthnicViewModel()
                            {
                                LSEthnicID = p.LSEthnicID,
                                LSEthnicCode = p.LSEthnicCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note

                            }).ToList();
            }
            catch
            {
                return new List<LS_tblEthnicViewModel>();
            }
        }
        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblEthnic.FirstOrDefault(p => p.LSEthnicCode.Equals(strCode));
            return (code != null);
        }
        public bool Add(LS_tblEthnic model, out string outMesssage)
        {
            if (CheckExistCode(model.LSEthnicCode))
            {
                outMesssage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            outMesssage = "";
            return true;
        }
        public LS_tblEthnic Find(int id)
        {
            return context.LS_tblEthnic.Find(id);
        }
        public LS_tblEthnicViewModel FindEditModel(int id)
        {
            LS_tblEthnicViewModel ret = context.LS_tblEthnic
                                        .Where(p => p.LSEthnicID == id)
                                        .Select(p => new LS_tblEthnicViewModel()
                                        {
                                            LSEthnicID = p.LSEthnicID,
                                            LSEthnicCode = p.LSEthnicCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }
        public bool Update(LS_tblEthnicViewModel model, out string errorMessage)
        {
            try
            {
                LS_tblEthnic modelUpdate = Find(model.LSEthnicID);
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

        #region Bind DropdownList       
        // dùng cho bind dropdownlist
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNum, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblEthnic.Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSEthnicID,
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
