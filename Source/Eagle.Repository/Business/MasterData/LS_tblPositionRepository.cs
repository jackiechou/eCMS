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
    public class LS_tblPositionRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblPositionRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<LS_tblPositionViewModel> List()
        {
            try
            {
                return context.LS_tblPosition
                            .Select(p => new LS_tblPositionViewModel()
                            {
                                LSPositionID = p.LSPositionID,
                                LSPositionCode = p.LSPositionCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note,
                                strUsed = p.Used == true ? "X" : ""

                            });
            }
            catch
            {
                return new List<LS_tblPositionViewModel>();
            }
        }
        public List<PositionEntity> GetListForDropDownList(int? LanguageId)
        {
            try
            {
                return (context.LS_tblPosition.Select(p => new PositionEntity()
                {
                    Id = p.LSPositionID,
                    Name = (LanguageId == 124)?p.Name:p.VNName
                })).ToList();
            }
            catch
            {
                return new List<PositionEntity>();
            }
        }
        public List<LS_tblPositionViewModel> GetList()
        {
            try
            {
                return context.LS_tblPosition
                            .Select(p => new LS_tblPositionViewModel()
                            {
                                LSPositionID = p.LSPositionID,
                                LSPositionCode = p.LSPositionCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).ToList();
            }
            catch
            {
                return new List<LS_tblPositionViewModel>();
            }
        }
        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblPosition.FirstOrDefault(p => p.LSPositionCode.Equals(strCode));
            return (code != null);
        }

        public string Add(LS_tblPosition model)
        {
            if (CheckExistCode(model.LSPositionCode))
            {
                string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                return errorMessage;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            return "success";
        }



        public LS_tblPosition Find(int id)
        {
            return context.LS_tblPosition.Find(id);
        }

        public bool Update(LS_tblPositionViewModel positionModel, out string errorMessage)
        {
            try
            {
                LS_tblPosition modelUpdate = Find(positionModel.LSPositionID);
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

        public LS_tblPositionViewModel FindEditModel(int id)
        {
            LS_tblPositionViewModel ret = context.LS_tblPosition
                                        .Where(p => p.LSPositionID == id)
                                        .Select(p => new LS_tblPositionViewModel()
                                        {
                                            LSPositionID = p.LSPositionID,
                                            LSPositionCode = p.LSPositionCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note
                                        }).FirstOrDefault();
            return ret;
        }

        #region Bind DropdownList ======================================================================================       
        // dùng cho bind dropdownlist
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNume, int? LanguageId)
        {
            List<AutoCompleteModel> lst = new List<AutoCompleteModel>();
            lst = context.LS_tblPosition
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new AutoCompleteModel
                           {
                               id = c.LSPositionID,
                               name = (LanguageId == 124) ? c.Name : c.VNName,
                               text = c.VNName
                           }).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
            return lst;
        }
        #endregion ====================================================================================================
    }
}
