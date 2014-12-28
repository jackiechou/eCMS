using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Model;

namespace Eagle.Repository
{
    public class OnlineProcessMasterRepository
    {
        public EntityDataContext context { get; set; }
        public OnlineProcessMasterRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<OnlineProcessMasterViewModel> GetAll(int LanguageId = 10001)
        {
            try
            {   
                var result = from p in context.SYS_tblProcessOnlineMaster
                             join Process in context.LS_tblOnlineProcess on p.DMQuiTrinhID equals Process.DMQuiTrinhID into List1
                             from l1 in List1.DefaultIfEmpty()
                             select new OnlineProcessMasterViewModel()
                             {
                                 OnlineProcessID = p.OnlineProcessID,
                                 DMQuiTrinhID = p.DMQuiTrinhID,
                                 StatusLevel1A = p.StatusLevel1A,
                                 StatusLevel1U = p.StatusLevel1U,
                                 StatusLevel2A = p.StatusLevel2A,
                                 StatusLevel2U = p.StatusLevel2U,
                                 StatusLevel3A = p.StatusLevel3A,
                                 StatusLevel3U = p.StatusLevel3U,
                                 StatusLevel4A = p.StatusLevel4A,
                                 StatusLevel4U = p.StatusLevel4U,
                                 StatusLevel5A = p.StatusLevel5A,
                                 StatusLevel5U = p.StatusLevel5U,
                                 Description = p.Description,
                                 NameProcessOnline = l1.NameProcessOnline
                                
                             };


                return result.ToList();
            }
            catch
            {
                return new List<OnlineProcessMasterViewModel>();
            }
        }

        public bool Add(SYS_tblProcessOnlineMaster model,out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                //Kiểm tra xem Quy trình này đã được thiết lập chưa
                if (OnlineProcessExists(model.DMQuiTrinhID))
                {
                    ErrorMessage = Eagle.Resource.LanguageResource.OnlineProcessExistsErrorMessage;
                    return false;
                }

                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return true;
            }
            catch
            {
                ErrorMessage = Eagle.Resource.LanguageResource.SystemError;
                return false;
            }
        }

        private bool OnlineProcessExists(int DMQuiTrinhID, int? ValidOnlineProcessID = null)
        {
            if (ValidOnlineProcessID == null)
            {
                var OnlineProcess = context.SYS_tblProcessOnlineMaster.SingleOrDefault(p => p.DMQuiTrinhID == DMQuiTrinhID);
                return OnlineProcess != null;
            }
            else
            {
                var OnlineProcess = context.SYS_tblProcessOnlineMaster.SingleOrDefault(p => p.DMQuiTrinhID == DMQuiTrinhID && p.OnlineProcessID != ValidOnlineProcessID);
                return OnlineProcess != null;
            }
            
        }

        public SYS_tblProcessOnlineMaster Find(int id)
        {
            return context.SYS_tblProcessOnlineMaster.Find(id);
        }

        public bool Update(SYS_tblProcessOnlineMaster model, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            { 
                //Kiểm tra xem Quy trình này đã được thiết lập chưa
                if (OnlineProcessExists(model.DMQuiTrinhID, model.OnlineProcessID))
                {
                    ErrorMessage = Eagle.Resource.LanguageResource.OnlineProcessExistsErrorMessage;
                    return false;
                }
                context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch
            {
                ErrorMessage = Eagle.Resource.LanguageResource.SystemError;
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var model = Find(id);
                if (model != null)
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
