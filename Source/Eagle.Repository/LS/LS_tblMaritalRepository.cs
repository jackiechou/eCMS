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
    public class LS_tblMaritalRepository
    {
        public EntityDataContext context { get; set; }
        public LS_tblMaritalRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<LS_tblMaritalViewModel> List()
        {
            try
            {
                return context.LS_tblMarital
                            .Select(p => new LS_tblMaritalViewModel()
                            {
                                LSMaritalID = p.LSMaritalID,
                                LSMaritalCode = p.LSMaritalCode,
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
                return new List<LS_tblMaritalViewModel>();
            }
        }

        public List<MaritalEntity> GetListForDropDownList(int LanguageId = 10001)
        {
            List<MaritalEntity> lst =new List<MaritalEntity>();            
            lst = (context.LS_tblMarital.Select(p => new MaritalEntity()
            {
                Id = p.LSMaritalID,
                Name = (LanguageId == 124) ? p.Name : p.VNName
            })).ToList();
            return lst;
        }

        public List<LS_tblMaritalViewModel> GetList()
        {
            try
            {
                return context.LS_tblMarital
                            .Select(p => new LS_tblMaritalViewModel()
                            {
                                LSMaritalID = p.LSMaritalID,
                                LSMaritalCode = p.LSMaritalCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note,
                                strUsed = p.Used == true ? "X" : ""

                            }).ToList();
            }
            catch
            {
                return new List<LS_tblMaritalViewModel>();
            }
        }
        public bool CheckExistCode(string strCode)
        {
            var code = context.LS_tblMarital.FirstOrDefault(p => p.LSMaritalCode.Equals(strCode));
            return (code != null);
        }

        public bool Add(LS_tblMarital model, out string outMesssage)
        {
            if (CheckExistCode(model.LSMaritalCode))
            {
                outMesssage = Eagle.Resource.LanguageResource.ExistCode;
                return false;
            }
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            outMesssage = "";
            return true;
        }

        public LS_tblMarital Find(int id)
        {
            return context.LS_tblMarital.Find(id);
        }
        public LS_tblMaritalViewModel FindEditModel(int id)
        {
            LS_tblMaritalViewModel ret = context.LS_tblMarital
                                        .Where(p => p.LSMaritalID == id)
                                        .Select(p => new LS_tblMaritalViewModel()
                                        {
                                            LSMaritalID = p.LSMaritalID,
                                            LSMaritalCode = p.LSMaritalCode,
                                            Name = p.Name,
                                            VNName = p.VNName,
                                            Rank = p.Rank,
                                            Used = p.Used,
                                            Note = p.Note,
                                            strUsed = p.Used == true ? "X" : "",
                                        }).FirstOrDefault();
            return ret;
        }
        public bool Update(LS_tblMaritalViewModel model, out string errorMessage)
        {
            try
            {
                LS_tblMarital modelUpdate = Find(model.LSMaritalID);
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
    }
}
