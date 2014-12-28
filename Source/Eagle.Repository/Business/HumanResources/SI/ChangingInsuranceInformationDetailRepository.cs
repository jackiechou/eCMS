using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class ChangingInsuranceInformationDetailRepository
    {
        public EntityDataContext context { get; set; }
        public ChangingInsuranceInformationDetailRepository(EntityDataContext context)
        {
            this.context = context;
        }



        public List<D07TSViewModel> GetByMasterID(int? MasterID)
        {
            var result = from d in context.SI_tblChangingInsuranceInformationDetail
                         join t in context.SI_tblChangingInsuranceInformationType on d.ChangeTypeID equals t.ChangeTypeID
                         join e in context.HR_tblEmp on d.EmpID equals e.EmpID
                         join c in context.LS_tblCompany on e.LSCompanyID equals c.LSCompanyID
                          where d.MasterID == MasterID
                          select new D07TSViewModel()
                          {
                              DetailID = d.DetailID,
                              MasterID = d.MasterID,
                              EmpID = d.EmpID,
                              FullName = d.FullName + " - " + c.VNName,
                              SIBook = d.SIBook,
                              ChangeTypeID = d.ChangeTypeID,
                              Old = d.Old,
                              New = d.New,
                              FromMonth = d.FromMonth,
                              ToMonth = d.ToMonth,
                              BasedChange = d.BasedChange,
                              ChangeTypeName = t.VNName,
                              Checked = false
                          };
            return result.ToList();
        }
    }
}
