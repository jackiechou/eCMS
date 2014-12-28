using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Eagle.Model;
using Eagle.Model.HR;
using Eagle.Core;
using System.Transactions;
using System.Data.Entity.Validation;
namespace Eagle.Repository
{
    public class IndividualOTRepository
    {
        public EntityDataContext context { get; set; }
        public IndividualOTRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public IEnumerable<IndividualOTListViewModel> Search(string strEmpCode, string strEmpName, int? LSCompanyID, bool isAdmin, string userGroupID, int moduleID)
        {

            try
            {
                // phan duyen du lieu  va tim kiem phong ban theo cay danh muc
                var jointable = context.SYS_spfrmDataPermission(userGroupID, moduleID).ToList();
                var Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID).Select(p => p.Lineage).FirstOrDefault();
                var allChildenCompany = context.LS_tblCompany.Where(p => p.Lineage.Contains(Lineage)).Select(p => p.LSCompanyID).ToList();

                return from d in context.Timesheet_tblOTRecordDetail
                       join m in context.Timesheet_tblOTRecordMaster on d.OTRecordID equals m.OTRecordID
                       join e in context.HR_tblEmp on m.EmpID equals e.EmpID
                       join p in context.LS_tblPosition on e.LSPositionID equals p.LSPositionID into JoinPosition from p in JoinPosition.DefaultIfEmpty()
                       join c in context.LS_tblCompany on e.LSCompanyID equals c.LSCompanyID into JoinCompany  from c in JoinCompany.DefaultIfEmpty()
                       where (LSCompanyID == 0 || LSCompanyID == null || allChildenCompany.Contains(e.LSCompanyID))
                       && (strEmpCode == null || e.EmpCode.Contains(strEmpCode))
                       && (strEmpName == null || (e.LastName + " " + e.FirstName).Contains(strEmpName))
                       && (/*isAdmin == true ||*/ jointable.Contains(e.EmpID)) // join voi bang phan quyen du lieu
                       && ( m.Status == 0 || m.Status == 1 || m.Status == 2 || m.Status == 4 ||m.Status == 6|| m.Status == 8  ||m.Status == 10)
                       select new IndividualOTListViewModel()
                       {
                           EmpCode = e.EmpCode,
                           FullName = e.LastName + " " + e.FirstName,
                           Department = c.Name,
                           DateID = d.DateID,
                           TimeInAM = d.TimeInAM,
                           TimeOutAM = d.TimeOutAM,
                           TimeInPM = d.TimeInPM,
                           TimeOutPM = d.TimeOutPM,
                           TotalHours= d.TotalHours,
                           NightHours = d.NightOT,
                           HolidayHours = d.HolidayOT,
                           Position  = p.Name
                       };
            }
            catch
            {
                return new List<IndividualOTListViewModel>();
            }
        }
    }
}
