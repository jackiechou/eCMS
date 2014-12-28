using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class ChangingInsuranceInformationTypeRepository
    {
        public EntityDataContext context { get; set; }
        public ChangingInsuranceInformationTypeRepository(EntityDataContext context)
        {
            this.context = context;
        }

      
        public List<AutoCompleteModel> ListDropdown(string searchTerm, int pageSize, int pageNum)
        {
            var ret = context.SI_tblChangingInsuranceInformationType
                          .Where(p => p.VNName.Contains(searchTerm) || p.Name.Contains(searchTerm))
                          .Select(c => new AutoCompleteModel()
                          {
                              id = c.ChangeTypeID,
                              name = c.Name,
                              text = c.VNName

                          }).OrderBy(c => c.name)
                           .Skip(pageSize * (pageNum - 1))
                           .Take(pageSize)
                           .ToList();
            return ret;
        }
    }
}
