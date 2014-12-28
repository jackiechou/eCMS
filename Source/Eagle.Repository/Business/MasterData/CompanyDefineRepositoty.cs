using Eagle.Core;
using Eagle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Repository
{
    public class CompanyDefineRepository
    {
        public EntityDataContext context { get; set; }
        public CompanyDefineRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<CompanyDefineViewModel> GetAll(int LanguageId)
        {
            return context.LS_tblCompanyDefine.Select(p => new CompanyDefineViewModel()
            {
                LSCompanyDefineID = p.LSCompanyDefineID,
                Name = LanguageId == 124 ? p.Name : p.VNName,
                Parent = p.Parent
            }).OrderBy(p => p.Parent).ToList();
        }
    }
}
