using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Repository
{
    public class SYS_tblDataPermissionRepository
    {
        public EntityDataContext context { get; set; }
        public SYS_tblDataPermissionRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public bool Remove(int GroupId, int ModuleId, int? LSCompanyID, out string ErrorMessage)
        {
            try
            {
                var dpList = context.SYS_tblDataPermission.Where(p => p.GroupID == GroupId &&
                                                                        p.ModuleID == ModuleId).ToList();
                foreach (SYS_tblDataPermission item in dpList)
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
                context.SaveChanges();
                ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool Update(int GroupId, int ModuleId,int? LSCompanyID, List<int?> CheckSection,out string ErrorMessage)
        {
            try
            {

                string Lineage = context.LS_tblCompany.Where(p => p.LSCompanyID == LSCompanyID).FirstOrDefault().Lineage;
                List<int> AllCompany = context.LS_tblCompany.Where(p => p.Lineage.StartsWith(Lineage)).Select(p => p.LSCompanyID).ToList();

                //Tất cả các DataPerission trong database
                var dpList = context
                    .SYS_tblDataPermission
                    .Where(p => p.GroupID == GroupId &&
                                p.ModuleID == ModuleId
                                && AllCompany.Contains(p.LSCompanyID.Value)
                                ).ToList();
                //Nếu bị uncheck trên giao diện => delete trong database
                foreach (var dp in dpList)
                {
                    if (!CheckSection.Contains(dp.LSCompanyID))
                    {
                        context.Entry(dp).State = System.Data.Entity.EntityState.Deleted;
                    }
                }

                foreach (int CompanyID in CheckSection)
                {
                    // nếu không có trong database => insert
                    //Nếu không có trong database
                    if (dpList.SingleOrDefault(p => p.LSCompanyID == CompanyID) == null)
                    {
                        if (context.SYS_tblDataPermission.SingleOrDefault(p => p.LSCompanyID == CompanyID
                            && p.ModuleID == ModuleId
                            && p.GroupID == GroupId) == null)
                        {
                            //Insert vào
                            SYS_tblDataPermission model = new SYS_tblDataPermission();
                            model.GroupID = GroupId;
                            model.ModuleID = ModuleId;
                            model.LSCompanyID = CompanyID;
                            context.Entry(model).State = System.Data.Entity.EntityState.Added;
                        }
                    }
                }
                context.SaveChanges();
                ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
    }
}
