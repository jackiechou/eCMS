using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class HospitalRepository
    {
        public EntityDataContext context { get; set; }
        public HospitalRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<HospitalViewModel> GetAll(int LanguageId = 10001)
        {
            var result = from ic in context.LS_tblHospital
                         join type in context.LS_tblProvince on ic.LSProvinceID equals type.LSProvinceID
                         orderby ic.Rank 
                         select new HospitalViewModel()
                         {
                             LSHospitalCode = ic.LSHospitalCode,
                             LSHospitalID = ic.LSHospitalID,
                             LSProvinceID = ic.LSProvinceID,
                             Name = (LanguageId == 124 ? ic.Name : ic.VNName),
                             Rank = ic.Rank,
                             Used = ic.Used,
                             Note = ic.Note,
                             LSProvinceName = (LanguageId == 124 ? type.Name : type.VNName)
                         };
            return result.ToList();
        }

        public bool Add(LS_tblHospital model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                //Kiểm tra xem Quy trình này đã được thiết lập chưa
                if (CodeIsExists(model.LSHospitalCode))
                {
                    ErrorMessage = Eagle.Resource.LanguageResource.CodeIsExists;
                    return false;
                }

                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        private bool CodeIsExists(string LSHospitalCode, int? LSHospitalID = null)
        {
            if (LSHospitalID == null)
            {
                var OnlineProcess = context
                            .LS_tblHospital
                            .FirstOrDefault(p => p.LSHospitalCode == LSHospitalCode);
                return OnlineProcess != null;
            }
            else
            {
                var OnlineProcess = context
                            .LS_tblHospital
                            .FirstOrDefault(p => p.LSHospitalCode == LSHospitalCode &&
                                p.LSHospitalID != LSHospitalID);
                return OnlineProcess != null;
            }
        }

        public LS_tblHospital Find(int id)
        {
            return context.LS_tblHospital.Find(id);
        }

        public HospitalViewModel FindEdit(int id, int LanguageId = 10001)
        {
            var result = (from ic in context.LS_tblHospital
                          join type in context.LS_tblProvince on ic.LSProvinceID equals type.LSProvinceID
                          where ic.LSHospitalID == id
                          select new HospitalViewModel() {
                              LSHospitalID = ic.LSHospitalID,
                              LSHospitalCode = ic.LSHospitalCode,
                              LSProvinceID = ic.LSProvinceID,
                              Name = ic.Name,
                              VNName = ic.VNName,
                              Rank = ic.Rank,
                              Used = ic.Used,
                              Note = ic.Note,
                              LSProvinceName = (LanguageId == 124 ? type.Name : type.VNName)
                          }).FirstOrDefault();
            return result;
        }
        public bool Update(LS_tblHospital model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                if (CodeIsExists(model.LSHospitalCode, model.LSHospitalID))
                {
                    ErrorMessage = Eagle.Resource.LanguageResource.CodeIsExists;
                    return false;
                }
                context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool Delete(int id, out string ErrorMessage)
        {
            try
            {
                var model = Find(id);
                if (model != null)
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
                ErrorMessage = "";
                return true;
            }
            catch( Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

        }

        #region Bind DropdownList 
        private List<AutoCompleteViewModel> GetDropdownList(string searchTerm)
        {
            var tmp = context.LS_tblHospital
                           .Where(c => c.Name.Contains(searchTerm))
                           .OrderBy(c => c.Rank)
                           .Select(c => new
                           {
                               id = c.LSHospitalID,
                               name = c.Name + (c.Used == true ? "" : Eagle.Resource.LanguageResource.NotUsed),
                               text = c.Name + (c.Used == true ? "" : Eagle.Resource.LanguageResource.NotUsed)
                           });
            List<AutoCompleteViewModel> ret = new List<AutoCompleteViewModel>();
            foreach (var item in tmp)
            {
                AutoCompleteViewModel p = new AutoCompleteViewModel()
                {
                    id = item.id.ToString(),
                    name = item.name,
                    text = item.text
                };
                ret.Add(p);
            }
            return ret;
        }
        // dùng cho bind dropdownlist goi tu controller
        public List<AutoCompleteViewModel> ListDropdown(string searchTerm, int pageSize, int pageNume)
        {
            return GetDropdownList(searchTerm).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
        }
        #endregion
    }
}
