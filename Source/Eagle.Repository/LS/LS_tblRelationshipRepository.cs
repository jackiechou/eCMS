using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;
using Eagle.Model.LS;

namespace Eagle.Repository.LS
{
    public class LS_tblRelationshipRepository
    {
         public EntityDataContext context { get; set; }
         public LS_tblRelationshipRepository(EntityDataContext context)
        {
            this.context = context;
        }
         public string GenerateCode(int num)
         {
             System.Nullable<Int32> Max_ID = (from u in context.LS_tblRelationship select u.LSRelationshipID).DefaultIfEmpty(0).Max() + 1;
             string _MaxID = Eagle.Common.Utilities.StringUtils.GenerateCode(Max_ID.ToString(), num);
             return _MaxID;
         }
        public IEnumerable<LS_tblRelationshipViewModel> List()
        {
            try
            {
                return context.LS_tblRelationship
                            .Select(p => new LS_tblRelationshipViewModel()
                            {
                                LSRelationshipID = p.LSRelationshipID,
                                LSRelationshipCode = p.LSRelationshipCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            });
            }
            catch
            {
                return new List<LS_tblRelationshipViewModel>();
            }
        }

        public List<RelationshipEntity> GetListForDropDownList(int? LanguageId = 10001)
        {
            try
            {
                if (LanguageId == 124)
                {
                    return (context.LS_tblRelationship.Select(p => new RelationshipEntity()
                    {
                        Id = p.LSRelationshipID,
                        Name = p.Name
                    })).ToList();
                }
                else
                {
                    return (context.LS_tblRelationship.Select(p => new RelationshipEntity()
                    {
                        Id = p.LSRelationshipID,
                        Name = p.VNName
                    })).ToList();
                }
            }
            catch
            {
                return new List<RelationshipEntity>();
            }
        }

        public List<LS_tblRelationshipViewModel> GetList()
        {
            try
            {
                return context.LS_tblRelationship
                            .Select(p => new LS_tblRelationshipViewModel()
                            {
                                LSRelationshipID = p.LSRelationshipID,
                                LSRelationshipCode = p.LSRelationshipCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).ToList();
            }
            catch
            {
                return new List<LS_tblRelationshipViewModel>();
            }
        }
        
        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblRelationship.FirstOrDefault(p => p.LSRelationshipCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblRelationshipViewModel model , out string outMesssage)
        {
            if (CheckExistCode(model.LSRelationshipCode))
            {
                outMesssage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            LS_tblRelationship entity = new LS_tblRelationship()
            {
                LSRelationshipCode = model.LSRelationshipCode,
                Name = model.Name,
                VNName = model.VNName,
                Rank = model.Rank,
                Used = model.Used,
                Note = model.Note
            };
            context.Entry(entity).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            outMesssage = "";
            return true;
        }

        public LS_tblRelationship Find(int id)
        {
            return context.LS_tblRelationship.Find(id);
        }

        public LS_tblRelationshipViewModel FindEditModel(int id)
        {
            LS_tblRelationshipViewModel ret = context.LS_tblRelationship
                                        .Where(p => p.LSRelationshipID == id)
                                        .Select(p => new LS_tblRelationshipViewModel()
                                        {
                                            LSRelationshipID = p.LSRelationshipID,
                                            LSRelationshipCode = p.LSRelationshipCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }

        public bool Update(LS_tblRelationshipViewModel model, out string errorMessage)
        {
            try
            {
                LS_tblRelationship modelUpdate = Find(model.LSRelationshipID);
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

        #region Bind DropdownList ======================================================================================       
        // dùng cho bind dropdownlist
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNume, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblRelationship
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSRelationshipID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion ===================================================================================================
    }
}
