using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model.LS;
using Eagle.Model;
using Eagle.Core;

namespace Eagle.Repository.LS
{
    public class LS_tblContractTypeRespository
    {
         public EntityDataContext context { get; set; }
         public LS_tblContractTypeRespository(EntityDataContext context)
        {
            this.context = context;
        }

        public IEnumerable<LS_tblContractTypeViewModel> List()
        {
            try
            {
                return context.LS_tblContractType
                            .Select(p => new LS_tblContractTypeViewModel()
                            {
                                LSContractTypeID = p.LSContractTypeID,
                                LSContractTypeCode = p.LSContractTypeCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Note = p.Note,
                                Used = p.Used
                            });
            }
            catch
            {
                return new List<LS_tblContractTypeViewModel>();
            }
        }   

        public List<GradeEntity> GetListForDropDownList()
        {
            try
            {
                return (context.LS_tblContractType.Select(p => new GradeEntity()
                {
                    Id = p.LSContractTypeID,
                    Name = p.Name
                })).ToList();
            }
            catch
            {
                return new List<GradeEntity>();
            }
        }

        public static List<LS_tblContractTypeViewModel> GetList()
        {
           
            using (EntityDataContext context = new EntityDataContext())
            {
                List<LS_tblContractTypeViewModel> lst = new List<LS_tblContractTypeViewModel>();
                lst = context.LS_tblContractType
                            .Select(p => new LS_tblContractTypeViewModel()
                            {
                                LSContractTypeID = p.LSContractTypeID,
                                LSContractTypeCode = p.LSContractTypeCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Note = p.Note,
                                Used = p.Used
                            }).ToList();
                return lst;
            }
          
        }
        public bool IsCodeExists(string strCode)
        {
            var code = context.LS_tblContractType.FirstOrDefault(p => p.LSContractTypeCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblContractTypeViewModel model, out string outMesssage)
        {
            bool result = false;
            try
            {
                if (IsCodeExists(model.LSContractTypeCode))
                {
                    outMesssage = Eagle.Resource.LanguageResource.DuplicateError;
                    result = false;
                }
                else
                {
                    LS_tblContractType add_entity = new LS_tblContractType()
                    {
                        LSContractTypeCode = model.LSContractTypeCode,
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

        public LS_tblContractType Find(int id)
        {
            return context.LS_tblContractType.Find(id);
        }

        public bool Update(LS_tblContractTypeViewModel edit_model, out string errorMessage)
        {
            try
            {
                LS_tblContractType entity = Find(edit_model.LSContractTypeID);
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
            lst = context.LS_tblContractType
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSContractTypeID,
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
