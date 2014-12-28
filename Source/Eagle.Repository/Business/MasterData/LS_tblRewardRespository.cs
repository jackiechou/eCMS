using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;
using Eagle.Model.LS;

namespace Eagle.Repository.LS
{
    public class LS_tblRewardRespository
    {
         public EntityDataContext context { get; set; }
         public LS_tblRewardRespository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<LS_tblRewardViewModel> List()
        {
            try
            {
                return context.LS_tblReward
                            .Select(p => new LS_tblRewardViewModel()
                            {
                                LSRewardID = p.LSRewardID,
                                LSRewardCode = p.LSRewardCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            });
            }
            catch
            {
                return new List<LS_tblRewardViewModel>();
            }
        }

        public List<RewardEntity> GetListForDropDownList(int? Type, int? LanguageId = 10001)
        {
            List<RewardEntity> lst = new List<RewardEntity>();           
            lst = (context.LS_tblReward.Where(p => p.Type == Type).Select(p => new RewardEntity()
            {
                Id = p.LSRewardID,
                Name = (LanguageId == 124) ? p.Name : p.VNName
            })).ToList();
            return lst;
        }
        public List<LS_tblRewardViewModel> GetList()
        {
            try
            {
                return context.LS_tblReward
                            .Select(p => new LS_tblRewardViewModel()
                            {
                                LSRewardID = p.LSRewardID,
                                LSRewardCode = p.LSRewardCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).ToList();
            }
            catch
            {
                return new List<LS_tblRewardViewModel>();
            }
        }
        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblReward.FirstOrDefault(p => p.LSRewardCode.Equals(strCode));
            return (code != null);
        }

        public string Add(LS_tblReward model)
        {
            if (CheckExistCode(model.LSRewardCode))
            {
                string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                return errorMessage;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            return "success";
        }



        public LS_tblReward Find(int id)
        {
            return context.LS_tblReward.Find(id);
        }

        public bool Update(LS_tblRewardViewModel positionModel, out string errorMessage)
        {
            try
            {
                LS_tblReward modelUpdate = Find(positionModel.LSRewardID);
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

        public LS_tblRewardViewModel FindEditModel(int id)
        {
            LS_tblRewardViewModel ret = context.LS_tblReward
                                        .Where(p => p.LSRewardID == id)
                                        .Select(p => new LS_tblRewardViewModel()
                                        {
                                            LSRewardID = p.LSRewardID,
                                            LSRewardCode = p.LSRewardCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }

        #region Bind DropdownList ================================================================================================       
        // dùng cho bind dropdownlist
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int? Type, int pageSize, int pageNum, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblReward.Where(c => c.Type == Type && c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSRewardID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNum - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion ===============================================================================================================
    }
}
