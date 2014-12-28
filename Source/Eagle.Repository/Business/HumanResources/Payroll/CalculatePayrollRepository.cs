using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//

using System.Web.Routing;
using Eagle.Common.Helpers;
using System.Data.Entity.Validation;
using Eagle.Common.Utilities;
using Eagle.Core;
using Eagle.Model;
namespace Eagle.Repository
{
    public class CalculatePayrollRepository
    {
        public EntityDataContext context { get; set; }
        public CalculatePayrollRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public List<PayrollViewModels> List(SearchForPayrollViewModels model, bool isAdmin, string userGroupID, int moduleID, int LanguageId)
        {
            try
            {
                // phan quyen du lieu
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
                // tim kiem theo cay thu muc cong ty
                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();
                
                int iYear = Int32.Parse(model.MMYYYY.Substring(3,4));
                int iMonth  = Int32.Parse(model.MMYYYY.Substring(0,2));
                return (from vP in context.PR_vPayRoll
                        join e in context.HR_tblEmp on vP.EmpID equals e.EmpID
                        join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID into JoinPosition
                        from p in JoinPosition.DefaultIfEmpty()
                        join c in context.LS_tblCompany on e.LSCompanyID equals c.LSCompanyID
                        where (model.LSCompanyID == 0 || model.LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                        && (model.EmpCode == null || e.EmpCode.Contains(model.EmpCode))
                        && (model.FullName == null || (e.LastName + " " + e.FirstName).Contains(model.FullName))
                        && jointable.Contains(e.EmpID) // join voi bang phan quyen du lieu
                        && vP.Year == iYear
                        && vP.Month == iMonth
                        select new PayrollViewModels()
                        {

                            EmpCode = e.EmpCode,
                            FullName = e.LastName + " " + e.FirstName,
                            Department = LanguageId == 124 ? c.Name : c.VNName,
                        }).ToList();
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return new List<PayrollViewModels>();
            }
        }
    }
}
