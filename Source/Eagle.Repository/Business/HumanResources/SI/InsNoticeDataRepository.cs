using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Eagle.Core;
using Eagle.Repository;
using Eagle.Model;
using System.Web.Routing;
using Eagle.Common.Helpers;
using System.Data.Entity.Validation;
namespace Eagle.Repository
{
    public class InsNoticeDataRepository
    {
        public EntityDataContext context { get; set; }
        public InsNoticeDataRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(DateTime dValidate, int EmpID, string SourceID)
        {
            var code = context.SI_tblInsNoticeData.FirstOrDefault(p => p.AriseDate.Equals(dValidate) && p.EmpID == EmpID && p.SourceID == SourceID);
            return (code != null);
        }
        public string Add(List<SI_tblInsNoticeData> modelList)
        {
            try
            {
                var first = modelList.First();
                if (first != null)
                {
                    foreach (var additem in modelList)
                    {
                        if (CheckExist(additem.AriseDate, additem.EmpID, additem.SourceID))
                        {
                            string errorMessage = Eagle.Resource.LanguageResource.ExistData;
                            return errorMessage;
                        }
                        context.Entry(additem).State = System.Data.Entity.EntityState.Added;
                    }
                    context.SaveChanges();
                    
                    return "success";
                }
                return "error";
            }
            catch //(Exception e)
            {
                return "error";
            }
        }
        public SI_tblInsNoticeData Find(int id)
        {
            return context.SI_tblInsNoticeData.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                SI_tblInsNoticeData modelUpdate = Find(id);
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return false;
            }

        }
        #region Bind DropdownList
        private List<AutoCompleteViewModel> GetDropdownList(string searchTerm, int LanguageId)
        {
            var tmp = context.LS_tblNguonBaoBH
                           .Where(c => c.Name.Contains(searchTerm) || c.VNName.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.NguonBaoBHID,
                               name = LanguageId == 124 ? c.Name : c.VNName,
                               text = c.VNName
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
        // dùng cho bind dropdownlist
        public List<AutoCompleteViewModel> ListDropdown(string searchTerm, int pageSize, int pageNume, int LanguageId)
        {
            return GetDropdownList(searchTerm, LanguageId).OrderBy(c => c.name)
                    .Skip(pageSize * (pageNume - 1))
                    .Take(pageSize)
                    .ToList();
        }
        #endregion
    }
}
