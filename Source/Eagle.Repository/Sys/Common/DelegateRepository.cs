using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Model;

namespace Eagle.Repository
{
    public class DelegateRepository
    {
        public EntityDataContext context { get; set; }
        public DelegateRepository(EntityDataContext context)
        {
            this.context = context;
        }

        public List<DelegateViewModel> GetAll()
        {

            try
            {
                var result = from p in context.SYS_tblDelegate
                             join process in context.LS_tblOnlineProcess on p.DMQuiTrinhID equals process.DMQuiTrinhID
                             select new DelegateViewModel() { DelegateID = p.DelegateID, Account = p.Account, Account_delegate = p.Account_delegate, NameProcessOnline = process.NameProcessOnline, FromDate = p.FromDate, ToDate = p.ToDate };
                return result.ToList();
            }
            catch
            {
                return new List<DelegateViewModel>();
            }
        }

        public SYS_tblDelegate Find(int id)
        {
            return context.SYS_tblDelegate.Find(id);
        }
        public DelegateViewModel FindEdit(int id)
        {
            var model = context.SYS_tblDelegate.Find(id);
            DelegateViewModel ret = new DelegateViewModel()
            {
                Account = model.Account,
                Account_delegate = model.Account_delegate,
                DMQuiTrinhID = model.DMQuiTrinhID,
                FromDateNullable = model.FromDate,
                ToDateNullable = model.ToDate,
                Note = model.Note,
                DelegateID = model.DelegateID
            };
            return ret;
        }
        public bool Delete(int id, out string errorMessage)
        {
            try
            {
                var modelDelete = Find(id);
                context.Entry(modelDelete).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                errorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool Update(DelegateViewModel modelUpdate, out string errorMessage)
        {
            try
            {
                var model = Find(modelUpdate.DelegateID);
                model.DMQuiTrinhID = modelUpdate.DMQuiTrinhID;
                model.Account_delegate = modelUpdate.Account_delegate;
                model.FromDate = modelUpdate.FromDate;
                model.ToDate = modelUpdate.ToDate;
                model.Editer = modelUpdate.Editer;
                model.Note = modelUpdate.Note;
                model.EditTime = DateTime.Now;

                //Kiem tra da ton tai
                var check = context.SYS_tblDelegate.Where(p => p.DMQuiTrinhID == modelUpdate.DMQuiTrinhID &&
                    p.DelegateID != modelUpdate.DelegateID &&
                    ((p.FromDate.CompareTo(modelUpdate.FromDate) <= 0 && p.ToDate.CompareTo(modelUpdate.FromDate) >= 0) ||
                    (p.FromDate.CompareTo(modelUpdate.ToDate) <= 0 && p.ToDate.CompareTo(modelUpdate.ToDate) >= 0)
                    )
                    ).FirstOrDefault();

                if (check != null)
                {
                    errorMessage = string.Format("Quy trình này đã được ủy quyền từ ngày {0} tới ngày {1} cho tài khoản \"{2}\"", check.FromDate.ToString("dd/MM/yyyy"), check.ToDate.ToString("dd/MM/yyyy"), check.Account_delegate);
                    return false;
                }



                context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                errorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool Add(SYS_tblDelegate modelAdd, out string errorMessage)
        {
            try
            {
                //Kiem tra da ton tai
                var check = context.SYS_tblDelegate.Where(p => p.DMQuiTrinhID == modelAdd.DMQuiTrinhID &&
                    ((p.FromDate.CompareTo(modelAdd.FromDate) <= 0 && p.ToDate.CompareTo(modelAdd.FromDate) >=0) ||
                    (p.FromDate.CompareTo(modelAdd.ToDate) <= 0 && p.ToDate.CompareTo(modelAdd.ToDate) >= 0)
                    )
                    ).FirstOrDefault();

                if (check != null)
                {
                    errorMessage = string.Format("Quy trình này đã được ủy quyền từ ngày {0} tới ngày {1} cho tài khoản \"{2}\"",check.FromDate.ToString("dd/MM/yyyy"),check.ToDate.ToString("dd/MM/yyyy"),check.Account_delegate);
                    return false;
                }
                //them
                context.Entry(modelAdd).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                errorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
