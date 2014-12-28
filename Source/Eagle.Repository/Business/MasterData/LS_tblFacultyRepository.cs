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
    public class LS_tblFacultyRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblFacultyRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<LS_tblFacultyViewModel> List()
        {
            try
            {
                return context.LS_tblFaculty
                            .Select(p => new LS_tblFacultyViewModel()
                            {
                                LSFacultyID = p.LSFacultyID,
                                LSFacultyCode = p.LSFacultyCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            });
            }
            catch
            {
                return new List<LS_tblFacultyViewModel>();
            }
        }

        public List<FacultyEntity> GetListForDropDownList()
        {
            try
            {
                return (context.LS_tblFaculty.Select(p => new FacultyEntity()
                {
                    Id = p.LSFacultyID,
                    Name = p.Name
                })).ToList();
            }
            catch
            {
                return new List<FacultyEntity>();
            }
        }
        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblFaculty.FirstOrDefault(p => p.LSFacultyCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblFaculty model, out string outMesssage)
        {
            if (CheckExistCode(model.LSFacultyCode))
            {
                outMesssage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            outMesssage = "";
            return true;
        }

        public LS_tblFaculty Find(int id)
        {
            return context.LS_tblFaculty.Find(id);
        }
        public LS_tblFacultyViewModel FindEditModel(int id)
        {
            LS_tblFacultyViewModel ret = context.LS_tblFaculty
                                        .Where(p => p.LSFacultyID == id)
                                        .Select(p => new LS_tblFacultyViewModel()
                                        {
                                            LSFacultyID = p.LSFacultyID,
                                            LSFacultyCode = p.LSFacultyCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }
        public bool Update(LS_tblFacultyViewModel model, out string errorMessage)
        {
            try
            {
                LS_tblFaculty modelUpdate = Find(model.LSFacultyID);
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
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNum, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblFaculty
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSFacultyID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNum - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion =================================================================================================
    }
}
