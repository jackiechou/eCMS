using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;
using Eagle.Model.LS;

namespace Eagle.Repository.LS
{
    public class LS_tblRewardLevelRespository
    {
         public EntityDataContext context { get; set; }
         public LS_tblRewardLevelRespository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<LS_tblRewardLevelViewModel> List()
        {
            try
            {
                return context.LS_tblRewardLevel
                            .Select(p => new LS_tblRewardLevelViewModel()
                            {
                                LSRewardLevelID = p.LSRewardLevelID,
                                LSRewardLevelCode = p.LSRewardLevelCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            });
            }
            catch
            {
                return new List<LS_tblRewardLevelViewModel>();
            }
        }
        public List<RewardLevelEntity> GetListForDropDownList(int? Type, int? LanguageId = 10001)
        {
            try
            {
                if (LanguageId == 124)
                {
                    return (context.LS_tblRewardLevel.Where(p => p.Type == Type).Select(p => new RewardLevelEntity()
                    {
                        Id = p.LSRewardLevelID,
                        Name = p.Name
                    })).ToList();
                }
                else
                {
                    return (context.LS_tblRewardLevel.Where(p => p.Type == Type).Select(p => new RewardLevelEntity()
                    {
                        Id = p.LSRewardLevelID,
                        Name = p.VNName
                    })).ToList();
                }
            }
            catch
            {
                return new List<RewardLevelEntity>();
            }
        }
        public List<LS_tblRewardLevelViewModel> GetList()
        {
            try
            {
                return context.LS_tblRewardLevel
                            .Select(p => new LS_tblRewardLevelViewModel()
                            {
                                LSRewardLevelID = p.LSRewardLevelID,
                                LSRewardLevelCode = p.LSRewardLevelCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).ToList();
            }
            catch
            {
                return new List<LS_tblRewardLevelViewModel>();
            }
        }
        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblRewardLevel.FirstOrDefault(p => p.LSRewardLevelCode.Equals(strCode));
            return (code != null);
        }

        public string Add(LS_tblRewardLevel model)
        {
            if (CheckExistCode(model.LSRewardLevelCode))
            {
                string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                return errorMessage;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            return "success";
        }



        public LS_tblRewardLevel Find(int id)
        {
            return context.LS_tblRewardLevel.Find(id);
        }

        public bool Update(LS_tblRewardLevelViewModel positionModel, out string errorMessage)
        {
            try
            {
                LS_tblRewardLevel modelUpdate = Find(positionModel.LSRewardLevelID);
                modelUpdate.Name = positionModel.Name;
                modelUpdate.VNName = positionModel.VNName;
                modelUpdate.Rank = positionModel.Rank;
                modelUpdate.Used = positionModel.Used;
                modelUpdate.Note = positionModel.Note;
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

        public LS_tblRewardLevelViewModel FindEditModel(int id)
        {
            LS_tblRewardLevelViewModel ret = context.LS_tblRewardLevel
                                        .Where(p => p.LSRewardLevelID == id)
                                        .Select(p => new LS_tblRewardLevelViewModel()
                                        {
                                            LSRewardLevelID = p.LSRewardLevelID,
                                            LSRewardLevelCode = p.LSRewardLevelCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }

        #region Bind DropdownList =======================================================================================================      
        // dùng cho bind dropdownlist
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int? Type, int pageSize, int pageNum, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblRewardLevel.Where(c => c.Type == Type && c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSRewardLevelID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNum - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion =====================================================================================================================
    }
}
