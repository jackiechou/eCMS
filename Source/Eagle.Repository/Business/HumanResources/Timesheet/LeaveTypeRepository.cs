using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Eagle.Model;
using Eagle.Core;
using System.Data.Entity.Validation;
namespace Eagle.Repository
{
   public class LeaveTypeRepository
    {
        public EntityDataContext context { get; set; }
        public LeaveTypeRepository(EntityDataContext context)
        {
            this.context = context;
        }
        public bool CheckExist(string strValidate)
        {
            var code = context.LS_tblLeaveType.FirstOrDefault(p => p.LSLeaveTypeCode.Equals(strValidate));
            return (code != null);
        }
       /// <summary>
       /// Kiem tra duplicate code  khi edit cho truong hop cho chinh sua Code
       /// </summary>
       /// <param name="strValidate"></param>
       /// <returns></returns>
        public bool CheckExistEdit(string strValidate, int LSLeaveTypeID)
        {
            var code = context.LS_tblLeaveType
                        .FirstOrDefault(p => p.LSLeaveTypeCode.Equals(strValidate) && p.LSLeaveTypeID != LSLeaveTypeID );
            return (code != null);
        }
        public List<LeaveTypeViewModel> List()
        {

            try
            {
                return (from p in context.LS_tblLeaveType
                        orderby p.Name
                          select new LeaveTypeViewModel()
                          {
                              LSLeaveTypeID = p.LSLeaveTypeID,
                              LSLeaveTypeCode = p.LSLeaveTypeCode,
                              Name = p.Name,
                              Note = p.Note
                          }).ToList();

                //List<LeaveTypeViewModel> ret = new List<LeaveTypeViewModel>();
                //foreach (var item in tmp)
                //{
                //    LeaveTypeViewModel s = new LeaveTypeViewModel()
                //    {
                //        LSLeaveTypeID = item.LSLeaveTypeID,
                //        LSLeaveTypeCode = item.LSLeaveTypeCode,
                //        Name = item.Name,
                //        Note = item.Note
                //    };
                //    ret.Add(s);
                //}
                //return ret;
            }
            catch
            {
                return new List<LeaveTypeViewModel>();
            }
        }
        public LS_tblLeaveType Find(int id)
        {
            return context.LS_tblLeaveType.Find(id);
        }
        public string  Add(LS_tblLeaveType model)
        {
            try
            {
                if (CheckExist(model.LSLeaveTypeCode))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                context.Entry(model).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return "success";
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return "error";
            }
        }

        public string  Update(LeaveTypeCreateViewModel model)
        {

            try
            {
                if (CheckExistEdit(model.LSLeaveTypeCode,model.LSLeaveTypeID))
                {
                    string errorMessage = Eagle.Resource.LanguageResource.ExistCode;
                    return errorMessage;
                }
                LS_tblLeaveType modelUpdate = Find(model.LSLeaveTypeID);
                modelUpdate.LSLeaveTypeCode = model.LSLeaveTypeCode;
                modelUpdate.Name = model.Name;
                modelUpdate.Note = model.Note;
                context.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                return strErr;
            }
        }

        public LeaveTypeCreateViewModel FindEdit(int id)
        {
            try
            {
                LeaveTypeCreateViewModel ret = context.LS_tblLeaveType
                                        .Where(p => p.LSLeaveTypeID == id)
                                        .Select(p => new LeaveTypeCreateViewModel()
                                        {
                                            LSLeaveTypeID = p.LSLeaveTypeID,
                                            LSLeaveTypeCode = p.LSLeaveTypeCode,
                                            Name = p.Name,
                                            Note = p.Note
                                        }).FirstOrDefault();
                return ret;
            }
            catch
            {
                return new LeaveTypeCreateViewModel();
            }

        }
        public bool Delete(int id)
        {
            try
            {
                LS_tblLeaveType modelUpdate = Find(id);
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
        private List<AutoCompleteViewModel> GetDropdownList(string searchTerm)
        {
            var tmp = context.LS_tblLeaveType
                           .Where(c => c.Name.Contains(searchTerm))
                           .Select(c => new
                           {
                               id = c.LSLeaveTypeID,
                               name = c.Name,
                               text = ""
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
