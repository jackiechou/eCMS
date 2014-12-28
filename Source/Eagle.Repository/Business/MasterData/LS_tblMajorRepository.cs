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
    public class LS_tblMajorRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblMajorRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<LS_tblMajorViewModel> List()
        {
            try
            {
                return context.LS_tblMajor
                            .Select(p => new LS_tblMajorViewModel()
                            {
                                LSMajorID = p.LSMajorID,
                                LSMajorCode = p.LSMajorCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            });
            }
            catch
            {
                return new List<LS_tblMajorViewModel>();
            }
        }
        public List<MajorEntity> GetListForDropDownList()
        {
            try
            {
                return (context.LS_tblMajor.Select(p => new MajorEntity()
                {
                    Id = p.LSMajorID,
                    Name = p.Name
                })).ToList();
            }
            catch
            {
                return new List<MajorEntity>();
            }
        }
        public List<LS_tblMajorViewModel> GetList()
        {
            try
            {
                return context.LS_tblMajor
                            .Select(p => new LS_tblMajorViewModel()
                            {
                                LSMajorID = p.LSMajorID,
                                LSMajorCode = p.LSMajorCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).ToList();
            }
            catch
            {
                return new List<LS_tblMajorViewModel>();
            }
        }

        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblMajor.FirstOrDefault(p => p.LSMajorCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblMajor model, out string outMesssage)
        {
            if (CheckExistCode(model.LSMajorCode))
            {
                outMesssage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            outMesssage = "";
            return true;
        }

        public LS_tblMajor Find(int id)
        {
            return context.LS_tblMajor.Find(id);
        }
        public LS_tblMajorViewModel FindEditModel(int id)
        {
            LS_tblMajorViewModel ret = context.LS_tblMajor
                                        .Where(p => p.LSMajorID == id)
                                        .Select(p => new LS_tblMajorViewModel()
                                        {
                                            LSMajorID = p.LSMajorID,
                                            LSMajorCode = p.LSMajorCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }
        public bool Update(LS_tblMajorViewModel model, out string outMesssage)
        {
            try
            {
                LS_tblMajor modelUpdate = Find(model.LSMajorID);
                modelUpdate.Name = model.Name;
                modelUpdate.VNName = model.VNName;
                modelUpdate.Rank = model.Rank;
                modelUpdate.Used = model.Used;
                modelUpdate.Note = model.Note;
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                outMesssage = "";
                return true;
            }
            catch
            {
                outMesssage = "Error";
                return false;
            }
        }

        #region Bind DropdownList ========================================================================       
        // dùng cho bind dropdownlist
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNume, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblMajor
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSMajorID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
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
