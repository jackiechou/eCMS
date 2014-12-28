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
    public class LS_tblQualificationRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblQualificationRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<LS_tblQualificationViewModel> List()
        {
            try
            {
                return context.LS_tblQualification
                            .Select(p => new LS_tblQualificationViewModel()
                            {
                                LSQualificationID = p.LSQualificationID,
                                LSQualificationCode = p.LSQualificationCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            });
            }
            catch
            {
                return new List<LS_tblQualificationViewModel>();
            }
        }

        public List<QualificationEntity> GetListForDropDownList()
        {
            try
            {
                return (context.LS_tblQualification.Select(p => new QualificationEntity()
                {
                    Id = p.LSQualificationID,
                    Name = p.Name
                })).ToList();
            }
            catch
            {
                return new List<QualificationEntity>();
            }
        }

        public List<LS_tblQualificationViewModel> GetList()
        {
            try
            {
                return context.LS_tblQualification
                            .Select(p => new LS_tblQualificationViewModel()
                            {
                                LSQualificationID = p.LSQualificationID,
                                LSQualificationCode = p.LSQualificationCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note

                            }).ToList();
            }
            catch
            {
                return new List<LS_tblQualificationViewModel>();
            }
        }
        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblQualification.FirstOrDefault(p => p.LSQualificationCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblQualification model, out string outMesssage)
        {
            if (CheckExistCode(model.LSQualificationCode))
            {
                outMesssage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            outMesssage = "";
            return true;
        }

        public LS_tblQualification Find(int id)
        {
            return context.LS_tblQualification.Find(id);
        }

        public LS_tblQualificationViewModel FindEditModel(int id)
        {
            LS_tblQualificationViewModel ret = context.LS_tblQualification
                                        .Where(p => p.LSQualificationID == id)
                                        .Select(p => new LS_tblQualificationViewModel()
                                        {
                                            LSQualificationID = p.LSQualificationID,
                                            LSQualificationCode = p.LSQualificationCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }

        public bool Update(LS_tblQualificationViewModel model, out string errorMessage)
        {
            try
            {
                LS_tblQualification modelUpdate = Find(model.LSQualificationID);
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
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNume, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblQualification
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSQualificationID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion =================================================================================================

    }
}
