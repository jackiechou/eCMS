using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Model;
using Eagle.Model.LS;

namespace Eagle.Repository.LS
{
    public class LS_tblTrainingTypeRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblTrainingTypeRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<LS_tblTrainingTypeViewModel> List()
        {
            try
            {
                return context.LS_tblTrainingType
                            .Select(p => new LS_tblTrainingTypeViewModel()
                            {
                                LSTrainingTypeID = p.LSTrainingTypeID,
                                LSTrainingTypeCode = p.LSTrainingTypeCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Note = p.Note,
                                Used = p.Used
                            });
            }
            catch
            {
                return new List<LS_tblTrainingTypeViewModel>();
            }
        }

        public List<GradeEntity> GetListForDropDownList()
        {
            try
            {
                return (context.LS_tblTrainingType.Select(p => new GradeEntity()
                {
                    Id = p.LSTrainingTypeID,
                    Name = p.Name
                })).ToList();
            }
            catch
            {
                return new List<GradeEntity>();
            }
        }

        public List<LS_tblTrainingTypeViewModel> GetList()
        {
            try
            {
                return context.LS_tblTrainingType
                           .Select(p => new LS_tblTrainingTypeViewModel()
                           {
                               LSTrainingTypeID = p.LSTrainingTypeID,
                               LSTrainingTypeCode = p.LSTrainingTypeCode,
                               Name = p.Name,
                               VNName = p.VNName,
                               Rank = p.Rank,
                               Note = p.Note,
                               Used = p.Used
                           }).ToList();
            }
            catch
            {
                return new List<LS_tblTrainingTypeViewModel>();
            }
        }
        public bool IsCodeExists(string strCode)
        {
            var code = context.LS_tblTrainingType.FirstOrDefault(p => p.LSTrainingTypeCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblTrainingTypeViewModel model, out string outMesssage)
        {
            bool result = false;
            try
            {
                if (IsCodeExists(model.LSTrainingTypeCode))
                {
                    outMesssage = Eagle.Resource.LanguageResource.DuplicateError;
                    result = false;
                }
                else
                {
                    LS_tblTrainingType add_entity = new LS_tblTrainingType()
                    {
                        LSTrainingTypeCode = model.LSTrainingTypeCode,
                        Name = model.Name,
                        VNName = model.VNName,
                        Rank = model.Rank,
                        Used = model.Used,
                        Note = model.Note
                    };

                    context.Entry(add_entity).State = System.Data.Entity.EntityState.Added;
                    context.SaveChanges();
                    result = true;
                    outMesssage = Eagle.Resource.LanguageResource.CreateSuccess;
                }
            }
            catch (Exception ex)
            {
                outMesssage = ex.ToString();
                result = false;
            }
            return result;
        }

        public LS_tblTrainingType Find(int id)
        {
            return context.LS_tblTrainingType.Find(id);
        }

        public bool Update(LS_tblTrainingTypeViewModel edit_model, out string errorMessage)
        {
            try
            {
                LS_tblTrainingType entity = Find(edit_model.LSTrainingTypeID);
                entity.Name = edit_model.Name;
                entity.VNName = edit_model.VNName;
                entity.Rank = edit_model.Rank;
                entity.Note = edit_model.Note;
                entity.Used = edit_model.Used;
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
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
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNum, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblTrainingType
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSTrainingTypeID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNum - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion ==================================================================================================
    }
}
