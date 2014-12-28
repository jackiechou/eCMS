using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
//
using Eagle.Model;
using Eagle.Core;
namespace Eagle.Repository
{
    public class LS_tblSchoolRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblSchoolRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<LS_tblSchoolViewModel> List()
        {
            try
            {
                return context.LS_tblSchool
                            .Select(p => new LS_tblSchoolViewModel()
                            {
                                LSSchoolID = p.LSSchoolID,
                                LSSchoolCode = p.LSSchoolCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            });
            }
            catch
            {
                return new List<LS_tblSchoolViewModel>();
            }
        }

        public List<SchoolEntity> GetListForDropDownList()
        {
            try
            {
                return (context.LS_tblSchool.Select(p => new SchoolEntity()
                {
                    Id = p.LSSchoolID,
                    Name = p.Name
                })).ToList();
            }
            catch
            {
                return new List<SchoolEntity>();
            }
        }

        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblSchool.FirstOrDefault(p => p.LSSchoolCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblSchool model, out string outMesssage)
        {
            if (CheckExistCode(model.LSSchoolCode))
            {
                outMesssage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            outMesssage = "";
            return true;
        }

        public LS_tblSchool Find(int id)
        {
            return context.LS_tblSchool.Find(id);
        }
        public LS_tblSchoolViewModel FindEditModel(int id)
        {
            LS_tblSchoolViewModel ret = context.LS_tblSchool
                                        .Where(p => p.LSSchoolID == id)
                                        .Select(p => new LS_tblSchoolViewModel()
                                        {
                                            LSSchoolID = p.LSSchoolID,
                                            LSSchoolCode = p.LSSchoolCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }
        public bool Update(LS_tblSchoolViewModel model, out string errorMessage)
        {
            try
            {
                LS_tblSchool modelUpdate = Find(model.LSSchoolID);
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
            lst = context.LS_tblSchool
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSSchoolID,
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
